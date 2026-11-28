using System.Configuration;
using MySql.Data.MySqlClient;
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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString =
                ConfigurationManager.ConnectionStrings["StockSpaceDb"].ConnectionString;

            try
            {
                using (var connection = new MySqlConnection(connectionString))
                using (var command = new MySqlCommand(
                    "SELECT Password FROM user WHERE Username = @username LIMIT 1",
                    connection))
                {
                    command.Parameters.Add("@username", MySqlDbType.VarChar, 100)
                        .Value = textBox1.Text.Trim();

                    connection.Open();
                    object result = command.ExecuteScalar();
                    string storedHash = result == null || result == DBNull.Value
                        ? null
                        : Convert.ToString(result);

                    if (storedHash != null &&
                        BCrypt.Net.BCrypt.Verify(textBox2.Text, storedHash))
                    {
                        new Dashboardp().Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Username or password is incorrect.");
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Database connection failed: " + ex.Message);
            }
        }
    }
}
