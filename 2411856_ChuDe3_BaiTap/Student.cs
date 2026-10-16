using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2411856_ChuDe3_BaiTap
{
    public class Student
    {
        public string MSSV { get; set; }
        public string HoVaTenLot { get; set; }
        public DateTime NgaySinh { get; set; }
        public string SoCMND { get; set; }
        public bool Phai { get; set; }
        public string DiaChi { get; set; }
        public string Ten { get; set; }
        public string Lop { get; set; }
        public string SoDienThoai { get; set; }
        public List<string> MonDK { get; set; }

        public string HoTen()
        {
            return HoVaTenLot + " " + Ten;
        }

        public Student()
        {
            MonDK = new List<string>();

        }
        public Student(string mssv, string hoVaTenLot, DateTime ngaySinh, string soCMND, bool phai, string diaChi, string ten, string lop, string soDienThoai, List<string> monDK)
        {
            this.MSSV = mssv;
            this.HoVaTenLot = hoVaTenLot;
            this.NgaySinh = ngaySinh;
            this.SoCMND = soCMND;
            this.Phai = phai;
            this.DiaChi = diaChi;
            this.Ten = ten;
            this.Lop = lop;
            this.SoDienThoai = soDienThoai;
            this.MonDK = monDK;
        }
    }
}
