using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab03
{
    public static class GenerisanjeSlike {
        public static Color VratiBoju(int broj) {
            return (broj == 0 ? Color.White : Color.Gold);
        } 
    }
}
