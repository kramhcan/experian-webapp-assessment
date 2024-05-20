using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerManagement.Api.Interfaces;

namespace CustomerManagement.Api.Implementations
{
    // this is factory class that creates the criteria class instances
    
    public class CriteriaFactory : ICriteriaFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public CriteriaFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ICustomerSearchCriteria Create(string criteriaType, string criteriaValue)
        {
            // var criteria = _serviceProvider.GetService<ICustomerSearchCriteria>(criteriaType);
            // criteria.Value = criteriaValue;
            // return criteria;

            // create instance based on criteria type
            return criteriaType.ToLower() switch
            {
                "email" => new EmailCriteria(criteriaValue),
                "firstname" => new FirstNameCriteria(criteriaValue),
                "lastname" => new LastNameCriteria(criteriaValue),
                // Can add more criteria types here
                _ => throw new ArgumentException("Invalid criteria type")
            };
        }
    }
}