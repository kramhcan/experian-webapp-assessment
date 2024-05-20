using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using CustomerManagement.Api.Controllers;
using CustomerManagement.Api.Interfaces;
using CustomerManagement.Api.Models;
using CustomerManagement.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagement.Tests
{
    public class CustomerControllerTests
    {
        [Fact]
        public void GetByCriteria_ReturnsOkResult_WithValidCriteria()
        {
            //note to self: https://medium.com/@pjbgf/title-testing-code-ocd-and-the-aaa-pattern-df453975ab80
            //and https://www.c-sharpcorner.com/UploadFile/dacca2/fundamental-of-unit-testing-understand-aaa-in-unit-testing/
            //there are a lot of note-to-selfs here, hmmmm...

            //Arrange
            //initialize mock and in-memory database context
            var mockFactory = new Mock<ICriteriaFactory>();

            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(databaseName: "CustomerDatabase")
                .Options;
            using var mockContext = new ApplicationDBContext(options);

            //create test customer to in-memory db
            mockContext.Customers.AddRange(
                new Customer
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john@example.com"
                },
                new Customer
                {
                    Id = 2,
                    FirstName = "Jane",
                    LastName = "Doe",
                    Email = "jane@example.com"
                }
            );
            mockContext.SaveChanges();

            //mock the criteria and set up expected behaviour
            var criteria = new Mock<ICustomerSearchCriteria>();

            //setup mock to use apply method and return customers with the email
            //note to self: c => c.Apply(It.IsAny<IQueryable<Customer>>()) is a lambda expression specifying that the setup applies to calls 
            //to the Apply method with any IQueryable<Customer> argument (in ICustomerSearchCriteria)
            //another note to self: It.IsAny<T>() is a method that returns a default value for the type T
            criteria.Setup(c => c.Apply(It.IsAny<IQueryable<Customer>>()))
                    //specifies that the Apply method should return a list of customers with the specific email
                    .Returns(mockContext.Customers.Where(c => c.Email == "john@example.com"));

            //setup mock to use create method with email criteria and return the criteria object
            mockFactory.Setup(f => f.Create("email", "john@example.com")).Returns(criteria.Object);

            //create instance of CustomerController with mocked dependencies
            var controller = new CustomerController(mockContext, mockFactory.Object);

            // Act
            //call GetByCriteria with valid criteria
            var result = controller.GetByCriteria("email", "john@example.com");

            // Assert
            // verify results return a OkObjectResult
            var okResult = Assert.IsType<OkObjectResult>(result);
            // verify the result is a list of customers
            var returnValue = Assert.IsType<List<Customer>>(okResult.Value);
        }

        [Fact]
        public void GetByCriteria_ReturnsNotFoundResult_WhenNoMatchingCriteria()
        {
            // Arrange
            var mockFactory = new Mock<ICriteriaFactory>();
            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(databaseName: "CustomerDatabase")
                .Options;
            using var mockContext = new ApplicationDBContext(options);


            //if id is same as other methods, test will fail
            //not sure why the id must be unique to other tests
            //maybe because the in-memory db is shared?
            mockContext.Customers.AddRange(
                new Customer
                {
                    Id = 3,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john@example.com"
                },
                new Customer
                {
                    Id = 4,
                    FirstName = "Jane",
                    LastName = "Doe",
                    Email = "jane@example.com"
                }
            );
            mockContext.SaveChanges();

            ///make it so returns empty list
            var criteria = new Mock<ICustomerSearchCriteria>();
            criteria.Setup(c => c.Apply(It.IsAny<IQueryable<Customer>>()))
                    .Returns(Enumerable.Empty<Customer>().AsQueryable());

            mockFactory.Setup(f => f.Create("email", "nomatch@example.com")).Returns(criteria.Object);

            var controller = new CustomerController(mockContext, mockFactory.Object);

            // Act
            var result = controller.GetByCriteria("email", "nomatch@example.com");

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetByCriteria_ReturnsBadRequest_WhenInvalidCriteria()
        {
            // Arrange
            var mockFactory = new Mock<ICriteriaFactory>();
            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(databaseName: "CustomerDatabase")
                .Options;
            var mockContext = new ApplicationDBContext(options);

            var controller = new CustomerController(mockContext, mockFactory.Object);

            // Act
            var result = controller.GetByCriteria(null, null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void GetByCriteria_ReturnsOkResult_WithFirstNameCriteria()
        {
            // Arrange
            var mockFactory = new Mock<ICriteriaFactory>();
            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(databaseName: "CustomerDatabase")
                .Options;
            using var mockContext = new ApplicationDBContext(options);

            mockContext.Customers.AddRange(
                new Customer
                {
                    Id = 5,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john@example.com"
                },
                new Customer
                {
                    Id = 6,
                    FirstName = "Jane",
                    LastName = "Doe",
                    Email = "jane@example.com"
                }
            );
            mockContext.SaveChanges();

            //set up criteria to return customers with first name "John"
            var criteria = new Mock<ICustomerSearchCriteria>();
            criteria.Setup(c => c.Apply(It.IsAny<IQueryable<Customer>>()))
                    .Returns(mockContext.Customers.Where(c => c.FirstName == "John"));

            mockFactory.Setup(f => f.Create("firstname", "John")).Returns(criteria.Object);

            var controller = new CustomerController(mockContext, mockFactory.Object);

            // Act
            var result = controller.GetByCriteria("firstname", "John");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<Customer>>(okResult.Value);
            Assert.Contains(returnValue, c => c.FirstName == "John" && c.LastName == "Doe");
        }
    }
}