using System.Data.SqlClient;
using System.Data;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyDatThueSanBongNhanTao
{
    public class Database
    {
        public static fHome home = new fHome();
        fKhachHang khach = new fKhachHang();
        fSanBong san = new fSanBong();

        public static string path = @"Data Source=LAPTOP-1Q49FOPR;Initial Catalog=QLDatThueSanBong;Integrated Security=True";
        public static SqlConnection sqlConnect = new SqlConnection(path);
        public Database() { }

        public static SqlCommand sqlCommand = new SqlCommand("", sqlConnect);
        public static SqlDataAdapter sqlAdapt = new SqlDataAdapter();
        public static SqlDataReader sqlRead = null;

        public static int ID = 0;
        public static int ID_San = 0;
        public static int ID_Khach = 0;
        public static int ID_DatSan = 0;
        public static int ID_HoaDon = 0;
        public static int ID_HoaDonDV = 0;
        public static int ID_VeThang = 0;
        public static int ID_Kho = 0;
        public static DateTime time;
    }
}
