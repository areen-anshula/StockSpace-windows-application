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
    public partial class storages : Form
    {
        public storages()
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

        private void Receive_Goods_Click(object sender, EventArgs e)
        {
            receivegood r1 = new receivegood();
            r1.Show();
            // Form1 එක සඟවන්න (hide) ඕන නම් පහත line එක uncomment කරන්න:
            this.Hide();
        }

        private void Bpresentage_Paint(object sender, PaintEventArgs e)
        {

        }

        private void storagesAstatus_Load(object sender, EventArgs e)
        {

        }

        private void label27_Click(object sender, EventArgs e)
        {

        }

        private void guna2ProgressBar4_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
