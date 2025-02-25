using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TeesAndTrees1
{
    public partial class Floriculture_Customer : Form
    {
        public Floriculture_Customer()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-5BFSRUD\\SQLEXPRESS;Initial Catalog=TeesAndTrees;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");


        private void pictureBox9_Click(object sender, EventArgs e)
        {
            FloricultureInventory floricultureInventory = new FloricultureInventory();
            this.Hide();
            floricultureInventory.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Floriculture_Customer floriculture_Customer = new Floriculture_Customer();
            this.Hide();
            floriculture_Customer.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Floriculture_Plants floriculture_Plants = new Floriculture_Plants();
            this.Hide();
            floriculture_Plants.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            this.Hide();
            settings.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            SqlCommand com = new SqlCommand("exec dbo.sp_searchCustomer1'" + int.Parse(textBox1.Text) + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            LoadAllRecords();
            textBox1.Clear();
        }
        void LoadAllRecords()
        {
            SqlCommand com = new SqlCommand("exec dbo.sp_viewCustomer1", con);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            P_AE_Customer p_AE_Customer = new P_AE_Customer();
            this.Hide();
            p_AE_Customer.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedCustomerID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["CUSTOMER ID"].Value);

                P_AE_Customer p_AE_Customer = new P_AE_Customer(this, selectedCustomerID);
                p_AE_Customer.Show();
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
                    SqlCommand com = new SqlCommand("exec dbo.sp_DeleteCustomer1 '" + customerID + "'", con);
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
        private void loadCustomerRecords()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("exec dbo.sp_viewCustomer1", con);
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

        private void button5_Click(object sender, EventArgs e)
        {
            Sales1 sales1 = new Sales1();
            sales1.Show();
            this.Hide();
        }

        private void Floriculture_Customer_Load(object sender, EventArgs e)
        {
            loadCustomerRecords();
        }
    }
    }
