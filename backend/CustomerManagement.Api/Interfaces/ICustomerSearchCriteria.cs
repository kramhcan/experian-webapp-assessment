using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerManagement.Api.Models;

namespace CustomerManagement.Api.Interfaces
{
    //interface for search criteria
    public interface ICustomerSearchCriteria
    {
        //method applying criterion to custmer query
        IQueryable<Customer> Apply(IQueryable<Customer> query);
    }
}