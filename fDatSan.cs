using QuanLyDatThueSanBongNhanTao.DAO;
using QuanLyDatThueSanBongNhanTao.DTO;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System;

namespace QuanLyDatThueSanBongNhanTao
{
    public partial class fDatSan : Form
    {
        /*public fDatSan()
        {
            InitializeComponent();
            LoadDatSan();
            LoadCboxSan();
            LoadCboxKhach();
            LoadSanBong();
            dtpNgayDat.Text = string.Empty;
            checkNgay.Checked = true;
        }*/

        private void fDatSan_Load(object sender, EventArgs e)
        {

        }

        private PhieuDatSan datSan = new PhieuDatSan();

        /*public void LoadSanBong()
        {
            SanBongDAO sb = new SanBongDAO();
            List<SanBong> listSan = new List<SanBong>();
            listSan = sb.GetSanBongList();
            foreach(SanBong san in listSan)
            {
                Button btn = new Button();
                {
                    btn.Width = 100;
                    btn.Height = 60;
                }
                btn.Text = san.TenSan;
                btn.Click += btn_Click;
                btn.Tag = san;

                switch(san.TrangThai)
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
*/
        void LoadSanNho(int ID)
        {

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
/*
        void ShowKhungGio(int ID, string date)
        {
            KhungGioSan gio = new KhungGioSan();
            VisibleKhungGio(true);
            gio = KhungGioSanDAO.GetKhungGioSan(ID, date);
            lbl_7_9.Text = gio.h7_9.ToString();
            lbl_9_11.Text = gio.h9_11.ToString();
            lbl_14_16.Text = gio.h14_16.ToString();
            lbl_16_18.Text = gio.h16_18.ToString();
            lbl_18_20.Text = gio.h18_20.ToString();
            lbl_20_22.Text = gio.h20_22.ToString();
        }

        

        private void btn_Click(object sender, EventArgs e)
        {
            int sanID = ((sender as Button).Tag as SanBong).ID;
            ShowKhungGio(sanID, );
        }
*/

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

        public void LoadCboxSan()
        {
            cboxSanBong.Items.Clear();
            PhieuDatSanDAO pds = new PhieuDatSanDAO();
            List<SanBong> sans = new List<SanBong>();
            sans = pds.GetComboBoxSan();
            foreach (SanBong san in sans)
            {
                cboxSanBong.Items.Add(san.TenSan);
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
            dtpNgayDat.Text = string.Empty;
            cboxKhungGio.Text = string.Empty;
            txtTienCoc.Text = string.Empty;
            txtGhiChu.Text = string.Empty;
            checkNgay.Checked = false;
            checkThang.Checked = false;
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
            if (cboxSanBong.Text == string.Empty
                || cboxKhachHang.Text == string.Empty
                || dtpNgayDat.Value == null
                || cboxKhungGio.Text == string.Empty
                || txtTienCoc.Text == "0")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }
            else if (txtTienCoc.Text.Length < 5 || txtTienCoc.Text.Length > 7)
            {
                MessageBox.Show("Tiền cọc có độ dài 6 chữ số!");
                txtTienCoc.Focus();
                return;
            }
            else if (txtGhiChu.Text == string.Empty)
            {
                txtGhiChu.Text = "";
            }
            GetDatSanText();
            PhieuDatSanDAO.ThemPhieuDatSan(datSan,1,1);
            ClearDatSanText();
            LoadDatSan();
            fHoaDon3 hdon = new fHoaDon3();
            hdon.LoadHoaDon();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ClearDatSan();
            ClearDatSanText();
            ClearChiTietSan();
        }

        private void cboxTgianThue_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void checkNgay_CheckedChanged(object sender, EventArgs e)
        {
            if(checkNgay.Checked == true)
            {
                checkThang.Checked = false;
                dtpNgayDat.BringToFront();
                cboxThang.SendToBack();
                txtTienCoc.Enabled = true;
                lblTgianThue.Text = "Ngày Đặt";
            }
        }

        private void checkThang_CheckedChanged(object sender, EventArgs e)
        {
            if(checkThang.Checked == true)
            {
                checkNgay.Checked = false;
                cboxThang.BringToFront();
                dtpNgayDat.SendToBack();
                txtTienCoc.Enabled = false;
                lblTgianThue.Text = "Tháng Đặt";
            }
        }
    }
}
