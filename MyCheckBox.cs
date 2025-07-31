using DataModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab03 {
    // Ovo jeste klasa koja sadrzi podatke, ali je ujedno i klasa koja nasledjuje Control iz namespace-a System.Windows.Forms, te je nemoguce izdvojiti je u poseban projekat
    public class MyCheckBox : CheckBox {
        int broj;

        public bool jeKarta { get; set; }
        public bool Vidljivost { get; set; }
        public int Dim { get; set; } = 45;
        public int X { get; set; } = 0;
        public int Y { get; set; } = 0;
        public new Image Image { get { try { return Image.FromFile(System.IO.Path.Combine(Program.data.resrcPath, this.broj.ToString() + ".png")); } catch { return null; } } }
        public Image BackImage {
            get {
                Image i = (Image) lab03.Properties.Resources.back;
                return i;
            }
        }

        public MyCheckBox(int broj = 0, int dim = 45, bool jeKarta = false) {
            Tag = this.broj = broj;
            this.jeKarta = jeKarta;
            Width = Height = Dim = dim;
            Vidljivost = false;
        }

        protected override void OnCheckedChanged(EventArgs e)
        {
            if (broj == 0 && Vidljivost) return; // TODO: ovde nesto logika ne valja -> ako se obicno polje duplo pritisne moze da se odcekira sto ne bi smelo
                                                 //       Sa druge strane program sam ponekad vrsi odcekiranje pa treba omoguciti i to
            Vidljivost = Checked;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e) {
            //base.OnPaint(pevent);
            if (!Vidljivost && jeKarta)
                e.Graphics.DrawImage(BackImage, 0, 0, Width, Height);
            else {
                if (this.broj == 0) {
                    e.Graphics.Clear(!Vidljivost ? Color.White : Color.Gold);
                } else {
                    e.Graphics.Clear(Color.White);
                    if (Image != null)
                        e.Graphics.DrawImage(Image, 0, 0, Width, Height);
                }
            }

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        }
    }
}
