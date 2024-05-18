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
            var stocks = _context.Customers.ToList();
            return Ok(stocks);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            // Deferred Execution
            var stocks = _context.Customers.Find(id);
            if (stocks == null)
                return NotFound();

            return Ok(stocks);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = customer.Id }, customer);
        }
    }
}