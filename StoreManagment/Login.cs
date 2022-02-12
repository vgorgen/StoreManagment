using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StoreManagment
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            SqlConnection sq = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=StockMarket;Integrated Security=True");

            SqlDataAdapter adapter = new SqlDataAdapter(@"SELECT *
            FROM [StockMarket].[dbo].[Login] Where UserName = '" + textBox1.Text + " ' and Password = '" + textBox2.Text + "'", sq);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            if (dt.Rows.Count == 1)
            {
                if (checkBox1.Checked == true)
                {
                    this.Hide();
                    StockMain main = new StockMain();
                    main.Show();
                }
                else if(checkBox2.Checked == true)
                {
                    this.Hide();
                    StockMain main = new StockMain();
                    main.Show();

                }
            }
            else
            {
                MessageBox.Show("Invalid User Name and Password!", "ERROR", MessageBoxButtons.OK ,MessageBoxIcon.Error);
                button2_Click(sender, e);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            

            textBox1.Text = "";

            textBox2.Clear();
            textBox1.Focus();
        }
    }
}
