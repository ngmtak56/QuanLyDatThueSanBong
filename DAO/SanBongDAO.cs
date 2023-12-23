using QuanLyDatThueSanBongNhanTao.DTO;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;
using System;


namespace QuanLyDatThueSanBongNhanTao.DAO
{
    public class SanBongDAO
    {
        public List<SanBong> GetSanBongList()
        {
            List<SanBong> list = new List<SanBong>();
            string query = "EXEC USP_GetAllSanBong";
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
                    SanBong san = new SanBong();
                    san.ID = (int)Database.sqlRead[0];
                    san.TenSan = Database.sqlRead[1].ToString();
                    san.DiaChi = Database.sqlRead[2].ToString();
                    san.SoLuong = (int)Database.sqlRead[3];
                    san.GiaThue = Convert.ToInt32(Database.sqlRead[4]);
                    san.TrangThai = Database.sqlRead[5].ToString();
                    
                    list.Add(san);
                }
                Database.sqlConnect.Close();
                return list;
            }
            catch (Exception ex) { throw ex; }
        }

        public List<SanBong> GetKhungGioSanBongList()
        {
            List<SanBong> list = new List<SanBong>();
            string query = "EXEC USP_GetKhungGioSanBongList";
            try
            {
                if(Database.sqlConnect.State == ConnectionState.Closed)
                {
                    Database.sqlConnect.Open();
                }


                Database.sqlCommand = new SqlCommand(query, Database.sqlConnect);
                Database.sqlRead = Database.sqlCommand.ExecuteReader();


                Database.sqlConnect.Close();
                return list;
            }
            catch (Exception ex) { throw ex; }
        }

        public static void ThemSanBong (SanBong san)
        {
            string query = "EXEC USP_InsertSanBong @TenSan, @DiaChi, @SoLuong, @GiaThue, @TrangThai";
            try
            {
                if(Database.sqlConnect.State == ConnectionState.Closed)
                {
                    Database.sqlConnect.Open();
                }
                Database.sqlCommand = new SqlCommand(query, Database.sqlConnect);
                Database.sqlCommand.Parameters.AddWithValue("@TenSan", san.TenSan);
                Database.sqlCommand.Parameters.AddWithValue("@DiaChi", san.DiaChi);
                Database.sqlCommand.Parameters.AddWithValue("@SoLuong", san.SoLuong);
                Database.sqlCommand.Parameters.AddWithValue("@GiaThue", san.GiaThue.ToString());
                Database.sqlCommand.Parameters.AddWithValue("@TrangThai", san.TrangThai);
                Database.sqlCommand.ExecuteNonQuery();
                MessageBox.Show("Thêm sân bóng thành công!");
                Database.sqlConnect.Close();
            }
            catch (Exception ex) { throw ex; }
        }

        public static void CapNhatSan (SanBong san)
        {
            string query = "EXEC USP_UpdateSanBong @ID, @TenSan, @DiaChi, @SoLuong, @GiaThue, @TrangThai";
            try
            {
                if (Database.sqlConnect.State == ConnectionState.Closed)
                {
                    Database.sqlConnect.Open();
                }
                Database.sqlCommand = new SqlCommand(query, Database.sqlConnect);
                Database.sqlCommand.Parameters.AddWithValue("@ID", Database.ID.ToString());
                Database.sqlCommand.Parameters.AddWithValue("@TenSan", san.TenSan);
                Database.sqlCommand.Parameters.AddWithValue("@DiaChi", san.DiaChi);
                Database.sqlCommand.Parameters.AddWithValue("@SoLuong", san.SoLuong);
                Database.sqlCommand.Parameters.AddWithValue("@GiaThue", san.GiaThue.ToString());
                Database.sqlCommand.Parameters.AddWithValue("@TrangThai", san.TrangThai);
                Database.sqlCommand.ExecuteNonQuery();
                MessageBox.Show("Cập nhật thông tin sân bóng thành công!");
                Database.sqlConnect.Close();
            }
            catch (Exception ex) { throw ex; }
        }

        public static void XoaSan (DataGridView dgv)
        {
            string query = "DELETE FROM SanBong WHERE ID LIKE '" + dgv.CurrentRow.Cells[0].Value + "'";
            try
            {
                if(Database.sqlConnect.State == ConnectionState.Closed)
                {
                    Database.sqlConnect.Open();
                }
                Database.sqlCommand = new SqlCommand(query, Database.sqlConnect);
                Database.sqlCommand.ExecuteNonQuery();
                MessageBox.Show("Sân bóng đã xóa thành công!");
                Database.sqlConnect.Close();
            }
            catch(Exception ex) { throw ex; }
        }

        public static void XoaSan (SanBong san)
        {
            string query = "EXEC USP_DeleteSanBong @ID";
            try
            {
                if (Database.sqlConnect.State == ConnectionState.Closed)
                {
                    Database.sqlConnect.Open();
                }
                Database.sqlCommand = new SqlCommand(query, Database.sqlConnect);
                Database.sqlCommand.Parameters.AddWithValue("@ID", san.ID);
                Database.sqlCommand.ExecuteNonQuery();
                Database.sqlConnect.Close();
            }
            catch (Exception ex) { throw ex; }
        }

        public List<SanBong> TimKiemSan(string key)
        {
            string query = "EXEC USP_FindSanBong @Key";
            List<SanBong> list = new List<SanBong>();
            try
            {
                if(Database.sqlConnect.State == ConnectionState.Closed)
                {
                    Database.sqlConnect.Open();
                }
                Database.sqlCommand = new SqlCommand(query, Database.sqlConnect);
                Database.sqlCommand.Parameters.AddWithValue("@Key", key);
                Database.sqlRead = Database.sqlCommand.ExecuteReader();
                while (Database.sqlRead.Read())
                {
                    SanBong temp = new SanBong();
                    temp.ID = (int)Database.sqlRead[0];
                    temp.TenSan = Database.sqlRead[1].ToString();
                    temp.DiaChi = Database.sqlRead[2].ToString();
                    temp.SoLuong = (int)Database.sqlRead[3];
                    temp.GiaThue = Convert.ToInt32(Database.sqlRead[4]);
                    temp.TrangThai = Database.sqlRead[5].ToString();

                    list.Add(temp);
                }
                /*switch (target)
                {
                    case 1:
                        Database.sqlCommand = new SqlCommand(query1, Database.sqlConnect);
                        Database.sqlCommand.Parameters.AddWithValue("@TenSan", san.TenSan);
                        Database.sqlRead = Database.sqlCommand.ExecuteReader();
                        while (Database.sqlRead.Read())
                        {
                            SanBong temp = new SanBong();
                            temp.ID = (int)Database.sqlRead[0];
                            temp.TenSan = Database.sqlRead[1].ToString();
                            temp.DiaChi = Database.sqlRead[2].ToString();
                            temp.SoLuong = (int)Database.sqlRead[3];
                            temp.GiaThue = Convert.ToInt32(Database.sqlRead[4]);
                            temp.TrangThai = Database.sqlRead[5].ToString();

                            list.Add(temp);
                        }
                        break;
                    case 2:
                        Database.sqlCommand = new SqlCommand(query2, Database.sqlConnect);
                        Database.sqlCommand.Parameters.AddWithValue("@DiaChiSan", san.DiaChi);
                        Database.sqlRead = Database.sqlCommand.ExecuteReader();
                        while (Database.sqlRead.Read())
                        {
                            SanBong temp = new SanBong();
                            temp.ID = (int)Database.sqlRead[0];
                            temp.TenSan = Database.sqlRead[1].ToString();
                            temp.DiaChi = Database.sqlRead[2].ToString();
                            temp.SoLuong = (int)Database.sqlRead[3];
                            temp.GiaThue = Convert.ToInt32(Database.sqlRead[4]);
                            temp.TrangThai = Database.sqlRead[5].ToString();

                            list.Add(temp);
                        }
                        break;
                    case 3:
                        Database.sqlCommand = new SqlCommand(query3, Database.sqlConnect);
                        Database.sqlCommand.Parameters.AddWithValue("@TrangThai", san.TrangThai);
                        Database.sqlRead = Database.sqlCommand.ExecuteReader();
                        while (Database.sqlRead.Read())
                        {
                            SanBong temp = new SanBong();
                            temp.ID = (int)Database.sqlRead[0];
                            temp.TenSan = Database.sqlRead[1].ToString();
                            temp.DiaChi = Database.sqlRead[2].ToString();
                            temp.SoLuong = (int)Database.sqlRead[3];
                            temp.GiaThue = Convert.ToInt32(Database.sqlRead[4]);
                            temp.TrangThai = Database.sqlRead[5].ToString();

                            list.Add(temp);
                        }
                        break;
                }*/
                Database.sqlConnect.Close();
                return list;
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
