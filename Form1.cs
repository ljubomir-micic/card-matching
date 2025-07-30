using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tabla = lab03.MyCheckBoxPolje; // alias za lakse referenciranje [ime klase je takvo kakvo je da bi se znalo cemu sluzi klasa]

namespace lab03
{
    public partial class Form1 : Form {
        public static readonly Random r = new Random();
        Tabla tabla; Label label1;
        int crd = -1;
        int poeni;

        int Poeni { get => poeni; set
            {
                poeni = value;
                this.label1.Text = "\t"+(value*300).ToString();
            } }

        public Form1() {
            InitializeComponent();
            this.Text = this.lab03ToolStripMenuItem.Text = "lab03";
            this.label1 = new Label();
            this.label1.Text = "\t0";
            this.label1.Dock = DockStyle.Bottom;

            tabla = new Tabla(Program.data.W, Program.data.H, jeKarta: true);

            this.SuspendLayout();

            Dictionary<int, byte> v = new Dictionary<int, byte>(); // za vrednosti v[broj] mora biti paran
            for (int i = 0; i < tabla.W; i++) {
                for (int j = 0; j < tabla.H; j++) {
                    tabla[i, j].Location = new System.Drawing.Point((int) (tabla.dimPolja*1.1*i)+35, (int) (tabla.dimPolja*1.1*j)+this.menuStrip1.Height+32); // +2 zbog razdvajanja od menustripa
                    tabla[i, j].Click += delegate(object sender, EventArgs e) {
                        MyCheckBox m = (MyCheckBox) sender;
                        if (crd!=-1) {
                            if ((int) (tabla[crd >> 4, crd & 0xF].Tag) == (int)(tabla[m.X, m.Y].Tag) && ((crd >> 4) != m.X || (crd & 0xF) != m.Y))
                                Poeni++;
                            else {
                                Thread.Sleep(500);
                                tabla[m.X, m.Y].Checked = false;
                                tabla[crd >> 4, crd & 7].Checked = false;
                            } 
                        
                            crd = -1;
                            return;
                        }
                        crd = ((m.X<<4)+m.Y); // najvise 9 sto pokriva 4 bita
                    };
                    this.Controls.Add(tabla[i, j]);
                }
            }

            this.Width = (int) (Program.data.W*tabla.dimPolja*1.4);
            this.Height = (int) (Program.data.H*tabla.dimPolja*1.4 + this.menuStrip1.Height+30);
            this.konfiguracijaToolStripMenuItem.Click += delegate {
                Konfiguracija k = new Konfiguracija();
                k.ShowDialog();
            };
            this.aboutToolStripMenuItem.Click += delegate { new AboutForm().Show(); };
            this.Controls.Add(label1);
            this.ResumeLayout(false);
        }
    }
}
