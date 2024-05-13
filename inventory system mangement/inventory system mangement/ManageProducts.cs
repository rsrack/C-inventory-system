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
    public partial class ManageProducts : Form
    {
        public ManageProducts()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\user pc\Documents\inventorydb.mdf"";Integrated Security=True;Connect Timeout=30");
        void fillcategory()
        {
            string query = " select * from CategoryTb1 ";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader rdr;
            try
            {
                con.Open();
                DataTable dt = new DataTable();
                dt.Columns.Add("CatName", typeof(string));
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                CatCombo.ValueMember = "CatName";
                CatCombo.DataSource = dt;
                SearchCombo.ValueMember = "CatName";
                SearchCombo.DataSource = dt;
                con.Close();
            }
            catch
            {

            }
        }
        private void label8_Click(object sender, EventArgs e)
        {

        }

       







        private void ManageProducts_Load(object sender, EventArgs e)
        {
            fillcategory();
            populate();
        }


        void populate()
        {
            try
            {
                con.Open();
                string Myquery = "select * from ProductTb1";
                SqlDataAdapter da = new SqlDataAdapter(Myquery, con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                ProductsGV.DataSource = ds.Tables[0];


                con.Close();
            }
            catch
            {

            }
        }

        void filterbycategory()
        {
            try
            {
                con.Open();
                string Myquery = "select * from ProductTb1 where prodcat='"+SearchCombo.SelectedValue.ToString()+"'";
                SqlDataAdapter da = new SqlDataAdapter(Myquery, con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                ProductsGV.DataSource = ds.Tables[0];


                con.Close();
            }
            catch
            {

            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                con.Open();

                // Using parameterized query to avoid SQL injection
                SqlCommand cmd = new SqlCommand("INSERT INTO ProductTb1 VALUES (@ProdId, @ProdName, @prodQty ,@Price,@Description,@CatCombo)", con);
                cmd.Parameters.AddWithValue("@ProdId", ProdIdTb.Text);
                cmd.Parameters.AddWithValue("@ProdName", ProdNameTb.Text);
                cmd.Parameters.AddWithValue("@prodQty", ProdQtyTb.Text);
                cmd.Parameters.AddWithValue("@Price", PriceTb.Text);
                cmd.Parameters.AddWithValue("@Description", DescriptionTb.Text);
                cmd.Parameters.AddWithValue("@CatCombo", CatCombo.SelectedValue.ToString());


                cmd.ExecuteNonQuery();

                MessageBox.Show("Product  Successfully Added");
                con.Close();

                populate();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (ProdIdTb.Text == "")
            {
                MessageBox.Show("ENTER THE PRODUCT ID ");
            }
            else
            {
                con.Open();
                string myquery = "DELETE FROM ProductTb1 WHERE ProdId='" + ProdIdTb.Text + "';";
                SqlCommand command = new SqlCommand(myquery, con);
                command.ExecuteNonQuery();
                MessageBox.Show("Product Successfully deleted");
                populate();
                con.Close();

            }
        }

        private void ProductsGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            ProdIdTb.Text = ProductsGV.SelectedRows[0].Cells[0].Value?.ToString();
            ProdNameTb.Text = ProductsGV.SelectedRows[0].Cells[1].Value?.ToString();
            ProdQtyTb.Text = ProductsGV.SelectedRows[0].Cells[2].Value?.ToString();
            PriceTb.Text = ProductsGV.SelectedRows[0].Cells[3].Value?.ToString();
            DescriptionTb.Text = ProductsGV.SelectedRows[0].Cells[4].Value?.ToString();
            CatCombo.SelectedValue = ProductsGV.SelectedRows[0].Cells[5].Value?.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            try
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\user pc\Documents\inventorydb.mdf"";Integrated Security=True;Connect Timeout=30")) // Assuming 'connectionString' is defined somewhere))
                {
                    con.Open();

                    string query = "UPDATE ProductTb1 SET ProdName = @ProdName, ProdQty=@ProdQty,ProdPrice = @ProdPrice,ProdDesc=@ProdDesc, ProdCat=@ProdCat WHERE ProdId = @ProdId";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ProdName", ProdNameTb.Text);
                        cmd.Parameters.AddWithValue("@ProdQty", ProdQtyTb.Text);
                        cmd.Parameters.AddWithValue("@ProdPrice", PriceTb.Text);
                        cmd.Parameters.AddWithValue("@ProdDesc", DescriptionTb.Text);
                        cmd.Parameters.AddWithValue("@ProdCat", CatCombo.SelectedValue.ToString());
                        cmd.Parameters.AddWithValue("@ProdId", ProdIdTb.Text);



                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Product Successfully Updated");
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

        private void button5_Click(object sender, EventArgs e)
        {
            
            filterbycategory();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            populate();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            HomeForm home = new HomeForm();
            home.Show();
            this.Hide();
        }
    }
    
}

    

