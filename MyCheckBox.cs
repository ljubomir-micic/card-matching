using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab03 {
    public class MyCheckBox : CheckBox {
        int broj;
        Color boja;

        bool Vidljivost { get; set; }
        public static int Dim { get => 45; }
        public Image Image { get; set; }

        public MyCheckBox(int broj) {
            this.broj = broj;
            Vidljivost = false;
            Width = Height = Dim;
            boja = Color.FromArgb(Form1.r.Next(0, 256), Form1.r.Next(0, 256), Form1.r.Next(0, 256));
        }

        protected override void OnCheckedChanged(EventArgs e)
        {
            if (broj == 0) return;
            Vidljivost = Checked;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e) {
            //base.OnPaint(pevent);
            GraphicsPath path = GenerisanjeSlike.VratiSliku(broj);
            e.Graphics.Clear(path != null || !Vidljivost ? Color.White : Color.Gold);
            if (path == null) return;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.FillPath(new SolidBrush(boja),path);
        }
    }
}
