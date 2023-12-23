using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;
using System;
using System.Collections.Generic;

namespace QuanLyDatThueSanBongNhanTao
{
    public partial class fHome : Form
    {
        private Form activeForm;
        public int ImageNumber = 1;
        private int borderSize = 2;

        public fHome()
        {
            InitializeComponent();
            CollapseMenu();
            this.Padding = new Padding(borderSize);
            this.BackColor = Color.FromArgb(254, 245, 237);
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
        }

        public void OpenChildForm(Form childForm, object btnSender)
        {
            if(activeForm != null)
            {
                activeForm.Close();
            }
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            /*childForm.BackColor = Color.FromArgb(211, 228, 205);*/
            childForm.BackColor = Color.FromArgb(254, 245, 237);
            this.pnlDesktop.Controls.Add(childForm);
            this.pnlDesktop.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            lblTitle.Text = childForm.Text;
        }

        private void AdjustForm()
        {
            switch (this.WindowState)
            {
                case FormWindowState.Maximized:
                    this.Padding = new Padding(0, 8, 8, 0);
                    break;
                case FormWindowState.Normal:
                    if (this.Padding.Top != borderSize)
                        this.Padding = new Padding(borderSize);
                    break;
            }
        }

        private void CollapseMenu()
        {
            if (this.pnlMenu.Width == 184)
            {
                this.pnlMenu.Width = 80;
                pboxTitleMenu.Visible = false;
                btnMenu.Dock = DockStyle.Fill;
                foreach (Button menuBtn in pnlMenu.Controls.OfType<Button>())
                {
                    menuBtn.Text = "";
                    menuBtn.ImageAlign = ContentAlignment.MiddleCenter;
                    menuBtn.Padding = new Padding(0);
                }
            }
            else
            {
                this.pnlMenu.Width = 184;
                pboxTitleMenu.Visible = true;
                btnMenu.Dock = DockStyle.Right;
                foreach (Button menuBtn in pnlMenu.Controls.OfType<Button>())
                {
                    menuBtn.Text = "    " + menuBtn.Tag.ToString();
                    menuBtn.ImageAlign = ContentAlignment.MiddleLeft;
                    menuBtn.Padding = new Padding(22, 0, 0, 0);
                }
            }
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hand, int wMsg, int wParam, int lParam);

        private void pnlTitle_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_NCCALCSIZE = 0x0083;

            const int WM_NCHITTEST = 0x0084;
            const int resizeAreaSize = 10;

            const int HTCLIENT = 1;
            const int HTLEFT = 10;
            const int HTRIGHT = 11;
            const int HTTOP = 12;
            const int HTTOPLEFT = 13;
            const int HTTOPRIGHT = 14;
            const int HTBOTTOM = 15;
            const int HTBOTTOMLEFT = 16;
            const int HTBOTTOMRIGHT = 17;

            if(m.Msg== WM_NCHITTEST)
            {
                base.WndProc(ref m);
                if (this.WindowState == FormWindowState.Normal)
                {
                    if ((int)m.Result == HTCLIENT)
                    {
                        Point screenPoint = new Point(m.LParam.ToInt32());
                        Point clientPoint = this.PointToClient(screenPoint);

                        if(clientPoint.Y <= resizeAreaSize)
                        {
                            if (clientPoint.X <= resizeAreaSize)
                                m.Result = (IntPtr)HTTOPLEFT;
                            else if(clientPoint.X < (this.Size.Width - resizeAreaSize)) 
                                    m.Result= (IntPtr)HTTOP;
                            else 
                                m.Result= (IntPtr)HTTOPRIGHT;
                        }
                        else if(clientPoint.Y <= (this.Size.Height - resizeAreaSize))
                        {
                            if(clientPoint.X <= resizeAreaSize)
                                m.Result=(IntPtr)HTLEFT;
                            else if(clientPoint.X > (this.Size.Width - resizeAreaSize))
                                m.Result= (IntPtr)HTRIGHT;
                        }
                        else
                        {
                            if (clientPoint.X <= resizeAreaSize)
                                m.Result = (IntPtr)HTBOTTOMLEFT;
                            else if (clientPoint.X < (this.Size.Width - resizeAreaSize))
                                m.Result = (IntPtr)HTBOTTOM;
                            else 
                                m.Result = (IntPtr)HTBOTTOMRIGHT;
                        }
                    }
                }
                return;
            }

            if (m.Msg == WM_NCCALCSIZE && m.WParam.ToInt32() == 1)
            {
                return;
            }
            base.WndProc(ref m);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void fHome_Resize(object sender, EventArgs e)
        {
            AdjustForm();
        }

        private void btnThuNho_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnPhongTo_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
                this.WindowState = FormWindowState.Maximized;
            else
                this.WindowState = FormWindowState.Normal;
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            CollapseMenu();
        }

        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            OpenChildForm(new fTrangChu(), sender);
        }

        private void btnSanBong_Click(object sender, EventArgs e)
        {
            OpenChildForm(new fSanBong(), sender);
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            OpenChildForm(new fKhachHang(), sender);
        }

        private void btnDatSan_Click(object sender, EventArgs e)
        {
            OpenChildForm(new fDatSan(), sender);  
        }

        private void btnHoaDon_Click(object sender, EventArgs e)
        {
            OpenChildForm(new fHoaDon(), sender);
        }

        private void btnDichVu_Click(object sender, EventArgs e)
        {
            OpenChildForm(new fDichVu(), sender);
        }

        private void btnKho_Click(object sender, EventArgs e)
        {
            OpenChildForm(new fKho(), sender);
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            OpenChildForm(new fThongKe(), sender);
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn đăng xuất?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void fHome_Load(object sender, EventArgs e)
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
