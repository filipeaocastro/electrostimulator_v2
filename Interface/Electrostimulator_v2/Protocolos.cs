using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eletroestimulador_v02
{
    class Protocolos
    {
        public const string amplitude = "IAM-";
        public const string frequencia = "FRQ-";
        public const string larguraPulso = "BDW-";
        public const string duracaoTotal = "TDR-";
        //public const string duracaoBurst = "BRW";
        //public const string intervaloBurst = "BRI";
        //public const string duracaoTB = "BTW";
        //public const string intervaloTB = "BTI";

        //public const string intervaloRndOFF = "RNDOFF";
        //public const string intervaloTBmin = "RNDMIN";
        //public const string intervaloTBmax = "RNDMAX";

        public const string iniciar = "STA";
        public const string parar = "STO";
        //public const string relatorio = "REP";
        //public const string atualizar = "ATT";
        //public const string correntePositiva = "POS";
        //public const string correnteNegativa = "NEG";

        public const string wf_spike = "WFM-SPK";
        public const string wf_sin = "WFM-SIN";
        public const string wf_triangular = "WFM-TRI";
        public const string wf_sawtooth = "WFM-DTS";
        public const string wf_square = "WFM-SQR";

        public const string iDirection_anodic = "IDR-ANO";
        public const string iDirection_cathodic = "IDR-CAT";
        public const string iDirection_biDirectional = "IDR-BID";

        public const string init_spk_transfer = "BGN-";
        public const string end_spk_transfer = "END";


        public List<string> Cods()
        {
            List<string> prot_base = new List<string>();
            prot_base.Add(amplitude);
            prot_base.Add(frequencia);
            prot_base.Add(duracaoTotal);
            prot_base.Add(larguraPulso);
            
            //prot_base.Add(duracaoBurst);
            //prot_base.Add(intervaloBurst);
            //prot_base.Add(duracaoTB);
            /*
            if(!rnd_on)
            {
                prot_base.Add(intervaloTB);
            }
            else
            {
                prot_base.Add(intervaloTBmin);
                prot_base.Add(intervaloTBmax);
            }*/


            return prot_base;
        }
    }

    
}
