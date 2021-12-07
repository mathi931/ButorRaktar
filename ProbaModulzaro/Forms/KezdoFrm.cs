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
    public partial class KezdoFrm : Form
    {
        #region Private fields
        List<ButorRaktar> raktarLista;
        #endregion

        #region Constructors
        public KezdoFrm()
        {
            InitializeComponent();
            raktarLista = new List<ButorRaktar>();
            LogKezeles.LogNyitas();
        }
        #endregion

        #region Form events
        private void KezdoFrm_Load(object sender, EventArgs e)
        {
            if (MessageBox.Show("Szeretne kezdőállományt importálni?", "Importálás...", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                openFileDialog1.InitialDirectory = Environment.CurrentDirectory;
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        raktarLista = ButorRaktar.ImportFeltoltes(openFileDialog1.FileName);
                        lsbButorRaktarak.DataSource = raktarLista;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                    }
                }
            }
            if (lsbButorRaktarak.Items.Count > 0)
            {
                lsbButorRaktarak.SelectedIndex = 0;
            }
            GombokEngedelyezese();
            this.Activate();
        }

        private void KezdoFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Valóban ki akar lépni?", "Kilépés", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
            else
            {
                if (openFileDialog1.FileName == "DEFAULT" && lsbButorRaktarak.Items.Count > 0 && MessageBox.Show("Szeretné exportálni az adatokat?", "Exportálás...", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            ButorRaktar.ExportLetoltes(saveFileDialog1.FileName, raktarLista);
                            MessageBox.Show("A mentés sikeresen megtörtént.", "Mentés...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LogKezeles.LogZaras();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            e.Cancel = true;
                        }
                    }
                }
                else if (openFileDialog1.FileName != "DEFAULT")
                {
                    try
                    {
                        ButorRaktar.ExportLetoltes(openFileDialog1.FileName, raktarLista);
                        MessageBox.Show("A mentés sikeresen megtörtént.", "Mentés...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LogKezeles.LogZaras();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        e.Cancel = true;
                    }
                }
            }
        }
        #endregion

        #region Component events
        private void btnRaktarUj_Click(object sender, EventArgs e)
        {
            //string raktarSzam = ButorRaktar.RaktarSzamMeghatarozas(raktarLista);
            RaktarKezelesFrm frm = new RaktarKezelesFrm(ButorRaktar.RaktarSzamMeghatarozas(raktarLista));
            if (frm.ShowDialog() == DialogResult.OK)
            {
                raktarLista.Add(frm.Raktar);
                lsbButorRaktarak.DataSource = null;
                lsbButorRaktarak.DataSource = raktarLista;
                lsbButorRaktarak.SelectedIndex = lsbButorRaktarak.Items.Count - 1;
                LogKezeles.LogIras(Funkcio.RaktárLétrehozás, frm.Raktar);
            }
        }

        private void btnRaktarModosit_Click(object sender, EventArgs e)
        {
            if (lsbButorRaktarak.SelectedIndex != -1)
            {
                RaktarKezelesFrm frm = new RaktarKezelesFrm((ButorRaktar)lsbButorRaktarak.SelectedItem);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    int index = lsbButorRaktarak.SelectedIndex;
                    lsbButorRaktarak.DataSource = null;
                    lsbButorRaktarak.DataSource = raktarLista;
                    lsbButorRaktarak.SelectedIndex = index;
                    LogKezeles.LogIras(Funkcio.RaktárMódosítás, frm.Raktar);
                    //LogKezeles.LogIras(Funkcio.RaktárMódosítás, (ButorRaktar)lsbButorRaktarak.SelectedItem);
                    //LogKezeles.LogIras(Funkcio.RaktárMódosítás, raktarLista[index]);
                }
            }
        }

        private void btnRaktarMegjelenit_Click(object sender, EventArgs e)
        {
            if (lsbButorRaktarak.SelectedIndex != -1)
            {
                RaktarKezelesFrm frm = new RaktarKezelesFrm((ButorRaktar)lsbButorRaktarak.SelectedItem, true);
                frm.ShowDialog();
                LogKezeles.LogIras(Funkcio.RaktárMegjelenítés, frm.Raktar);
                //LogKezeles.LogIras(Funkcio.RaktárMegjelenítés, (ButorRaktar)lsbButorRaktarak.SelectedItem);
                //LogKezeles.LogIras(Funkcio.RaktárMegjelenítés, raktarLista[lsbButorRaktarak.SelectedIndex]);

            }
        }

        private void btnRaktarTorol_Click(object sender, EventArgs e)
        {
            if (lsbButorRaktarak.SelectedIndex != -1 && MessageBox.Show("Valóban törli a kijelölt raktárat?", "Raktár törlése", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK)
            {
                LogKezeles.LogIras(Funkcio.RaktárTörlés, (ButorRaktar)lsbButorRaktarak.SelectedItem);
                //LogKezeles.LogIras(Funkcio.RaktárTörlés, raktarLista[lsbButorRaktarak.SelectedIndex]);
                raktarLista.RemoveAt(lsbButorRaktarak.SelectedIndex);
                lsbButorRaktarak.DataSource = null;
                lsbButorRaktarak.DataSource = raktarLista;
                if (lsbButorRaktarak.Items.Count != 0)
                {
                    lsbButorRaktarak.SelectedIndex = 0;
                }
                GombokEngedelyezese();
            }
        }

        private void btnButorUj_Click(object sender, EventArgs e)
        {
            if (lsbButorRaktarak.SelectedIndex != -1)
            {
                if ((lsbButorRaktarak.SelectedItem as ButorRaktar).ButorokMaxSzama - (lsbButorRaktarak.SelectedItem as ButorRaktar).TaroltButorokSzama > 0)
                {
                    ButorKezelesFrm frm = new ButorKezelesFrm((ButorRaktar)lsbButorRaktarak.SelectedItem);
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        ButorListaFrissites();
                        LogKezeles.LogIras(Funkcio.BútorLétrehozás, (ButorRaktar)lsbButorRaktarak.SelectedItem, (lsbButorRaktarak.SelectedItem as ButorRaktar).Butorok.Last());
                    }
                }
                else
                {
                    MessageBox.Show("A kijelölt bútorraktárhoz nem lehet új bútort hozzáadni!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Kérem, jelöljön ki egy raktárat!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnButorModosit_Click(object sender, EventArgs e)
        {
            if (lsbButorRaktarak.SelectedIndex != -1)
            {
                int index = 0;
                if (lsbFabutorok.SelectedIndex != -1)
                {
                    while (index < (lsbButorRaktarak.SelectedItem as ButorRaktar).Butorok.Count && (lsbButorRaktarak.SelectedItem as ButorRaktar).Butorok[index].CikkSzam != (lsbFabutorok.SelectedItem as Fabutor).CikkSzam)
                    {
                        index++;
                    }
                }
                else if (lsbFembutorok.SelectedIndex != -1)
                {
                    while (index < (lsbButorRaktarak.SelectedItem as ButorRaktar).Butorok.Count && (lsbButorRaktarak.SelectedItem as ButorRaktar).Butorok[index].CikkSzam != (lsbFembutorok.SelectedItem as Fembutor).CikkSzam)
                    {
                        index++;
                    }
                }
                ButorKezelesFrm frm = new ButorKezelesFrm((ButorRaktar)lsbButorRaktarak.SelectedItem, index);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    ButorListaFrissites();
                    LogKezeles.LogIras(Funkcio.BútorMódosítás, (ButorRaktar)lsbButorRaktarak.SelectedItem, (lsbButorRaktarak.SelectedItem as ButorRaktar).Butorok[index]);
                }
            }
            else
            {
                MessageBox.Show("Kérem, jelöljön ki egy raktárat!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnButorMegjelenit_Click(object sender, EventArgs e)
        {
            if (lsbButorRaktarak.SelectedIndex != -1)
            {
                int index = 0;
                if (lsbFabutorok.SelectedIndex != -1)
                {
                    while (index < (lsbButorRaktarak.SelectedItem as ButorRaktar).Butorok.Count && (lsbButorRaktarak.SelectedItem as ButorRaktar).Butorok[index].CikkSzam != (lsbFabutorok.SelectedItem as Fabutor).CikkSzam)
                    {
                        index++;
                    }
                }
                else if (lsbFembutorok.SelectedIndex != -1)
                {
                    while (index < (lsbButorRaktarak.SelectedItem as ButorRaktar).Butorok.Count && (lsbButorRaktarak.SelectedItem as ButorRaktar).Butorok[index].CikkSzam != (lsbFembutorok.SelectedItem as Fembutor).CikkSzam)
                    {
                        index++;
                    }
                }
                ButorKezelesFrm frm = new ButorKezelesFrm((ButorRaktar)lsbButorRaktarak.SelectedItem, index, true);
                frm.ShowDialog();
                LogKezeles.LogIras(Funkcio.BútorMegjelenítés, (ButorRaktar)lsbButorRaktarak.SelectedItem, (lsbButorRaktarak.SelectedItem as ButorRaktar).Butorok[index]);
            }
            else
            {
                MessageBox.Show("Kérem, jelöljön ki egy raktárat!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnButorTorol_Click(object sender, EventArgs e)
        {
            if (lsbButorRaktarak.SelectedIndex != -1)
            {
                int index = 0;
                if (lsbFabutorok.SelectedIndex != -1 && MessageBox.Show("Biztosan törli a kijelölt fabútort?", "Törlés", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    while (index < (lsbButorRaktarak.SelectedItem as ButorRaktar).Butorok.Count && (lsbButorRaktarak.SelectedItem as ButorRaktar).Butorok[index].CikkSzam != (lsbFabutorok.SelectedItem as Fabutor).CikkSzam)
                    {
                        index++;
                    }
                    LogKezeles.LogIras(Funkcio.BútorTörlés, (ButorRaktar)lsbButorRaktarak.SelectedItem, (lsbButorRaktarak.SelectedItem as ButorRaktar).Butorok[index]);
                    raktarLista[lsbButorRaktarak.SelectedIndex].ButorTorles(index);
                    ButorListaFrissites();
                }
                else if (lsbFembutorok.SelectedIndex != -1 && MessageBox.Show("Biztosan törli a kijelölt fémbútort?", "Törlés", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    while (index < (lsbButorRaktarak.SelectedItem as ButorRaktar).Butorok.Count && (lsbButorRaktarak.SelectedItem as ButorRaktar).Butorok[index].CikkSzam != (lsbFembutorok.SelectedItem as Fembutor).CikkSzam)
                    {
                        index++;
                    }
                    LogKezeles.LogIras(Funkcio.BútorTörlés, (ButorRaktar)lsbButorRaktarak.SelectedItem, (lsbButorRaktarak.SelectedItem as ButorRaktar).Butorok[index]);
                    raktarLista[lsbButorRaktarak.SelectedIndex].ButorTorles(index);
                    ButorListaFrissites();
                }
            }
            else
            {
                MessageBox.Show("Kérem, jelöljön ki egy raktárat!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnRaktarakMegjelenit_Click(object sender, EventArgs e)
        {
            if (raktarLista.Count > 0)
            {
                MegjelenitesekFrm frm = new MegjelenitesekFrm(raktarLista);
                frm.ShowDialog();
                LogKezeles.LogIras(Funkcio.RaktárakMegjelenítése);
            }
        }

        private void btnButorokMegjelenit_Click(object sender, EventArgs e)
        {
            if (lsbButorRaktarak.SelectedIndex != -1)
            {
                MegjelenitesekFrm frm = new MegjelenitesekFrm(raktarLista, lsbButorRaktarak.SelectedIndex);
                frm.ShowDialog();
                LogKezeles.LogIras(Funkcio.BútorokMegjelenítése);
            }
        }

        private void btnRendezesek_Click(object sender, EventArgs e)
        {
            RendezesekFrm frm = new RendezesekFrm(raktarLista);
            frm.ShowDialog();
            LogKezeles.LogIras(Funkcio.Rendezések);
        }

        private void btnKilepes_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lsbButorRaktarak_SelectedIndexChanged(object sender, EventArgs e)
        {
            ButorListaFrissites();
            GombokEngedelyezese();
        }

        private void lsbFabutorok_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsbFabutorok.SelectedIndex != -1)
            {
                lblButorAdatok.Text = (lsbFabutorok.SelectedItem as Fabutor).ButorAdatok();
            }
            else
            {
                lblButorAdatok.Text = String.Empty;
            }
            GombokEngedelyezese();
        }

        private void lsbFembutorok_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsbFembutorok.SelectedIndex != -1)
            {
                lblButorAdatok.Text = (lsbFembutorok.SelectedItem as Fembutor).ButorAdatok();
            }
            else
            {
                lblButorAdatok.Text = String.Empty;
            }
            GombokEngedelyezese();
        }

        private void lsbFabutorok_MouseClick(object sender, MouseEventArgs e)
        {
            if (sender is ListBox lb)
            {
                foreach (Control item in this.Controls)
                {
                    if (item is ListBox && item != lb && item != lsbButorRaktarak)
                    {
                        (item as ListBox).SelectedIndex = -1;
                    }
                }
            }
        }

        private void névjegyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            NevjegyFrm frm = new NevjegyFrm();
            frm.ShowDialog();
        }

        private void lsbButorRaktarak_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lsbButorRaktarak.Items.Count != 0 && lsbButorRaktarak.SelectedIndex != -1)
            {
                MessageBox.Show((lsbButorRaktarak.SelectedItem as ButorRaktar).RaktarAdatok(), (lsbButorRaktarak.SelectedItem as ButorRaktar).RaktarSzam, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region Methods
        private void GombokEngedelyezese()
        {
            // Semmi nincs
            if (lsbButorRaktarak.SelectedIndex == -1)
            {
                foreach (Control item in grbButorRaktar.Controls)
                {
                    item.Enabled = (item.Name == "btnRaktarUj") ? true : false;
                }
                foreach (Control item in grbButor.Controls)
                {
                    item.Enabled = false;
                }
            }
            // Már van Bútorraktár és lehet, hogy van Bútor
            else
            {
                foreach (Control item in grbButorRaktar.Controls)
                {
                    item.Enabled = true;
                }
                if (lsbFabutorok.SelectedIndex == -1 && lsbFembutorok.SelectedIndex == -1)
                {
                    foreach (Control item in grbButor.Controls)
                    {
                        item.Enabled = (item.Name == "btnButorUj") ? true : false;
                    }
                }
                else
                {
                    foreach (Control item in grbButor.Controls)
                    {
                        item.Enabled = true;
                    }
                }
            }
        }

        private void ButorListaFrissites()
        {
            // Mindkét bútorlista listboxot kiürítjük.
            lsbFabutorok.DataSource = lsbFembutorok.DataSource = null;
            // A kijelölt bútorraktár listájával felöltjük
            List<Fabutor> faButorok = new List<Fabutor>();
            List<Fembutor> femButorok = new List<Fembutor>();
            if (lsbButorRaktarak.SelectedIndex != -1 && (lsbButorRaktarak.SelectedItem as ButorRaktar).Butorok.Count > 0)
            {
                foreach (AltalanosButor item in (lsbButorRaktarak.SelectedItem as ButorRaktar).Butorok)
                {
                    if (item is Fabutor)
                    {
                        faButorok.Add(item as Fabutor);
                    }
                    else
                    {
                        femButorok.Add(item as Fembutor);
                    }
                }
                lsbFabutorok.DataSource = faButorok;
                lsbFembutorok.DataSource = femButorok;
                // Mindkét listboxról elvesszük a fókuszt.
                lsbFabutorok.SelectedIndex = lsbFembutorok.SelectedIndex = -1;
                lblButorRaktarak.Text = $" Bútorraktárak - (Bútorraktárak ára összesen: {String.Format(CultureInfo.CurrentCulture, "{0:C0}", ButorRaktar.RaktarArOsszesen(raktarLista))})";
            }
        }
        #endregion

        
    }
}
