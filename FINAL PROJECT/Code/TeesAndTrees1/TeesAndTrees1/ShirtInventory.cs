using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TeesAndTrees1
{
    public partial class ShirtInventory : Form
    {
        public ShirtInventory()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Shirt_Customer shirt_Customer = new Shirt_Customer();
            this.Hide();
            shirt_Customer.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Shirt_Customized shirt_customized = new Shirt_Customized();
            this.Hide();
            shirt_customized.Show();
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
            this.Hide();
            Sales sales = new Sales();
            sales.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Main main = new Main();
            main.Show();
            this.Hide();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
