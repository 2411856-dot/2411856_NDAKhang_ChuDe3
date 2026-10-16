using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace _2411856_ChuDe3_BaiTap
{
    public class DocGhiFile
    {
        public static List<Student> DocFile(string tenFile)
        {
            string duoi = Path.GetExtension(tenFile).ToLower();

            if (duoi == ".txt")
                return DocTXT(tenFile);

            if (duoi == ".xml")
                return DocXML(tenFile);

            if (duoi == ".json")
                return DocJSON(tenFile);

            return new List<Student>();
        }

        public static void GhiFile(string tenFile, List<Student> ds)
        {
            string duoi = Path.GetExtension(tenFile).ToLower();

            if (duoi == ".txt")
                GhiTXT(tenFile, ds);
            else if (duoi == ".xml")
                GhiXML(tenFile, ds);
            else if (duoi == ".json")
                GhiJSON(tenFile, ds);
        }

        // ================= TXT =================

        private static List<Student> DocTXT(string tenFile)
        {
            List<Student> ds = new List<Student>();

            if (!File.Exists(tenFile))
                return ds;

            foreach (string line in File.ReadAllLines(tenFile))
            {
                if (line.Trim() == "")
                    continue;

                string[] c = line.Split('|');

                Student sv = new Student();

                sv.MSSV = c[0];
                sv.HoVaTenLot = c[1];
                sv.Ten = c[2];
                sv.NgaySinh = DateTime.ParseExact(
                    c[3], "dd/MM/yyyy",
                    CultureInfo.InvariantCulture);

                sv.Phai = c[4] == "Nam";
                sv.Lop = c[5];
                sv.SoCMND = c[6];
                sv.SoDienThoai = c[7];
                sv.DiaChi = c[8];

                sv.MonDK = new List<string>(c[9].Split(','));

                ds.Add(sv);
            }

            return ds;
        }

        private static void GhiTXT(string tenFile, List<Student> ds)
        {
            List<string> lines = new List<string>();

            foreach (Student sv in ds)
            {
                string dong =
                    sv.MSSV + "|" +
                    sv.HoVaTenLot + "|" +
                    sv.Ten + "|" +
                    sv.NgaySinh.ToString("dd/MM/yyyy") + "|" +
                    (sv.Phai ? "Nam" : "Nu") + "|" +
                    sv.Lop + "|" +
                    sv.SoCMND + "|" +
                    sv.SoDienThoai + "|" +
                    sv.DiaChi + "|" +
                    string.Join(",", sv.MonDK);

                lines.Add(dong);
            }

            File.WriteAllLines(tenFile, lines);
        }

        // ================= XML =================

        private static List<Student> DocXML(string tenFile)
        {
            if (!File.Exists(tenFile))
                return new List<Student>();

            XmlSerializer xl =
                new XmlSerializer(typeof(List<Student>));

            using (FileStream fs =
                new FileStream(tenFile, FileMode.Open))
            {
                return (List<Student>)xl.Deserialize(fs);
            }
        }

        private static void GhiXML(string tenFile, List<Student> ds)
        {
            XmlSerializer xl =
                new XmlSerializer(typeof(List<Student>));

            using (FileStream fs =
                new FileStream(tenFile, FileMode.Create))
            {
                xl.Serialize(fs, ds);
            }
        }

        // ================= JSON =================

        private static List<Student> DocJSON(string tenFile)
        {
            if (!File.Exists(tenFile))
                return new List<Student>();

            string json = File.ReadAllText(tenFile);

            List<Student> ds =
                JsonConvert.DeserializeObject<List<Student>>(json);

            if (ds == null)
                return new List<Student>();

            return ds;
        }
        private static void GhiJSON(string tenFile, List<Student> ds)
        {
            string json =
                JsonConvert.SerializeObject(ds, Formatting.Indented);

            File.WriteAllText(tenFile, json);
        }
    }
}
