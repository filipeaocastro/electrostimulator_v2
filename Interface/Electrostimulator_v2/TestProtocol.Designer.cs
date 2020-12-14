namespace Eletroestimulador_v02
{
    partial class TestProtocol
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
            this.button_start = new System.Windows.Forms.Button();
            this.label_countDown = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pictureBox_textura = new System.Windows.Forms.PictureBox();
            this.button_status = new System.Windows.Forms.Button();
            this.progressBar_cross = new System.Windows.Forms.ProgressBar();
            this.label_counter = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_textura)).BeginInit();
            this.SuspendLayout();
            // 
            // button_start
            // 
            this.button_start.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.button_start.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button_start.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_start.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_start.Location = new System.Drawing.Point(161, 662);
            this.button_start.Name = "button_start";
            this.button_start.Size = new System.Drawing.Size(163, 80);
            this.button_start.TabIndex = 0;
            this.button_start.Text = "Start";
            this.button_start.UseVisualStyleBackColor = false;
            this.button_start.Click += new System.EventHandler(this.button_start_Click);
            // 
            // label_countDown
            // 
            this.label_countDown.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label_countDown.BackColor = System.Drawing.Color.Transparent;
            this.label_countDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_countDown.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label_countDown.Location = new System.Drawing.Point(99, 225);
            this.label_countDown.Name = "label_countDown";
            this.label_countDown.Size = new System.Drawing.Size(346, 108);
            this.label_countDown.TabIndex = 1;
            this.label_countDown.Text = "5";
            this.label_countDown.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label_countDown.Visible = false;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // pictureBox_textura
            // 
            this.pictureBox_textura.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pictureBox_textura.Location = new System.Drawing.Point(12, 27);
            this.pictureBox_textura.Name = "pictureBox_textura";
            this.pictureBox_textura.Size = new System.Drawing.Size(500, 750);
            this.pictureBox_textura.TabIndex = 2;
            this.pictureBox_textura.TabStop = false;
            // 
            // button_status
            // 
            this.button_status.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_status.Location = new System.Drawing.Point(461, 690);
            this.button_status.Name = "button_status";
            this.button_status.Size = new System.Drawing.Size(49, 47);
            this.button_status.TabIndex = 1;
            this.button_status.UseVisualStyleBackColor = false;
            this.button_status.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.button_status_PreviewKeyDown);
            // 
            // progressBar_cross
            // 
            this.progressBar_cross.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.progressBar_cross.Location = new System.Drawing.Point(171, 352);
            this.progressBar_cross.Maximum = 7300;
            this.progressBar_cross.Name = "progressBar_cross";
            this.progressBar_cross.Size = new System.Drawing.Size(192, 23);
            this.progressBar_cross.Step = 1;
            this.progressBar_cross.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar_cross.TabIndex = 5;
            this.progressBar_cross.Visible = false;
            // 
            // label_counter
            // 
            this.label_counter.BackColor = System.Drawing.Color.Transparent;
            this.label_counter.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_counter.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label_counter.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.label_counter.Location = new System.Drawing.Point(457, 27);
            this.label_counter.Name = "label_counter";
            this.label_counter.Size = new System.Drawing.Size(57, 47);
            this.label_counter.TabIndex = 6;
            // 
            // TestProtocol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(522, 749);
            this.Controls.Add(this.progressBar_cross);
            this.Controls.Add(this.label_countDown);
            this.Controls.Add(this.button_start);
            this.Controls.Add(this.pictureBox_textura);
            this.Controls.Add(this.label_counter);
            this.Controls.Add(this.button_status);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "TestProtocol";
            this.Text = "TestProtocol";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TestProtocol_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_textura)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_start;
        private System.Windows.Forms.Label label_countDown;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox pictureBox_textura;
        private System.Windows.Forms.Button button_status;
        private System.Windows.Forms.ProgressBar progressBar_cross;
        private System.Windows.Forms.Label label_counter;
    }
}