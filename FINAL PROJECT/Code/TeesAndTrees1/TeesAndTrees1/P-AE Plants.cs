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
    public partial class P_AE_Plants : Form
    {
        private int plantID;
        public P_AE_Plants(Floriculture_Plants parentForm, int plantID)
        {
            InitializeComponent();
            this.plantID = plantID;

            LoadDataForEditing(plantID);
        }
        public P_AE_Plants()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-5BFSRUD\\SQLEXPRESS;Initial Catalog=TeesAndTrees;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        private void LoadDataForEditing(int plantID)
        {
            try
            {
                con.Open();
                string query = "SELECT * FROM Plant_Inventory WHERE plantID = @plantID";
                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@plantID", plantID);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // Populate textboxes with the loaded data
                    textBox1.Text = reader["plantID"].ToString();
                    textBox2.Text = reader["plantName"].ToString();
                    textBox3.Text = reader["species"].ToString();
                    textBox4.Text = reader["price"].ToString();
                    textBox5.Text = reader["plantStock"].ToString();

                    // Check for DBNull before trying to read
                    object stocksValue = reader["plantStock"];
                    textBox5.Text = (stocksValue != DBNull.Value) ? stocksValue.ToString() : string.Empty;
                }
                else
                {
                    MessageBox.Show("Data not found for plantID: " + plantID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            string plantID = textBox1.Text;

            try
            {
                con.Open();
                string query = "select plantID from Plant_Inventory where plantID = '" + plantID + "'";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dataTable = new DataTable();
                da.Fill(dataTable);

                if (dataTable.Rows.Count == 0)
                {
                    String PlantID = textBox1.Text, Pname = textBox2.Text, specie = textBox3.Text, price = textBox4.Text, Pstocks = textBox5.Text;
                    SqlCommand com = new SqlCommand("exec dbo.sp_insertPlants '" + int.Parse(PlantID) + "','" + Pname + "','" + specie + "','" + decimal.Parse(price) + "'," + int.Parse(Pstocks), con);
                    com.ExecuteNonQuery();
                    MessageBox.Show("Plants Added");
                }
                else
                {
                    String PlantID = textBox1.Text, Pname = textBox2.Text, specie = textBox3.Text, price = textBox4.Text, Pstocks = textBox5.Text;
                    SqlCommand com = new SqlCommand("exec dbo.sp_updatePlants '" + int.Parse(PlantID) + "','" + Pname + "','" + specie + "','" + decimal.Parse(price) + "'," + int.Parse(Pstocks), con);
                    com.ExecuteNonQuery();
                    MessageBox.Show("Plants Updated");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message);
            }
            finally
            {
                con.Close();

                Floriculture_Plants floriculture_Plants = new Floriculture_Plants();
                this.Hide();
                floriculture_Plants.Show();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Floriculture_Plants floriculture = new Floriculture_Plants();
            this.Hide();
            floriculture.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            this.Hide();
            settings.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Floriculture_Plants floriculture_Plants = new Floriculture_Plants();
            this.Hide();
            floriculture_Plants.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Floriculture_Customer floriculture_ = new Floriculture_Customer();
            this.Hide();
            floriculture_.Show();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Sales1 sales1 = new Sales1();
            sales1.Show();
            this.Hide();
        }

        private void P_AE_Plants_Load(object sender, EventArgs e)
        {

        }
    }
}
