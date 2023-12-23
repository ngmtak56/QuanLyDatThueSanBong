using QuanLyDatThueSanBongNhanTao.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Policy;

namespace QuanLyDatThueSanBongNhanTao.DAO
{
    class KhoDAO
    {
        public List<Kho> GetKho()
        {
            List<Kho> list = new List<Kho>();
            string query = "EXEC USP_GetKho";
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
                    Kho kho = new Kho();
                    kho.ID = (int)Database.sqlRead[0];
                    kho.TenSp = Database.sqlRead[1].ToString();
                    kho.SoLuong = (int)Database.sqlRead[2];
                    kho.NgayNhap = Database.sqlRead[3].ToString();
                    kho.GiaNhap = (int)Database.sqlRead[4];

                    list.Add(kho);
                }
                Database.sqlConnect.Close();
                return list;
            }
            catch (Exception ex) { throw ex; }
        }

        public static void ThemSanPhamVaoKho(Kho kho)
        {
            string query = "EXEC USP_InsertSanPhamVaoKho @TenSP, @SoLuong, @NgayNhap, @GiaNhap";
            try
            {
                if (Database.sqlConnect.State == ConnectionState.Closed)
                {
                    Database.sqlConnect.Open();
                }
                Database.sqlCommand = new SqlCommand(query, Database.sqlConnect);
                Database.sqlCommand.Parameters.AddWithValue("@TenSP", kho.TenSp);
                Database.sqlCommand.Parameters.AddWithValue("@SoLuong", kho.SoLuong);
                Database.sqlCommand.Parameters.AddWithValue("@NgayNhap", kho.NgayNhap);
                Database.sqlCommand.Parameters.AddWithValue("@GiaNhap", kho.GiaNhap);
                Database.sqlCommand.ExecuteNonQuery();
                Database.sqlConnect.Close();
            }
            catch (Exception ex) { throw ex; }
        }

        public static void NhapHang(Kho kho)
        {
            string query = "EXEC USP_NhapHang @IDKho, @SoLuong, @NgayNhap";
            try
            {
                if (Database.sqlConnect.State == ConnectionState.Closed)
                {
                    Database.sqlConnect.Open();
                }
                Database.sqlCommand = new SqlCommand(query, Database.sqlConnect);
                Database.sqlCommand.Parameters.AddWithValue("@IDKho", Database.ID_Kho);
                Database.sqlCommand.Parameters.AddWithValue("@SoLuong", kho.SoLuong);
                Database.sqlCommand.Parameters.AddWithValue("@NgayNhap", kho.NgayNhap);
                Database.sqlCommand.ExecuteNonQuery();
                Database.sqlConnect.Close();
            }
            catch (Exception ex) { throw ex; }
        }

        public static void XoaSanPhamTuKho(int ID)
        {
            string query = "EXEC USP_DeleteSanPhamTuKho @ID";
            try
            {
                if (Database.sqlConnect.State == ConnectionState.Closed)
                {
                    Database.sqlConnect.Open();
                }
                Database.sqlCommand = new SqlCommand(query, Database.sqlConnect);
                Database.sqlCommand.Parameters.AddWithValue("@ID", Database.ID_Kho);
                Database.sqlCommand.ExecuteNonQuery();
                Database.sqlConnect.Close();
            }
            catch (Exception ex) { throw ex; }
        }

        public List<Kho> GetLoaiSanPham(string loai)
        {
            List<Kho> list = new List<Kho>();
            string query = "EXEC USP_GetLoaiSanPham @Loai";
            try
            {
                if (Database.sqlConnect.State == ConnectionState.Closed)
                {
                    Database.sqlConnect.Open();
                }
                Database.sqlCommand = new SqlCommand(query, Database.sqlConnect);
                Database.sqlCommand.Parameters.AddWithValue("@Loai", loai);
                Database.sqlRead = Database.sqlCommand.ExecuteReader();
                while (Database.sqlRead.Read())
                {
                    Kho kho = new Kho();
                    kho.ID = (int)Database.sqlRead[0];
                    kho.TenSp = Database.sqlRead[1].ToString();
                    kho.SoLuong = (int)Database.sqlRead[2];
                    kho.NgayNhap = Database.sqlRead[3].ToString();
                    kho.GiaNhap = (int)Database.sqlRead[4];

                    list.Add(kho);
                }
                Database.sqlConnect.Close();
                return list;
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
