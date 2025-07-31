using Extensions;
using System;
using System.Threading;
using System.Windows.Forms;
using Tabla = lab03.MyCheckBoxPolje; // alias za lakse referenciranje [ime klase je takvo kakvo je da bi se znalo cemu sluzi klasa]

namespace lab03
{
    public partial class Form1 : Form {
        public static readonly Random r = new Random();
        System.Windows.Forms.Timer timer;
        Tabla tabla; Label label1;
        uint elapsed;
        int crd = -1;
        int poeni;

        int Poeni {
            get => poeni;
            set {
                poeni = value;
                this.label1.Text = "\t"+(value*300).ToString();
            }
        }

        void NovaIgra() {
            // TODO: Tabla mora da kreira uparene brojeve
            tabla = new Tabla(Program.data.W, Program.data.H, jeKarta: true);
            timer = new System.Windows.Forms.Timer() { Interval = 1000 };
            timer.Start();
        }

        void Kraj() {
            timer.Stop();
            timer.Dispose();
            timer = null;
        }

        public Form1() {
            InitializeComponent();
            this.Text = this.lab03ToolStripMenuItem.Text = "lab03";
            this.label1 = new Label();
            this.label1.Text = "";
            this.label1.Dock = DockStyle.Bottom;

            NovaIgra();

            timer.Tick += delegate { this.label1.Text = Stoperica.ToTime(++elapsed); };

            this.SuspendLayout();

            for (int i = 0; i < tabla.W; i++) {
                for (int j = 0; j < tabla.H; j++) {
                    tabla[i, j].Location = new System.Drawing.Point((int) (tabla.dimPolja*1.1*i)+35, (int) (tabla.dimPolja*1.1*j)+this.menuStrip1.Height+32); // +2 zbog razdvajanja od menustripa
                    tabla[i, j].Click += delegate(object sender, EventArgs e) {
                        MyCheckBox m = (MyCheckBox) sender;
                        if (((int) m.Tag) == 0) return;
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

            this.Width = (int) (Program.data.W * tabla.dimPolja * 1.1 + 80);
            this.Height = (int) (Program.data.H * tabla.dimPolja * 1.1 + this.menuStrip1.Height + 100);
            this.restartToolStripMenuItem.Click += delegate { NovaIgra(); }; // TODO: Otkloniti bag
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
