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
    public partial class Konfiguracija : Form
    {
        Matrica matrica; Label dimMat; TextBox rscLok; Button save;
        int duz, sir;

        int Duzina { get => 6; set {
                duz = value;
                dimMat.Text = value + " x " + Sirina;
            }
        }
        int Sirina { get => 5; set {
                sir = value;
                dimMat.Text = Duzina + " x " + value; 
            }
        }

        public Konfiguracija()
        {
            InitializeComponent();

            matrica = new Matrica(10, 10, size: 25);
            rscLok = new TextBox() { Text = Program.data.resrcPath, Size = new Size(250, 0), Location = new Point((Width - 250)>>1, 20) };
            for (int i = 0; i < matrica.W; i++) {
                for (int j = 0; j < matrica.H; j++) {
                    matrica[i, j].Location = new System.Drawing.Point((int) (matrica.dimPolja*1.2*i)+25, (int) (matrica.dimPolja*1.2*j)+((Width-matrica.dimPolja*matrica.H)>>1));
                    matrica[i, j].MouseEnter += delegate { Duzina = i+1; Sirina = j+1; };
                    matrica[i, j].MouseLeave += delegate { Duzina = Program.data.W; Sirina = Program.data.H; };
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
