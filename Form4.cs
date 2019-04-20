using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

// Employee Entry Page
namespace Supershop
{
    public partial class Form4 : Form
    {
        string cs = @"Data Source=ASUS-PC\SQLEXPRESS;Initial Catalog=SuperShopDb;Integrated Security=True;";
        public Form4()
        {
            InitializeComponent();
            loadData();

        }

        public void loadData()
        {
            SqlConnection con = new SqlConnection(cs);
            string sqlQuery = @"SELECT * from users";
            SqlCommand cmd6 = new SqlCommand(sqlQuery, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd6);
            DataTable table = new DataTable();
            da.Fill(table);
            dataGridView1.DataSource = new BindingSource(table, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd8 = new SqlCommand("insert into users (user_name,user_type,user_password) values (@name , @type , @password) ", con);
            cmd8.Parameters.AddWithValue("@name", textBox1.Text);
            cmd8.Parameters.AddWithValue("@type", textBox2.Text);
            cmd8.Parameters.AddWithValue("@password", textBox3.Text);
            con.Open();
            int i = cmd8.ExecuteNonQuery();

            con.Close();

            if (i != 0)
            {
                MessageBox.Show(i + " User Inserted!" );
            }

            string sqlQuery = @"SELECT * from users";
            SqlCommand cmd7 = new SqlCommand(sqlQuery, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd7);
            DataTable table = new DataTable();
            da.Fill(table);
            dataGridView1.DataSource = new BindingSource(table, null);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 form2 = new Form2();
            form2.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3 form3 = new Form3();
            form3.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Update form5 = new Update();
            form5.ShowDialog();
        }
    }
}
