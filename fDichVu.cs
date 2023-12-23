using QuanLyDatThueSanBongNhanTao.DAO;
using QuanLyDatThueSanBongNhanTao.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace QuanLyDatThueSanBongNhanTao
{
    public partial class fDichVu : Form
    {
        private HoaDonDV hdon;
        public fDichVu()
        {
            InitializeComponent();
            LoadSanPham();
            LoadKhachHang();
            LoadHoaDonDV();
            pnlDichVu.BringToFront();
        }

        private void fDichVu_Load(object sender, EventArgs e)
        {
            dtpNgayXuat.Text = string.Empty;
        }

        private void LoadSanPham()
        {
            SanPhamDAO spham = new SanPhamDAO();
            List<SanPham> list = new List<SanPham>();
            List<SanPham> list1 = new List<SanPham>();
            List<SanPham> list2 = new List<SanPham>();
            list = spham.GetSanPhamList();
            list1 = spham.GetAoDaBongList();
            list2 = spham.GetGiayDaBongList();

            foreach (SanPham sp in list1)
            {
                Button btn = new Button();
                {
                    btn.Width = 90;
                    btn.Height = 60;
                }
                btn.Text = sp.TenSP;
                btn.Click += btn_Click;
                btn.Tag = sp;
                btn.BackColor = Color.FromArgb(97, 135, 110);
                btn.ForeColor = Color.WhiteSmoke;
                flpAo.Controls.Add(btn);
            }

            foreach (SanPham sp in list2)
            {
                Button btn = new Button();
                {
                    btn.Width = 90;
                    btn.Height = 60;
                }
                btn.Text = sp.TenSP;
                btn.Click += btn_Click;
                btn.Tag = sp;
                btn.BackColor = Color.FromArgb(97, 135, 110);
                btn.ForeColor = Color.WhiteSmoke;
                flpGiay.Controls.Add(btn);
            }

            foreach (SanPham sp in list)
            {
                Button btn = new Button();
                {
                    btn.Width = 90;
                    btn.Height = 60;
                }
                btn.Text = sp.TenSP;
                btn.Click += btn_Click;
                btn.Tag = sp;
                btn.BackColor = Color.FromArgb(97, 135, 110);
                btn.ForeColor = Color.WhiteSmoke;
                flpSanPham.Controls.Add(btn);
            }
        }

        public void LoadKhachHang()
        {
            cboxKhach.Items.Clear();
            cboxKhach2.Items.Clear();
            PhieuDatSanDAO pds = new PhieuDatSanDAO();
            List<KhachHang> khachs = new List<KhachHang>();
            khachs = pds.GetComboBoxKhach();
            foreach (KhachHang khach in khachs)
            {
                cboxKhach.Items.Add(khach.TenKhach);
                cboxKhach2.Items.Add(khach.TenKhach);
            }
        }

        public void ClearDichVu()
        {
            cboxKhach.Text = string.Empty;
            numSoLuong.Text = string.Empty;
            dtpNgayXuat.Text = string.Empty;
            dgvDichVu.Rows.Clear();
            flpSanPham.Controls.Clear();
            flpAo.Controls.Clear();
            flpGiay.Controls.Clear();
            LoadSanPham();
            LoadKhachHang();
        }


        private void btn_Click(object sender, EventArgs e)
        {
            SanPham sp;
            Button btn = (Button)sender;
            sp = btn.Tag as SanPham;
            btn.Enabled = false;
            btn.BackColor = Color.FromArgb(254, 245, 237);
            dgvDichVu.Rows.Add(sp.ID, sp.TenSP, 0, sp.GiaBan, sp.Loai);
        }

        private void UpdateCellValue(DataGridViewRow row, int colIndex, object newVal)
        {
            if (row != null && colIndex >= 0 && colIndex < row.Cells.Count)
            {
                DataGridViewCell cell = row.Cells[colIndex];
                cell.Value = newVal;
            }
            
        }

        private void numSoLuong_ValueChanged(object sender, EventArgs e)
        {
            HoaDonDVDAO dao = new HoaDonDVDAO();
            float GiaTien = 0;
            int Num = 0;
            string sp;
            DataGridViewRow currRow = dgvDichVu.CurrentRow;
            UpdateCellValue(currRow, 2, numSoLuong.Value);
            Num = Convert.ToInt32(dgvDichVu.CurrentRow.Cells[2].Value.ToString());
            sp = dgvDichVu.CurrentRow.Cells[1].Value.ToString();
            int check = dao.KiemTraHetHangTrongKho(sp, Num);
            if(check == 0)
            {
                MessageBox.Show("Bạn đang đặt sản phẩm với số lượng vượt quá trong kho!");
                numSoLuong.Value--;
                numSoLuong.Focus();
                return;
            }
        }

        private void dgvDichVu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string ColName = dgvDichVu.Columns[e.ColumnIndex].Name;
            int RowName = (int)dgvDichVu.Rows[e.RowIndex].Cells[0].Value;
            decimal num = decimal.Parse(dgvDichVu.CurrentRow.Cells[2].Value.ToString());
            numSoLuong.Value = num;
            if (ColName == "ColHuy")
            {
                numSoLuong.Value = 0;
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    dgvDichVu.Rows.RemoveAt(e.RowIndex);
                    foreach (Button btn in flpSanPham.Controls)
                    {
                        SanPham tmp = new SanPham();
                        tmp = btn.Tag as SanPham;
                        if (tmp.ID == RowName)
                        {
                            btn.Enabled = true;
                            btn.BackColor = Color.FromArgb(97, 135, 110);
                            btn.ForeColor = Color.WhiteSmoke;
                        }
                    }
                    foreach (Button btn in flpAo.Controls)
                    {
                        SanPham tmp = new SanPham();
                        tmp = btn.Tag as SanPham;
                        if (tmp.ID == RowName)
                        {
                            btn.Enabled = true;
                            btn.BackColor = Color.FromArgb(97, 135, 110);
                            btn.ForeColor = Color.WhiteSmoke;
                        }
                    }
                    foreach (Button btn in flpGiay.Controls)
                    {
                        SanPham tmp = new SanPham();
                        tmp = btn.Tag as SanPham;
                        if (tmp.ID == RowName)
                        {
                            btn.Enabled = true;
                            btn.BackColor = Color.FromArgb(97, 135, 110);
                            btn.ForeColor = Color.WhiteSmoke;
                        }
                    }
                }
            }
        }

        private void btnHoaDon_Click(object sender, EventArgs e)
        {
            pnlDichVu.SendToBack();
            pnlHoaDonDV.BringToFront();
        }

        private void btnDichVu_Click(object sender, EventArgs e)
        {
            pnlDichVu.BringToFront();
            pnlHoaDonDV.SendToBack();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            int ID, check = 0;
            HoaDonDV hd = new HoaDonDV();
            HoaDonDVDAO hdon = new HoaDonDVDAO();
            if (cboxKhach.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng chọn khách hàng!");
                cboxKhach.Focus();
                return;
            }
            else
            {
                try
                {
                    hd.Khach = cboxKhach.Text;
                    hd.NgayXuat = dtpNgayXuat.Text;
                    hdon.InsertHoaDonDV(hd);
                    ID = hdon.GetIDHoaDonDV();
                    foreach (DataGridViewRow row in dgvDichVu.Rows)
                    {
                        HoaDonDV tmp = new HoaDonDV();
                        tmp.ID = ID;
                        tmp.SanPham = row.Cells[1].Value.ToString();
                        tmp.SoLuong = Int32.Parse(row.Cells[2].Value.ToString());
                        tmp.GiaTien = float.Parse(row.Cells[3].Value.ToString());
                        tmp.TongTien = tmp.GiaTien * tmp.SoLuong;

                        hdon.InsertChiTietHDDV(tmp);
                    }
                    MessageBox.Show("Đặt thành công!");
                    LoadHoaDonDV();
                }
                catch (Exception ex) { throw ex; }
                
            }
            ClearDichVu();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ClearDichVu();
        }

        private void LoadHoaDonDV()
        {
            int i = 0;
            dgvHoaDonDV.Rows.Clear();
            List<HoaDonDV> list = new List<HoaDonDV>();
            HoaDonDVDAO dao = new HoaDonDVDAO();
            list = dao.GetHoaDonDVList();
            foreach(HoaDonDV tmp in list)
            {
                i++;
                dgvHoaDonDV.Rows.Add(tmp.ID, i, tmp.Khach, tmp.NgayXuat, tmp.TrangThai);
            }
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            HoaDonDVDAO hoaDon = new HoaDonDVDAO();
            int ID = Convert.ToInt32(dgvHoaDonDV.CurrentRow.Cells[0].Value.ToString());
            string TrangThai = dgvHoaDonDV.CurrentRow.Cells[4].Value.ToString();
            hoaDon.ThanhToanHoaDon(ID, TrangThai);
            LoadHoaDonDV();
        }

        private void btnReset2_Click(object sender, EventArgs e)
        {
            dgvHoaDonDV.Rows.Clear();
            dgvChiTietHD.Rows.Clear();
            LoadHoaDonDV();
            cboxKhach2.Text = string.Empty;
            lblIDChiTiet.Text = string.Empty;
            lblKhachCT.Text = string.Empty;
            lblNgayXuatCT.Text = string.Empty;
            lblTrangThai.Text = string.Empty;
            lblTongTienCT.Text = string.Empty;
        }

        private void cboxKhach2_TextChanged(object sender, EventArgs e)
        {
            int i = 0;
            dgvHoaDonDV.Rows.Clear();
            HoaDonDVDAO hd = new HoaDonDVDAO();
            List<HoaDonDV> list = new List<HoaDonDV>();
            string khach = cboxKhach2.Text;
            list = hd.GetKhachHoaDon(khach);
            foreach (HoaDonDV item in list)
            {
                i++;
                dgvHoaDonDV.Rows.Add(item.ID, i, item.Khach, item.NgayXuat, item.TrangThai);
            }
        }

        private void dgvHoaDonDV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string ColName = dgvHoaDonDV.Columns[e.ColumnIndex].Name;
            Database.ID_HoaDonDV = Convert.ToInt32(dgvHoaDonDV.CurrentRow.Cells[0].Value.ToString());
            HoaDonDVDAO hoaDon = new HoaDonDVDAO();
            if(ColName == "ColChiTiet")
            {
                int i = 0;
                float TongTien = 0;
                dgvChiTietHD.Rows.Clear();
                List<HoaDonDV> list = new List<HoaDonDV>();
                Database.ID_HoaDonDV = Convert.ToInt32(dgvHoaDonDV.CurrentRow.Cells[0].Value.ToString());
                lblIDChiTiet.Text = dgvHoaDonDV.CurrentRow.Cells[0].Value.ToString();
                lblKhachCT.Text = dgvHoaDonDV.CurrentRow.Cells[2].Value.ToString();
                lblNgayXuatCT.Text = dgvHoaDonDV.CurrentRow.Cells[3].Value.ToString();
                lblTrangThai.Text = dgvHoaDonDV.CurrentRow.Cells[4].Value.ToString();
                list = hoaDon.GetChiTietHoaDon(Database.ID_HoaDonDV);
                foreach (HoaDonDV item in list)
                {
                    i++;
                    dgvChiTietHD.Rows.Add(i, item.SanPham, item.SoLuong, item.GiaTien);
                    TongTien += item.GiaTien;
                }
                lblTongTienCT.Text = TongTien.ToString() + " VND";
            }
        }

        private void InHoaDonDV_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int count = 0;
            float Tong = 0;
            HoaDonDVDAO hd = new HoaDonDVDAO();
            List<HoaDonDV> list = new List<HoaDonDV>();
            HoaDonDV hDon = hd.GetThongTinHoaDonDV(Database.ID_HoaDonDV);
            list = hd.GetChiTietHoaDon(Database.ID_HoaDonDV);
            if(hDon.NgayXuat == null || hDon.Khach == null || hDon.Sdt == null)
            {
                hDon.NgayXuat = "";
                hDon.Khach = "";
                hDon.Sdt = "";
            }
                
            e.PageSettings.PaperSize = new PaperSize("Custome Size", 500, 700);
            e.Graphics.DrawString("HÓA ĐƠN DỊCH VỤ", new Font("Calibri", 50, FontStyle.Bold), Brushes.Black, new Point(150, 10));
            for (int i = 20; i <= 810; i += 10)
            {
                e.Graphics.DrawString("_", new Font("Calibri", 16, FontStyle.Bold), Brushes.Black, new Point(i, 80));
                e.Graphics.DrawString("_", new Font("Calibri", 16, FontStyle.Bold), Brushes.Black, new Point(i, 375));
                e.Graphics.DrawString("-", new Font("Calibri", 14, FontStyle.Bold), Brushes.Black, new Point(i, 430));
                e.Graphics.DrawString("-", new Font("Calibri", 14, FontStyle.Bold), Brushes.Black, new Point(i, 800));
                e.Graphics.DrawString("_", new Font("Calibri", 16, FontStyle.Bold), Brushes.Black, new Point(i, 857));
            }
            foreach (HoaDonDV tmp in list)
            {
                e.Graphics.DrawString(tmp.SanPham, new Font("Calibri", 22, FontStyle.Bold), Brushes.Black, new Point(45, 455 + count * 35));
                e.Graphics.DrawString(tmp.SoLuong.ToString() , new Font("Calibri", 22, FontStyle.Bold), Brushes.Black, new Point(500, 455 + count * 35));
                e.Graphics.DrawString(tmp.GiaTien.ToString() , new Font("Calibri", 22, FontStyle.Bold), Brushes.Black, new Point(670, 455 + count * 35));
                Tong += (tmp.GiaTien);
                count++;
            }
            e.Graphics.DrawString("Hóa đơn #" + Database.ID_HoaDonDV.ToString(), new Font("Calibri", 30, FontStyle.Bold), Brushes.Black, new Point(80, 130));
            e.Graphics.DrawString("Ngày Xuất: " + hDon.NgayXuat.ToString(), new Font("Calibri", 30, FontStyle.Regular), Brushes.Black, new Point(80, 200));
            e.Graphics.DrawString("Khách hàng: " + hDon.Khach.ToString(), new Font("Calibri", 30, FontStyle.Regular), Brushes.Black, new Point(80, 260));
            e.Graphics.DrawString("Số điện thoại: " + hDon.Sdt.ToString(), new Font("Calibri", 30, FontStyle.Regular), Brushes.Black, new Point(80, 320));
            e.Graphics.DrawString("Sản Phẩm", new Font("Calibri", 22, FontStyle.Regular), Brushes.Black, new Point(45, 405));
            e.Graphics.DrawString("Số Lượng", new Font("Calibri", 22, FontStyle.Regular), Brushes.Black, new Point(450, 405));
            e.Graphics.DrawString("Giá Tiền", new Font("Calibri", 22, FontStyle.Regular), Brushes.Black, new Point(660, 405));

            e.Graphics.DrawString("Tổng tiền: " + Tong.ToString() + " VND", new Font("Calibri", 30, FontStyle.Bold), Brushes.Black, new Point(420, 820));
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = InHoaDonDV;
            printPreviewDialog1.Name = "Hóa Đơn Dịch Vụ";
            printPreviewDialog1.ShowDialog();
        }

        private void pnlDichVu_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
