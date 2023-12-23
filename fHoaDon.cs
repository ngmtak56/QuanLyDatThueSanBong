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
    public partial class fHoaDon : Form
    {
        public fHoaDon()
        {
            InitializeComponent();
            dtpNgayLap.Text = string.Empty;
            LoadHoaDon();
        }

        private void fHoaDon_Load(object sender, EventArgs e)
        {

        }

        private HoaDon hDon = new HoaDon();

        public void LoadHoaDon()
        {
            int i = 0;
            dgvHoaDon.Rows.Clear();
            HoaDonDAO hoaDon = new HoaDonDAO();
            List<HoaDon> list = new List<HoaDon>();
            list = hoaDon.GetHoaDonList();
            foreach (HoaDon item in list)
            {
                i++;
                dgvHoaDon.Rows.Add(item.ID, i, item.TenKhach, item.TenSan, item.Ngaylap, item.TrangThai);
            }
        }

        private void ClearHoaDon()
        {
            txtTenSan.Text = string.Empty;
            txtTenKhach.Text = string.Empty;
        }

        private void ClearHoaDonText()
        {
            txtTenKhach.Text = string.Empty;
            txtTenSan.Text = string.Empty;
            dtpNgayLap.Text = string.Empty;
            cboxTrangThai.Text = string.Empty;
        }

        private void EnableBtnCapNhatHoaDon(bool enable)
        {
            btnCapNhat.Enabled = enable;
            btnIn.Enabled = !enable;
            btnXuat.Enabled = !enable;
        }

        private void ResetHoaDon()
        {
            ClearHoaDonText();
            btnXuat.Enabled = true;
            btnIn.Enabled = true;
            EnableBtnCapNhatHoaDon(false);
            LoadHoaDon();
        }

        private void GetHoaDonText()
        {
            hDon = new HoaDon();
            if (txtTenKhach.Text == null)
                hDon.TenKhach = "";
            else
                hDon.TenKhach = txtTenKhach.Text;
            if (txtTenSan.Text == null)
                hDon.TenSan = "";
            else
                hDon.TenSan = txtTenSan.Text;
            hDon.Ngaylap = dtpNgayLap.Text;
            hDon.TrangThai = cboxTrangThai.Text;
        }

        private void dgvHoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string ColName = dgvHoaDon.Columns[e.ColumnIndex].Name;
            if (ColName == "ColChiTietHD")
            {
                GetHoaDonText();
                HoaDon temp = new HoaDon();
                HoaDonDAO hoaDon = new HoaDonDAO();

                hDon.ID = (int)dgvHoaDon.CurrentRow.Cells[0].Value;
                temp = hoaDon.GetChiTietHoaDon(hDon.ID);
                if (temp != null)
                {
                    fChiTietHoaDon chiTiet = new fChiTietHoaDon(temp);
                    chiTiet.ShowDialog();
                }
            }
            else if (ColName == "ColSuaHD")
            {
                if (MessageBox.Show("Bạn muốn cập nhật hóa đơn này?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    DateTime dateVal;
                    string date = dgvHoaDon.CurrentRow.Cells[4].Value.ToString();

                    txtTenKhach.Text = dgvHoaDon.CurrentRow.Cells[2].Value.ToString();
                    txtTenSan.Text = dgvHoaDon.CurrentRow.Cells[3].Value.ToString();
                    if (DateTime.TryParse(date, out dateVal))
                        dtpNgayLap.Value = dateVal;
                    cboxTrangThai.Text = dgvHoaDon.CurrentRow.Cells[5].Value.ToString();
                    hDon.ID = (int)dgvHoaDon.CurrentRow.Cells[0].Value;
                    EnableBtnCapNhatHoaDon(true);
                }
            }
            else if (ColName == "ColXoaHoaDon")
            {
                if (MessageBox.Show("Xóa hóa đơn này khỏi danh sách?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    hDon = new HoaDon();
                    hDon.ID = (int)dgvHoaDon.CurrentRow.Cells[0].Value;
                    HoaDonDAO.XoaHoaDon(hDon);
                    ClearHoaDonText();
                    LoadHoaDon();
                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetHoaDon();
        }
    }
}
