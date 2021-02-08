namespace Eletroestimulador_v02
{
    partial class TelaMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.checkBox_manterAmp = new System.Windows.Forms.CheckBox();
            this.checkBox_manterFreq = new System.Windows.Forms.CheckBox();
            this.numericUpDown_estimuloAtual = new System.Windows.Forms.NumericUpDown();
            this.label_estimuloAtual = new System.Windows.Forms.Label();
            this.groupBox_tipoOnda = new System.Windows.Forms.GroupBox();
            this.radioButton_tipoDenteDeSerra = new System.Windows.Forms.RadioButton();
            this.radioButton_tipoTriangular = new System.Windows.Forms.RadioButton();
            this.radioButton_tipoQuadrada = new System.Windows.Forms.RadioButton();
            this.radioButton_tipoSenoide = new System.Windows.Forms.RadioButton();
            this.button_TravarQteEstimulos = new System.Windows.Forms.Button();
            this.label_qteEstimulos = new System.Windows.Forms.Label();
            this.numericUpDown_qteEstimulos = new System.Windows.Forms.NumericUpDown();
            this.groupBox_parametros = new System.Windows.Forms.GroupBox();
            this.label_usPulseWd = new System.Windows.Forms.Label();
            this.textBox_amplitude = new System.Windows.Forms.TextBox();
            this.label_pulseWd = new System.Windows.Forms.Label();
            this.label_amplitude = new System.Windows.Forms.Label();
            this.textBox_pulseWd = new System.Windows.Forms.TextBox();
            this.label_uApp = new System.Windows.Forms.Label();
            this.textBox_freq = new System.Windows.Forms.TextBox();
            this.label_msDuracao = new System.Windows.Forms.Label();
            this.label_freq = new System.Windows.Forms.Label();
            this.label_duracao = new System.Windows.Forms.Label();
            this.label_hzFreq = new System.Windows.Forms.Label();
            this.textBox_duracao = new System.Windows.Forms.TextBox();
            this.button_conectar = new System.Windows.Forms.Button();
            this.button_iniciar = new System.Windows.Forms.Button();
            this.button_salvar = new System.Windows.Forms.Button();
            this.groupBox_tipoCorrente = new System.Windows.Forms.GroupBox();
            this.radioButton_tipoAlternada = new System.Windows.Forms.RadioButton();
            this.radioButton_tipoAnodica = new System.Windows.Forms.RadioButton();
            this.radioButton_tipoCatodica = new System.Windows.Forms.RadioButton();
            this.checkBox_intervaloAleat = new System.Windows.Forms.CheckBox();
            this.textBox_intervalo = new System.Windows.Forms.TextBox();
            this.label_intervalo = new System.Windows.Forms.Label();
            this.label_msIntervalo = new System.Windows.Forms.Label();
            this.textBox_intervaloMax = new System.Windows.Forms.TextBox();
            this.label_intervaloMax = new System.Windows.Forms.Label();
            this.label_msIntervaloMax = new System.Windows.Forms.Label();
            this.textBox_intervaloMin = new System.Windows.Forms.TextBox();
            this.label_intervaloMin = new System.Windows.Forms.Label();
            this.label_msIntervaloMin = new System.Windows.Forms.Label();
            this.groupBox_intervalo = new System.Windows.Forms.GroupBox();
            this.checkBox_aletorio = new System.Windows.Forms.CheckBox();
            this.label_ANL = new System.Windows.Forms.Label();
            this.label_ANLvalue = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_estimuloAtual)).BeginInit();
            this.groupBox_tipoOnda.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_qteEstimulos)).BeginInit();
            this.groupBox_parametros.SuspendLayout();
            this.groupBox_tipoCorrente.SuspendLayout();
            this.groupBox_intervalo.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkBox_manterAmp
            // 
            this.checkBox_manterAmp.AutoSize = true;
            this.checkBox_manterAmp.Enabled = false;
            this.checkBox_manterAmp.Location = new System.Drawing.Point(144, 110);
            this.checkBox_manterAmp.Name = "checkBox_manterAmp";
            this.checkBox_manterAmp.Size = new System.Drawing.Size(107, 17);
            this.checkBox_manterAmp.TabIndex = 30;
            this.checkBox_manterAmp.Text = "Manter amplitude";
            this.checkBox_manterAmp.UseVisualStyleBackColor = true;
            // 
            // checkBox_manterFreq
            // 
            this.checkBox_manterFreq.AutoSize = true;
            this.checkBox_manterFreq.Enabled = false;
            this.checkBox_manterFreq.Location = new System.Drawing.Point(12, 110);
            this.checkBox_manterFreq.Name = "checkBox_manterFreq";
            this.checkBox_manterFreq.Size = new System.Drawing.Size(112, 17);
            this.checkBox_manterFreq.TabIndex = 29;
            this.checkBox_manterFreq.Text = "Manter frequência";
            this.checkBox_manterFreq.UseVisualStyleBackColor = true;
            // 
            // numericUpDown_estimuloAtual
            // 
            this.numericUpDown_estimuloAtual.Enabled = false;
            this.numericUpDown_estimuloAtual.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDown_estimuloAtual.Location = new System.Drawing.Point(148, 81);
            this.numericUpDown_estimuloAtual.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown_estimuloAtual.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_estimuloAtual.Name = "numericUpDown_estimuloAtual";
            this.numericUpDown_estimuloAtual.Size = new System.Drawing.Size(49, 21);
            this.numericUpDown_estimuloAtual.TabIndex = 28;
            this.numericUpDown_estimuloAtual.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label_estimuloAtual
            // 
            this.label_estimuloAtual.AutoSize = true;
            this.label_estimuloAtual.Enabled = false;
            this.label_estimuloAtual.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_estimuloAtual.Location = new System.Drawing.Point(54, 83);
            this.label_estimuloAtual.Name = "label_estimuloAtual";
            this.label_estimuloAtual.Size = new System.Drawing.Size(88, 15);
            this.label_estimuloAtual.TabIndex = 27;
            this.label_estimuloAtual.Text = "Estímulo atual:";
            // 
            // groupBox_tipoOnda
            // 
            this.groupBox_tipoOnda.Controls.Add(this.radioButton_tipoDenteDeSerra);
            this.groupBox_tipoOnda.Controls.Add(this.radioButton_tipoTriangular);
            this.groupBox_tipoOnda.Controls.Add(this.radioButton_tipoQuadrada);
            this.groupBox_tipoOnda.Controls.Add(this.radioButton_tipoSenoide);
            this.groupBox_tipoOnda.Location = new System.Drawing.Point(12, 33);
            this.groupBox_tipoOnda.Name = "groupBox_tipoOnda";
            this.groupBox_tipoOnda.Size = new System.Drawing.Size(328, 42);
            this.groupBox_tipoOnda.TabIndex = 18;
            this.groupBox_tipoOnda.TabStop = false;
            this.groupBox_tipoOnda.Text = "Tipo de onda";
            // 
            // radioButton_tipoDenteDeSerra
            // 
            this.radioButton_tipoDenteDeSerra.AutoSize = true;
            this.radioButton_tipoDenteDeSerra.Enabled = false;
            this.radioButton_tipoDenteDeSerra.Location = new System.Drawing.Point(227, 19);
            this.radioButton_tipoDenteDeSerra.Name = "radioButton_tipoDenteDeSerra";
            this.radioButton_tipoDenteDeSerra.Size = new System.Drawing.Size(95, 17);
            this.radioButton_tipoDenteDeSerra.TabIndex = 3;
            this.radioButton_tipoDenteDeSerra.Text = "Dente de serra";
            this.radioButton_tipoDenteDeSerra.UseVisualStyleBackColor = true;
            // 
            // radioButton_tipoTriangular
            // 
            this.radioButton_tipoTriangular.AutoSize = true;
            this.radioButton_tipoTriangular.Enabled = false;
            this.radioButton_tipoTriangular.Location = new System.Drawing.Point(149, 19);
            this.radioButton_tipoTriangular.Name = "radioButton_tipoTriangular";
            this.radioButton_tipoTriangular.Size = new System.Drawing.Size(72, 17);
            this.radioButton_tipoTriangular.TabIndex = 2;
            this.radioButton_tipoTriangular.Text = "Triangular";
            this.radioButton_tipoTriangular.UseVisualStyleBackColor = true;
            // 
            // radioButton_tipoQuadrada
            // 
            this.radioButton_tipoQuadrada.AutoSize = true;
            this.radioButton_tipoQuadrada.Checked = true;
            this.radioButton_tipoQuadrada.Location = new System.Drawing.Point(76, 19);
            this.radioButton_tipoQuadrada.Name = "radioButton_tipoQuadrada";
            this.radioButton_tipoQuadrada.Size = new System.Drawing.Size(72, 17);
            this.radioButton_tipoQuadrada.TabIndex = 1;
            this.radioButton_tipoQuadrada.TabStop = true;
            this.radioButton_tipoQuadrada.Text = "Quadrada";
            this.radioButton_tipoQuadrada.UseVisualStyleBackColor = true;
            this.radioButton_tipoQuadrada.CheckedChanged += new System.EventHandler(this.radioButton_tipoQuadrada_CheckedChanged);
            // 
            // radioButton_tipoSenoide
            // 
            this.radioButton_tipoSenoide.AutoSize = true;
            this.radioButton_tipoSenoide.Enabled = false;
            this.radioButton_tipoSenoide.Location = new System.Drawing.Point(6, 19);
            this.radioButton_tipoSenoide.Name = "radioButton_tipoSenoide";
            this.radioButton_tipoSenoide.Size = new System.Drawing.Size(64, 17);
            this.radioButton_tipoSenoide.TabIndex = 0;
            this.radioButton_tipoSenoide.Text = "Senoide";
            this.radioButton_tipoSenoide.UseVisualStyleBackColor = true;
            // 
            // button_TravarQteEstimulos
            // 
            this.button_TravarQteEstimulos.BackgroundImage = global::Eletroestimulador_v02.Properties.Resources._lock;
            this.button_TravarQteEstimulos.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_TravarQteEstimulos.Enabled = false;
            this.button_TravarQteEstimulos.Location = new System.Drawing.Point(221, -1);
            this.button_TravarQteEstimulos.Name = "button_TravarQteEstimulos";
            this.button_TravarQteEstimulos.Size = new System.Drawing.Size(30, 32);
            this.button_TravarQteEstimulos.TabIndex = 26;
            this.button_TravarQteEstimulos.UseVisualStyleBackColor = true;
            // 
            // label_qteEstimulos
            // 
            this.label_qteEstimulos.AutoSize = true;
            this.label_qteEstimulos.Enabled = false;
            this.label_qteEstimulos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_qteEstimulos.Location = new System.Drawing.Point(13, 7);
            this.label_qteEstimulos.Name = "label_qteEstimulos";
            this.label_qteEstimulos.Size = new System.Drawing.Size(147, 15);
            this.label_qteEstimulos.TabIndex = 25;
            this.label_qteEstimulos.Text = "Quantidade de estímulos:";
            // 
            // numericUpDown_qteEstimulos
            // 
            this.numericUpDown_qteEstimulos.Enabled = false;
            this.numericUpDown_qteEstimulos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDown_qteEstimulos.Location = new System.Drawing.Point(166, 5);
            this.numericUpDown_qteEstimulos.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown_qteEstimulos.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_qteEstimulos.Name = "numericUpDown_qteEstimulos";
            this.numericUpDown_qteEstimulos.Size = new System.Drawing.Size(49, 21);
            this.numericUpDown_qteEstimulos.TabIndex = 24;
            this.numericUpDown_qteEstimulos.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // groupBox_parametros
            // 
            this.groupBox_parametros.Controls.Add(this.label_usPulseWd);
            this.groupBox_parametros.Controls.Add(this.textBox_amplitude);
            this.groupBox_parametros.Controls.Add(this.label_pulseWd);
            this.groupBox_parametros.Controls.Add(this.label_amplitude);
            this.groupBox_parametros.Controls.Add(this.textBox_pulseWd);
            this.groupBox_parametros.Controls.Add(this.label_uApp);
            this.groupBox_parametros.Controls.Add(this.textBox_freq);
            this.groupBox_parametros.Controls.Add(this.label_msDuracao);
            this.groupBox_parametros.Controls.Add(this.label_freq);
            this.groupBox_parametros.Controls.Add(this.label_duracao);
            this.groupBox_parametros.Controls.Add(this.label_hzFreq);
            this.groupBox_parametros.Controls.Add(this.textBox_duracao);
            this.groupBox_parametros.Location = new System.Drawing.Point(12, 158);
            this.groupBox_parametros.Name = "groupBox_parametros";
            this.groupBox_parametros.Size = new System.Drawing.Size(239, 122);
            this.groupBox_parametros.TabIndex = 22;
            this.groupBox_parametros.TabStop = false;
            this.groupBox_parametros.Text = "Parâmetros";
            // 
            // label_usPulseWd
            // 
            this.label_usPulseWd.AutoSize = true;
            this.label_usPulseWd.Enabled = false;
            this.label_usPulseWd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_usPulseWd.Location = new System.Drawing.Point(198, 94);
            this.label_usPulseWd.Name = "label_usPulseWd";
            this.label_usPulseWd.Size = new System.Drawing.Size(20, 15);
            this.label_usPulseWd.TabIndex = 43;
            this.label_usPulseWd.Text = "μs";
            // 
            // textBox_amplitude
            // 
            this.textBox_amplitude.Location = new System.Drawing.Point(92, 15);
            this.textBox_amplitude.Name = "textBox_amplitude";
            this.textBox_amplitude.Size = new System.Drawing.Size(100, 20);
            this.textBox_amplitude.TabIndex = 31;
            this.textBox_amplitude.TextChanged += new System.EventHandler(this.textBox_amplitude_TextChanged);
            // 
            // label_pulseWd
            // 
            this.label_pulseWd.AutoSize = true;
            this.label_pulseWd.Enabled = false;
            this.label_pulseWd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_pulseWd.Location = new System.Drawing.Point(3, 94);
            this.label_pulseWd.Name = "label_pulseWd";
            this.label_pulseWd.Size = new System.Drawing.Size(103, 15);
            this.label_pulseWd.TabIndex = 42;
            this.label_pulseWd.Text = "Largura de pulso:";
            this.label_pulseWd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_amplitude
            // 
            this.label_amplitude.AutoSize = true;
            this.label_amplitude.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_amplitude.Location = new System.Drawing.Point(21, 16);
            this.label_amplitude.Name = "label_amplitude";
            this.label_amplitude.Size = new System.Drawing.Size(65, 15);
            this.label_amplitude.TabIndex = 32;
            this.label_amplitude.Text = "Amplitude:";
            this.label_amplitude.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox_pulseWd
            // 
            this.textBox_pulseWd.Enabled = false;
            this.textBox_pulseWd.Location = new System.Drawing.Point(106, 93);
            this.textBox_pulseWd.Name = "textBox_pulseWd";
            this.textBox_pulseWd.Size = new System.Drawing.Size(86, 20);
            this.textBox_pulseWd.TabIndex = 41;
            this.textBox_pulseWd.TextChanged += new System.EventHandler(this.textBox_pulseWd_TextChanged);
            // 
            // label_uApp
            // 
            this.label_uApp.AutoSize = true;
            this.label_uApp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_uApp.Location = new System.Drawing.Point(198, 16);
            this.label_uApp.Name = "label_uApp";
            this.label_uApp.Size = new System.Drawing.Size(21, 15);
            this.label_uApp.TabIndex = 33;
            this.label_uApp.Text = "μA";
            // 
            // textBox_freq
            // 
            this.textBox_freq.Location = new System.Drawing.Point(92, 41);
            this.textBox_freq.Name = "textBox_freq";
            this.textBox_freq.Size = new System.Drawing.Size(100, 20);
            this.textBox_freq.TabIndex = 34;
            this.textBox_freq.TextChanged += new System.EventHandler(this.textBox_freq_TextChanged);
            // 
            // label_msDuracao
            // 
            this.label_msDuracao.AutoSize = true;
            this.label_msDuracao.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_msDuracao.Location = new System.Drawing.Point(198, 68);
            this.label_msDuracao.Name = "label_msDuracao";
            this.label_msDuracao.Size = new System.Drawing.Size(24, 15);
            this.label_msDuracao.TabIndex = 39;
            this.label_msDuracao.Text = "ms";
            // 
            // label_freq
            // 
            this.label_freq.AutoSize = true;
            this.label_freq.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_freq.Location = new System.Drawing.Point(14, 42);
            this.label_freq.Name = "label_freq";
            this.label_freq.Size = new System.Drawing.Size(72, 15);
            this.label_freq.TabIndex = 35;
            this.label_freq.Text = "Frequência:";
            this.label_freq.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_duracao
            // 
            this.label_duracao.AutoSize = true;
            this.label_duracao.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_duracao.Location = new System.Drawing.Point(29, 68);
            this.label_duracao.Name = "label_duracao";
            this.label_duracao.Size = new System.Drawing.Size(57, 15);
            this.label_duracao.TabIndex = 38;
            this.label_duracao.Text = "Duração:";
            this.label_duracao.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_hzFreq
            // 
            this.label_hzFreq.AutoSize = true;
            this.label_hzFreq.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_hzFreq.Location = new System.Drawing.Point(198, 42);
            this.label_hzFreq.Name = "label_hzFreq";
            this.label_hzFreq.Size = new System.Drawing.Size(22, 15);
            this.label_hzFreq.TabIndex = 36;
            this.label_hzFreq.Text = "Hz";
            // 
            // textBox_duracao
            // 
            this.textBox_duracao.Location = new System.Drawing.Point(92, 67);
            this.textBox_duracao.Name = "textBox_duracao";
            this.textBox_duracao.Size = new System.Drawing.Size(100, 20);
            this.textBox_duracao.TabIndex = 37;
            this.textBox_duracao.TextChanged += new System.EventHandler(this.textBox_duracao_TextChanged);
            // 
            // button_conectar
            // 
            this.button_conectar.Location = new System.Drawing.Point(118, 455);
            this.button_conectar.Name = "button_conectar";
            this.button_conectar.Size = new System.Drawing.Size(133, 42);
            this.button_conectar.TabIndex = 21;
            this.button_conectar.Text = "Conectar ao μC";
            this.button_conectar.UseVisualStyleBackColor = true;
            this.button_conectar.Click += new System.EventHandler(this.button_conectar_Click);
            // 
            // button_iniciar
            // 
            this.button_iniciar.Enabled = false;
            this.button_iniciar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_iniciar.Location = new System.Drawing.Point(12, 501);
            this.button_iniciar.Name = "button_iniciar";
            this.button_iniciar.Size = new System.Drawing.Size(239, 42);
            this.button_iniciar.TabIndex = 20;
            this.button_iniciar.Text = "Atualizar";
            this.button_iniciar.UseVisualStyleBackColor = true;
            this.button_iniciar.TextChanged += new System.EventHandler(this.button_iniciar_TextChanged);
            this.button_iniciar.Click += new System.EventHandler(this.button_iniciar_Click);
            // 
            // button_salvar
            // 
            this.button_salvar.Location = new System.Drawing.Point(12, 455);
            this.button_salvar.Name = "button_salvar";
            this.button_salvar.Size = new System.Drawing.Size(100, 42);
            this.button_salvar.TabIndex = 19;
            this.button_salvar.Text = "Salvar\r\nparâmetros";
            this.button_salvar.UseVisualStyleBackColor = true;
            // 
            // groupBox_tipoCorrente
            // 
            this.groupBox_tipoCorrente.Controls.Add(this.radioButton_tipoAlternada);
            this.groupBox_tipoCorrente.Controls.Add(this.radioButton_tipoAnodica);
            this.groupBox_tipoCorrente.Controls.Add(this.radioButton_tipoCatodica);
            this.groupBox_tipoCorrente.Location = new System.Drawing.Point(6, 377);
            this.groupBox_tipoCorrente.Name = "groupBox_tipoCorrente";
            this.groupBox_tipoCorrente.Size = new System.Drawing.Size(239, 42);
            this.groupBox_tipoCorrente.TabIndex = 19;
            this.groupBox_tipoCorrente.TabStop = false;
            this.groupBox_tipoCorrente.Text = "Tipo de corrente";
            this.groupBox_tipoCorrente.Visible = false;
            // 
            // radioButton_tipoAlternada
            // 
            this.radioButton_tipoAlternada.AutoSize = true;
            this.radioButton_tipoAlternada.Location = new System.Drawing.Point(149, 19);
            this.radioButton_tipoAlternada.Name = "radioButton_tipoAlternada";
            this.radioButton_tipoAlternada.Size = new System.Drawing.Size(70, 17);
            this.radioButton_tipoAlternada.TabIndex = 2;
            this.radioButton_tipoAlternada.TabStop = true;
            this.radioButton_tipoAlternada.Text = "Alternada";
            this.radioButton_tipoAlternada.UseVisualStyleBackColor = true;
            // 
            // radioButton_tipoAnodica
            // 
            this.radioButton_tipoAnodica.AutoSize = true;
            this.radioButton_tipoAnodica.Location = new System.Drawing.Point(76, 19);
            this.radioButton_tipoAnodica.Name = "radioButton_tipoAnodica";
            this.radioButton_tipoAnodica.Size = new System.Drawing.Size(64, 17);
            this.radioButton_tipoAnodica.TabIndex = 1;
            this.radioButton_tipoAnodica.TabStop = true;
            this.radioButton_tipoAnodica.Text = "Anódica";
            this.radioButton_tipoAnodica.UseVisualStyleBackColor = true;
            // 
            // radioButton_tipoCatodica
            // 
            this.radioButton_tipoCatodica.AutoSize = true;
            this.radioButton_tipoCatodica.Checked = true;
            this.radioButton_tipoCatodica.Location = new System.Drawing.Point(6, 19);
            this.radioButton_tipoCatodica.Name = "radioButton_tipoCatodica";
            this.radioButton_tipoCatodica.Size = new System.Drawing.Size(67, 17);
            this.radioButton_tipoCatodica.TabIndex = 0;
            this.radioButton_tipoCatodica.TabStop = true;
            this.radioButton_tipoCatodica.Text = "Catódica";
            this.radioButton_tipoCatodica.UseVisualStyleBackColor = true;
            // 
            // checkBox_intervaloAleat
            // 
            this.checkBox_intervaloAleat.AutoSize = true;
            this.checkBox_intervaloAleat.Location = new System.Drawing.Point(82, 45);
            this.checkBox_intervaloAleat.Name = "checkBox_intervaloAleat";
            this.checkBox_intervaloAleat.Size = new System.Drawing.Size(110, 17);
            this.checkBox_intervaloAleat.TabIndex = 40;
            this.checkBox_intervaloAleat.Text = "Intervalo aleatório";
            this.checkBox_intervaloAleat.UseVisualStyleBackColor = true;
            // 
            // textBox_intervalo
            // 
            this.textBox_intervalo.Location = new System.Drawing.Point(85, 19);
            this.textBox_intervalo.Name = "textBox_intervalo";
            this.textBox_intervalo.Size = new System.Drawing.Size(100, 20);
            this.textBox_intervalo.TabIndex = 44;
            // 
            // label_intervalo
            // 
            this.label_intervalo.AutoSize = true;
            this.label_intervalo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_intervalo.Location = new System.Drawing.Point(23, 20);
            this.label_intervalo.Name = "label_intervalo";
            this.label_intervalo.Size = new System.Drawing.Size(56, 15);
            this.label_intervalo.TabIndex = 45;
            this.label_intervalo.Text = "Intervalo:";
            this.label_intervalo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_msIntervalo
            // 
            this.label_msIntervalo.AutoSize = true;
            this.label_msIntervalo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_msIntervalo.Location = new System.Drawing.Point(191, 20);
            this.label_msIntervalo.Name = "label_msIntervalo";
            this.label_msIntervalo.Size = new System.Drawing.Size(24, 15);
            this.label_msIntervalo.TabIndex = 46;
            this.label_msIntervalo.Text = "ms";
            // 
            // textBox_intervaloMax
            // 
            this.textBox_intervaloMax.Location = new System.Drawing.Point(85, 64);
            this.textBox_intervaloMax.Name = "textBox_intervaloMax";
            this.textBox_intervaloMax.Size = new System.Drawing.Size(100, 20);
            this.textBox_intervaloMax.TabIndex = 47;
            // 
            // label_intervaloMax
            // 
            this.label_intervaloMax.AutoSize = true;
            this.label_intervaloMax.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_intervaloMax.Location = new System.Drawing.Point(24, 65);
            this.label_intervaloMax.Name = "label_intervaloMax";
            this.label_intervaloMax.Size = new System.Drawing.Size(55, 15);
            this.label_intervaloMax.TabIndex = 48;
            this.label_intervaloMax.Text = "Máximo:";
            this.label_intervaloMax.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_msIntervaloMax
            // 
            this.label_msIntervaloMax.AutoSize = true;
            this.label_msIntervaloMax.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_msIntervaloMax.Location = new System.Drawing.Point(191, 65);
            this.label_msIntervaloMax.Name = "label_msIntervaloMax";
            this.label_msIntervaloMax.Size = new System.Drawing.Size(24, 15);
            this.label_msIntervaloMax.TabIndex = 49;
            this.label_msIntervaloMax.Text = "ms";
            // 
            // textBox_intervaloMin
            // 
            this.textBox_intervaloMin.Location = new System.Drawing.Point(85, 90);
            this.textBox_intervaloMin.Name = "textBox_intervaloMin";
            this.textBox_intervaloMin.Size = new System.Drawing.Size(100, 20);
            this.textBox_intervaloMin.TabIndex = 50;
            // 
            // label_intervaloMin
            // 
            this.label_intervaloMin.AutoSize = true;
            this.label_intervaloMin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_intervaloMin.Location = new System.Drawing.Point(27, 91);
            this.label_intervaloMin.Name = "label_intervaloMin";
            this.label_intervaloMin.Size = new System.Drawing.Size(52, 15);
            this.label_intervaloMin.TabIndex = 51;
            this.label_intervaloMin.Text = "Mínimo:";
            this.label_intervaloMin.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_msIntervaloMin
            // 
            this.label_msIntervaloMin.AutoSize = true;
            this.label_msIntervaloMin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_msIntervaloMin.Location = new System.Drawing.Point(191, 91);
            this.label_msIntervaloMin.Name = "label_msIntervaloMin";
            this.label_msIntervaloMin.Size = new System.Drawing.Size(24, 15);
            this.label_msIntervaloMin.TabIndex = 52;
            this.label_msIntervaloMin.Text = "ms";
            // 
            // groupBox_intervalo
            // 
            this.groupBox_intervalo.Controls.Add(this.textBox_intervalo);
            this.groupBox_intervalo.Controls.Add(this.checkBox_intervaloAleat);
            this.groupBox_intervalo.Controls.Add(this.textBox_intervaloMin);
            this.groupBox_intervalo.Controls.Add(this.label_msIntervalo);
            this.groupBox_intervalo.Controls.Add(this.label_intervalo);
            this.groupBox_intervalo.Controls.Add(this.label_msIntervaloMax);
            this.groupBox_intervalo.Controls.Add(this.label_intervaloMin);
            this.groupBox_intervalo.Controls.Add(this.label_intervaloMax);
            this.groupBox_intervalo.Controls.Add(this.textBox_intervaloMax);
            this.groupBox_intervalo.Controls.Add(this.label_msIntervaloMin);
            this.groupBox_intervalo.Enabled = false;
            this.groupBox_intervalo.Location = new System.Drawing.Point(12, 286);
            this.groupBox_intervalo.Name = "groupBox_intervalo";
            this.groupBox_intervalo.Size = new System.Drawing.Size(239, 115);
            this.groupBox_intervalo.TabIndex = 20;
            this.groupBox_intervalo.TabStop = false;
            this.groupBox_intervalo.Text = "Intervalo";
            // 
            // checkBox_aletorio
            // 
            this.checkBox_aletorio.AutoSize = true;
            this.checkBox_aletorio.Enabled = false;
            this.checkBox_aletorio.Location = new System.Drawing.Point(44, 133);
            this.checkBox_aletorio.Name = "checkBox_aletorio";
            this.checkBox_aletorio.Size = new System.Drawing.Size(160, 17);
            this.checkBox_aletorio.TabIndex = 31;
            this.checkBox_aletorio.Text = "Executar em ordem aleatória";
            this.checkBox_aletorio.UseVisualStyleBackColor = true;
            // 
            // label_ANL
            // 
            this.label_ANL.AutoSize = true;
            this.label_ANL.Enabled = false;
            this.label_ANL.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_ANL.Location = new System.Drawing.Point(13, 422);
            this.label_ANL.Name = "label_ANL";
            this.label_ANL.Size = new System.Drawing.Size(101, 15);
            this.label_ANL.TabIndex = 32;
            this.label_ANL.Text = "Corrente na pele:";
            // 
            // label_ANLvalue
            // 
            this.label_ANLvalue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_ANLvalue.Enabled = false;
            this.label_ANLvalue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_ANLvalue.Location = new System.Drawing.Point(150, 422);
            this.label_ANLvalue.Name = "label_ANLvalue";
            this.label_ANLvalue.Size = new System.Drawing.Size(101, 15);
            this.label_ANLvalue.TabIndex = 33;
            this.label_ANLvalue.Text = "0 μA";
            this.label_ANLvalue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TelaMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(347, 551);
            this.Controls.Add(this.label_ANLvalue);
            this.Controls.Add(this.label_ANL);
            this.Controls.Add(this.checkBox_aletorio);
            this.Controls.Add(this.groupBox_intervalo);
            this.Controls.Add(this.groupBox_tipoCorrente);
            this.Controls.Add(this.checkBox_manterAmp);
            this.Controls.Add(this.checkBox_manterFreq);
            this.Controls.Add(this.numericUpDown_estimuloAtual);
            this.Controls.Add(this.label_estimuloAtual);
            this.Controls.Add(this.groupBox_tipoOnda);
            this.Controls.Add(this.button_TravarQteEstimulos);
            this.Controls.Add(this.label_qteEstimulos);
            this.Controls.Add(this.numericUpDown_qteEstimulos);
            this.Controls.Add(this.groupBox_parametros);
            this.Controls.Add(this.button_conectar);
            this.Controls.Add(this.button_iniciar);
            this.Controls.Add(this.button_salvar);
            this.Name = "TelaMain";
            this.Text = "TelaMain";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.fechaApp);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_estimuloAtual)).EndInit();
            this.groupBox_tipoOnda.ResumeLayout(false);
            this.groupBox_tipoOnda.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_qteEstimulos)).EndInit();
            this.groupBox_parametros.ResumeLayout(false);
            this.groupBox_parametros.PerformLayout();
            this.groupBox_tipoCorrente.ResumeLayout(false);
            this.groupBox_tipoCorrente.PerformLayout();
            this.groupBox_intervalo.ResumeLayout(false);
            this.groupBox_intervalo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBox_manterAmp;
        private System.Windows.Forms.CheckBox checkBox_manterFreq;
        private System.Windows.Forms.NumericUpDown numericUpDown_estimuloAtual;
        private System.Windows.Forms.Label label_estimuloAtual;
        private System.Windows.Forms.GroupBox groupBox_tipoOnda;
        private System.Windows.Forms.RadioButton radioButton_tipoTriangular;
        private System.Windows.Forms.RadioButton radioButton_tipoQuadrada;
        private System.Windows.Forms.RadioButton radioButton_tipoSenoide;
        private System.Windows.Forms.Button button_TravarQteEstimulos;
        private System.Windows.Forms.Label label_qteEstimulos;
        private System.Windows.Forms.NumericUpDown numericUpDown_qteEstimulos;
        private System.Windows.Forms.GroupBox groupBox_parametros;
        private System.Windows.Forms.Button button_conectar;
        private System.Windows.Forms.Button button_iniciar;
        private System.Windows.Forms.Button button_salvar;
        private System.Windows.Forms.TextBox textBox_amplitude;
        private System.Windows.Forms.Label label_amplitude;
        private System.Windows.Forms.Label label_uApp;
        private System.Windows.Forms.Label label_hzFreq;
        private System.Windows.Forms.Label label_freq;
        private System.Windows.Forms.TextBox textBox_freq;
        private System.Windows.Forms.GroupBox groupBox_tipoCorrente;
        private System.Windows.Forms.RadioButton radioButton_tipoAlternada;
        private System.Windows.Forms.RadioButton radioButton_tipoAnodica;
        private System.Windows.Forms.RadioButton radioButton_tipoCatodica;
        private System.Windows.Forms.Label label_msDuracao;
        private System.Windows.Forms.Label label_duracao;
        private System.Windows.Forms.TextBox textBox_duracao;
        private System.Windows.Forms.CheckBox checkBox_intervaloAleat;
        private System.Windows.Forms.Label label_usPulseWd;
        private System.Windows.Forms.Label label_pulseWd;
        private System.Windows.Forms.TextBox textBox_pulseWd;
        private System.Windows.Forms.TextBox textBox_intervalo;
        private System.Windows.Forms.Label label_intervalo;
        private System.Windows.Forms.Label label_msIntervalo;
        private System.Windows.Forms.TextBox textBox_intervaloMax;
        private System.Windows.Forms.Label label_intervaloMax;
        private System.Windows.Forms.Label label_msIntervaloMax;
        private System.Windows.Forms.TextBox textBox_intervaloMin;
        private System.Windows.Forms.Label label_intervaloMin;
        private System.Windows.Forms.Label label_msIntervaloMin;
        private System.Windows.Forms.GroupBox groupBox_intervalo;
        private System.Windows.Forms.CheckBox checkBox_aletorio;
        private System.Windows.Forms.RadioButton radioButton_tipoDenteDeSerra;
        private System.Windows.Forms.Label label_ANL;
        private System.Windows.Forms.Label label_ANLvalue;
    }
}