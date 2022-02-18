using Gringotts.Core.Model;
using Gringotts.Core.ServiceInterface;
using GringottsBank.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Gringotts.Test
{
    public class CustomerControllerTests
    {
        private readonly Mock<ICustomerService> mockCustomerService;
        private readonly CustomerController customerController;

        public CustomerControllerTests()
        {
            mockCustomerService = new Mock<ICustomerService>();
            customerController = new CustomerController(mockCustomerService.Object, null);
        }

        [Fact]
        public void Get_WhenCalled_Returns_All_CustomerList()
        {
            var mockCustomers = new List<Customer>()
            {
                new Customer
                {
                    CustomerNumber = "620d65eb2b34f6346406a6d2",
                    InsertDateTime = DateTime.Now,
                    UpdateDateTime = DateTime.MinValue,
                    Name = "Halil",
                    Surname = "Ozkul"
                },
                new Customer
                {
                    CustomerNumber = "620d65eb2b34f6346406a6d5",
                    InsertDateTime = DateTime.Now,
                    UpdateDateTime = DateTime.MinValue,
                    Name = "İlker",
                    Surname = "Eski"
                }
            };
            mockCustomerService.Setup(mock => mock.GetCustomers()).Returns(mockCustomers);
            var result = customerController.GetCustomers() as OkObjectResult;
            var accountResp = Assert.IsType<List<Customer>>(((BaseResponse<List<Customer>>)(result.Value)).ResultObj);
            Assert.Equal(2, accountResp.Count());
        }

        [Fact]
        public void Add_WhenCalled_Adds_Customer_Returns_Customer()
        {
            var mockCustomer = new Customer()
            {
                CustomerNumber = "620d65eb2b34f6346406a6d2",
                InsertDateTime = DateTime.Now,
                UpdateDateTime = DateTime.MinValue,
                Name = "Halil",
                Surname = "Ozkul"
            };

            mockCustomerService.Setup(mock => mock.AddCustomer(mockCustomer)).Returns(mockCustomer);
            var result = customerController.AddCustomer(mockCustomer) as OkObjectResult;
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Get_WhenCalled_With_CustomerId_Returns_Customer()
        {
            var mockCustomerNumber = "620d65eb2b34f6346406a6d2";
            var mockCustomer = new Customer()
            {
                CustomerNumber = "620d65eb2b34f6346406a6d2",
                InsertDateTime = DateTime.Now,
                UpdateDateTime = DateTime.MinValue,
                Name = "Halil",
                Surname = "Ozkul"
            };
            mockCustomerService.Setup(mock => mock.GetCustomerById(mockCustomerNumber)).Returns(mockCustomer);
            var result = customerController.GetCustomerById(mockCustomerNumber) as OkObjectResult;
            var customerResp = Assert.IsType<Customer>(((BaseResponse<Customer>)(result.Value)).ResultObj);
            Assert.Equal(customerResp.CustomerNumber, mockCustomerNumber);
        }

        [Fact]
        public void Delete_WhenCalled_With_CustomerId_Deletes_Customer_Returns_DeletedCustomerIdMessage()
        {
            var mockCustomerNumber = "620d65eb2b34f6346406a6d2";
            var mockCustomer = new Customer()
            {
                CustomerNumber = "620d65eb2b34f6346406a6d2",
                InsertDateTime = DateTime.Now,
                UpdateDateTime = DateTime.MinValue,
                Name = "Halil",
                Surname = "Ozkul"
            };
            mockCustomerService.Setup(mock => mock.DeleteCustomer(mockCustomerNumber)).Returns(string.Format("Customer {0} deleted.", mockCustomerNumber));
            var result = customerController.DeleteCustomer(mockCustomerNumber) as OkObjectResult;
            var customerResp = Assert.IsType<string>(((BaseResponse<string>)(result.Value)).ResultObj);
            Assert.Equal(mockCustomer.CustomerNumber, mockCustomerNumber);
        }
    }
}
