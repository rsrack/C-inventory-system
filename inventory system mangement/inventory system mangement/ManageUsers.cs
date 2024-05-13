using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace inventory_system_mangement
{
    public partial class ManageUsers : Form
    {
        public ManageUsers()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\user pc\Documents\inventorydb.mdf"";Integrated Security=True;Connect Timeout=30");

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        void populate()
        {
            try
            {
                con.Open();
                string Myquery = "select * from UserTb1";
                SqlDataAdapter da = new SqlDataAdapter(Myquery, con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                UsersGV.DataSource = ds.Tables[0];


                con.Close();
            }
            catch
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();

                // Using parameterized query to avoid SQL injection
                SqlCommand cmd = new SqlCommand("INSERT INTO UserTb1 VALUES (@uname, @fname, @password, @phone)", con);
                cmd.Parameters.AddWithValue("@uname", unameTb.Text);
                cmd.Parameters.AddWithValue("@fname", fnameTb.Text);
                cmd.Parameters.AddWithValue("@password", passwordTb.Text);
                cmd.Parameters.AddWithValue("@phone", phoneTb.Text);

                cmd.ExecuteNonQuery();

                MessageBox.Show("User Successfully Added");
                con.Close();

                populate();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }


        private void ManageUsers_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void UsersGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            unameTb.Text = UsersGV.SelectedRows[0].Cells[0].Value?.ToString();
            fnameTb.Text = UsersGV.SelectedRows[0].Cells[1].Value?.ToString();
            passwordTb.Text = UsersGV.SelectedRows[0].Cells[2].Value?.ToString();
            phoneTb.Text = UsersGV.SelectedRows[0].Cells[3].Value?.ToString();






        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (phoneTb.Text == "")
            {
                MessageBox.Show("User Successfully deleted ");
            }
            else
            {
                con.Open();
                string myquery = "DELETE FROM UserTb1 WHERE Uphone='" + phoneTb.Text + "';";
                SqlCommand command = new SqlCommand(myquery, con);
                command.ExecuteNonQuery();
                MessageBox.Show("User Successfully deleted");
                populate();
                con.Close();

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\user pc\Documents\inventorydb.mdf"";Integrated Security=True;Connect Timeout=30")) // Assuming 'connectionString' is defined somewhere
                {
                    con.Open();

                    string query = "UPDATE UserTb1 SET Uname = @uname, Ufullname = @fname, Upassword = @password WHERE UPhone = @phone";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@uname", unameTb.Text);
                        cmd.Parameters.AddWithValue("@fname", fnameTb.Text);
                        cmd.Parameters.AddWithValue("@password", passwordTb.Text);
                        cmd.Parameters.AddWithValue("@phone", phoneTb.Text);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("User Successfully updated");
                            populate(); // Assuming 'populate' is a method to refresh the user interface
                        }
                        else
                        {
                            MessageBox.Show("User update failed");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }


        }

        private void button4_Click(object sender, EventArgs e)
        {
            HomeForm home = new HomeForm();
            home.Show();
            this.Hide();
        }
    }
}
