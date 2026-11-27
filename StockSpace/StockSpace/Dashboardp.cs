using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockSpace
{
    public partial class Dashboardp : Form
    {
        private int usedSpace;
        private int totalCapacity;

        public Dashboardp()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.Stored_Goods.Click += Stored_Goods_Click;
            DashboardNavigation.WireSectionPages(this, Transfer_Goods, Release_Goods, History, Reports, Settings);
        }

        private void Dashboardp_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void guna2CircleProgressBar1_ValueChanged(object sender, EventArgs e)
        {
            double percentage = (usedSpace / totalCapacity) * 100;

            guna2CircleProgressBar1.Minimum = 0;
            guna2CircleProgressBar1.Maximum = 100;
            guna2CircleProgressBar1.Value = (int)Math.Round(percentage);

            guna2CircleProgressBar1.ProgressColor = Color.FromArgb(245, 158, 11);
            guna2CircleProgressBar1.ProgressColor2 = Color.FromArgb(245, 158, 11);
            guna2CircleProgressBar1.FillColor = Color.FromArgb(243, 244, 246);
            guna2CircleProgressBar1.ProgressThickness = 18;
            guna2CircleProgressBar1.FillThickness = 18;
            guna2CircleProgressBar1.ShowText = false;

            plable.Text = percentage.ToString("0.0") + "%";
        }

        private void chart1_Click(object sender, EventArgs e)
        {
            chart1.Series.Clear();

            var chartArea = chart1.ChartAreas[0];
            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisY.MajorGrid.Enabled = false;
            chartArea.AxisY.LabelStyle.Enabled = false;
            chartArea.AxisY.LineWidth = 0;
            chartArea.AxisX.LineColor = Color.LightGray;
            chartArea.AxisX.MajorTickMark.Enabled = false;

            var seriesReceived = new System.Windows.Forms.DataVisualization.Charting.Series("Received");
            seriesReceived.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            seriesReceived.Color = Color.FromArgb(245, 158, 11);
            seriesReceived["PointWidth"] = "0.6";

            var seriesReleased = new System.Windows.Forms.DataVisualization.Charting.Series("Released");
            seriesReleased.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            seriesReleased.Color = Color.FromArgb(30, 30, 30);
            seriesReleased["PointWidth"] = "0.6";

            string[] days = { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };
            int[] receivedData = { 65, 55, 80, 35, 70, 25, 50 };
            int[] releasedData = { 50, 62, 35, 45, 65, 12, 32 };

            for (int i = 0; i < days.Length; i++)
            {
                seriesReceived.Points.AddXY(days[i], receivedData[i]);
                seriesReleased.Points.AddXY(days[i], releasedData[i]);
            }

            chart1.Series.Add(seriesReceived);
            chart1.Series.Add(seriesReleased);
        }

        private void UpdateStorageBar(Guna.UI2.WinForms.Guna2ProgressBar bar, Label lblInfo, double used, double capacity)
        {
            int percent = (int)Math.Round((used / capacity) * 100);
            bar.Value = percent;
            lblInfo.Text = $"{used} of {capacity} m³ · {percent}%";

            if (percent >= 85)
            {
                bar.ProgressColor = Color.FromArgb(239, 68, 68);
                bar.ProgressColor2 = Color.FromArgb(239, 68, 68);
            }
            else if (percent >= 70)
            {
                bar.ProgressColor = Color.FromArgb(245, 158, 11);
                bar.ProgressColor2 = Color.FromArgb(245, 158, 11);
            }
            else
            {
                bar.ProgressColor = Color.FromArgb(16, 185, 129);
                bar.ProgressColor2 = Color.FromArgb(16, 185, 129);
            }
        }

        private void guna2ProgressBar1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {

        }

        private void resivegood_Click(object sender, EventArgs e)
        {
            receivegood f2 = new receivegood();
            f2.Show();
            f2.WindowState = FormWindowState.Maximized;
            this.Hide();
        }

        private void label16_Click_1(object sender, EventArgs e)
        {

        }

        private void guna2Separator1_Click(object sender, EventArgs e)
        {

        }

        private void label24_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel11_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label36_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel16_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Panel17_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Dashboard_Click(object sender, EventArgs e)
        {
        }

        private void Stored_Goods_Click(object sender, EventArgs e)
        {
            StoredGoods storedGoods = new StoredGoods();
            storedGoods.Show();
            this.Hide();
        }

        private void Storages_Click(object sender, EventArgs e)
        {
            storages s1 = new storages ();
            s1.Show();
            s1.WindowState = FormWindowState.Maximized;
            this.Hide();
        }

        private void Receive_Goods_Click(object sender, EventArgs e)
        {
            receivegood r1 = new receivegood();
            r1.Show();
            r1.WindowState = FormWindowState.Maximized;
            this.Hide();
        }
    }
}
