namespace Eletroestimulador_v02
{
    partial class SpikesForm
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
            this.components = new System.ComponentModel.Container();
            this.button_update = new System.Windows.Forms.Button();
            this.button_connectUc = new System.Windows.Forms.Button();
            this.timer_spk = new System.Windows.Forms.Timer(this.components);
            this.button_toggleVisible = new System.Windows.Forms.Button();
            this.button_loadTex = new System.Windows.Forms.Button();
            this.textBox_fileName = new System.Windows.Forms.TextBox();
            this.label_fileName = new System.Windows.Forms.Label();
            this.button_initProtocol = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button_update
            // 
            this.button_update.Enabled = false;
            this.button_update.Location = new System.Drawing.Point(67, 117);
            this.button_update.Name = "button_update";
            this.button_update.Size = new System.Drawing.Size(145, 42);
            this.button_update.TabIndex = 6;
            this.button_update.Text = "Update";
            this.button_update.UseVisualStyleBackColor = true;
            this.button_update.TextChanged += new System.EventHandler(this.button_update_TextChanged);
            this.button_update.Click += new System.EventHandler(this.button_update_Click);
            this.button_update.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.button_update_PreviewKeyDown);
            // 
            // button_connectUc
            // 
            this.button_connectUc.Enabled = false;
            this.button_connectUc.Location = new System.Drawing.Point(86, 76);
            this.button_connectUc.Name = "button_connectUc";
            this.button_connectUc.Size = new System.Drawing.Size(107, 35);
            this.button_connectUc.TabIndex = 21;
            this.button_connectUc.Text = "Connect μC";
            this.button_connectUc.UseVisualStyleBackColor = true;
            this.button_connectUc.Click += new System.EventHandler(this.button_connectUc_Click);
            // 
            // timer_spk
            // 
            this.timer_spk.Interval = 1;
            // 
            // button_toggleVisible
            // 
            this.button_toggleVisible.Enabled = false;
            this.button_toggleVisible.Location = new System.Drawing.Point(243, 12);
            this.button_toggleVisible.Name = "button_toggleVisible";
            this.button_toggleVisible.Size = new System.Drawing.Size(27, 26);
            this.button_toggleVisible.TabIndex = 32;
            this.button_toggleVisible.TabStop = false;
            this.button_toggleVisible.UseVisualStyleBackColor = true;
            this.button_toggleVisible.Click += new System.EventHandler(this.button_toggleVisible_Click);
            // 
            // button_loadTex
            // 
            this.button_loadTex.Location = new System.Drawing.Point(12, 12);
            this.button_loadTex.Name = "button_loadTex";
            this.button_loadTex.Size = new System.Drawing.Size(97, 26);
            this.button_loadTex.TabIndex = 38;
            this.button_loadTex.Text = "Load Textures";
            this.button_loadTex.UseVisualStyleBackColor = true;
            this.button_loadTex.Click += new System.EventHandler(this.button_loadTex_Click);
            // 
            // textBox_fileName
            // 
            this.textBox_fileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_fileName.Location = new System.Drawing.Point(147, 49);
            this.textBox_fileName.Name = "textBox_fileName";
            this.textBox_fileName.Size = new System.Drawing.Size(94, 21);
            this.textBox_fileName.TabIndex = 37;
            // 
            // label_fileName
            // 
            this.label_fileName.AutoSize = true;
            this.label_fileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_fileName.Location = new System.Drawing.Point(31, 52);
            this.label_fileName.Name = "label_fileName";
            this.label_fileName.Size = new System.Drawing.Size(100, 15);
            this.label_fileName.TabIndex = 37;
            this.label_fileName.Text = "Output file name:";
            // 
            // button_initProtocol
            // 
            this.button_initProtocol.Enabled = false;
            this.button_initProtocol.Location = new System.Drawing.Point(67, 165);
            this.button_initProtocol.Name = "button_initProtocol";
            this.button_initProtocol.Size = new System.Drawing.Size(145, 42);
            this.button_initProtocol.TabIndex = 39;
            this.button_initProtocol.Text = "Initiate Test Protocol";
            this.button_initProtocol.UseVisualStyleBackColor = true;
            this.button_initProtocol.Click += new System.EventHandler(this.button_initProtocol_Click);
            // 
            // SpikesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 233);
            this.Controls.Add(this.button_initProtocol);
            this.Controls.Add(this.label_fileName);
            this.Controls.Add(this.textBox_fileName);
            this.Controls.Add(this.button_loadTex);
            this.Controls.Add(this.button_toggleVisible);
            this.Controls.Add(this.button_connectUc);
            this.Controls.Add(this.button_update);
            this.Name = "SpikesForm";
            this.Text = "Spikes";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TelaSpikes_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button_update;
        private System.Windows.Forms.Button button_connectUc;
        private System.Windows.Forms.Timer timer_spk;
        private System.Windows.Forms.Button button_toggleVisible;
        private System.Windows.Forms.Button button_loadTex;
        private System.Windows.Forms.TextBox textBox_fileName;
        private System.Windows.Forms.Label label_fileName;
        private System.Windows.Forms.Button button_initProtocol;
    }
}