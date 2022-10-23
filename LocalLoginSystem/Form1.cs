using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace LocalLoginSystem
{
    public partial class LoginPage : Form
    {
        #region Private members

        /// <summary>
        /// A list of usernames filled from outer resources such as text files and databases
        /// </summary>
        private readonly List<string> usernameList = new List<string>();
        private readonly List<string> passwordList = new List<string>();
        private readonly List<string> roleList = new List<string>();
        private readonly List<string> firstNameList = new List<string>();
        private readonly List<string> lastNameList = new List<string>();
        private readonly List<string> dateList = new List<string>();
        private readonly List<string> userDetail = new List<string>();


        #endregion

        #region Сonstructor

        public LoginPage()
        {
            InitializeComponent();
        }

        #endregion

        /// <summary>
        /// Load usernames and passwords on startup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            // Find a text file with usernames and passwords
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
                passwordList.Add(componentsUser[1]);
                roleList.Add(componentsUser[2]);
                firstNameList.Add(componentsUser[3]);
                lastNameList.Add(componentsUser[4]);
                dateList.Add(componentsUser[5]);
            }

            // Close the StreamReader
            sr.Close();
        }

        /// <summary>
        /// Click the Login button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (usernameList.Contains(textBox1.Text) && passwordList.Contains(textBox2.Text) && passwordList[usernameList.IndexOf(textBox1.Text)] == textBox2.Text)
            {
                this.Hide();
                string role = roleList[usernameList.IndexOf(textBox1.Text)];
                int index = usernameList.IndexOf(textBox1.Text);
                userDetail.Add(usernameList[index]);
                userDetail.Add(roleList[index]);
                userDetail.Add(firstNameList[index]);
                userDetail.Add(lastNameList[index]);
                userDetail.Add(dateList[index]);

                TextEditor form2 = new TextEditor(userDetail);
                form2.Closed += (s, args) => this.Close();
                form2.Show();
            }
            else
                // Show the error message
                MessageBox.Show("The username and/or password is incorrect.");
        }



        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            RegisterPage f = new RegisterPage();
            f.Closed += (s, args) => this.Close();
            f.Show();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
