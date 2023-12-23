using System;

namespace QuanLyDatThueSanBongNhanTao.DTO
{
    public class PhieuDatSan
    {
        public int ID { get; set; }

        public int IDSan { get; set; }

        public int IDKhach { get; set; }

        public string TenSan { get; set; }

        public string SanNho { get; set; }

        public string TenKhach { get; set; }

        public string TrongTai { get; set; }

        public string NgayDat { get; set; }

        public string Thang { get; set; }

        public string Nam { get; set; }

        public string KhungGio { get; set; }

        public string Thu { get; set; }

        public string GhiChu { get; set; }

        public string TrangThai { get; set; }

        public PhieuDatSan()
        {
            IDSan = 0;
            IDKhach = 0;
            TenSan = string.Empty;
            SanNho = string.Empty;
            TenKhach = string.Empty;
            NgayDat = string.Empty;
            Thang = string.Empty;
            Nam = string.Empty;
            KhungGio = string.Empty;
            GhiChu = string.Empty;
            TrangThai = string.Empty;
        }

        public PhieuDatSan(PhieuDatSan pds)
        {
            ID = pds.ID;
            IDSan = pds.IDSan;
            IDKhach= pds.IDKhach;
            NgayDat = pds.NgayDat;
            KhungGio = pds.KhungGio;
            GhiChu = pds.GhiChu;
        }

        public PhieuDatSan(System.Data.DataRow row)
        {
            this.ID = (int)row["ID"];
            this.IDSan = (int)row["IDSan"];
            this.IDKhach = (int)row["IDKhachHang"];
            this.NgayDat = row["NgayDat"].ToString();
            this.KhungGio = row["KhungGio"].ToString();
            this.GhiChu = row["GhiChu"].ToString();
        }

        
    }
}
