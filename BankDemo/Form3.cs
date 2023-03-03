using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Microsoft.VisualBasic;

namespace BankDemo
{
    public partial class Form3 : Form
    {
        bool Chequing;
        bool Savings;
        bool Investing;
        string clientNumber;
        SqlConnection connObj;

        public Form3(string clientNumber)
        {

            this.clientNumber = clientNumber;
            Chequing = false;
            Savings = false;
            Investing = false;
            connObj = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\repos\Teaching\C#\Winter2023\BankDemo\BankDemo\Bank.mdf;Integrated Security=True");
            InitializeComponent();
        }
        private bool Find_Data(string sql = "SELECT * From Accounts")
        {
            SqlDataReader reader;
            SqlCommand command = new SqlCommand(sql, connObj);
            reader = command.ExecuteReader();
            if (!reader.HasRows)
            {
                reader.Close();
                MessageBox.Show("No Accounts");
                return false;
            }
            while (reader.Read())
            {

                Chequing = Convert.ToBoolean(reader.GetValue(1));
                Savings = Convert.ToBoolean(reader.GetValue(2));
                Investing = Convert.ToBoolean(reader.GetValue(3));

            }
            if (Chequing)
            {
                MessageBox.Show("There is already a Chequing account");
                return false;
            }
            reader.Close();
            return true;
        }
        void Update_Data()
        {
            SqlCommand command = new SqlCommand($"SELECT * from Accounts WHERE CustomerID = {clientNumber};", connObj);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
        }
        private void Form3_Load(object sender, EventArgs e)
        {

            connObj.Open();
            Update_Data();

        }



        private void Chequing_Click(object sender, EventArgs e)
        {
            if (!Find_Data($"SELECT * from Accounts WHERE CustomerID = {clientNumber}"))
            {
                return;
            }
            // add a search for super key
            SqlCommand command = new SqlCommand($"INSERT INTO Accounts (Chequing, CustomerID) Values ('{Chequing}', '{clientNumber}');", connObj);
            command.ExecuteNonQuery();
            Update_Data();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!Find_Data($"SELECT * from Accounts WHERE CustomerID = {clientNumber}"))
            {
                return;
            }
            
            // add a search for super key
            SqlCommand command = new SqlCommand($"INSERT INTO Accounts (Savings, CustomerID) Values ('{Savings}', '{clientNumber}');", connObj);
            command.ExecuteNonQuery();
            Update_Data();
        }
    }
}
