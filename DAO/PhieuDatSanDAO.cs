using QuanLyDatThueSanBongNhanTao.DTO;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;
using System;


namespace QuanLyDatThueSanBongNhanTao.DAO
{
    public class PhieuDatSanDAO
    {
        public static void LoadDatSan(DataGridView dgv)
        {
            int i = 0;
            string query = "EXEC USP_GetAllDatSan";

            dgv.Rows.Clear();
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
                    i++;
                    dgv.Rows.Add(Database.sqlRead[0], i, Database.sqlRead[1], Database.sqlRead[2], Database.sqlRead[3], Database.sqlRead[4], Database.sqlRead[5], Database.sqlRead[6]);
                }

                Database.sqlConnect.Close();
            }
            catch (Exception ex) { throw ex; }
        }

        public List<PhieuDatSan> GetDatSanList()
        {
            List<PhieuDatSan> list = new List<PhieuDatSan>();   
            string query = "EXEC USP_GetAllDatSan";
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
                    PhieuDatSan pds = new PhieuDatSan();
                    pds.ID = (int)Database.sqlRead[0];
                    pds.TenSan = Database.sqlRead[1].ToString();
                    pds.SanNho = Database.sqlRead[2].ToString();
                    pds.TenKhach = Database.sqlRead[3].ToString();
                    pds.NgayDat = Database.sqlRead[4].ToString();
                    pds.KhungGio = Database.sqlRead[5].ToString();
                    pds.GhiChu = Database.sqlRead[6].ToString();

                    list.Add(pds);
                }
            Database.sqlConnect.Close();
            return list;
            }
            catch(Exception ex) { throw ex; }
        }

        public List<PhieuDatSan> GetVeThanglist()
        {
            List<PhieuDatSan> list = new List<PhieuDatSan>();
            string query = "EXEC USP_GetAllVeThang";
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
                    PhieuDatSan pds = new PhieuDatSan();
                    pds.ID = (int)Database.sqlRead[0];
                    pds.TenKhach = Database.sqlRead[1].ToString();
                    pds.TenSan = Database.sqlRead[2].ToString();
                    pds.SanNho = Database.sqlRead[3].ToString();
                    pds.Thang = Database.sqlRead[4].ToString();
                    pds.Nam = Database.sqlRead[5].ToString();
                    pds.KhungGio = Database.sqlRead[6].ToString();
                    pds.Thu = Database.sqlRead[7].ToString();
                    pds.TrangThai = Database.sqlRead[8].ToString();

                    list.Add(pds);
                }
                Database.sqlConnect.Close();
                return list;
            }
            catch (Exception ex) { throw ex; }
        }

        public List<SanBong> GetComboBoxSan()
        {
            List<SanBong> items = new List<SanBong>();
            string queryGetSan = "EXEC USP_GetTenSanConTrong";
            try
            {
                if (Database.sqlConnect.State == ConnectionState.Closed)
                {
                    Database.sqlConnect.Open();
                }

                Database.sqlCommand = new SqlCommand(queryGetSan, Database.sqlConnect);
                Database.sqlRead = Database.sqlCommand.ExecuteReader();
                while (Database.sqlRead.Read())
                {
                    SanBong sb = new SanBong();
                    sb.TenSan = Database.sqlRead["TenSan"].ToString();
                    items.Add(sb);
                }
                Database.sqlConnect.Close();
                return items;
            }
            catch (Exception ex) { throw ex; }
        }

        public List<KhachHang> GetComboBoxKhach()
        {
            List<KhachHang> items = new List<KhachHang>();
            string queryGetKhach = "EXEC USP_GetTenKhach";
            try
            {
                if (Database.sqlConnect.State == ConnectionState.Closed)
                {
                    Database.sqlConnect.Open();
                }

                Database.sqlCommand = new SqlCommand(queryGetKhach, Database.sqlConnect);
                Database.sqlRead = Database.sqlCommand.ExecuteReader();
                while (Database.sqlRead.Read())
                {
                    KhachHang kh = new KhachHang();
                    kh.TenKhach = Database.sqlRead["TenKhachHang"].ToString();
                    items.Add(kh);
                }
                Database.sqlConnect.Close();
                return items;
            }
            catch (Exception ex) { throw ex; }
        }

        public static void ThemPhieuDatSan(PhieuDatSan pds, int target, int SoBuoi)
        {
            string query = "EXEC USP_InsertDatSan @TenSan, @SoSan, @TenKH, @TrongTai, @NgayDat, @KhungGio, @GhiChu";
            try
            {
                if (Database.sqlConnect.State == ConnectionState.Closed)
                {
                    Database.sqlConnect.Open();
                }
                
                switch (target)
                {
                    case 1:
                        Database.sqlCommand = new SqlCommand(query, Database.sqlConnect);
                        Database.sqlCommand.Parameters.AddWithValue("@TenSan", pds.TenSan);
                        Database.sqlCommand.Parameters.AddWithValue("@SoSan", pds.SanNho);
                        Database.sqlCommand.Parameters.AddWithValue("@TenKH", pds.TenKhach);
                        Database.sqlCommand.Parameters.AddWithValue("@TrongTai", pds.TrongTai);
                        Database.sqlCommand.Parameters.AddWithValue("@NgayDat", pds.NgayDat);
                        Database.sqlCommand.Parameters.AddWithValue("@KhungGio", pds.KhungGio);
                        Database.sqlCommand.Parameters.AddWithValue("@GhiChu", pds.GhiChu);
                        Database.sqlCommand.ExecuteNonQuery();
                        MessageBox.Show("Đặt sân thành công!");
                        break;
                    case 2:
                        for (int i = 0; i < SoBuoi; i++)
                        {
                            DateTime tmp = Database.time.AddDays(i * 7);
                            pds.NgayDat = string.Empty;
                            pds.NgayDat = Convert.ToString(tmp);
                            Database.sqlCommand = new SqlCommand(query, Database.sqlConnect);
                            Database.sqlCommand.Parameters.AddWithValue("@TenSan", pds.TenSan);
                            Database.sqlCommand.Parameters.AddWithValue("@SoSan", pds.SanNho);
                            Database.sqlCommand.Parameters.AddWithValue("@TenKH", pds.TenKhach);
                            Database.sqlCommand.Parameters.AddWithValue("@TrongTai", pds.TrongTai);
                            Database.sqlCommand.Parameters.AddWithValue("@NgayDat", pds.NgayDat);
                            Database.sqlCommand.Parameters.AddWithValue("@KhungGio", pds.KhungGio);
                            Database.sqlCommand.Parameters.AddWithValue("@GhiChu", pds.GhiChu);
                            Database.sqlCommand.ExecuteNonQuery();
                        }
                        break;
                    case 3:
                        for(int i = 0; i < SoBuoi; i++)
                        {
                            DateTime tmp = Database.time.AddMonths(i);
                            pds.NgayDat = string.Empty;
                            pds.NgayDat = Convert.ToString(tmp);
                            Database.sqlCommand = new SqlCommand(query, Database.sqlConnect);
                            Database.sqlCommand.Parameters.AddWithValue("@TenSan", pds.TenSan);
                            Database.sqlCommand.Parameters.AddWithValue("@SoSan", pds.SanNho);
                            Database.sqlCommand.Parameters.AddWithValue("@TenKH", pds.TenKhach);
                            Database.sqlCommand.Parameters.AddWithValue("@TrongTai", pds.TrongTai);
                            Database.sqlCommand.Parameters.AddWithValue("@NgayDat", pds.NgayDat);
                            Database.sqlCommand.Parameters.AddWithValue("@KhungGio", pds.KhungGio);
                            Database.sqlCommand.Parameters.AddWithValue("@GhiChu", pds.GhiChu);
                            Database.sqlCommand.ExecuteNonQuery();
                        }
                        break;
                }
                Database.sqlConnect.Close();

            }
            catch (Exception ex) { throw ex; }
        }

        public static void ThemVeThang(PhieuDatSan pds)
        {
            string query = "EXEC USP_InsertVeThang @TenSan, @SoSan, @TenKH, @Thang, @KhungGio, @Thu";
            try
            {
                if (Database.sqlConnect.State == ConnectionState.Closed)
                {
                    Database.sqlConnect.Open();
                }
                Database.sqlCommand = new SqlCommand(query, Database.sqlConnect);
                Database.sqlCommand.Parameters.AddWithValue("@TenSan", pds.TenSan);
                Database.sqlCommand.Parameters.AddWithValue("@SoSan", pds.SanNho);
                Database.sqlCommand.Parameters.AddWithValue("@TenKH", pds.TenKhach);
                Database.sqlCommand.Parameters.AddWithValue("@Thang", pds.Thang);
                Database.sqlCommand.Parameters.AddWithValue("@KhungGio", pds.KhungGio);
                Database.sqlCommand.Parameters.AddWithValue("@Thu", pds.Thu);
                Database.sqlCommand.ExecuteNonQuery();
                MessageBox.Show("Đăng ký vé tháng cho khách hàng cho khách hàng " + pds.TenKhach + " thành công!");
                Database.sqlConnect.Close();
            }
            catch (Exception ex) { throw ex; }
        }

        public static void UpdateVeThang(PhieuDatSan pds)
        {
            string query = "EXEC USP_UpdateVeThang @ID, @TenSan, @SoSan, @TenKH, @Thang, @KhungGio, @Thu, @TrangThai";
            try
            {
                if (Database.sqlConnect.State == ConnectionState.Closed)
                {
                    Database.sqlConnect.Open();
                }
                Database.sqlCommand = new SqlCommand(query, Database.sqlConnect);
                Database.sqlCommand.Parameters.AddWithValue("@ID", Database.ID_VeThang);
                Database.sqlCommand.Parameters.AddWithValue("@TenSan", pds.TenSan);
                Database.sqlCommand.Parameters.AddWithValue("@SoSan", pds.SanNho);
                Database.sqlCommand.Parameters.AddWithValue("@TenKH", pds.TenKhach);
                Database.sqlCommand.Parameters.AddWithValue("@Thang", pds.Thang);
                Database.sqlCommand.Parameters.AddWithValue("@KhungGio", pds.KhungGio);
                Database.sqlCommand.Parameters.AddWithValue("@Thu", pds.Thu);
                Database.sqlCommand.Parameters.AddWithValue("@TrangThai", pds.TrangThai);
                Database.sqlCommand.ExecuteNonQuery();
                MessageBox.Show("Cập nhật vé tháng cho khách hàng cho khách hàng " + pds.TenKhach + " thành công!");
                Database.sqlConnect.Close();
            }
            catch (Exception ex) { throw ex; }
        }

        public static void HuyPhieuDatSan(PhieuDatSan pds)
        {
            string query = "EXEC USP_DeleteDatSan @ID";
            try
            {
                if (Database.sqlConnect.State == ConnectionState.Closed)
                {
                    Database.sqlConnect.Open();
                }
                Database.sqlCommand = new SqlCommand(query, Database.sqlConnect);
                Database.sqlCommand.Parameters.AddWithValue("@ID", pds.ID);
                Database.sqlCommand.ExecuteNonQuery();
                Database.sqlConnect.Close();
            }
            catch (Exception ex) { throw ex; }
        }

        public static void DeleteVeThang(PhieuDatSan pds)
        {
            string query = "EXEC USP_DeleteVeThang @ID";
            try
            {
                if (Database.sqlConnect.State == ConnectionState.Closed)
                {
                    Database.sqlConnect.Open();
                }
                Database.sqlCommand = new SqlCommand(query, Database.sqlConnect);
                Database.sqlCommand.Parameters.AddWithValue("@ID", pds.ID);
                Database.sqlCommand.ExecuteNonQuery();
                Database.sqlConnect.Close();
            }
            catch (Exception ex) { throw ex; }
        }

        public static string CheckHoaDonDatSan(PhieuDatSan pds)
        {
            string TrangThai;
            string query = "EXEC USP_GetTrangThaiHoaDonTuDatSan @ID";
            try
            {
                if (Database.sqlConnect.State == ConnectionState.Closed)
                {
                    Database.sqlConnect.Open();
                }
                Database.sqlCommand = new SqlCommand(query, Database.sqlConnect);
                Database.sqlCommand.Parameters.AddWithValue("@ID", pds.ID);
                Database.sqlRead = Database.sqlCommand.ExecuteReader();
                TrangThai = Database.sqlRead.ToString();
                /*if(TrangThai == "Đã thanh toán")
                {
                    check = true;
                }
                else if(TrangThai == "Chưa thanh toán")
                {
                    check = false;
                }*/
                Database.sqlConnect.Close();
                return TrangThai;
            }
            catch(Exception ex) { throw ex; }
        }

        public static bool CheckTrangThaiKhungGio(PhieuDatSan pds)
        {
            bool tmp;
            string query = "EXEC USP_GetTrangThaiKhungGioSanBong @IDSoSan , @KhungGio";
            try
            {
                if (Database.sqlConnect.State == ConnectionState.Closed)
                {
                    Database.sqlConnect.Open();
                }
                Database.sqlCommand = new SqlCommand(query, Database.sqlConnect);
                Database.sqlCommand.Parameters.AddWithValue("@IDSoSan", pds.ID);
                Database.sqlCommand.Parameters.AddWithValue("@KhungGio", pds.KhungGio);
                Database.sqlRead = Database.sqlCommand.ExecuteReader();
                tmp = Convert.ToBoolean(Database.sqlRead);
                
                Database.sqlConnect.Close();
                return tmp;
            }
            catch (Exception ex) { throw ex; }
        }

        public static int CheckTrungLapDatSan(string date, string time)
        {
            int check = 0;
            string query = "EXEC USP_CheckTrungLapDatSan @NgayDat, @KhungGio";
            try
            {
                if (Database.sqlConnect.State == ConnectionState.Closed)
                {
                    Database.sqlConnect.Open();
                }
                Database.sqlCommand = new SqlCommand(query, Database.sqlConnect);
                Database.sqlCommand.Parameters.AddWithValue("@NgayDat", date);
                Database.sqlCommand.Parameters.AddWithValue("@KhungGio", time);
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

        public static int CheckTrungLapDatSanTuan(string date, string time)
        {
            int check = 0;
            string query = "EXEC USP_CheckTrungLapDatSan @NgayDat, @KhungGio";
            try
            {
                if (Database.sqlConnect.State == ConnectionState.Closed)
                {
                    Database.sqlConnect.Open();
                }
                Database.sqlCommand = new SqlCommand(query, Database.sqlConnect);
                Database.sqlCommand.Parameters.AddWithValue("@NgayDat", date);
                Database.sqlCommand.Parameters.AddWithValue("@KhungGio", time);
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
