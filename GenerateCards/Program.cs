using System;
using System.Drawing;
using System.Windows.Forms;

namespace GenerateCards {
    internal static class Program {
        private static Bitmap SlikaSBojom(int broj, int w, int h) {
            Bitmap b = new Bitmap(w, h);
            Random r = new Random();
            Color color = Color.FromArgb(r.Next(256), r.Next(256), r.Next(256));
            Graphics g = Graphics.FromImage(b);
            if (broj%2 == 0)
                g.FillRectangle(new SolidBrush(color), w/3, h/3, w/3, h/3);
            else
                g.FillEllipse(new SolidBrush(color), w/3, h/3, w/3, h/3);
            return b;
        }

        
        [STAThread]
        static void Main() {
            if (!System.IO.Directory.Exists(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "GenerateCards_out")))
                System.IO.Directory.CreateDirectory(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "GenerateCards_out"));
            
            for (int i = 1; i < 18; i++)
                SlikaSBojom(i, 512, 512).Save(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "GenerateCards_out", i+".png"));
        }
    }
}
