using QuanLyDatThueSanBongNhanTao.DTO;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;
using System.Text;
using System;


namespace QuanLyDatThueSanBongNhanTao.DAO
{
    public class TaiKhoanDAO
    {
        public static bool CheckLogin(string TenTK, string MatKhau)
        {
            bool check;
            TenTK = TaiKhoanDAO.GetMD5Hash(TenTK);
            MatKhau = TaiKhoanDAO.GetMD5Hash(MatKhau);
            string query = "EXEC USP_GetTaiKhoan @TenDangNhap, @MatKhau";
            if(Database.sqlConnect.State == ConnectionState.Closed)
            {
                Database.sqlConnect.Open();
            }
            Database.sqlCommand = new SqlCommand(query, Database.sqlConnect);
            Database.sqlCommand.Parameters.AddWithValue("@TenDangNhap", TenTK); 
            Database.sqlCommand.Parameters.AddWithValue("@MatKhau", MatKhau);
            Database.sqlRead = Database.sqlCommand.ExecuteReader();

            if (Database.sqlRead.HasRows == true)
            {
                check = true;
            }
            else
            {
                check = false;
            }
            Database.sqlConnect.Close();
            return check;
        }

        public List<TaiKhoan> TaiKhoans(string query)
        {
            List<TaiKhoan> list = new List<TaiKhoan>();
            if (Database.sqlConnect.State == ConnectionState.Closed)
            {
                Database.sqlConnect.Open();
            }
            Database.sqlCommand = new SqlCommand(query, Database.sqlConnect);
            Database.sqlRead = Database.sqlCommand.ExecuteReader();
            while (Database.sqlRead.Read())
            {
                list.Add(new TaiKhoan(Database.sqlRead.GetString(1), Database.sqlRead.GetString(2)));
            }
            Database.sqlConnect.Close();
            return list;
        }

        public static void ThemTaiKhoan(TaiKhoan taiKhoan)
        {
            string query = "EXEC USP_InsertTaiKhoan @TenDangNhap, @MatKhau";
            try
            {
                if(Database.sqlConnect.State == ConnectionState.Closed)
                {
                    Database.sqlConnect.Open();
                }
                taiKhoan.TenTaiKhoan = TaiKhoanDAO.GetMD5Hash(taiKhoan.TenTaiKhoan);
                taiKhoan.MatKhau = TaiKhoanDAO.GetMD5Hash(taiKhoan.MatKhau);
                Database.sqlCommand = new SqlCommand(query, Database.sqlConnect);
                Database.sqlCommand.Parameters.AddWithValue("@TenDangNhap", taiKhoan.TenTaiKhoan);
                Database.sqlCommand.Parameters.AddWithValue("@MatKhau", taiKhoan.MatKhau);
                Database.sqlCommand.ExecuteNonQuery();
                Database.sqlConnect.Close();
            }
            catch(Exception ex) { throw ex; }
        }

        public static string GetMD5Hash(string str)
        {
            using (MD5 md5Hash = MD5.Create())
            {                
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(str));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    builder.Append(data[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
