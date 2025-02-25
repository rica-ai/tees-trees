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
    public partial class AE_Shirt_Customized : Form
    {

        private int tshirtID;

        public AE_Shirt_Customized(Shirt_Customized parentForm, int tshirtID)
        {
            InitializeComponent();
            this.tshirtID = tshirtID;

            // Load data associated with tshirtID into textboxes
            LoadDataForEditing(tshirtID);
        }

        public AE_Shirt_Customized()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-5BFSRUD\\SQLEXPRESS;Initial Catalog=TeesAndTrees;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
private void LoadDataForEditing(int tshirtID)
        {
            try
            {
                con.Open();
                string query = "SELECT * FROM Tshirt_Inventory WHERE tshirtID = @tshirtID";
                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@tshirtID", tshirtID);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // Populate textboxes with the loaded data
                    textBox1.Text = reader.GetInt32(reader.GetOrdinal("tshirtID")).ToString();
                    textBox2.Text = reader.GetString(reader.GetOrdinal("tshirtCaption"));
                    textBox3.Text = reader.GetString(reader.GetOrdinal("color"));
                    textBox5.Text = reader.GetDecimal(reader.GetOrdinal("price")).ToString();
                    textBox4.Text = reader.GetInt32(reader.GetOrdinal("orderCount")).ToString();
                }

                else
                {
                    MessageBox.Show("Data not found for tshirtID: " + tshirtID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        

        private void AE_Shirt_Customized_Load(object sender, EventArgs e)
        {

        }

        //add and edit
        private void button7_Click(object sender, EventArgs e)
        {
            string tshirtID = textBox1.Text;

            try
            {
                con.Open();
                string query = "select tshirtID from Tshirt_Inventory where tshirtID = '" + tshirtID + "'";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dataTable = new DataTable();
                da.Fill(dataTable);

                if (dataTable.Rows.Count == 0)
                {
                    String TshirtID = textBox1.Text, caption = textBox2.Text, color = textBox3.Text, price = textBox5.Text, orderNum = textBox4.Text;
                    SqlCommand com = new SqlCommand("exec dbo.sp_insertShirt '" + int.Parse(TshirtID) + "','" + caption + "','" + color + "','" + decimal.Parse(price) + "','" + orderNum + "'", con);
                    com.ExecuteNonQuery();
                    MessageBox.Show("Tshirt Added");
                }
                else
                {
                    String TshirtID = textBox1.Text, caption = textBox2.Text, color = textBox3.Text, price = textBox5.Text, orderNum = textBox4.Text;
                    SqlCommand com = new SqlCommand("exec dbo.sp_updateShirt '" + int.Parse(TshirtID) + "','" + caption + "','" + color + "','" + decimal.Parse(price) + "','" + orderNum + "'", con);
                    com.ExecuteNonQuery();
                    MessageBox.Show("Tshirt Updated");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message);
            }
            finally
            {
                con.Close();

                Shirt_Customized shirt_Customized = new Shirt_Customized();

                this.Hide();
                shirt_Customized.Show();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Shirt_Customized shirt_Customized = new Shirt_Customized();
            this.Hide();
            shirt_Customized.Show();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        

        private void pictureBox9_Click_1(object sender, EventArgs e)
        {
            Shirt_Customized shirt_Customized = new Shirt_Customized();
            this.Hide();
            shirt_Customized.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Shirt_Customer shirt_Customer = new Shirt_Customer();
            this.Hide();
            shirt_Customer.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Shirt_Equipment shirt_Equipment = new Shirt_Equipment();
            this.Hide();
            shirt_Equipment.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Shirt_Material shirt_Material = new Shirt_Material();
            this.Hide();
            shirt_Material.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {

            Settings settings = new Settings();
            this.Hide();
            settings.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Sales sales = new Sales();
            sales.Show();
            this.Hide();
        }
    }
}
