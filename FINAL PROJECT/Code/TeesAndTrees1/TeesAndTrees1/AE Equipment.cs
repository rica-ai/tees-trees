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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TeesAndTrees1
{
    public partial class AE_Equipment : Form
    {
        private int equipmentID;
        public AE_Equipment(Shirt_Equipment parentForm, int equipmentID)
        {
            InitializeComponent();
            this.equipmentID = equipmentID;

            // Load data associated with tshirtID into textboxes
            LoadDataForEditing(equipmentID);
        }

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-5BFSRUD\\SQLEXPRESS;Initial Catalog=TeesAndTrees;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        public AE_Equipment()
        {
            InitializeComponent();
        }

        private void AE_Equipment_Load(object sender, EventArgs e)
        {
            
        }

        private void LoadDataForEditing(int equipmentID)
        {
            try
            {
                con.Open();
                string query = "SELECT * FROM Printing_Equipment WHERE equipmentID = @equipmentID";
                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@equipmentID", equipmentID);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // Populate textboxes with the loaded data
                    textBox1.Text = reader["equipmentID"].ToString();
                    comboBox1.Text = reader["equipmentType"].ToString();
                    textBox3.Text = reader["equipmentBrand"].ToString();

                    // Check for DBNull before trying to read
                    object stocksValue = reader["equipmentStocks"];
                    textBox4.Text = (stocksValue != DBNull.Value) ? stocksValue.ToString() : string.Empty;
                }
                else
                {
                    MessageBox.Show("Data not found for equipmentID: " + equipmentID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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


        //add / edit
        private void button7_Click(object sender, EventArgs e)
        {
            string equipmentID = textBox1.Text;

            try
            {
                con.Open();
                string query = "select equipmentID from Printing_Equipment where equipmentID = '" + equipmentID + "'";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dataTable = new DataTable();
                da.Fill(dataTable);

                if (dataTable.Rows.Count == 0)
                {
                    String EquipmentID = textBox1.Text, type = comboBox1.Text, brand = textBox3.Text, stocks = textBox4.Text;
                    SqlCommand com = new SqlCommand("exec dbo.sp_insertEquipment '" + int.Parse(EquipmentID) + "','" + type + "','" + brand + "','" + int.Parse(stocks) + "'", con);
                    com.ExecuteNonQuery();
                    MessageBox.Show("Equipment Added");
                }
                else
                {
                    String EquipmentID = textBox1.Text, type = comboBox1.Text, brand = textBox3.Text, stocks = textBox4.Text;
                    SqlCommand com = new SqlCommand("exec dbo.sp_updateEquipment '" + int.Parse(EquipmentID) + "','" + type + "','" + brand + "','" + int.Parse(stocks) + "'", con);
                    com.ExecuteNonQuery();
                    MessageBox.Show("Equipment Updated");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message);
            }
            finally
            {
                con.Close();

                Shirt_Equipment shirt_Equipment = new Shirt_Equipment();
                this.Hide();
                shirt_Equipment.Show();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Shirt_Equipment shirt_Equipment = new Shirt_Equipment();
            this.Hide();
            shirt_Equipment.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AE_Equipment aE_Equipment = new AE_Equipment();
            this.Hide();
            aE_Equipment.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Shirt_Customized customized = new Shirt_Customized();
            this.Hide();
            customized.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Shirt_Customer shirt_Customer = new Shirt_Customer();
            this.Show();
            shirt_Customer.Hide();
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
