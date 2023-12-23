using QuanLyDatThueSanBongNhanTao.DAO;
using QuanLyDatThueSanBongNhanTao.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyDatThueSanBongNhanTao
{
    public partial class fKho : Form
    {
        public fKho()
        {
            InitializeComponent();
            LoadKho();
            dtpNgayNhap.Text = string.Empty;
        }

        SanPham sp = new SanPham();
        Kho kho = new Kho();

        private void LoadKho()
        {
            int i = 0;
            dgvKho.Rows.Clear();
            KhoDAO kh = new KhoDAO();
            List<Kho> list = new List<Kho>();
            list = kh.GetKho();
            foreach (Kho item in list)
            {
                i++;
                dgvKho.Rows.Add(item.ID, i, item.TenSp, item.SoLuong, item.NgayNhap, item.GiaNhap);
            }
        }

        private void ClearKho()
        {
            kho = new Kho();
        }

        private void ClearSanPham()
        {
            sp = new SanPham();
        }

        private void GetSanPham_KhoText()
        {
            sp.TenSP = txtTenSanPham.Text;
            if(txtGiaBan.Enabled == true)
            {
                sp.GiaBan = Int32.Parse(txtGiaBan.Text);
            }
            sp.Loai = cboxLoaiSP.Text;
            kho.TenSp = txtTenSanPham.Text;
            kho.SoLuong = (int)numSoLuong.Value;
            kho.NgayNhap = dtpNgayNhap.Value.ToString();
            kho.GiaNhap = float.Parse(txtGiaNhap.Text);
        }

        private void ClearSanPham_KhoText()
        {
            txtTenSanPham.Text = string.Empty;
            txtGiaBan.Text = string.Empty;
            cboxLoaiSP.Text = string.Empty;
            txtTenSanPham.Text = string.Empty;
            numSoLuong.Value = 0;
            cboxLoai.Text = string.Empty;
            dtpNgayNhap.Text = string.Empty;
            txtGiaNhap.Text = string.Empty;
            btnThem.Enabled = true;
            btnNhapHang.Enabled = false;
        }

        private void fKho_Load(object sender, EventArgs e)
        {

        }

        private void dgvKho_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string ColName = dgvKho.Columns[e.ColumnIndex].Name;
            if (ColName == "ColNhapKho")
            {
                if ((MessageBox.Show("Bạn có muốn nhập hàng sản phẩm này?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes))
                {
                    ClearKho();
                    txtGiaBan.Enabled = false;
                    cboxLoaiSP.Enabled = false;
                    Database.ID_Kho = (int)dgvKho.CurrentRow.Cells[0].Value;
                    txtTenSanPham.Text = dgvKho.CurrentRow.Cells[2].Value.ToString();
                    numSoLuong.Text = dgvKho.CurrentRow.Cells[3].Value.ToString();
                    numSoLuong.Minimum = (int)dgvKho.CurrentRow.Cells[3].Value;
                    dtpNgayNhap.Text = string.Empty;
                    txtGiaNhap.Text = dgvKho.CurrentRow.Cells[5].Value.ToString();
                    btnThem.Enabled = false;
                    btnNhapHang.Enabled = true;
                }
            }
            else if (ColName == "ColXoaKho")
            {
                if (MessageBox.Show("Xóa sản phẩm này khỏi kho?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    ClearKho();
                    Database.ID_Kho = (int)dgvKho.CurrentRow.Cells[0].Value;
                    KhoDAO.XoaSanPhamTuKho(Database.ID_Kho);
                    MessageBox.Show("Khách hàng đã xóa thành công!");
                    ClearSanPham_KhoText();
                    LoadKho();
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            GetSanPham_KhoText();
            SanPhamDAO.ThemSanPham(sp);
            KhoDAO.ThemSanPhamVaoKho(kho);
            MessageBox.Show("Thêm sản phẩm thành công!");
            ClearSanPham_KhoText();
            LoadKho();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            dgvKho.Rows.Clear();
            ClearSanPham_KhoText();
            LoadKho();
        }

        private void btnNhapHang_Click(object sender, EventArgs e)
        {
            GetSanPham_KhoText();
            KhoDAO.NhapHang(kho);
            MessageBox.Show("Nhập hàng thành công!");
            ClearSanPham_KhoText();
            txtGiaBan.Enabled = true;
            cboxLoaiSP.Enabled = true;
            btnNhapHang.Enabled = false;
            btnThem.Enabled = true;
            LoadKho();
        }

        private void txtTenSanPham_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                numSoLuong.Focus();
                e.Handled = true;
            }
        }

        private void numSoLuong_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                dtpNgayNhap.Focus();
                e.Handled = true;
            }
        }

        private void dtpNgayNhap_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtGiaNhap.Focus();
                e.Handled = true;
            }
        }

        private void txtGiaNhap_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtGiaBan.Focus();
                e.Handled = true;
            }
        }

        private void txtGiaBan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cboxLoaiSP.Focus();
                e.Handled = true;
            }
        }

        private void cboxLoaiSP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnThem.Focus();
                e.Handled = true;
            }
        }

        private void cboxLoai_TextChanged(object sender, EventArgs e)
        {
            int i = 0;
            dgvKho.Rows.Clear();
            KhoDAO kho = new KhoDAO();
            List<Kho> list = new List<Kho>();
            string loai = cboxLoai.Text;
            list = kho.GetLoaiSanPham(loai);
            foreach (Kho item in list)
            {
                i++;
                dgvKho.Rows.Add(item.ID, i, item.TenSp, item.SoLuong, item.NgayNhap, item.GiaNhap);
            }
        }

    }
}

