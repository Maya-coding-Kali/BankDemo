using System.Data.SqlClient;
using System.Data;
using Microsoft.VisualBasic;
using System.Diagnostics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Dynamic;
using System.Diagnostics.Metrics;
using System.Data.SqlTypes;

namespace BankDemo
{
    public partial class Form1 : Form
    {
        private readonly SqlConnection connObj;
        //string fName;
        //string lName;
        //string streetNo;
        //string streetName;
        //string City;
        //string province;
        //string Postal;
        //string Country;
        //string phoneNo;
        //string email;
        //string DOB;
        int currentID;
        //accounts
        //employees
        //manager
        //
        public Form1()
        {
            //fName = "";
            //lName = "";
            //streetNo = "";
            //streetName = "";
            //City = "";
            //province = "";
            //Postal = "";
            //Country = "";
            //phoneNo = "";
            //email = "";
            //DOB = "";
            currentID = 0;

            InitializeComponent();
        }
        private void ClearForm()
        {
            currentID = 0;
            textBox1.Clear();
            textBox2.Clear();
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
        private Customer? FillVariableFormData()
        {
            if (IsAnyFieldEmpty())
            {
                MessageBox.Show("Please fill in all the fields.");
                return null;
            }

            Customer customer = new Customer
            {
                CustomerID = Convert.ToInt32(textBox2.Text),
                FirstName = textBox3.Text,
                LastName = textBox4.Text,
                DateOfBirth = dateTimePicker1.Value,
                ContactDetail = new()
                {
                    Phone = textBox12.Text,
                    Email = textBox13.Text
                },
                HomeAddress = new()
                {
                    StreetNumber = textBox6.Text,
                    StreetName = textBox7.Text,
                    City = textBox8.Text,
                    Province = textBox9.Text,
                    PostalCode = textBox10.Text,
                    Country = textBox11.Text,
                }
            };

            currentID = customer.CustomerID;
            return customer;
        }

        private bool IsAnyFieldEmpty()
        {
            return string.IsNullOrWhiteSpace(textBox3.Text) ||
                   string.IsNullOrWhiteSpace(textBox4.Text) ||
                   string.IsNullOrWhiteSpace(textBox6.Text) ||
                   string.IsNullOrWhiteSpace(textBox7.Text) ||
                   string.IsNullOrWhiteSpace(textBox8.Text) ||
                   string.IsNullOrWhiteSpace(textBox9.Text) ||
                   string.IsNullOrWhiteSpace(textBox10.Text) ||
                   string.IsNullOrWhiteSpace(textBox11.Text) ||
                   string.IsNullOrWhiteSpace(textBox12.Text) ||
                   string.IsNullOrWhiteSpace(textBox13.Text);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private bool FindData(int id = 0, string sqlQuery = "SELECT * FROM Customers")
        {
            try
            {
                const string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\repos\Teaching\C#\Winter2023\BankDemo\BankDemo\Bank.mdf;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        if (id != 0)
                        {
                            sqlQuery = "SELECT * FROM Customers WHERE CustomerID = @CustomerID";
                            command.Parameters.AddWithValue("@CustomerID", id);
                            command.CommandText = sqlQuery;
                        }
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    UpdateTextBoxes(reader);
                                }
                                return true;
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error Finding Customer Data" + ex);
            }
            return false;
        }
        private void UpdateTextBoxes(SqlDataReader reader)
        {
            currentID = Convert.ToInt32(reader[0]);
            textBox2.Text = reader[0].ToString();
            textBox3.Text = reader[2].ToString();
            textBox4.Text = reader[3].ToString();
            dateTimePicker1.Value = (DateTime)reader[4];
            textBox6.Text = reader[5].ToString();
            textBox7.Text = reader[6].ToString();
            textBox8.Text = reader[7].ToString();
            textBox9.Text = reader[8].ToString();
            textBox10.Text = reader[9].ToString();
            textBox11.Text = reader[10].ToString();
            textBox12.Text = reader[11].ToString();
            textBox13.Text = reader[12].ToString();
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
            try
            {
                Customer? customer = FillVariableFormData();
                if (customer == null)
                {
                    return;
                }
                // add a search for super key
                if (FindData(sqlQuery: $"SELECT * FROM Customers WHERE FirstName = '{customer.FirstName}' AND LastName = '{customer.LastName}' AND PhoneNo = '{customer.ContactDetail.Phone}' AND Email = '{customer.ContactDetail.Email}'"))
                {
                    MessageBox.Show("Customer Already Exisits");
                    return;
                }
                using (SqlConnection connObj = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\repos\Teaching\C#\Winter2023\BankDemo\BankDemo\Bank.mdf;Integrated Security=True"))
                {
                    connObj.Open();
                    using SqlCommand command = new SqlCommand("INSERT INTO Customers (FirstName, LastName, DOB, StreetNo, StreetName, City, Province, PostalCode, Country, PhoneNo, Email) Values (@FirstName, @LastName, @DOB, @StreetNo, @StreetName, @City, @Province, @PostalCode, @Country, @PhoneNo, @Email)", connObj);

                    command.Parameters.AddWithValue("@FirstName", customer.FirstName);
                    command.Parameters.AddWithValue("@LastName", customer.LastName);
                    command.Parameters.AddWithValue("@DOB", customer.DateOfBirth);
                    command.Parameters.AddWithValue("@StreetNo", customer.HomeAddress.StreetNumber);
                    command.Parameters.AddWithValue("@StreetName", customer.HomeAddress.StreetName);
                    command.Parameters.AddWithValue("@City", customer.HomeAddress.City);
                    command.Parameters.AddWithValue("@Province", customer.HomeAddress.Province);
                    command.Parameters.AddWithValue("@PostalCode", customer.HomeAddress.PostalCode);
                    command.Parameters.AddWithValue("@Country", customer.HomeAddress.Country);
                    command.Parameters.AddWithValue("@PhoneNo", customer.ContactDetail.Phone);
                    command.Parameters.AddWithValue("@Email", customer.ContactDetail.Email);


                    //FindData($"SELECT * FROM Customers WHERE FirstName = '{fName}' AND LastName = '{lName}' AND PhoneNo = '{phoneNo}' AND Email = '{email}'");
                    // Form3 Accounts = new Form3(textBox2.Text);
                    //Accounts.Show();
                    int recordsAffected = command.ExecuteNonQuery();
                    if (recordsAffected == 1)
                    {
                        MessageBox.Show("Record added");
                    }
                    else
                    {
                        MessageBox.Show("Could Not Add Record");
                    }
                }
            }
            catch (SqlException ex)
            {

                MessageBox.Show("Error Adding Customer data" + ex);
            }
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
            try
            {



                Customer? customer = FillVariableFormData();
                if (customer == null)
                {
                    return;
                }
                using SqlConnection connObj = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\repos\Teaching\C#\Winter2023\BankDemo\BankDemo\Bank.mdf;Integrated Security=True");
                connObj.Open();
                using SqlCommand cmd = new SqlCommand("UPDATE Customers SET FirstName = @FirstName, LastName = @LastName, BranchID = @BranchID, DOB = @DOB, StreetNo = @StreetNo, StreetName = @StreetName, City = @City, Province = @Province, PostalCode = @PostalCode, Country = @Country, PhoneNo = @PhoneNo, Email = @Email WHERE CustomerID = @CustomerID", connObj);
                cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
                cmd.Parameters.AddWithValue("@LastName", customer.LastName);
                cmd.Parameters.AddWithValue("@BranchID", 2);
                cmd.Parameters.AddWithValue("@DOB", customer.DateOfBirth);
                cmd.Parameters.AddWithValue("@StreetNo", customer.HomeAddress.StreetNumber);
                cmd.Parameters.AddWithValue("@StreetName", customer.HomeAddress.StreetName);
                cmd.Parameters.AddWithValue("@City", customer.HomeAddress.City);
                cmd.Parameters.AddWithValue("@Province", customer.HomeAddress.Province);
                cmd.Parameters.AddWithValue("@PostalCode", customer.HomeAddress.PostalCode);
                cmd.Parameters.AddWithValue("@Country", customer.HomeAddress.Country);
                cmd.Parameters.AddWithValue("@PhoneNo", customer.ContactDetail.Phone);
                cmd.Parameters.AddWithValue("@Email", customer.ContactDetail.Email);
                cmd.Parameters.AddWithValue("@CustomerID", currentID);
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Successfully Updated Customer Data");
                }
                else
                {
                    MessageBox.Show("Could Not Update Customer Data");
                }
            }
            catch (SqlException ex)
            {

                MessageBox.Show("Error Adding Customer Data" + ex);
            }
            //find the record you want
            //if not found, return with message
            //ask what the user would like to update
            //let them know if it was successful
        }
        private void Previous_Click(object sender, EventArgs e)
        {
            //search the Database for the first customer that who Has an ID less than the current customer if there is not ID less than the current, let the user know they are at the beginning of the file
            if (!FindData(sqlQuery: $"SELECT TOP 1 * FROM Customers WHERE CustomerID < {currentID} ORDER BY CustomerID DESC"))
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
            if (!FindData(sqlQuery: $"SELECT TOP 1 * FROM Customers WHERE CustomerID > {currentID} ORDER BY CustomerID ASC"))
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
            if (FindData(currentID))
            {
                try
                {
                    const string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\repos\Teaching\C#\Winter2023\BankDemo\BankDemo\Bank.mdf;Integrated Security=True";
                    const string sqlQuery = $"DELETE FROM Customers WHERE CustomerID = @CustomerID;";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                        {
                            command.Parameters.AddWithValue("@CustomerID", currentID);
                            int rowsAffected = command.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Delete Successful");
                                ClearForm();
                            }
                            else
                            {
                                MessageBox.Show("Could Not Delete Customer Data");
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Error Finding Customer Data" + ex);
                }

            }
            else
            {
                MessageBox.Show("Could Not Find Customer To Delete");
            }
            //find the record you want
            //if not found, return with message
            //Check if the user is sure they want to delete?
            //Delete Record
            //let them know it was successful 
        }
        private void Get_Accounts_Click(object sender, EventArgs e)
        {
            Form3 Accounts = new Form3(textBox2.Text);
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
            if (!FindData(Convert.ToInt32(textBox1.Text)))
            {
                MessageBox.Show("Cannot find data");
            }
            //Fill_Form(dt);
        }
        private void Clear_Form_Click(object sender, EventArgs e)
        {
            ClearForm();
        }
    }
}

