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
    public partial class fChiTietHoaDon : Form
    {
        public fChiTietHoaDon(HoaDon chiTiet)
        {
            InitializeComponent();

            lblIDHoaDon.Text = lblIDHoaDon.Text + Database.ID_HoaDon.ToString();
            lblNgayXuat.Text = lblNgayXuat.Text + chiTiet.Ngaylap;
            lblTenKhach.Text = lblTenKhach.Text + chiTiet.TenKhach;
            lblDiaChi.Text = lblDiaChi.Text + chiTiet.DiaChi;
            lblSanBong.Text = lblSanBong.Text + chiTiet.TenSan;
            lblNgayThue.Text = lblNgayThue.Text + chiTiet.NgayThue.ToString();
            lblTgianThue.Text = lblTgianThue.Text + chiTiet.KhungGio.ToString();
            lblThanhToan.Text = lblThanhToan.Text + chiTiet.GiaThue.ToString() + " VND";
        }

        private void fChiTietHoaDon_Load(object sender, EventArgs e) { }
    }
}
