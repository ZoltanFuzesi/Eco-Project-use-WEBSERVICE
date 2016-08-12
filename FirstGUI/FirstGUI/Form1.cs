using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FirstGUI
{
    public partial class Form1 : Form
    {
        private Form2 secondForm;// 
        private string username;
        private string password;
        private int stateControll = 0;
        private int addControl = 0;
        private string imagePathToUpload = "";
        private string imageNameToSave = "";
        private string loginTime =  "";
        string logInOutUrl = "";


        public Form1(string usr, string psr, string time,string logInOutUrl)
        {
            loginTime = time;
            username = usr;
            password = psr;
            this.logInOutUrl = logInOutUrl;
            InitializeComponent();
            setAllComponentsUnvisible();
            loadDefaultPicture();
            this.Text = "Eco project using WebService connection";
        }

        //Url for connection
        private string getUrl()
        {
            return "???";//Hide
        }
        //Log in - out url
        private string getLogOutUrl()
        {
            return logInOutUrl;
        }
        //Login time 
        private string getLogintime()
        {
            return loginTime;
        }
        //User name
        private string getUserName()
        {
            return username;
        }
        //Password
        private string getPassword()
        {
            return password;
        }

        private void setUsername(string usr)
        {
            username = usr;
        }
 
        //Close application
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            getUserName();
            if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure to close application", "Exit application", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.No)
                {
                    
                    e.Cancel = true;
                }
                else if (dialogResult == DialogResult.Yes)
                {
                    string logInTime = getLogintime();
                    logOut(logInTime);
                    e.Cancel = false;
                    Application.Exit();
                }
            }
        }

        //Exit button
        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void setAllComponentsUnvisible()
        {
            label18.Visible = false;
            label15.Visible = false;
            label16.Visible = false;
            label14.Visible = false;
            label13.Visible = false;
            label12.Visible = false;
            label11.Visible = false;
            label10.Visible = false;
            label9.Visible = false;
            label8.Visible = false;
            label7.Visible = false;
            label6.Visible = false;
            label5.Visible = false;
            textBox9.Visible = false;
            textBox8.Visible = false;
            textBox7.Visible = false;
            textBox6.Visible = false;
            textBox5.Visible = false;
            textBox4.Visible = false;
            textBox3.Visible = false;
            textBox2.Visible = false;
            textBox1.Visible = false;
            comboBox1.Visible = false;
            comboBox2.Visible = false;
            comboBox3.Visible = false;
            button2.Visible = false;
            button15.Visible = false;
            button16.Visible = false;


        }

        private void searchTextBoxesVisibleAndUnEditable()
        {
            textBox9.Visible = true;
            textBox8.Visible = true;
            textBox7.Visible = true;
            textBox6.Visible = true;
            textBox5.Visible = true;
            textBox4.Visible = true;
            textBox3.Visible = true;
            textBox2.Visible = true;
            textBox1.Visible = true;
            textBox9.ReadOnly = true; 
            textBox8.ReadOnly = true;
            textBox7.ReadOnly = true;
            textBox6.ReadOnly = true;
            textBox5.ReadOnly = true;
            textBox4.ReadOnly = true;
            textBox3.ReadOnly = true;
            textBox2.ReadOnly = true;
            textBox1.ReadOnly = true;
            loadDefaultPicture();
        }

        private void searchTextBoxesVisibleAndEditable()
        {
            textBox9.Visible = true;
            textBox8.Visible = true;
            textBox7.Visible = true;
            textBox6.Visible = true;
            textBox5.Visible = true;
            textBox4.Visible = true;
            textBox3.Visible = true;
            textBox2.Visible = true;
            textBox1.Visible = true;

            textBox9.ReadOnly = true;
            textBox8.ReadOnly = false;
            textBox7.ReadOnly = false;
            textBox6.ReadOnly = false;
            textBox5.ReadOnly = false;
            textBox4.ReadOnly = false;
            textBox3.ReadOnly = false;
            textBox2.ReadOnly = false;
            textBox1.ReadOnly = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Location = new Point(0, 0);
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
        }

        //designe for Search option components visibility
        private void searchVisibility()
        {
            label16.Visible = true;
            label15.Visible = true;
            label14.Visible = true;
            label13.Visible = true;
            label12.Visible = true;
            label11.Visible = true;
            label10.Visible = true;
            label9.Visible = true;
            label8.Visible = true;
            label7.Visible = true;
            label6.Visible = true;
            label5.Visible = true;


            comboBox1.Visible = true;
            comboBox2.Visible = true;
            comboBox3.Visible = true;

            button2.Visible = true;
        }

       //get Next Increment ID
       private void nextID(string str)
        {
            string idSource = "Default";
 
            using (WebClient client2 = new WebClient())
            {
                NameValueCollection postData2 = new NameValueCollection()
               {
                   { "username", username }, 
                   { str , username}
                   
               };

                try
                {
                    idSource = Encoding.UTF8.GetString(client2.UploadValues(getUrl(), postData2));

                }
                catch (Exception) { idSource = "Catch"; MessageBox.Show(idSource); }

            }
            textBox9.Text = idSource;
        }

       //Use to for product
        private ArrayList getProducts()
        {
            ArrayList list = new ArrayList();
         
            string source = "Default";
            using (WebClient client2 = new WebClient())
            {
                NameValueCollection postData2 = new NameValueCollection()
               {
                   { "username", username }, 
                   { "productNames",  ""}
               };

                try
                {
                    source = Encoding.UTF8.GetString(client2.UploadValues(getUrl(), postData2));
                }
                catch (Exception) { source = "Catch"; MessageBox.Show(source); }

            }
            list = getNames(source);
            return list;
        }

        //Build any drop boxes
        private void fillDropboxes(string whatID, string whatName)//fillDropboxes("userNames", "getUsers");
        {
            
            string nameSource = "Default";
            string idSource = "Default";
          

            //get ID's
            using (WebClient client2 = new WebClient())
            {
                NameValueCollection postData2 = new NameValueCollection()
               {
                   { "username", username }, 
                   { whatID,  ""}
               };


                try
                {
                    idSource = Encoding.UTF8.GetString(client2.UploadValues(getUrl(), postData2));
                }
                catch (Exception) { idSource = "Catch"; MessageBox.Show(idSource); }

            }
            ArrayList b = getIds(idSource);
          
            fillIDComboBox(b);
      
            //get names
            using (WebClient client = new WebClient())
               {
                   NameValueCollection postData = new NameValueCollection()
               {
                   { "username", username }, 
                   { whatName, ""}
               };

                   try
                   {
                        nameSource = Encoding.UTF8.GetString(client.UploadValues(getUrl(), postData));
                   }
                   catch (Exception) { nameSource = "Catch"; MessageBox.Show(nameSource); }

               }

            ArrayList a = getNames(nameSource);
       
            fillNameComboBox(a);
   
        }

        //fill name combobox
        private void fillNameComboBox(ArrayList a)
        {
            string[] stringArray = (string[])a.ToArray(typeof(string));
            comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox3.Items.Clear();
            comboBox3.BeginUpdate(); // <- Stop painting
            try
            {
                foreach (string item in stringArray)
                {
                    comboBox3.Items.Add(item);
                }
            }
            finally
            {
                comboBox3.EndUpdate(); // <- Finally, repaint if required
            }
        }

        //fill ID combobox
        private void fillIDComboBox(ArrayList b)
        {
            string[] stringArray = (string[])b.ToArray(typeof(string));
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.Items.Clear();
            comboBox2.BeginUpdate(); // <- Stop painting
            try
            {
                foreach (string item in stringArray)
                {
                    comboBox2.Items.Add(item);
                }
            }
            finally
            {
                comboBox2.EndUpdate(); // <- Finally, repaint if required
            }
        }

        //Clear Product tDropDown
        private void emptyProductDropDown()
        {
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.Items.Clear();
        }

        //fill Produc tDropDown
        private void fillProductDropDown(ArrayList b)
        {
            string[] stringArray = (string[])b.ToArray(typeof(string));
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.Items.Clear();
            comboBox1.BeginUpdate(); // <- Stop painting
            try
            {
                foreach (string item in stringArray)
                {
                    comboBox1.Items.Add(item);
                }
            }
            finally
            {
                comboBox1.EndUpdate(); // <- Finally, repaint if required
            }
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

        private string ReverseString(string s)
        {
            char[] arr = s.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }

        //get name from name drop down for query
        private string getNameFromDrop(string str)
        {     
            string name = str.Replace(" ", "");
                if (name.Length <= 0)
                {

                }
                else
                {       int startIndex = name.IndexOf("-")+3;
                        int endIndex = name.Length;
                        name = name.Substring(startIndex, endIndex - startIndex);                           
                }
            return name;
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

        //Clear all text boxes
        private void clearTextDetails()
        {
            textBox9.Text = "";
            textBox8.Text = "";
            textBox7.Text = "";
            textBox6.Text = "";
            textBox5.Text = "";
            textBox4.Text = "";
            textBox3.Text = "";
            textBox2.Text = "";
            textBox1.Text = "";
        }

        private void fillUserDetails(string[] al)
        {
            textBox9.Text = al[0];
            textBox8.Text = al[1];
        }
        //Fill text from droopdown
        private void fillTextDetails(string[] al)
        {
             if (getStatecontroll() == 3 && getaddControl() == 2)
             {
                 textBox7.Text = al[1];
             }
             else
             {
                textBox9.Text = al[0];
                textBox8.Text = al[1];
                textBox7.Text = al[2];
                textBox6.Text = al[3];
                textBox5.Text = al[4];
                textBox4.Text = al[5];
                textBox3.Text = al[6];
                textBox2.Text = al[7];
                textBox1.Text = al[8];
            }
            if(getStatecontroll()==3 && (getaddControl() == 0 || getaddControl() == 1))
            {
                setImagePath(al[8]);
                getPictureFromServer(al[8]);
            }
        }

        //Statecontroll 
        private void setStatecontroll(int i, int n)
        {       
            stateControll = i;
            addControl = n;
        }
        //Statecontroll 
        private int getStatecontroll()
        {
            return stateControll;
        }

        //Statecontroll 
        private int getaddControl()
        {
            return addControl;
        }

        //Search supplier button
        private void button12_Click(object sender, EventArgs e)
        {
            loadDefaultPicture();
            emptyProductDropDown();
            setStatecontroll(1,0);
            clearTextDetails();
            fillDropboxes("supplierID", "supplierNames");
            setAllComponentsUnvisible();
            label16.Text = "Search by Id,Name";
            label15.Text = "Search Supplier";
            label14.Text = "SupplierID";
            label13.Text = "Supplier Name";
            label12.Text = "Street";
            label11.Text = "Town";
            label10.Text = "County";
            label9.Text = "Country";
            label8.Text = "Post Code";
            label7.Text = "Email";
            label6.Text = "Contact";
            label5.Text = "Products";

            searchTextBoxesVisibleAndUnEditable();

            searchVisibility();

            button2.Text = "Change Supplier Details";


        }

        //Add Supplier button
        private void button13_Click(object sender, EventArgs e)
        {
            emptyProductDropDown();
            loadDefaultPicture();
            setAllComponentsUnvisible();
            clearTextDetails();
            setStatecontroll(1,1);
            label16.Text = "Search by Id,Name";
            label15.Text = "Add Supplier";
            label14.Text = "SupplierID";
            label13.Text = "Supplier Name";
            label12.Text = "Street";
            label11.Text = "Town";
            label10.Text = "County";
            label9.Text = "Country";
            label8.Text = "Post Code";
            label7.Text = "Email";
            label6.Text = "Contact";

            searchTextBoxesVisibleAndEditable();

            searchVisibility();
            label16.Visible = false;
            label5.Visible = false;
            comboBox1.Visible = false;
            comboBox2.Visible = false;
            comboBox3.Visible = false;

            button2.Text = "Add Supplier";
            nextID("nextSupplierID");
        }

        //Delete Supplier button
        private void button10_Click(object sender, EventArgs e)
        {
            emptyProductDropDown();
            loadDefaultPicture();
            setStatecontroll(1,1);
            clearTextDetails();
            fillDropboxes("supplierID", "supplierNames");
            setAllComponentsUnvisible();
            label16.Text = "Search by Id,Name";
            label15.Text = "Delete Supplier";
            label14.Text = "SupplierID";
            label13.Text = "Supplier Name";
            label12.Text = "Street";
            label11.Text = "Town";
            label10.Text = "County";
            label9.Text = "Country";
            label8.Text = "Post Code";
            label7.Text = "Email";
            label6.Text = "Contact";
           // label5.Text = "Products";

            searchTextBoxesVisibleAndUnEditable();

            searchVisibility();
            label5.Visible = false;
            comboBox1.Visible = false;

            button2.Text = "Delete Supplier";
        }

        //History button
        private void button11_Click(object sender, EventArgs e)
        {
            secondForm = new Form2(getUserName(), getPassword(), getUrl());
            secondForm.Show();
            
        }


      

        //Search Customer button
        private void button6_Click(object sender, EventArgs e)
        {
            emptyProductDropDown();
            loadDefaultPicture();
            setStatecontroll(2,0);
            clearTextDetails();
            fillDropboxes("customerID", "customerNames");
            setAllComponentsUnvisible();
            label16.Text = "Search by Id,Name";
            label15.Text = "Search Customer";
            label14.Text = "CustomerID";
            label13.Text = "Customer Name";
            label12.Text = "Street";
            label11.Text = "Town";
            label10.Text = "County";
            label9.Text = "Country";
            label8.Text = "Post Code";
            label7.Text = "Email";
            label6.Text = "Contact";
            label5.Text = "Bougth Products";

            searchTextBoxesVisibleAndUnEditable();

            searchVisibility();

            button2.Text = "Change Customer Details";
        }

        //Add Customer button
        private void button14_Click(object sender, EventArgs e)
        {
            emptyProductDropDown();
            loadDefaultPicture();
            setStatecontroll(2,1);
            clearTextDetails();
            setAllComponentsUnvisible();
            label16.Text = "Search by Id,Name";
            label15.Text = "Add Customer";
            label14.Text = "CustomerID";
            label13.Text = "Customer Name";
            label12.Text = "Street";
            label11.Text = "Town";
            label10.Text = "County";
            label9.Text = "Country";
            label8.Text = "Post Code";
            label7.Text = "Email";
            label6.Text = "Contact";
            label5.Text = "Bougth Products";

            searchTextBoxesVisibleAndEditable();

            searchVisibility();
            label16.Visible = false;
            label5.Visible = false;
            comboBox1.Visible = false;
            comboBox2.Visible = false;
            comboBox3.Visible = false;

            button2.Text = "Add customer";
            nextID("nextCustomerID");
        }

        //Delete customer button
        private void button5_Click(object sender, EventArgs e)
        {
            emptyProductDropDown();
            loadDefaultPicture();
            setStatecontroll(2,1);
            clearTextDetails();
            fillDropboxes("customerID", "customerNames");
            setAllComponentsUnvisible();
            label16.Text = "Search by Id,Name";
            label15.Text = "Delete Customer";
            label14.Text = "CustomerID";
            label13.Text = "Customer Name";
            label12.Text = "Street";
            label11.Text = "Town";
            label10.Text = "County";
            label9.Text = "Country";
            label8.Text = "Post Code";
            label7.Text = "Email";
            label6.Text = "Contact";
            label5.Text = "Bougth Products";

            searchTextBoxesVisibleAndUnEditable();

            searchVisibility();
            label5.Visible = false;
            comboBox1.Visible = false;

            button2.Text = "Delete customer";
        }
        //Search product button
        private void button8_Click(object sender, EventArgs e)
        {
            emptyProductDropDown();
            loadDefaultPicture();
            setStatecontroll(3,0);
            clearTextDetails();
            fillDropboxes("productID", "productNames");
            setAllComponentsUnvisible();
            label16.Text = "Search by Id,Name";
            label15.Text = "Search Product";
            label14.Text = "ProductID";
            label13.Text = "Product Name";
            label12.Text = "Supplier";
            label11.Text = "Type";
            label10.Text = "Size";
            label9.Text = "Price";
            label8.Text = "Colour";
            label7.Text = "Stock";

            searchTextBoxesVisibleAndUnEditable();

            searchVisibility();
            textBox1.Visible = false;
            label6.Visible = false;
            label5.Visible = false;
            comboBox1.Visible = false;
            button2.Text = "Change Product Details";
        }

        //Add Product button
        private void button4_Click(object sender, EventArgs e)
        {
            emptyProductDropDown();
            loadDefaultPicture();
            setStatecontroll(3,2);
            clearTextDetails();
            fillDropboxes("supplierID", "supplierNames");
            setAllComponentsUnvisible();
            label16.Text = "Add Supplier by Id,Name";
            label15.Text = "Add Product";
            label14.Text = "ProductID";
            label13.Text = "Product Name";
            label12.Text = "Supplier";
            label11.Text = "Type";
            label10.Text = "Size";
            label9.Text = "Price";
            label8.Text = "Colour";
            label7.Text = "Stock";

            searchTextBoxesVisibleAndEditable();

            searchVisibility();
            label18.Text = "Select image";
            //label18.Visible = true;
            label16.Visible = true;
            textBox1.Visible = false;
            label6.Visible = false;
            label5.Visible = false;
            comboBox1.Visible = false;
            comboBox2.Visible = true;
            comboBox3.Visible = true;
            button15.Visible = true;
            button2.Text = "Add Product";
            textBox7.ReadOnly = true;
            textBox8.Select(0, 0);
            nextID("nextProductID");
            setImagePath("");

        }

        //Delete Product button
        private void button3_Click(object sender, EventArgs e)
        {
            emptyProductDropDown();
            loadDefaultPicture();
            setStatecontroll(3,1);
            clearTextDetails();
            fillDropboxes("productID", "productNames");
            setAllComponentsUnvisible();
            label16.Text = "Search by Id,Name";
            label15.Text = "Delete Product";
            label14.Text = "ProductID";
            label13.Text = "Product Name";
            label12.Text = "Supplier";
            label11.Text = "Type";
            label10.Text = "Size";
            label9.Text = "Price";
            label8.Text = "Colour";
            label7.Text = "Stock";

            searchTextBoxesVisibleAndUnEditable();

            searchVisibility();
            textBox1.Visible = false;
            label6.Visible = false;
            label5.Visible = false;
            comboBox1.Visible = false;
            button2.Text = "Delete Product";
        }



        //Action button change details - save changes
        private void button2_Click(object sender, EventArgs e)
        {
            //string[] arr = getTextDetails();
            string buttonValue = (sender as Button).Text;
            if (getStatecontroll() == 1 && getaddControl() == 0)//Supplier
            {
                if (String.IsNullOrEmpty(textBox9.Text))
                    errorMessageBox("Please select a supplier!");
                else
                    changeVisibility();

            }
            else if (getStatecontroll() == 2 && getaddControl() == 0)//Customer
            {
                if (String.IsNullOrEmpty(textBox9.Text))
                    errorMessageBox("Please select a customer!");
                else
                    changeVisibility();
                fillProductDropDown(getProducts());
                                                   

            }
            else if (getStatecontroll() == 3 && getaddControl() == 0)//Product
            {
                if (String.IsNullOrEmpty(textBox9.Text))
                    errorMessageBox("Please select a product!");
                else
                    changeVisibility();

            }
            else if (getStatecontroll() == 1 && getaddControl() == 1)//Supplier Actions 
            {
                //Do something
                if (buttonValue.Equals("Add Supplier"))
                {
                    //Add supplier
                    if (checkFields(getTextDetails()))
                    {
                        if (IsValidEmail(this.textBox2.Text) && ckeckNumber(this.textBox1.Text, "Contact"))
                        {
                            fillFields("addSupplier", textBox9.Text, "SupplierAdd");

                        }
                    }
                }
                else if (buttonValue.Equals("Save Changes"))//Update supplier
                {
                    //Save changes for supplier
                    if (checkFields(getTextDetails()))
                    {
                        if (IsValidEmail(this.textBox2.Text) && ckeckNumber(this.textBox1.Text, "Contact"))
                        {
                            fillFields("changeSupplierDetails", textBox9.Text, "SupplierChange");
                        }
                    }
                }
                else if (buttonValue.Equals("Delete Supplier"))
                {
                    if (String.IsNullOrEmpty(textBox9.Text))
                    {
                        errorMessageBox("Please select a supplier!");
                    }
                    else
                    {
                        //delete supplier
                        DialogResult dialogResult = MessageBox.Show("Are you sure to delete Supplier " + textBox8.Text, "Delete Supplier", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.No)
                        {
                            //do nothing
                        }
                        else if (dialogResult == DialogResult.Yes)
                        {
                            fillFields("deleteSupplier", textBox9.Text, "SupplierDelete");
                        }
                    }
                }
            }
            else if (getStatecontroll() == 2 && getaddControl() == 1)//Customer Actions 
            {
                //Do something
                if (buttonValue.Equals("Add customer"))
                {
                    if (checkFields(getTextDetails()))
                    {
                        if (IsValidEmail(this.textBox2.Text) && ckeckNumber(this.textBox1.Text, "Contact"))
                        {
                            //add customer
                            fillFields("addCustomer", textBox9.Text, "CustomerAdd");
                        }
                    }
                }
                else if (buttonValue.Equals("Save Changes"))
                {
                    //Save chages for customer
                    if (checkFields(getTextDetails()))
                    {
                        if (IsValidEmail(this.textBox2.Text) && ckeckNumber(this.textBox1.Text, "Contact"))
                        {
                            //Save changes action for customer
                            fillFields("changeCustomerDetails", textBox9.Text, "CustomerChange");
                        }
                    }
                }
                else if (buttonValue.Equals("Delete customer"))
                {
                    if (String.IsNullOrEmpty(textBox9.Text))
                    {
                        errorMessageBox("Please select a customer!");
                    }
                    else
                    {
                        //delete customer
                        DialogResult dialogResult = MessageBox.Show("Are you sure to delete Customer " + textBox8.Text, "Delete Customer", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.No)
                        {
                            //do nothing
                        }
                        else if (dialogResult == DialogResult.Yes)
                        {

                            fillFields("deleteCustomer", textBox9.Text, "CustomerDelete");
                        }
                    }
                }
            }
            else if (getStatecontroll() == 3 && getaddControl() == 2)//Only add product
            {

                if (buttonValue.Equals("Add Product"))
                {
                    if (checkFields(getTextDetails()))
                    {
                        if (!String.IsNullOrEmpty(getImagePath()) && ckeckNumber(this.textBox5.Text, "Size") && ckeckNumber(this.textBox4.Text, "Price") && ckeckNumber(this.textBox2.Text, "Stock"))
                        {
                            //Set the image name to upload
                            setImageName(this.textBox8.Text + ".jpg");
                            string imgLocation = "picture_library/" + getImageName();
                            imgLocation = imgLocation.Replace(" ", String.Empty);
                            imgLocation = Regex.Replace(imgLocation, @"[\s+]", "");
                            setImageName(imgLocation);
                            //add product
                            fillFields("addProduct", textBox9.Text, "ProductAdd");
                            loadDefaultPicture();
                        }
                        else
                        {
                            if (String.IsNullOrEmpty(getImagePath()))
                                errorMessageBox("Please select image");
                        }
                    }
                }
            }
            else if (getStatecontroll() == 3 && getaddControl() == 1)//Product Actions
            {

                if (buttonValue.Equals("Save Changes"))//Update product
                {
                    //Save changes for product
                    if (checkFields(getTextDetails()))
                    {
                        if (ckeckNumber(this.textBox5.Text, "Size") && ckeckNumber(this.textBox4.Text, "Price") && ckeckNumber(this.textBox2.Text, "Stock"))
                        {
                            //Save changes action for product
                            fillFields("changeProductDetails", textBox9.Text, "ProductChange");
                            loadDefaultPicture();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Save changes product sometghing went wrong");// CHANGE LATER
                    }
                }
                else if (buttonValue.Equals("Delete Product"))//Delete product
                {
                    if (String.IsNullOrEmpty(textBox9.Text))
                    {
                        errorMessageBox("Please select a product!");
                    }
                    else
                    {
                        //delete product
                        DialogResult dialogResult = MessageBox.Show("Are you sure to delete Product " + textBox8.Text, "Delete Product", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.No)
                        {
                            //do nothing
                        }
                        else if (dialogResult == DialogResult.Yes)
                        {
                            fillFields("deleteProduct", textBox9.Text, "ProductDelete");
                        }
                    }
                }
            }
            else if (getStatecontroll() == 4 && getaddControl() == 0)//Admin Actions
            {
                string[] arr = getTextDetails();
                string userName = getUserName();
                string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string response = "";
                //arr 0-1-2-3 used for name create password - repeat password - email

                if (buttonValue.Equals("Create User"))
                {
                    if (!String.IsNullOrEmpty(arr[0]) && !String.IsNullOrEmpty(arr[1]) && !String.IsNullOrEmpty(arr[2]) && !String.IsNullOrEmpty(arr[3]) && IsValidEmail(arr[3]))
                    {
                        if (arr[1].Equals(arr[2]))
                        {
                            using (WebClient client2 = new WebClient())
                            {
                                NameValueCollection postData2 = new NameValueCollection()
                            {
                               { "username", userName }, //order: {"parameter name", "parameter value"}
                               { "CreateUser", arr[0] },//change supplier , supplier id
                               { "password", arr[1] },
                               { "email", arr[3] },
                               { "timeStamp", time}
                            };
                                try
                                {
                                    response = Encoding.UTF8.GetString(client2.UploadValues(getUrl(), postData2));
                                }
                                catch (Exception) { response = "Catch"; MessageBox.Show(response); }
                                if (response.Equals("SUCCESS"))
                                {
                                    setAllComponentsUnvisible();
                                    clearTextDetails();
                                    label15.Text = "New user created ";
                                    label15.Visible = true;
                                }
                                else if (response.Equals("DENIED"))
                                {
                                    errorMessageBox("User " + arr[0] + " already in the system");
                                }
                                else
                                {
                                    errorMessageBox("Can't create new user!");
                                }
                            }//using finish
                        }//check passwords finish
                        else
                        {
                            errorMessageBox("The passwords are not same");
                        }
                    }
                    else
                    {
                        if (String.IsNullOrEmpty(arr[0]))
                            errorMessageBox("Empty user name");
                        if (String.IsNullOrEmpty(arr[1]))
                            errorMessageBox("Empty Password");
                        if (String.IsNullOrEmpty(arr[2]))
                            errorMessageBox("Empty Repeat password");
                        if (String.IsNullOrEmpty(arr[3]))
                            errorMessageBox("Empty Email");
                    }
                }
                else if (buttonValue.Equals("Delete User"))//delete user
                {
                    string deleteUser = arr[0];
                    if (!String.IsNullOrEmpty(arr[0]))
                    { 
                        //if user has privilages fill drop boxes
                        using (WebClient client2 = new WebClient())
                        {
                            NameValueCollection postData2 = new NameValueCollection()
                            {
                               { "username", userName }, //order: {"parameter name", "parameter value"}
                               { "DeleteUser", arr[0] },//change supplier , supplier id
                               { "timeStamp", time}
                            };
                            try
                            {
                                response = Encoding.UTF8.GetString(client2.UploadValues(getUrl(), postData2));
                            }
                            catch (Exception) { response = "Catch"; MessageBox.Show(response); }
                            if (response.Equals("SUCCESS"))
                            {
                                setAllComponentsUnvisible();
                                clearTextDetails();
                                label15.Text = "The user was deleted! ";
                                label15.Visible = true;
                            }
                            else if (response.Equals("DENIED"))
                            {
                                errorMessageBox("You have No Privileges!");
                            }
                            else
                            {
                                errorMessageBox("Can't delete user!");
                            }
                        }
                      }
                    else
                    {
                        errorMessageBox("Please select a user");
                    }
                    }
                    else if (buttonValue.Equals("Change"))//user password
                    {
                    if (!String.IsNullOrEmpty(arr[0]) && !String.IsNullOrEmpty(arr[1]) && !String.IsNullOrEmpty(arr[2]))
                    {
                        if (textBox8.Text.Equals(textBox7.Text))
                        {
                            using (WebClient client2 = new WebClient())
                            {
                                NameValueCollection postData2 = new NameValueCollection()
                                {
                                   { "username", userName }, //order: {"parameter name", "parameter value"}$usr = 
                                   { "ChangeP", arr[0]},
                                   { "pass", arr[1]},
                                   { "timeStamp", time}
                                };
                                try
                                {
                                    response = Encoding.UTF8.GetString(client2.UploadValues(getUrl(), postData2));
                                }
                                catch (Exception) { response = "Catch"; MessageBox.Show(response); }
                                if (response.Equals("SUCCESS"))
                                {
                                    setAllComponentsUnvisible();
                                    clearTextDetails();
                                    label15.Text = "The password has updated! ";
                                    label15.Visible = true;
                                }
                                else if (response.Equals("DENIED"))
                                {
                                    errorMessageBox("You have No Privileges!");
                                }
                                else
                                {
                                    errorMessageBox("Can't update user password!");
                                }
                            }
                        }
                        else
                        {
                            errorMessageBox("The passwords are not same!");
                        }
                    }//if checking finish
                    else
                    {
                        if (String.IsNullOrEmpty(textBox9.Text))
                            errorMessageBox("User name is empty");
                        if (String.IsNullOrEmpty(textBox8.Text))
                            errorMessageBox("New password is empty");
                        if (String.IsNullOrEmpty(textBox7.Text))
                            errorMessageBox("Repeat password is empty");
                    }
                    }//else if finish

                }
           

        }

        //designe for change option components visibility
        private void changeVisibility()
        {
            button2.Text = "Save Changes";
            textBox9.ReadOnly = true;
            textBox8.ReadOnly = true;
            textBox7.ReadOnly = false;
            textBox6.ReadOnly = false;
            textBox5.ReadOnly = false;
            textBox4.ReadOnly = false;
            textBox3.ReadOnly = false;
            textBox2.ReadOnly = false;
            textBox1.ReadOnly = false;


            label15.Visible = true;
            label14.Visible = true;
            label13.Visible = true;
            label12.Visible = true;
            label11.Visible = true;
            label10.Visible = true;
            label9.Visible = true;
            label8.Visible = true;
            label7.Visible = true;
            label6.Visible = true;
            label5.Visible = false;
            comboBox1.Visible = false;

            //show products drop down
            
            //hide serch label and drop boxes
            label16.Visible = false;
            comboBox2.Visible = false;
            comboBox3.Visible = false;

            button2.Visible = true;
            if(getStatecontroll()==1)//get ready for save supplier changes 
            {
                setStatecontroll(1,1);
            }
            if (getStatecontroll() == 2)//get ready for save customer changes 
            {
                setStatecontroll(2, 1);
                comboBox1.Visible = true;
                label5.Text = "Buying Product";
                label5.Visible = true;
            }
            if (getStatecontroll() == 3)//get ready for save product changes 
            {
                setStatecontroll(3, 1);
                textBox7.ReadOnly = true;
                textBox6.ReadOnly = true;
                label7.Text = "Stock";
                label6.Visible = false;
            }
        }



        //Delete User button
        private void button7_Click(object sender, EventArgs e)
        {
            loadDefaultPicture();
            emptyProductDropDown();
            setStatecontroll(4, 0);
            clearTextDetails();
            setAllComponentsUnvisible();
            fillDropboxes("userNames", "getUserIDs");
            label16.Text = "Search by Id,Name";
            label15.Text = "Delete User";
            label14.Text = "User Name";
            label13.Text = "Password";

            label16.Visible = true; label16.Text = "Search by Id,Name";
            label15.Visible = true;
            label14.Visible = true;
            label13.Visible = true;

            textBox9.ReadOnly = true;
            textBox8.ReadOnly = true;
            textBox9.Visible = true;
            textBox8.Visible = true;

            comboBox2.Visible = true;
            comboBox3.Visible = true;

            button2.Text = "Delete User";
            button2.Visible = true;
        }

        private void label14_Click(object sender, EventArgs e)
        {
            setAllComponentsUnvisible();
        }

        //Add user button
        private void button9_Click(object sender, EventArgs e)
        {
            loadDefaultPicture();
            emptyProductDropDown();
            setStatecontroll(4, 0);
            clearTextDetails();
            setAllComponentsUnvisible();
            label15.Text = "Add User";
            label14.Text = "User Name";
            label13.Text = "Create Password";
            label12.Text = "Repeat Password";
            label11.Text = "Email";

            label15.Visible = true;
            label14.Visible = true;
            label13.Visible = true;
            label12.Visible = true;
            label11.Visible = true;

            textBox9.ReadOnly = false;
            textBox8.ReadOnly = false;
            textBox7.ReadOnly = false;
            textBox6.ReadOnly = false;
            textBox9.Visible = true;
            textBox8.Visible = true;
            textBox7.Visible = true;
            textBox6.Visible = true;

            button2.Text = "Create User";
            button2.Visible = true;
            button16.Visible = true;
        }


        private void label18_Click_1(object sender, EventArgs e)
        {

        }

        //File chooser to upload picture to server
        private void button15_Click(object sender, EventArgs e)
        {
            string path = "";
            OpenFileDialog file = new OpenFileDialog();
            if (file.ShowDialog() == DialogResult.OK)
            {
                 path = file.FileName;
            }

            //copy file to server
            pictureBox1.ImageLocation = path;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            label18.Text = path;
            label18.Visible = true;
            setImagePath(path);
        }

        private void loadDefaultPicture()
        {
            pictureBox1.Image = Properties.Resources.Default;
        }


        //Change user password
        private void button16_Click(object sender, EventArgs e)
        {
            loadDefaultPicture();
            emptyProductDropDown();
            setStatecontroll(4, 0);
            clearTextDetails();
            setAllComponentsUnvisible();
            label15.Text = "Change Password";
            label14.Text = "User Name";
            label13.Text = "New Password";
            label12.Text = "Repeat Password";

            label15.Visible = true;
            label14.Visible = true;
            label13.Visible = true;
            label12.Visible = true;

            textBox9.ReadOnly = false;
            textBox9.ReadOnly = false;
            textBox9.ReadOnly = false;
            textBox9.Visible = true;
            textBox8.Visible = true;
            textBox7.Visible = true;

            button2.Text = "Change";
            button2.Visible = true;
          
        }

     

        //ID DropBox 
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string action = "";
            if (getStatecontroll() == 1)//supplier
            {
                action = "getSupplierDetails";
            }
            else if (getStatecontroll() == 2 )//customer
            {
                action = "getCustomerDetails";
                emptyProductDropDown();
            }
            else if (getStatecontroll() == 3 && getaddControl() == 0)//product
            {
                action = "getProductDetails";
            }
            else if (getStatecontroll() == 3 && getaddControl() == 1)//product delete
            {
                action = "getProductDetails";
            }
            else if (getStatecontroll() == 3 && getaddControl() == 2)//product add
            {
                action = "getSupplierDetails";
            }
            else if (getStatecontroll() == 4)//product add
            {
                action = "userDetails";
            }
            fillFields(action, comboBox2.Text, "Getdetails");
        }

        //Name Dropbox
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = "";
            string action = "";
            if (getStatecontroll() == 1)//supplier
            {
                action = "getSupplierDetails";
            }
            else if(getStatecontroll() == 2)//customer
            {
                action = "getCustomerDetails";
            }
            else if(getStatecontroll() == 3 && getaddControl() == 0)//product
            {
                action = "getProductDetails";
            }
            else if (getStatecontroll() == 3 && getaddControl() == 1)//product delete
            {
                action = "getProductDetails";
            }
            else if (getStatecontroll() == 3 && getaddControl() == 2)//product add
            {
                action = "getSupplierDetails";
            }
            else if (getStatecontroll() == 4)//product add
            {
                action = "userDetails";
            }
            if (getStatecontroll() < 4)
            {
                id = getNameFromDrop(comboBox3.Text);
            }
            else
            {
                id = getID("getUserId",comboBox3.Text);
            }
            fillFields(action, id, "Getdetails");
        }

        //Product DropBox
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //load picture when clicked
            if(getStatecontroll()==1 || getStatecontroll() == 2)
            {
                   
                    string name = comboBox1.Text;
                    string id = getNameFromDrop(name);
                Console.WriteLine("Id for image " + id);
                    string imgLoc = "";
                    using (WebClient client2 = new WebClient())
                {
                    NameValueCollection postData2 = new NameValueCollection()
               {
                   { "username", username }, //order: {"parameter name", "parameter value"}
                   { "getProductsImageLocation", id },


               };


                    try
                    {
                        imgLoc = Encoding.UTF8.GetString(client2.UploadValues(getUrl(), postData2));
                    }
                    catch (Exception) { imgLoc = "Catch"; MessageBox.Show(imgLoc); }

                }
                getPictureFromServer(imgLoc);
            }
        }

        //get id from name 
        private string getID(string whatAction, string whatName)
        {
            
            string idSource = "Default";
            
            //get ID's
            using (WebClient client2 = new WebClient())
            {
                NameValueCollection postData2 = new NameValueCollection()
               {
                   { "username", username }, //order: {"parameter name", "parameter value"}
                   { whatAction, whatName },


               };


                try
                {
                    idSource = Encoding.UTF8.GetString(client2.UploadValues(getUrl(), postData2));
                }
                catch (Exception) { idSource = "Catch"; MessageBox.Show(idSource); }

            }
            return idSource;
        }

        //log out process
        private void logOut(string time)
        {
            string userName = getUserName();
            string logOut = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string res = "Problem to log out";
       
            using (WebClient client2 = new WebClient())
            {
                NameValueCollection postData2 = new NameValueCollection()
               {
                   { "username", userName }, //order: {"parameter name", "parameter value"}
                   { "logIn", time },
                   { "logOut", logOut }


               };


                try
                {
                    res = Encoding.UTF8.GetString(client2.UploadValues(getLogOutUrl(), postData2));
                }
                catch (Exception) { res = "Catch"; MessageBox.Show(res); }
            }
            
        }

        //Build any drop boxes and make querys
        private void fillFields(string whatAction, string whatID,string methodAction)
        {
            getUserName();
            string[] arr = getTextDetails();
            string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string bougthProducts = "Default";
            string nameSource = "Default";
            string products = "Default";
            
          
            if (methodAction.Equals("SupplierChange"))//Change supplier details
            {
                using (WebClient client2 = new WebClient())
                {
                    NameValueCollection postData2 = new NameValueCollection()
                    {
                       { "username", username }, //order: {"parameter name", "parameter value"}
                       { whatAction, whatID },//change supplier , supplier id
                       { "name", arr[1] },
                       { "street", arr[2] },
                       { "town", arr[3] },
                       { "county", arr[4] },
                       { "country", arr[5] },
                       { "postCode", arr[6] },
                       { "email", arr[7] },
                       { "contact", arr[8] },
                       { "timeStamp", time}


                    };
                    try
                    {
                        nameSource = Encoding.UTF8.GetString(client2.UploadValues(getUrl(), postData2));
                    }
                    catch (Exception) { nameSource = "Catch"; MessageBox.Show(nameSource); }
                    if(nameSource.Equals("SUCCESS"))
                    {
                        setAllComponentsUnvisible();
                        clearTextDetails();
                        label15.Text = "Supplier details updated ";
                        label15.Visible = true;
                        loadDefaultPicture();
                    }
                    else
                    {
                        errorMessageBox("Can't update supplier details");
                    }
                }
            }
            else if (methodAction.Equals("CustomerChange"))//Change Customer Details
            {
                using (WebClient client2 = new WebClient())
                {
                    string product = comboBox1.Text;
                    NameValueCollection postData2 = new NameValueCollection()
                    {
                       { "username", username }, //order: {"parameter name", "parameter value"}
                       { whatAction, whatID },//change customer , customer id
                       { "name", arr[1] },
                       { "street", arr[2] },
                       { "town", arr[3] },
                       { "county", arr[4] },
                       { "country", arr[5] },
                       { "postCode", arr[6] },
                       { "email", arr[7] },
                       { "contact", arr[8] },
                       { "timeStamp", time},
                       { "buying" , product }


                    };
                    try
                    {
                        nameSource = Encoding.UTF8.GetString(client2.UploadValues(getUrl(), postData2));
                    }
                    catch (Exception) { nameSource = "Catch"; MessageBox.Show(nameSource); }
                    if (nameSource.Equals("SUCCESS"))
                    {
                        setAllComponentsUnvisible();
                        clearTextDetails();
                        label15.Text = "Customer details updated ";
                        label15.Visible = true;
                        loadDefaultPicture();
                    }
                    else
                    {
                        errorMessageBox("Can't update customer details");
                    }
                }
            }
            else if (methodAction.Equals("ProductChange"))
            {
                using (WebClient client2 = new WebClient())
                {

                    NameValueCollection postData2 = new NameValueCollection()
                    {
                       { "username", username },
                       { whatAction, whatID },//change product , product id
                       { "desc", arr[2] },
                       { "type", arr[3] },
                       { "size", arr[4] },
                       { "price", arr[5] },
                       { "colour", arr[6] },
                       { "stock", arr[7] },
                       { "timeStamp", time}
                        

                };
                    try
                    {
                        nameSource = Encoding.UTF8.GetString(client2.UploadValues(getUrl(), postData2));
                        
                    }
                    catch (Exception) { nameSource = "Catch"; MessageBox.Show(nameSource); }
                    if (nameSource.Equals("SUCCESS"))
                    {
                        setAllComponentsUnvisible();
                        clearTextDetails();
                        label15.Text = "Product details updated ";
                        label15.Visible = true;
                    }
                    else
                    {
                        errorMessageBox("Can't update product details");
                    }
                }
            }
            else if (methodAction.Equals("ProductAdd"))
            {
                using (WebClient client2 = new WebClient())
                {
                    NameValueCollection postData2 = new NameValueCollection()
                    {
                       { "username", username }, //order: {"parameter name", "parameter value"}
                       { whatAction, whatID },//addProduct , product id
                       { "id", arr[0] },
                       { "name", arr[1] },
                       { "supplierName", arr[2] },
                       { "type", arr[3] },
                       { "size", arr[4] },
                       { "price", arr[5] },
                       { "colour", arr[6] },
                       { "stock", arr[7] },
                       { "target_file" , getImageName()},
                       { "timeStamp", time }
                    };
                    try
                    {
                        nameSource = Encoding.UTF8.GetString(client2.UploadValues(getUrl(), postData2));
                    }
                    catch (Exception) { nameSource = "Catch"; MessageBox.Show(nameSource); }
                    if (nameSource.Equals("SUCCESS"))
                    {
                        UpLoadFile(getImagePath());
                        setAllComponentsUnvisible();
                        clearTextDetails();
                        label15.Text = "Product added to database ";
                        label15.Visible = true;
                        setImagePath("");
                        setImageName("");
                    }
                    else
                    {
                        errorMessageBox("Can't update product details");
                    }
                }
            }
            else if (methodAction.Equals("CustomerAdd"))//Add customer
            {
                using (WebClient client2 = new WebClient())
                {
                    NameValueCollection postData2 = new NameValueCollection()
                    {
                       { "username", username }, //order: {"parameter name", "parameter value"}
                       { whatAction, whatID },//addCustomer , customer id
                       { "id", arr[0] },
                       { "name", arr[1] },
                       { "street", arr[2] },
                       { "town", arr[3] },
                       { "county", arr[4] },
                       { "country", arr[5] },
                       { "postCode", arr[6] },
                       { "email", arr[7] },
                       { "contact" , arr[8]},
                       { "timeStamp", time }
                    };
                    try
                    {
                        nameSource = Encoding.UTF8.GetString(client2.UploadValues(getUrl(), postData2));
                    }
                    catch (Exception) { nameSource = "Catch"; MessageBox.Show(nameSource); }
                    if (nameSource.Equals("SUCCESS"))
                    {
                        setAllComponentsUnvisible();
                        clearTextDetails();
                        label15.Text = "Customer added to database ";
                        label15.Visible = true;
                    }
                    else
                    {
                        errorMessageBox("Can't update customer details");
                    }
                }
            }
            else if (methodAction.Equals("SupplierAdd"))//Add supplier
            {
                using (WebClient client2 = new WebClient())
                {
                    NameValueCollection postData2 = new NameValueCollection()
                    {
                       { "username", username }, //order: {"parameter name", "parameter value"}
                       { whatAction, whatID },//addSupplier , supplier id
                       { "id", arr[0] },
                       { "name", arr[1] },
                       { "street", arr[2] },
                       { "town", arr[3] },
                       { "county", arr[4] },
                       { "country", arr[5] },
                       { "postCode", arr[6] },
                       { "email", arr[7] },
                       { "contact" , arr[8]},
                       { "timeStamp", time }
                    };
                    try
                    {
                        nameSource = Encoding.UTF8.GetString(client2.UploadValues(getUrl(), postData2));
                    }
                    catch (Exception) { nameSource = "Catch"; MessageBox.Show(nameSource); }
                    if (nameSource.Equals("SUCCESS"))
                    {
                        setAllComponentsUnvisible();
                        clearTextDetails();
                        label15.Text = "Supplier added to database ";
                        label15.Visible = true;
                        loadDefaultPicture();
                    }
                    else
                    {
                        errorMessageBox("Can't update supplier details");
                    }
                }
            }
            else if (methodAction.Equals("SupplierDelete"))//Delete supplier
            {
                using (WebClient client2 = new WebClient())
                {
                    NameValueCollection postData2 = new NameValueCollection()
                    {
                       { "username", username }, //order: {"parameter name", "parameter value"}
                       { whatAction, whatID },//deleteSupplier , supplier id
                       { "name", arr[1] },
                       { "timeStamp", time }
                    };
                    try
                    {
                        nameSource = Encoding.UTF8.GetString(client2.UploadValues(getUrl(), postData2));
                    }
                    catch (Exception) { nameSource = "Catch"; MessageBox.Show(nameSource); }
                    if (nameSource.Equals("SUCCESS"))
                    {
                        setAllComponentsUnvisible();
                        clearTextDetails();
                        label15.Text = "Supplier deleted from database ";
                        label15.Visible = true;
                    }
                    else
                    {
                        errorMessageBox("Can't delete supplier ");
                    }
                }
            }
            else if (methodAction.Equals("CustomerDelete"))//Delete customer
            {
                using (WebClient client2 = new WebClient())
                {
                    NameValueCollection postData2 = new NameValueCollection()
                    {
                       { "username", username }, //order: {"parameter name", "parameter value"}
                       { whatAction, whatID },//deletecustomer , customer id
                       { "name", arr[1] },
                       { "timeStamp", time }
                    };
                    try
                    {
                        nameSource = Encoding.UTF8.GetString(client2.UploadValues(getUrl(), postData2));
                    }
                    catch (Exception) { nameSource = "Catch"; MessageBox.Show(nameSource); }
                    if (nameSource.Equals("SUCCESS"))
                    {
                        setAllComponentsUnvisible();
                        clearTextDetails();
                        label15.Text = "Customer deleted from database ";
                        label15.Visible = true;
                    }
                    else
                    {
                        errorMessageBox("Can't delete customer ");
                    }
                }
            }
            else if (methodAction.Equals("ProductDelete"))//Delete product
            {
                using (WebClient client2 = new WebClient())
                {
                    NameValueCollection postData2 = new NameValueCollection()
                    {
                       { "username", username }, //order: {"parameter name", "parameter value"}
                       { whatAction, whatID },//deleteProduct , product id
                       { "name", arr[1] },  //product name
                       { "timeStamp", time }    //actual time
                    };
                    try
                    {
                        nameSource = Encoding.UTF8.GetString(client2.UploadValues(getUrl(), postData2));
                    }
                    catch (Exception) { nameSource = "Catch"; MessageBox.Show(nameSource); }
                    if (nameSource.Equals("SUCCESS"))
                    {
                        setAllComponentsUnvisible();
                        clearTextDetails();
                        label15.Text = "Product deleted from database ";
                        label15.Visible = true;
                        deleteImageFromServer(getImagePath());
                        loadDefaultPicture();
                    }
                    else
                    {
                        errorMessageBox("Can't delete product ");
                    }
                }
            }
            else if (methodAction.Equals("Getdetails"))
            {
                //get Details
                using (WebClient client2 = new WebClient())
                {
                    NameValueCollection postData2 = new NameValueCollection()
               {
                   { "username", username }, //order: {"parameter name", "parameter value"}
                   { whatAction, whatID },


               };


                    try
                    {
                        nameSource = Encoding.UTF8.GetString(client2.UploadValues(getUrl(), postData2));
                    }
                    catch (Exception) { nameSource = "Catch"; MessageBox.Show(nameSource); }

                }
                ArrayList a = getNames(nameSource);
                string[] ar = (string[])a.ToArray(typeof(string));
               
                if(getStatecontroll()<4)
                {
                    fillTextDetails(ar);
                }
                else
                {
                    fillUserDetails(ar);
                }
                
            }
          




            //Default actions must call every time!!!!!!!!********************


            //Get the product associated with supplier
            if (getStatecontroll()==1)
            {
                using (WebClient client2 = new WebClient())
                {
                    NameValueCollection postData2 = new NameValueCollection()
               {
                   { "username", username }, //order: {"parameter name", "parameter value"}
                   { "getProducts", whatID },


               };


                    try
                    {
                        products = Encoding.UTF8.GetString(client2.UploadValues(getUrl(), postData2));
                    }
                    catch (Exception) { products = "Catch"; MessageBox.Show(products); }

                }
                ArrayList pr = getNames(products);

                fillProductDropDown(pr);
            }
           if(getStatecontroll() == 2)
            {
                using (WebClient client2 = new WebClient())
                {
                    NameValueCollection postData2 = new NameValueCollection()
               {
                   { "username", username }, //order: {"parameter name", "parameter value"}
                   { "bougthProducts", whatID },


               };


                    try
                    {
                        bougthProducts = Encoding.UTF8.GetString(client2.UploadValues(getUrl(), postData2));
                    }
                    catch (Exception) { bougthProducts = "Catch"; MessageBox.Show(bougthProducts); }

                }
                ArrayList pr = getNames(bougthProducts);
                foreach (string n in pr)
                fillProductDropDown(pr);
            }

        }

        //Email validation 
        private bool IsValidEmail(string email)
        {
            bool isEmail = Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            if (!isEmail)
                errorMessageBox("The Email address not valid");

            return isEmail;
        }

        //Check number input
        private Boolean ckeckNumber(String str, string message)
        {
            Boolean ch = true;
            try
            {
                long l1 = Convert.ToInt64(str);
            }
            catch (Exception)
            {

                ch = false;
            }
            //use for supplier and customer only
            if (ch == false )
                errorMessageBox("The " + message + " number not valid");

            return ch;
        }

        //Check fields for empty value Return TRUE if no empty value
        private Boolean checkFields(string[] arr)
        {
            string message = "";
            Boolean ans = true;

            for (int i = 0; i < arr.Length; i++)
            {
                if (String.IsNullOrEmpty(arr[i].Trim()))
                {

                    //check don't use switch for product hidden textbox1
                    if ( getStatecontroll() == 3 && i == 8 && getaddControl() == 2|| getStatecontroll() == 4 && i == 8)
                    {
                        //do nothing 
                    }
                    else
                    {
                        ans = false;
                        switch (i)
                        {
                            case 0: message = checkState() ? "ID filed empty" : "ID filed empty"; errorMessageBox(message); break;
                            case 1: message = checkState() ? "Name filed empty" : "Name filed empty"; errorMessageBox(message); break;
                            case 2: message = checkState() ? "Supplier filed empty" : "Street filed empty"; errorMessageBox(message); break;
                            case 3: message = checkState() ? "Type filed empty" : "Town filed empty"; errorMessageBox(message); break;
                            case 4: message = checkState() ? "Size filed empty" : "County filed empty"; errorMessageBox(message); break;
                            case 5: message = checkState() ? "Price filed empty" : "Country filed empty"; errorMessageBox(message); break;
                            case 6: message = checkState() ? "Colour filed empty" : "Post Code filed empty"; errorMessageBox(message); break;
                            case 7: message = checkState() ? "Stock filed empty" : "Email filed empty"; errorMessageBox(message); break;
                            case 8: message = checkState() ? "No imput required" : "Contact filed empty"; errorMessageBox(message); break;
                            default: break;
                        }
                    }
                }
            }
            return ans;
        }

        //helper method for checking empty fileds
        private Boolean checkState()
        {
            //if Product true ELSE false
            if (getStatecontroll() == 3 || getStatecontroll() == 4)
                return true;
            else
                return false;
        }

        //Popup Message box
        private void errorMessageBox(string str)
        {
            MessageBox.Show(str);
        }

        //Delete image from server
        private void deleteImageFromServer(string path)
        {
            try
            {
                FtpWebRequest requestFileDelete = (FtpWebRequest)WebRequest.Create("???" + path);
                requestFileDelete.Credentials = new NetworkCredential("???", "???");
                requestFileDelete.Method = WebRequestMethods.Ftp.DeleteFile;
                FtpWebResponse responseFileDelete = (FtpWebResponse)requestFileDelete.GetResponse();


            }
            catch (Exception) { errorMessageBox("Error deleting image!"); }
        }

        //get all text box values
        private string[] getTextDetails()
        {
            string[] arr = new string[9];
            arr[0] = textBox9.Text;
            arr[1] = textBox8.Text;
            arr[2] = textBox7.Text;
            arr[3] = textBox6.Text;
            arr[4] = textBox5.Text;
            arr[5] = textBox4.Text;
            arr[6] = textBox3.Text;
            arr[7] = textBox2.Text;
            arr[8] = textBox1.Text;

            return arr;
        }

        //Uploading picture to server
        public void UpLoadFile(string localFilePath)
        {
            try
            {
                //var fileName = Path.GetFileName(localFilePath);
                var fileName = getImageName();
                fileName = fileName.Replace(" ", String.Empty);
                var request = (FtpWebRequest)WebRequest.Create("???" + fileName);

                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential("???", "???");
                request.UsePassive = true;
                request.UseBinary = true;
                request.KeepAlive = false;

                using (var fileStream = File.OpenRead(localFilePath))
                {
                    using (var requestStream = request.GetRequestStream())
                    {
                        fileStream.CopyTo(requestStream);
                        requestStream.Close();
                    }
                }

                var response = (FtpWebResponse)request.GetResponse();
                response.Close();
            }
            catch (Exception) { errorMessageBox("Can't connect to server"); }
        }

        //set image path 
        private void setImagePath(string path)
        {
            imagePathToUpload = path;
        }
       
        //Set the image name to Product name
        private void setImageName(string name)
        {
            imageNameToSave = name;
        }

        //Get image name for upload
        private string getImageName()
        {
            return imageNameToSave;
        }
        //Get the image path to upload
        private string getImagePath()
        {
            return imagePathToUpload;
        }

        //Load image to product from server
        public void getPictureFromServer(String imgLocation)
        {
            Console.WriteLine(imgLocation);
            if (string.IsNullOrEmpty(imgLocation))
            {
                imgLocation = "picture_library/NO_IMAGE.jpg";
            }
   
                pictureBox1.ImageLocation = "???" + imgLocation;
        }


        //************************************************************

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

    }
}
