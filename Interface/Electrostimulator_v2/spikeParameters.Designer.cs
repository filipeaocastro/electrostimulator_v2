namespace Eletroestimulador_v02
{
    partial class spikeParameters
    {
        /// <summary> 
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Designer de Componentes

        /// <summary> 
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox_currentDirection = new System.Windows.Forms.GroupBox();
            this.radioButton_anodic = new System.Windows.Forms.RadioButton();
            this.radioButton_cathodic = new System.Windows.Forms.RadioButton();
            this.label_textureNumber = new System.Windows.Forms.Label();
            this.numericUpDown_textureNumber = new System.Windows.Forms.NumericUpDown();
            this.label_amplitudeValue = new System.Windows.Forms.Label();
            this.label_amplitude = new System.Windows.Forms.Label();
            this.textBox_amplitude = new System.Windows.Forms.TextBox();
            this.button_save = new System.Windows.Forms.Button();
            this.button_cancel = new System.Windows.Forms.Button();
            this.checkBox_random = new System.Windows.Forms.CheckBox();
            this.groupBox_currentDirection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_textureNumber)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox_currentDirection
            // 
            this.groupBox_currentDirection.Controls.Add(this.radioButton_anodic);
            this.groupBox_currentDirection.Controls.Add(this.radioButton_cathodic);
            this.groupBox_currentDirection.Location = new System.Drawing.Point(17, 63);
            this.groupBox_currentDirection.Name = "groupBox_currentDirection";
            this.groupBox_currentDirection.Size = new System.Drawing.Size(239, 42);
            this.groupBox_currentDirection.TabIndex = 20;
            this.groupBox_currentDirection.TabStop = false;
            this.groupBox_currentDirection.Text = "Current direction";
            // 
            // radioButton_anodic
            // 
            this.radioButton_anodic.AutoSize = true;
            this.radioButton_anodic.Checked = true;
            this.radioButton_anodic.Location = new System.Drawing.Point(161, 19);
            this.radioButton_anodic.Name = "radioButton_anodic";
            this.radioButton_anodic.Size = new System.Drawing.Size(58, 17);
            this.radioButton_anodic.TabIndex = 1;
            this.radioButton_anodic.TabStop = true;
            this.radioButton_anodic.Text = "Anodic";
            this.radioButton_anodic.UseVisualStyleBackColor = true;
            // 
            // radioButton_cathodic
            // 
            this.radioButton_cathodic.AutoSize = true;
            this.radioButton_cathodic.Enabled = false;
            this.radioButton_cathodic.Location = new System.Drawing.Point(36, 19);
            this.radioButton_cathodic.Name = "radioButton_cathodic";
            this.radioButton_cathodic.Size = new System.Drawing.Size(67, 17);
            this.radioButton_cathodic.TabIndex = 0;
            this.radioButton_cathodic.Text = "Cathodic";
            this.radioButton_cathodic.UseVisualStyleBackColor = true;
            // 
            // label_textureNumber
            // 
            this.label_textureNumber.AutoSize = true;
            this.label_textureNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_textureNumber.Location = new System.Drawing.Point(7, 37);
            this.label_textureNumber.Name = "label_textureNumber";
            this.label_textureNumber.Size = new System.Drawing.Size(97, 15);
            this.label_textureNumber.TabIndex = 35;
            this.label_textureNumber.Text = "Texture number:";
            // 
            // numericUpDown_textureNumber
            // 
            this.numericUpDown_textureNumber.Location = new System.Drawing.Point(109, 37);
            this.numericUpDown_textureNumber.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_textureNumber.Name = "numericUpDown_textureNumber";
            this.numericUpDown_textureNumber.Size = new System.Drawing.Size(43, 20);
            this.numericUpDown_textureNumber.TabIndex = 34;
            this.numericUpDown_textureNumber.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label_amplitudeValue
            // 
            this.label_amplitudeValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_amplitudeValue.Location = new System.Drawing.Point(206, 7);
            this.label_amplitudeValue.Name = "label_amplitudeValue";
            this.label_amplitudeValue.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label_amplitudeValue.Size = new System.Drawing.Size(30, 15);
            this.label_amplitudeValue.TabIndex = 31;
            this.label_amplitudeValue.Text = "μA";
            this.label_amplitudeValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_amplitude
            // 
            this.label_amplitude.AutoSize = true;
            this.label_amplitude.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_amplitude.Location = new System.Drawing.Point(26, 7);
            this.label_amplitude.Name = "label_amplitude";
            this.label_amplitude.Size = new System.Drawing.Size(65, 15);
            this.label_amplitude.TabIndex = 29;
            this.label_amplitude.Text = "Amplitude:";
            // 
            // textBox_amplitude
            // 
            this.textBox_amplitude.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_amplitude.Location = new System.Drawing.Point(97, 4);
            this.textBox_amplitude.Name = "textBox_amplitude";
            this.textBox_amplitude.Size = new System.Drawing.Size(103, 21);
            this.textBox_amplitude.TabIndex = 30;
            this.textBox_amplitude.Text = "3000";
            // 
            // button_save
            // 
            this.button_save.Location = new System.Drawing.Point(3, 111);
            this.button_save.Name = "button_save";
            this.button_save.Size = new System.Drawing.Size(128, 36);
            this.button_save.TabIndex = 36;
            this.button_save.Text = "Save";
            this.button_save.UseVisualStyleBackColor = true;
            this.button_save.Click += new System.EventHandler(this.button_save_Click);
            // 
            // button_cancel
            // 
            this.button_cancel.Location = new System.Drawing.Point(140, 111);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(128, 36);
            this.button_cancel.TabIndex = 37;
            this.button_cancel.Text = "Cancel";
            this.button_cancel.UseVisualStyleBackColor = true;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // checkBox_random
            // 
            this.checkBox_random.AutoSize = true;
            this.checkBox_random.Location = new System.Drawing.Point(167, 40);
            this.checkBox_random.Name = "checkBox_random";
            this.checkBox_random.Size = new System.Drawing.Size(66, 17);
            this.checkBox_random.TabIndex = 38;
            this.checkBox_random.Text = "Random";
            this.checkBox_random.UseVisualStyleBackColor = true;
            this.checkBox_random.CheckedChanged += new System.EventHandler(this.checkBox_random_CheckedChanged);
            // 
            // spikeParameters
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.checkBox_random);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_save);
            this.Controls.Add(this.groupBox_currentDirection);
            this.Controls.Add(this.label_textureNumber);
            this.Controls.Add(this.textBox_amplitude);
            this.Controls.Add(this.label_amplitude);
            this.Controls.Add(this.numericUpDown_textureNumber);
            this.Controls.Add(this.label_amplitudeValue);
            this.Name = "spikeParameters";
            this.Size = new System.Drawing.Size(271, 150);
            this.groupBox_currentDirection.ResumeLayout(false);
            this.groupBox_currentDirection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_textureNumber)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox_currentDirection;
        private System.Windows.Forms.RadioButton radioButton_anodic;
        private System.Windows.Forms.RadioButton radioButton_cathodic;
        private System.Windows.Forms.Label label_textureNumber;
        private System.Windows.Forms.NumericUpDown numericUpDown_textureNumber;
        private System.Windows.Forms.Label label_amplitudeValue;
        private System.Windows.Forms.Label label_amplitude;
        private System.Windows.Forms.TextBox textBox_amplitude;
        private System.Windows.Forms.Button button_save;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.CheckBox checkBox_random;
    }
}
