using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LocalLoginSystem
{
    public partial class RegisterPage : Form
    {
        private readonly List<string> usernameList = new List<string>();
        public RegisterPage()
        {
            loadUser();
            InitializeComponent();
            this.Text = "Register New User";
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            redirectToHome();
        }

        private void loadUser()
        {
            StreamReader sr = new StreamReader("login.txt");
            // Create an empty string which will be filled with a text file line
            string line;

            // If the text file still has lines...
            while ((line = sr.ReadLine()) != null)
            {
                // split the line with a whitespace
                string[] components = line.Split("\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                string[] componentsUser = line.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                // add the first part of the line to the list of usernames
                usernameList.Add(componentsUser[0]);
            }

            // Close the StreamReader
            sr.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (usernameList.Contains(textBox1.Text))
            {
                textBox1.Text = String.Empty;
                MessageBox.Show("Please Search another unique username");
                return;
            }
            string username = textBox1.Text;
            string password = textBox2.Text;
            string repassword = textBox3.Text;
            string firstName = textBox4.Text;
            string lastName = textBox5.Text;
            string date = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            string UserType = comboBox1.Text;
            if (firstName == "" || username == "" || password == "" || lastName == "" || UserType == "")
            {
                MessageBox.Show("Required Field, Please fill field correctly");
                return;
            }

            if (password != repassword)
            {
                MessageBox.Show("Password And Repassword Not Match");
                return;
            }


            TextWriter tw = new StreamWriter("login.txt", true);
            tw.WriteLine(username + "," + password + "," + UserType + "," + firstName + "," + lastName + "," + date);
            tw.Close();
            MessageBox.Show("Success Register, Please Login...");
            redirectToHome();
            // close the stream
        }

        private void redirectToHome()
        {
            this.Hide();
            LoginPage f = new LoginPage();
            f.Closed += (s, args) => this.Close();
            f.Show();
        }
    }
}
