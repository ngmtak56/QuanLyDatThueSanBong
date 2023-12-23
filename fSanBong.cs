using QuanLyDatThueSanBongNhanTao.DTO;
using QuanLyDatThueSanBongNhanTao.DAO;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System;
using System.Drawing.Printing;

namespace QuanLyDatThueSanBongNhanTao
{
    public partial class fSanBong : Form
    {
        public fSanBong()
        {
            InitializeComponent();
            LoadSanBong();
            LoadDatSan();
            LoadCboxSan();
            LoadCboxKhach();
            LoadSanBongDatSan();
            LoadHoaDon();
            LoadTrongTai();
            dtpNgayDat.Text = string.Empty;
            dtpNgayLap.Text = string.Empty;
            checkNgay.Checked = true;
            pnlNhapSan.BringToFront();
            pnlHienThiSan.BringToFront();
        }

        private SanBong sanBong = new SanBong();

        private void LoadSanBong()
        {
            int i = 0;
            dgvSanBong.Rows.Clear();
            SanBongDAO sb = new SanBongDAO();
            List<SanBong> listSan = new List<SanBong>();
            listSan = sb.GetSanBongList();
            foreach (SanBong item in listSan)
            {
                i++;
                dgvSanBong.Rows.Add(item.ID, i, item.TenSan, item.DiaChi, item.SoLuong, item.GiaThue, item.TrangThai);
            }
        }

        public void CLearSan()
        {
            sanBong.ID = 0;
            sanBong.TenSan = string.Empty;
            sanBong.DiaChi = string.Empty;
            sanBong.SoLuong = 0;
            sanBong.GiaThue = 0;
            sanBong.TrangThai = string.Empty;
        }

        public void GetSanText()
        {
            CLearSan();
            sanBong.TenSan = txtTenSan.Text;
            sanBong.DiaChi = txtDiaChiSan.Text;
            sanBong.SoLuong = Int32.Parse(txtSoLuong.Text);
            sanBong.GiaThue = Int32.Parse(txtGiaThue.Text);
            sanBong.TrangThai = cboxTrangThai.Text;
        }

        private void ClearSanText()
        {
            txtTenSan.Clear();
            txtDiaChiSan.Clear();
            txtSoLuong.Clear();
            txtGiaThue.Clear();
            txtTimKiem.Clear();
            cboxTrangThai.Text = string.Empty;
            txtTenSan.Enabled = true;
            txtDiaChiSan.Enabled = true;
            txtGiaThue.Enabled = true;
            cboxTrangThai.Enabled = true;
        }

        private void ResetSan()
        {
            ClearSanText();
            btnCapNhat.Enabled = false;
            btnThem.Enabled = true;
            btnTimKiemSan.Enabled = true;
            LoadSanBong();
        }

        private void fSanBong_Load(object sender, EventArgs e)
        {

        }

        private void btnDatSan_Click(object sender, EventArgs e)
        {
            pnlHienThiDatSan.Visible = true;
            pnlHienThiSan.Visible = false;
            pnlHienThiHoaDon.Visible = false;
            pnlHienThiVeThang.Visible = false;
            pnlNhapDatSan.Visible = true;
            pnlNhapHoaDon.Visible = false;
            pnlNhapSan.Visible = false;
            pnlVeThang.Visible = false;
            pnlNhapDatSan.BringToFront();
        }

        private void btnHoaDon_Click(object sender, EventArgs e)
        {
            pnlHienThiSan.Visible = false;
            pnlHienThiDatSan.Visible = false;
            pnlHienThiHoaDon.Visible = true;
            pnlHienThiVeThang.Visible = false;
            pnlNhapSan.Visible = false;
            pnlNhapDatSan.Visible = false;
            pnlNhapHoaDon.Visible = true;
            pnlVeThang.Visible = false;
            pnlNhapHoaDon.BringToFront();
        }

        private void btnSanBong_Click(object sender, EventArgs e)
        {
            pnlHienThiSan.Visible = true;
            pnlHienThiDatSan.Visible = false;
            pnlHienThiHoaDon.Visible = false;
            pnlHienThiVeThang.Visible = false;
            pnlNhapSan.Visible = true;
            pnlNhapDatSan.Visible = false;
            pnlNhapHoaDon.Visible = false;
            pnlVeThang.Visible = false;
            pnlNhapSan.BringToFront();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTenSan.Text)
                || string.IsNullOrEmpty(txtDiaChiSan.Text)
                || txtGiaThue.Text == "0"
                || string .IsNullOrEmpty(txtSoLuong.Text)
                || string.IsNullOrEmpty(cboxTrangThai.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                txtTenSan.Focus();
                return;
            }
            else if (txtGiaThue.Text.Length <= 5 || txtGiaThue.Text.Length >= 7)
            {
                MessageBox.Show("Giá thuê có độ dài 6 chữ số!");
                txtGiaThue.Focus();
                return;
            }
            GetSanText();
            SanBong San = new SanBong(sanBong);
            fDatSan datSan = new fDatSan();
            SanBongDAO.ThemSanBong(San);
            ClearSanText();
            LoadSanBong();
            LoadCboxSan();
            LoadSanBongDatSan();
        }

        private void btnTimKiemSan_Click(object sender, EventArgs e)
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
                dgvSanBong.Rows.Clear();
                SanBongDAO sb = new SanBongDAO();
                List<SanBong> listSan = new List<SanBong>();
                listSan = sb.TimKiemSan(txtTimKiem.Text);
                foreach (SanBong item in listSan)
                {
                    i++;
                    dgvSanBong.Rows.Add(item.ID, i, item.TenSan, item.DiaChi, item.SoLuong, item.GiaThue, item.TrangThai);
                }
                CLearSan();
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            GetSanText();
            SanBong San = new SanBong(sanBong);
            SanBongDAO.CapNhatSan(San);
            ClearSanText();
            btnCapNhat.Enabled = false;
            btnThem.Enabled = true;
            btnTimKiemSan.Enabled = true;
            LoadSanBong();
            LoadCboxSan();
            LoadSanBongDatSan();
        }

        private void dgvSanBong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string ColName = dgvSanBong.Columns[e.ColumnIndex].Name;
            if (ColName == "ColSua")
            {
                //ResetSan();
                if ((MessageBox.Show("Bạn có muốn sửa thông tin sân bóng này?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes))
                {
                    Database.ID = (int)dgvSanBong.CurrentRow.Cells[0].Value;
                    txtTenSan.Text = dgvSanBong.CurrentRow.Cells[2].Value.ToString();
                    txtDiaChiSan.Text = dgvSanBong.CurrentRow.Cells[3].Value.ToString();
                    txtSoLuong.Text = dgvSanBong.CurrentRow.Cells[4].Value.ToString();
                    txtGiaThue.Text = dgvSanBong.CurrentRow.Cells[5].Value.ToString();
                    cboxTrangThai.Text = dgvSanBong.CurrentRow.Cells[6].Value.ToString();
                    btnCapNhat.Enabled = true;
                    btnThem.Enabled = false;
                    txtTenSan.Select();
                }
            }
            else if (ColName == "ColXoa")
            {
                if (MessageBox.Show("Xóa sân bóng này khỏi danh sách ?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    CLearSan();
                    sanBong.ID = (int)dgvSanBong.CurrentRow.Cells[0].Value;
                    SanBongDAO.XoaSan(sanBong);
                    MessageBox.Show("Sân bóng đã xóa thành công!");
                    ClearSanText();
                    LoadSanBong();
                    LoadCboxSan();
                    LoadSanBongDatSan();
                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetSan();
        }

        private void txtTenSan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDiaChiSan.Focus();
                e.Handled = true;
            }
        }

        private void txtDiaChiSan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSoLuong.Focus();
                e.Handled = true;
            }
        }

        private void txtSoLuong_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtGiaThue.Focus();
                e.Handled = true;
            }
        }

        private void txtGiaThue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cboxTrangThai.Focus();
                e.Handled = true;
            }
        }

        private void cboxTrangThai_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnThem.Select();
                e.Handled = true;
            }
        }


        //================================================================================
        //                                   Dat San
        //================================================================================
        private int SoBuoi = 0;
        private PhieuDatSan datSan = new PhieuDatSan();
        public void LoadSanBongDatSan()
        {
            SanBongDAO sb = new SanBongDAO();
            List<SanBong> listSan = new List<SanBong>();
            listSan = sb.GetSanBongList();
            flpSanBong.Controls.Clear();
            foreach (SanBong san in listSan)
            {
                Button btn = new Button();
                {
                    btn.Width = 100;
                    btn.Height = 60;
                }
                btn.Text = san.TenSan;
                btn.Click += btn_Click;
                btn.Tag = san;

                switch (san.TrangThai)
                {
                    case "Còn trống":
                        btn.BackColor = Color.FromArgb(254, 245, 237);
                        break;
                    default:
                        btn.BackColor = Color.FromArgb(97, 135, 110);
                        btn.ForeColor = Color.WhiteSmoke;
                        break;
                }
                flpSanBong.Controls.Add(btn);
            }
        }

        private void LoadTrongTai()
        {
            KhachHangDAO ttai = new KhachHangDAO();
            List<KhachHang> list = new List<KhachHang>();
            list = ttai.GetTrongTaiList();
            foreach (KhachHang tt in list)
            {
                cboxTrongTai.Items.Add(tt.TenKhach);
            }
        }

        private void btn_Click(object sender, EventArgs e)
        {
            flpSanNho.Controls.Clear();
            int sanID = ((sender as Button).Tag as SanBong).ID;
            cboxSanBong.Text = (sender as Button).Text;
            ShowSanNho(sanID);
        }

        private void ShowSanNho(int ID)
        {
            SanNhoDAO san = new SanNhoDAO();
            List<SanNho> list = new List<SanNho>();
            list = san.GetSanNho(ID);
            foreach (SanNho tmp in list)
            {
                Button btn = new Button();
                {
                    btn.Width = 80;
                    btn.Height = 60;
                }
                btn.Text = tmp.SoSan;
                btn.Click += btnSanNho_Click;
                btn.Tag = tmp;
                btn.BackColor = Color.FromArgb(97, 135, 110);
                btn.ForeColor = Color.WhiteSmoke;

                flpSanNho.Controls.Add(btn);
            }
        }

        private void VisibleKhungGio(bool check)
        {
            lbl7_9.Visible = check;
            lbl9_11.Visible = check;
            lbl14_16.Visible = check;
            lbl16_18.Visible = check;
            lbl18_20.Visible = check;
            lbl20_22.Visible = check;
        }

        private void ClearKhungGio()
        {
            lbl_7_9.Text = string.Empty;
            lbl_9_11.Text = string.Empty;
            lbl_14_16.Text = string.Empty;
            lbl_16_18.Text = string.Empty;
            lbl_18_20.Text = string.Empty;
            lbl_20_22.Text = string.Empty;
        }

        private void ShowKhungGio(int ID)
        {
            string date = dtpNgayDat.Value.ToString();
            KhungGioSan gio = new KhungGioSan();
            VisibleKhungGio(true);
            ClearKhungGio();
            gio = KhungGioSanDAO.GetKhungGioSan(ID, date);
            lbl_7_9.Text = gio.h7_9.ToString();
            lbl_9_11.Text = gio.h9_11.ToString();
            lbl_14_16.Text = gio.h14_16.ToString();
            lbl_16_18.Text = gio.h16_18.ToString();
            lbl_18_20.Text = gio.h18_20.ToString();
            lbl_20_22.Text = gio.h20_22.ToString();
        }

        private void btnSanNho_Click(object sender, EventArgs e)
        {
            string date = dtpNgayDat.Value.ToString();
            int sanID = ((sender as Button).Tag as SanNho).ID;
            datSan.ID = sanID;
            txtSanNho.Text = (sender as Button).Text;
            ShowKhungGio(sanID);
        }

        private void LoadDatSan()
        {
            int i = 0;
            dgvDatSan.Rows.Clear();
            PhieuDatSanDAO pds = new PhieuDatSanDAO();
            List<PhieuDatSan> list = new List<PhieuDatSan>();
            list = pds.GetDatSanList();
            foreach (PhieuDatSan item in list)
            {
                i++;
                dgvDatSan.Rows.Add(item.ID, i, item.TenSan, item.SanNho, item.TenKhach, item.NgayDat, item.KhungGio, item.GhiChu);
            }
        }

        public void LoadCboxSan()
        {
            cboxSanBong.Items.Clear();
            PhieuDatSanDAO pds = new PhieuDatSanDAO();
            List<SanBong> sans = new List<SanBong>();
            sans = pds.GetComboBoxSan();
            foreach (SanBong san in sans)
            {
                cboxSanBong.Items.Add(san.TenSan);
                cboxSan_VT.Items.Add(san.TenSan);
            }
        }

        public void LoadCboxKhach()
        {
            cboxKhachHang.Items.Clear();
            PhieuDatSanDAO pds = new PhieuDatSanDAO();
            List<KhachHang> khachs = new List<KhachHang>();
            khachs = pds.GetComboBoxKhach();
            foreach (KhachHang khach in khachs)
            {
                cboxKhachHang.Items.Add(khach.TenKhach);
                cboxKhach_VT.Items.Add(khach.TenKhach);
            }
        }

        private void ClearDatSan()
        {
            datSan = new PhieuDatSan();
            SoBuoi = 0;
        }

        private void ClearChiTietSan()
        {
            VisibleKhungGio(false);
            lbl_7_9.Text = string.Empty;
            lbl_9_11.Text = string.Empty;
            lbl_14_16.Text = string.Empty;
            lbl_16_18.Text = string.Empty;
            lbl_18_20.Text = string.Empty;
            lbl_20_22.Text = string.Empty;
        }

        private void ClearDatSanText()
        {
            cboxSanBong.Text = string.Empty;
            cboxKhachHang.Text = string.Empty;
            txtSanNho.Text = string.Empty;
            dtpNgayDat.Text = string.Empty;
            cboxKhungGio.Text = string.Empty;
            txtGhiChu.Text = string.Empty;
            checkNgay.Checked = false;
            checkThang.Checked = false;
        }

        private void GetDatSanText()
        {
            ClearDatSan();
            datSan.TenSan = cboxSanBong.Text;
            datSan.SanNho = txtSanNho.Text;
            datSan.TenKhach = cboxKhachHang.Text;
            datSan.NgayDat = dtpNgayDat.Value.ToString();
            Database.time = dtpNgayDat.Value;
            SoBuoi = Convert.ToInt32(numSoBuoi.Value);
            datSan.KhungGio = cboxKhungGio.Text;
            if (checkTrongTai.Checked == false) datSan.TrongTai = "Không";
            else datSan.TrongTai = cboxTrongTai.Text;
            datSan.GhiChu = txtGhiChu.Text;
        }

        private void dgvDatSan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string ColName = dgvDatSan.Columns[e.ColumnIndex].Name;
            if (ColName == "ColHuyPDS")
            {
                if (MessageBox.Show("Xóa phiếu đặt sân này khỏi danh sách ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    ClearDatSan();
                    datSan.ID = (int)dgvDatSan.CurrentRow.Cells[0].Value;
                    PhieuDatSanDAO.HuyPhieuDatSan(datSan);
                    MessageBox.Show("Hủy đặt sân thành công");
                    LoadDatSan();
                }
            }
            else if (ColName == "ColHoaDon")
            {
                ClearDatSan();
                string TrangThai;
                datSan.ID = (int)dgvDatSan.CurrentRow.Cells[0].Value;
                TrangThai = PhieuDatSanDAO.CheckHoaDonDatSan(datSan);
                if (TrangThai == "Đã thanh toán")
                {
                    MessageBox.Show("Hóa đơn này đã thanh toán");
                    return;
                }
                else if (TrangThai == "Chưa thanh toán")
                {
                    MessageBox.Show("Hóa đơn này chưa thanh toán");
                    return;
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            int check = 0;
            if (cboxSanBong.Text == string.Empty
                || cboxKhachHang.Text == string.Empty
                || dtpNgayDat.Value == null
                || cboxKhungGio.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }
            else if (txtGhiChu.Text == string.Empty)
            {
                txtGhiChu.Text = "";
            }
            /*check = PhieuDatSanDAO.CheckTrungLapDatSan(dtpNgayDat.Value.ToString(), cboxKhungGio.Text);
            if (check != 0)
            {
                MessageBox.Show("Ngày đặt sân vào khung giờ này không còn trống! \nVui lòng chọn khung giờ hoặc ngày");
                cboxKhungGio.Focus();
                return;
            }*/
            if(checkTuan.Checked == true || checkThang.Checked == true)
            {
                if(numSoBuoi.Value == 0)
                {
                    MessageBox.Show("Vui lòng nhập số buổi thuê sân!");
                    numSoBuoi.Focus();
                    return;
                }
            }
            GetDatSanText();
            if (checkNgay.Checked == true)
            {
                PhieuDatSanDAO.ThemPhieuDatSan(datSan, 1, 0);
            }
            else if(checkTuan.Checked == true)
            {
                PhieuDatSanDAO.ThemPhieuDatSan(datSan, 2, SoBuoi);  
            }
            else if(checkThang.Checked == true)
            {
                PhieuDatSanDAO.ThemPhieuDatSan(datSan, 3, SoBuoi);
            }
            LoadDatSan();
            LoadHoaDon();
            ClearDatSan();
            ClearDatSanText();
            ClearChiTietSan();
            flpSanNho.Controls.Clear();
        }

        private void btnResetDatSan_Click(object sender, EventArgs e)
        {
            ClearDatSan();
            ClearDatSanText();
            ClearChiTietSan();
            flpSanNho.Controls.Clear();
        }

        private void checkNgay_CheckedChanged(object sender, EventArgs e)
        {
            if (checkNgay.Checked == true)
            {
                checkThang.Checked = false;
                checkTuan.Checked = false;
            }
        }

        private void checkTuan_CheckedChanged(object sender, EventArgs e)
        {
            if (checkTuan.Checked == true)
            {
                checkNgay.Checked = false;
                checkThang.Checked = false;
            }
        }

        private void checkThang_CheckedChanged(object sender, EventArgs e)
        {
            if(checkThang.Checked == true)
            {
                checkNgay.Checked = false;
                checkTuan.Checked = false;
            }
        }

        private void checkTrongTai_CheckedChanged(object sender, EventArgs e)
        {
            if (checkTrongTai.Checked == true)
                 cboxTrongTai.Enabled = true;
            else cboxTrongTai.Enabled = false;
        }

        //================================================================================
        //                                   Hoa Don
        //================================================================================

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
                dgvHoaDon.Rows.Add(item.ID, i, item.TenKhach, item.TenSan, item.SoSan, item.Ngaylap, item.TrangThai);
            }
        }

        private void ClearHoaDon()
        {
            txtTenSan.Text = string.Empty;
            txtKhachHD.Text = string.Empty;
            dtpNgayLap.Text = string.Empty;
            cboxTrangThaiHD.Text = string.Empty;
        }

        private void ClearHoaDonText()
        {
            txtKhachHD.Text = string.Empty;
            txtTenSan.Text = string.Empty;
            dtpNgayLap.Text = string.Empty;
            cboxTrangThai.Text = string.Empty;
        }

        private void EnableBtnCapNhatHoaDon(bool enable)
        {
            btnCapNhatHD.Enabled = enable;
            btnIn.Enabled = !enable;
        }

        private void ResetHoaDon()
        {
            ClearHoaDon();
            ClearHoaDonText();
            btnIn.Enabled = true;
            EnableBtnCapNhatHoaDon(false);
            LoadHoaDon();
        }

        private void GetHoaDonText()
        {
            hDon = new HoaDon();
            hDon.TenKhach = txtKhachHD.Text;
            hDon.TenSan = txtTenSan.Text;
            hDon.Ngaylap = dtpNgayLap.Text;
            hDon.TrangThai = cboxTrangThai.Text;
        }

        private void dgvHoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string ColName = dgvHoaDon.Columns[e.ColumnIndex].Name;
            Database.ID_HoaDon = (int)dgvHoaDon.CurrentRow.Cells[0].Value;
            txtKhachHD.Text = dgvHoaDon.CurrentRow.Cells[2].Value.ToString();
            txtSanHD.Text = dgvHoaDon.CurrentRow.Cells[3].Value.ToString();
            txtSanNhoHD.Text = dgvHoaDon.CurrentRow.Cells[4].Value.ToString();
            dtpNgayLap.Text = dgvHoaDon.CurrentRow.Cells[5].Value.ToString();
            cboxTrangThaiHD.Text = dgvHoaDon.CurrentRow.Cells[6].Value.ToString();
            if (ColName == "ColSuaHD")
            {
                if (MessageBox.Show("Bạn muốn cập nhật hóa đơn này?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    DateTime dateVal;
                    string date = dgvHoaDon.CurrentRow.Cells[5].Value.ToString();

                    txtKhachHD.Text = dgvHoaDon.CurrentRow.Cells[2].Value.ToString();
                    txtSanHD.Text = dgvHoaDon.CurrentRow.Cells[3].Value.ToString();
                    if (DateTime.TryParse(date, out dateVal))
                        dtpNgayLap.Value = dateVal;
                    cboxTrangThaiHD.Text = dgvHoaDon.CurrentRow.Cells[6].Value.ToString();
                    Database.ID_HoaDon = (int)dgvHoaDon.CurrentRow.Cells[0].Value;
                    EnableBtnCapNhatHoaDon(true);
                }
            }
            else if (ColName == "ColXoaHoaDon")
            {
                if (MessageBox.Show("Xóa hóa đơn này khỏi danh sách?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    hDon = new HoaDon();
                    Database.ID_HoaDon = (int)dgvHoaDon.CurrentRow.Cells[0].Value;
                    HoaDonDAO.XoaHoaDon(hDon);
                    ClearHoaDonText();
                    LoadHoaDon();
                }
            }
        } 

        private void btnIn_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = InHoaDon;
            printPreviewDialog1.Name = "Hóa Đơn";
            printPreviewDialog1.ShowDialog();
        }

        private void InHoaDon_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            hDon = new HoaDon();
            HoaDonDAO hd = new HoaDonDAO();
            hDon = hd.GetChiTietHoaDon(Database.ID_HoaDon);
            e.PageSettings.PaperSize = new PaperSize("Custome Size", 500, 700);
            e.Graphics.DrawString("HÓA ĐƠN ĐẶT SÂN", new Font("Calibri", 40, FontStyle.Bold), Brushes.Black, new Point(200, 10));
            for (int i = 0; i <= 1000; i += 10)
            {
                e.Graphics.DrawString("_", new Font("Calibri", 16, FontStyle.Bold), Brushes.Black, new Point(i, 60));
                e.Graphics.DrawString("-", new Font("Calibri", 14, FontStyle.Bold), Brushes.Black, new Point(i, 380));
                e.Graphics.DrawString("-", new Font("Calibri", 14, FontStyle.Bold), Brushes.Black, new Point(i, 580));
                e.Graphics.DrawString("-", new Font("Calibri", 14, FontStyle.Bold), Brushes.Black, new Point(i, 660));
                e.Graphics.DrawString("-", new Font("Calibri", 14, FontStyle.Bold), Brushes.Black, new Point(i, 820));
            }
            e.Graphics.DrawString("Hóa đơn No." + Database.ID_HoaDon.ToString(), new Font("Calibri", 22, FontStyle.Bold), Brushes.Black, new Point(100, 130));
            e.Graphics.DrawString("Ngày Xuất: " + hDon.Ngaylap, new Font("Calibri", 22, FontStyle.Regular), Brushes.Black, new Point(100, 170));
            e.Graphics.DrawString("Khách hàng: " + hDon.TenKhach, new Font("Calibri", 22, FontStyle.Regular), Brushes.Black, new Point(100, 210));
            e.Graphics.DrawString("Số điện thoại: " + hDon.Sdt, new Font("Calibri", 22, FontStyle.Regular), Brushes.Black, new Point(100, 250));
            e.Graphics.DrawString("Đặt Sân", new Font("Calibri", 34, FontStyle.Regular), Brushes.Black, new Point(350, 320));
            e.Graphics.DrawString("Sân bóng: " + hDon.TenSan, new Font("Calibri", 22, FontStyle.Regular), Brushes.Black, new Point(100, 420));
            e.Graphics.DrawString("Địa Chỉ: " + hDon.DiaChi, new Font("Calibri", 22, FontStyle.Regular), Brushes.Black, new Point(100, 460));
            e.Graphics.DrawString("Sân số: " + hDon.SoSan, new Font("Calibri", 22, FontStyle.Regular), Brushes.Black, new Point(100, 500));
            e.Graphics.DrawString("Khung giờ: " + hDon.KhungGio, new Font("Calibri", 22, FontStyle.Regular), Brushes.Black, new Point(100, 540));
            e.Graphics.DrawString("Thuê trọng tài: " + hDon.TrongTai, new Font("Calibri", 22, FontStyle.Regular), Brushes.Black, new Point(100, 620));
            e.Graphics.DrawString("Giá thuê sân bóng: " + hDon.GiaThue.ToString() + " VND", new Font("Calibri", 22, FontStyle.Regular), Brushes.Black, new Point(100, 700));
            e.Graphics.DrawString("Giá thuê trọng tài: " + hDon.GiaThueTT.ToString() + " VND", new Font("Calibri", 22, FontStyle.Regular), Brushes.Black, new Point(100, 740));
            e.Graphics.DrawString("Tổng tiền: " + hDon.TongTien.ToString() + " VND", new Font("Calibri", 25, FontStyle.Bold), Brushes.Black, new Point(100, 780));
        }

        private void btnResetHD_Click(object sender, EventArgs e)
        {
            ResetHoaDon();
        }

        private void btnCapNhatHD_Click(object sender, EventArgs e)
        {
            hDon.TrangThai = cboxTrangThaiHD.Text;
            HoaDonDAO.CapNhatHoaDon(hDon);
            ClearHoaDonText();
            EnableBtnCapNhatHoaDon(false);
            LoadHoaDon();
        }

        private void btnTimKiemHD_Click(object sender, EventArgs e)
        {
            int i = 0;
            if (txtTimKiemHD.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng nhập thông tin tìm kiếm!");
                txtTimKiemHD.Focus();
                return;
            }
            else
            {
                dgvHoaDon.Rows.Clear();
                HoaDonDAO hd = new HoaDonDAO();
                List<HoaDon> list = new List<HoaDon>();
                list = hd.TimKiemHoaDon(txtTimKiemHD.Text);
                foreach (HoaDon item in list)
                {
                    i++;
                    dgvHoaDon.Rows.Add(item.ID, i, item.TenKhach, item.TenSan, item.SoSan, item.Ngaylap, item.TrangThai);
                }
                CLearSan();
            }
        }

        private void txtTenKhach_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSanHD.Focus();
                e.Handled = true;
            }
        }

        private void txtSanHD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dtpNgayLap.Focus();
                e.Handled = true;
            }
        }

        private void dtpNgayLap_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cboxTrangThai.Focus();
                e.Handled = true;
            }
        }

        private void cboxTrangThaiHD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnCapNhatHD.Select();
                e.Handled = true;
            }
        }


        //================================================================================
        //                                   Ve Thang
        //================================================================================

        private void btnVeThang_Click(object sender, EventArgs e)
        {
            pnlHienThiSan.Visible = false;
            pnlHienThiDatSan.Visible = false;
            pnlHienThiHoaDon.Visible = false;
            pnlHienThiVeThang.Visible = true;
            pnlNhapSan.Visible = false;
            pnlNhapDatSan.Visible = false;
            pnlNhapHoaDon.Visible = false;
            pnlVeThang.Visible = true;
            pnlVeThang.BringToFront();
        }

        private void LoadVeThang()
        {
            int i = 0;
            dgvVeThang.Rows.Clear();
            PhieuDatSanDAO pds = new PhieuDatSanDAO();
            List<PhieuDatSan> list = new List<PhieuDatSan>();
            list = pds.GetVeThanglist();
            foreach (PhieuDatSan item in list)
            {
                i++;
                dgvVeThang.Rows.Add(item.ID, i, item.TenKhach, item.TenSan, item.SanNho, item.Thang, item.Nam, item.KhungGio, item.Thu, item.TrangThai);
            }
        }

        private void ClearVeThangText()
        {
            cboxSan_VT.Text = string.Empty;
            txtSanNho_VT.Text = string.Empty;
            cboxKhach_VT.Text = string.Empty;
            cboxThang_VT.Text = string.Empty;
            cboxKhungGio_VT.Text = string.Empty;
            cboxTrangThai_VT.Text = string.Empty;
        }

        private void GetVeThangText()
        {
            ClearDatSan();
            datSan.TenSan = cboxSan_VT.Text;
            datSan.SanNho =  txtSanNho_VT.Text;
            datSan.TenKhach = cboxKhach_VT.Text;
            datSan.Thang = cboxThang_VT.Text;
            datSan.KhungGio =  cboxKhungGio_VT.Text;
            datSan.TrangThai =  cboxTrangThai_VT.Text;
        }

        private void dgvVeThang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string ColName = dgvVeThang.Columns[e.ColumnIndex].Name;
            if (ColName == "ColSuaVT")
            {
                if ((MessageBox.Show("Bạn có muốn cập nhật vé tháng này?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes))
                {
                    ClearDatSan();
                    Database.ID_VeThang = (int)dgvVeThang.CurrentRow.Cells[0].Value;
                    cboxKhach_VT.Text = dgvVeThang.CurrentRow.Cells[2].Value.ToString();
                    cboxSan_VT.Text = dgvVeThang.CurrentRow.Cells[3].Value.ToString();
                    txtSanNho_VT.Text = dgvVeThang.CurrentRow.Cells[4].Value.ToString();
                    cboxThang_VT.Text = dgvVeThang.CurrentRow.Cells[5].Value.ToString();
                    cboxKhungGio_VT.Text = dgvVeThang.CurrentRow.Cells[7].Value.ToString();
                    cboxTrangThai_VT.Text = dgvVeThang.CurrentRow.Cells[8].Value.ToString();
                    btnCapNhat_VT.Enabled = true;
                    cboxSan_VT.Select();
                }
            }
            else if (ColName == "ColXoaVT")
            {
                if (MessageBox.Show("Bạn muốn xóa vé tháng này?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    ClearDatSan();
                    Database.ID_VeThang = (int)dgvVeThang.CurrentRow.Cells[0].Value;
                    PhieuDatSanDAO.DeleteVeThang(datSan);
                    MessageBox.Show("Vé tháng đã xóa thành công!");
                    ClearVeThangText();
                    LoadVeThang();
                }
            }
        }

        private void btnCapNhat_VT_Click(object sender, EventArgs e)
        {
            GetVeThangText();
            PhieuDatSanDAO.UpdateVeThang(datSan);
            ClearVeThangText();
            btnCapNhat_VT.Enabled = false;
            LoadVeThang();
        }

        private void btnReset_VT_Click(object sender, EventArgs e)
        {
            ClearVeThangText();
            btnCapNhat_VT.Enabled = false;
        }

        private void cboxKhungGio_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                if(checkNgay.Checked == true) { }
            }
        }

        private void cboxSanBong_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                txtSanNho.Focus();
                e.Handled = true;
            }
        }

        private void txtSanNho_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cboxKhachHang.Focus();
                e.Handled = true;
            }
        }

        private void dgvVeThang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dtpNgayDat_ValueChanged(object sender, EventArgs e)
        {
            
        }
    }
}


