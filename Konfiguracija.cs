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
                        matrica[i, j].Vidljivost = (j <= y && i <= x);
                        Duzina = x + 1;
                        Sirina = y + 1;
                        matrica.Invalidate();
                    }
                }
            };
            rscLok = new TextBox() { Text = Program.data.resrcPath, Size = new Size(250, 0), Location = new Point((Width - 250)>>1, 20) };
            for (int i = 0; i < matrica.W; i++) {
                for (int j = 0; j < matrica.H; j++) {
                    matrica[i, j].Location = new System.Drawing.Point((int) (matrica.dimPolja*1.2*i)+25, (int) (matrica.dimPolja*1.2*j)+((Width-matrica.dimPolja*matrica.H)>>1));
                    this.Controls.Add(matrica[i, j]);
                }
            }
            dimMat = new Label() { Text = "0 x 0", Location = new Point(20, Height-10) };
            save = new Button() { Text = "Sacuvaj" };
            save.Click += delegate { Program.data.resrcPath = rscLok.Text; Program.data.W = Duzina; Program.data.H = Sirina; Program.data.Save(); };
            this.Controls.Add(rscLok);
            this.Controls.Add(save);
            this.Controls.Add(dimMat);
        }
    }
}
