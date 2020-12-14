using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Eletroestimulador_v02
{
    public partial class spikeParameters : UserControl
    {
        private bool confFileExist;
        private string confFileName = "conf.txt";
        private string confDocPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private string confFilePath;

        private int amplitude;
        private int direction;
        private int stimuliQuantity;
        private string direction_str;
        private int textNumber;


         public spikeParameters()
        {
            InitializeComponent();
            
        }

        public spikeParameters(int _amplitude, int _direction, int _stimuliQuantity)
        {
            InitializeComponent();

            amplitude = _amplitude;
            direction = _direction;
            stimuliQuantity = _stimuliQuantity;

            textBox_amplitude.Text = amplitude.ToString();
            /*
            if (direction == "AND")
                radioButton_anodic.Checked = true;*/

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

        */ 

        public void writeFile()
        {
            StreamWriter fileOutput = new StreamWriter(confFilePath, false);
            fileOutput.WriteLine("Amplitude\t" + amplitude.ToString() + Environment.NewLine);
            fileOutput.WriteLine("Current_Direction\t" + direction_str + Environment.NewLine);
            fileOutput.WriteLine("Texture\t" + textNumber.ToString() + Environment.NewLine);
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

                if (checkBox_random.Checked)
                    textNumber = 0;
                else
                    textNumber = Convert.ToInt32(numericUpDown_textureNumber.Value);

                writeFile();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString()); 
            }

            this.Hide();
            
        }

        private void checkBox_random_CheckedChanged(object sender, EventArgs e)
        {
            label_textureNumber.Enabled = !checkBox_random.Checked;
            numericUpDown_textureNumber.Enabled = !checkBox_random.Checked;
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
