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
    public partial class ManageOrders : Form
    {
        public ManageOrders()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\user pc\Documents\inventorydb.mdf"";Integrated Security=True;Connect Timeout=30");
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        void populateProducts()
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
                //Combo.ValueMember = "CatName";
                //CatCombo.DataSource = dt;
                SearchCombo.ValueMember = "CatName";
                SearchCombo.DataSource = dt;
                con.Close();
            }
            catch
            {

            }
        }
        int num = 0;
        int uprice, Totalprice, qty;
        string product;

        private void ManageOrders_Load(object sender, EventArgs e)
        {
            populate();
            populateProducts();
            fillcategory();
        }

        private void ProductsGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            product = ProductsGV.SelectedRows[0].Cells[1].Value.ToString();
            qty = Convert.ToInt32(QtyTb.Text);
            uprice= Convert.ToInt32(ProductsGV.SelectedRows[0].Cells[3].Value.ToString());
            Totalprice = qty * uprice;
             flag = 1;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (QtyTb.Text == "")
                MessageBox.Show("ENTER THE QUANTITY OF PRODUCT");
            else if (flag == 0)
                MessageBox.Show("SELECT THE PRODUCT");
            else
            {
                


                DataTable table = new DataTable();
                num = num + 1;
                qty = Convert.ToInt32(QtyTb.Text);
                Totalprice = qty * uprice;
                table.Rows.Add(num, product, qty, uprice, Totalprice);
                OrderGv.DataSource = table;
                flag = 0;
            }
            
        }

        private void CustomersGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CustId.Text = CustomersGV.SelectedRows[0].Cells[0].Value?.ToString();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            HomeForm home = new HomeForm();
            home.Show();
            this.Hide();
        }

        int flag = 0;
        private void SearchCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                string Myquery = "select * from ProductTb1 where prodcat='" + SearchCombo.SelectedValue.ToString() + "'";
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
    }
    }

