using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerManagement.Api.Interfaces
{
    // interface to define factory method
    public interface ICriteriaFactory
    {
        // factory method to create criteria instances
        ICustomerSearchCriteria Create(string criteriaType, string criteriaValue);
    }
}