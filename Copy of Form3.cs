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
        int f;
        double sub = 0 , vat = 0 , total = 0 ;
        IDictionary<string, int> products;
        int soldSuccessful = 0;

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
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            if (e.RowIndex >= 0) { 
                  DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                    f = 0;

                        foreach (ListViewItem item in listView1.Items)
                        {
                            if (item.Text == row.Cells["product_name"].Value.ToString())
                            {
                                f = 1;
                                int a = Convert.ToInt32(item.SubItems[2].Text.ToString());
                                a++;
                                item.SubItems[2].Text = a.ToString();
                                sub = sub + Convert.ToInt32(row.Cells["product_price"].Value.ToString());
                                products[row.Cells["product_name"].Value.ToString()]++ ;
                                vat = (sub) * 0.05;
                                total = sub + vat;
                                label5.Text = sub.ToString();
                                label6.Text = vat.ToString();
                                label7.Text = total.ToString();
                            }
                        }

                    if(f==0) {
                        ListViewItem Cart = new ListViewItem(row.Cells["product_name"].Value.ToString());
                        Cart.SubItems.Add(row.Cells["product_price"].Value.ToString());
                        Cart.SubItems.Add("1");
                        sub = sub +  Convert.ToInt32(row.Cells["product_price"].Value.ToString());
                        products[row.Cells["product_name"].Value.ToString()] = 1;
                        vat = (sub) * 0.05;
                        total = sub + vat;
                        label5.Text = sub.ToString();
                        label6.Text = vat.ToString();
                        label7.Text = total.ToString();
                        listView1.Items.Add(Cart);
                    }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            foreach (string key in products.Keys)
            {
               // MessageBox.Show(products[key].ToString());
                SqlConnection con = new SqlConnection(cs);
                string sqlQuery = "UPDATE products SET product_available = product_available-@updateItem Where product_name = @name";
                SqlCommand cmd4 = new SqlCommand(sqlQuery, con);
                cmd4.Parameters.AddWithValue("@updateItem", products[key]);
                cmd4.Parameters.AddWithValue("@name", key);
                con.Open();
                int i = cmd4.ExecuteNonQuery();

                con.Close();

                if (i != 0)
                {
                    soldSuccessful = 1;
                }
                else {
                    soldSuccessful = 0;
                }

            }

            if (soldSuccessful == 1)
            {
               
                SqlConnection con = new SqlConnection(cs);
                string sqlQuery = "insert into transactions(transaction_total) values (@total) ";
                SqlCommand cmd5 = new SqlCommand(sqlQuery, con);
                cmd5.Parameters.AddWithValue("@total", total);
                con.Open();
                int i = cmd5.ExecuteNonQuery();

                con.Close();

                if (i != 0)
                {
                    MessageBox.Show("Successfully Sold and Updated Database!");
                }

                listView1.Items.Clear();
                loadData();
                label5.Text = "0";
                label6.Text = "0";
                label7.Text = "0";
            }
            else
            {
                MessageBox.Show("Error updating database!");
            }  
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try {
                int a = Convert.ToInt32(listView1.SelectedItems[0].SubItems[2].Text);
                if (a >= 1)
                {
                    a--;
                    listView1.SelectedItems[0].SubItems[2].Text = a.ToString();
                    sub = sub - Convert.ToInt32(listView1.SelectedItems[0].SubItems[1].Text.ToString());
                    vat = (sub) * 0.05;
                    total = sub + vat;
                    label5.Text = sub.ToString();
                    label6.Text = vat.ToString();
                    label7.Text = total.ToString();
                }
                else if (a == 0)
                {
                    listView1.SelectedItems[0].Remove();
                }
            }

            catch {
                MessageBox.Show("Select Item Please or list is empty!");
            }
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
            Form5 form5 = new Form5();
            form5.ShowDialog();
        }
    }
}
