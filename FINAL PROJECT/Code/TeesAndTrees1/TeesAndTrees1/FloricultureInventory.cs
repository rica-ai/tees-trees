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
    public partial class FloricultureInventory : Form
    {
        public FloricultureInventory()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           Floriculture_Customer floriculture_Customer = new Floriculture_Customer();
            this.Hide();
            floriculture_Customer.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Floriculture_Plants floriculture_ = new Floriculture_Plants();
            this.Hide();
            floriculture_.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Sales1 sales1 = new Sales1();
            this.Hide();
            sales1.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Main main = new Main();
            this.Hide();
            main.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            this.Hide();
            settings.Show();
        }
    }
}
