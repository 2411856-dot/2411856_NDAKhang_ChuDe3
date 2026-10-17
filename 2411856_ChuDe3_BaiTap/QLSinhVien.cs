using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace _2411856_ChuDe3_BaiTap
{
    public class QLSinhVien
    {
        private List<Student> dsSinhVien;
        private string tenFile;

        public List<Student> DanhSach
        {
            get { return dsSinhVien; }
        }

        // Constructor nhận đường dẫn file (mặc định là students.txt)
        public QLSinhVien(string file)
        {
            tenFile = file;
            dsSinhVien = DocGhiFile.DocFile(tenFile);
            
        }


        public bool Them(Student sv)
        {
            Student sinhVienTimThay = TimTheoMSSV(sv.MSSV);

            if (sinhVienTimThay != null)
            {
                return false;
            }

            dsSinhVien.Add(sv);

            

            return true;
        }

        // =========================================
        // CẬP NHẬT SINH VIÊN
        // =========================================
        public bool CapNhat(Student svMoi)
        {
            Student svCu = TimTheoMSSV(svMoi.MSSV);

            if (svCu == null)
            {
                return false;
            }

            svCu.HoVaTenLot = svMoi.HoVaTenLot;
            svCu.Ten = svMoi.Ten;
            svCu.NgaySinh = svMoi.NgaySinh;
            svCu.Phai = svMoi.Phai;
            svCu.Lop = svMoi.Lop;
            svCu.SoCMND = svMoi.SoCMND;
            svCu.SoDienThoai = svMoi.SoDienThoai;
            svCu.DiaChi = svMoi.DiaChi;
            svCu.MonDK = svMoi.MonDK;

            

            return true;
        }

        // =========================================
        // TÌM THEO MSSV
        // =========================================
        public Student TimTheoMSSV(string mssv)
        {
            foreach (Student sv in dsSinhVien)
            {
                if (sv.MSSV == mssv)
                {
                    return sv;
                }
            }

            return null;
        }

        // =========================================
        // TÌM THEO TÊN
        // =========================================
        public List<Student> TimTheoTen(string ten)
        {
            List<Student> ketQua = new List<Student>();

            foreach (Student sv in dsSinhVien)
            {
                if (sv.Ten.ToLower().Contains(ten.ToLower()))
                {
                    ketQua.Add(sv);
                }
            }

            return ketQua;
        }

        // =========================================
        // TÌM THEO LỚP
        // =========================================
        public List<Student> TimTheoLop(string lop)
        {
            List<Student> ketQua = new List<Student>();

            foreach (Student sv in dsSinhVien)
            {
                if (sv.Lop.ToLower().Contains(lop.ToLower()))
                {
                    ketQua.Add(sv);
                }
            }

            return ketQua;
        }

        // =========================================
        // XÓA 1 SINH VIÊN
        // =========================================
        public bool Xoa(string mssv)
        {
            Student sv = TimTheoMSSV(mssv);

            if (sv == null)
            {
                return false;
            }

            dsSinhVien.Remove(sv);

            

            return true;
        }

        // =========================================
        // XÓA NHIỀU SINH VIÊN
        // =========================================
        public void XoaNhieu(List<string> danhSachMSSV)
        {
            foreach (string mssv in danhSachMSSV)
            {
                Student sv = TimTheoMSSV(mssv);

                if (sv != null)
                {
                    dsSinhVien.Remove(sv);
                }
            }

            
        }

        // =========================================
        // KIỂM TRA MSSV
        // =========================================
        public bool KiemTraMSSVTonTai(string mssv)
        {
            Student sv = TimTheoMSSV(mssv);

            if (sv != null)
            {
                return true;
            }

            return false;
        }

        // =========================================
        // LƯU FILE
        // =========================================
        public void LuuFile()
        {
            DocGhiFile.GhiFile(tenFile, dsSinhVien);
        }

        // =========================================
        // ĐỌC LẠI FILE
        // =========================================
        public void DocLaiFile()
        {
            dsSinhVien = DocGhiFile.DocFile(tenFile);
        }
        public List<Student> TimNhieuDieuKien(
    string mssv,
    string ten,
    string lop)
        {
            List<Student> ketQua = new List<Student>();

            foreach (Student sv in dsSinhVien)
            {
                bool dungMSSV = true;
                bool dungTen = true;
                bool dungLop = true;

                if (mssv != "")
                {
                    dungMSSV =
                        sv.MSSV.Contains(mssv);
                }

                if (ten != "")
                {
                    

                    dungTen =
                        sv.Ten.ToLower()
                        .Contains(ten.ToLower());
                }

                if (lop != "")
                {
                    dungLop =
                        sv.Lop.ToLower()
                        .Contains(lop.ToLower());
                }

                if (dungMSSV && dungTen && dungLop)
                {
                    ketQua.Add(sv);
                }
            }

            return ketQua;
        }
    }
}