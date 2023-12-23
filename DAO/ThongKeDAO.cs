using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace QuanLyDatThueSanBongNhanTao.DAO
{
    public class ThongKeDAO
    {
        public float DoanhThuTheoNgay(string date)
        {
            float tien = 0;
            string query = "EXEC USP_DoanhThuTheoNgay @Date";
            try
            {
                if (Database.sqlConnect.State == ConnectionState.Closed)
                {
                    Database.sqlConnect.Open();
                }
                Database.sqlCommand = new SqlCommand(query, Database.sqlConnect);
                Database.sqlCommand.Parameters.AddWithValue("@Date", date);
                object tmp = Database.sqlCommand.ExecuteScalar();
                if (tmp != null)
                {
                    tien = (float)Convert.ToDouble((tmp));
                }
                else if(tmp == null) { tien = 0; }
                Database.sqlConnect.Close();
                return tien;
            }
            catch (Exception ex) { throw ex; }
        }

        public float DoanhThuTheoThang(string date)
        {
            float tien = 0;
            string query = "EXEC USP_DoanhThuTheoThang @Date";
            try
            {
                if (Database.sqlConnect.State == ConnectionState.Closed)
                {
                    Database.sqlConnect.Open();
                }
                Database.sqlCommand = new SqlCommand(query, Database.sqlConnect);
                Database.sqlCommand.Parameters.AddWithValue("@Date", date);
                object tmp = Database.sqlCommand.ExecuteScalar();
                if (tmp != null)
                {
                    tien = (float)Convert.ToDouble((tmp));
                }
                else { tien = 0; }
                Database.sqlConnect.Close();
                return tien;
            }
            catch (Exception ex) { throw ex; }
        }

        public float DoanhThuTheoNam(string date)
        {
            float tien = 0;
            string query = "EXEC USP_DoanhThuTheoNam @Date";
            try
            {
                if (Database.sqlConnect.State == ConnectionState.Closed)
                {
                    Database.sqlConnect.Open();
                }
                Database.sqlCommand = new SqlCommand(query, Database.sqlConnect);
                Database.sqlCommand.Parameters.AddWithValue("@Date", date);
                object tmp = Database.sqlCommand.ExecuteScalar();
                if (tmp != null)
                {
                    tien = (float)Convert.ToDouble((tmp));
                }
                else { tien = 0; }
                Database.sqlConnect.Close();
                return tien;
            }
            catch (Exception ex) { throw ex; }
        }

        public int SoLanDatSanTheoNgay(string date)
        {
            int lan = 0;
            string query = "EXEC USP_SoLanDatSanTheoNgay @Date";
            try
            {
                if (Database.sqlConnect.State == ConnectionState.Closed)
                {
                    Database.sqlConnect.Open();
                }
                Database.sqlCommand = new SqlCommand(query, Database.sqlConnect);
                Database.sqlCommand.Parameters.AddWithValue("@Date", date);
                object tmp = Database.sqlCommand.ExecuteScalar();
                if (tmp != null)
                {
                    lan = Convert.ToInt32((tmp));
                }
                else lan = 0;
                Database.sqlConnect.Close();
                return lan;
            }
            catch (Exception ex) { throw ex; }
        }

        public int SoLanDatSanTheoThang(string date)
        {
            int lan = 0;
            string query = "EXEC USP_SoLanDatSanTheoThang @Date";
            try
            {
                if (Database.sqlConnect.State == ConnectionState.Closed)
                {
                    Database.sqlConnect.Open();
                }
                Database.sqlCommand = new SqlCommand(query, Database.sqlConnect);
                Database.sqlCommand.Parameters.AddWithValue("@Date", date);
                object tmp = Database.sqlCommand.ExecuteScalar();
                if (tmp != null)
                {
                    lan = Convert.ToInt32((tmp));
                }
                else lan = 0;
                Database.sqlConnect.Close();
                return lan;
            }
            catch (Exception ex) { throw ex; }
        }

        public int SoLanDatSanTheoNam(string date)
        {
            int lan = 0;
            string query = "EXEC USP_SoLanDatSanTheoNam @Date";
            try
            {
                if (Database.sqlConnect.State == ConnectionState.Closed)
                {
                    Database.sqlConnect.Open();
                }
                Database.sqlCommand = new SqlCommand(query, Database.sqlConnect);
                Database.sqlCommand.Parameters.AddWithValue("@Date", date);
                object tmp = Database.sqlCommand.ExecuteScalar();
                if (tmp != null)
                {
                    lan = Convert.ToInt32((tmp));
                }
                else lan = 0;
                Database.sqlConnect.Close();
                return lan;
            }
            catch (Exception ex) { throw ex; }
        }

        public float DoanhThuDichVuTheoNgay(string date)
        {
            float tien = 0;
            string query = "EXEC USP_DoanhThuDichVuTheoNgay @Date";
            try
            {
                if (Database.sqlConnect.State == ConnectionState.Closed)
                {
                    Database.sqlConnect.Open();
                }
                Database.sqlCommand = new SqlCommand(query, Database.sqlConnect);
                Database.sqlCommand.Parameters.AddWithValue("@Date", date);
                object tmp = Database.sqlCommand.ExecuteScalar();
                if (tmp != null)
                {
                    tien = (float)Convert.ToDouble((tmp));
                }
                else if (tmp == null) { tien = 0; }
                Database.sqlConnect.Close();
                return tien;
            }
            catch (Exception ex) { throw ex; }
        }

        public float DoanhThuDichVuTheoThang(string date)
        {
            float tien = 0;
            string query = "EXEC USP_DoanhThuDichVuTheoThang @Date";
            try
            {
                if (Database.sqlConnect.State == ConnectionState.Closed)
                {
                    Database.sqlConnect.Open();
                }
                Database.sqlCommand = new SqlCommand(query, Database.sqlConnect);
                Database.sqlCommand.Parameters.AddWithValue("@Date", date);
                object tmp = Database.sqlCommand.ExecuteScalar();
                if (tmp != null)
                {
                    tien = (float)Convert.ToDouble((tmp));
                }
                else if (tmp == null) { tien = 0; }
                Database.sqlConnect.Close();
                return tien;
            }
            catch (Exception ex) { throw ex; }
        }

        public float DoanhThuDichVuTheoNam(string date)
        {
            float tien = 0;
            string query = "EXEC USP_DoanhThuDichVuTheoNam @Date";
            try
            {
                if (Database.sqlConnect.State == ConnectionState.Closed)
                {
                    Database.sqlConnect.Open();
                }
                Database.sqlCommand = new SqlCommand(query, Database.sqlConnect);
                Database.sqlCommand.Parameters.AddWithValue("@Date", date);
                object tmp = Database.sqlCommand.ExecuteScalar();
                if (tmp != null)
                {
                    tien = (float)Convert.ToDouble((tmp));
                }
                else if (tmp == null) { tien = 0; }
                Database.sqlConnect.Close();
                return tien;
            }
            catch (Exception ex) { throw ex; }
        }

        public int SoKhachHangTheoNgay(string date)
        {
            int lan = 0;
            string query = "EXEC USP_SoluongKhachHangTheoNgay @Date";
            try
            {
                if (Database.sqlConnect.State == ConnectionState.Closed)
                {
                    Database.sqlConnect.Open();
                }
                Database.sqlCommand = new SqlCommand(query, Database.sqlConnect);
                Database.sqlCommand.Parameters.AddWithValue("@Date", date);
                object tmp = Database.sqlCommand.ExecuteScalar();
                if (tmp != null)
                {
                    lan = Convert.ToInt32((tmp));
                }
                else lan = 0;
                Database.sqlConnect.Close();
                return lan;
            }
            catch (Exception ex) { throw ex; }
        }

        public int SoKhachHangDVTheoNgay(string date)
        {
            int lan = 0;
            string query = "EXEC USP_SoluongKhachHangDVTheoNgay @Date";
            try
            {
                if (Database.sqlConnect.State == ConnectionState.Closed)
                {
                    Database.sqlConnect.Open();
                }
                Database.sqlCommand = new SqlCommand(query, Database.sqlConnect);
                Database.sqlCommand.Parameters.AddWithValue("@Date", date);
                object tmp = Database.sqlCommand.ExecuteScalar();
                if (tmp != null)
                {
                    lan = Convert.ToInt32((tmp));
                }
                else lan = 0;
                Database.sqlConnect.Close();
                return lan;
            }
            catch (Exception ex) { throw ex; }
        }


        public int SoKhachHangTheoThang(string date)
        {
            int lan = 0;
            string query = "EXEC USP_SoluongKhachHangTheoThang @Date";
            try
            {
                if (Database.sqlConnect.State == ConnectionState.Closed)
                {
                    Database.sqlConnect.Open();
                }
                Database.sqlCommand = new SqlCommand(query, Database.sqlConnect);
                Database.sqlCommand.Parameters.AddWithValue("@Date", date);
                object tmp = Database.sqlCommand.ExecuteScalar();
                if (tmp != null)
                {
                    lan = Convert.ToInt32((tmp));
                }
                else lan = 0;
                Database.sqlConnect.Close();
                return lan;
            }
            catch (Exception ex) { throw ex; }
        }

        public int SoKhachHangDVTheoThang(string date)
        {
            int lan = 0;
            string query = "EXEC USP_SoluongKhachHangDVTheoThang @Date";
            try
            {
                if (Database.sqlConnect.State == ConnectionState.Closed)
                {
                    Database.sqlConnect.Open();
                }
                Database.sqlCommand = new SqlCommand(query, Database.sqlConnect);
                Database.sqlCommand.Parameters.AddWithValue("@Date", date);
                object tmp = Database.sqlCommand.ExecuteScalar();
                if (tmp != null)
                {
                    lan = Convert.ToInt32((tmp));
                }
                else lan = 0;
                Database.sqlConnect.Close();
                return lan;
            }
            catch (Exception ex) { throw ex; }
        }


        public int SoKhachHangTheoNam(string date)
        {
            int lan = 0;
            string query = "EXEC USP_SoluongKhachHangTheoNam @Date";
            try
            {
                if (Database.sqlConnect.State == ConnectionState.Closed)
                {
                    Database.sqlConnect.Open();
                }
                Database.sqlCommand = new SqlCommand(query, Database.sqlConnect);
                Database.sqlCommand.Parameters.AddWithValue("@Date", date);
                object tmp = Database.sqlCommand.ExecuteScalar();
                if (tmp != null)
                {
                    lan = Convert.ToInt32((tmp));
                }
                else lan = 0;
                Database.sqlConnect.Close();
                return lan;
            }
            catch (Exception ex) { throw ex; }
        }

        public int SoKhachHangDVTheoNam(string date)
        {
            int lan = 0;
            string query = "EXEC USP_SoluongKhachHangDVTheoNam @Date";
            try
            {
                if (Database.sqlConnect.State == ConnectionState.Closed)
                {
                    Database.sqlConnect.Open();
                }
                Database.sqlCommand = new SqlCommand(query, Database.sqlConnect);
                Database.sqlCommand.Parameters.AddWithValue("@Date", date);
                object tmp = Database.sqlCommand.ExecuteScalar();
                if (tmp != null)
                {
                    lan = Convert.ToInt32((tmp));
                }
                else lan = 0;
                Database.sqlConnect.Close();
                return lan;
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
