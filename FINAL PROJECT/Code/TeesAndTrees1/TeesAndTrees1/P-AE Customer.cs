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
    public partial class P_AE_Customer : Form
    {
        private int customerID;
        public P_AE_Customer(Floriculture_Customer parentForm, int customerID)
        {
            InitializeComponent();
            this.customerID = customerID;

            LoadDataForEditing(customerID);
        }

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-5BFSRUD\\SQLEXPRESS;Initial Catalog=TeesAndTrees;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        public P_AE_Customer()
        {
            InitializeComponent ();
        }
        private void LoadDataForEditing(int customerID)
        {
            try
            {
                con.Open();
                string query = "SELECT * FROM CustomerOne WHERE customerID = @customerID";
                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@customerID", customerID);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // Populate textboxes with the loaded data
                    textBox1.Text = reader.GetInt32(reader.GetOrdinal("customerID")).ToString();
                    textBox2.Text = reader.GetString(reader.GetOrdinal("fname"));
                    textBox3.Text = reader.GetString(reader.GetOrdinal("lname"));
                    textBox4.Text = reader.GetString(reader.GetOrdinal("contactInfo"));
                    textBox5.Text = reader.GetString(reader.GetOrdinal("town"));
                    textBox6.Text = reader.GetString(reader.GetOrdinal("city"));

                }

                else
                {
                    MessageBox.Show("Data not found for customerID: " + customerID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data for editing: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string customerID = textBox1.Text;

            try
            {
                con.Open();
                string query = "select customerID from CustomerOne where customerID = '" + customerID + "'";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dataTable = new DataTable();
                da.Fill(dataTable);

                if (dataTable.Rows.Count == 0)
                {
                    String custID = textBox1.Text, Fname = textBox2.Text, Lname = textBox3.Text, contact = textBox4.Text, Town = textBox5.Text, City = textBox6.Text;
                    SqlCommand com = new SqlCommand("exec dbo.sp_insertCustomer1 '" + int.Parse(custID) + "','" + Fname + "','" + Lname + "','" + contact + "','" + Town + "','" + City + "'", con);
                    com.ExecuteNonQuery();
                    MessageBox.Show("Customer Added");
                }
                else
                {
                    String custID = textBox1.Text, Fname = textBox2.Text, Lname = textBox3.Text, contact = textBox4.Text, Town = textBox5.Text, City = textBox6.Text;
                    SqlCommand com = new SqlCommand("exec dbo.sp_updateCustomer1 '" + int.Parse(custID) + "','" + Fname + "','" + Lname + "','" + contact + "','" + Town + "','" + City + "'", con);
                    com.ExecuteNonQuery();
                    MessageBox.Show("Customer Updated");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message);
            }
            finally
            {
                con.Close();
                Floriculture_Customer floriculture_Customer = new Floriculture_Customer();
                this.Hide();
                floriculture_Customer.Show();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Floriculture_Customer floriculture_Customer = new Floriculture_Customer();
            this.Hide();
            floriculture_Customer.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Floriculture_Customer floriculture_Customer1 = new Floriculture_Customer();
            this.Hide();
            floriculture_Customer1.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Floriculture_Plants floriculture_ = new Floriculture_Plants();
            this.Hide();
            floriculture_.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            settings.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Sales1 sales1 = new Sales1();
            this.Hide();
            sales1.Show();
        }

        private void P_AE_Customer_Load(object sender, EventArgs e)
        {
            LoadAllRecords();
        }
        void LoadAllRecords()
        {

        }

    }
}
