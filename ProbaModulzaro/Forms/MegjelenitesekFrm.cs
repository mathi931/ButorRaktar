using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProbaModulzaro
{
    public partial class MegjelenitesekFrm : Form
    {
        List<ButorRaktar> raktarLista;
        List<AltalanosButor> butorLista;
        internal MegjelenitesekFrm(List<ButorRaktar> raktarLista, int index = -1)
        {
            InitializeComponent();
            if (index == -1)
            {
                this.raktarLista = raktarLista;
                ListViewToltes(true);
            }
            else
            {
                this.butorLista = raktarLista[index].Butorok;
                ListViewToltes(false);
            }
        }

        private void ListViewToltes(bool raktar)
        {
            if (raktar)
            {
                lsvAdatok.Columns.Add("Raktár száma", -2);
                lsvAdatok.Columns.Add("Raktár neve", -2);
                lsvAdatok.Columns.Add("Raktár címe", -2);
                lsvAdatok.Columns.Add("Bútorok max.száma", -2);
                lsvAdatok.Columns.Add("Nyitva van-e vasárnap?", -2);
                lsvAdatok.Columns.Add("Bútorok össz.ára", -2);
                foreach (ButorRaktar item in raktarLista)
                {
                    ListViewItem elem = new ListViewItem(item.RaktarSzam);
                    elem.SubItems.Add(item.RaktarNev);
                    elem.SubItems.Add(item.RaktarCime.ToString());
                    elem.SubItems.Add(item.ButorokMaxSzama.ToString());
                    elem.SubItems.Add(item.Vasarnap ? "Igen" : "Nem");
                    elem.SubItems.Add(String.Format(CultureInfo.CurrentCulture, "{0:C0}", item.TaroltButorokAra));
                    lsvAdatok.Items.Add(elem);
                }
            }
            else
            {
                lsvAdatok.Columns.Add("Cikkszám", -2);
                lsvAdatok.Columns.Add("Megnevezés", -2);
                lsvAdatok.Columns.Add("Gyártási év", -2);
                lsvAdatok.Columns.Add("Bútortípus", -2);
                lsvAdatok.Columns.Add("Felhaszn.hely", -2);
                lsvAdatok.Columns.Add("Használta bútor?", -2);
                lsvAdatok.Columns.Add("Bútor ára", -2);
                lsvAdatok.Columns.Add("Fafajta", -2);
                lsvAdatok.Columns.Add("Kezelt?", -2);
                lsvAdatok.Columns.Add("Fémfajta", -2);
                lsvAdatok.Columns.Add("Hegesztett?", -2);
                foreach (AltalanosButor item in butorLista)
                {
                    ListViewItem elem = new ListViewItem(item.CikkSzam);
                    elem.SubItems.Add(item.Megnevezes);
                    elem.SubItems.Add(item.GyartasiEv.ToString());
                    elem.SubItems.Add(item.ButorTipus.ToString());
                    elem.SubItems.Add(item.FelhasznalasiHely.ToString());
                    elem.SubItems.Add(item.HasznaltButor ? "Igen" : "Nem");
                    elem.SubItems.Add(String.Format(CultureInfo.CurrentCulture, "{0:C0}", item.ButorAra));
                    if (item is Fabutor)
                    {
                        elem.SubItems.Add((item as Fabutor).FaFajta.ToString());
                        elem.SubItems.Add((item as Fabutor).Kezelt ? "Kezelt" : "Kezeletlen");
                        elem.SubItems.Add("N/A");
                        elem.SubItems.Add("N/A");
                    }
                    else
                    {
                        elem.SubItems.Add("N/A");
                        elem.SubItems.Add("N/A");
                        elem.SubItems.Add((item as Fembutor).FemFajta.ToString());
                        elem.SubItems.Add((item as Fembutor).Hegesztett ? "Hegesztett" : "Csavarozott");
                    }
                    lsvAdatok.Items.Add(elem);
                }
            }
        }
    }
}
