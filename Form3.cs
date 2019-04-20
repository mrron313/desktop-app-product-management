using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

// Product Sale Page
namespace Supershop
{
    public partial class Form3 : Form
    {

        IDictionary<string, int> products;

        string cs = @"Data Source=ASUS-PC\SQLEXPRESS;Initial Catalog=SuperShopDb;Integrated Security=True;";
        public Form3()
        {
            InitializeComponent();
            loadData();
        }

        public void loadData() {
            products = new Dictionary<string, int>();
            SqlConnection con = new SqlConnection(cs);
            string sqlQuery = @"SELECT * from products";
            SqlCommand cmd3 = new SqlCommand(sqlQuery, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd3);
            DataTable table = new DataTable();
            da.Fill(table);
            dataGridView1.DataSource = new BindingSource(table, null);

            string sqlQuery2 = @"SELECT * from users";
            SqlCommand cmd6 = new SqlCommand(sqlQuery2, con);
            SqlDataAdapter da2 = new SqlDataAdapter(cmd6);
            DataTable table2 = new DataTable();
            da2.Fill(table2);
            dataGridView2.DataSource = new BindingSource(table2, null);

            string sqlQuery3 = @"SELECT * from transactions";
            SqlCommand cmd7 = new SqlCommand(sqlQuery3, con);
            SqlDataAdapter da3 = new SqlDataAdapter(cmd7);
            DataTable table3 = new DataTable();
            da3.Fill(table3);
            dataGridView3.DataSource = new BindingSource(table3, null);


            string result = "SELECT SUM(transaction_total) from transactions where MONTH(GETDATE())=MONTH(transaction_date)";
            SqlCommand showresult = new SqlCommand(result, con);
            con.Open();
            label3.Text = showresult.ExecuteScalar().ToString();

            string result2 = "SELECT SUM(transaction_total) from transactions where YEAR(GETDATE())=YEAR(transaction_date)";
            SqlCommand showresult2 = new SqlCommand(result2, con);
            label4.Text = showresult2.ExecuteScalar().ToString();

            string result3= "SELECT SUM(transaction_total) from transactions where DAY(GETDATE())=DAY(transaction_date)";
            SqlCommand showresult3 = new SqlCommand(result3, con);
            label6.Text = showresult3.ExecuteScalar().ToString();

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

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form4 form4 = new Form4();
            form4.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Update form5 = new Update();
            form5.ShowDialog();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
