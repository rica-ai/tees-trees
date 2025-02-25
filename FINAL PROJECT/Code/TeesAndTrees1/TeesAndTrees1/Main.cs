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
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(" Data Source=DESKTOP-5BFSRUD\\SQLEXPRESS;Initial Catalog=TeesAndTrees;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        private void button1_Click(object sender, EventArgs e)
        {
            ShirtInventory shirtInventory = new ShirtInventory();
            this.Hide();
            shirtInventory.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FloricultureInventory floricultureInventory = new FloricultureInventory();
            this.Hide();
            floricultureInventory.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Logout();
        }
        private void Logout()
        {
            DialogResult result = MessageBox.Show("Are you sure you want to logout?", "Logout Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Register registerForm = new Register();
                this.Hide();
                registerForm.Show();
            }
            
        }
    }
}
