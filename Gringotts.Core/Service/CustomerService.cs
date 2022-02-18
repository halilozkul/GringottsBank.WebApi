using Gringotts.Core.DbUtil.Interface;
using Gringotts.Core.Exceptions;
using Gringotts.Core.Model;
using Gringotts.Core.ServiceInterface;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Gringotts.Core.Service
{
    public class CustomerService : ICustomerService
    {
        public IMongoCollection<Customer> _customer { get; set; }

        private ILogger<CustomerService> _logger;

        public CustomerService(IGringottsDbSettings settings, ILogger<CustomerService> logger)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _customer = database.GetCollection<Customer>(settings.CustomerCollection);
            _logger = logger;
        }

        public List<Customer> GetCustomers()
        {
            var customerList = _customer.Find(cust => true).ToList();
            if (customerList.Count == 0)
                throw new Exception(ExceptionMessages.noCustomerFoundInCollection);
            _logger.LogInformation("GetCustomers Service Called Succesfully");
            return customerList;
        }

        public Customer AddCustomer(Customer customer)
        {
            customer.InsertDateTime = DateTime.Now;
            customer.InsertDateTime = DateTime.MinValue;
            _customer.InsertOne(customer);
            _logger.LogInformation("AddCustomer Service Called Succesfully");
            return customer;
        }

        public Customer GetCustomerById(string id)
        {
            var customer = _customer.Find(cust => cust.CustomerNumber == id).ToList();
            if (customer.Count == 0)
                throw new Exception(ExceptionMessages.CustomerNotFoundException(id));
            _logger.LogInformation("GetCustomerById Service Called Succesfully");
            return customer[0];
        }

        public string DeleteCustomer(string id)
        {
            var customer = GetCustomerById(id);
            _customer.DeleteOne(cust => cust.CustomerNumber == customer.CustomerNumber);
            _logger.LogInformation("DeleteCustomer Service Called Succesfully");
            return string.Format("Customer {0} deleted.", id);
        }
    }
}
