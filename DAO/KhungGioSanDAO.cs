using QuanLyDatThueSanBongNhanTao.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDatThueSanBongNhanTao.DAO
{
    class KhungGioSanDAO
    {
        public static KhungGioSan GetKhungGioSan(int ID, string date)
        {
            KhungGioSan gio = new KhungGioSan();
            /*string query = "EXEC USP_GetKhungGioSan @IDSan";*/
            string query = "EXEC USP_GetKhungGioSanBongListSanNho @IDSan, @Date";
            try
            {
                if (Database.sqlConnect.State == ConnectionState.Closed)
                {
                    Database.sqlConnect.Open();
                }

                Database.sqlCommand = new SqlCommand(query, Database.sqlConnect);
                Database.sqlCommand.Parameters.AddWithValue("@IDSan", ID);
                Database.sqlCommand.Parameters.AddWithValue("@Date", date);
                Database.sqlRead = Database.sqlCommand.ExecuteReader();

                while (Database.sqlRead.Read())
                {
                    gio.h7_9 = Database.sqlRead[0].ToString();
                    gio.h9_11 = Database.sqlRead[1].ToString();
                    gio.h14_16 = Database.sqlRead[2].ToString();
                    gio.h16_18 = Database.sqlRead[3].ToString();
                    gio.h18_20 = Database.sqlRead[4].ToString();
                    gio.h20_22 = Database.sqlRead[5].ToString();
                }
                Database.sqlConnect.Close();
                return gio;
            }
            catch (Exception ex) { throw ex; }
        }

        public List<KhungGioSan> GetKhungGioSans(int id)
        {
            List<KhungGioSan> list = new List<KhungGioSan> { };

            return list;
        }
    }
}
