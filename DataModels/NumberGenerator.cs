using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels
{
    public class NumberGenerator {
        static int[] niz;

        public NumberGenerator(int brojS, int brojP) {
            Random r = new Random();
            niz = new int[brojS];
            for (int i = 0; i < brojP; i++)
                niz[r.Next()%niz.Length] += 2;
        }

        bool isEmpty { get { bool ie = true; for (int i = 0; i < niz.Length; i++) if (niz[i] != 0) ie = false; return ie; } }

        public int getNumber(Random r) {
            int ind = -1;
            do {
                ind = r.Next()%niz.Length;
            } while (niz[ind] == 0 && !isEmpty);
            niz[ind] = niz[ind]==0 ? 0 : niz[ind]-1;
            return ind;
        }
    }
}
