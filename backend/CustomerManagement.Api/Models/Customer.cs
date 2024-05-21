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
        public required string FirstName { get; set; } 
        public required string LastName { get; set; }
        public required string Email { get; set; }
    }


}