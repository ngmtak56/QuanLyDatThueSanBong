using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyDatThueSanBongNhanTao
{
    public partial class fInHoaDon : Form
    {
        public fInHoaDon()
        {
            InitializeComponent();
        }

        private void fInHoaDon_Load(object sender, EventArgs e)
        {

        }

        private void InHoaDon_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("Hóa Đơn", new Font("Calibri", 14, FontStyle.Regular), Brushes.Black, new Point(10,10));
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
           /* printPreviewDialog1.Document = InHoaDon;
            printPreviewDialog1.Name = "Hóa Đơn";
            printPreviewDialog1.ShowDialog();*/
        }
    }
}
