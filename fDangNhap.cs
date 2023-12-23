using QuanLyDatThueSanBongNhanTao.DAO;
using System.Windows.Forms;
using System;



namespace QuanLyDatThueSanBongNhanTao
{
    public partial class fDangNhap : Form
    {
        public fDangNhap()
        {
            InitializeComponent();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string TenDN = txtTenDangNhap.Text;
            string MatKhau = (txtMatKhau.Text);
            if (TenDN == string.Empty || MatKhau == string.Empty)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }
            if (TaiKhoanDAO.CheckLogin(TenDN, MatKhau) == true)
            {
                MessageBox.Show("Đăng nhập thành công!");
                fHome f = new fHome();
                f.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Tài khoản không hợp lệ! Vui lòng đăng nhập lại!");
                txtTenDangNhap.Clear();
                txtMatKhau.Clear();
                txtTenDangNhap.Select();
            }
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            fDangKy f = new fDangKy();
            f.ShowDialog();
            this.Hide();
        }

        private void txtTenDangNhap_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtMatKhau.Focus();
                e.Handled = true;
            }
        }

        private void txtMatKhau_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnDangNhap.Select();
                e.Handled = true;
            }
        }

        private void btnShowMK_Click(object sender, EventArgs e)
        {
            if (txtMatKhau.UseSystemPasswordChar == true)
            {
                txtMatKhau.UseSystemPasswordChar = false;
                btnShowMK.Visible = false;
                btnShowMK.Enabled = false;
                btnHideMK.Visible = true;
                btnHideMK.Enabled = true;
                btnHideMK.Focus();
            }
        }

        private void btnHideMK_Click(object sender, EventArgs e)
        {
            if(txtMatKhau.UseSystemPasswordChar == false)
            {
                txtMatKhau.UseSystemPasswordChar = true;
                btnShowMK.Visible = true;
                btnShowMK.Enabled = true;
                btnHideMK.Visible = false;
                btnHideMK.Enabled = false;
                btnShowMK.Focus();
            }
        }

        private void btnExitLogin_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}