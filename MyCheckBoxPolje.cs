using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab03 {
    public class MyCheckBoxPolje {
        MyCheckBox[,] polje;

        public int W { get; set; }
        public int H { get; set; }
        public int dimPolja { get => polje == null ? 45 : polje[0, 0].Dim; }

        // *U pokusaju da se naucim lepo da projektujem klase cak i u C# po Single Responsibility principu pored navike za spaghetti code*
        public MyCheckBoxPolje(int x, int y, int size = 45, bool jeKarta = false) {
            W = x;
            H = y;

            polje = new MyCheckBox[W, H];
            for (int i = 0; i < W; i++)
                for (int j = 0; j < H; j++)
                    polje[i, j] = new MyCheckBox(!jeKarta ? 0 : Form1.r.Next(0, 18), dim: size, jeKarta: jeKarta) { X = i, Y = j };
        }

        public MyCheckBoxPolje(bool jeKarta = false) : this(6, 5, jeKarta: jeKarta) { }

        public MyCheckBox this[int x, int y] {
            get => this.polje[x, y];
            set { this.polje[x, y] = value; }
        }
    }
}
