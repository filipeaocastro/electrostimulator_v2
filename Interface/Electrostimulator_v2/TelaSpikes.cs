using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Security;
using System.IO.Ports;
using System.Threading;
using System.Diagnostics;


namespace Eletroestimulador_v02
{
    public partial class SpikesForm : Form
    {
        // Dados da comunicação serial
        private string busDescriptionESP = "CP2102 USB to UART Bridge Controller";
        public SerialPort ESPSerial;
        private bool serialConectada = false;
        public Thread th;

        spkParameters spikeParameters;

        Stopwatch intervalo_echo;
        private long nanosecPerTick = (1000L * 1000L * 1000L) / Stopwatch.Frequency;
        private long tempoDecorrido = 0;

        private int amplitude = 3000;
        public int textureNumber = 0; // 0 = random
        private int spk_width = 1000;

        List<string[,]> textureList = new List<string[,]>();



        OpenFileDialog openFileDialog1;
        private FolderBrowserDialog FolderBrowserDialog;
        private string spikesTxt = "";
        private bool[] spikes;
        private UInt16 index_spk = 0;
        private int duration = 0;
        private int samples = 0;

        private string txtPath;

        StreamReader confFileOutputR;
        StreamWriter confFileOutputW;
        private bool confFileExist;
        private string confFileName = "conf.txt";
        private string confDocPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private string confFilePath;

        private int totalTextures = 0;
        private int aleatTexture = 1;
        Random rand;

        public event EventHandler StateChanged;

        private TestProtocol testProtocol;
        public bool testProtocolOn = false;
        public int stateProtocol = 0;

        private enum estados_estimulacao
        {
            ATIVO,
            ATUALIZADO,
            DESATUALIZADO
        }
        estados_estimulacao estadoAtual = estados_estimulacao.DESATUALIZADO;

        public SpikesForm()
        {
            InitializeComponent();
            setTimer();
            setConfFile();
            
            
        }

        #region Funções de configuração da serial e thread de leitura

        /**
         * Função de leitura da porta serial do ESP que roda como uma Thread em background
         */
        public void rotinaLerSerial()
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

                    // STOPPED quer dizer que a estimulação chegou ao fim, então a label do botão muda 
                    //  pra "Iniciar"
                    if (txt.ToString().Equals("STOPPED"))
                    {
                        if(testProtocolOn)
                        {
                            stateProtocol = 0;
                        }
                        else
                        {
                            newLabel = "Start";
                            changeState(newLabel);  // Função que muda o estado dos botões
                        }
                        
                    }


                    // Caso for INITIATED, a label muda pra "Parar"
                    else if (txt.ToString().Equals("INITIATED"))
                    {
                        if (testProtocolOn)
                        {
                            stateProtocol = 1;
                        }
                        else
                        {
                            newLabel = "Stop";
                            changeState(newLabel);  // Função que muda o estado dos botões
                        }
                    }


                    // Ele retorna OK! quando atualizamos os dados no ESP e o botão vira "Iniciar"
                    else if (txt.ToString().Equals("OK!"))
                    {
                        if (testProtocolOn)
                        {
                            stateProtocol = 2;
                        }
                        else
                        {
                            newLabel = "Start";
                            changeState(newLabel);  // Função que muda o estado dos botões
                        }
                    }
                }
            }
        }

        // Como a thread de leitura tenta alterar um parâmetro da thread principal (que rege o form) ela
        //  precisa de um delegado que solicita essa mudança para a thread principal, evitando uma exceção
        private delegate void changeStateDelegate(string newLabel);

        private void changeState(string newLabel)
        {
            // Caso a thread principal esteja usando o botão, ele invoca o delegado para que ele seja alterado
            if (this.button_update.InvokeRequired)
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
            button_update.Text = newLabel;
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

        #region Buttons events

        private void button_connectUc_Click(object sender, EventArgs e)
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

                    button_update.Enabled = true;  // Ativa o botão iniciar
                    button_connectUc.Enabled = false;   // Desativa o botão de conectar
                    button_initProtocol.Enabled = true; // Ativa botão do protocolo
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            // Proteção contra valores nulos nas textboxes
            // Caso uma delas não possua um valor inserido, o sistema não permite que dados sejam
            // enviados ao ESP.
            //foreach (TextBox tb in textBoxes)
            ///{
            ///
            /*
            if (textBox_duration.Text == "" || textBox_amplitude.Text == "")
            {
                MessageBox.Show("Algum campo não foi preenchido!",
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }*/
            // }

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
                        //initTimer(false);   // Para o timer, só inicia quando o ESP avisa que pode
                        break;

                    case estados_estimulacao.ATUALIZADO:
                        // Enviar comando p/ INICIAR e só mudar o estado quando o ESP avisar que iniciou
                        ESPSerial.WriteLine(Protocolos.iniciar);
                        //label_fileName.Enabled = false;
                        //textBox_fileName.Enabled = false;
                        break;

                    case estados_estimulacao.DESATUALIZADO:
                        // Enviar comandos p/ atualizar os dados do ESP e só mudar o estado quando o ESP der OK!

                        //string dados = coletaDados();
                        //duration = Convert.ToInt32(textBox_duration.Text);
                        //if (duration == 0)
                        //    duration = spikesTxt.Length;

                        //if(checkBox_applyParameters.Checked == false)
                        //{

                        //}
                        button_update.Enabled = false;
                        readConf();
                        updateTexture(textureNumber);
                        sendData();
                        spikeTransfer();
                        ESPSerial.WriteLine(Protocolos.wf_spike);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button_update_TextChanged(object sender, EventArgs e)
        {
            string texto = button_update.Text;

            // Switch (case) que relaciona a label com a alteração de estado do sistema
            switch (texto)
            {
                case "Update":
                    estadoAtual = estados_estimulacao.DESATUALIZADO;
                    //label_fileName.Enabled = true;
                    //textBox_fileName.Enabled = true;
                    //enableTBs(true);
                    break;

                case "Stop":
                    estadoAtual = estados_estimulacao.ATIVO;
                    //initTimer(true);
                    intervalo_echo = new Stopwatch();
                    intervalo_echo.Start();
                    //enableTBs(false);
                    break;

                case "Start":
                    // ESPSerial.WriteLine(Protocolos.);
                    //initTimer(false);   // Para o timer, só inicia quando o ESP avisa que pode
                    button_update.Enabled = true;
                    estadoAtual = estados_estimulacao.ATUALIZADO;
                    //label_fileName.Enabled = true;
                    //textBox_fileName.Enabled = true;
                    //enableTBs(true);
                    break;
            }
        }

        #endregion

        private void trackBar_spikeWidth_Scroll(object sender, EventArgs e)
        {
            //label_spikeWidthValue.Text = (Convert.ToDouble(trackBar_spikeWidth.Value) / 10.0).ToString() + " ms";
            mudaLabelAtualizar();
        }

        private void HandleStateChanged(object sender, EventArgs e)
        {

        }

        private void setParameterLabels(OpenFileDialog ofd)
        {
            int nSpikes = 0;
            //label_fileNameW.Text = ofd.SafeFileName;
            samples = spikesTxt.Length;
            //label_sampleW.Text = samples.ToString();
            for (int i = 0; i < spikesTxt.Length; i++)
                if (spikesTxt[i] == 1)
                    nSpikes++;
            
        }

        private void setTimer()
        {
            timer_spk.Tick += new EventHandler(timer_spk_Tick);
        }

        private void timer_spk_Tick(object Sender, EventArgs e)
        {
            ESPSerial.WriteLine(spikesTxt[index_spk].ToString());
            index_spk++;
            if (index_spk >= samples)
                index_spk = 0;
            if (index_spk > duration)
                button_update.Text = "Start";
        }

        #region Interface functions

        private void mudaLabelAtualizar()
        {
            estadoAtual = estados_estimulacao.DESATUALIZADO;
            button_update.Text = "Update";
        }

        private void textBox_duration_TextChanged(object sender, EventArgs e)
        {
            mudaLabelAtualizar();
        }

        private void textBox_amplitude_TextChanged(object sender, EventArgs e)
        {
            mudaLabelAtualizar();
        }

        private void radioButton_tipoCatodica_CheckedChanged(object sender, EventArgs e)
        {
            mudaLabelAtualizar();
        }

        private void radioButton_tipoAnodica_CheckedChanged(object sender, EventArgs e)
        {
            mudaLabelAtualizar();
        }

        private void TelaSpikes_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(ESPSerial.IsOpen)
                ESPSerial.WriteLine(Protocolos.parar); 
            Application.Exit();
        }

        private void button_toggleVisible_Click(object sender, EventArgs e)
        {
            mudaLabelAtualizar();
            spikeParameters.Show();
        }

        private void numericUpDown_textureNumber_ValueChanged(object sender, EventArgs e)
        {
            mudaLabelAtualizar();
        }

        private void checkBox_applyParameters_CheckedChanged(object sender, EventArgs e)
        {
            mudaLabelAtualizar();
        }

        #endregion
        /*
         * Carrega a pasta com as texturas de spikes
         */
        private void button_loadTex_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog = new FolderBrowserDialog();
            if (Environment.UserName == "Filipe Augusto")
                FolderBrowserDialog.SelectedPath = "C:\\Users\\Filipe Augusto\\Google Drive\\UFU\\BioLab\\TCC\\texturas";
            else
                FolderBrowserDialog.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            FolderBrowserDialog.Description = "Selecione a pasta com as texturas" + Environment.NewLine 
                + "Cada textura deve ser nomeada 1.txt, 2.txt, ... até 9.txt";

            // Abre o diálogo para escolha da pasta
            if (FolderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var folderPath = FolderBrowserDialog.SelectedPath;
                    Console.WriteLine(folderPath.ToString());
                    string[] files = Directory.GetFiles(FolderBrowserDialog.SelectedPath);
                    foreach (string str in files)
                    {
                        Console.WriteLine(str);
                        isTexture(str);
                    }

                    button_toggleVisible.Enabled = true;
                    button_connectUc.Enabled = true;
                    button_loadTex.Enabled = false;
                    spikeParameters = new spkParameters(amplitude, totalTextures, spk_width, textureNumber);
                }
                catch (SecurityException ex)
                {
                    MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                }
            }
        }

        public void spikeTransfer()
        {
            int count = 0;
            ESPSerial.WriteLine(Protocolos.init_spk_transfer + spikesTxt.Length.ToString());
            Console.WriteLine(Protocolos.init_spk_transfer + spikesTxt.Length.ToString());
            int dif = 0;
            while(count < spikesTxt.Length)
            {
                dif = spikesTxt.Length - count;
                if (dif >= 60)
                {
                    ESPSerial.WriteLine(spikesTxt.Substring(count, 60));
                    //Console.WriteLine(spikesTxt.Substring(count, 60));
                    count += 60;
                }
                else
                {
                    ESPSerial.WriteLine(spikesTxt.Substring(count));
                    count += dif;
                }
                
            }
            ESPSerial.WriteLine(Protocolos.end_spk_transfer);
        }

        private void TelaSpikes_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void TelaSpikes_Click(object sender, EventArgs e)
        {

        }

        private void TelaSpikes_KeyPress(object sender, KeyPressEventArgs e)
        {
            Console.WriteLine("Bateu a tecla");
            Console.WriteLine(e.KeyChar.ToString());
            
        }

        private void saveOutput()
        {
            if (estadoAtual == estados_estimulacao.ATIVO)
            {
                // Enviar comando p/ PARAR e o estado só muda quando o ESP avisar que parou
                ESPSerial.WriteLine(Protocolos.parar);
                if (intervalo_echo.IsRunning)
                    intervalo_echo.Stop();

                try
                {
                    tempoDecorrido = intervalo_echo.ElapsedTicks * nanosecPerTick;
                    tempoDecorrido /= 1000000;

                    string[] lines = {"Spike file path: " + txtPath, "Duration: "
                        + tempoDecorrido.ToString() + " ms"};

                    string docPath =
                    Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    string filename = "teste n" + rand.Next(300).ToString();
                    docPath = Path.Combine(docPath, "saves txt", filename + ".txt");


                    using (StreamWriter outputFile = new StreamWriter(docPath))
                    {
                        foreach (string line in lines)
                            outputFile.WriteLine(line);
                    }
                    

                    MessageBox.Show("This acquisition data was saved on: " + docPath, "Data saved!", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString(), "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
                //initTimer(false);   // Para o timer, só inicia quando o ESP avisa que pode

            }
        }

        private void button_update_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            Console.WriteLine("Bateu a tecla");
           // if (e.KeyCode == Keys.Up)
           //     saveOutput();
        }

        private void setConfFile()
        {

            try
            {
                confFilePath = Path.Combine(confDocPath, confFileName);

                if (File.Exists(confFilePath) == false)
                {
                    confFileOutputW = new StreamWriter(confFilePath);
                    confFileOutputW.WriteLine("Amplitude\t3000" + Environment.NewLine);
                    //confFileOutputW.WriteLine("Current_Direction\tCAT" + Environment.NewLine);
                    confFileOutputW.WriteLine("Texture\t0" + Environment.NewLine);
                    confFileOutputW.WriteLine("Width\t1000" + Environment.NewLine);
                    confFileOutputW.Close();

                }
                readConf();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString(), "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
        private void readConf()
        {
            List<string> vs = new List<string>();
            confFileOutputR = new StreamReader(confFilePath);
            while(!confFileOutputR.EndOfStream)
            {
                vs.Add(confFileOutputR.ReadLine());
            }
            confFileOutputR.Close();
            
            foreach(string str in vs)
            {
                string[] vs1 = str.Split('\t');
                if (vs1[0] == "Amplitude")
                    amplitude = Convert.ToInt32(vs1[1]);
               /* if (vs1[0] == "Current_Direction")
                {
                    if (vs1[1] == "CAT")
                        iDirection = 1;
                    else
                        iDirection = 0;
                }*/
                if(vs1[0] == "Texture")
                {
                    textureNumber = Convert.ToInt32(vs1[1]);
                    Console.WriteLine("Tex number: " + textureNumber.ToString());
                }
                if(vs1[0] == "Width")
                {
                    spk_width = Convert.ToInt32(vs1[1]);
                }
            }
        }
        // Atualiza o arquivo de textura a ser carregado para o uC
        public void updateTexture(int texNumber)
        {
            string texData = "";
            int aleatTex = texNumber;

            // Caso textureNumber seja 0, a textura a ser carregada deve ser selecionada de forma aleatória
            if(textureNumber == 0)
            {
                rand = new Random();
                aleatTex = rand.Next(1, totalTextures + 1);
            }

            // Imprime no console os números de texturas disponíveis
            foreach (string[,] str in textureList)  
                Console.WriteLine("Texture: [ " + str[0, 1] + " ]");
            

            // Seleciona da lista o arquivo de textura definido pelo usuário (ou de forma aleatória)
            // e salva seu conteúdo em texData
            foreach (string [,] str in textureList)
                if (str[0, 1] == aleatTex.ToString())
                {
                    texData = str[0, 0];
                    break;
                }
            
            spikesTxt = ""; // Variável onde será salvo o conteúdo da textura sem vírgulas

            // Percorre o conteúdo de texData, remove as vírgulas e salva os números restantes em spikesTxt
            for (int i = 0; i < texData.Length; i++)
                if (texData[i] != ',')
                    spikesTxt += texData[i];
        }

        public void sendData()
        {
            ESPSerial.WriteLine(Protocolos.amplitude + amplitude.ToString());
            ESPSerial.WriteLine(Protocolos.larguraPulso + spk_width.ToString());
            /*
            if(iDirection == 0)
                ESPSerial.WriteLine(Protocolos.iDirection_anodic);
            else if(iDirection == 1)
                ESPSerial.WriteLine(Protocolos.iDirection_cathodic);
*/
            Console.WriteLine(Protocolos.amplitude + amplitude.ToString());
            Console.WriteLine(Protocolos.larguraPulso + spk_width.ToString());
            /*
            if (iDirection == 0)
                Console.WriteLine(Protocolos.iDirection_anodic);
            else if (iDirection == 1)
                Console.WriteLine(Protocolos.iDirection_cathodic);
                */
        }

        /*
         * Essa função verifica se a string dada como entrada é um arquivo de textura. Caso seja, o arquivo de
         * textura é lido e seu conteúdo é salvo em "textureList", que contém tanto o conteúdo de cada textura
         * quanto seu número.
         * 
         * TODOS os arquivos de textura devem se chamar "n.txt", onde n é um algarismo de 1 a 9 que representa a 
         * textura.
         */
        private void isTexture(string path)
        {
            string number;
            try
            {
                // Confere se é um arquivo .txt
                if (path.EndsWith(".txt"))
                {
                    // Salva o número da textura do nome do arquivo
                    number = path[path.Length - 5].ToString();

                    for (int i = 1; i < 9; i++)
                    {
                        // Confere se é um número de 1 a 9
                        if (i == Convert.ToInt32(number))
                        {
                            string textureData;
                            string[,] listObj;
                            
                            totalTextures++;    // Incrementa o número total de texturas
                            var sr = new StreamReader(path);    // Abre o arquivo de textura
                            textureData = sr.ReadToEnd();
                            textureData = textureData.Trim();   // Remove espaços em branco
                            // Salva o conteúdo da textura e seu número em listObj
                            listObj = new string[1, 2] { { textureData, number.ToString()} };
                            textureList.Add(listObj);   // Adiciona à lista de texturas
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button_initProtocol_Click(object sender, EventArgs e)
        {
            //if(estadoAtual == estados_estimulacao.DESATUALIZADO)
            //    button_initProtocol.PerformClick();
            
            testProtocol = new TestProtocol(this);
            testProtocol.Show();
            this.Hide();
        }
    }
}


/* TO DO:
 * 
 *  Colocar proteção nas textboxes pros valores não passarem do que devem
 *  
 *  Tirar número do protocolo
 *  
 */ 
