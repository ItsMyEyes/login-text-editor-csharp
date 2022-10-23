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
        public RegisterPage()
        {
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

        private void button1_Click(object sender, EventArgs e)
        {
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
