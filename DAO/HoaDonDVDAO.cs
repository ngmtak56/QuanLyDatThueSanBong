using QuanLyDatThueSanBongNhanTao.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Security.Policy;
using System.IO;
using System.Windows.Forms;

namespace QuanLyDatThueSanBongNhanTao.DAO
{
    class HoaDonDVDAO
    {
        public List<HoaDonDV> GetHoaDonDVList()
        {
            List<HoaDonDV> list = new List<HoaDonDV>();
            string query = "EXEC USP_GetAllHoaDonDV";
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
                    HoaDonDV hd = new HoaDonDV();
                    hd.ID = (int)Database.sqlRead[0];
                    hd.Khach = Database.sqlRead[1].ToString();
                    hd.NgayXuat = Database.sqlRead[2].ToString();
                    hd.TrangThai = Database.sqlRead[3].ToString();

                    list.Add(hd);
                }
                Database.sqlConnect.Close();
                return list;
            }
            catch (Exception ex) { throw ex; }
        }

        public List<HoaDonDV> GetKhachHoaDon(string khach)
        {
            List<HoaDonDV> list = new List<HoaDonDV>();
            string query = "EXEC USP_GetHoaDonKhach @TenKhach";
            try
            {
                if (Database.sqlConnect.State == ConnectionState.Closed)
                {
                    Database.sqlConnect.Open();
                }
                Database.sqlCommand = new SqlCommand(query, Database.sqlConnect);
                Database.sqlCommand.Parameters.AddWithValue("@TenKhach", khach);
                Database.sqlRead = Database.sqlCommand.ExecuteReader();
                while (Database.sqlRead.Read())
                {
                    HoaDonDV hd = new HoaDonDV();
                    hd.ID = (int)Database.sqlRead[0];
                    hd.Khach = Database.sqlRead[1].ToString();
                    hd.NgayXuat = Database.sqlRead[2].ToString();
                    hd.TrangThai = Database.sqlRead[3].ToString();

                    list.Add(hd);
                }
                Database.sqlConnect.Close();
                return list;
            }
            catch (Exception ex) { throw ex; }
        }

        public void InsertHoaDonDV(HoaDonDV hd)
        {
            string query = "EXEC USP_InsertHoaDonDV @TenKhach, @NgayXuat";
            try
            {
                if (Database.sqlConnect.State == ConnectionState.Closed)
                {
                    Database.sqlConnect.Open();
                }
                Database.sqlCommand = new SqlCommand(query, Database.sqlConnect);
                Database.sqlCommand.Parameters.AddWithValue("@TenKhach", hd.Khach);
                Database.sqlCommand.Parameters.AddWithValue("@NgayXuat", hd.NgayXuat);
                Database.sqlCommand.ExecuteNonQuery();
                Database.sqlConnect.Close();
            }
            catch (Exception ex) { throw ex; }
        }

        public void InsertChiTietHDDV(HoaDonDV hd)
        {
            string query = "EXEC USP_InsertChiTietHDDV @IDHoaDon, @SanPham, @SoLuong, @TongTien";
            try
            {
                if (Database.sqlConnect.State == ConnectionState.Closed)
                {
                    Database.sqlConnect.Open();
                }
                Database.sqlCommand = new SqlCommand(query, Database.sqlConnect);
                Database.sqlCommand.Parameters.AddWithValue("@IDHoaDon", hd.ID);
                Database.sqlCommand.Parameters.AddWithValue("@SanPham", hd.SanPham);
                Database.sqlCommand.Parameters.AddWithValue("@SoLuong", hd.SoLuong);
                Database.sqlCommand.Parameters.AddWithValue("@TongTien", hd.TongTien);
                Database.sqlCommand.ExecuteNonQuery();
                Database.sqlConnect.Close();
            }
            catch (Exception ex) { throw ex; }
        }

        public int GetIDHoaDonDV()
        {
            int ID = 0;
            string query = "EXEC USP_FindIDHoaDonDV";
            try
            {
                if (Database.sqlConnect.State == ConnectionState.Closed)
                {
                    Database.sqlConnect.Open();
                }
                Database.sqlCommand = new SqlCommand(query, Database.sqlConnect);
                object tmp = Database.sqlCommand.ExecuteScalar();
                if (tmp != null)
                {
                    ID = Convert.ToInt32((tmp));
                }
                Database.sqlConnect.Close();
                return ID;
            }
            catch (Exception ex) { throw ex; }
        }

        public List<HoaDonDV> GetChiTietHoaDon(int ID)
        {
            List<HoaDonDV> list = new List<HoaDonDV>();
            string query = "EXEC USP_GetChiTietHoaDonDV @ID";
            try
            {
                if (Database.sqlConnect.State == ConnectionState.Closed)
                {
                    Database.sqlConnect.Open();
                }
                Database.sqlCommand = new SqlCommand(query, Database.sqlConnect);
                Database.sqlCommand.Parameters.AddWithValue("@ID", ID);
                Database.sqlRead = Database.sqlCommand.ExecuteReader();
                while (Database.sqlRead.Read())
                {
                    HoaDonDV hd = new HoaDonDV();
                    hd.SanPham = Database.sqlRead[0].ToString();
                    hd.SoLuong = Convert.ToInt32(Database.sqlRead[1].ToString());
                    hd.GiaTien = (float)Convert.ToDouble(Database.sqlRead[2].ToString());

                    list.Add(hd);
                }
                Database.sqlConnect.Close();
                return list;
            }
            catch (Exception ex) { throw ex; }
        }

        public HoaDonDV GetThongTinHoaDonDV(int ID)
        {
            string query = "EXEC USP_GetThongTinHoaDonDV @ID";
            try
            {
                HoaDonDV hd = new HoaDonDV();
                if (Database.sqlConnect.State == ConnectionState.Closed)
                {
                    Database.sqlConnect.Open();
                }
                Database.sqlCommand = new SqlCommand(query, Database.sqlConnect);
                Database.sqlCommand.Parameters.AddWithValue("@ID", ID);
                Database.sqlRead = Database.sqlCommand.ExecuteReader();
                while (Database.sqlRead.Read())
                {
                    hd.Khach = Database.sqlRead[0].ToString();
                    hd.Sdt = Database.sqlRead[1].ToString();
                    hd.NgayXuat = Database.sqlRead[2].ToString();
                }
                Database.sqlConnect.Close();
                return hd;
            }
            catch (Exception ex) { throw ex; }
        }

        public int KiemTraHetHangTrongKho(string sp, int soLuong)
        {
            int check = 0;
            string query = "EXEC USP_TestDatHang @SanPham, @SoLuong";
            try
            {
                if (Database.sqlConnect.State == ConnectionState.Closed)
                {
                    Database.sqlConnect.Open();
                }
                Database.sqlCommand = new SqlCommand(query, Database.sqlConnect);
                Database.sqlCommand.Parameters.AddWithValue("@SanPham", sp);
                Database.sqlCommand.Parameters.AddWithValue("@SoLuong", soLuong);
                object tmp = Database.sqlCommand.ExecuteScalar();
                if (tmp != null)
                {
                    check = Convert.ToInt32((tmp));
                }
                Database.sqlConnect.Close();
                return check;
            }
            catch (Exception ex) { throw ex; }
        }

        public void ThanhToanHoaDon(int ID, string TrangThai)
        {
            string query = "EXEC USP_ThanhToanHoaDon @ID, @TrangThai";
            try
            {
                if (Database.sqlConnect.State == ConnectionState.Closed)
                {
                    Database.sqlConnect.Open();
                }
                Database.sqlCommand = new SqlCommand(query, Database.sqlConnect);
                Database.sqlCommand.Parameters.AddWithValue("@ID", ID);
                Database.sqlCommand.Parameters.AddWithValue("@TrangThai", TrangThai);
                Database.sqlCommand.ExecuteNonQuery();
                MessageBox.Show("Thanh toán thành công!");
                Database.sqlConnect.Close();
            }
            catch (Exception ex) { throw ex; }
        }
        
        public static int CheckTrungLapKhachDV(string khach)
        {
            int check = 0;
            string query = "EXEC USP_CheckTrungLapKhachDV @TenKhach";
            try
            {
                if (Database.sqlConnect.State == ConnectionState.Closed)
                {
                    Database.sqlConnect.Open();
                }
                Database.sqlCommand = new SqlCommand(query, Database.sqlConnect);
                Database.sqlCommand.Parameters.AddWithValue("@TenKhach", khach);
                object tmp = Database.sqlCommand.ExecuteScalar();
                if (tmp != null)
                {
                    check = Convert.ToInt32((tmp));
                }
                Database.sqlConnect.Close();
                return check;
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
