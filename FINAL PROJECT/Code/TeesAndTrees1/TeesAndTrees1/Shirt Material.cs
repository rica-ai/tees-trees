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
    public partial class Shirt_Material : Form
    {
        public Shirt_Material()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-5BFSRUD\\SQLEXPRESS;Initial Catalog=TeesAndTrees;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        private void Shirt_Material_Load(object sender, EventArgs e)
        {
            loadMaterialRecords();
        }

        //load records
        private void loadMaterialRecords()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("exec dbo.sp_viewMaterials", con);
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

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            ShirtInventory shirtInventory = new ShirtInventory();
            this.Hide();
            shirtInventory.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Shirt_Customer shirt_Customer = new Shirt_Customer();
            this.Show();
            shirt_Customer.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Shirt_Customized customized = new Shirt_Customized();
            this.Hide();
            customized.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Shirt_Equipment shirt_Equipment = new Shirt_Equipment();
            this.Hide();
            shirt_Equipment.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            this.Hide();
            settings.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Shirt_Material shirt_Material = new Shirt_Material();
            this.Hide();
            shirt_Material.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            SqlCommand com = new SqlCommand("exec dbo.sp_searchmaterials'" + int.Parse(textBox1.Text) + "'", con);
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
            SqlCommand com = new SqlCommand("exec dbo.sp_viewMaterials", con);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            AE_Materials aE_Materials = new AE_Materials();
            aE_Materials.Show();
            this.Hide();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedMaterial = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["MATERIAL ID"].Value);

                AE_Materials aE_Materials = new AE_Materials(this, selectedMaterial);
                aE_Materials.Show();
                this.Close();
            }
        }
        private void DeleteMaterialRecord(int materialID)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to delete?", "Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    con.Open();

                    // Assuming you have a stored procedure for deleting records
                    SqlCommand com = new SqlCommand("exec dbo.sp_DeleteMaterials'" + materialID + "'", con);
                    com.ExecuteNonQuery();
                    loadMaterialRecords();
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

        private void button10_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Sales sales = new Sales();
            sales.Show();
            this.Hide();
        }
    }
}
