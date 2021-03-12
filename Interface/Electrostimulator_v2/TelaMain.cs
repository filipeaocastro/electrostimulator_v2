using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using System.Threading;
using System.Management;
using System.Diagnostics;
using System.Security.Permissions;

namespace Eletroestimulador_v02
{
    public partial class TelaMain : Form
    {
        // Dados da comunicação serial
        private string busDescriptionESP = "CP2102 USB to UART Bridge Controller";  // ESP32 Bus description (string that will
                                                                                    // be searched to identify the ESP32
        private SerialPort ESPSerial;
        private bool serialConectada = false;

        private List<TextBox> textBoxes = new List<TextBox>();  // List to store the textboxes

        // Stimulation states
        private enum estados_estimulacao
        {
            ATIVO,          // Active
            ATUALIZADO,     // Updated
            DESATUALIZADO   // Not updated
        }

        // Set the initial state
        estados_estimulacao estadoAtual = estados_estimulacao.DESATUALIZADO;

        // Thread to control the data flow through serial port
        Thread th;

        public TelaMain()
        {
            InitializeComponent();

            // Control the activation of the parameters exclusive to square wave (pulse width)
            if (radioButton_tipoQuadrada.Checked)
                ativaOndaQ(true);
            else
                ativaOndaQ(false);

            adicionaTBs();  // Include the textboxes in the list
        }

        #region Funções de configuração da serial e thread de leitura

        /**
         * Função de leitura da porta serial do ESP que roda como uma Thread em background
         */
        private void rotinaLerSerial()
        {
            // Loop que funciona enquanto a porta com o ESP estiver aberta
            while (ESPSerial.IsOpen)
            {
                // Toda vez que chega um dado em forma de linha (terminado com '\n') ele é lido
                //  caso o dado seja igual a "STOPED" ou "INITIATED" ele atualiza o form
                if (ESPSerial.BytesToRead > 0)
                {
                    string txt = ESPSerial.ReadLine();
                    Console.WriteLine(txt);
                    string newLabel = "";
                    string[] split = txt.ToString().Split('-');

                    // STOPPED quer dizer que a estimulação chegou ao fim, então a label do botão muda 
                    //  pra "Iniciar"
                    if (txt.ToString().Equals("STOPPED"))
                    {
                        newLabel = "Iniciar";
                        changeState(newLabel);  // Função que muda o estado dos botões
                    }
                        

                    // Caso for INITIATED, a label muda pra "Parar"
                    else if (txt.ToString().Equals("INITIATED"))
                    {
                        newLabel = "Parar";
                        changeState(newLabel);  // Função que muda o estado dos botões
                    }
                        

                    // Ele retorna OK! quando atualizamos os dados no ESP e o botão vira "Iniciar"
                    else if (txt.ToString().Equals("OK!"))
                    {
                        newLabel = "Iniciar";
                        changeState(newLabel);  // Função que muda o estado dos botões
                    }

                    
                    // Retorno da leitura de corrente de saída
                    else if(split[0].Equals("ANL"))
                    {
                        double ANLvalue = (Convert.ToInt32(split[1]) * 3.3) / 4095; // Convert from bits to Volts
                        ANLvalue /= 1000;       // Convert from Volts to Ampères
                        ANLvalue *= 1000000;    // Convert from Ampères to μA
                        changeLabelAnalog(Convert.ToInt32(ANLvalue).ToString() + " μA"); // Change the label
                    }
                }
            }
        }

        // Como a thread de leitura tenta alterar um parâmetro da thread principal (que rege o form) ela
        //  precisa de um delegado que solicita essa mudança para a thread principal, evitando uma exceção
        private delegate void changeStateDelegate(string newLabel);

        // Função que chama o delegado para alterar o parâmetro, caso necessário
        private void changeState(string newLabel)
        {
            // Caso a thread principal esteja usando o botão, ele invoca o delegado para que ele seja alterado
            if (this.button_iniciar.InvokeRequired)
            {
                object[] args = new object[] { newLabel };
                changeStateDelegate changeState_Delegate = changeLabel;
                this.Invoke(changeState_Delegate, args);
            }
            // Caso contrário, apenas muda o botão
            else
                changeLabel(newLabel);
        }

        // Função que ativa e desativa o botão de 'Parar'
        // >> Ressaltando que existe um evento ligado à mudança da label do botão, que altera a 
        //      máquina de estados do sistema
        void changeLabel(string newLabel)
        {
            button_iniciar.Text = newLabel;
        }


        /**
        * Essa função procura nos dispositivos do Windows um que possua o nome definido pelo barramento
         "CP2102 USB to UART Bridge Controller", identifica a porta serial deste e abre a porta.
        * 
        * Retorna se a conexão ocorreu com ou sem sucesso.
        */
        private bool conectarESP()
        {
            List<Win32DeviceMgmt.DeviceInfo> devices = new List<Win32DeviceMgmt.DeviceInfo>();
            // Adiciona todas as portas COM conectadas ao Windows à lista
            foreach (Win32DeviceMgmt.DeviceInfo dev in Win32DeviceMgmt.GetAllCOMPorts())
            {
                devices.Add(dev);
            }
            bool deviceFound = false;

            // Compara o nome descrito pelo barramento de todos os devices
            foreach (Win32DeviceMgmt.DeviceInfo dev in devices)
            {
                if (dev.bus_description.ToLower().Equals(busDescriptionESP.ToLower()))
                {
                    //Localizei um port COM para comunicação USB via CABO.
                    ESPSerial.PortName = dev.name;
                    //Abrir port encontrado
                    abrirConexaoESP();
                    deviceFound = true;
                }
            }

            // Mostra mensagem de erro caso o dispositivo não tenha sido encontrado
            if (!deviceFound)
            {
                MessageBox.Show("Dispositivo " + busDescriptionESP + " não encontrado!", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return deviceFound;
        }

        /**
         *  Função que abre a porta serial com o ESP e deixa ela pronta para ser utilizada
         */
        private void abrirConexaoESP()
        {
            try
            {
                ESPSerial.Open();   // Abre a porta
                ESPSerial.DiscardInBuffer();    // Descarta os dados do buffer de entrada
                MessageBox.Show("Porta serial aberta com sucesso!", "Porta serial aberta", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                serialConectada = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        // Função de delegado para alterar a label que indica a corrente de saída
        #region AnalogDelegate
        // Como a thread de leitura tenta alterar um parâmetro da thread principal (que rege o form) ela
        //  precisa de um delegado que solicita essa mudança para a thread principal, evitando uma exceção
        private delegate void changeLabelAnalogDelegate(string newLabel);

        private void changeLabelAnalog(string newLabel)
        {
            // Caso a thread principal esteja usando o botão, ele invoca o delegado para que ele seja alterado
            if (this.label_ANLvalue.InvokeRequired)
            {
                object[] args = new object[] { newLabel };
                changeLabelAnalogDelegate changeLabelAnalog_Delegate = changeLabelANL;
                this.Invoke(changeLabelAnalog_Delegate, args);
            }
            // Caso contrário, apenas muda o botão
            else
                changeLabelANL(newLabel);
        }

        // Função que ativa e desativa o botão de 'Parar'
        // >> Ressaltando que existe um evento ligado à mudança da label do botão, que altera a 
        //      máquina de estados do sistema
        void changeLabelANL(string newLabel)
        {
            label_ANLvalue.Text = newLabel;
        }
        #endregion


        /*
         *  Fluxo de funcionamento do envio de dados e mudança de estados
         *  
         *  info: O botão Iniciar/Parar/Atualizar são o mesmo botão, alterando apenas a label.
         *  
         *  Existem 3 estados do sistema: 
         *  => DESATUALIZADO: Quando o ESP não recebeu os dados mais recentes das textboxes, assim o
         *  botão Atualizar/Iniciar fica com a label "Atualizar".
         *  => ATUALIZADO: Quando o ESP está configurado com as informações atuais das textboxes. Assim
         *  o botão Atualizar/Iniciar fica com a label "Iniciar".
         *  => ATIVO: Quando o ESP está fazendo a eletroestimulação, assim o botão Atualizar/Iniciar
         *  fica com a label "Parar".
         *  
         *  O sistema inicia no estado DESATUALIZADO. Ao preencher as textboxes, o usuário pode clicar
         *  em Atualizar e enviar os dados ao ESP. Quando isso acontece, o ESP retorna um "OK!" e só
         *  então o botão Iniciar torna-se disponível. Ao clicar nele o sistema envia o ESP o comando
         *  para iniciar a eletroestimulação, e o mesmo retorna o comando "INITIATED", e só então
         *  o botão Parar torna-se disponível. Ao clicar nele, o sistema envia ao ESP o comando de parar
         *  e o mesmo retorna "STOPPED", e só então o botão Iniciar torna-se disponível.
         *  Caso o botão Iniciar esteja ativo e ocorrer qualquer alteração nas textboxes o mesmo volta
         *  para Atualizar, já que os dados enviados pelo ESP da última vez são diferentes dos presentes
         *  nas textboxes.
         *  
         *  Note que o estado do botão Iniciar/Parar/Atualizar apenas é atualizado quando o ESP responde
         *  ao comando que o sistema enviou. Logo, o sistema e o ESP são codependentes quanto ao seu 
         *  funcionamento.
         * 
         */

        #region Eventos dos botões de Atualizar/Iniciar e Conectar


        /*
         * Função que inicia a comunicação serial com o ESP e ativa a thread responsável por essa
         * comunicação em background
         */ 
        private void button_conectar_Click(object sender, EventArgs e)
        {
            try
            {
                // Cria o objeto de porta serial e inicia com velocidade de 115200 bps
                ESPSerial = new SerialPort();
                ESPSerial.BaudRate = 115200;

                serialConectada = conectarESP();    // Inicia a conexão
               

                // Se a conexão inicar com sucesso o programa habilita os controles e inicia a thread
                //  de leitura da serial
                if (serialConectada)
                {
                    // Inicia a thread de leitura da serial
                    th = new Thread(rotinaLerSerial);
                    th.IsBackground = true;
                    th.Start();

                    button_iniciar.Enabled = true;  // Ativa o botão iniciar
                    label_ANL.Enabled = true;
                    label_ANLvalue.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button_iniciar_Click(object sender, EventArgs e)
        {
            // Proteção contra valores nulos nas textboxes
            // Caso uma delas não possua um valor inserido, o sistema não permite que dados sejam
            // enviados ao ESP.
            foreach (TextBox tb in textBoxes)
            {
                if ((tb.Text == null) || (tb.Text == ""))
                {
                    MessageBox.Show("Algum campo não foi preenchido!",
                        "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            /*
             * Switch (case) que define quais comandos serão enviados ao ESP de acordo com o estado
             * do sistema quando ocorrer o clique no botão Iniciar/Atualizar/Parar
             * 
             * Se o sistema estiver ATIVO (eletroestimulação acontecendo) o botão estará com a label 
             * "Parar", então o comando de parar a eletroestimulação é enviado ao ESP.
             * 
             * Se o sistema estiver ATUALIZADO (eletroestimulação pronta parar acontecer) o botão 
             * estará com a label "Iniciar", então o comando de iniciar a eletroestimulação é 
             * enviado ao ESP.
             * 
             * Se o sistema estiver DESATUALIZADO (dados presentes no ESP diferentes dos presentes nas
             * textboxes) o botão estará com a label Atualizar, então os parâmetros atualizados serão
             * enviados ao ESP.
             */ 
            try
            {
                switch (estadoAtual)
                {
                    case estados_estimulacao.ATIVO:
                        // Enviar comando p/ PARAR e o estado só muda quando o ESP avisar que parou
                        ESPSerial.WriteLine(Protocolos.parar);
                        break;

                    case estados_estimulacao.ATUALIZADO:
                        // Enviar comando p/ INICIAR e só mudar o estado quando o ESP avisar que iniciou
                        ESPSerial.WriteLine(Protocolos.iniciar);
                        break;

                    case estados_estimulacao.DESATUALIZADO:
                        // Enviar comandos p/ atualizar os dados do ESP e só mudar o estado quando o ESP der OK!
                        string dados = coletaDados();
                        ESPSerial.Write(dados);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /*
         * A parte do sistema que muda a label do botão Iniciar é a thread de leitura da serial. Portanto,
         * quando ocorre a mudança na label, este evento é ativado e o estado do sistema é alterado de
         * acordo com a nova label do botão.
         */ 
        private void button_iniciar_TextChanged(object sender, EventArgs e)
        {
            string texto = button_iniciar.Text;

            // Switch (case) que relaciona a label com a alteração de estado do sistema
            switch (texto)
            {
                case "Atualizar":
                    estadoAtual = estados_estimulacao.DESATUALIZADO;
                    enableTBs(true);
                    break;

                case "Parar":
                    estadoAtual = estados_estimulacao.ATIVO;
                    enableTBs(false);
                    break;

                case "Iniciar":
                    estadoAtual = estados_estimulacao.ATUALIZADO;
                    enableTBs(true);
                    break;
            }
        }

        #endregion

        #region Funções complementares

        /**
        * Função que pega os dados de todas as text boxes e outros campos do form e forma uma string que 
        * compila todos os códigos de protocolo com seus respectivos valores para que sejam enviados ao 
        * Arduino de forma correta.
        */
        private string coletaDados()
        {
            //bool rnd_on = checkBox_intervaloTBaleat.Checked;
            string dados = "";

            // Chama a função Cods() da classe Protocolos que coloca os códigos correspondentes a cada textbox
            // num objeto do tipo List
            Protocolos protocolos = new Protocolos();
            List<string> codigos = protocolos.Cods();

            // Engloba os códigos e seus respectivos valores (duas listas) num só objeto iterável
            var comandos = textBoxes.Zip(codigos, (val, cod) => new { Valor = val, Codigos = cod });

            // Coloca os protocolos em sequência com o valor => AAA-XXXX (AAA = código, XXXX = valor)
            foreach (var ad in comandos)
            {
                dados += ad.Codigos + ad.Valor.Text + "\n";
            }

            dados += verificaDirecao(); // Verifica os radiobuttons de direção da corrente
            dados += verificaOnda();    // Verifica os radiobuttons relacionados à forma de onda

            /* Devido ao sistema trabalhar com uma precisão de 1 us e resolução de 256 pontos, frequências
                acima de 3900 Hz deixam o passo entre cada ponto menor que 1 us. Exemplo:
                Frequência: 3900 Hz, Período = 256 us 
                Passo entre cada amostra: Período/pontos = 256/256 = 1 us

                
                Frequência = 4000, Período = 250 us
                Passo entre cada amostra: Período/pontos = 250/256 = 0.98 us
                Como a resolução do sistema é de 1 us, passos menores não são possíveis.

                Para aumentar a resolução, deve-se diminuir a quantidade de pontos (seria a resulução 
                da onda, nesse caso)
            */
            if(Convert.ToInt32(textBox_freq.Text) > 3900)
                MessageBox.Show("O sistema pode não operar com a precisão necessária em frequências " +
                    "acima de 3900 Hz!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            // Escreve os dados a serem enviado no console, para fins de depuração
            Console.Write(dados);

            return dados;
        }

        /*
         * Adiciona as textboxes da interface na lista "textBoxes"
         */
        private void adicionaTBs()
        {
            textBoxes.Add(textBox_amplitude);
            textBoxes.Add(textBox_freq);
            textBoxes.Add(textBox_duracao);
            textBoxes.Add(textBox_pulseWd);
        }

        // Closes the application (not just the window)
        private void fechaApp(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }


        /*
         * Confere os radiobuttons relacionados à forma de onda e retorna o código de protocolo 
         * relacionado à mesma
         */
        private string verificaOnda()
        {
            if (radioButton_tipoSenoide.Checked == true)
                return Protocolos.wf_sin + "\n";
            else if (radioButton_tipoQuadrada.Checked == true)
                return Protocolos.wf_square + "\n";
            else if (radioButton_tipoTriangular.Checked == true)
                return Protocolos.wf_triangular + "\n";
            else //(radioButton_tipoDenteDeSerra.Checked == true)
                return Protocolos.wf_sawtooth + "\n";
        }

        /*
         * Confere os radiobuttons relacionados à direção da corrente e retorna o código de protocolo 
         * relacionado à mesma
         */
        private string verificaDirecao()
        {
            if (radioButton_tipoAnodica.Checked == true)
                return Protocolos.iDirection_anodic + "\n";
            else if (radioButton_tipoCatodica.Checked == true)
                return Protocolos.iDirection_cathodic + "\n";
            else
                return Protocolos.iDirection_biDirectional + "\n";
        }

        /*
         * Quando chamada, essa função muda o estado do sistema para DESATUALIZADO e muda a label
         * do botão Iniciar para "Atualizar"
         */ 
        private void mudaLabelAtualizar()
        {
            estadoAtual = estados_estimulacao.DESATUALIZADO;
            button_iniciar.Text = "Atualizar";
        }

        private void enableTBs(bool enabled)
        {
            foreach (TextBox tb in textBoxes)
                tb.Enabled = enabled;
        }

        // Event that is called when the radio buttons check change
        private void radioButton_tipoQuadrada_CheckedChanged(object sender, EventArgs e)
        {
            // Control the activation of the parameters exclusive to square wave (pulse width)
            if (radioButton_tipoQuadrada.Checked)
                ativaOndaQ(true);
            else
                ativaOndaQ(false);
        }

        // Control the activation of the parameters exclusive to square wave (pulse width)
        private void ativaOndaQ(bool ativa)
        {
            label_pulseWd.Enabled = ativa;
            label_usPulseWd.Enabled = ativa;
            textBox_pulseWd.Enabled = ativa;

        }

        #endregion


        /*
         * Sempre que o texto de uma texbox é alterado, significa que os dados presentes no ESP estão
         * desatualizados em relação aos presentes nas textboxes. Portanto, sempre que ocorre uma 
         * alteração nas mesmas, o botão Iniciar muda sua label para Atualizar e o sistema muda seu
         * estado para DESATUALIZADO.
         */
        #region Eventos das textboxes para avisar que houve alterações

        private void textBox_amplitude_TextChanged(object sender, EventArgs e)
        {
            mudaLabelAtualizar();
        }

        private void textBox_freq_TextChanged(object sender, EventArgs e)
        {
            mudaLabelAtualizar();
        }

        private void textBox_duracao_TextChanged(object sender, EventArgs e)
        {
            mudaLabelAtualizar();
        }

        private void textBox_pulseWd_TextChanged(object sender, EventArgs e)
        {
            mudaLabelAtualizar();
        }

        #endregion
    }
}
