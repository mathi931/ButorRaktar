using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProbaModulzaro
{
    class ButorRaktar
    {
        #region Private fileds
        string raktarSzam;
        string raktarNev;
        RaktarCim raktarCime;
        byte butorokMaxSzama;
        bool vasarnap;
        List<AltalanosButor> butorok;
        #endregion

        #region Public properties
        public string RaktarSzam
        {
            get => raktarSzam;
            private set
            {
                if (!string.IsNullOrEmpty(value) && value.Length == 5)
                {
                    raktarSzam = value;
                }
                else
                {
                    throw new Exception("A RAKTÁR SZÁMA mezőt kötelező kitölteni, nem lehet üres, valamint pontosan 5 karakter kell, hogy legyen!");
                }
            }
        }
        public string RaktarNev
        {
            get => raktarNev;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    raktarNev = value;
                }
                else
                {
                    throw new Exception("A RAKTÁR NEVE mezőt kötelező kitölteni, nem lehet üres!");
                }
            }
        }
        internal RaktarCim RaktarCime { get => raktarCime; set => raktarCime = value; }
        public byte ButorokMaxSzama
        {
            get => butorokMaxSzama;
            set
            {
                if (value >= 10 && value <= 100)
                {
                    butorokMaxSzama = value;
                }
                else
                {
                    throw new Exception("A BÚTOROK MAXIMÁLIS SZÁMA mező értéke 10 és 100 között kell, hogy legyen!");
                }
            }
        }
        public bool Vasarnap { get => vasarnap; set => vasarnap = value; }
        internal List<AltalanosButor> Butorok
        {
            get => butorok.ToList();
            //get
            //{
            //    List<AltalanosButor> visszaAd = new List<AltalanosButor>();
            //    foreach (AltalanosButor item in butorok)
            //    {
            //        visszaAd.Add(item);
            //    }
            //    return visszaAd;
            //}
            //set => butorok = value; 
        }

        public uint TaroltButorokAra
        {
            get
            {
                uint butorokAraOsszesen = 0;
                foreach (AltalanosButor item in butorok)
                {
                    butorokAraOsszesen += item.ButorAra;
                }
                return butorokAraOsszesen;
            }
        }

        public uint TaroltButorokSzama
        {
            get
            {
                return (byte)butorok.Count;
                //byte butorokSzamaOsszesen = 0;
                //foreach (AltalanosButor item in butorok)
                //{
                //    butorokSzamaOsszesen++;
                //}
                //return butorokSzamaOsszesen;
            }
        }
        #endregion

        #region Constructors
        public ButorRaktar(string raktarSzam, string raktarNev, RaktarCim raktarCime, byte butorokMaxSzama, bool vasarnap)
        {
            butorok = new List<AltalanosButor>();
            RaktarSzam = raktarSzam;
            RaktarNev = raktarNev;
            RaktarCime = raktarCime;
            ButorokMaxSzama = butorokMaxSzama;
            Vasarnap = vasarnap;
        }

        public ButorRaktar(string raktar)
        {
            // RKT;00001;Raktár 1.;1132;Budapest;Kő utca;1.;15;True
            butorok = new List<AltalanosButor>();
            string[] tmp = raktar.Split(';');
            RaktarSzam = String.Format("{0:00000}", int.Parse(tmp[1].Trim()));
            RaktarNev = tmp[2].Trim();
            RaktarCime = new RaktarCim(raktar);
            butorokMaxSzama = byte.Parse(tmp[7].Trim());
            Vasarnap = bool.Parse(tmp[8].Trim());
        }
        #endregion

        #region Methods
        public static string RaktarSzamMeghatarozas(List<ButorRaktar> raktarLista)
        {
            if (raktarLista.Count == 0)
            {
                // Nincs raktár, visszaadom a 00001 azonosítót.
                return String.Format("{0:00000}", 1);
            }
            else
            {
                //string tmpSzam = raktarLista.Last().RaktarSzam;
                //int tmpInt = int.Parse(tmpSzam);
                //int EzKell = tmpInt + 1;
                // Van raktár, visszaadom az utolsó azonosító + 1 értéket.
                return String.Format("{0:00000}", (int.Parse(raktarLista.Last().raktarSzam)) + 1);
            }
        }

        public string CikkSzamMeghatarozas(bool faButor)
        {
            string cikkSzamTmp = String.Empty;
            if (faButor)
            {
                foreach (AltalanosButor item in butorok)
                {
                    if (item.CikkSzam.StartsWith("FAB"))
                    {
                        cikkSzamTmp = item.CikkSzam;
                    }
                }
            }
            else
            {
                foreach (AltalanosButor item in butorok)
                {
                    if (item.CikkSzam.StartsWith("FEM"))
                    {
                        cikkSzamTmp = item.CikkSzam;
                    }
                }
            }
            if (cikkSzamTmp == String.Empty)
            {
                return faButor ? $"FAB{String.Format("{0:00000}", 1)}" : $"FEM{String.Format("{0:00000}", 1)}";
            }
            else
            {
                return faButor ? $"FAB{String.Format("{0:00000}", (int.Parse(cikkSzamTmp.Substring(3))) + 1)}" : $"FEM{String.Format("{0:00000}", (int.Parse(cikkSzamTmp.Substring(3))) + 1)}";
            }

            //// ***** LINQ-val megoldva
            //if (faButor)
            //{
            //    return (butorok.Where(item => item.CikkSzam.StartsWith("FAB")).ToList().Count == 0) ? $"FAB{String.Format("{0:00000}", 1)}" : $"FAB{String.Format("{0:00000}", (int.Parse(butorok.Where(item => item.CikkSzam.StartsWith("FAB")).ToList().Last().CikkSzam.Substring(3))) + 1)}";
            //}
            //else
            //{
            //    return (butorok.Where(item => item.CikkSzam.StartsWith("FEM")).ToList().Count == 0) ? $"FEM{String.Format("{0:00000}", 1)}" : $"FEM{String.Format("{0:00000}", (int.Parse(butorok.Where(item => item.CikkSzam.StartsWith("FEM")).ToList().Last().CikkSzam.Substring(3))) + 1)}";
            //}
            //// ***** LINQ-val megoldva
        }

        public void UjButor(AltalanosButor uj)
        {
            if (TaroltButorokSzama + 1 <= ButorokMaxSzama)
            {
                butorok.Add(uj);
            }
        }
        public void ButorModositas(int index, AltalanosButor modositando)
        {
            butorok.RemoveAt(index);
            butorok.Insert(index, modositando);
        }
        public void ButorTorles(int index)
        {
            butorok.RemoveAt(index);
        }

        public static List<ButorRaktar> ImportFeltoltes(string fileNev)
        {
            List<ButorRaktar> lista = new List<ButorRaktar>();
            ButorRaktar tmp = null;

            // Állomány első sora RKT-val kezdődik-e?
            if (File.ReadLines(fileNev, Encoding.UTF8).First().StartsWith("RKT"))
            {
                foreach (string item in File.ReadAllLines(fileNev, Encoding.UTF8))
                {
                    try
                    {
                        // Raktár feldolgozás
                        if (item.StartsWith("RKT"))
                        {
                            if (tmp != null)
                            {
                                lista.Add(tmp);
                                tmp = null;
                            }
                            tmp = new ButorRaktar(item);
                        }
                        // Fabútor feldolgozás
                        else if (item.StartsWith("FAB"))
                        {
                            tmp.butorok.Add(new Fabutor(item));
                        }
                        // Fémbútor feldolgozás
                        else if (item.StartsWith("FEM"))
                        {
                            tmp.butorok.Add(new Fembutor(item));
                        }
                        // Hibadobás
                        else
                        {
                            throw new Exception();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
                lista.Add(tmp);
            }
            else
            {
                throw new Exception("Hibás állomány! Importálás nem lehetséges.");
            }

            return lista;
        }

        public static void ExportLetoltes(string fileNev, List<ButorRaktar> raktarLista)
        {
            try
            {
                StreamWriter export = new StreamWriter(fileNev, false, Encoding.UTF8);
                foreach (ButorRaktar item in raktarLista)
                {
                    export.WriteLine(item.ToCSV());
                    foreach (AltalanosButor butorItem in item.Butorok)
                    {
                        if (butorItem is Fabutor)
                        {
                            export.WriteLine((butorItem as Fabutor).ToCSV());
                        }
                        else
                        {
                            export.WriteLine((butorItem as Fembutor).ToCSV());
                        }
                    }
                }
                export.Close();
            }
            catch (Exception)
            {
                throw new Exception("Az exportálás sikertelen!");
            }
        }

        public static uint RaktarArOsszesen(List<ButorRaktar> raktarLista)
        {
            uint osszesenAr = 0;
            foreach (ButorRaktar item in raktarLista)
            {
                osszesenAr += item.TaroltButorokAra;
            }
            return osszesenAr;
        }

        public override string ToString()
        {
            return $"{RaktarSzam} - {RaktarNev} - {RaktarCime} - {ButorokMaxSzama} - Vasárnap {(vasarnap ? "nyitva" : "zárva")} - {String.Format(CultureInfo.CurrentCulture, "{0:C0}", TaroltButorokAra)}";
        }

        public string ToCSV()
        {
            //RKT; 00001; Raktár 1.; 1132; Budapest; Kő utca; 1.; 15; True
            return $"RKT;{RaktarSzam};{RaktarNev};{RaktarCime.ToCSV()};{ButorokMaxSzama};{Vasarnap}";
        }

        public string RaktarAdatok()
        {
            string fabutorok = $"\n\r\n\rFABÚTOROK";
            string fembutorok = $"\n\r\n\rFÉMBÚTOROK";

            foreach (AltalanosButor item in Butorok)
            {
                if (item is Fabutor)
                {
                    fabutorok += $"\n\r\tCikkszám: {item.CikkSzam} - Megnevezés: {item.Megnevezes}";
                }
                else
                {
                    fembutorok += $"\n\r\tCikkszám: {item.CikkSzam} - Megnevezés: {item.Megnevezes}";
                }
            }

            return $"Bútorraktárban lévő bútorok darabszáma: {TaroltButorokSzama} darab\n\rBútorok összértéke: {String.Format(CultureInfo.CurrentCulture, "{0:C0}", TaroltButorokAra)}{fabutorok}{fembutorok}";
        }
        #endregion
    }
}
