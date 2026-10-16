using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2411856_ChuDe3
{
    public class StudentInfo
    {
        public string MSSV { get; set; }
        public string Hoten { get; set; }
        public int Tuoi { get; set; }
        public double Diem { get; set; }
        public bool Tongiao { get; set; }

        // Phương thức tạo lập
        public StudentInfo(string mssv, string hoten, int tuoi,
                           double diem, bool tongiao)
        {
            this.MSSV = mssv;
            this.Hoten = hoten;
            this.Tuoi = tuoi;
            this.Diem = diem;
            this.Tongiao = tongiao;
        }
    }
}
