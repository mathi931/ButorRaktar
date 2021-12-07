using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProbaModulzaro
{
    enum HasznaltFaFajta
    {
        bükk, nyár, tölgy, fenyő, egyéb
    }
    class Fabutor : AltalanosButor
    {
        #region Private fileds
        HasznaltFaFajta faFajta;
        bool kezelt;
        #endregion

        #region Public properties
        internal HasznaltFaFajta FaFajta { get => faFajta; set => faFajta = value; }
        public bool Kezelt { get => kezelt; set => kezelt = value; }
        #endregion

        #region Constructors
        public Fabutor(HasznaltFaFajta faFajta, bool kezelt, string cikkSzam, string megnevezes, int gyartasiEv, ButorTipusa butorTipus, FelhasznalasiHelye felhasznalasiHely, bool hasznaltButor, uint butorAra) : base(cikkSzam, megnevezes, gyartasiEv, butorTipus, felhasznalasiHely, hasznaltButor, butorAra)
        {
            FaFajta = faFajta;
            Kezelt = kezelt;
        }

        public Fabutor(string butor) : base(butor)
        {
            //FAB;FAB00001;Íróasztal;2000;0;0;False;12500;0;False
            string[] tmp = butor.Split(';');
            FaFajta = (HasznaltFaFajta)int.Parse(tmp[8].Trim());
            Kezelt = bool.Parse(tmp[9].Trim());
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return $"{base.ToString()} ({FaFajta} - {(kezelt ? "Kezelt" : "Kezeletlen")})";
        }

        public override string ToCSV()
        {
            return $"FAB;{base.ToCSV()};{(int)FaFajta};{Kezelt}";
        }

        public override string ButorAdatok()
        {
            return $"{base.ButorAdatok()}\n\r[FABÚTOR] Felhasznált fafajta: {FaFajta} - {(Kezelt? "Kezelt": "Kezeletlen")}";
        }
        #endregion
    }
}
