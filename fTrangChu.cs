using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyDatThueSanBongNhanTao
{
    public partial class fTrangChu : Form
    {
        public int ImageNumber = 1;
        public fTrangChu()
        {
            InitializeComponent();
        }

        private void fTrangChu_Load(object sender, System.EventArgs e)
        {

        }

        public void LoadNextImages()
        {
            timer1.Start();
            ImageNumber++;
            if (ImageNumber > 3)
            {
                ImageNumber = 1;
            }

            pnlDesktop.BackgroundImage = Image.FromFile(string.Format(@"E:\NGHIA\C#\BTL_C#\QuanLyDatThueSanBongNhanTao\QuanLyDatThueSanBongNhanTao\Intro\Ball" + ImageNumber + ".jpeg"));
            pnlDesktop.BackgroundImageLayout = ImageLayout.Zoom;
        }

        public void LoadPreviousImages()
        {
            timer1.Start();
            ImageNumber--;
            if (ImageNumber < 1)
            {
                ImageNumber = 3;
            }
            pnlDesktop.BackgroundImage = Image.FromFile(string.Format(@"E:\NGHIA\C#\BTL_C#\QuanLyDatThueSanBongNhanTao\QuanLyDatThueSanBongNhanTao\Intro\Ball" + ImageNumber + ".jpeg"));
            pnlDesktop.BackgroundImageLayout = ImageLayout.Zoom;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            LoadNextImages();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            LoadNextImages();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            LoadPreviousImages();
        }
    }
}
