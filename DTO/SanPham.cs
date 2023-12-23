using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDatThueSanBongNhanTao.DTO
{
    class SanPham
    {
        public int ID { get; set; }

        public string TenSP { get; set; }

        public int GiaBan { get; set; }

        public string Loai { get; set; }

        public SanPham() { }

        SanPham(SanPham sp)
        {
            ID = sp.ID;
            TenSP = sp.TenSP;
            GiaBan = sp.GiaBan;
            Loai = sp.Loai;
        }
    }
}
