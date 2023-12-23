using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDatThueSanBongNhanTao.DTO
{
    class Kho
    {
        public int ID { get; set; }

        public string TenSp { get; set; }

        public int SoLuong { get; set; }

        public string NgayNhap { get; set; }

        public float GiaNhap { get; set; }


        public Kho() { }

        public Kho(Kho kho)
        { 
            ID = kho.ID;
            TenSp = kho.TenSp;
            SoLuong = kho.SoLuong;
            GiaNhap = kho.GiaNhap;
        }
    }
}
