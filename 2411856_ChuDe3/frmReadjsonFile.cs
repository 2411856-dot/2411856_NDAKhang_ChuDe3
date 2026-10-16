using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace _2411856_ChuDe3
{
    public partial class frmReadjsonFile : Form
    {
        public frmReadjsonFile()
        {
            InitializeComponent();
        }

        private void btnReadJSON_Click(object sender, EventArgs e)
        {
            string Str = "";
            string Path = @"Students.json";
            List<StudentInfo> List = LoadJson(Path);
            for (int i = 0;i < List.Count; i++  )
            {
                StudentInfo info = List[i];
                Str += string.Format("Sinh viên {0} có MSSV: {1}, họ tên: {2}," + "điểm TB: {3}\r\n", (i+1), info.MSSV, info.Hoten, info.Diem);
            }
            MessageBox.Show(Str);
        }
        private List<StudentInfo> LoadJson(string Path)
        {
            // Khai báo danh sách lưu trữ
            List<StudentInfo> List = new List<StudentInfo>();

            // Đọc tập tin
            StreamReader r = new StreamReader(Path);

            string json = r.ReadToEnd(); // Đọc hết

            // Chuyển về thành mảng các đối tượng
            var array = (JObject)JsonConvert.DeserializeObject(json);

            // Lấy đối tượng sinhvien
            var students = array["sinhvien"].Children();

            foreach (var item in students) // Duyệt mảng
            {
                // Lấy các thành phần
                string mssv = item["MSSV"].Value<string>();
                string hoten = item["hoten"].Value<string>();
                int tuoi = item["tuoi"].Value<int>();
                double diem = item["diem"].Value<double>();
                bool tongiao = item["tongiao"].Value<bool>();

                // Chuyển vào đối tượng StudentInfo
                StudentInfo info = new StudentInfo(
                    mssv,
                    hoten,
                    tuoi,
                    diem,
                    tongiao
                );

                List.Add(info); // Thêm vào danh sách
            }

            return List;
        }
    }
}
