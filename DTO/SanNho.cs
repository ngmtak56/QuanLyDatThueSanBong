using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDatThueSanBongNhanTao.DTO
{
    class SanNho
    {
        public int ID { get; set; }
        public string SoSan { get; set; }
        public string MoTa { get; set; }

        public SanNho() { }

        public SanNho(SanNho san)
        {
            ID = san.ID;
            SoSan = san.SoSan;
            MoTa = san.MoTa;
        }
    }
}
