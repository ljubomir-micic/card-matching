using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tabla = lab03.MyCheckBoxPolje; // alias za lakse referenciranje [ime klase je takvo kakvo je da bi se znalo cemu sluzi klasa]

namespace lab03
{
    public partial class Form1 : Form {
        Tabla tabla;
        public static Random r;

        public Form1() {
            InitializeComponent();
            this.Text = this.lab03ToolStripMenuItem.Text = "lab03";

            //if (!System.IO.File.Exists(DataManager.filePath))
                Program.data.Save();

            r = new Random();
            tabla = new Tabla();

            this.SuspendLayout();

            Dictionary<int, byte> v = new Dictionary<int, byte>(); // za vrednosti v[broj] mora biti paran
            for (int i = 0; i < tabla.W; i++) {
                for (int j = 0; j < tabla.H; j++) {
                    tabla[i, j].Location = new System.Drawing.Point((int) (tabla.dimPolja*1.1*i), (int) (tabla.dimPolja*1.1*j)+this.menuStrip1.Height+2); // +2 zbog razdvajanja od menustripa

                    this.Controls.Add(tabla[i, j]);
                }
            }

            this.Width = (int) (DataManager.W*tabla.dimPolja*1.15);
            this.Height = (int) (DataManager.H*tabla.dimPolja*1.15 + this.menuStrip1.Height+30);
            this.konfiguracijaToolStripMenuItem.Click += new EventHandler(adminPodesavanja);
            this.aboutToolStripMenuItem.Click += delegate { new AboutForm().Show(); };
            this.ResumeLayout(false);
        }

        protected void adminPodesavanja(object sender, EventArgs e) {

        }
    }
}
