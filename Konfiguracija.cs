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
    // DONE: Dodaj textfield za broj elemenata i slika
    public partial class Konfiguracija : Form {
        Matrica matrica; Label dimMat; TextBox rscLok; Button save;
        TextBox[] textBoxes; Label[] labels;

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
        int BrojParova { get => Program.data.P; set => Program.data.P = value; }
        int BrojStranica { get => Program.data.S; set => Program.data.S = value; }

        public Konfiguracija(int dim)
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
            textBoxes = new TextBox[3];  labels = new Label[3];
            for (int i = 0; i < 3; i++) { textBoxes[i] = new TextBox() { Location = new Point(15 + 110*i, Height - 60 - 10) }; labels[i] = new Label() { Location = new Point(15 + 110*i, Height - 75 - 10) }; }
            textBoxes[0].Text = Program.data.P.ToString();
            textBoxes[0].Leave += delegate { int max = (Duzina * Sirina) >> 1; if (int.Parse(textBoxes[0].Text) > max) textBoxes[0].Text = max.ToString(); if (int.Parse(textBoxes[0].Text) < 7) textBoxes[0].Text = "7"; };
            textBoxes[1].Text = Program.data.S.ToString();
            textBoxes[1].Leave += delegate { if (int.Parse(textBoxes[1].Text) < 5) textBoxes[1].Text = "5"; };
            textBoxes[2].Text = dim.ToString();
            textBoxes[2].Leave += delegate { if (int.Parse(textBoxes[2].Text) < 45) textBoxes[2].Text = "45"; };
            labels[0].Text = "Broj parova";
            labels[1].Text = "Broj slika";
            labels[2].Text = "Dimenzije polja";
            for (int tb = 0; tb < 3; tb++) textBoxes[tb].KeyPress += delegate(object sender, KeyPressEventArgs e) { if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar)) e.Handled = true; };
            for (int i = 0; i < matrica.W; i++) {
                for (int j = 0; j < matrica.H; j++) {
                    matrica[i, j].Checked = (i < Duzina && j < Sirina);
                    matrica[i, j].Location = new System.Drawing.Point((int) (matrica.D*1.2*i)+25, (int) (matrica.D*1.2*j)+50);
                    this.Controls.Add(matrica[i, j]);
                }
            }
            dimMat = new Label() { Text = Duzina + " x " + Sirina, Location = new Point(20, Height-80-20) };
            save = new Button() { Text = "Sacuvaj", Location = new Point(rscLok.Right + 5, rscLok.Top), Width = 65 };
            save.Click += delegate { Program.data.resrcPath = rscLok.Text; Program.data.W = Duzina; Program.data.H = Sirina; Program.data.P = int.Parse(textBoxes[0].Text); Program.data.S = int.Parse(textBoxes[1].Text); Program.data.D = int.Parse(textBoxes[2].Text); Program.data.Save(); this.Close(); };
            this.Controls.Add(rscLok);
            this.Controls.Add(save);
            this.Controls.Add(textBoxes[0]);
            this.Controls.Add(textBoxes[1]);
            this.Controls.Add(textBoxes[2]);
            this.Controls.Add(labels[0]);
            this.Controls.Add(dimMat);
            this.Controls.Add(labels[1]);
            this.Controls.Add(labels[2]);
        }
    }
}
