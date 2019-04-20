using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

// Update Page
namespace Supershop
{
    public partial class Update : Form
    {
        string cs = @"Data Source=ASUS-PC\SQLEXPRESS;Initial Catalog=SuperShopDb;Integrated Security=True;";

        public Update()
        {
            InitializeComponent();
            textBox4.Visible = false;
            loadData();
        }


        public void loadData()
        {
            SqlConnection con = new SqlConnection(cs);
            string sqlQuery = @"SELECT * from products";
            SqlCommand cmd3 = new SqlCommand(sqlQuery, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd3);
            DataTable table = new DataTable();
            da.Fill(table);
            dataGridView1.DataSource = new BindingSource(table, null);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                textBox1.Text = row.Cells["product_name"].Value.ToString();
                textBox2.Text = row.Cells["product_price"].Value.ToString();
                textBox3.Text = row.Cells["product_available"].Value.ToString();
                textBox4.Text = row.Cells["product_id"].Value.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string sqlQuery = "UPDATE products SET product_name = @name , product_price = @price , product_available = @available Where product_id = @id";
            SqlCommand cmd4 = new SqlCommand(sqlQuery, con);
            cmd4.Parameters.AddWithValue("@name", textBox1.Text);
            cmd4.Parameters.AddWithValue("@price", textBox2.Text);
            cmd4.Parameters.AddWithValue("@available", textBox3.Text);
            cmd4.Parameters.AddWithValue("@id", textBox4.Text);
            con.Open();
            int i = cmd4.ExecuteNonQuery();

            if (i != 0)
            {
                MessageBox.Show("Successfully Updated!");
                loadData();
            }
            else
            {
                MessageBox.Show("Not Updated! There are some errors .");
            }
            con.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3 form3 = new Form3();
            form3.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form4 form4 = new Form4();
            form4.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.ShowDialog();
        }
    }
}
