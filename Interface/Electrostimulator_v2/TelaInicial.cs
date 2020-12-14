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
    public partial class TelaInicial : Form
    {
        public TelaInicial()
        {
            InitializeComponent();
        }

        TelaMain telaMain;
        SpikesForm telaSpikes;

        private void button_novo_Click(object sender, EventArgs e)
        {
            hideCtrls();
            telaMain = new TelaMain();
            this.Hide();
            telaMain.Show();
        }



        private void hideCtrls()
        {
            label_eletroestimulador.Visible = false;
            button_carregar.Visible = false;
            button_novo.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            hideCtrls();
            telaSpikes = new SpikesForm();
            this.Hide();
            telaSpikes.Show();
        }
    }
}
