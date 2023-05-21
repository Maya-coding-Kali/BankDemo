using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace BankDemo
{
    internal class Address
    {
        public Address()
        {
            StreetNumber = "";
            StreetName = "";
            City = "";
            Province = "";
            PostalCode = "";
            Country = "";
          
        }

        public Address(string streetNumber, string streetName, string city,
                        string province, string postalCode, string country)
        {

            StreetNumber = streetNumber;
            StreetName = streetName;
            City = city;
            Province = province;
            PostalCode = postalCode;
            Country = country;
          
        }
        public string StreetNumber { get; set; }
        public string StreetName { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
     
    }
}
