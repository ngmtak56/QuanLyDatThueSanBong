using QuanLyDatThueSanBongNhanTao.DAO;
using System.Windows.Forms;
using System;

namespace QuanLyDatThueSanBongNhanTao
{
    public partial class fThongKe : Form
    {
        public fThongKe()
        {
            InitializeComponent();
            DateTime time = DateTime.Now;
            lblDate.Text = time.ToShortDateString();
            dtpNgay.Text = string.Empty;
        }

        private void fThongKe_Load(object sender, EventArgs e)
        {

        }

        private float doanhThu = 0, doanhThuDV = 0;
        private int soLanDS = 0;
        private int soKh = 0;
        private int soKhDV = 0;
        private ThongKeDAO tk = new ThongKeDAO();
        private DateTime time;

        private void GetDate()
        {
            time = dtpNgay.Value;
        }

        private void btnToday_Click(object sender, EventArgs e)
        {
            GetDate();
            lblDoanhThu.Text = string.Empty;
            string ngay = time.ToShortDateString();
            doanhThu = tk.DoanhThuTheoNgay(ngay);
            doanhThuDV = tk.DoanhThuDichVuTheoNgay(ngay);
            soLanDS = tk.SoLanDatSanTheoNgay(ngay);
            soKh = tk.SoKhachHangTheoNgay(ngay);
            soKhDV = tk.SoKhachHangDVTheoNgay(ngay);
            lblDoanhThu.Text = doanhThu.ToString() + " VND";
            lblDoanhThuDV.Text = doanhThuDV.ToString() + " VND";
            lblDatSan.Text = soLanDS.ToString() + " Lần";
            lblTenKhach.Text = soKh.ToString() + " Người";
            lblKhachDV.Text = soKhDV.ToString() + " Người";
        }

        private void btnMonth_Click(object sender, EventArgs e)
        {
            GetDate();
            lblDoanhThu.Text = string.Empty;
            string thang = time.ToShortDateString();
            doanhThu = tk.DoanhThuTheoThang(thang);
            doanhThuDV = tk.DoanhThuDichVuTheoThang(thang);
            soLanDS = tk.SoLanDatSanTheoThang(thang);
            soKh = tk.SoKhachHangTheoThang(thang);
            soKhDV = tk.SoKhachHangDVTheoThang(thang);
            lblDoanhThu.Text = doanhThu.ToString() + " VND";
            lblDoanhThuDV.Text = doanhThuDV.ToString() + " VND";
            lblDatSan.Text = soLanDS.ToString() + " Lần";
            lblTenKhach.Text = soKh.ToString() + " Người";
            lblKhachDV.Text = soKhDV.ToString() + " Người";
        }

        private void tlpThongKe_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnNam_Click(object sender, EventArgs e)
        {
            GetDate();
            lblDoanhThu.Text = string.Empty;
            string nam = time.ToShortDateString();
            doanhThu = tk.DoanhThuTheoNam(nam);
            doanhThuDV = tk.DoanhThuDichVuTheoNam(nam);
            soLanDS = tk.SoLanDatSanTheoNam(nam);
            soKh = tk.SoKhachHangTheoNam(nam);
            soKhDV = tk.SoKhachHangDVTheoNam(nam);
            lblDoanhThu.Text = doanhThu.ToString() + " VND";
            lblDoanhThuDV.Text = doanhThuDV.ToString() + " VND";
            lblDatSan.Text = soLanDS.ToString() + " Lần";
            lblTenKhach.Text = soKh.ToString() + " Người";
            lblKhachDV.Text = soKhDV.ToString() + " Người";
        }
    }
}
