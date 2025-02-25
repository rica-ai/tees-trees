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
    public partial class Print1 : Form
    {
        public Print1()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-5BFSRUD\\SQLEXPRESS;Initial Catalog=TeesAndTrees;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        private void Print1_Load(object sender, EventArgs e)
        {
            LoadACustomerName();
        }
        void LoadACustomerName()
        {
            SqlCommand com = new SqlCommand("exec dbo.sp_viewCustName1", con);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Check if a row is selected
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Retrieve the selected row
                DataGridViewRow row = dataGridView1.SelectedRows[0];

                // Clear the contents of the RichTextBox
                richTextBox1.Clear();

                // Construct the receipt information
                StringBuilder receiptInfo = new StringBuilder();
                richTextBox1.Clear();


                
                receiptInfo.AppendLine(" ╔═══════════════════════════════════════════════════════");
                receiptInfo.AppendLine("║                    RECIEPT OF YOUR PLANT ORDERS!                     ║");
                receiptInfo.AppendLine(" ╚═══════════════════════════════════════════════════════");

                // Add your custom column names and their values to the receipt information
                receiptInfo.AppendLine($"════════════════════════════════════════════════════════");
                receiptInfo.AppendLine($"Transaction ID:                          {row.Cells["Transaction ID"].Value}                                                                    ");
                receiptInfo.AppendLine($"Product ID:                                 {row.Cells["Product ID"].Value}                                                                          ");
                receiptInfo.AppendLine($"Quantity:                                     {row.Cells["Quantity"].Value}                                                                                  ");
                receiptInfo.AppendLine($"TotalPrice:                                  {row.Cells["TotalPrice"].Value}                                                                           ");
                receiptInfo.AppendLine($"Sale Date:                                   {row.Cells["Sale Date"].Value}                                                                                        ");
                receiptInfo.AppendLine($"Customer Name:                       {row.Cells["Customer Name"].Value}                                                                      ");
                 
                receiptInfo.AppendLine(" ╔═══════════════════════════════════════════════════════");
                receiptInfo.AppendLine("║                             THANKYOU FOR TRUSTING!                         ║");
                receiptInfo.AppendLine(" ╚═══════════════════════════════════════════════════════");

                // Display the receipt information in the RichTextBox
                richTextBox1.Text = receiptInfo.ToString();
            }
            else
            {
                MessageBox.Show("Please select a row.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString(richTextBox1.Text, new Font("Microsoft San Serif", 20, FontStyle.Bold), Brushes.Black, new Point(10, 10));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Sales1 sales1 = new Sales1();
            this.Hide();
            sales1.Show();
        }
    }
}
