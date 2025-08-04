using Extensions;
using Microsoft.Win32;
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Tabla = lab03.MyCheckBoxPolje; // alias za lakse referenciranje [ime klase je takvo kakvo je da bi se znalo cemu sluzi klasa]

namespace lab03 {
    public partial class Form1 : Form {
        public static readonly Random r = new Random();
        System.Windows.Forms.Timer timer;
        Tabla tabla; Label label1;
        int crd = -1;

        uint elapsed { get => tabla.Elapsed; set => tabla.Elapsed = value; }
        int Poeni { get => tabla.BrPog; set => tabla.BrPog = value; }

        void NovaIgra(Tabla t = null) {
            // DONE: Tabla mora da kreira uparene brojeve
            this.SuspendLayout();
            if (timer != null)
                Kraj(this, new EventArgs());
            
            if (tabla != null)
                for (int i = 0; i < tabla.W; i++)
                    for (int j = 0; j < tabla.H; j++)
                        tabla[i, j].Dispose();
            
            this.predajaToolStripMenuItem.Text = "Predaja";
            this.predajaToolStripMenuItem.BackColor = System.Drawing.Color.IndianRed;
            this.predajaToolStripMenuItem.ForeColor = System.Drawing.Color.White;

            tabla = t ?? new Tabla(Program.data.W, Program.data.H, Program.data.D, jeKarta: true);
            tabla.Ende += new EventHandler(Kraj);

            if (tabla == null)
                this.elapsed = 0;
            
            timer = new System.Windows.Forms.Timer() { Interval = 1000 };
            timer.Tick += delegate { this.label1.Text = Stoperica.ToTime(++elapsed); };
            
            for (int i = 0; i < tabla.W; i++) {
                for (int j = 0; j < tabla.H; j++) {
                    tabla[i, j].Location = new System.Drawing.Point((int) (tabla.D*1.1*i)+35, (int) (tabla.D*1.1*j)+this.menuStrip1.Height+32); // +2 zbog razdvajanja od menustripa
                    tabla[i, j].Click += delegate(object sender, EventArgs e) {
                        MyCheckBox m = (MyCheckBox) sender;
                        if (((int) m.Tag) == 0) return;
                        if (crd!=-1) {
                            if ((crd >> 4) == m.X && (crd & 0xF) == m.Y) { return; }
                            if (tabla[crd >> 4, crd & 0xF].Pogodak || tabla[m.X, m.Y].Pogodak) return;
                            
                            if ((int) (tabla[crd >> 4, crd & 0xF].Tag) == (int)(tabla[m.X, m.Y].Tag)) {
                                tabla[m.X, m.Y].Pogodak = tabla[crd >> 4, crd & 0xF].Pogodak = true;
                                Poeni+=2;
                            } else {
                                Thread.Sleep(500);
                                MyCheckBox.Permissions = DataModels.Permissions.System;
                                tabla[m.X, m.Y].Checked = tabla[crd >> 4, crd & 0xF].Checked = false;
                                MyCheckBox.Permissions = DataModels.Permissions.User;
                            } 
                        
                            crd = -1;
                            return;
                        }
                        // RESENO: help bukv ako se klikne prvo ovo a otkriveno je ima da se postavi na crd -> problem je sto ne mogu da skontam da li je checked promenjeno sad ili pre
                        if (!m.Pogodak)
                            crd = ((m.X<<4)+m.Y); // najvise 9 sto pokriva 4 bita
                    };
                    this.Controls.Add(tabla[i, j]);
                }
            }
            
            this.Width = (int) ((t == null ? Program.data.W : t.W) * tabla.D * 1.1 + 80);
            this.Height = (int) ((t == null ? Program.data.H : t.H) * tabla.D * 1.1 + this.menuStrip1.Height + 100);
            this.ResumeLayout(false);
            this.timer.Start();
        }

        void Kraj(object sender, EventArgs e) {
            this.predajaToolStripMenuItem.Text = "Nova igra";
            this.predajaToolStripMenuItem.BackColor = SystemColors.Control;
            this.predajaToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            timer.Stop();
            timer.Dispose();
            if (sender is Tabla)
            new Ende(label1.Text).Show(); // HWND nije sacuvan ali svakako ostaje dok se ne ugasi prozor, nakon toga ce automatski da se zatvori i
                                          // dispozuje tako da ne bi trebalo da postoji ikakav mem leak iako moram da priznam ovako nikad nisam radio
            timer = null;
        }

        public Form1() {
            InitializeComponent();
            this.Text = this.lab03ToolStripMenuItem.Text = "lab03";
            this.label1 = new Label();
            this.label1.Text = "";
            this.label1.Dock = DockStyle.Bottom;
            this.label1.Padding = new Padding(10, 0, 0, 0);

            NovaIgra();

            this.restartToolStripMenuItem.Click += delegate { NovaIgra(); };
            this.konfiguracijaToolStripMenuItem.Click += delegate {
                Konfiguracija k = new Konfiguracija(tabla.D);
                if (timer != null) timer.Stop();
                k.ShowDialog();
                if (timer != null) timer.Start();
            };
            this.aboutToolStripMenuItem.Click += delegate { new AboutForm().Show(); };
            this.Controls.Add(label1);
        }

        // TODO: Ne isprobavati - radioaktivno
        private void ucitajToolStripMenuItem_Click(object sender, EventArgs e) {
            if (timer != null) timer.Stop();
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() != DialogResult.Cancel) {
                NovaIgra(Tabla.Load(ofd.FileName));
                return;
            }
            if (timer != null) timer.Start();
        }

        private void sacuvajToolStripMenuItem_Click(object sender, EventArgs e) {
            tabla.Save();
        }

        private void predajaToolStripMenuItem_Click(object sender, EventArgs e) {
            ToolStripMenuItem mi = (ToolStripMenuItem) sender;
            if (timer == null) {
                NovaIgra();
                return;
            }
            
            for (int i = 0; i < tabla.W; i++)
                for (int j = 0; j < tabla.H; j++) {
                    MyCheckBox.Permissions = DataModels.Permissions.System;
                    tabla[i, j].Checked = true;
                    MyCheckBox.Permissions = DataModels.Permissions.User;
                }
            
            Kraj(sender, e);
        }
    }
}
