using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Matrica = lab03.MyCheckBoxPolje;

namespace lab03
{
    // TODO: Dodaj textfield za broj elemenata i slika
    public partial class Konfiguracija : Form {
        Matrica matrica; Label dimMat; TextBox rscLok; Button save;
        TextBox[] textBoxes;

        int Duzina { get => Program.data.W; set {
                Program.data.W = value;
                dimMat.Text = value + " x " + Sirina;
            }
        }

        int Sirina { get => Program.data.H; set {
                Program.data.H = value;
                dimMat.Text = Duzina + " x " + value; 
            }
        }

        int BrojParova { get => Program.data.P; set {
                Program.data.P = value;
            }
        }

        int BrojStranica { get => Program.data.S; set {
                Program.data.S = value;
            }
        }

        public Konfiguracija()
        {
            InitializeComponent();

            matrica = new Matrica(10, 10, size: 25);
            matrica.Click += delegate {
                int x = matrica.LastCheck >> 4, y = matrica.LastCheck & 0xF;
                if (x < 5) x = 5;
                if (y < 4) y = 4;
                for (int i = 0; i < matrica.W; i++) {
                    for (int j = 0; j < matrica.H; j++) {
                        matrica[i, j].Checked = (j <= y && i <= x);
                        Duzina = x + 1;
                        Sirina = y + 1;
                        matrica.Invalidate();
                    }
                }
            };
            rscLok = new TextBox() { Text = Program.data.resrcPath, Size = new Size(250, 0), Location = new Point(20, 20) };
            textBoxes = new TextBox[2];
            textBoxes[0] = new TextBox() { Text = Program.data.P.ToString(), Location = new Point(20, Height - 60 - 10) };
            textBoxes[1] = new TextBox() { Text = Program.data.S.ToString(), Location = new Point(Width - 40 - textBoxes[0].Width, Height - 60 - 10) };
            for (int tb = 0; tb < 2; tb++) textBoxes[tb].KeyPress += (sender, e) => { if (!Char.IsDigit(e.KeyChar) || !Char.IsControl(e.KeyChar)) e.Handled = true; };
            for (int i = 0; i < matrica.W; i++) {
                for (int j = 0; j < matrica.H; j++) {
                    matrica[i, j].Checked = (i < Duzina && j < Sirina);
                    matrica[i, j].Location = new System.Drawing.Point((int) (matrica.dimPolja*0.7*i)+25, (int) (matrica.dimPolja*0.7*j)+45);
                    this.Controls.Add(matrica[i, j]);
                }
            }
            dimMat = new Label() { Text = Duzina + " x " + Sirina, Location = new Point(20, Height-80-10) };
            save = new Button() { Text = "Sacuvaj", Location = new Point(rscLok.Right + 5, rscLok.Top), Width = 65 };
            save.Click += delegate { Program.data.resrcPath = rscLok.Text; Program.data.W = Duzina; Program.data.H = Sirina; Program.data.P = int.Parse(textBoxes[0].Text); Program.data.S = int.Parse(textBoxes[1].Text); Program.data.Save(); };
            this.Controls.Add(rscLok);
            this.Controls.Add(save);
            this.Controls.Add(dimMat);
            this.Controls.Add(textBoxes[0]);
            this.Controls.Add(textBoxes[1]);
        }
    }
}
