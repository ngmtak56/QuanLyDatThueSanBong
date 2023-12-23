using QuanLyDatThueSanBongNhanTao.DAO;
using QuanLyDatThueSanBongNhanTao.DTO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System;

namespace QuanLyDatThueSanBongNhanTao
{
    public partial class fDangKy : Form
    {
        public fDangKy()
        {
            InitializeComponent();
            txtTenDKy.Focus();
        }

        public bool CheckTenTaiKhoan(string tk)
        {
            return Regex.IsMatch(tk, @"^[a-zA-Z]{6,24}$");
        }

        public bool CheckMatKhau(string tk)
        {
            return Regex.IsMatch(tk, @"^[a-zA-Z0-9]{6,50}$");
        }

        public bool CheckSdt(string sdt)
        {
            return Regex.IsMatch(sdt, @"^[0-9]{10,11}$");
        }

        private void btnExitDky_Click(object sender, EventArgs e)
        {
            this.Dispose();
            fDangNhap f = new fDangNhap();
            f.ShowDialog();
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            TaiKhoan tk = new TaiKhoan();
            KhachHang kh = new KhachHang();
            tk.TenTaiKhoan = txtTenDKy.Text;
            tk.MatKhau = txtMatKhauDky.Text;
            kh.TenKhach = txtHoTenDky.Text;
            kh.DiaChi = txtDiaChiDky.Text;
            kh.Sdt = txtSdtDky.Text;

            if (!CheckTenTaiKhoan(txtTenDKy.Text)) { MessageBox.Show("Vui lòng nhập tên tài khoản có độ dài 6-24 ký tự, viết hoa, viết thường"); return; }
            if (!CheckMatKhau(txtMatKhauDky.Text)) { MessageBox.Show("Vui lòng nhập mật khẩu độ dài 6 ký tự trở lên, viết hoa, viết thường, số"); return; }
            if (!CheckSdt(txtSdtDky.Text)) { MessageBox.Show("Nhập số điện thoại độ dài 10 hoặc 11 số!"); return; }

            if (TaiKhoanDAO.CheckLogin(tk.TenTaiKhoan, tk.MatKhau) == true || KhachHangDAO.CheckKhachHang(kh) == true)
            {
                MessageBox.Show("Tài khoản đã tồn tại! Vui lòng đăng ký thông tin khác!");
                txtTenDKy.Focus();
                return;
            }
            else
            {
                if(txtMatKhauDky.Text != txtXacMinh.Text)
                {
                    MessageBox.Show("Mật khẩu xác minh không khớp! Vui lòng nhập lại!");
                    txtXacMinh.Focus();
                    return;
                }
                else
                {
                    TaiKhoanDAO.ThemTaiKhoan(tk);
                    KhachHangDAO.ThemKhach(kh);
                    MessageBox.Show("Đăng ký thành công!");
                    fDangNhap f = new fDangNhap();
                    this.Dispose();
                    f.ShowDialog();
                }
            }
        }

        private void txtXacMinh_Leave(object sender, EventArgs e)
        {
            ErrorProvider errorProvider = new ErrorProvider();
            if(txtXacMinh.Text != txtMatKhauDky.Text)
            {
                errorProvider.SetError(txtXacMinh, "Mật khẩu xác nhận không khớp!");
            }
            else
            {
                errorProvider.SetError(txtXacMinh, null);
            }
        }

        private void txtTenDKy_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                txtMatKhauDky.Focus();
                e.Handled = true;
            }
        }

        private void txtMatKhauDky_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtXacMinh.Focus();
                e.Handled = true;
            }
        }

        private void txtXacMinh_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtHoTenDky.Focus();
                e.Handled = true;
            }
        }

        private void txtHoTenDky_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDiaChiDky.Focus();
                e.Handled = true;
            }
        }

        private void txtDiaChiDky_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSdtDky.Focus();
                e.Handled = true;
            }
        }

        private void txtSdtDky_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnDangKy.Focus();
                e.Handled = true;
            }
        }

    }
}
