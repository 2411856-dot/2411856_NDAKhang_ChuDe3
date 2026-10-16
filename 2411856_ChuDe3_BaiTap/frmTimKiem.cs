using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2411856_ChuDe3_BaiTap
{
    public partial class frmTimKiem : Form
    {
        QLSinhVien qlsv;
        
        public frmTimKiem(QLSinhVien ql)
        {
            InitializeComponent();
            qlsv = ql;

        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string mssv = mtxtMSSV.Text.Trim();
            string ten = txtTen.Text.Trim();
            string lop = cboLop.Text.Trim();

            if (mssv == "" &&
                ten == "" &&
                lop == "")
            {
                MessageBox.Show(
                    "Vui lòng nhập ít nhất một điều kiện tìm kiếm!"
                );

                return;
            }

            List<Student> ketQua =
                qlsv.TimNhieuDieuKien(
                    mssv,
                    ten,
                    lop
                );

            HienThiKetQua(ketQua);
        }
        private void HienThiKetQua(List<Student> ds)
        {
            lvKetQua.Items.Clear();

            foreach (Student sv in ds)
            {
                ListViewItem item =
                    new ListViewItem(sv.MSSV);

                item.SubItems.Add(sv.HoVaTenLot);
                item.SubItems.Add(sv.Ten);
                item.SubItems.Add(sv.NgaySinh.ToString("dd/MM/yyyy"));
                item.SubItems.Add(sv.Lop);
                item.SubItems.Add(sv.SoCMND);
                item.SubItems.Add(sv.SoDienThoai);

                lvKetQua.Items.Add(item);
            }

            if (ds.Count == 0)
            {
                MessageBox.Show("Không tìm thấy sinh viên!");
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void frmTimKiem_Load(object sender, EventArgs e)
        {
            lvKetQua.View = View.Details;
            lvKetQua.FullRowSelect = true;
            lvKetQua.GridLines = true;
        }
    }
}
