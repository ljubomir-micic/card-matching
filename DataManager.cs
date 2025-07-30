using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using System.Xml;

namespace lab03 {
    public class DataManager {
        public string filePath { get; } = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "ljubomirmicic19788-lab03.xml");
        public string resrcPath { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public int W { get; set; } = 6;
        public int H { get; set; } = 5;

        public void Save() {
            XmlTextWriter wr = null;

            try {
                wr = new XmlTextWriter(filePath, Encoding.UTF8);
                XmlSerializer sr = new XmlSerializer(typeof(DataManager));
                sr.Serialize(wr, this);
            } catch (Exception err) { 
                System.Diagnostics.Debug.WriteLine(err.Message);
            } finally {
                if (wr!=null) wr.Close();
            }
        }

        public static DataManager Load() {
            StreamReader rd = null;

            try {
                rd = new StreamReader(filePath, Encoding.UTF8);
                XmlSerializer sr = new XmlSerializer(typeof(DataManager));
                return (DataManager)sr.Deserialize(rd);
            } catch (Exception err) { 
                System.Diagnostics.Debug.WriteLine(err.Message);
                return new DataManager();
            } finally {
                if (rd!=null) rd.Close(); // ovo nece da se izvrsi ako se izvrsi return u try bloku, po mom shvatanju, ali ovako je radjeno i na racunskim vezbama
            }
        }
    }
}
