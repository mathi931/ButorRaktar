using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProbaModulzaro
{
    enum Funkcio
    {
        RaktárLétrehozás,
        RaktárMódosítás,
        RaktárMegjelenítés,
        RaktárTörlés,
        BútorLétrehozás,
        BútorMódosítás,
        BútorMegjelenítés,
        BútorTörlés,
        RaktárakMegjelenítése,
        BútorokMegjelenítése,
        Rendezések
    }
    static class LogKezeles
    {
        #region Private fildes
        static StreamWriter logFile;
        #endregion

        #region Public properties

        #endregion

        #region Constructors

        #endregion

        #region Methods
        public static void LogNyitas()
        {
            logFile = new StreamWriter("ButorRaktarKezelo.log");
            logFile.WriteLine("********************");
            logFile.WriteLine($"Programfutás kezdete: {DateTime.Now}");
            logFile.WriteLine($"Futtató felhasználó: {Environment.UserName}");
            logFile.WriteLine("********************");
            logFile.Flush();
        }

        public static void LogZaras()
        {
            logFile.WriteLine("********************");
            logFile.WriteLine($"Programfutás vége: {DateTime.Now}");
            logFile.WriteLine($"Futtató felhasználó: {Environment.UserName}");
            logFile.WriteLine("********************");
            logFile.Close();
        }

        public static void LogIras(Funkcio id, ButorRaktar raktar = null, AltalanosButor butor = null)
        {
            switch (id)
            {
                case Funkcio.RaktárLétrehozás:
                    try
                    {
                        logFile.WriteLine($"Bútorraktár létrehozva: {raktar.RaktarSzam} ({DateTime.Now})");
                        logFile.Flush();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    break;
                case Funkcio.RaktárMódosítás:
                    try
                    {
                        logFile.WriteLine($"Bútorraktár módosítva: {raktar.RaktarSzam} ({DateTime.Now})");
                        logFile.Flush();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    break;
                case Funkcio.RaktárMegjelenítés:
                    try
                    {
                        logFile.WriteLine($"Bútorraktár megjelenítve: {raktar.RaktarSzam} ({DateTime.Now})");
                        logFile.Flush();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    break;
                case Funkcio.RaktárTörlés:
                    try
                    {
                        logFile.WriteLine($"Bútorraktár törölve: {raktar.RaktarSzam} ({DateTime.Now})");
                        logFile.Flush();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    break;
                case Funkcio.BútorLétrehozás:
                    try
                    {
                        if (butor is Fabutor)
                        {
                            logFile.WriteLine($"Fabútor létrehozva: {butor.CikkSzam}, Bútorraktár száma: {raktar.RaktarSzam} ({DateTime.Now})");
                            logFile.Flush();
                        }
                        else
                        {
                            logFile.WriteLine($"Fémbútor létrehozva: {butor.CikkSzam}, Bútorraktár száma: {raktar.RaktarSzam} ({DateTime.Now})");
                            logFile.Flush();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    break;
                case Funkcio.BútorMódosítás:
                    try
                    {
                        if (butor is Fabutor)
                        {
                            logFile.WriteLine($"Fabútor módosítva: {butor.CikkSzam}, Bútorraktár száma: {raktar.RaktarSzam} ({DateTime.Now})");
                            logFile.Flush();
                        }
                        else
                        {
                            logFile.WriteLine($"Fémbútor módosítva: {butor.CikkSzam}, Bútorraktár száma: {raktar.RaktarSzam} ({DateTime.Now})");
                            logFile.Flush();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    break;
                case Funkcio.BútorMegjelenítés:
                    try
                    {
                        if (butor is Fabutor)
                        {
                            logFile.WriteLine($"Fabútor megjelenítve: {butor.CikkSzam}, Bútorraktár száma: {raktar.RaktarSzam} ({DateTime.Now})");
                            logFile.Flush();
                        }
                        else
                        {
                            logFile.WriteLine($"Fémbútor megjelenítve: {butor.CikkSzam}, Bútorraktár száma: {raktar.RaktarSzam} ({DateTime.Now})");
                            logFile.Flush();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    break;
                case Funkcio.BútorTörlés:
                    try
                    {
                        if (butor is Fabutor)
                        {
                            logFile.WriteLine($"Fabútor törölve: {butor.CikkSzam}, Bútorraktár száma: {raktar.RaktarSzam} ({DateTime.Now})");
                            logFile.Flush();
                        }
                        else
                        {
                            logFile.WriteLine($"Fémbútor törölve: {butor.CikkSzam}, Bútorraktár száma: {raktar.RaktarSzam} ({DateTime.Now})");
                            logFile.Flush();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    break;
                case Funkcio.RaktárakMegjelenítése:
                    logFile.WriteLine($"Raktárak megjelenítve ({DateTime.Now})");
                    logFile.Flush();
                    break;
                case Funkcio.BútorokMegjelenítése:
                    logFile.WriteLine($"Bútorok megjelenítve ({DateTime.Now})");
                    logFile.Flush();
                    break;
                case Funkcio.Rendezések:
                    logFile.WriteLine($"Rendezések megjelenítve ({DateTime.Now})");
                    logFile.Flush();
                    break;
            }
        }
        #endregion
    }
}
