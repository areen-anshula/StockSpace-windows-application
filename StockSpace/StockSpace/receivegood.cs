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
        }

        private void Dashboard_Click(object sender, EventArgs e)
        {
            Dashboardp d1 = new Dashboardp();
            d1.Show();
            // Form1 එක සඟවන්න (hide) ඕන නම් පහත line එක uncomment කරන්න:
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
    }
}
