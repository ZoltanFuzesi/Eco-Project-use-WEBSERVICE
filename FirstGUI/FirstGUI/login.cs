using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FirstGUI
{
    public partial class login : Form
    {
        private Form1 mainWindow;
        public login()
        {
            InitializeComponent();
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }


        //Login button check for credentials
        private void button2_Click(object sender, EventArgs e)
        {
            string pagesource = "Check your connection";
            string username = textBox9.Text;
            string password = textBox1.Text;
            string urlAddress = "???";//Hide only

            if (username.Length > 0 && password.Length > 0)
            {
                string logIN = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                {
                    { "username", username }, 
                    { "password", password },
                    { "LogTime", logIN }
                };


                    try
                    {
                        pagesource = Encoding.UTF8.GetString(client.UploadValues(urlAddress, postData));

                    }
                    catch (Exception) { MessageBox.Show(pagesource); }
        
                }
            }
            else
            {
                MessageBox.Show("Please fill up User name & Password ! ");
            }
            if (pagesource.Equals("SUCCES"))
            {
                string logIN = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                mainWindow = new Form1(username, password, logIN, urlAddress);
                this.Hide();
                mainWindow.Show();
            }
            else
            {
                MessageBox.Show(pagesource);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
