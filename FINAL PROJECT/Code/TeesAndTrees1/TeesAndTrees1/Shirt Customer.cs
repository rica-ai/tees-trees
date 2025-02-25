using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TeesAndTrees1
{
    public partial class Shirt_Customer : Form
    {
        public Shirt_Customer()
        {
            InitializeComponent();
        }
        
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-5BFSRUD\\SQLEXPRESS;Initial Catalog=TeesAndTrees;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");


        private void pictureBox9_Click(object sender, EventArgs e)
        {
            ShirtInventory shirtInventory = new ShirtInventory();
            this.Hide();
            shirtInventory.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            // Search button logic
            SqlCommand com = new SqlCommand("exec dbo.sp_searchTCustomer '" + int.Parse(textBox1.Text) + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            AE_Customer aE_Customer = new AE_Customer();
            aE_Customer.Show();
            this.Hide();
        }


        private void button9_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedCustomerID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["CUSTOMER ID"].Value);

                AE_Customer aE_Customer = new AE_Customer(this, selectedCustomerID);
                aE_Customer.Show();
                this.Close();
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedCustomerID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["CUSTOMER ID"].Value);

                // Assuming you have a method for deleting records
                DeleteCustomerRecord(selectedCustomerID);

                // Refresh the DataGridView after deletion
                loadCustomerRecords();

                MessageBox.Show("Record deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void DeleteCustomerRecord(int customerID)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to delete?", "Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    con.Open();

                    // Assuming you have a stored procedure for deleting records
                    SqlCommand com = new SqlCommand("exec dbo.sp_DeleteCustomer'" + customerID + "'", con);
                    com.ExecuteNonQuery();
                    loadCustomerRecords();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting record: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            // Additional button logic if needed
        }

        private void Shirt_Customer_Load(object sender, EventArgs e)
        {
            loadCustomerRecords();
        }

        //load records
        private void loadCustomerRecords()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("exec dbo.sp_viewCustomer", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Shirt_Customized shirt_Customized = new Shirt_Customized();
            shirt_Customized.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Shirt_Equipment shirt_Equipment = new Shirt_Equipment();
            shirt_Equipment.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Shirt_Material shirt_Material = new Shirt_Material();
            shirt_Material.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            settings.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Shirt_Customer shirt_Customer = new Shirt_Customer();
            shirt_Customer.Show();
            this.Hide();
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            SqlCommand com = new SqlCommand("exec dbo.sp_searchCustomer'" + int.Parse(textBox1.Text) + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void button11_Click_1(object sender, EventArgs e)
        {
            LoadAllRecords();
            textBox1.Clear();
        }
        void LoadAllRecords()
        {
            SqlCommand com = new SqlCommand("exec dbo.sp_viewCustomer", con);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Sales sales = new Sales();
            this.Hide();
            sales.Show();
        }
    }
}
