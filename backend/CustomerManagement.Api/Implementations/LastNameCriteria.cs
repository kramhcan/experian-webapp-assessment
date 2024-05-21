using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerManagement.Api.Interfaces;
using CustomerManagement.Api.Models;

namespace CustomerManagement.Api.Implementations
{
    //class to search customers by last name
    public class LastNameCriteria : ICustomerSearchCriteria
    {
        private readonly string _lastName;

        public LastNameCriteria(string lastName)
        {
            _lastName = lastName;
        }

        public IQueryable<Customer> Apply(IQueryable<Customer> query)
        {
            return query.Where(c => c.LastName == _lastName);
        }
    }
}