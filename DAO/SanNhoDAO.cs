using QuanLyDatThueSanBongNhanTao.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDatThueSanBongNhanTao.DAO
{
    class SanNhoDAO
    {
        public List<SanNho> GetSanNho(int ID)
        {
            List<SanNho> list = new List<SanNho>();
            string query = "EXEC USP_GetAllSanNho @IDSan";
            try
            {
                if (Database.sqlConnect.State == ConnectionState.Closed)
                {
                    Database.sqlConnect.Open();
                }
                Database.sqlCommand = new SqlCommand(query, Database.sqlConnect);
                Database.sqlCommand.Parameters.AddWithValue("@IDSan", ID);
                Database.sqlRead = Database.sqlCommand.ExecuteReader();
                while (Database.sqlRead.Read())
                {
                    SanNho san = new SanNho();
                    san.ID = (int)Database.sqlRead[0];
                    san.SoSan = Database.sqlRead[1].ToString();
                    san.MoTa = Database.sqlRead[2].ToString();

                    list.Add(san);
                }
                Database.sqlConnect.Close();
                return list;
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
