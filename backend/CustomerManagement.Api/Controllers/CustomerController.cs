using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CustomerManagement.Api.Data;
using CustomerManagement.Api.Implementations;
using CustomerManagement.Api.Interfaces;
using CustomerManagement.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly ICriteriaFactory _criteriaFactory;

        // 
        public CustomerController(ApplicationDBContext context, ICriteriaFactory criteriaFactory)
        {
            // Dependency Injector container injecting the context and factory
            _context = context;
            _criteriaFactory = criteriaFactory;
        }

        // GET /api/Customer
        [HttpGet]
        public IActionResult GetAll()
        {
            var customer = _context.Customers.ToList();
            return Ok(customer);
        }

        // GET /api/Customer/byId/{id}
        [HttpGet("byId/{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        // GET /api/Customer/byCriteria?criteriaType=XXXX&criteriaValue=XXXX 
        // i think this adheres to Open/Closed Principle?
        [HttpGet("byCriteria")]
        public IActionResult GetByCriteria([FromQuery] string? criteriaType, [FromQuery] string? criteriaValue)
        {
            if (string.IsNullOrWhiteSpace(criteriaType) || string.IsNullOrWhiteSpace(criteriaValue))
                return BadRequest("Criteria type and value must be provided.");

            // // Create an instance of the appropriate criteria class based on the criteriaType
            // ICustomerSearchCriteria? criteria = criteriaType.ToLower() switch
            // {
            //     "firstname" => new FirstNameCriteria(criteriaValue),
            //     "lastname" => new LastNameCriteria(criteriaValue),
            //     "email" => new EmailCriteria(criteriaValue),
            //     _ => null
            // };
            // if (criteria == null)
            //     return BadRequest($"Invalid criteria type: {criteriaType}. Valid types are: firstname, lastname, email.");
            // var customers = criteria.Apply(_context.Customers.AsQueryable()).ToList();

            // if (!customers.Any())
            //     return NotFound();

            // return Ok(customers);


            // move the above logic to the factory
            try{
                // use factory to create the criteria object based on input
                var criteria = _criteriaFactory.Create(criteriaType, criteriaValue);
                // Apply the criteria to the customers query
                var customers = criteria.Apply(_context.Customers.AsQueryable()).ToList();

                if (!customers.Any())
                    return NotFound();
                
                return Ok(customers);
            } catch (ArgumentException e){
                return BadRequest(e.Message);
            }

            
        }

        // POST /api/Customer
        [HttpPost]
        public IActionResult Create([FromBody] Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = customer.Id }, customer);
        }
    }
}