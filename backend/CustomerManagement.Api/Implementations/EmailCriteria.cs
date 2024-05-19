using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerManagement.Api.Interfaces;
using CustomerManagement.Api.Models;

namespace CustomerManagement.Api.Implementations
{
    public class EmailCriteria : ICustomerSearchCriteria
    {
        private readonly string _email;

        public EmailCriteria(string email)
        {
            _email = email;
        }

        public IQueryable<Customer> Apply(IQueryable<Customer> query)
        {
            return query.Where(c => c.Email == _email);
        }
    }
}