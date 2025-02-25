using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TeesAndTrees1
{
    public partial class Sales1 : Form
    {
        public Sales1()
        {
            InitializeComponent();
            loadRecords();
        }

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-5BFSRUD\\SQLEXPRESS;Initial Catalog=TeesAndTrees;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string plantID = textBox2.Text;

            if (int.TryParse(plantID, out int parseProdID))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("dbo.sp_SelectProdIDplant", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@plantID", parseProdID);

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            label12.Text = dt.Rows[0]["Plant Name"].ToString();
                            // Additional code for other columns if needed
                        }
                        else
                        {
                            label12.Text = "No Data";
                        }

                    }
                    using (SqlCommand command = new SqlCommand("dbo.sp_selectQuantityPlant", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@plantID", parseProdID);

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            label14.Text = dt.Rows[0]["Plant Stocks"].ToString();
                            // Additional code for other columns if needed
                        }
                        else
                        {
                            label14.Text = "No Data";
                        }
                    }
                    using (SqlCommand command = new SqlCommand("dbo.sp_selectPriceTransacPlant", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@plantID", parseProdID);

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            label7.Text = dt.Rows[0]["Price"].ToString();
                            // Additional code for other columns if needed
                        }
                        else
                        {
                            label7.Text = "No Data";
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string quan = textBox3.Text, price = label7.Text;
                decimal sellPrice = decimal.Parse(price.ToString());
                decimal TotPrice = decimal.Parse(quan) * sellPrice;

                label15.Text = TotPrice.ToString();


            }
            catch
            {
                label15.Text = "Invalid";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string transacID = textBox1.Text, productID = textBox2.Text, qntySold = textBox3.Text, totPrice = label15.Text, custID = textBox5.Text;
            DateTime date = dateTimePicker1.Value;

            try
            {
                con.Open();
                SqlCommand sqlCommand = new SqlCommand("exec dbo.sp_insertPlantTransac '" + int.Parse(transacID) + "','" + int.Parse(productID) + "','" + int.Parse(qntySold) + "','" + decimal.Parse(totPrice) + "','" + date + "','" + int.Parse(custID) + "'", con);
                sqlCommand.ExecuteNonQuery();
                MessageBox.Show("Transaction Added");
                loadRecords();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                textBox1.Clear();
                textBox2.Clear( );
                textBox3.Clear( );
                textBox5.Clear( );
                con.Close();
            }
        }

        private void loadRecords()
        {
            SqlCommand command = new SqlCommand("exec dbo.sp_viewPlantTransac", con);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            sqlDataAdapter.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string transacID = textBox1.Text, productID = textBox2.Text, qntySold = textBox3.Text, totPrice = label15.Text, custID = textBox5.Text;
            DateTime date = dateTimePicker1.Value;

            try
            {
                con.Open();
                SqlCommand sqlCommand = new SqlCommand("exec dbo.sp_updatePlantTransac '" + int.Parse(transacID) + "','" + int.Parse(productID) + "','" + int.Parse(qntySold) + "','" + decimal.Parse(totPrice) + "','" + date + "','" + int.Parse(custID) + "'", con);
                sqlCommand.ExecuteNonQuery();
                MessageBox.Show("Transaction Updated");
                loadRecords();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox5.Clear();
                con.Close();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string transacID = textBox1.Text;
            try
            {
                if (MessageBox.Show("Are you sure you want to delete?", "Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    con.Open();

                    // Assuming you have a stored procedure for deleting records
                    SqlCommand com = new SqlCommand("exec dbo.sp_deletePlantTransac '" + transacID + "'", con);
                    MessageBox.Show("Transaction Deleted");
                    com.ExecuteNonQuery();
                    loadRecords();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting record: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                textBox1.Clear();
                textBox2.Clear( );
                textBox3.Clear( );
                textBox5.Clear( );
                label12.Text = "";
                label14.Text = "";
                label7.Text = "";
                label15.Text = "";
                con.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Sales sales = new Sales();
            sales.Show();
            this.Hide();    
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            settings.Show();
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                SqlCommand command = new SqlCommand("exec dbo.sp_viewCustNamePlant '" + textBox4.Text + "'", con);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
                DataTable table = new DataTable();
                sqlDataAdapter.Fill(table);
                dataGridView1.DataSource = table;
            }
            else
            {
                SqlCommand command = new SqlCommand("exec dbo.sp_viewCustNamePlant  '" + textBox4.Text + "'", con);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
                DataTable table = new DataTable();
                sqlDataAdapter.Fill(table);
                dataGridView1.DataSource = table;

                loadRecords();
                textBox4.Clear();
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox5.Clear();
                label12.Text = "";
                label14.Text = "";
                label7.Text = "";
                label15.Text = "";

            }
           
        }

        private void button11_Click(object sender, EventArgs e)
        {
            loadRecords();
            textBox4.Clear();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox5.Clear();
            label12.Text = "";
            label14.Text = "";
            label7.Text = "";
            label15.Text = "";
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Main main = new Main();
            main.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "PDF (*.pdf)|*.pdf";
                save.FileName = "Result";

                if (save.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Check if the file already exists and delete it
                        if (File.Exists(save.FileName))
                        {
                            File.Delete(save.FileName);
                        }

                        // Create PDF table
                        PdfPTable ptable = new PdfPTable(dataGridView1.Columns.Count);
                        ptable.DefaultCell.Padding = 2;
                        ptable.WidthPercentage = 100;
                        ptable.HorizontalAlignment = Element.ALIGN_LEFT;

                        // Add column headers to the PDF table
                        foreach (DataGridViewColumn col in dataGridView1.Columns)
                        {
                            PdfPCell pCell = new PdfPCell(new Phrase(col.HeaderText));
                            ptable.AddCell(pCell);
                        }

                        // Add data rows to the PDF table
                        foreach (DataGridViewRow viewRow in dataGridView1.Rows)
                        {
                            foreach (DataGridViewCell cell in viewRow.Cells)
                            {
                                PdfPCell pCell = new PdfPCell(new Phrase(cell.Value?.ToString()));
                                ptable.AddCell(pCell);
                            }
                        }

                        // Create PDF document and write the table to it
                        using (FileStream fileStream = new FileStream(save.FileName, FileMode.Create))
                        {
                            Document document = new Document(PageSize.A4, 8f, 16f, 16f, 8f);
                            PdfWriter.GetInstance(document, fileStream);

                            document.Open();
                            document.Add(ptable);
                            document.Close();
                        }

                        MessageBox.Show("Data Exported Successfully", "Info");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error while exporting data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("No Record Found", "Info");
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                LoadACustomerName();
            }
            
        }
        void LoadACustomerName()
        {
            SqlCommand com = new SqlCommand("exec dbo.sp_viewCustName1", con);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void Sales1_Load(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            Print1 print = new Print1();
            this.Hide();
            print.Show();
        }
    }
}
