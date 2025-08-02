using DataModels;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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
        public bool Pogodak { get; set; }
        public int Dim { get; set; } = 45;
        public int X { get; set; } = 0;
        public int Y { get; set; } = 0;
        public static Permissions Permissions { get; set; } = Permissions.User;
        public new Image Image { get { try { return Image.FromFile(System.IO.Path.Combine(Program.data.resrcPath, this.broj.ToString() + ".png")); } catch { return null; } } }
        public Image BackImage {
            get {
                Image i = (Image) lab03.Properties.Resources.back;
                return i;
            }
        }

        public MyCheckBox(int broj, int dim = 45, bool jeKarta = false) {
            Tag = this.broj = broj;
            this.jeKarta = jeKarta;
            base.Width = base.Height = Dim = dim;
            Checked = Pogodak = false;
            X = Y = 0;
            Dim = 45;
        }
        
        public new int Width { get => Dim; }
        public new int Height { get => Dim; }

        protected override void OnCheckedChanged(EventArgs e)
        {
            if (Permissions == Permissions.User && !Checked && jeKarta) Checked = true;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e) {
            //base.OnPaint(pevent);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            if (!Checked && jeKarta)
                e.Graphics.DrawImage(BackImage, 0, 0, Width, Height);
            else {
                if (this.broj == 0) {
                    e.Graphics.Clear(!Checked ? Color.White : Color.Gold);
                } else {
                    e.Graphics.Clear(Color.White);
                    if (Image != null)
                        e.Graphics.DrawImage(Image, 0, 0, Width, Height);
                }
            }
            e.Graphics.DrawRectangle(new Pen(Color.FromArgb(0, 50, 30)), new Rectangle(0,0,Width-1,Height-1));
            // napravio bih i zaobljeni pravougaonik sa 4 ARC na ivicama ali zajsta nemam vremena za trosenje
        }
    }
}
