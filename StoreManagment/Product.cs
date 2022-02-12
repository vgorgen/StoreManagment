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
    public partial class Product : Form
    {
        public Product()
        {
            InitializeComponent();
        }
        private void Product_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            LoadData();
        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //İnsert Logic
            SqlConnection sq = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=StockMarket;Integrated Security=True");
            sq.Open();
            bool status = false;
            if(comboBox1.SelectedIndex == 0)
            {
                status = true;
            }
            else
            {
                status = false;
            }

            var sqlQuery = "";
            if(ifProductExist(sq, textBox1.Text))
            {
                sqlQuery = @"UPDATE[Products]
   SET [ProductName] = '" + textBox2.Text + "' ,[ProductStatus] = '" + status + "' WHERE [ProductCode] = '" + textBox1.Text + "'";
            }
            else
            {
                sqlQuery = @"INSERT INTO [dbo].[Products]
           ([ProductCode]
           ,[ProductName]
           ,[ProductStatus])
            VALUES ('" + textBox1.Text + "' ,'" + textBox2.Text + "', '" + status + "')";

            }
             
            SqlCommand cmd = new SqlCommand(sqlQuery, sq);
            cmd.ExecuteNonQuery();
            sq.Close();

            //Reading Data
            LoadData();


            }
        private bool ifProductExist(SqlConnection sq, string productcode)
        {
            SqlDataAdapter ada = new SqlDataAdapter("SELECT 1 From [StockMarket].[dbo].[Products] WHERE = '"+ productcode + "'" , sq);
            DataTable dt2 = new DataTable();
            ada.Fill(dt2);
            if(dt2.Rows.Count > 0)
            
            return true;
            else
                return false;
        }
        public void LoadData()
        {
            SqlConnection sq = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=StockMarket;Integrated Security=True");
            SqlDataAdapter ada = new SqlDataAdapter("SELECT * From [StockMarket].[dbo].[Products]", sq);
            DataTable dt2 = new DataTable();
            ada.Fill(dt2);
            dataGridView1.Rows.Clear();
            foreach (DataRow item in dt2.Rows)
            {
                int i = dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = item["ProductCode"].ToString();
                dataGridView1.Rows[i].Cells[1].Value = item["ProductName"].ToString();
                if ((bool)item["ProductStatus"])
                {
                    dataGridView1.Rows[i].Cells[2].Value = "Active";
                }
                else
                {
                    dataGridView1.Rows[i].Cells[2].Value = "Deactive";
                }
            }

        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].ToString();
            textBox2.Text = dataGridView1.SelectedRows[1].Cells[1].ToString();
            if (dataGridView1.SelectedRows[0].Cells[2].ToString() == "Active")
            {
                comboBox1.SelectedIndex = 0;

            }
            else
            {
                comboBox1.SelectedIndex = 1;
            }
            comboBox1.SelectedText = dataGridView1.SelectedRows[0].Cells[2].ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection sq = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=StockMarket;Integrated Security=True");
            var sqlQuery = "";
            if (ifProductExist(sq, textBox1.Text))
            {
                sq.Open();
                sqlQuery = @"DELETE FROM [Products] WHERE [ProductCode] = '" + textBox1.Text + "'";
                SqlCommand cmd = new SqlCommand(sqlQuery, sq);
                cmd.ExecuteNonQuery();
                sq.Close();
            }
            else
            {
                MessageBox.Show("Ürün Kayıtlı Değil");

            }

            

            //Reading Data
            LoadData();

        }
    }
}
