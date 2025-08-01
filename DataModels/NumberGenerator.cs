using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels
{
    public class NumberGenerator {
        static Dictionary<int, int> niz;

        public NumberGenerator(int brojSlika, int brojParova, int brEl) {
            Random r = new Random();
            niz = new Dictionary<int, int>();
            for (int i = 0; i < brojParova; i++) {
                int kljuc = r.Next(brojSlika);
                if (!niz.ContainsKey(kljuc)) niz[kljuc] = 2;
                else niz[kljuc] += 2;
            }
            niz[-1] = 0;
            for (int i = 0; i < (brEl - (brojParova<<1)); i++)
                niz[-1] += 1;
        }

        public bool imaNepar { get { foreach (var i in niz.Values) if (i % 2 == 1) return true; return false; } }
        public bool isEmpty { get { bool ie = true; foreach (var i in niz.Keys) if (niz[i] != 0) ie = false; return ie; } }

        public int getNumber(Random r) {
            var kljucevi = niz.Keys.ToList();
            int ind;
            do { ind = kljucevi[r.Next(niz.Count)]; }
            while (niz[ind] == 0);
            niz[ind] = niz[ind]-1;
            return ind+1;
        }
    }
}
