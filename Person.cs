using System.Diagnostics.Metrics;
using System.Net;
using System.Numerics;

namespace SimpleDB
{
    public class Person
    {
        public int Id;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }


        public Person ()
        {
            FirstName = String.Empty;
            LastName = String.Empty;
            Address = String.Empty;
            City = String.Empty;
            State = String.Empty;
            Country = String.Empty;
            PostalCode = String.Empty;
            Phone = String.Empty;
        }

        public Person(int id, string firstName, string lastName)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Address = String.Empty;
            City = String.Empty; 
            State = String.Empty; 
            Country = String.Empty; 
            PostalCode = String.Empty; 
            Phone = String.Empty; 
        }

        public Person(int id, string firstName, string lastName, string address, string city, string state, string country, string postalCode, string phone)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            City = city;
            State = state;
            Country = country;
            PostalCode = postalCode;
            Phone = phone;
        }
    }

}
