﻿using QuanLyDatThueSanBongNhanTao.DAO;
using System.Windows.Forms;
using System;


namespace QuanLyDatThueSanBongNhanTao
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new fDangNhap());
        }
    }
}
