using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProbaModulzaro
{
    enum HasznaltFemFajta
    {
        alumínium, acél, egyéb
    }
    class Fembutor : AltalanosButor
    {
        #region Private fileds
        HasznaltFemFajta femFajta;
        bool hegesztett;
        #endregion

        #region Public properties
        internal HasznaltFemFajta FemFajta { get => femFajta; set => femFajta = value; }
        public bool Hegesztett { get => hegesztett; set => hegesztett = value; }
        #endregion

        #region Constructors
        public Fembutor(HasznaltFemFajta femFajta, bool hegesztett, string cikkSzam, string megnevezes, int gyartasiEv, ButorTipusa butorTipus, FelhasznalasiHelye felhasznalasiHely, bool hasznaltButor, uint butorAra) : base(cikkSzam, megnevezes, gyartasiEv, butorTipus, felhasznalasiHely, hasznaltButor, butorAra)
        {
            FemFajta = femFajta;
            Hegesztett = hegesztett;
        }

        public Fembutor(string butor) : base(butor)
        {
            //FAB;FAB00001;Íróasztal;2000;0;0;False;12500;0;False
            string[] tmp = butor.Split(';');
            FemFajta = (HasznaltFemFajta)int.Parse(tmp[8].Trim());
            Hegesztett = bool.Parse(tmp[9].Trim());
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return $"{base.ToString()} ({FemFajta} - {(Hegesztett ? "Hegesztett" : "Csavarozott")})";
        }

        public override string ToCSV()
        {
            return $"FEM;{base.ToCSV()};{(int)FemFajta};{Hegesztett}";
        }
        public override string ButorAdatok()
        {
            return $"{base.ButorAdatok()}\n\r[FÉMBÚTOR] Felhasznált fémfajta: {FemFajta} - {(Hegesztett ? "Hegesztett" : "Csavarozott")}";
        }
        #endregion
    }
}
