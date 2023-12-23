
namespace QuanLyDatThueSanBongNhanTao.DTO
{
    public class SanBong
    {
        public int ID { get; set; }
        public string TenSan { get; set; }
        public string DiaChi { get; set; }
        public int SoLuong { get; set; }
        public int GiaThue { get; set; }
        public string TrangThai { get; set; }
        public bool Check { get; set; }
        public string SoSan { get; set; }
        public string MoTa { get; set; }

        public SanBong() { }
        public SanBong(string TenSan, string DiaChi, int GiaThue, string TrangThai)
        {
            this.TenSan = TenSan;
            this.DiaChi = DiaChi;
            this.GiaThue = GiaThue;
            this.TrangThai = TrangThai;
        }

        public SanBong (SanBong san)
        {
            this.ID = san.ID;
            this.TenSan = san.TenSan;
            this.DiaChi = san.DiaChi;
            this.SoLuong = san.SoLuong;
            this.GiaThue = san.GiaThue;
            this.TrangThai = san.TrangThai;
        }
    }
}
