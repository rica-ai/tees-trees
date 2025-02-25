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
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-5BFSRUD\\SQLEXPRESS;Initial Catalog=TeesAndTrees;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");


        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Main main = new Main();
            this.Hide();
            main.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Sales sales = new Sales();
            this.Hide();
            sales.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Sales1 sales1 = new Sales1();
            this.Hide();
            sales1.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SignOut();
        }
        private void SignOut()
        {
            DialogResult result = MessageBox.Show("Are you sure you want to signout?", "Sign Out Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Register registerForm = new Register();
                this.Hide();
                registerForm.Show();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to delete your account?", "Delete Account", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    con.Open();

                    // Assuming you have a stored procedure for deleting records
                    SqlCommand com = new SqlCommand("delete from registerApp", con);
                    com.ExecuteNonQuery();
                 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting record: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Register register = new Register();
                this.Hide(); 
                register.Show();
                con.Close();
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                int currentUserId = GetCurrentUserID();

                if (currentUserId == 0)
                {
                    MessageBox.Show("Unable to identify the current user. Please log in again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string currentUsername = textBox1.Text; 
                string currentPassword = textBox5.Text; 
                string newUsername = textBox3.Text;   
                string newPassword = textBox2.Text;    
                string confirmPassword = textBox4.Text;

                // Validate the input
                if (string.IsNullOrWhiteSpace(newUsername) || string.IsNullOrWhiteSpace(newPassword) || string.IsNullOrWhiteSpace(confirmPassword))
                {
                    MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (newPassword != confirmPassword)
                {
                    MessageBox.Show("New password and confirm password do not match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                con.Open();

                // Check if the current username and password are valid
                using (SqlCommand checkCommand = new SqlCommand("SELECT COUNT(*) FROM registerApp WHERE userID = @userID AND userName = @userName AND pass = @pass", con))
                {
                    checkCommand.Parameters.AddWithValue("@userID", currentUserId);
                    checkCommand.Parameters.AddWithValue("@userName", currentUsername);
                    checkCommand.Parameters.AddWithValue("@pass", currentPassword);

                    int count = (int)checkCommand.ExecuteScalar();

                    if (count == 0)
                    {
                        MessageBox.Show("Invalid current username or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                // Update the user profile
                using (SqlCommand updateCommand = new SqlCommand("EXEC sp_updateUser @userID, @newUsername, @newPassword", con))
                {
                    updateCommand.Parameters.AddWithValue("@userID", currentUserId);
                    updateCommand.Parameters.AddWithValue("@newUsername", newUsername);
                    updateCommand.Parameters.AddWithValue("@newPassword", newPassword);

                    updateCommand.ExecuteNonQuery();
                }

                MessageBox.Show("User profile updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating user profile: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }

        }
        private int AuthenticateUser(string username, string password)
        {
            int userID = 0;

            try
            {
                con.Open();

                string query = "SELECT userID FROM registerApp WHERE userName = @userName AND pass = @password";
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    command.Parameters.AddWithValue("@userName", username);
                    command.Parameters.AddWithValue("@password", password);

                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        userID = Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error authenticating user: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                
                con.Close();
            }

            return userID;
        }

        private int GetCurrentUserID()
        {
            string currentUsername = textBox1.Text; 
            string currentPassword = textBox5.Text; 

            int currentUserId = AuthenticateUser(currentUsername, currentPassword);

            return currentUserId;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox5.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.PasswordChar = checkBox1.Checked ? '\0' : '*';
            textBox4.PasswordChar = checkBox1.Checked ? '\0' : '*';
        }
    }
}