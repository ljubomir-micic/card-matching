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
    public class MyCheckBox : CheckBox {
        int broj;
        Color boja;

        bool jeKarta { get; set; }
        bool Vidljivost { get; set; }
        public int Dim { get; set; } = 45;
        public int X { get; set; } = 0;
        public int Y { get; set; } = 0;
        public Image Image { get { try { return Image.FromFile(System.IO.Path.Combine(Program.data.resrcPath, this.broj.ToString() + ".png")); } catch { return null; } } set { } }
        public Image BackImage {
            get {
                Image i = (Image) lab03.Properties.Resources.back;
                return i;
            }
        }

        public MyCheckBox(int broj, Image im = null, int dim = 45, bool jeKarta = false) {
            Tag = this.broj = broj;
            this.jeKarta = jeKarta;
            Width = Height = Dim = dim;
            Vidljivost = false;
            Image = im;
        }

        protected override void OnCheckedChanged(EventArgs e)
        {
            if (broj == 0) return;
            Vidljivost = Checked;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e) {
            //base.OnPaint(pevent);
            if (!Vidljivost && jeKarta)
                e.Graphics.DrawImage(BackImage, 0, 0, Width, Height);
            else {
                if (this.broj == 0) {
                    e.Graphics.Clear(Color.Gold);
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
