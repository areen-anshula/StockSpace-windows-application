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
    public partial class receivegood : Form
    {
        public receivegood()
        {
            InitializeComponent();
            Stored_Goods.Click += Stored_Goods_Click;
            //DashboardNavigation.WireSectionPages(this, Transfer_Goods, Release_Goods, History, Reports, Settings);
        }

        private void Dashboard_Click(object sender, EventArgs e)
        {
            Dashboardp d1 = new Dashboardp();
            d1.Show();
            d1.WindowState = FormWindowState.Maximized;
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void Total_Storages_Click(object sender, EventArgs e)
        {

        }

        private void Storages_Click(object sender, EventArgs e)
        {
            storages s1 = new storages();
            s1.Show();
            s1.WindowState = FormWindowState.Maximized; 
            this.Hide();
        }

        private void Stored_Goods_Click(object sender, EventArgs e)
        {
            StoredGoods storedGoods = new StoredGoods();
            storedGoods.Show();
            this.Hide();
        }
    }
}
