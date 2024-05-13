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
    public partial class ManageCategories : Form
    {
        public ManageCategories()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\user pc\Documents\inventorydb.mdf"";Integrated Security=True;Connect Timeout=30");

        void populate()
        {
            try
            {
                con.Open();
                string Myquery = "select * from CategoryTb1";
                SqlDataAdapter da = new SqlDataAdapter(Myquery, con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                CategoriesGV.DataSource = ds.Tables[0];


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
                SqlCommand cmd = new SqlCommand("INSERT INTO CategoryTb1 VALUES (@uname, @fname)", con);
                cmd.Parameters.AddWithValue("@uname", CatIdTb.Text);
                cmd.Parameters.AddWithValue("@fname", CatNameTb.Text);
                


                cmd.ExecuteNonQuery();

                MessageBox.Show("Categories Successfully Added");
                con.Close();

                populate();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (CatIdTb.Text == "")
            {
                MessageBox.Show("ENTER THE CUSTOMER ID ");
            }
            else
            {
                con.Open();
                string myquery = "DELETE FROM CategoryTb1 WHERE Catid='" + CatIdTb.Text + "';";
                SqlCommand command = new SqlCommand(myquery, con);
                command.ExecuteNonQuery();
                MessageBox.Show("Category Successfully deleted");
                populate();
                con.Close();

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user pc\Documents\inventorydb.mdf;Integrated Security=True;Connect Timeout=30"))
                {
                    con.Open();

                    string query = "UPDATE CategoryTb1 SET CatName = @CatName WHERE CatId = @CatId";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@CatName", CatNameTb.Text);
                        cmd.Parameters.AddWithValue("@CatId", CatIdTb.Text);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Categories Successfully Updated");
                            // Call any method to refresh UI if needed
                        }
                        else
                        {
                            MessageBox.Show("Categories update failed");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }


        }

        private void ManageCategories_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void CategoriesGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CatIdTb.Text = CategoriesGV.SelectedRows[0].Cells[0].Value?.ToString();
            CatNameTb.Text = CategoriesGV.SelectedRows[0].Cells[1].Value?.ToString();
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            HomeForm home = new HomeForm();
            home.Show();
            this.Hide();
        }
    }
}
    

