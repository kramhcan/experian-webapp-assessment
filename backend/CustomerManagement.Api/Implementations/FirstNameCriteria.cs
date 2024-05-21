using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerManagement.Api.Interfaces;
using CustomerManagement.Api.Models;

namespace CustomerManagement.Api.Implementations
{
    //class to search customers by first name
    public class FirstNameCriteria : ICustomerSearchCriteria
    {
        private readonly string _firstName;

        public FirstNameCriteria(string firstName)
        {
            _firstName = firstName;
        }

        public IQueryable<Customer> Apply(IQueryable<Customer> query)
        {
            return query.Where(c => c.FirstName == _firstName);
        }
    }
}
