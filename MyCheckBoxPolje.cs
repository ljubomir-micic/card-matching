using DataModels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace lab03 {
    // Kako je MyCheckBox nemoguce pozicionirati u poseban projekat (u ovoj klasi je neophodna inicijalizacija, a konstruktor je u ovom namespace-u) nemoguce je i ovu klasu
    // Pokusao sam, definitivno ne
    public class MyCheckBoxPolje {
        MyCheckBox[,] polje; Dictionary<int, bool> praznaPolja;
        public event EventHandler Click = delegate { };
        public event EventHandler Ende = delegate { };
        int brPog = 0;

        public uint Elapsed { get; set; } = 0;
        public int W { get; set; }
        public int H { get; set; }
        public int D { get; set; }

        [XmlArray("PoljePodaci")]
        [XmlArrayItem("PoljePodaciElement")]
        public int[] PoljePodaci {
            get {
                var lista = new LinkedList<int>();
                for (int i = 0; i < W; i++)
                    for (int j = 0; j < H; j++) {
                        lista.AddFirst(polje[i, j].VALUE);
                    }
                return lista.ToArray();
            } set {
                praznaPolja = new Dictionary<int, bool>();
                for (int i = 0; i < value.Length; i++) if ((value[i]&0xFF) == 0)
                    praznaPolja[(value[i] >> 8) & 0xFF] = true;
                polje = new MyCheckBox[W, H];
                for (int i = 0; i < value.Length; i++) {
                    int x = (value[i] >> 12) & 0xF, y = (value[i] >> 8) & 0xF;
                    polje[x, y] = new MyCheckBox(D) { VALUE = (value[i]) };
                }
            }
        }

        public int BrPog {
            get => brPog;
            set {
                brPog = value;
                if (value == W*H-praznaPolja.Count) {
                    foreach (int i in praznaPolja.Keys)
                        polje[(i >> 4), (i & 0xF)].Checked = true;
                    Ende.Invoke(this, EventArgs.Empty);
                }
            }
        }
        public int LastCheck { get; set; } = -1;
        int BrojParova { get => Program.data.P; }
        int BrojSlika { get => Program.data.S; }

        // *U pokusaju da se naucim lepo da projektujem klase cak i u C# po Single Responsibility principu pored navike za spaghetti code*
        public MyCheckBoxPolje() { this.praznaPolja = new Dictionary<int, bool>(); }
        public MyCheckBoxPolje(int x, int y, int size, bool jeKarta = false) {
            praznaPolja = new Dictionary<int, bool>();
            NumberGenerator numberGenerator = new NumberGenerator(BrojSlika, BrojParova, x*y);
            W = x;
            H = y;
            D = size;

            polje = new MyCheckBox[W, H];
            for (int i = 0; i < W; i++)
                for (int j = 0; j < H; j++) {
                    polje[i, j] = new MyCheckBox(broj: jeKarta ? numberGenerator.getNumber(Form1.r) : 0, dim: D, jeKarta: jeKarta) { X = i, Y = j };
                    if (jeKarta && (int) (polje[i, j].Tag) == 0) { praznaPolja[(i<<4)+j] = true; }
                    polje[i, j].Click += delegate(object sender, EventArgs e) { MyCheckBox m = ((MyCheckBox) sender); if (m.jeKarta) return; LastCheck = (m.X << 4) + m.Y; Click.Invoke(this, new EventArgs()); };
                }
            //System.Diagnostics.Debug.WriteLine(numberGenerator.imaNepar);
        }

        public MyCheckBox this[int x, int y] {
            get => this.polje[x, y];
            set { this.polje[x, y] = value; }
        }

        public void Invalidate()
        {
            for (int i = 0; i < W; i++)
                for (int j = 0; j < H; j++)
                    polje[i, j].Invalidate();
        }

        public void Save() {
            XmlTextWriter wr = null;

            try {
                SaveFileDialog fileDialog = new SaveFileDialog();
                fileDialog.ShowDialog();
                wr = new XmlTextWriter(fileDialog.FileName, Encoding.UTF8);
                XmlSerializer sr = new XmlSerializer(typeof(MyCheckBoxPolje));
                sr.Serialize(wr, this);
            } catch (Exception err) { 
                System.Diagnostics.Debug.WriteLine(err.Message);
            } finally {
                if (wr!=null) wr.Close();
            }
        }

        // Ok ovo me je namucilo previse
        public static MyCheckBoxPolje Load(string filename) {
            StreamReader rd = null;

            try {
                rd = new StreamReader(filename, Encoding.UTF8);
                XmlSerializer sr = new XmlSerializer(typeof(MyCheckBoxPolje));
                return (MyCheckBoxPolje)sr.Deserialize(rd);
            } catch (Exception err) { 
                System.Diagnostics.Debug.WriteLine(err.Message);
                return new MyCheckBoxPolje();
            } finally {
                if (rd!=null) rd.Close();
            }
        }
    }
}
