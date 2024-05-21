using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerManagement.Api.Interfaces;

namespace CustomerManagement.Api.Implementations
{
    //this is the factory class that creates the criteria class instances
    public class CriteriaFactory : ICriteriaFactory
    {
        private readonly IServiceProvider _serviceProvider;

        //constructor that accepts IServiceProvider
        public CriteriaFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        //method to create criteria instance based on criteria type
        public ICustomerSearchCriteria Create(string criteriaType, string criteriaValue)
        {
            // create instance based on criteria type
            return criteriaType.ToLower() switch
            {
                "email" => new EmailCriteria(criteriaValue),
                "firstname" => new FirstNameCriteria(criteriaValue),
                "lastname" => new LastNameCriteria(criteriaValue),
                //can add more criteria types here
                _ => throw new ArgumentException("Invalid criteria type")
            };
        }
    }
}