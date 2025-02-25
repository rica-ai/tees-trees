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
    public partial class AE_Materials : Form
    {
        private int materialID;
        public AE_Materials(Shirt_Material parentForm, int materialID )
        {
            InitializeComponent();
            this.materialID = materialID;

            // Load data associated with tshirtID into textboxes
            LoadDataForEditing(materialID);
        }
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-5BFSRUD\\SQLEXPRESS;Initial Catalog=TeesAndTrees;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        public AE_Materials()
        {
            InitializeComponent();
        }
        private void AE_Materials_Load(object sender, EventArgs e)
        {

        }
        private void LoadDataForEditing(int materialID)
        {
            try
            {
                con.Open();
                string query = "SELECT * FROM Printing_Materials WHERE materialID = @materialID";
                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@materialID", materialID);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // Populate textboxes with the loaded data
                    textBox1.Text = reader["materialID"].ToString();
                    textBox2.Text = reader["materialName"].ToString();
                    textBox3.Text = reader["materialType"].ToString();
                    textBox4.Text = reader["materialAvailable"].ToString();

                    // Check for DBNull before trying to read
                    object stocksValue = reader["materialAvailable"];
                    textBox4.Text = (stocksValue != DBNull.Value) ? stocksValue.ToString() : string.Empty;
                }
                else
                {
                    MessageBox.Show("Data not found for materialID: " + materialID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            string materialID = textBox1.Text;

            try
            {
                con.Open();
                string query = "select materialID from Printing_Materials where materialID = '" + materialID + "'";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dataTable = new DataTable();
                da.Fill(dataTable);

                if (dataTable.Rows.Count == 0)
                {
                    String MaterialID = textBox1.Text, Mname = textBox2.Text, Mtype = textBox3.Text, Mstocks = textBox4.Text;
                    SqlCommand com = new SqlCommand("exec dbo.sp_insertMaterials '" + int.Parse(MaterialID) + "','" + Mname + "','" + Mtype + "','" + int.Parse(Mstocks) + "'", con);
                    com.ExecuteNonQuery();
                    MessageBox.Show("Equipment Added");
                }
                else
                {
                    String MaterialID = textBox1.Text, Mname = textBox2.Text, Mtype = textBox3.Text, Mstocks = textBox4.Text;
                    SqlCommand com = new SqlCommand("exec dbo.sp_updateMaterials '" + int.Parse(MaterialID) + "','" + Mname + "','" + Mtype + "','" + int.Parse(Mstocks) + "'", con); 
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

                Shirt_Material shirt_Material = new Shirt_Material ();
                this.Hide();
                shirt_Material.Show();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Shirt_Material shirt_Material = new Shirt_Material();
            this.Hide();
            shirt_Material.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Sales sales = new Sales();
            sales.Show();
            this.Hide();
        }
    }
}
