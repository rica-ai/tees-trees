﻿using System;
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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(" Data Source=DESKTOP-5BFSRUD\\SQLEXPRESS;Initial Catalog=TeesAndTrees;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.PasswordChar = checkBox1.Checked ? '\0' : '*';
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String userName, password;
            userName = textBox1.Text;
            password = textBox2.Text;

            try
            {
                con.Open();
                string querry = "insert into registerApp (userName, pass) values ('" + userName + "','" + password + "')";
                SqlCommand com = new SqlCommand(querry, con);
                com.ExecuteNonQuery();
                MessageBox.Show("Account Registered");

                Register registerForm = new Register();
                this.Hide();
                registerForm.Show();
            }
            catch
            {
                MessageBox.Show("Error");
            }
            finally
            {
                con.Close();
            }
        }
    }
}
