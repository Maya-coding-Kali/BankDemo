using System.Data.SqlClient;
using System.Data;
using Microsoft.VisualBasic;

namespace BankDemo
{
    public partial class Form1 : Form
    {
        SqlConnection connObj;

        string fName;
        string lName;
        string streetNo;
        string streetName;
        string City;
        string province;
        string Postal;
        string Country;
        string phoneNo;
        string email;
        string DOB;
        int currentID;
        public Form1()
        {
            fName = "";
            lName = "";
            streetNo = "";
            streetName = "";
            City = "";
            province = "";
            Postal = "";
            Country = "";
            phoneNo = "";
            email = "";
            DOB = "";
            currentID = 0;
            connObj = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\repos\Teaching\C#\Winter2023\BankDemo\BankDemo\Bank.mdf;Integrated Security=True");
            InitializeComponent();
        }
        private void Clear_Form()
        {
            currentID = 1;
            textBox1.Clear();
            textBox3.Clear();
            textBox4.Clear();
            dateTimePicker1.ResetText();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
            textBox10.Clear();
            textBox11.Clear();
            textBox12.Clear();
            textBox13.Clear();

        }
        private bool FillVariables()
        {
            if (textBox3.Text == "" || textBox4.Text == "" || textBox6.Text == "" || textBox7.Text == "" || textBox8.Text == "" || textBox9.Text == "" || textBox10.Text == "" || textBox11.Text == "" || textBox12.Text == "" || textBox13.Text == "")
            {
                MessageBox.Show("Please Fill In All the Fields");
                return true;
            }
            fName = textBox3.Text;
            lName = textBox4.Text;
            DOB = dateTimePicker1.Text;
            streetNo = textBox6.Text;
            streetName = textBox7.Text;
            City = textBox8.Text;
            province = textBox9.Text;
            Postal = textBox10.Text;
            Country = textBox11.Text;
            phoneNo = textBox12.Text;
            email = textBox13.Text;
            currentID = Convert.ToInt32(textBox1.Text);
            return false;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Clear_Form();
            connObj = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\repos\Teaching\C#\Winter2023\BankDemo\BankDemo\Bank.mdf;Integrated Security=True");
            connObj.Open();
            SqlCommand command = new SqlCommand("select * from Customers;", connObj);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);

        }
        private bool Find_Data(string sql = "SELECT * from Customers")
        {
            
            SqlDataReader reader;
            SqlCommand command = new SqlCommand(sql, connObj);
            reader = command.ExecuteReader();
            if (!reader.HasRows)
            {
                reader.Close();
                return false;
            }
            while (reader.Read())
            {
                currentID = Convert.ToInt32(reader.GetValue(0));
                textBox1.Text = reader.GetValue(0).ToString();
                textBox3.Text = reader.GetValue(2).ToString();
                textBox4.Text = reader.GetValue(3).ToString();
                dateTimePicker1.Text = reader.GetValue(4).ToString();
                textBox6.Text = reader.GetValue(5).ToString();
                textBox7.Text = reader.GetValue(6).ToString();
                textBox8.Text = reader.GetValue(7).ToString();
                textBox9.Text = reader.GetValue(8).ToString();
                textBox10.Text = reader.GetValue(9).ToString();
                textBox11.Text = reader.GetValue(10).ToString();
                textBox12.Text = reader.GetValue(11).ToString();
                textBox13.Text = reader.GetValue(12).ToString();
            }
            reader.Close();
            return true;
        }

        private void Read_Click(object sender, EventArgs e)
        {
            // show all customers
            Form2 AllCustomers = new Form2();
            AllCustomers.Show();


        }
        private void Insert_click(object sender, EventArgs e)
        {
            //You should have a connection to the data base on your form load
            // Using SqlCommand and SqlDataReader do the following

            //make sure all of the textboxes are filled in
            //search if the record already exists (use sqlCommand to make the query, SqlDataReader go through the data)
            //    if it doesn't exist proceed to adding the record 
            //    if it does then tell the user it already exists 
            //instert the data into the database    (use sqlCommand to insert data and a function that is part of the sqlCommand called .ExecuteNonQuery())
            //Let the user know it was successful
            //----------Optional--------//
            //add another form that connects to accounts so you can add a chequing account for the user
            if (FillVariables())
            {
                return;
            }
            // add a search for super key
            if (Find_Data($"SELECT * FROM Customers WHERE FirstName = '{fName}' AND LastName = '{lName}' AND PhoneNo = '{phoneNo}' AND Email = '{email}'"))
            {               
                MessageBox.Show("Customer Already Exisits");
                return;
            }
            SqlCommand command = new SqlCommand($"INSERT INTO Customers (BranchID, FirstName, LastName, DOB, StreetNo, StreetName, City, Province, PostalCode, Country, PhoneNo, Email) Values ('2', '{fName}','{lName}','{DOB}', '{streetNo}' ,'{streetName}','{City}','{province}','{Postal}','{Country}','{phoneNo}', '{email}');", connObj);
            Find_Data($"SELECT * FROM Customers WHERE FirstName = '{fName}' AND LastName = '{lName}' AND PhoneNo = '{phoneNo}' AND Email = '{email}'");
            Form3 Accounts = new Form3(textBox1.Text);
            Accounts.Show();
            command.ExecuteNonQuery();
        }
        private void Update_Click(object sender, EventArgs e)
        {
            //You should have a connection to the data base on your form load
            // Using SqlCommand and SqlDataReader do the following

            //make sure all of the textboxes are filled in
            //search if the record already exists (use sqlCommand to make the query, SqlDataReader go through the data)
            //    if it doesn't exist proceed to adding the record 
            //    if it does then tell the user it already exists 
            //instert the data into the database    (use sqlCommand to insert data and a function that is part of the sqlCommand called .ExecuteNonQuery())
            //Let the user know it was successful
            //----------Optional--------//
            //add another form that connects to accounts so you can add a chequing account for the user
            if (FillVariables())
            {
                return;
            }
            SqlCommand cmd = new SqlCommand($"UPDATE Customers SET FirstName = '{fName}', LastName = '{lName}', BranchID = '2', DOB = '{DOB}', StreetNo = '{streetNo}', StreetName = '{streetName}', City = '{City}', Province = '{province}', PostalCode = '{Postal}', Country = '{Country}', PhoneNo = '{phoneNo}', Email = '{email}' Where CustomerID = {currentID}", connObj);
            cmd.ExecuteNonQuery();
            //find the record you want
            //if not found, return with message
            //ask what the user would like to update
            //let them know if it was successful
        }
        private void Previous_Click(object sender, EventArgs e)
        {
            //search the Database for the first customer that who Has an ID less than the current customer if there is not ID less than the current, let the user know they are at the beginning of the file
            if (!Find_Data($"SELECT TOP 1 * FROM Customers WHERE CustomerID < {currentID} ORDER BY CustomerID DESC"))
            {
                MessageBox.Show("Beginning of file");
            }
            //Determin where you are in the file
            //move to the next record or row
            //if at end of file let the user kknow

        }


        private void Next_Click(object sender, EventArgs e)
        {
            //search the Database for the first customer that who Has an ID grester than the current customer if there is not ID greater than the current, let the user know they are at the end of the file
            if (!Find_Data($"SELECT TOP 1 * FROM Customers WHERE CustomerID > {currentID} ORDER BY CustomerID ASC"))
            {
                MessageBox.Show("End of file");
            }

            //Determin where you are in the file
            //move to the next record or row
            //if at end of file let the user kknow

        }
        private void Delete_Click(object sender, EventArgs e)
        {
            // ask the user if they are sure they want to 
            //    if they dont want to delete, leave the function
            //    if they do proceed to the next step
            //search for the record
            //if you find it delete it
            //inform the user it had successfuly been deleted
            // clear data form form
            DialogResult dialogResult = MessageBox.Show("Are you sure you would like to delete????", "Delete", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.No)
            {
                return;
            }
            if (Find_Data($"SELECT * FROM Customers WHERE CustomerID = {currentID};"))
            {
                Find_Data($"DELETE FROM Customers WHERE CustomerID = {currentID};");
                MessageBox.Show("Delete Successful");
                Clear_Form();
            }
            //find the record you want
            //if not found, return with message
            //Check if the user is sure they want to delete?
            //Delete Record
            //let them know it was successful 
        }


        private void Get_Accounts_Click(object sender, EventArgs e)
        {
            Form3 Accounts = new Form3(textBox1.Text);
            Accounts.Show();
        }
        private void Find_Click(object sender, EventArgs e)
        {
            //if the customer ID is empty, let the user know to fill it in and leave the function
            //Search for the record
            //if found add to the textboxes using DataReader
            //if not found let the user know
            if (textBox1.Text == "")
            {
                
                MessageBox.Show("Please Fill The Customer ID");
                return;
            }
            if (!Find_Data($"SELECT * FROM Customers WHERE CustomerID = {textBox1.Text}"))
            {
                MessageBox.Show("Cannot find data");
            }
            //Fill_Form(dt);

        }
    }
}
