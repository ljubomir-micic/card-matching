using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace lab03 {
    public class MyCheckBoxPolje {
        MyCheckBox[,] polje;

        public int W { get => DataManager.W; }
        public int H { get => DataManager.H; }
        public int dimPolja { get => MyCheckBox.Dim; }

        // *U pokusaju da se naucim lepo da projektujem klase cak i u C# po Single Responsibility principu pored navike za spaghetti code*
        public MyCheckBoxPolje() {
            polje = new MyCheckBox[W, H];
            for (int i = 0; i < W; i++)
                for (int j = 0; j < H; j++)
                    polje[i, j] = new MyCheckBox(Form1.r.Next(17));
        }

        public MyCheckBox this[int x, int y] {
            get => this.polje[x, y];
            set { this.polje[x, y] = value; }
        }
    }
}
