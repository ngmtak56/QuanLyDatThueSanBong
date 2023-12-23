using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDatThueSanBongNhanTao.DTO
{
    class HoaDonDV
    {
        public int ID { get; set; }

        public string Khach { get; set; }

        public string NgayXuat { get; set; }

        public string Sdt { get; set; }

        public string TrangThai { get; set; }

        public string SanPham { get; set; }

        public int SoLuong { get; set; }

        public float GiaTien { get; set; }

        public float TongTien { get; set; }

        public HoaDonDV() { }

        HoaDonDV(HoaDonDV hd)
        {
            ID = hd.ID;
            Khach = hd.Khach;
            NgayXuat = hd.NgayXuat;
            TrangThai = hd.TrangThai;
        }
    }
}
