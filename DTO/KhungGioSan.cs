using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDatThueSanBongNhanTao.DTO
{
    class KhungGioSan
    {
        public int ID { get; set; }

        public string h7_9 { get; set; }

        public string h9_11 { get; set; }

        public string h14_16 { get; set; }

        public string h16_18 { get; set; }

        public string h18_20 { get; set; }

        public string h20_22 { get; set; }


        public KhungGioSan() { }

        KhungGioSan(KhungGioSan gio)
        {
            ID = gio.ID;
            h7_9 = gio.h7_9;
            h9_11 = gio.h9_11;
            h14_16 = gio.h14_16;
            h16_18 = gio.h16_18;
            h18_20 = gio.h18_20;
            h20_22 = gio.h20_22;
        }
    }
}
