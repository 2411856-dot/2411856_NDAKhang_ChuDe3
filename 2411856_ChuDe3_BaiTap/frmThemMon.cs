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
    public partial class frmThemMon : Form
    {
        public string TenMon
        {
            get { return txtTenMon.Text.Trim(); }
        }
        public frmThemMon()
        {
            InitializeComponent();
        }
        private void frmThemMon_Load(object sender, EventArgs e)
        {
            // Có thể để trống hoặc viết code khởi tạo cho form thêm môn học ở đây
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (txtTenMon.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập tên môn học!");
                txtTenMon.Focus();
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
