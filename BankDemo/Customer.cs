using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BankDemo
{
    internal class Customer
    {        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int CustomerID { get; set; }
        public Address HomeAddress { get; set; }
        public Contact ContactDetail { get; set; }

        public Customer()
        {
            FirstName = "";
            LastName = "";
            DateOfBirth = DateTime.Now;
            CustomerID = 0;
            HomeAddress = new Address();
            ContactDetail = new Contact();
        }
        ~Customer()
        {
            Console.WriteLine("Good Bye");
        }
        public Customer(string firstName, string lastName, 
                        DateTime dateOfBirth, int customerID, Address addressAddress)
        {
            FirstName = firstName;
            LastName = lastName;           
            DateOfBirth = dateOfBirth;
            CustomerID = customerID;
            HomeAddress = addressAddress;
        }
    }
}
