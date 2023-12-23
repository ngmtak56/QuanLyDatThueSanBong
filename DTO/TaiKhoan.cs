
namespace QuanLyDatThueSanBongNhanTao.DTO
{
    public class TaiKhoan
    {
        public int ID { get; set; }

        public string TenTaiKhoan { get; set; }

        public string MatKhau { get; set; }

        public string Quyen { get; set; }

        public TaiKhoan() { }

        public TaiKhoan(string TenTaiKhoan, string MatKhau)
        {
            this.TenTaiKhoan = TenTaiKhoan;
            this.MatKhau = MatKhau;
        }

        public TaiKhoan(int ID, string TenTaiKhoan, string MatKhau, string Quyen)
        {
            this.ID = ID;
            this.TenTaiKhoan = TenTaiKhoan;
            this.MatKhau = MatKhau;
            this.Quyen = Quyen;
        }

        public TaiKhoan(TaiKhoan tk)
        {
            this.TenTaiKhoan = tk.TenTaiKhoan;
            this.MatKhau = tk.MatKhau;
        }
    }
}
