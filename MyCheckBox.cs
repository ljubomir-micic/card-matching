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

        int cast() { string a = broj.ToString(); if (a.Length == 1) a = "0"+a; return ((a[0] - '0')<<4) + (a[1]-'0'); }
        int cast(int broj) { return (broj>>4)*10 + (broj&0xF); }
        public int VALUE {
            get {
                return ((((((((jeKarta ? 1 : 0) << 1) + (Pogodak ? 1 : 0)) << 4) + X) << 4) + Y) << 8) + cast();
            } set {
                broj = cast(value & 0xFF);
                Tag = broj;
                value >>= 8;
                Y = value & 0xF;
                X = (value >> 4)&0xF;
                Checked = Pogodak = ((value & 0b100000000)>>8) == 1;
                jeKarta = (value>>9) == 1;
            }
        }
        public bool jeKarta { get; set; }
        public bool Pogodak { get; set; }
        public int Dim { get; set; } = 45;
        public int X { get; set; } = 0;
        public int Y { get; set; } = 0;
        public new int Width { get => Dim; }
        public new int Height { get => Dim; }
        public static Permissions Permissions { get; set; } = Permissions.User;
        public new Image Image { get { try { return Image.FromFile(System.IO.Path.Combine(Program.data.resrcPath, this.broj.ToString() + ".png")); } catch { return null; } } }
        
        public Image BackImage {
            get {
                Image i = (Image) lab03.Properties.Resources.back;
                return i;
            }
        }

        public MyCheckBox() { base.Width = base.Height = this.Dim = 45; }

        public MyCheckBox(int broj, int dim = 45, bool jeKarta = false) {
            this.Tag = this.broj = broj;
            this.jeKarta = jeKarta;
            base.Width = base.Height = this.Dim = dim;
            this.Checked = this.Pogodak = false;
            this.X = this.Y = 0;
            this.Dim = 45;
        }

        protected override void OnCheckedChanged(EventArgs e)
        {
            if (Permissions == Permissions.User && !Checked && jeKarta) this.Checked = true;
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
