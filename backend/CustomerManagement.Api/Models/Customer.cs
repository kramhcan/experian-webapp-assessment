using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CustomerManagement.Api.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; } 
        public string LastName { get; set; }
        public string Email { get; set; }

        public void ValidateEmptyOrNull()
        {
            if (string.IsNullOrEmpty(FirstName))
                throw new ArgumentException("First Name is required.");

            if (string.IsNullOrEmpty(LastName))
                throw new ArgumentException("Last Name is required.");

            if (string.IsNullOrEmpty(Email) || !IsValidEmail(Email))
                throw new ArgumentException("Invalid Email format.");
                
        }

        private bool IsValidEmail(string email)
        {
            const string emailRegex = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4})$";
            return Regex.IsMatch(email, emailRegex);
        }
    }


}