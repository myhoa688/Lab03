using AxWMPLib;
using System;
using System.Windows.Forms;

namespace Lab03_01
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // Bắt đầu Timer để cập nhật đồng hồ
            timer1.Interval = 1000; // 1 giây
            timer1.Start();
            UpdateTime();
        }

        private void UpdateTime()
        {
            toolStripStatusLabel1.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateTime();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Media Files|*.mp3;*.mp4;*.wav;*.avi;*.mpeg;*.midi|" +
                         "All files (*.*)|*.*";
            ofd.Title = "Chọn file media để phát";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                axWindowsMediaPlayer1.URL = ofd.FileName;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}