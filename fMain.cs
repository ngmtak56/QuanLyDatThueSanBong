using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using QuanLyDatThueSanBongNhanTao.DTO;
using QuanLyDatThueSanBongNhanTao.DAO;


namespace QuanLyDatThueSanBongNhanTao
{
    public partial class fMain : Form
    {
        public fMain()
        {
            InitializeComponent();
            LoadSanBong();
            LoadKhachHang();
            LoadDatSan();
            LoadHoaDon();
        }

        private void fMain_Load(object sender, EventArgs e)
        {
            LoadCboxSan();
            LoadCboxKhach();
        }


        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hand, int wMsg, int wParam, int lParam);

        private void pnlTop_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_NCCALCSIZE = 0x0083;
            if(m.Msg== WM_NCCALCSIZE && m.WParam.ToInt32() == 1)
            {
                return;
            }
            base.WndProc(ref m);
        }

        private void LoadCboxSan()
        {
            cboxSanBong.Items.Clear();
            PhieuDatSanDAO pds = new PhieuDatSanDAO();
            List<SanBong> sans = new List<SanBong>();
            sans = pds.GetComboBoxSan();
            foreach(SanBong san in sans)
            {
                cboxSanBong.Items.Add(san.TenSan);
            }
        }

        private void LoadCboxKhach()
        {
            cboxKhachHang.Items.Clear();
            PhieuDatSanDAO pds = new PhieuDatSanDAO();
            List<KhachHang> khachs = new List<KhachHang>();
            khachs = pds.GetComboBoxKhach();
            foreach(KhachHang khach in khachs)
            {
                cboxKhachHang.Items.Add(khach.TenKhach);
            }
        }

        private void btnThuNho_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnPhongTo_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
                this.WindowState = FormWindowState.Maximized;
            else 
                this.WindowState = FormWindowState.Normal;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn đăng xuất ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        //----------------------------------------------------
        //______________________Sân Bóng______________________
        //
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
                dgvSanBong.Rows.Add(item.ID, i, item.TenSan, item.DiaChi, item.GiaThue, item.TrangThai);
            }
        }

        public void CLearSan()
        {
            sanBong.TenSan = string.Empty;
            sanBong.DiaChi = string.Empty;
            sanBong.GiaThue = 0;
            sanBong.TrangThai = string.Empty;
        }

        public void GetSanText()
        {
            CLearSan();
            sanBong.TenSan = txtTenSan.Text;
            sanBong.DiaChi = txtDiaChiSan.Text;
            sanBong.GiaThue = Int32.Parse(txtGiaThue.Text);
            sanBong.TrangThai = cboxTrangThai.Text;
        }

        private void ClearSanText()
        {
            txtTenSan.Clear();
            txtDiaChiSan.Clear();
            txtGiaThue.Clear();
            cboxTrangThai.Text = string.Empty;
            txtTenSan.Enabled = true;
            txtDiaChiSan.Enabled= true;
            txtGiaThue.Enabled = true;
            cboxTrangThai.Enabled = true;
            btnTK_ThucThiSan.Visible = false;
            VisableBtnTK_San(false);
        }

        private void ResetSan()
        {
            ClearSanText();
            btnCapNhat.Enabled = false;
            btnThem.Enabled = true;
            btnTimKiemSan.Enabled = true;
            btnTimKiemSan.BackColor = Color.LightGoldenrodYellow;
            LoadSanBong();
            LoadCboxSan();
        }

        private void VisableBtnTK_San(bool sw)
        {
            btnTK_TenSan.Visible = sw;
            btnTK_DiaChiSan.Visible = sw;
            btnTK_TrangThai.Visible = sw;
        }

        private void btnThem_Click (object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTenSan.Text)
                || string.IsNullOrEmpty(txtDiaChiSan.Text)
                || txtGiaThue.Text == "0"
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
            SanBongDAO.ThemSanBong(San);
            ClearSanText();
            LoadSanBong();
            LoadCboxSan();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetSan();
        }

        private void dgvSanBong_CellClick (object sender, DataGridViewCellEventArgs e)
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
                    txtGiaThue.Text = dgvSanBong.CurrentRow.Cells[4].Value.ToString();
                    cboxTrangThai.Text = dgvSanBong.CurrentRow.Cells[5].Value.ToString();
                    btnCapNhat.Enabled = true;
                    btnThem.Enabled = false;
                    btnTimKiemSan.Enabled = false;
                    txtTenSan.Select();
                }
            }
            else if (ColName == "ColXoa")
            {
                if (MessageBox.Show("Xóa sân bóng này khỏi danh sách ?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    SanBongDAO.XoaSan(dgvSanBong);
                    ClearSanText();
                    LoadSanBong();
                    LoadCboxSan();
                }
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
            btnTimKiemSan.Enabled=true;
            LoadSanBong();
            LoadCboxSan();
        }

        private void btnTK_TenSan_Click(object sender, EventArgs e)
        {
            ClearSanText();
            txtDiaChiSan.Enabled = false;
            txtGiaThue.Enabled = false;
            cboxTrangThai.Enabled = false;
            btnTK_ThucThiSan.Visible = true;
            btnTimKiemSan.BackColor = Color.LightGoldenrodYellow;
            VisableBtnTK_San(false);
            txtTenSan.Select();
            LoadSanBong();
        }

        private void btnTK_DiaChiSan_Click(object sender, EventArgs e)
        {
            ClearSanText();
            txtTenSan.Enabled = false;
            txtGiaThue.Enabled = false;
            cboxTrangThai.Enabled = false;
            btnTK_ThucThiSan.Visible = true;
            btnTimKiemSan.BackColor = Color.LightGoldenrodYellow;
            VisableBtnTK_San(false);
            txtDiaChiSan.Select();
            LoadSanBong();
        }

        private void btnTK_TrangThai_Click(object sender, EventArgs e)
        {
            ClearSanText();
            txtTenSan.Enabled = false;
            txtDiaChiSan.Enabled = false;
            txtGiaThue.Enabled = false;
            btnTK_ThucThiSan.Visible = true;
            btnTimKiemSan.BackColor = Color.LightGoldenrodYellow;
            VisableBtnTK_San(false);
            cboxTrangThai.Select();
            LoadSanBong();
        }

        private void btnTimKiemSan_Click(object sender, EventArgs e)
        {
            btnTimKiemSan.BackColor = Color.DarkKhaki;
            VisableBtnTK_San(true);
        }

        private void btnTK_ThucThiSan_Click(object sender, EventArgs e)
        {
            int i = 0;
            dgvSanBong.Rows.Clear();
            SanBongDAO sb = new SanBongDAO();
            List<SanBong> listSan = new List<SanBong>();
            listSan = sb.TimKiemSan(txtTenSan.Text);
            /*if (txtTenSan.Enabled == true && txtTenSan.Text != "")
            {
                sanBong.TenSan = txtTenSan.Text;
                listSan = sb.TimKiemSan(sanBong, 1);
            }
            else if (txtDiaChiSan.Enabled == true && txtDiaChiSan.Text != "")
            {
                sanBong.DiaChi = txtDiaChiSan.Text;
                listSan = sb.TimKiemSan(sanBong, 2);
            }
            else if (cboxTrangThai.Enabled == true && cboxTrangThai.Text != "")
            {
                sanBong.TrangThai = cboxTrangThai.Text;
                listSan = sb.TimKiemSan(sanBong, 3);
            }*/
            foreach (SanBong item in listSan)
            {
                i++;
                dgvSanBong.Rows.Add(item.ID, i, item.TenSan, item.DiaChi, item.GiaThue, item.TrangThai);
            }
            CLearSan();
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


        //------------------------------------------------------
        //______________________Khách Hàng______________________
        //
        private KhachHang khach = new KhachHang();

        private void LoadKhachHang()
        {
            int i = 0;
            dgvKhach.Rows.Clear();
            KhachHangDAO sb = new KhachHangDAO();
            List<KhachHang> listKhach = new List<KhachHang>();
            listKhach = sb.GetKhachHangList();
            foreach (KhachHang item in listKhach)
            {
                i++;
                dgvKhach.Rows.Add(item.ID, i, item.TenKhach, item.DiaChi, item.Sdt);
            }
            txtTongKhach.Text = "Tổng khách hàng (" + i.ToString() + ")";
        }

        private void ClearKhach()
        {
            khach.TenKhach = string.Empty;
            khach.DiaChi = string.Empty;
            khach.Sdt = string.Empty;
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
            btnTK_ThucThiKhach.Visible = false;
            VisibleBtnTK_Khach(false);
        }

        private void ResetKhach()
        {
            ClearKhachText();
            btnSuaKhach.Enabled = false;
            btnThemKhach.Enabled = true;
            btnTimKiemKhach.Enabled = true;
            btnTimKiemKhach.BackColor = Color.LightGoldenrodYellow;
            LoadKhachHang();
            LoadCboxKhach();
        }

        private void VisibleBtnTK_Khach(bool sw)
        {
            btnTK_TenKhach.Visible = sw;
            btnTK_DiaChiKhach.Visible = sw;
            btnTK_Sdt.Visible = sw;
        }

        private void btnThemKhach_Click(object sender, EventArgs e)
        {
            if (txtTenKhach.Text == string.Empty
                || txtDiaChiKhach.Text == string.Empty
                || txtSdtKhach.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
                txtTenSan.Focus();
                return;
            }
            GetKhachText();
            KhachHangDAO.ThemKhach(khach);
            MessageBox.Show("Thêm khách hàng thành công!");
            ClearKhachText();
            LoadKhachHang();
            LoadCboxKhach();
        }

        private void dgvKhach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string ColName = dgvKhach.Columns[e.ColumnIndex].Name;
            if (ColName == "ColSuaKhach")
            {
                if ((MessageBox.Show("Bạn có muốn sửa thông tin khách hàng này?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes))
                {
                    txtTenKhach.Text = dgvKhach.CurrentRow.Cells[2].Value.ToString();
                    txtDiaChiKhach.Text = dgvKhach.CurrentRow.Cells[3].Value.ToString();
                    txtSdtKhach.Text = dgvKhach.CurrentRow.Cells[4].Value.ToString();
                    btnSuaKhach.Enabled = true;
                    btnThemKhach.Enabled = false;
                    btnTimKiemKhach.Enabled = false;
                }
            }
            else if (ColName == "ColXoaKhach")
            {
                if (MessageBox.Show("Xóa khách hàng này khỏi danh sách ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    KhachHangDAO.XoaKhach(dgvKhach);
                    ClearKhachText();
                    LoadKhachHang();
                    LoadCboxKhach();
                }
            }
            else if(ColName == "ColDatSan")
            {
                if (MessageBox.Show("Bạn muốn đặt sân không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    tcMain.SelectedTab = tpDatSan;
                    cboxKhachHang.Text = dgvKhach.CurrentRow.Cells[2].Value.ToString();
                }
            }
        }

        private void btnSuaKhach_Click(object sender, EventArgs e)
        {
            GetKhachText();
            KhachHangDAO.SuaKhach(khach);
            ClearKhachText();
            btnSuaKhach.Enabled = false;
            btnThemKhach.Enabled = true;
            btnTimKiemKhach.Enabled = true;
            LoadKhachHang();
            LoadCboxKhach();
        }

        private void btnTK_TenKhach_Click(object sender, EventArgs e)
        {
            ClearKhachText();
            txtDiaChiKhach.Enabled = false;
            txtSdtKhach.Enabled = false;
            btnTK_ThucThiKhach.Visible = true;
            btnTimKiemKhach.BackColor = Color.LightGoldenrodYellow;
            VisableBtnTK_San(false);
            txtTenKhach.Select();
            LoadKhachHang();
        }

        private void btnTK_DiaChiKhach_Click(object sender, EventArgs e)
        {
            ClearKhachText();
            txtTenKhach.Enabled = false;
            txtSdtKhach.Enabled = false;
            btnTK_ThucThiKhach.Visible = true;
            btnTimKiemKhach.BackColor = Color.LightGoldenrodYellow;
            VisableBtnTK_San(false);
            txtDiaChiKhach.Select();
            LoadKhachHang();
        }

        private void btnTK_Sdt_Click(object sender, EventArgs e)
        {
            ClearKhachText();
            txtTenKhach.Enabled = false;
            txtDiaChiKhach.Enabled = false;
            btnTK_ThucThiKhach.Visible = true;
            btnTimKiemKhach.BackColor = Color.LightGoldenrodYellow;
            VisableBtnTK_San(false);
            txtSdtKhach.Select();
            LoadKhachHang();
        }

        private void btnTK_ThucThiKhach_Click(object sender, EventArgs e)
        {
            int i = 0;
            dgvKhach.Rows.Clear();
            KhachHangDAO kh = new KhachHangDAO();
            List<KhachHang> listKhach = new List<KhachHang>();
            /*if (txtTenKhach.Enabled == true && txtTenKhach.Text != "")
            {
                khach.TenKhach = txtTenKhach.Text;
                listKhach = kh.TimKiemKhach(khach, 1);
            }
            else if (txtDiaChiKhach.Enabled == true && txtDiaChiKhach.Text != "")
            {
                khach.DiaChi = txtDiaChiKhach.Text;
                listKhach = kh.TimKiemKhach(khach, 2);
            }
            else if (txtSdtKhach.Enabled == true && txtSdtKhach.Text != "")
            {
                khach.Sdt = txtSdtKhach.Text;
                listKhach = kh.TimKiemKhach(khach, 3);
            }*/
            foreach (KhachHang item in listKhach)
            {
                i++;
                dgvKhach.Rows.Add(item.ID, i, item.TenKhach, item.DiaChi, item.Sdt);
            }
            ClearKhach();
        }

        private void btnResetKhach_Click(object sender, EventArgs e)
        {
            ResetKhach();
        }

        private void btnTimKiemKhach_Click(object sender, EventArgs e)
        {
            btnTimKiemKhach.BackColor = Color.DarkKhaki;
            VisibleBtnTK_Khach(true);
        }

        private void txtTenKhach_KeyDown(object sender, KeyEventArgs e)
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
                txtDiaChiKhach.Focus();
                e.Handled = true;
            }
        }

        private void txtSdtKhach_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnThemKhach.Focus();
                e.Handled = true;
            }
        }

        //----------------------------------------------------------------
        //__________________________Phiếu Đặt Sân_________________________
        //

        private PhieuDatSan datSan = new PhieuDatSan();

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
                dgvDatSan.Rows.Add(item.ID, i, item.TenSan, item.TenKhach, item.NgayDat, item.KhungGio, item.GhiChu);
            }
        }

        private void ClearDatSan()
        {
            datSan.TenSan = string.Empty;
            datSan.TenKhach = string.Empty;
            datSan.NgayDat = string.Empty;
            datSan.KhungGio = string.Empty;
            datSan.GhiChu = string.Empty;
        }

        private void ClearDatSanText()
        {
            cboxSanBong.Text = string.Empty;
            cboxKhachHang.Text = string.Empty;
            dtpNgayDat.Text = string.Empty;
            cboxKhungGio.Text = string.Empty;
            txtTienCoc.Text = string.Empty;
            txtGhiChu.Text = string.Empty;
        }

        private void GetDatSanText()
        {
            ClearDatSan();
            datSan.TenSan = cboxSanBong.Text;
            datSan.TenKhach = cboxKhachHang.Text;
            datSan.NgayDat = dtpNgayDat.Value.ToString();
            datSan.KhungGio = cboxKhungGio.Text;
            datSan.GhiChu = txtGhiChu.Text;
        }

        private void btnLuuDatSan_Click(object sender, EventArgs e)
        {
            if (cboxSanBong.Text == string.Empty 
                || cboxKhachHang.Text == string.Empty 
                || dtpNgayDat.Value == null 
                || cboxKhungGio.Text == string.Empty 
                || txtTienCoc.Text == "0") 
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            } 
            else if(txtTienCoc.Text.Length < 5 || txtTienCoc.Text.Length > 7)
            {
                MessageBox.Show("Tiền cọc có độ dài 6 chữ số!");
                txtTienCoc.Focus();
                return;
            }
            else if(txtGhiChu.Text == string.Empty)
            {
                txtGhiChu.Text = "";
            }
            GetDatSanText();
            PhieuDatSanDAO.ThemPhieuDatSan(datSan, 1, 1);
            ClearDatSanText();
            LoadDatSan();
        }

        private void dgvDatSan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string ColName = dgvDatSan.Columns[e.ColumnIndex].Name;
            if(ColName == "ColHuyPDS")
            {
                if (MessageBox.Show("Xóa phiếu đặt sân này khỏi danh sách ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    ClearDatSan();
                    datSan.ID = (int)dgvDatSan.CurrentRow.Cells[0].Value;
                    datSan.TenSan = dgvDatSan.CurrentRow.Cells[2].Value.ToString();
                    datSan.TenKhach = dgvDatSan.CurrentRow.Cells[3].Value.ToString();
                    datSan.NgayDat = dgvDatSan.CurrentRow.Cells[4].Value.ToString();
                    datSan.KhungGio = dgvDatSan.CurrentRow.Cells[5].Value.ToString();
                    datSan.GhiChu = dgvDatSan.CurrentRow.Cells[6].Value.ToString();
                    PhieuDatSanDAO.HuyPhieuDatSan(datSan);
                    MessageBox.Show("Hủy đặt sân thành công");
                    LoadDatSan();
                }
            }
            else if(ColName == "ColHoaDon")
            {
                if (MessageBox.Show("Bạn muốn xuất hóa đơn?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    tcMain.SelectedTab = tpHoaDon;
                    txtHD_TenKhach.Text = dgvDatSan.CurrentRow.Cells[3].Value.ToString();
                    txtHD_TenSan.Text = dgvDatSan.CurrentRow.Cells[2].Value.ToString();
                    dtpNgayLap.Text = DateTime.Now.ToShortDateString();
                    cboxHD_TrangThai.SelectedIndex = 0;
                }
            }
        }

        private void btnResetDatSan_Click(object sender, EventArgs e)
        {
            ClearDatSanText();
            LoadDatSan();
        }

        private void cboxSanBong_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cboxKhachHang.Focus();
                e.Handled = true;
            }
        }

        private void cboxKhachHang_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dtpNgayDat.Focus();
                e.Handled = true;
            }
        }

        private void dtpNgayDat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cboxKhungGio.Focus();
                e.Handled = true;
            }
        }

        private void cboxKhungGio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtTienCoc.Focus();
                e.Handled = true;
            }
        }

        private void txtTienCoc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtGhiChu.Focus();
                e.Handled = true;
            }
        }

        private void txtGhiChu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLuuDatSan.Focus();
                e.Handled = true;
            }
        }


        //----------------------------------------------------------------
        //____________________________Hóa Đơn_____________________________
        //
        private HoaDon hDon = new HoaDon();

        private void LoadHoaDon()
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
            txtHD_TenSan.Text = string.Empty;
            txtHD_TenKhach.Text = string.Empty;
        }

        private void ClearHoaDonText()
        {
            txtHD_TenKhach.Text = string.Empty;
            txtHD_TenSan.Text = string.Empty;
            dtpNgayLap.Text = string.Empty;
            cboxHD_TrangThai.Text = string.Empty;
        }

        private void ClearChiTietHD()
        {
            lblIDHoaDon.Text = "Hóa Đơn # ";
            lblNgayXuat.Text = "Ngày xuất hóa đơn: ";
            lblHD_TenKhach.Text = "Khách Hàng: ";
            lblHD_DiaChi.Text = "Địa Chỉ: ";
            lblHD_SanBong.Text = "Sân Bóng: ";
            lblHD_NgayThue.Text = "Ngày Thuê Sân: ";
            lblHD_TgianThue.Text = "Thời Gian Thuê: ";
            lblHD_PhiThue.Text = "Phí Thuê Sân: ";
            lblHD_TienCoc.Text = "Tiền Cọc: ";
            lblHD_ThanhToan.Text = "Tiền cần thanh toán: ";
        }

        private void ResetHoaDon()
        {
            ClearHoaDonText();
            ClearChiTietHD();
            btnXuatHD.Enabled = true;
            btnInHD.Enabled = true;
            EnableBtnCapNhatHoaDon(false);
            LoadHoaDon();
        }

        private void GetHoaDonText()
        {
            hDon = new HoaDon();
            if (txtHD_TenKhach.Text == null)
                hDon.TenKhach = "";
            else
                hDon.TenKhach = txtHD_TenKhach.Text;
            if (txtHD_TenSan.Text == null)
                hDon.TenSan = "";
            else
                hDon.TenSan = txtHD_TenSan.Text;
            hDon.Ngaylap = dtpNgayLap.Text;
            hDon.TrangThai = cboxHD_TrangThai.Text;
        }

        private void btnInHD_Click(object sender, EventArgs e)
        {

        }

        private void btnXuatHD_Click(object sender, EventArgs e)
        {
            if (txtHD_TenKhach.Text == string.Empty
                || txtHD_TenSan.Text == string.Empty
                || dtpNgayLap.Value == null
                || cboxHD_TrangThai.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }
            if (MessageBox.Show("Bạn muốn xuất hóa đơn?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                GetHoaDonText();
                HoaDonDAO.XuatHoaDon(hDon);
                ClearHoaDonText();
                LoadHoaDon();
            }
        }

        private void btnResetHD_Click(object sender, EventArgs e)
        {
            ResetHoaDon();
        }

        private void btnCapNhatHD_Click(object sender, EventArgs e)
        {
            HoaDonDAO.CapNhatHoaDon(hDon);
            hDon.TrangThai = string.Empty;
            hDon.TrangThai = cboxHD_TrangThai.Text;
            ClearHoaDonText();
            EnableBtnCapNhatHoaDon(false);
            LoadHoaDon();
        }

        private void EnableBtnCapNhatHoaDon(bool enable)
        {
            btnCapNhatHD.Enabled = enable;
            btnInHD.Enabled = !enable;
            btnXuatHD.Enabled = !enable;
        }

        private void dgvHoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string ColName = dgvHoaDon.Columns[e.ColumnIndex].Name;
            if (ColName == "ColChiTietHD")
            {
                GetHoaDonText();
                ClearChiTietHD();
                HoaDon temp = new HoaDon();
                HoaDonDAO hoaDon = new HoaDonDAO();
                hDon.ID = (int)dgvHoaDon.CurrentRow.Cells[0].Value;
                temp = hoaDon.GetChiTietHoaDon(hDon.ID);
                if (temp != null)
                {
                    lblIDHoaDon.Text = lblIDHoaDon.Text + dgvHoaDon.CurrentRow.Cells[0].Value.ToString();
                    lblNgayXuat.Text = lblNgayXuat.Text + dgvHoaDon.CurrentRow.Cells[4].Value.ToString();
                    lblHD_TenKhach.Text = lblHD_TenKhach.Text + dgvHoaDon.CurrentRow.Cells[2].Value.ToString();
                    lblHD_DiaChi.Text = lblHD_DiaChi.Text + temp.DiaChi.ToString();
                    lblHD_SanBong.Text = lblHD_SanBong.Text + dgvHoaDon.CurrentRow.Cells[3].Value.ToString();
                    lblHD_NgayThue.Text = lblHD_NgayThue.Text + temp.NgayThue.ToString();
                    lblHD_TgianThue.Text = lblHD_TgianThue.Text + temp.KhungGio.ToString();
                    lblHD_PhiThue.Text = lblHD_PhiThue.Text + temp.GiaThue.ToString() + " VND";
                }
            }
            else if (ColName == "ColSuaHD")
            {
                if (MessageBox.Show("Bạn muốn cập nhật hóa đơn này?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    DateTime dateVal;
                    string date = dgvHoaDon.CurrentRow.Cells[4].Value.ToString();
                    
                    hDon.ID = (int)dgvHoaDon.CurrentRow.Cells[0].Value;
                    txtHD_TenKhach.Text = dgvHoaDon.CurrentRow.Cells[2].Value.ToString();
                    txtHD_TenSan.Text = dgvHoaDon.CurrentRow.Cells[3].Value.ToString();
                    if (DateTime.TryParse(date, out dateVal)) 
                        dtpNgayLap.Value = dateVal;
                    cboxHD_TrangThai.Text = dgvHoaDon.CurrentRow.Cells[5].Value.ToString();
                    EnableBtnCapNhatHoaDon(true);
                }
            }
            else if (ColName == "ColXoaHoaDon")
            {
                if (MessageBox.Show("Xóa hóa đơn này khỏi danh sách ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    hDon = new HoaDon();
                    hDon.ID = (int)dgvKhach.CurrentRow.Cells[2].Value;
                    HoaDonDAO.XoaHoaDon(hDon);
                    ClearHoaDonText();
                    LoadHoaDon();
                }
            }
        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}