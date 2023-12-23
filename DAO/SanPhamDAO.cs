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
    class SanPhamDAO
    {
        public List<SanPham> GetSanPhamList()
        {
            List<SanPham> list = new List<SanPham>();
            string query = "EXEC USP_GetMenu";
            try
            {
                if (Database.sqlConnect.State == ConnectionState.Closed)
                {
                    Database.sqlConnect.Open();
                }
                Database.sqlCommand = new SqlCommand(query, Database.sqlConnect);
                Database.sqlRead = Database.sqlCommand.ExecuteReader();
                while (Database.sqlRead.Read())
                {
                    SanPham sp = new SanPham();
                    sp.ID = (int)Database.sqlRead[0];
                    sp.TenSP = Database.sqlRead[1].ToString();
                    sp.GiaBan = Convert.ToInt32(Database.sqlRead[2]);
                    sp.Loai = Database.sqlRead[3].ToString();

                    list.Add(sp);
                }
                Database.sqlConnect.Close();
                return list;
            }
            catch (Exception ex) { throw ex; }
        }

        public List<SanPham> GetAoDaBongList()
        {
            List<SanPham> list = new List<SanPham>();
            string query = "EXEC USP_GetAoBongDa";
            try
            {
                if (Database.sqlConnect.State == ConnectionState.Closed)
                {
                    Database.sqlConnect.Open();
                }
                Database.sqlCommand = new SqlCommand(query, Database.sqlConnect);
                Database.sqlRead = Database.sqlCommand.ExecuteReader();
                while (Database.sqlRead.Read())
                {
                    SanPham sp = new SanPham();
                    sp.ID = (int)Database.sqlRead[0];
                    sp.TenSP = Database.sqlRead[1].ToString();
                    sp.GiaBan = Convert.ToInt32(Database.sqlRead[2]);
                    sp.Loai = Database.sqlRead[3].ToString();

                    list.Add(sp);
                }
                Database.sqlConnect.Close();
                return list;
            }
            catch (Exception ex) { throw ex; }
        }

        public List<SanPham> GetGiayDaBongList()
        {
            List<SanPham> list = new List<SanPham>();
            string query = "EXEC USP_GetGiayBongDa";
            try
            {
                if (Database.sqlConnect.State == ConnectionState.Closed)
                {
                    Database.sqlConnect.Open();
                }
                Database.sqlCommand = new SqlCommand(query, Database.sqlConnect);
                Database.sqlRead = Database.sqlCommand.ExecuteReader();
                while (Database.sqlRead.Read())
                {
                    SanPham sp = new SanPham();
                    sp.ID = (int)Database.sqlRead[0];
                    sp.TenSP = Database.sqlRead[1].ToString();
                    sp.GiaBan = Convert.ToInt32(Database.sqlRead[2]);
                    sp.Loai = Database.sqlRead[3].ToString();

                    list.Add(sp);
                }
                Database.sqlConnect.Close();
                return list;
            }
            catch (Exception ex) { throw ex; }
        }

        public static void ThemSanPham(SanPham sp)
        {
            string query = "EXEC USP_InsertSanPham @TenSP, @GiaBan, @Loai";
            try
            {
                if (Database.sqlConnect.State == ConnectionState.Closed)
                {
                    Database.sqlConnect.Open();
                }
                Database.sqlCommand = new SqlCommand(query, Database.sqlConnect);
                Database.sqlCommand.Parameters.AddWithValue("@TenSP", sp.TenSP);
                Database.sqlCommand.Parameters.AddWithValue("@GiaBan", sp.GiaBan);
                Database.sqlCommand.Parameters.AddWithValue("@Loai", sp.Loai);
                Database.sqlCommand.ExecuteNonQuery();
                Database.sqlConnect.Close();
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
