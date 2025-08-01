using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab03
{
    public partial class Ende : Form
    {
        GraphicsPath zvezda(Rectangle rect) {
            float r = Math.Min(rect.Size.Width/2, rect.Size.Height/2);
            PointF[] tacke = new PointF[10];
            double ugao = Math.PI/2;
            for (int i = 0; i < tacke.Length; i++) {
                tacke[i] = new PointF((float) Math.Cos(ugao)*(i%2==1?r:r/2) + rect.Size.Width/2+rect.X, (float)Math.Sin(ugao)*(i%2==1?r:r/2) + rect.Size.Height/2+rect.Y);
                ugao = (ugao + Math.PI/5) % (2*Math.PI);
            }
            GraphicsPath p = new GraphicsPath();
            p.AddLines(tacke);
            p.CloseFigure();
            return p;
        }

        public Ende(string time)
        {
            InitializeComponent();
            label2.Text += time + ".";
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using (var brush = new SolidBrush(Color.Gold))
                using (var path = zvezda(new Rectangle(40, 20, 50, 50)))
                    e.Graphics.FillPath(brush, path);
        }
    }
}
