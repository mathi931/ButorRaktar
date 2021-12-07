using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProbaModulzaro
{
    enum Valasztas
    {
        választás, fabútor, fémbútor
    }

    enum ButorTipusa
    {
        asztal, szék, szekrény, ágy, egyéb
    }
    enum FelhasznalasiHelye
    {
        nappali, hálószoba, konyha, fürdőszoba, előszoba, egyéb
    }

    abstract class AltalanosButor
    {
        #region Private fileds
        string cikkSzam;
        string megnevezes;
        int gyartasiEv;
        ButorTipusa butorTipus;
        FelhasznalasiHelye felhasznalasiHely;
        bool hasznaltButor;
        uint butorAra;
        #endregion

        #region Public properties
        public string CikkSzam
        {
            get => cikkSzam;
            private set
            {
                if (!string.IsNullOrEmpty(value) && value.Length == 8 && CikkSzamEllenorzes(value))
                {
                    cikkSzam = value;
                }
                else
                {
                    throw new Exception("A BÚTOR CIKKSZÁMA mezőt kötelező kitölteni, nem lehet üres,\n\rvalamint pontosan 8 karakter kell, hogy legyen!\n\rA formátuma kötött:\n\rFABxxxxx vagy FEMxxxxx, ahol x egy 5 karakterből álló futó sorszám!");
                }
            }
        }
        public string Megnevezes
        {
            get => megnevezes;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    megnevezes = value;
                }
                else
                {
                    throw new Exception("A BÚTOR MEGNEVEZÉSE mezőt kötelező kitölteni, nem lehet üres!");
                }
            }
        }
        public int GyartasiEv
        {
            get => gyartasiEv;
            set
            {
                if (value >= DateTime.MinValue.Year && value <= DateTime.Now.Year)
                {
                    gyartasiEv = value;
                }
                else
                {
                    throw new Exception("A GYÁRTÁSI ÉV mező nem lehet üres, és nem lehet jövőbeni dátum!");
                }
            }
        }
        internal ButorTipusa ButorTipus { get => butorTipus; set => butorTipus = value; }
        internal FelhasznalasiHelye FelhasznalasiHely { get => felhasznalasiHely; set => felhasznalasiHely = value; }
        public bool HasznaltButor { get => hasznaltButor; set => hasznaltButor = value; }
        public uint ButorAra
        {
            get => butorAra;
            set
            {
                if (value > 0)
                {
                    butorAra = value;
                }
                else
                {
                    throw new Exception("A BÚTOR ÁRA mező értéke nem lehet 0!");
                }
            }
        }
        #endregion

        #region Constructors
        protected AltalanosButor(string cikkSzam, string megnevezes, int gyartasiEv, ButorTipusa butorTipus, FelhasznalasiHelye felhasznalasiHely, bool hasznaltButor, uint butorAra)
        {
            CikkSzam = cikkSzam;
            Megnevezes = megnevezes;
            GyartasiEv = gyartasiEv;
            ButorTipus = butorTipus;
            FelhasznalasiHely = felhasznalasiHely;
            HasznaltButor = hasznaltButor;
            ButorAra = butorAra;
        }

        protected AltalanosButor(string butor)
        {
            //FAB;FAB00001;Íróasztal;2000;0;0;False;12500;0;False
            string[] tmp = butor.Split(';');
            CikkSzam = tmp[1].Trim();
            Megnevezes = tmp[2].Trim();
            GyartasiEv = int.Parse(tmp[3].Trim());
            ButorTipus = (ButorTipusa)int.Parse(tmp[4].Trim());
            FelhasznalasiHely = (FelhasznalasiHelye)int.Parse(tmp[5].Trim());
            HasznaltButor = bool.Parse(tmp[6].Trim());
            ButorAra = uint.Parse(tmp[7].Trim());
        }
        #endregion

        #region Methods
        private bool CikkSzamEllenorzes(string cikkSzam)
        {
            return (cikkSzam.StartsWith("FAB") || cikkSzam.StartsWith("FEM")) && int.TryParse(cikkSzam.Substring(3), out int result) ? true : false;
        }
        public override string ToString()
        {
            return $"{CikkSzam} - {Megnevezes} - {GyartasiEv} - {ButorTipus} - {FelhasznalasiHely} - {(HasznaltButor ? "Használt" : "Új")} bútor - {String.Format(CultureInfo.CurrentCulture, "{0:C0}", ButorAra)}";
        }

        public virtual string ToCSV()
        {
            //FAB; FAB00001; Íróasztal; 2000; 0; 0; False; 12500; 0; False
            return $"{CikkSzam};{Megnevezes};{GyartasiEv};{(int)ButorTipus};{(int)FelhasznalasiHely};{HasznaltButor};{ButorAra}";
        }

        public virtual string ButorAdatok()
        {
            return $"Cikkszám: {CikkSzam} - Megnevezés: {Megnevezes} - Gyártási év: {GyartasiEv}\n\rBútortípus: {ButorTipus} - Felhasználási hely: {FelhasznalasiHely} Haszált bútor: {(HasznaltButor? "Igen":"Nem")} - Bútor ára: {String.Format(CultureInfo.CurrentCulture, "{0:C0}", ButorAra)}";
        }
        #endregion
    }
}
