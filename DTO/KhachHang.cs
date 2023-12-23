
namespace QuanLyDatThueSanBongNhanTao.DTO
{
    public class KhachHang
    {
        public int ID { get; set; }
        public string TenKhach { get; set; }
        public string DiaChi { get; set; }
        public string Sdt { get; set; }

        public float GiaThue { get; set; }
        public KhachHang() { }
        public KhachHang(string TenKhach, string DiaChi, string Sdt)
        {
            this.TenKhach = TenKhach;
            this.DiaChi = DiaChi;
            this.Sdt = Sdt;
        }
    }
}
