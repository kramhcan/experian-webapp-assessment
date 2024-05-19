using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerManagement.Api.Models;

namespace CustomerManagement.Api.Interfaces
{
    public interface ICustomerSearchCriteria
    {
        IQueryable<Customer> Apply(IQueryable<Customer> query);
    }
}