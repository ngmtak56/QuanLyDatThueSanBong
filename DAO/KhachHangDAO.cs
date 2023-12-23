using QuanLyDatThueSanBongNhanTao.DTO;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;
using System;
using System.Security.Policy;
using System.Collections;

namespace QuanLyDatThueSanBongNhanTao.DAO
{
    public class KhachHangDAO
    {
        public static void LoadKhachHang(DataGridView dgv , TextBox txt)
        {
            int i = 0;
            dgv.Rows.Clear();
            try
            {
                if (Database.sqlConnect.State == ConnectionState.Closed)
                {
                    Database.sqlConnect.Open();
                }
                Database.sqlCommand = new SqlCommand("SELECT * FROM KhachHang", Database.sqlConnect);
                Database.sqlRead = Database.sqlCommand.ExecuteReader();

                while (Database.sqlRead.Read())
                {
                    i++;
                    dgv.Rows.Add(Database.sqlRead[0], i, Database.sqlRead[1], Database.sqlRead[2], Database.sqlRead[3]);
                }
                txt.Text = "Tổng khách hàng (" + i.ToString() + ")";
                Database.sqlConnect.Close();
            }
            catch (Exception ex) { ex.ToString(); }
        }

        public List<KhachHang> GetKhachHangList()
        {
            List<KhachHang> list = new List<KhachHang>();
            string query = "EXEC USP_GetAllKhachHang";
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
                    KhachHang khach = new KhachHang();
                    khach.ID = (int)Database.sqlRead[0];
                    khach.TenKhach = Database.sqlRead[1].ToString();
                    khach.DiaChi = Database.sqlRead[2].ToString();
                    khach.Sdt = Database.sqlRead[3].ToString();

                    list.Add(khach);
                }
                Database.sqlConnect.Close();
                return list;
            }
            catch (Exception ex) { throw ex; }
        }

        public List<KhachHang> GetTrongTaiList()
        {
            List<KhachHang> list = new List<KhachHang>();
            string query = "EXEC USP_GetAllTrongTai";
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
                    KhachHang khach = new KhachHang();
                    khach.ID = Convert.ToInt32(Database.sqlRead[0]);
                    khach.TenKhach = Database.sqlRead[1].ToString();
                    khach.Sdt = Database.sqlRead[2].ToString();
                    khach.GiaThue = (float)Convert.ToDouble(Database.sqlRead[3].ToString());
                    if(khach.ID > 0) 
                        list.Add(khach);
                }
                Database.sqlConnect.Close();
                return list;
            }
            catch (Exception ex) { throw ex; }
        }

        public static bool CheckKhachHang(KhachHang khach)
        {
            bool check;
            string query = "EXEC USP_GetKhachHang @TenKhack, @DiaChi, @Sdt";
            try
            {
                if(Database.sqlConnect.State == ConnectionState.Closed)
                {
                    Database.sqlConnect.Open();
                }
                Database.sqlCommand = new SqlCommand(query, Database.sqlConnect);
                Database.sqlCommand.Parameters.AddWithValue("@TenKhack", khach.TenKhach);
                Database.sqlCommand.Parameters.AddWithValue("@DiaChi", khach.DiaChi);
                Database.sqlCommand.Parameters.AddWithValue("@Sdt", khach.Sdt);
                Database.sqlRead = Database.sqlCommand.ExecuteReader();
                if(Database.sqlRead.HasRows == true)
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
            catch(Exception ex) { throw ex; }
        }

        public static void ThemKhach (KhachHang Khach)
        {
            string query = "EXEC USP_InsertKhachHang @TenKhachHang, @DiaChiKhach, @Sdt";
            try
            {
                if (Database.sqlConnect.State == ConnectionState.Closed)
                {
                    Database.sqlConnect.Open();
                }
                Database.sqlCommand = new SqlCommand(query, Database.sqlConnect);
                Database.sqlCommand.Parameters.AddWithValue("@TenKhachHang", Khach.TenKhach);
                Database.sqlCommand.Parameters.AddWithValue("@DiaChiKhach", Khach.DiaChi);
                Database.sqlCommand.Parameters.AddWithValue("@Sdt", Khach.Sdt);
                Database.sqlCommand.ExecuteNonQuery();
                Database.sqlConnect.Close();
            }
            catch (Exception ex) { throw ex; }
        }

        public static void ThemTrongTai(KhachHang trongTai)
        {
            string query = "EXEC USP_InsertTrongTai @TenTrongTai, @Sdt, @GiaThue";
            try
            {
                if (Database.sqlConnect.State == ConnectionState.Closed)
                {
                    Database.sqlConnect.Open();
                }
                Database.sqlCommand = new SqlCommand(query, Database.sqlConnect);
                Database.sqlCommand.Parameters.AddWithValue("@TenTrongTai", trongTai.TenKhach);
                Database.sqlCommand.Parameters.AddWithValue("@Sdt", trongTai.Sdt);
                Database.sqlCommand.Parameters.AddWithValue("@GiaThue", trongTai.GiaThue);
                Database.sqlCommand.ExecuteNonQuery();
                Database.sqlConnect.Close();
            }
            catch (Exception ex) { throw ex; }
        }

        public static void SuaKhach(KhachHang Khach)
        {
            /*string query = "UPDATE KhachHang SET TenKhachHang=@TenKhach, DiaChiKH=@DiaChiKhach, Sdt=@Sdt WHERE ID=@ID";*/
            string query = "EXEC USP_UpdateKhachHang @ID, @TenKhachHang, @DiaChiKhach, @Sdt";
            try
            {
                if(Database.sqlConnect.State == ConnectionState.Closed)
                {
                    Database.sqlConnect.Open();
                }
                Database.sqlCommand = new SqlCommand(query, Database.sqlConnect);
                Database.sqlCommand.Parameters.AddWithValue("@ID", Database.ID_Khach.ToString());
                Database.sqlCommand.Parameters.AddWithValue("@TenKhachHang", Khach.TenKhach);
                Database.sqlCommand.Parameters.AddWithValue("@DiaChiKhach", Khach.DiaChi);
                Database.sqlCommand.Parameters.AddWithValue("@Sdt", Khach.Sdt);
                Database.sqlCommand.ExecuteNonQuery();
                MessageBox.Show("Cập nhật thông tin khách hàng thành công!");
                Database.sqlConnect.Close();
            }
            catch (Exception ex) { throw ex; }
        }

        public static void SuaTrongTai(KhachHang trongTai)
        {
            string query = "EXEC USP_UpdateTrongTai @ID, @TenTrongTai, @Sdt, @GiaThue";
            try
            {
                if (Database.sqlConnect.State == ConnectionState.Closed)
                {
                    Database.sqlConnect.Open();
                }
                Database.sqlCommand = new SqlCommand(query, Database.sqlConnect);
                Database.sqlCommand.Parameters.AddWithValue("@ID", Database.ID_Khach.ToString());
                Database.sqlCommand.Parameters.AddWithValue("@TenTrongTai", trongTai.TenKhach);
                Database.sqlCommand.Parameters.AddWithValue("@Sdt", trongTai.Sdt);
                Database.sqlCommand.Parameters.AddWithValue("@GiaThue", trongTai.GiaThue);
                Database.sqlCommand.ExecuteNonQuery();
                MessageBox.Show("Cập nhật thông tin trọng tài thành công!");
                Database.sqlConnect.Close();
            }
            catch (Exception ex) { throw ex; }
        }

        public static void XoaKhach(DataGridView dgv)
        {
            string query = "DELETE FROM KhachHang WHERE ID LIKE '" + dgv.CurrentRow.Cells[0].Value + "'";
            try
            {
                if (Database.sqlConnect.State == ConnectionState.Closed)
                {
                    Database.sqlConnect.Open();
                }
                Database.sqlCommand = new SqlCommand(query, Database.sqlConnect);
                Database.sqlCommand.ExecuteNonQuery();
                MessageBox.Show("Khách hàng đã xóa thành công!");
                Database.sqlConnect.Close();
            }
            catch(Exception ex) { throw ex; }
        }

        public static void XoaKhach(KhachHang khach)
        {
            string query = "EXEC USP_DeleteKhachHang @ID";
            try
            {
                if (Database.sqlConnect.State == ConnectionState.Closed)
                {
                    Database.sqlConnect.Open();
                }
                Database.sqlCommand = new SqlCommand(query, Database.sqlConnect);
                Database.sqlCommand.Parameters.AddWithValue("ID", khach.ID);
                Database.sqlCommand.ExecuteNonQuery();
                Database.sqlConnect.Close();
            }
            catch (Exception ex) { throw ex; }
        }

        public static void XoaTrongTai(int ID)
        {
            string query = "EXEC USP_DeleteTrongTai @ID";
            try
            {
                if (Database.sqlConnect.State == ConnectionState.Closed)
                {
                    Database.sqlConnect.Open();
                }
                Database.sqlCommand = new SqlCommand(query, Database.sqlConnect);
                Database.sqlCommand.Parameters.AddWithValue("ID", ID);
                Database.sqlCommand.ExecuteNonQuery();
                Database.sqlConnect.Close();
            }
            catch (Exception ex) { throw ex; }
        }

        public List<KhachHang> TimKiemKhach(string key)
        {
            string query = "EXEC USP_FindKhachHang @Key";
            KhachHang temp;
            List<KhachHang> list = new List<KhachHang>();
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
                    temp = new KhachHang();
                    temp.ID = (int)Database.sqlRead[0];
                    temp.TenKhach = Database.sqlRead[1].ToString();
                    temp.DiaChi = Database.sqlRead[2].ToString();
                    temp.Sdt = Database.sqlRead[3].ToString();

                    list.Add(temp);
                }
                /*switch (target)
                {
                    case 1:
                        Database.sqlCommand = new SqlCommand(query1, Database.sqlConnect);
                        Database.sqlCommand.Parameters.AddWithValue("@TenKhach", khach.TenKhach);
                        Database.sqlRead = Database.sqlCommand.ExecuteReader();
                        while (Database.sqlRead.Read())
                        {
                            temp = new KhachHang();
                            temp.ID = (int)Database.sqlRead[0];
                            temp.TenKhach = Database.sqlRead[1].ToString();
                            temp.DiaChi = Database.sqlRead[2].ToString();
                            temp.Sdt = Database.sqlRead[3].ToString();

                            list.Add(temp);
                        }
                        break;
                    case 2:
                        Database.sqlCommand = new SqlCommand(query2, Database.sqlConnect);
                        Database.sqlCommand.Parameters.AddWithValue("@DiaChi", khach.DiaChi);
                        Database.sqlRead = Database.sqlCommand.ExecuteReader();
                        while (Database.sqlRead.Read())
                        {
                            temp = new KhachHang();
                            temp.ID = (int)Database.sqlRead[0];
                            temp.TenKhach = Database.sqlRead[1].ToString();
                            temp.DiaChi = Database.sqlRead[2].ToString();
                            temp.Sdt = Database.sqlRead[3].ToString();

                            list.Add(temp);
                        }
                        break;
                    case 3:
                        Database.sqlCommand = new SqlCommand(query3, Database.sqlConnect);
                        Database.sqlCommand.Parameters.AddWithValue("@Sdt", khach.Sdt);
                        Database.sqlRead = Database.sqlCommand.ExecuteReader();
                        while (Database.sqlRead.Read())
                        {
                            temp = new KhachHang();
                            temp.ID = (int)Database.sqlRead[0];
                            temp.TenKhach = Database.sqlRead[1].ToString();
                            temp.DiaChi = Database.sqlRead[2].ToString();
                            temp.Sdt = Database.sqlRead[3].ToString();

                            list.Add(temp);
                        }
                        break;
                }*/
                Database.sqlConnect.Close();
                return list;
            }
            catch (Exception ex) { throw ex; }
        }

        public static int CheckTrungLapSoDienThoai(string sdt)
        {
            int check = 0;
            string query = "EXEC USP_CheckTrungLapKhachHangSdt @Sdt";
            try
            {
                if (Database.sqlConnect.State == ConnectionState.Closed)
                {
                    Database.sqlConnect.Open();
                }
                Database.sqlCommand = new SqlCommand(query, Database.sqlConnect);
                Database.sqlCommand.Parameters.AddWithValue("@Sdt", sdt);
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

        public static int CheckTrungLapSoDienThoaiTT(string sdt)
        {
            int check = 0;
            string query = "EXEC USP_CheckTrungLapTrongTaiSdt @Sdt";
            try
            {
                if (Database.sqlConnect.State == ConnectionState.Closed)
                {
                    Database.sqlConnect.Open();
                }
                Database.sqlCommand = new SqlCommand(query, Database.sqlConnect);
                Database.sqlCommand.Parameters.AddWithValue("@Sdt", sdt);
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
