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

namespace Eletroestimulador_v02
{
    public partial class spkParameters : Form
    {
        private bool confFileExist;
        private string confFileName = "conf.txt";
        private string confDocPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private string confFilePath;

        private int amplitude;
        //private int direction;
        private int stimuliQuantity;
        private int width;
        //private string direction_str;
        private int textNumber;

        public spkParameters()
        {
            InitializeComponent();
        }

        public spkParameters(int _amplitude, int _stimuliQuantity, int _width, int _textNumber)
        {
            InitializeComponent();

            amplitude = _amplitude;
            //direction = _direction;
            stimuliQuantity = _stimuliQuantity;
            width = _width;
            textNumber = _textNumber;

            updateInterface();

            numericUpDown_textureNumber.Maximum = stimuliQuantity;
        }

        public void setTextureNumber(int number)
        {
            numericUpDown_textureNumber.Maximum = number;
        }

        /*
          File structure:
         
            Amplitude   XXXX
            Current_Direction   AND or CAT
            Texture     X    (0 means random)
            Width   XXXX

        */

        public void writeFile()
        {
            confFilePath = Path.Combine(confDocPath, confFileName);
            StreamWriter fileOutput = new StreamWriter(confFilePath, false);
            fileOutput.WriteLine("Amplitude\t" + amplitude.ToString() + Environment.NewLine);
            //fileOutput.WriteLine("Current_Direction\t" + direction_str + Environment.NewLine);
            fileOutput.WriteLine("Texture\t" + textNumber.ToString() + Environment.NewLine);
            fileOutput.WriteLine("Width\t" + width.ToString() + Environment.NewLine);
            fileOutput.Close();
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            try
            {
                if ((textBox_amplitude.Text == "") || (textBox_amplitude.Text == null))
                {
                    MessageBox.Show("Define the amplitude before saving!", "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                amplitude = Convert.ToInt32(textBox_amplitude.Text);
                width = trackBar_spkWidth.Value;
                /*
                if (radioButton_anodic.Checked)
                {
                    direction = 0;
                    direction_str = "AND";
                }

                else
                {
                    direction = 1;
                    direction_str = "CAT";
                }
                */
                if (checkBox_random.Checked)
                    textNumber = 0;
                else
                    textNumber = Convert.ToInt32(numericUpDown_textureNumber.Value);

                

                writeFile();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            this.Hide();
        }

        private void updateInterface()
        {
            textBox_amplitude.Text = amplitude.ToString();
            trackBar_spkWidth.Value = width;

            if (textNumber == 0)
                checkBox_random.Checked = true;
            else
            {
                numericUpDown_textureNumber.Value = textNumber;
                checkBox_random.Checked = false;
            }
                
            /*
            if (direction == 0)
                radioButton_anodic.Checked = true;
            else if (direction == 1)
                radioButton_cathodic.Checked = true;*/
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void checkBox_random_CheckedChanged(object sender, EventArgs e)
        {
            label_textureNumber.Enabled = !checkBox_random.Checked;
            numericUpDown_textureNumber.Enabled = !checkBox_random.Checked;
        }

        private void trackBar_spkWidth_Scroll(object sender, EventArgs e)
        {
            width = trackBar_spkWidth.Value;
            label_spkWidth_value.Text = width.ToString() + " μs";
        }
    }
}
