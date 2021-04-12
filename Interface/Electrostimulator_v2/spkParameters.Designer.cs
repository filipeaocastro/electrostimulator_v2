namespace Eletroestimulador_v02
{
    partial class spkParameters
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
            this.checkBox_random = new System.Windows.Forms.CheckBox();
            this.button_cancel = new System.Windows.Forms.Button();
            this.button_save = new System.Windows.Forms.Button();
            this.label_textureNumber = new System.Windows.Forms.Label();
            this.textBox_amplitude = new System.Windows.Forms.TextBox();
            this.label_amplitude = new System.Windows.Forms.Label();
            this.numericUpDown_textureNumber = new System.Windows.Forms.NumericUpDown();
            this.label_amplitudeValue = new System.Windows.Forms.Label();
            this.label_spkWidth = new System.Windows.Forms.Label();
            this.label_spkWidth_value = new System.Windows.Forms.Label();
            this.trackBar_spkWidth = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_textureNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_spkWidth)).BeginInit();
            this.SuspendLayout();
            // 
            // checkBox_random
            // 
            this.checkBox_random.AutoSize = true;
            this.checkBox_random.Location = new System.Drawing.Point(172, 88);
            this.checkBox_random.Name = "checkBox_random";
            this.checkBox_random.Size = new System.Drawing.Size(66, 17);
            this.checkBox_random.TabIndex = 47;
            this.checkBox_random.Text = "Random";
            this.checkBox_random.UseVisualStyleBackColor = true;
            this.checkBox_random.CheckedChanged += new System.EventHandler(this.checkBox_random_CheckedChanged);
            // 
            // button_cancel
            // 
            this.button_cancel.Location = new System.Drawing.Point(149, 126);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(128, 36);
            this.button_cancel.TabIndex = 46;
            this.button_cancel.Text = "Cancel";
            this.button_cancel.UseVisualStyleBackColor = true;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // button_save
            // 
            this.button_save.Location = new System.Drawing.Point(12, 126);
            this.button_save.Name = "button_save";
            this.button_save.Size = new System.Drawing.Size(128, 36);
            this.button_save.TabIndex = 45;
            this.button_save.Text = "Save";
            this.button_save.UseVisualStyleBackColor = true;
            this.button_save.Click += new System.EventHandler(this.button_save_Click);
            // 
            // label_textureNumber
            // 
            this.label_textureNumber.AutoSize = true;
            this.label_textureNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_textureNumber.Location = new System.Drawing.Point(12, 85);
            this.label_textureNumber.Name = "label_textureNumber";
            this.label_textureNumber.Size = new System.Drawing.Size(97, 15);
            this.label_textureNumber.TabIndex = 44;
            this.label_textureNumber.Text = "Texture number:";
            // 
            // textBox_amplitude
            // 
            this.textBox_amplitude.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_amplitude.Location = new System.Drawing.Point(101, 7);
            this.textBox_amplitude.Name = "textBox_amplitude";
            this.textBox_amplitude.Size = new System.Drawing.Size(103, 21);
            this.textBox_amplitude.TabIndex = 41;
            this.textBox_amplitude.Text = "3000";
            // 
            // label_amplitude
            // 
            this.label_amplitude.AutoSize = true;
            this.label_amplitude.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_amplitude.Location = new System.Drawing.Point(30, 10);
            this.label_amplitude.Name = "label_amplitude";
            this.label_amplitude.Size = new System.Drawing.Size(65, 15);
            this.label_amplitude.TabIndex = 40;
            this.label_amplitude.Text = "Amplitude:";
            // 
            // numericUpDown_textureNumber
            // 
            this.numericUpDown_textureNumber.Location = new System.Drawing.Point(114, 85);
            this.numericUpDown_textureNumber.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_textureNumber.Name = "numericUpDown_textureNumber";
            this.numericUpDown_textureNumber.Size = new System.Drawing.Size(43, 20);
            this.numericUpDown_textureNumber.TabIndex = 43;
            this.numericUpDown_textureNumber.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label_amplitudeValue
            // 
            this.label_amplitudeValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_amplitudeValue.Location = new System.Drawing.Point(210, 10);
            this.label_amplitudeValue.Name = "label_amplitudeValue";
            this.label_amplitudeValue.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label_amplitudeValue.Size = new System.Drawing.Size(30, 15);
            this.label_amplitudeValue.TabIndex = 42;
            this.label_amplitudeValue.Text = "μA";
            this.label_amplitudeValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_spkWidth
            // 
            this.label_spkWidth.AutoSize = true;
            this.label_spkWidth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_spkWidth.Location = new System.Drawing.Point(6, 37);
            this.label_spkWidth.Name = "label_spkWidth";
            this.label_spkWidth.Size = new System.Drawing.Size(75, 15);
            this.label_spkWidth.TabIndex = 48;
            this.label_spkWidth.Text = "Spike Width:";
            // 
            // label_spkWidth_value
            // 
            this.label_spkWidth_value.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_spkWidth_value.Location = new System.Drawing.Point(197, 37);
            this.label_spkWidth_value.Name = "label_spkWidth_value";
            this.label_spkWidth_value.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label_spkWidth_value.Size = new System.Drawing.Size(54, 28);
            this.label_spkWidth_value.TabIndex = 50;
            this.label_spkWidth_value.Text = "1000 μs";
            this.label_spkWidth_value.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // trackBar_spkWidth
            // 
            this.trackBar_spkWidth.LargeChange = 1000;
            this.trackBar_spkWidth.Location = new System.Drawing.Point(87, 37);
            this.trackBar_spkWidth.Maximum = 5000;
            this.trackBar_spkWidth.Minimum = 250;
            this.trackBar_spkWidth.Name = "trackBar_spkWidth";
            this.trackBar_spkWidth.Size = new System.Drawing.Size(104, 45);
            this.trackBar_spkWidth.SmallChange = 250;
            this.trackBar_spkWidth.TabIndex = 51;
            this.trackBar_spkWidth.TickFrequency = 250;
            this.trackBar_spkWidth.Value = 250;
            this.trackBar_spkWidth.Scroll += new System.EventHandler(this.trackBar_spkWidth_Scroll);
            this.trackBar_spkWidth.ValueChanged += new System.EventHandler(this.trackBar_spkWidth_Scroll);
            // 
            // spkParameters
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 170);
            this.Controls.Add(this.trackBar_spkWidth);
            this.Controls.Add(this.label_spkWidth);
            this.Controls.Add(this.label_spkWidth_value);
            this.Controls.Add(this.checkBox_random);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_save);
            this.Controls.Add(this.label_textureNumber);
            this.Controls.Add(this.textBox_amplitude);
            this.Controls.Add(this.label_amplitude);
            this.Controls.Add(this.numericUpDown_textureNumber);
            this.Controls.Add(this.label_amplitudeValue);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(700, 700);
            this.Name = "spkParameters";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "spkParameters";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_textureNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_spkWidth)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBox_random;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.Button button_save;
        private System.Windows.Forms.Label label_textureNumber;
        private System.Windows.Forms.TextBox textBox_amplitude;
        private System.Windows.Forms.Label label_amplitude;
        private System.Windows.Forms.NumericUpDown numericUpDown_textureNumber;
        private System.Windows.Forms.Label label_amplitudeValue;
        private System.Windows.Forms.Label label_spkWidth;
        private System.Windows.Forms.Label label_spkWidth_value;
        private System.Windows.Forms.TrackBar trackBar_spkWidth;
    }
}