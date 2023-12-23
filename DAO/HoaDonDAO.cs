using QuanLyDatThueSanBongNhanTao.DTO;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;
using System;

namespace QuanLyDatThueSanBongNhanTao.DAO
{
    public class HoaDonDAO
    {
        public List<HoaDon> GetHoaDonList()
        {
            List<HoaDon> list  = new List<HoaDon>();
            string query = "EXEC USP_GetHoaDonList";
            try
            {
                if(Database.sqlConnect.State == ConnectionState.Closed)
                {
                    Database.sqlConnect.Open();
                }
                Database.sqlCommand = new SqlCommand(query, Database.sqlConnect);
                Database.sqlRead = Database.sqlCommand.ExecuteReader();
                while (Database.sqlRead.Read())
                {
                    HoaDon hd = new HoaDon();
                    hd.ID = (int)Database.sqlRead[0];
                    hd.TenKhach = Database.sqlRead[1].ToString();
                    hd.TenSan = Database.sqlRead[2].ToString();
                    hd.SoSan = Database.sqlRead[3].ToString();
                    hd.Ngaylap = Database.sqlRead[4].ToString();
                    hd.TrangThai = Database.sqlRead[5].ToString();

                    list.Add(hd);
                }
                Database.sqlConnect.Close();
                return list;
            }
            catch (Exception ex) { throw ex; }
        }

        public static void XuatHoaDon(HoaDon hoaDon)
        {
            string query = "EXEC USP_InsertHoaDon @TenKhach, @TenSan, @SoSan, @NgayLap, @TrangThai";
            try
            {
                if (Database.sqlConnect.State == ConnectionState.Closed)
                {
                    Database.sqlConnect.Open();
                }
                Database.sqlCommand = new SqlCommand(query, Database.sqlConnect);
                Database.sqlCommand.Parameters.AddWithValue("@TenKhach", hoaDon.TenKhach);
                Database.sqlCommand.Parameters.AddWithValue("@TenSan", hoaDon.TenSan);
                Database.sqlCommand.Parameters.AddWithValue("@SoSan", hoaDon.SoSan);
                Database.sqlCommand.Parameters.AddWithValue("@NgayLap", hoaDon.Ngaylap);
                Database.sqlCommand.Parameters.AddWithValue("@TrangThai", hoaDon.TrangThai);
                Database.sqlCommand.ExecuteNonQuery();
                MessageBox.Show("Xuất hóa đơn thành công!");
                Database.sqlConnect.Close();
            }
            catch (Exception ex) { throw ex; }
        }

        public HoaDon GetChiTietHoaDon(int key)
        {
            string query = "EXEC USP_GetChiTietHoaDon @ID";
            try
            {
                HoaDon hd = new HoaDon();
                if(Database.sqlConnect.State == ConnectionState.Closed)
                {
                    Database.sqlConnect.Open();
                }
                Database.sqlCommand = new SqlCommand(query, Database.sqlConnect);
                Database.sqlCommand.Parameters.AddWithValue("@ID", key);
                Database.sqlRead = Database.sqlCommand.ExecuteReader();
                while (Database.sqlRead.Read())
                {
                    hd.Ngaylap = Database.sqlRead[0].ToString();
                    hd.TenKhach = Database.sqlRead[1].ToString();
                    hd.Sdt = Database.sqlRead[2].ToString();
                    hd.TenSan = Database.sqlRead[3].ToString();
                    hd.DiaChi = Database.sqlRead[4].ToString();
                    hd.SoSan = Database.sqlRead[5].ToString();
                    hd.KhungGio = Database.sqlRead[6].ToString();
                    hd.TrongTai = Database.sqlRead[7].ToString();
                    hd.GiaThue = Convert.ToInt32(Database.sqlRead[8]);
                    hd.GiaThueTT = Convert.ToInt32(Database.sqlRead[9]);
                    hd.TongTien = (float)Convert.ToDouble(Database.sqlRead[10]);
                }
                Database.sqlConnect.Close();
                return hd;
            }
            catch (Exception ex) { throw ex; }
        }

        public static void CapNhatHoaDon(HoaDon hoaDon)
        {
            string query = "EXEC USP_UpdateHoaDon @ID, @TrangThai";
            try
            {
                if(Database.sqlConnect.State == ConnectionState.Closed)
                {
                    Database.sqlConnect.Open();
                }
                Database.sqlCommand = new SqlCommand(query, Database.sqlConnect);
                Database.sqlCommand.Parameters.AddWithValue("@ID", Database.ID_HoaDon);
                Database.sqlCommand.Parameters.AddWithValue("@TrangThai", hoaDon.TrangThai);
                Database.sqlCommand.ExecuteNonQuery();
                MessageBox.Show("Cập nhật hóa đơn thành công!");
                Database.sqlConnect.Close();
            }
            catch (Exception ex) { throw ex; }
        }

        public static void XoaHoaDon(HoaDon hoaDon)
        {
            string query = "EXEC USP_DeleteHoaDon @ID";
            try
            {
                if (Database.sqlConnect.State == ConnectionState.Closed)
                {
                    Database.sqlConnect.Open();
                }
                Database.sqlCommand = new SqlCommand(query, Database.sqlConnect);
                Database.sqlCommand.Parameters.AddWithValue("@ID", Database.ID_HoaDon);
                Database.sqlCommand.ExecuteNonQuery();
                MessageBox.Show("Xóa hóa đơn thành công!");
                Database.sqlConnect.Close();
            }
            catch (Exception ex) { throw ex; }
        }

        public List<HoaDon> TimKiemHoaDon(string key)
        {
            string query = "EXEC USP_FindHoaDon @Key";
            List<HoaDon> list = new List<HoaDon>();
            try
            {
                if (Database.sqlConnect.State == ConnectionState.Closed)
                {
                    Database.sqlConnect.Open();
                }
                Database.sqlCommand = new SqlCommand(query, Database.sqlConnect);
                Database.sqlCommand.Parameters.AddWithValue("@Key", key);
                Database.sqlRead = Database.sqlCommand.ExecuteReader();
                while (Database.sqlRead.Read())
                {
                    HoaDon hd = new HoaDon();
                    hd.ID = (int)Database.sqlRead[0];
                    hd.TenKhach = Database.sqlRead[1].ToString();
                    hd.TenSan = Database.sqlRead[2].ToString();
                    hd.SoSan = Database.sqlRead[3].ToString();
                    hd.Ngaylap = Database.sqlRead[4].ToString();
                    hd.TrangThai = Database.sqlRead[5].ToString();

                    list.Add(hd);
                }
                Database.sqlConnect.Close();
                return list;
            }
            catch (Exception ex) { throw ex; }
        }

    }
}
