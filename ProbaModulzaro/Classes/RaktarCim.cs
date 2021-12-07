using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProbaModulzaro
{
    class RaktarCim
    {
        #region Private fileds
        ushort iranyitoSzam;
        string helyseg;
        string utca;
        string hazSzam;
        #endregion

        #region Public properties
        public ushort IranyitoSzam
        {
            get => iranyitoSzam;
            set
            {
                if (value >= 1000 && value <= 9999)
                {
                    iranyitoSzam = value;
                }
                else
                {
                    throw new Exception("Az IRÁNYÍTÓSZÁM mező értéke 1000 és 9999 között kell, hogy legyen!");
                }
            }
        }
        public string Helyseg
        {
            get => helyseg;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    helyseg = value;
                }
                else
                {
                    throw new Exception("A HELYSÉG mezőt kötelező kitölteni, nem lehet üres!");
                }
            }
        }
        public string Utca
        {
            get => utca;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    utca = value;
                }
                else
                {
                    throw new Exception("Az UTCA mezőt kötelező kitölteni, nem lehet üres!");
                }
            }
        }
        public string HazSzam
        {
            get => hazSzam;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    hazSzam = value;
                }
                else
                {
                    throw new Exception("A HÁZSZÁM mezőt kötelező kitölteni, nem lehet üres!");
                }
            }
        }
        #endregion

        #region Constructors
        public RaktarCim(ushort iranyitoSzam, string helyseg, string utca, string hazSzam)
        {
            IranyitoSzam = iranyitoSzam;
            Helyseg = helyseg;
            Utca = utca;
            HazSzam = hazSzam;
        }

        public RaktarCim(string raktar)
        {
            //RKT;00001;Raktár 1.;1132;Budapest;Kő utca;1.;15;True
            string[] tmp = raktar.Split(';');
            IranyitoSzam = ushort.Parse(tmp[3].Trim());
            Helyseg = tmp[4].Trim();
            Utca = tmp[5].Trim();
            HazSzam = tmp[6].Trim();
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return $"{IranyitoSzam}. {Helyseg}, {Utca} {HazSzam}";
        }

        public string ToCSV()
        {
            return $"{IranyitoSzam};{Helyseg};{Utca};{HazSzam}";
        }
        #endregion
    }
}
