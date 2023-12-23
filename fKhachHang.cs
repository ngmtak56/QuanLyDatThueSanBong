using QuanLyDatThueSanBongNhanTao.DAO;
using QuanLyDatThueSanBongNhanTao.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyDatThueSanBongNhanTao
{
    public partial class fKhachHang : Form
    {
        public fKhachHang()
        {
            InitializeComponent();
            LoadKhachHang();
            LoadTrongTai();
            pnlNhapKhach.BringToFront();
            pnlHienThiKhach.BringToFront();
        }

        private KhachHang khach = new KhachHang();

        private void LoadKhachHang()
        {
            int i = 0;
            dgvKhach.Rows.Clear();
            KhachHangDAO kh = new KhachHangDAO();
            List<KhachHang> list = new List<KhachHang>();
            list = kh.GetKhachHangList();
            foreach (KhachHang item in list)
            {
                i++;
                dgvKhach.Rows.Add(item.ID, i, item.TenKhach, item.DiaChi, item.Sdt);
            }
        }

        private void LoadTrongTai()
        {
            int i = 0;
            dgvTrongTai.Rows.Clear();
            KhachHangDAO kh = new KhachHangDAO();
            List<KhachHang> list = new List<KhachHang>();
            list = kh.GetTrongTaiList();
            foreach (KhachHang item in list)
            {
                i++;
                dgvTrongTai.Rows.Add(item.ID, i, item.TenKhach, item.Sdt, item.GiaThue);
            }
        }

        private void ClearKhach()
        {
            khach.ID = 0;
            khach.TenKhach = string.Empty;
            khach.DiaChi = string.Empty;
            khach.Sdt = string.Empty;
            khach.GiaThue = 0;
        }

        private void GetKhachText()
        {
            ClearKhach();
            khach.TenKhach = txtTenKhach.Text;
            khach.DiaChi = txtDiaChiKhach.Text;
            khach.Sdt = txtSdtKhach.Text;
        }

        private void ClearKhachText()
        {
            txtTenKhach.Clear();
            txtDiaChiKhach.Clear();
            txtSdtKhach.Clear();
            txtTenKhach.Enabled = true;
            txtDiaChiKhach.Enabled = true;
            txtSdtKhach.Enabled = true;
            txtTimKiem.Clear();
        }

        private void ResetKhach()
        {
            ClearKhachText();
            btnSua.Enabled = false;
            btnThem.Enabled = true;
            LoadKhachHang();
        }

        private void fKhachHang_Load(object sender, EventArgs e)
        {

        }

        private void dgvKhach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string ColName = dgvKhach.Columns[e.ColumnIndex].Name;
            if (ColName == "ColSuaKhach")
            {
                if ((MessageBox.Show("Bạn có muốn sửa thông tin khách hàng này?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes))
                {
                    Database.ID_Khach = (int)dgvKhach.CurrentRow.Cells[0].Value;
                    txtTenKhach.Text = dgvKhach.CurrentRow.Cells[2].Value.ToString();
                    txtDiaChiKhach.Text = dgvKhach.CurrentRow.Cells[3].Value.ToString();
                    txtSdtKhach.Text = dgvKhach.CurrentRow.Cells[4].Value.ToString();
                    btnSua.Enabled = true;
                    btnThem.Enabled = false;
                }
            }
            else if (ColName == "ColXoaKhach")
            {
                if (MessageBox.Show("Xóa khách hàng này khỏi danh sách ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    ClearKhach();
                    khach.ID = (int)dgvKhach.CurrentRow.Cells[0].Value;
                    KhachHangDAO.XoaKhach(khach);
                    MessageBox.Show("Khách hàng đã xóa thành công!");
                    ClearKhachText();
                    LoadKhachHang();
                    fSanBong San = new fSanBong();
                    fDichVu Dv = new fDichVu();
                    San.LoadCboxKhach();
                    Dv.LoadKhachHang();
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            int check = 0;
            if (txtTenKhach.Text == string.Empty
                || txtDiaChiKhach.Text == string.Empty
                || txtSdtKhach.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
                txtTenKhach.Focus();
                return;
            }
            check = KhachHangDAO.CheckTrungLapSoDienThoai(txtSdtKhach.Text);
            if(check != 0)
            {
                MessageBox.Show("Số điện thoại đã bị trùng lặp! Vui lòng nhập số điện thoại khác");
                txtSdtKhach.Focus();
                return;
            }
            else
            {
                GetKhachText();
                KhachHangDAO.ThemKhach(khach);
                fSanBong San = new fSanBong();
                fDichVu Dv = new fDichVu();
                MessageBox.Show("Thêm khách hàng thành công!");
                ClearKhachText();
                LoadKhachHang();
                San.LoadCboxKhach();
                Dv.LoadKhachHang();
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetKhach();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            GetKhachText();
            KhachHangDAO.SuaKhach(khach);
            ClearKhachText();
            btnSua.Enabled = false;
            btnThem.Enabled = true;
            LoadKhachHang();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            int i = 0;
            if (txtTimKiem.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng nhập thông tin tìm kiếm!");
                txtTimKiem.Focus();
                return;
            }
            else
            {
                dgvKhach.Rows.Clear();
                KhachHangDAO kh = new KhachHangDAO();
                List<KhachHang> listKhach = new List<KhachHang>();
                listKhach = kh.TimKiemKhach(txtTimKiem.Text);
                foreach (KhachHang item in listKhach)
                {
                    i++;
                    dgvKhach.Rows.Add(item.ID, i, item.TenKhach, item.DiaChi, item.Sdt);
                }
                ClearKhach();
            }
        }

        private void txtTenKhach_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDiaChiKhach.Focus();
                e.Handled = true;
            }
        }

        private void txtDiaChiKhach_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSdtKhach.Focus();
                e.Handled = true;
            }
        }

        private void txtSdtKhach_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnThem.Select();
                e.Handled = true;
            }
        }

        private void btnTrongTai_Click(object sender, EventArgs e)
        {
            pnlHienThiTrongTai.BringToFront();
            pnlNhapTrongTai.BringToFront();
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            pnlHienThiKhach.BringToFront();
            pnlNhapKhach.BringToFront();
        }

        private void dgvTrongTai_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string ColName = dgvTrongTai.Columns[e.ColumnIndex].Name;
            if (ColName == "ColSuaTT")
            {
                if ((MessageBox.Show("Bạn có muốn sửa thông tin trọng tài này?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes))
                {
                    Database.ID_Khach = (int)dgvTrongTai.CurrentRow.Cells[0].Value;
                    txtTenTrongTai.Text = dgvTrongTai.CurrentRow.Cells[2].Value.ToString();
                    txtSdtTrongTai.Text = dgvTrongTai.CurrentRow.Cells[3].Value.ToString();
                    txtGiaThueTT.Text = dgvTrongTai.CurrentRow.Cells[4].Value.ToString();
                    btnSuaTrongTai.Enabled = true;
                    btnThemTrongTai.Enabled = false;
                }
            }
            else if (ColName == "ColXoaTT")
            {
                if (MessageBox.Show("Xóa trọng tài này khỏi danh sách ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    ClearKhach();
                    Database.ID_Khach = (int)dgvTrongTai.CurrentRow.Cells[0].Value;
                    KhachHangDAO.XoaTrongTai(Database.ID_Khach);
                    MessageBox.Show("Trọng tài đã xóa thành công!");
                    ClearTrongTaiText();
                    LoadTrongTai();
                }
            }
        }

        private void ClearTrongTaiText()
        {
            txtTenTrongTai.Text = String.Empty;
            txtSdtTrongTai.Text = String.Empty;
            txtGiaThueTT.Text = String.Empty;
        }

        private void btnThemTrongTai_Click(object sender, EventArgs e)
        {
            int check = 0;
            if(txtTenTrongTai.Text == string.Empty || txtSdtTrongTai.Text == string.Empty || txtGiaThueTT.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!");
                return;
            }
            check = KhachHangDAO.CheckTrungLapSoDienThoaiTT(txtSdtTrongTai.Text);
            if (check != 0)
            {
                MessageBox.Show("Số điện thoại đã bị trùng lặp! Vui lòng nhập số điện thoại khác");
                txtSdtTrongTai.Focus();
                return;
            }
            else
            {
                ClearKhach();
                khach.TenKhach = txtTenTrongTai.Text;
                khach.Sdt = txtSdtTrongTai.Text;
                khach.GiaThue = (float)Convert.ToDouble(txtGiaThueTT.Text);
                KhachHangDAO.ThemTrongTai(khach);
                MessageBox.Show("Thêm trọng tài thành công");
                ClearTrongTaiText();
                LoadTrongTai();
            }
        }

        private void btnSuaTrongTai_Click(object sender, EventArgs e)
        {
            ClearKhach();
            khach.TenKhach = txtTenTrongTai.Text;
            khach.Sdt = txtSdtTrongTai.Text;
            khach.GiaThue = (float)Convert.ToDouble(txtGiaThueTT.Text);
            KhachHangDAO.SuaTrongTai(khach);
            ClearTrongTaiText();
            btnSuaTrongTai.Enabled = false;
            btnThemTrongTai.Enabled = true;
            ClearTrongTaiText();
            LoadKhachHang();
        }

        private void btnResetTrongTai_Click(object sender, EventArgs e)
        {
            ClearTrongTaiText();
            btnThemTrongTai.Enabled = true;
            btnSuaTrongTai.Enabled = false;
        }


        private void txtTenTrongTai_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSdtTrongTai.Focus();
                e.Handled = true;
            }
        }

        private void txtSdtTrongTai_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtGiaThueTT.Focus();
                e.Handled = true;
            }
        }

        private void txtGiaThueTT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnThemTrongTai.Focus();
                e.Handled = true;
            }
        }
    }
}
