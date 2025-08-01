using DataModels;
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

        public MyCheckBox[,] Polje { get { return polje; } set { } }

        public int BrPog {
            get => brPog;
            set {
                brPog = value;
                if (value == W*H-praznaPolja.Count) Ende.Invoke(this, EventArgs.Empty);
            }
        }
        public int W { get; set; }
        public int H { get; set; }
        public int dimPolja { get => polje == null ? 45 : polje[0, 0].Dim; }
        public int LastCheck { get; set; } = -1;
        int BrojParova { get => Program.data.P; }
        int BrojSlika { get => Program.data.S; }

        // *U pokusaju da se naucim lepo da projektujem klase cak i u C# po Single Responsibility principu pored navike za spaghetti code*
        public MyCheckBoxPolje() { }
        public MyCheckBoxPolje(bool jeKarta = false) : this(6, 5, jeKarta: jeKarta) { }
        public MyCheckBoxPolje(int x, int y, int size = 45, bool jeKarta = false) {
            praznaPolja = new Dictionary<int, bool>();
            NumberGenerator numberGenerator = new NumberGenerator(BrojSlika, BrojParova, x*y);
            W = x;
            H = y;

            polje = new MyCheckBox[W, H];
            for (int i = 0; i < W; i++)
                for (int j = 0; j < H; j++) {
                    polje[i, j] = new MyCheckBox(broj: jeKarta ? numberGenerator.getNumber(Form1.r) : 0, dim: size, jeKarta: jeKarta) { X = i, Y = j };
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

        public static MyCheckBoxPolje Load() {
            StreamReader rd = null;

            try {
                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.ShowDialog();
                rd = new StreamReader(fileDialog.FileName, Encoding.UTF8);
                XmlSerializer sr = new XmlSerializer(typeof(MyCheckBoxPolje));
                return (MyCheckBoxPolje)sr.Deserialize(rd);
            } catch (Exception err) { 
                System.Diagnostics.Debug.WriteLine(err.Message);
                return new MyCheckBoxPolje();
            } finally {
                if (rd!=null) rd.Close(); // ovo nece da se izvrsi ako se izvrsi return u try bloku, po mom shvatanju, ali ovako je radjeno i na racunskim vezbama
            }
        }
    }
}
