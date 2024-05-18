using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CustomerManagement.Api.Data;
using CustomerManagement.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        // Unmutable
        private readonly ApplicationDBContext _context;
        public CustomerController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            // Deferred Execution
            var customer = _context.Customers.ToList();
            return Ok(customer);
        }

        [HttpGet("byId/{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            // Deferred Execution
            var customer = _context.Customers.Find(id);
            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        [HttpGet("byFirstName/{firstName}")]
        public IActionResult GetByFirstName([FromRoute] string firstName)
        {
            // Deferred Execution
            var customer = _context.Customers.Where(x => x.FirstName == firstName).ToList();
            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        [HttpGet("byLastName/{lastName}")]
        public IActionResult GetByLastName([FromRoute] string lastName)
        {
            // Deferred Execution
            var customer = _context.Customers.Where(x => x.LastName == lastName).ToList();
            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        [HttpGet("byEmail/{email}")]
        public IActionResult GetByEmail([FromRoute] string email)
        {
            // Deferred Execution
            var customer = _context.Customers.Where(x => x.Email == email).ToList();
            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        //TODO: Create GetByCriteria method for Open/Close Principle (OCP)

        [HttpPost]
        public IActionResult Create([FromBody] Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = customer.Id }, customer);
        }
    }
}