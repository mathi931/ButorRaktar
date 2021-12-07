using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProbaModulzaro
{
    public partial class RendezesekFrm : Form
    {
        RadioButton rdbValasztas1;
        RadioButton rdbValasztas2;
        RadioButton rdbValasztas3;
        RadioButton rdbValasztas4;
        ListBox lsbButorRaktarak;
        ListBox lsbFabutorok;
        ListBox lsbFembutorok;
        Button btnKilepes;
        List<ButorRaktar> raktarLista;
        List<AltalanosButor> butorLista;

        internal RendezesekFrm(List<ButorRaktar> raktarLista)
        {
            InitializeComponent();
            Komponensek();
            rdbValasztas1.CheckedChanged += RdbValasztas1_CheckedChanged;
            rdbValasztas2.CheckedChanged += RdbValasztas2_CheckedChanged;
            rdbValasztas3.CheckedChanged += RdbValasztas3_CheckedChanged;
            rdbValasztas4.CheckedChanged += RdbValasztas4_CheckedChanged;
            lsbButorRaktarak.SelectedIndexChanged += LsbButorRaktarak_SelectedIndexChanged;
            this.raktarLista = new List<ButorRaktar>();
            this.butorLista = new List<AltalanosButor>();
            foreach (ButorRaktar item in raktarLista)
            {
                this.raktarLista.Add(item);
                lsbButorRaktarak.Items.Add(item);
            }
            lsbButorRaktarak.SelectedIndex = 0;
        }

        private void RdbValasztas1_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbValasztas1.Checked)
            {
                ListaRendezesek(0);
                ListBoxFrissitesek();
            }
        }

        private void RdbValasztas2_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbValasztas2.Checked)
            {
                ListaRendezesek(1);
                ListBoxFrissitesek();
            }
        }

        private void RdbValasztas3_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbValasztas3.Checked)
            {
                ButorRaktar tmp = (ButorRaktar)lsbButorRaktarak.SelectedItem;
                LsbButorRaktarakFrissites(true);
                ListaRendezesek(2);
                ListBoxFrissitesek();
                lsbButorRaktarak.SelectedItem = tmp;
            }
        }

        private void RdbValasztas4_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbValasztas4.Checked)
            {
                ButorRaktar tmp = (ButorRaktar)lsbButorRaktarak.SelectedItem;
                LsbButorRaktarakFrissites(false);
                ListaRendezesek(3);
                ListBoxFrissitesek();
                lsbButorRaktarak.SelectedItem = tmp;
            }
        }

        private void LsbButorRaktarak_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdbValasztas1.Checked)
            {
                ListaRendezesek(0);
                ListBoxFrissitesek();
            }
            else if (rdbValasztas2.Checked)
            {
                ListaRendezesek(1);
                ListBoxFrissitesek();
            }
            else if (rdbValasztas3.Checked)
            {
                ListaRendezesek(2);
                ListBoxFrissitesek();
            }
            else if (rdbValasztas4.Checked)
            {
                ListaRendezesek(3);
                ListBoxFrissitesek();
            }
        }

        private void Komponensek()
        {
            rdbValasztas1 = new RadioButton()
            {
                Parent = this,
                Top = this.Top + 10,
                Left = this.Left + 10,
                Width = this.Width / 3,
                Checked = true,
                Text = "Rendezés A-tól Z-ig"
            };
            rdbValasztas2 = new RadioButton()
            {
                Parent = this,
                Top = rdbValasztas1.Bottom,
                Left = rdbValasztas1.Left,
                Width = rdbValasztas1.Width,
                Text = "Rendezés Z-től A-ig"
            };
            rdbValasztas3 = new RadioButton()
            {
                Parent = this,
                Top = rdbValasztas2.Bottom,
                Left = rdbValasztas1.Left,
                Width = rdbValasztas1.Width,
                Text = "Rendezés növekvő sorrendben"
            };
            rdbValasztas4 = new RadioButton()
            {
                Parent = this,
                Top = rdbValasztas3.Bottom,
                Left = rdbValasztas1.Left,
                Width = rdbValasztas1.Width,
                Text = "Rendezés csökkenő sorrendben"
            };
            lsbButorRaktarak = new ListBox()
            {
                Parent = this,
                Top = rdbValasztas4.Bottom,
                Left = rdbValasztas1.Left,
                Width = this.Width - 35,
                Height = 200

            };
            lsbFabutorok = new ListBox()
            {
                Parent = this,
                Top = lsbButorRaktarak.Bottom,
                Left = rdbValasztas1.Left,
                Width = this.Width - 35,
                Height = 200

            };
            lsbFembutorok = new ListBox()
            {
                Parent = this,
                Top = lsbFabutorok.Bottom,
                Left = rdbValasztas1.Left,
                Width = this.Width - 35,
                Height = 200

            };
            btnKilepes = new Button()
            {
                Parent = this,
                Top = lsbFembutorok.Bottom,
                Left = this.Width - 135,
                Width = 100,
                Height = 25,
                DialogResult = DialogResult.Cancel,
                Text = "Kilépés"
            };
            this.CancelButton = btnKilepes;
        }

        private void ListaRendezesek(int index)
        {
            switch (index)
            {
                // Rendezés A-tól Z-ig - Javított beillesztéses rendezés
                //i, j, segéd: egész
                //Ciklus i:= 2 - től n - ig
                //   j:= i - 1
                //   s:= t[i]
                //   Ciklus amíg j > 0 ÉS t[j] > s
                //      t[j + 1] := t[j]
                //      j:= j - 1
                //   CV.
                // t[j + 1] := s
                //CV.

                case 0:
                    butorLista.Clear();
                    if (lsbButorRaktarak.SelectedIndex != -1)
                    {
                        foreach (AltalanosButor item in (lsbButorRaktarak.SelectedItem as ButorRaktar).Butorok)
                        {
                            butorLista.Add(item);
                        }
                    }
                    AltalanosButor segedFo0;
                    int j0;
                    for (int i0 = 1; i0 < butorLista.Count; i0++)
                    {
                        j0 = i0 - 1;
                        segedFo0 = butorLista[i0];
                        while (j0 >= 0 && (butorLista[j0] as AltalanosButor).Megnevezes.CompareTo(segedFo0.Megnevezes) == 1)
                        {
                            AltalanosButor segedJ = butorLista[j0];
                            AltalanosButor segedJ1 = butorLista[j0 + 1];
                            butorLista.RemoveAt(j0);
                            butorLista.Insert(j0, segedJ1);
                            butorLista.RemoveAt(j0 + 1);
                            butorLista.Insert(j0 + 1, segedJ);
                            j0 = j0 - 1;
                        }
                        butorLista.RemoveAt(j0 + 1);
                        butorLista.Insert(j0 + 1, segedFo0);
                    }
                    break;
                // Rendezés Z-től A-ig - Javított buborékos rendezés

                //i, j, csere: egész
                //i := n
                //Ciklus amíg i >= 2
                //  csere:= 0
                //  Ciklus j := 1 - től i - 1 - ig
                //    Ha t[j] > t[j + 1]    akkor
                //      Csere(t[j], t[j + 1])
                //      csere:= j
                //    EV.
                //  CV.
                //  i := csere
                //CV.
                case 1:
                    butorLista.Clear();
                    if (lsbButorRaktarak.SelectedIndex != -1)
                    {
                        foreach (AltalanosButor item in (lsbButorRaktarak.SelectedItem as ButorRaktar).Butorok)
                        {
                            butorLista.Add(item);
                        }
                    }
                    int i1 = butorLista.Count - 1;
                    int csere1;
                    while (i1 > 0)
                    {
                        csere1 = 0;
                        for (int j1 = 0; j1 < i1; j1++)
                        {
                            if ((butorLista[j1] as AltalanosButor).Megnevezes.CompareTo((butorLista[j1 + 1] as AltalanosButor).Megnevezes) == -1)
                            {
                                AltalanosButor segedJ = butorLista[j1];
                                AltalanosButor segedJ1 = butorLista[j1 + 1];
                                butorLista.RemoveAt(j1);
                                butorLista.Insert(j1, segedJ1);
                                butorLista.RemoveAt(j1 + 1);
                                butorLista.Insert(j1 + 1, segedJ);
                                csere1 = j1;
                            }
                        }
                        i1 = csere1;
                    }
                    break;
                // Rendezés növekvő sorrendben - Javított beillesztéses rendezés
                case 2:
                    butorLista.Clear();
                    if (lsbButorRaktarak.SelectedIndex != -1)
                    {
                        foreach (AltalanosButor item in (lsbButorRaktarak.SelectedItem as ButorRaktar).Butorok)
                        {
                            butorLista.Add(item);
                        }
                    }
                    AltalanosButor segedFo2;
                    int j2;
                    for (int i2 = 1; i2 < butorLista.Count; i2++)
                    {
                        j2 = i2 - 1;
                        segedFo2 = butorLista[i2];
                        while (j2 >= 0 && (butorLista[j2] as AltalanosButor).ButorAra > segedFo2.ButorAra)
                        {
                            AltalanosButor segedJ = butorLista[j2];
                            AltalanosButor segedJ1 = butorLista[j2 + 1];
                            butorLista.RemoveAt(j2);
                            butorLista.Insert(j2, segedJ1);
                            butorLista.RemoveAt(j2 + 1);
                            butorLista.Insert(j2 + 1, segedJ);
                            j2 = j2 - 1;
                        }
                        butorLista.RemoveAt(j2 + 1);
                        butorLista.Insert(j2 + 1, segedFo2);
                    }
                    break;
                // Rendezés csökkenő sorrendben - Javított buborékos rendezés
                case 3:
                    butorLista.Clear();
                    if (lsbButorRaktarak.SelectedIndex != -1)
                    {
                        foreach (AltalanosButor item in (lsbButorRaktarak.SelectedItem as ButorRaktar).Butorok)
                        {
                            butorLista.Add(item);
                        }
                    }
                    int i3 = butorLista.Count - 1;
                    int csere3;
                    while (i3 > 0)
                    {
                        csere3 = 0;
                        for (int j3 = 0; j3 < i3; j3++)
                        {
                            if ((butorLista[j3] as AltalanosButor).ButorAra < (butorLista[j3 + 1] as AltalanosButor).ButorAra)
                            {
                                AltalanosButor segedJ = butorLista[j3];
                                AltalanosButor segedJ1 = butorLista[j3 + 1];
                                butorLista.RemoveAt(j3);
                                butorLista.Insert(j3, segedJ1);
                                butorLista.RemoveAt(j3 + 1);
                                butorLista.Insert(j3 + 1, segedJ);
                                csere3 = j3;
                            }
                        }
                        i3 = csere3;
                    }
                    break;

            }
        }

        private void ListBoxFrissitesek()
        {
            lsbFabutorok.Items.Clear();
            lsbFembutorok.Items.Clear();
            foreach (AltalanosButor item in butorLista)
            {
                if (item is Fabutor)
                {
                    lsbFabutorok.Items.Add(item as Fabutor);
                }
                else
                {
                    lsbFembutorok.Items.Add(item as Fembutor);
                }
            }
        }

        private void LsbButorRaktarakFrissites(bool novekvo)
        {

            if (novekvo)
            {
                // Rendezés növekvő sorrendben - Javított beillesztéses rendezés (Az árra vonatkozik mindhárom ListBoxban)
                ButorRaktar segedFo2;
                int j2;
                for (int i2 = 1; i2 < raktarLista.Count; i2++)
                {
                    j2 = i2 - 1;
                    segedFo2 = raktarLista[i2];
                    while (j2 >= 0 && (raktarLista[j2] as ButorRaktar).TaroltButorokAra > segedFo2.TaroltButorokAra)
                    {
                        ButorRaktar segedJ = raktarLista[j2];
                        ButorRaktar segedJ1 = raktarLista[j2 + 1];
                        raktarLista.RemoveAt(j2);
                        raktarLista.Insert(j2, segedJ1);
                        raktarLista.RemoveAt(j2 + 1);
                        raktarLista.Insert(j2 + 1, segedJ);
                        j2 = j2 - 1;
                    }
                    raktarLista.RemoveAt(j2 + 1);
                    raktarLista.Insert(j2 + 1, segedFo2);
                }
            }
            else
            {
                // Rendezés csökkenő sorrendben - Javított buborékos rendezés (Az árra vonatkozik mindhárom ListBoxban)
                int i3 = raktarLista.Count - 1;
                int csere3;
                while (i3 > 0)
                {
                    csere3 = 0;
                    for (int j3 = 0; j3 < i3; j3++)
                    {
                        if ((raktarLista[j3] as ButorRaktar).TaroltButorokAra < (raktarLista[j3 + 1] as ButorRaktar).TaroltButorokAra)
                        {
                            ButorRaktar segedJ = raktarLista[j3];
                            ButorRaktar segedJ1 = raktarLista[j3 + 1];
                            raktarLista.RemoveAt(j3);
                            raktarLista.Insert(j3, segedJ1);
                            raktarLista.RemoveAt(j3 + 1);
                            raktarLista.Insert(j3 + 1, segedJ);
                            csere3 = j3;
                        }
                    }
                    i3 = csere3;
                }
            }
            lsbButorRaktarak.Items.Clear();
            foreach (ButorRaktar item in raktarLista)
            {
                lsbButorRaktarak.Items.Add(item);
            }
        }
    }
}
