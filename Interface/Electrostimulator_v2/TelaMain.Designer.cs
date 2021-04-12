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
            this.label_ANL = new System.Windows.Forms.Label();
            this.label_ANLvalue = new System.Windows.Forms.Label();
            this.groupBox_parametros.SuspendLayout();
            this.SuspendLayout();
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
            this.groupBox_parametros.Location = new System.Drawing.Point(12, 12);
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
            this.textBox_amplitude.Text = "1000";
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
            this.textBox_pulseWd.Text = "5000";
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
            this.textBox_freq.Text = "100";
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
            this.textBox_duracao.Text = "30000";
            this.textBox_duracao.TextChanged += new System.EventHandler(this.textBox_duracao_TextChanged);
            // 
            // button_conectar
            // 
            this.button_conectar.Location = new System.Drawing.Point(118, 169);
            this.button_conectar.Name = "button_conectar";
            this.button_conectar.Size = new System.Drawing.Size(133, 42);
            this.button_conectar.TabIndex = 0;
            this.button_conectar.Text = "Conectar ao μC";
            this.button_conectar.UseVisualStyleBackColor = true;
            this.button_conectar.Click += new System.EventHandler(this.button_conectar_Click);
            // 
            // button_iniciar
            // 
            this.button_iniciar.Enabled = false;
            this.button_iniciar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_iniciar.Location = new System.Drawing.Point(12, 215);
            this.button_iniciar.Name = "button_iniciar";
            this.button_iniciar.Size = new System.Drawing.Size(239, 42);
            this.button_iniciar.TabIndex = 1;
            this.button_iniciar.Text = "Atualizar";
            this.button_iniciar.UseVisualStyleBackColor = true;
            this.button_iniciar.TextChanged += new System.EventHandler(this.button_iniciar_TextChanged);
            this.button_iniciar.Click += new System.EventHandler(this.button_iniciar_Click);
            // 
            // button_salvar
            // 
            this.button_salvar.Enabled = false;
            this.button_salvar.Location = new System.Drawing.Point(12, 169);
            this.button_salvar.Name = "button_salvar";
            this.button_salvar.Size = new System.Drawing.Size(100, 42);
            this.button_salvar.TabIndex = 19;
            this.button_salvar.Text = "Salvar\r\nparâmetros";
            this.button_salvar.UseVisualStyleBackColor = true;
            // 
            // label_ANL
            // 
            this.label_ANL.AutoSize = true;
            this.label_ANL.Enabled = false;
            this.label_ANL.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_ANL.Location = new System.Drawing.Point(12, 137);
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
            this.label_ANLvalue.Location = new System.Drawing.Point(145, 137);
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
            this.ClientSize = new System.Drawing.Size(258, 266);
            this.Controls.Add(this.label_ANLvalue);
            this.Controls.Add(this.label_ANL);
            this.Controls.Add(this.groupBox_parametros);
            this.Controls.Add(this.button_conectar);
            this.Controls.Add(this.button_iniciar);
            this.Controls.Add(this.button_salvar);
            this.MaximizeBox = false;
            this.Name = "TelaMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TelaMain";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.fechaApp);
            this.groupBox_parametros.ResumeLayout(false);
            this.groupBox_parametros.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
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
        private System.Windows.Forms.Label label_msDuracao;
        private System.Windows.Forms.Label label_duracao;
        private System.Windows.Forms.TextBox textBox_duracao;
        private System.Windows.Forms.Label label_usPulseWd;
        private System.Windows.Forms.Label label_pulseWd;
        private System.Windows.Forms.TextBox textBox_pulseWd;
        private System.Windows.Forms.Label label_ANL;
        private System.Windows.Forms.Label label_ANLvalue;
    }
}