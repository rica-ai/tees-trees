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
    public partial class Shirt_Equipment : Form
    {
        public Shirt_Equipment()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-5BFSRUD\\SQLEXPRESS;Initial Catalog=TeesAndTrees;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        private void Shirt_Equipment_Load(object sender, EventArgs e)
        {
            loadEquipmentRecords();
        }

        //load records
        private void loadEquipmentRecords()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("exec dbo.sp_viewEquipment", con);
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

        //add
        private void button8_Click(object sender, EventArgs e)
        {
            AE_Equipment aE_Equipment = new AE_Equipment();
            aE_Equipment.Show();
            this.Hide();
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

        //search
        private void button7_Click(object sender, EventArgs e)
        {
            SqlCommand com = new SqlCommand("exec dbo.sp_searchEquipment'" + int.Parse(textBox1.Text) + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        //view all
        private void button11_Click(object sender, EventArgs e)
        {
            LoadAllRecords();
            textBox1.Clear();
        }
        void LoadAllRecords()
        {
            SqlCommand com = new SqlCommand("exec dbo.sp_viewEquipment", con);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }
        //edit
        private void button9_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedEquipmentID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["EQUIPMENT ID"].Value);

                AE_Equipment aE_Equipment= new AE_Equipment(this, selectedEquipmentID);
                aE_Equipment.Show();
                this.Close();
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedEquipmentID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["EQUIPMENT ID"].Value);

                // Assuming you have a method for deleting records
                DeleteEquipmentRecord(selectedEquipmentID);

                // Refresh the DataGridView after deletion
                loadEquipmentRecords();

                MessageBox.Show("Record deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void DeleteEquipmentRecord(int equipmentID)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to delete?", "Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    con.Open();

                    // Assuming you have a stored procedure for deleting records
                    SqlCommand com = new SqlCommand("exec dbo.sp_DeleteEquipment'" + equipmentID + "'", con);
                    com.ExecuteNonQuery();
                    loadEquipmentRecords();
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

        private void button3_Click(object sender, EventArgs e)
        {
            Shirt_Equipment shirt_Equipment = new Shirt_Equipment();
            this.Hide();
            shirt_Equipment.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Sales sales = new Sales();
            sales.Show();
            this.Hide();
        }
    }
}
