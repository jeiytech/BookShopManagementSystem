using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;

namespace BookShopMGT
{
    public partial class MainPage : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\HP\Documents\bookshop.mdf;Integrated Security=True;Connect Timeout=30");

        public MainPage()
        {
            InitializeComponent();
           /* loadBooks();*/
        }

        private void MainPage_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'bookshopDataSet.Product' table. You can move, or remove it, as needed.
            //this.productTableAdapter.Fill(this.bookshopDataSet.Product);
            dataGridView1.Visible = false;
            button6.Hide();
        }

        void fillGridView()
        {
            //This line fills the datagrid with data from the database
            SqlDataAdapter sda = new SqlDataAdapter("Select * from Product", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        public void loadBooks()
        {
            //This block of code serves as a search function.
            int i = 0;
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            String query = "SELECT * FROM Product where CONCAT(p_id, Title, Author, Publisher, isbn, Price, p_Date) LIKE '%" + textBox6.Text + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView1.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString());
            }
            dr.Close();
            con.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Add Button Click
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "" && dateTimePicker1.Text != "")
            {
                try
                {
                    String query = "insert into Product ([Title],[Author], [Publisher], [isbn], [Price], [p_Date]) values('" + textBox1.Text.Trim() + "','" + textBox2.Text.Trim() + "', '" + textBox3.Text.Trim() + "', '" + textBox4.Text.Trim() + "', '" + textBox5.Text.Trim() + "', '" + dateTimePicker1.Text.Trim() + "')";
                    SqlCommand cmd = new SqlCommand(query, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Book has been added succesfully!...");
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox4.Clear();
                    textBox5.Clear();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Details Exists", ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please fill out the form");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Update Button click
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "" && dateTimePicker1.Text != "")
            {
                try
                {
                    String query = "update Product set Title= '" + textBox1.Text.Trim() + "', Author= '" + textBox2.Text.Trim() + "', Publisher= '" + textBox3.Text.Trim() + "', isbn= '" + textBox4.Text.Trim() + "', Price= '" + textBox5.Text.Trim() + "', p_Date= '" + dateTimePicker1.Text + "' ";
                    SqlCommand cmd = new SqlCommand(query, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Book has been updated succesfully!...");
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox4.Clear();
                    textBox5.Clear();
                    fillGridView();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Details Exists", ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("Please fill out the form");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //This block of code fills out the textboxs with data specifically chosen from the datagrid
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                label7.Text = row.Cells[0].Value.ToString();
                textBox1.Text = row.Cells[1].Value.ToString();
                textBox2.Text = row.Cells[2].Value.ToString();
                textBox3.Text = row.Cells[3].Value.ToString();
                textBox4.Text = row.Cells[4].Value.ToString();
                textBox5.Text = row.Cells[5].Value.ToString();
                dateTimePicker1.Text = row.Cells[6].Value.ToString();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Show Button click
            dataGridView1.Visible = true;
            fillGridView();
            button4.Hide();
            button6.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //Hide Button click
            dataGridView1.Visible = false;
            button6.Hide();
            button4.Show();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            //Search box text changed
            loadBooks();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //Closes the app
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
        }
    }
}
