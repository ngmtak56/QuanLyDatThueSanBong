namespace QuanLyDatThueSanBongNhanTao.DTO
{
    public class HoaDon
    {
        public int ID { get; set; }

        public int IDSan { get; set; }

        public int IDKhach { get; set; }

        public string TenSan { get; set; }

        public string SoSan { get; set; }

        public string TenKhach { get; set; }

        public string Sdt { get; set; }

        public string DiaChi { get; set; }

        public string NgayThue { get; set; }

        public string KhungGio { get; set; }

        public string TrongTai { get; set; }

        public int GiaThue { get; set; }

        public int GiaThueTT { get; set; }

        public string Ngaylap { get; set; }

        public string TrangThai { get; set; }

        public float TongTien { get; set; }

        public HoaDon() { }

        public HoaDon(int iD, int iDSan, int iDKhach, string trangThai)
        {
            ID = iD;
            IDSan = iDSan;
            IDKhach = iDKhach;
            TrangThai = trangThai;
        }

        public HoaDon(HoaDon hdon)
        {
            this.ID = hdon.ID;
            this.TenKhach = hdon.TenKhach;
            this.TenSan = hdon.TenSan;
            this.TrangThai = hdon.TrangThai;
        }
    }
}
