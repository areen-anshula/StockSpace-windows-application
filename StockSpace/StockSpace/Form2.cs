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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            button1.Parent = pictureBox1;
            button1.BackColor = Color.Transparent;
            button1.FlatAppearance.BorderSize = 0;

            button2.Parent = pictureBox1;
            button2.BackColor = Color.Transparent;
            button2.FlatAppearance.BorderSize = 0;

            button3.Parent = pictureBox1;
            button3.BackColor = Color.Transparent;
            button3.FlatAppearance.BorderSize = 0;

            button4.Parent = pictureBox1;
            button4.BackColor = Color.Transparent;
            button4.FlatAppearance.BorderSize = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}
