using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace inventory_system_mangement
{
    public partial class ManageCustomers : Form
    {
        public ManageCustomers()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\user pc\Documents\inventorydb.mdf"";Integrated Security=True;Connect Timeout=30");


        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        void populate()
        {
            try
            {
                con.Open();
                string Myquery = "select * from CustomerTb1";
                SqlDataAdapter da = new SqlDataAdapter(Myquery, con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                CustomersGV.DataSource = ds.Tables[0];


                con.Close();
            }
            catch
            {

            }
        }

        private void UsersGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            populate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();

                // Using parameterized query to avoid SQL injection
                SqlCommand cmd = new SqlCommand("INSERT INTO CustomerTb1 VALUES (@uname, @fname, @password)", con);
                cmd.Parameters.AddWithValue("@uname", CustomerIdTb.Text);
                cmd.Parameters.AddWithValue("@fname", CustomernameTb.Text);
                cmd.Parameters.AddWithValue("@password", CustomerPhoneTb.Text);
               

                cmd.ExecuteNonQuery();

                MessageBox.Show("Customer Successfully Added");
                con.Close();

               populate();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void ManageCustomers_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (CustomerIdTb.Text == "")
            {
                MessageBox.Show("ENTER THE CUSTOMER ID ");
            }
            else
            {
                con.Open();
                string myquery = "DELETE FROM CustomerTb1 WHERE CustId='" + CustomerIdTb.Text + "';";
                SqlCommand command = new SqlCommand(myquery, con);
                command.ExecuteNonQuery();
                MessageBox.Show("Customer Successfully deleted");
                populate();
                con.Close();

            }
        }

        private void CustomersGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CustomerIdTb.Text = CustomersGV.SelectedRows[0].Cells[0].Value?.ToString();
            CustomernameTb.Text = CustomersGV.SelectedRows[0].Cells[1].Value?.ToString();
            CustomerPhoneTb.Text = CustomersGV.SelectedRows[0].Cells[2].Value?.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\user pc\Documents\inventorydb.mdf"";Integrated Security=True;Connect Timeout=30")) // Assuming 'connectionString' is defined somewhere))
                {
                    con.Open();

                    string query = "UPDATE CustomerTb1 SET CustName = @fname, CustPhone = @phone WHERE CustId = @id";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@id", CustomerIdTb.Text);
                        cmd.Parameters.AddWithValue("@fname", CustomernameTb.Text);
                        cmd.Parameters.AddWithValue("@phone", CustomerPhoneTb.Text);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Customer Successfully Updated");
                            // Call any method to refresh UI if needed
                        }
                        else
                        {
                            MessageBox.Show("Customer update failed");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }



        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            HomeForm home = new HomeForm();
            home.Show();
            this.Hide();

        }
    }
    }

