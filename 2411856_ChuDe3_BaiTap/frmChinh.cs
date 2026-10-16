using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace _2411856_ChuDe3_BaiTap
{
    public partial class frmChinh : Form
    {
        QLSinhVien QLSV;
        bool daThayDoi = false;

        public frmChinh()
        {
            InitializeComponent();
        }

        private void frmChinh_Load(object sender, EventArgs e)
        {

            OpenFileDialog open = new OpenFileDialog();

            open.Title = "Chọn file danh sách sinh viên";

            open.Filter =
                "File sinh viên|*.txt;*.xml;*.json|" +
                "TXT|*.txt|" +
                "XML|*.xml|" +
                "JSON|*.json";

            if (open.ShowDialog() == DialogResult.OK)
            {
                QLSV = new QLSinhVien(open.FileName);

                HienThiDanhSach(QLSV.DanhSach);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn file danh sách sinh viên!");

                this.Close();
            }
        }
        private void HienThiDanhSach(List<Student> ds)
        {
            lvSinhVien.Items.Clear();

            foreach (Student sv in ds)
            {
                ListViewItem item = new ListViewItem(sv.MSSV);

                item.SubItems.Add(sv.HoVaTenLot);
                item.SubItems.Add(sv.Ten);
                item.SubItems.Add(sv.NgaySinh.ToString("dd/MM/yyyy"));
                item.SubItems.Add(sv.Lop);
                item.SubItems.Add(sv.SoCMND);
                item.SubItems.Add(sv.SoDienThoai);
                item.SubItems.Add(sv.DiaChi);

                item.Tag = sv;

                lvSinhVien.Items.Add(item);
            }
        }
        
        public Student GetStudent()
        {
            Student sv = new Student();
            List<string> mon = new List<string>();
            sv.MSSV = mtxtMSSV.Text.Trim();
            sv.HoVaTenLot = txtHoVaTenLot.Text.Trim();
            sv.NgaySinh = dtpNgaySinh.Value;
            sv.SoCMND = mtxtCMND.Text.Trim();
            sv.DiaChi = txtDiaChi.Text.Trim();
            if (rdNam.Checked)
                sv.Phai = true;
            else
                sv.Phai = false;
            sv.Ten = txtTen.Text.Trim();
            sv.SoDienThoai = mtxtSoDT.Text.Trim();
            sv.Lop = cboLop.Text.Trim();
            for (int i = 0; i < clbMonHocDK.Items.Count; i++)
            {
                if (clbMonHocDK.GetItemChecked(i))
                {
                    mon.Add(clbMonHocDK.Items[i].ToString());
                }
            }
            sv.MonDK = mon;
            return sv;
        }


        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult kq = MessageBox.Show(
                "Bạn có chắc chắn muốn thoát chương trình không?",
                "Xác nhận thoát",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (kq == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void frmChinh_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (daThayDoi)
            {
                DialogResult kq = MessageBox.Show(
                    "Danh sách sinh viên đã được thay đổi.\nBạn có muốn lưu lại vào file không?",
                    "Xác nhận thoát",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question);

                if (kq == DialogResult.Yes)
                {
                   
                    QLSV.LuuFile(); // Ghi file khi người dùng bấm Lưu
                }
                else if (kq == DialogResult.Cancel)
                {
                    e.Cancel = true; // Hủy lệnh thoát, giữ lại form
                }
            }
        }

        

        private bool KiemTraDuLieu()
        {
            if (mtxtMSSV.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập MSSV!");
                mtxtMSSV.Focus();
                return false;
            }

            if (txtHoVaTenLot.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập họ và tên lót!");
                txtHoVaTenLot.Focus();
                return false;
            }

            if (txtTen.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập tên!");
                txtTen.Focus();
                return false;
            }

            if (mtxtCMND.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập số CMND!");
                mtxtCMND.Focus();
                return false;
            }

            if (mtxtSoDT.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập số điện thoại!");
                mtxtSoDT.Focus();
                return false;
            }

            if (txtDiaChi.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập địa chỉ!");
                txtDiaChi.Focus();
                return false;
            }

            if (cboLop.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng chọn lớp!");
                cboLop.Focus();
                return false;
            }

            if (!rdNam.Checked && !rdNu.Checked)
            {
                MessageBox.Show("Vui lòng chọn giới tính!");
                return false;
            }

            if (clbMonHocDK.CheckedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một môn học!");
                return false;
            }

            if (!KiemTraMSSV())
                return false;

            return true;
        }

        

        private void HienThiSinhVien(Student sv)
        {
            mtxtMSSV.Text = sv.MSSV;
            txtHoVaTenLot.Text = sv.HoVaTenLot;
            txtTen.Text = sv.Ten;
            dtpNgaySinh.Value = sv.NgaySinh;
            mtxtCMND.Text = sv.SoCMND;
            mtxtSoDT.Text = sv.SoDienThoai;
            txtDiaChi.Text = sv.DiaChi;
            cboLop.Text = sv.Lop;

            if (sv.Phai)
                rdNam.Checked = true;
            else
                rdNu.Checked = true;

            for (int i = 0; i < clbMonHocDK.Items.Count; i++)
            {
                clbMonHocDK.SetItemChecked(i, false);
            }

            foreach (string mon in sv.MonDK)
            {
                for (int i = 0; i < clbMonHocDK.Items.Count; i++)
                {
                    if (clbMonHocDK.Items[i].ToString().Trim().Equals(mon.Trim(), StringComparison.OrdinalIgnoreCase))
                    {
                        clbMonHocDK.SetItemChecked(i, true);
                    }
                }
            }
        }

        

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            frmTimKiem frm = new frmTimKiem(QLSV);
            frm.ShowDialog();
        }

        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            if (!KiemTraDuLieu())
                return;

            Student sv = GetStudent();

            if (!QLSV.Them(sv))
            {
                MessageBox.Show("MSSV đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mtxtMSSV.Focus();
                return;
            }

            daThayDoi = true;
            HienThiDanhSach(QLSV.DanhSach);
            XoaTrang();

            MessageBox.Show("Thêm sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void lvSinhVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvSinhVien.SelectedItems.Count == 0)
                return;

            Student sv = lvSinhVien.SelectedItems[0].Tag as Student;

            if (sv == null)
                return;

            HienThiSinhVien(sv);
        }

        private void XoaTrang()
        {
            mtxtMSSV.Clear();
            txtHoVaTenLot.Clear();
            txtTen.Clear();
            mtxtCMND.Clear();
            mtxtSoDT.Clear();
            txtDiaChi.Clear();

            dtpNgaySinh.Value = DateTime.Now;
            rdNam.Checked = false;
            rdNu.Checked = false;
            cboLop.SelectedIndex = -1;

            for (int i = 0; i < clbMonHocDK.Items.Count; i++)
            {
                clbMonHocDK.SetItemChecked(i, false);
            }

            lvSinhVien.SelectedItems.Clear();
            mtxtMSSV.Focus();
        }
        

        private void mnuThemMon_Click(object sender, EventArgs e)
        {
            frmThemMon frm = new frmThemMon();

            if (frm.ShowDialog() == DialogResult.OK)
            {
                string mon = frm.TenMon;

                if (clbMonHocDK.Items.Contains(mon))
                {
                    MessageBox.Show("Môn học đã tồn tại!");
                    return;
                }

                clbMonHocDK.Items.Add(mon);

                MessageBox.Show("Thêm môn thành công!");
            }
        }

        private void mnuXoaMon_Click(object sender, EventArgs e)
        {
            int index = clbMonHocDK.SelectedIndex;

            if (index == -1)
            {
                MessageBox.Show("Vui lòng chọn môn cần xóa!");
                return;
            }

            DialogResult kq = MessageBox.Show(
                "Bạn có chắc muốn xóa môn này?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (kq == DialogResult.Yes)
            {
                clbMonHocDK.Items.RemoveAt(index);
            }
        }

        private void mnXoaSinhVien_Click(object sender, EventArgs e)
        {
            if (lvSinhVien.CheckedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn sinh viên cần xóa!");
                return;
            }

            DialogResult kq = MessageBox.Show(
                "Bạn có chắc muốn xóa các sinh viên đã chọn?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (kq != DialogResult.Yes)
                return;

            List<string> dsMSSV = new List<string>();

            foreach (ListViewItem item in lvSinhVien.CheckedItems)
            {
                dsMSSV.Add(item.Text);
            }

            QLSV.XoaNhieu(dsMSSV);

            daThayDoi = true;
            HienThiDanhSach(QLSV.DanhSach);
            XoaTrang();

            MessageBox.Show("Xóa thành công!");
        }

        private void btnHienThiLaiDS_Click(object sender, EventArgs e)
        {
            if (QLSV != null)
            {
                HienThiDanhSach(QLSV.DanhSach);
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (!KiemTraDuLieu())
                return;

            Student sv = GetStudent();

            if (QLSV.CapNhat(sv))
            {
                MessageBox.Show("Cập nhật thành công!");

                HienThiDanhSach(QLSV.DanhSach);
            }
            else
            {
                MessageBox.Show(
                    "Không tìm thấy sinh viên!");
            }
        }

        private string LayNamNhapHoc(string lop)
        {
            string lopUpper = lop.Trim().ToUpper();
            if (lopUpper.Contains("K42")) return "18";
            if (lopUpper.Contains("K43")) return "19";
            if (lopUpper.Contains("K44")) return "20";
            if (lopUpper.Contains("K45")) return "21";
            if (lopUpper.Contains("K46")) return "22";
            if (lopUpper.Contains("K47")) return "23";
            if (lopUpper.Contains("K48")) return "24";
            if (lopUpper.Contains("K49")) return "25";
            if (lopUpper.Contains("K50")) return "26";
            return "";
        }

        private bool KiemTraMSSV()
        {
            string mssv = mtxtMSSV.Text.Trim();
            string namNhapHoc = LayNamNhapHoc(cboLop.Text);

            if (mssv.Length != 7 || !mssv.All(char.IsDigit))
            {
                MessageBox.Show("MSSV phải gồm đúng 7 chữ số!");
                return false;
            }

            if (namNhapHoc == "")
            {
                MessageBox.Show("Không xác định được năm nhập học từ lớp!");
                return false;
            }

            if (mssv.Substring(0, 2) != namNhapHoc)
            {
                MessageBox.Show("2 số đầu MSSV phải là " + namNhapHoc + " theo năm nhập học của lớp!");
                return false;
            }

            if (mssv.Substring(2, 2) != "10")
            {
                MessageBox.Show("Số thứ 3 và thứ 4 của MSSV phải là 10!");
                return false;
            }

            return true;
        }
        private void clbMonHocDK_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int index = clbMonHocDK.IndexFromPoint(e.Location);
                if (index != ListBox.NoMatches)
                {
                    clbMonHocDK.SelectedIndex = index;
                }
                else
                {
                    clbMonHocDK.ClearSelected();
                }
            }
        }
    }

}