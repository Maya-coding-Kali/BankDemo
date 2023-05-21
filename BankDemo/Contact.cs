using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankDemo
{
    internal class Contact
    {
        public Contact()
        {           
            Phone = "";
            Email = "";
        }

        public Contact(string phone, string email)
        {
            Phone = phone;
            Email = email;
        }
       
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
