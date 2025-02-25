using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TeesAndTrees1
{
    public partial class Shirt_Customized : Form
    {
        public Shirt_Customized()
        {
            InitializeComponent();

        }

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-5BFSRUD\\SQLEXPRESS;Initial Catalog=TeesAndTrees;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        private void Shirt_Customized_Load(object sender, EventArgs e)
        {
            loadShirtRecords();
        }

        //load records
        private void loadShirtRecords()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("exec dbo.sp_viewShirt",con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch(Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message);
            }
        }

        //add
        private void button8_Click(object sender, EventArgs e)
        {
            AE_Shirt_Customized aeshirtform = new AE_Shirt_Customized();
            aeshirtform.Show();
            this.Hide();
        }

        //edit
        private void button9_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedTshirtID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["T-SHIRT ID"].Value);

                AE_Shirt_Customized aeShirtForm = new AE_Shirt_Customized(this, selectedTshirtID);
                aeShirtForm.Show();
                this.Close();
            }
        }

        //delete
        private void button7_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedTshirtID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["T-SHIRT ID"].Value);

                // Assuming you have a method for deleting records
                DeleteShirtRecord(selectedTshirtID);

                // Refresh the DataGridView after deletion
                loadShirtRecords();

                MessageBox.Show("Record deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void DeleteShirtRecord(int tshirtID)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to delete?", "Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    con.Open();

                    // Assuming you have a stored procedure for deleting records
                    SqlCommand com = new SqlCommand("exec dbo.sp_DeleteShirt '" + tshirtID + "'", con);
                    com.ExecuteNonQuery();
                    loadShirtRecords();
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

        private void button10_Click(object sender, EventArgs e, object parentForm)
        {
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            SqlCommand com = new SqlCommand("exec dbo.sp_searchShirt'" + int.Parse(textBox1.Text) + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            ShirtInventory shirtInventory = new ShirtInventory();
            this.Hide();
            shirtInventory.Show();
        }

        //view all
        private void button11_Click(object sender, EventArgs e)
        {
            LoadAllRecords();
            textBox1.Clear();
        }
        void LoadAllRecords()
        {   
            SqlCommand com = new SqlCommand("exec dbo.sp_viewShirt", con);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

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

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
