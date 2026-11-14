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
        }

        private void Dashboardp_Load(object sender, EventArgs e)
        {
            //UpdateSpaceUtilization(1722, 2300);
          
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
            
            // 1. ප්‍රතිශතය (Percentage) ගණනය කිරීම
            double percentage = (usedSpace / totalCapacity) * 100;

            // 2. Guna2CircleProgressBar එකේ Properties සැකසීම
            guna2CircleProgressBar1.Minimum = 0;
            guna2CircleProgressBar1.Maximum = 100;
            guna2CircleProgressBar1.Value = (int)Math.Round(percentage); // 74.9% -> 75 ලෙස පෙන්වයි

            // 3. වර්ණ සහ හැඩය සැකසීම (Image එකේ ඇති ආකාරයට)
            guna2CircleProgressBar1.ProgressColor = Color.FromArgb(245, 158, 11);  // තැඹිලි පාට
            guna2CircleProgressBar1.ProgressColor2 = Color.FromArgb(245, 158, 11); // තනි පාටක් වීමට
            guna2CircleProgressBar1.FillColor = Color.FromArgb(243, 244, 246);      // ඉතිරි කොටසේ ලා අළු පාට
            guna2CircleProgressBar1.ProgressThickness = 18;
            guna2CircleProgressBar1.FillThickness = 18;
            guna2CircleProgressBar1.ShowText = false; // Default අකුරු අයින් කිරීමට

            // 4. රවුම මැද ඇති Label එක Update කිරීම (උදා: "74.9%")
            // ඔයා මැදට දාපු Label එකේ නම lblPercentage නම්:
            plable.Text = percentage.ToString("0.0") + "%";
        }

        private void chart1_Click(object sender, EventArgs e)
        {
            // 1. පරණ Series අයින් කරන්න
            chart1.Series.Clear();

            // 2. X සහ Y අක්ෂ වල අනවශ්‍ය ඉරි සහ අංක අයින් කිරීම (Clean Look එකක් සඳහා)
            var chartArea = chart1.ChartAreas[0];
            chartArea.AxisX.MajorGrid.Enabled = false; // සිරස් ඉරි අයින් කරන්න
            chartArea.AxisY.MajorGrid.Enabled = false; // තිරස් ඉරි අයින් කරන්න
            chartArea.AxisY.LabelStyle.Enabled = false; // වම් පැත්තේ අංක අයින් කරන්න
            chartArea.AxisY.LineWidth = 0; // Y අක්ෂයේ ඉර අයින් කරන්න
            chartArea.AxisX.LineColor = Color.LightGray; // X අක්ෂයේ ඉර ලා අළු පාට කරන්න
            chartArea.AxisX.MajorTickMark.Enabled = false;

            // 3. "Received" (තැඹිලි පාට තීරු) Series එක සෑදීම
            var seriesReceived = new System.Windows.Forms.DataVisualization.Charting.Series("Received");
            seriesReceived.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            seriesReceived.Color = Color.FromArgb(245, 158, 11); // තැඹිලි පාට
            seriesReceived["PointWidth"] = "0.6"; // තීරු වල මහත

            // 4. "Released" (කළු පාට තීරු) Series එක සෑදීම
            var seriesReleased = new System.Windows.Forms.DataVisualization.Charting.Series("Released");
            seriesReleased.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            seriesReleased.Color = Color.FromArgb(30, 30, 30); // තද කළු පාට
            seriesReleased["PointWidth"] = "0.6";

            // 5. දවස් 7ට අදාළ Data එකතු කිරීම (Mon - Sun)
            string[] days = { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };
            int[] receivedData = { 65, 55, 80, 35, 70, 25, 50 };
            int[] releasedData = { 50, 62, 35, 45, 65, 12, 32 };

            for (int i = 0; i < days.Length; i++)
            {
                seriesReceived.Points.AddXY(days[i], receivedData[i]);
                seriesReleased.Points.AddXY(days[i], releasedData[i]);
            }

            // 6. Chart එකට Series දෙක ඇතුළත් කිරීම
            chart1.Series.Add(seriesReceived);
            chart1.Series.Add(seriesReleased);
        }

        private void UpdateStorageBar(Guna.UI2.WinForms.Guna2ProgressBar bar, Label lblInfo, double used, double capacity)
        {
            int percent = (int)Math.Round((used / capacity) * 100);
            bar.Value = percent;
            lblInfo.Text = $"{used} of {capacity} m³ · {percent}%";

            if (percent >= 85) // Critical (රතු)
            {
                bar.ProgressColor = Color.FromArgb(239, 68, 68);
                bar.ProgressColor2 = Color.FromArgb(239, 68, 68);
            }
            else if (percent >= 70) // Warning (තැඹිලි)
            {
                bar.ProgressColor = Color.FromArgb(245, 158, 11);
                bar.ProgressColor2 = Color.FromArgb(245, 158, 11);
            }
            else // Normal (කොළ)
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
    }
}
