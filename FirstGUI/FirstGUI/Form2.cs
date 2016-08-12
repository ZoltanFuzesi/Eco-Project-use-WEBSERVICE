using System;
using System.Collections;
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
    public partial class Form2 : Form
    {
        private string username;
        private string password;
        string urlAddress = "";
        private string printMessage = "";

        public Form2(string username, string password,string urlAddress)
        {
            InitializeComponent();
            setDropBoxes();
            this.username = username;
            this.password = password;
            this.urlAddress = urlAddress;
            loadDropboxes();
            

        }

        //get url
        private string getUrl()
        {
            return urlAddress;
        }

        //set drop boxes style
        private void setDropBoxes()
        {

            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox4.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox5.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox6.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        //Load default fix drop boxes 
        private void loadDropboxes()
        {
            fillDropboxes("user","");
            fillDropboxes("customer","");
            fillDropboxes("allOtherLogs", "");

        }

        //Message area to print message
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            richTextBox1.ReadOnly = true;
           
        }

        //Close history window
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }


        //Clear button
        private void button10_Click(object sender, EventArgs e)
        {
            printMessage = "";
            richTextBox1.Text = printMessage;
        }

        //Build any drop boxes
        private void fillDropboxes(string webAction, string with)
        {

            string customerSource = "Default";
            string userSource = "Default";
            ArrayList names = new ArrayList();
            ArrayList id = new ArrayList();
            

            if (webAction.Equals("user"))
            {
                using (WebClient client2 = new WebClient())
                {
                    NameValueCollection postData2 = new NameValueCollection()
               {
                   { "username", username }, //order: {"parameter name", "parameter value"}
                   { webAction,  ""}
               };


                    try
                    {
                        userSource = Encoding.UTF8.GetString(client2.UploadValues(getUrl(), postData2));
                    }
                    catch (Exception) { userSource = "Catch"; MessageBox.Show(userSource); }

                }
                ArrayList b = getNames(userSource);
                userDrop(b);

            }
            else if (webAction.Equals("customer"))
            {
                //get names
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
               {
                   { "username", username }, //order: {"parameter name", "parameter value"}
                   { webAction, ""}
               };


                    try
                    {
                        customerSource = Encoding.UTF8.GetString(client.UploadValues(getUrl(), postData));
                    }
                    catch (Exception) { customerSource = "Catch"; MessageBox.Show(customerSource); }

                }

                ArrayList a = getNames(customerSource);
                customerDrop(a);
            }
            else if (webAction.Equals("userActivity"))
            {
                //get names
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
               {
                   { "username", username }, //order: {"parameter name", "parameter value"}
                   { webAction, with}
               };


                    try
                    {
                        customerSource = Encoding.UTF8.GetString(client.UploadValues(getUrl(), postData));
                    }
                    catch (Exception) { customerSource = "Catch"; MessageBox.Show(customerSource); }

                }

                ArrayList a = getNames(customerSource);
                loadUserActions(a);
            }
            else if (webAction.Equals("userLogs"))
            {
                //get names
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
               {
                   { "username", username }, //order: {"parameter name", "parameter value"}
                   { webAction, with}
               };


                    try
                    {
                        customerSource = Encoding.UTF8.GetString(client.UploadValues(getUrl(), postData));
                    }
                    catch (Exception) { customerSource = "Catch"; MessageBox.Show(customerSource); }

                }

                ArrayList a = getNames(customerSource);
                loadUserLogs(a);
            }
            else if (webAction.Equals("customerActivity"))
            {
                //get names
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
               {
                   { "username", username }, //order: {"parameter name", "parameter value"}
                   { webAction, with}
               };


                    try
                    {
                        customerSource = Encoding.UTF8.GetString(client.UploadValues(getUrl(), postData));
                    }
                    catch (Exception) { customerSource = "Catch"; MessageBox.Show(customerSource); }

                }

                ArrayList a = getNames(customerSource);
                loadCustomerBougthProducts(a);
            }
            else if (webAction.Equals("allOtherLogs"))
            {
                //get names
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
               {
                   { "username", username }, //order: {"parameter name", "parameter value"}
                   { webAction, with}
               };


                    try
                    {
                        customerSource = Encoding.UTF8.GetString(client.UploadValues(getUrl(), postData)); 
                    }
                    catch (Exception) { customerSource = "Catch"; MessageBox.Show(customerSource); }

                }

                ArrayList a = getNames(customerSource);
                loadAllOtherLogs(a);
            }
        }

        //Load the selected user activity
        private void loadUserActions(ArrayList a)
        {
            comboBox1.Items.Clear();
            comboBox1.BeginUpdate(); // <- Stop painting

            try
            {
                // Adding new items into the cmbMovieListingBox 
                foreach (string item in a)
                {
                    comboBox1.Items.Add(item);
                }
            }
            finally
            {
                comboBox1.EndUpdate(); // <- Finally, repaint if required
            }
        }

        //Load the selected user activity
        private void loadAllOtherLogs(ArrayList a)
        {
            comboBox6.Items.Clear();
            comboBox6.BeginUpdate(); // <- Stop painting

            try
            {
                // Adding new items into the cmbMovieListingBox 
                foreach (string item in a)
                {
                    comboBox6.Items.Add(item);
                }
            }
            finally
            {
                comboBox6.EndUpdate(); // <- Finally, repaint if required
            }
        }

        //Load the selected customer bougth products
        private void loadCustomerBougthProducts(ArrayList a)
        {
            comboBox4.Items.Clear();
            comboBox4.BeginUpdate(); // <- Stop painting

            try
            {
                // Adding new items into the cmbMovieListingBox 
                foreach (string item in a)
                {
                    comboBox4.Items.Add(item);
                }
            }
            finally
            {
                comboBox4.EndUpdate(); // <- Finally, repaint if required
            }
        }

        //Load the selected user log records
        private void loadUserLogs(ArrayList a)
        {
            comboBox5.Items.Clear();
            comboBox5.BeginUpdate(); // <- Stop painting

            try
            {
                // Adding new items into the cmbMovieListingBox 
                foreach (string item in a)
                {
                    comboBox5.Items.Add(item);
                }
            }
            finally
            {
                comboBox5.EndUpdate(); // <- Finally, repaint if required
            }
        }

        //fill user combobox
        private void userDrop(ArrayList a)
        {
            comboBox3.Items.Clear();
            comboBox3.BeginUpdate(); // <- Stop painting

            try
            {
                // Adding new items into the cmbMovieListingBox 
                foreach (string item in a)
                {
                    comboBox3.Items.Add(item);
                }
            }
            finally
            {
                comboBox3.EndUpdate(); // <- Finally, repaint if required
            }
        }

        //fill customer combobox
        private void customerDrop(ArrayList b)
        {
            comboBox2.Items.Clear();
            comboBox2.BeginUpdate(); // <- Stop painting
            try
            {
                // Adding new items into the cmbMovieListingBox 
                foreach (string item in b)
                {
                    comboBox2.Items.Add(item);
                }
            }
            finally
            {
                comboBox2.EndUpdate(); // <- Finally, repaint if required
            }
        }

        //get id array from php result
        private ArrayList getIds(string str)
        {
            ArrayList list = new ArrayList();
            int loop = str.Length;
            Boolean trough = true;
            if (str.Length <= 0)
            {

            }
            else
            {
                while (trough)
                {
                    int index = str.IndexOf("+") + 1;

                    string subStr = str.Substring(0, index - 1);
                    list.Add(subStr);

                    str = str.Substring(index);
                    if (str.Length <= 0)
                    {
                        trough = false;
                    }

                }
            }

            return list;
        }

        //get name array from php result
        private ArrayList getNames(string str)
        {
            ArrayList list = new ArrayList();
            int loop = str.Length;
            Boolean trough = true;
            if (str.Length <= 0)
            {

            }
            else
            {
                while (trough)
                {
                    int index = str.IndexOf("+") + 1;
                    string subStr = str.Substring(0, index - 1);
                    list.Add(subStr);
                    str = str.Substring(index);
                    if (str.Length < 1)
                    {
                        trough = false;
                    }

                }
            }

            return list;
        }
        //get name from name drop down for query
        private string getNameFromDrop(string str)
        {
            string name = str.Replace(" ", "");
            if (name.Length <= 0)
            {

            }
            else
            {
                int startIndex = name.IndexOf("-") + 3;
                int endIndex = name.Length;
                name = name.Substring(startIndex, endIndex - startIndex);
            }
            return name;
        }


        //Users drop down
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = this.comboBox3.GetItemText(this.comboBox3.SelectedItem);
      
            fillDropboxes("userActivity",selected);
            fillDropboxes("userLogs", selected);
        }

        //Customers drop down
        private void comboBox2_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string selected = this.comboBox2.GetItemText(this.comboBox2.SelectedItem);
            string id = getNameFromDrop(selected);
            fillDropboxes("customerActivity", id);
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        //User Activity drop box click
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = this.comboBox1.GetItemText(this.comboBox1.SelectedItem);
            printMessage = printMessage + "\n" + selected;
            richTextBox1.Text = printMessage;
        }

        //Bougth products drop down click
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = this.comboBox4.GetItemText(this.comboBox4.SelectedItem);
            printMessage = printMessage + "\n" + selected;
            richTextBox1.Text = printMessage;
        }

        //User logs drop down click
        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = this.comboBox5.GetItemText(this.comboBox5.SelectedItem);
            printMessage = printMessage + "\n" + selected;
            richTextBox1.Text = printMessage;
        }

        //All other logs drop down click
        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = this.comboBox6.GetItemText(this.comboBox6.SelectedItem);
            printMessage = printMessage + "\n" + selected;
            richTextBox1.Text = printMessage;
        }
    }
}
