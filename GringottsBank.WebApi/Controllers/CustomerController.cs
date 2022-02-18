using Gringotts.Core.Authentication;
using Gringotts.Core.Model;
using Gringotts.Core.ServiceInterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Security.Claims;

namespace GringottsBank.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("Customer")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        private ILogger<CustomerController> _logger;

        public CustomerController(ICustomerService customerSercie, ILogger<CustomerController> logger)
        {
            _customerService = customerSercie;

            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetCustomers()
        {
            var response = new BaseResponse<List<Customer>>();
            response.ResultObj = _customerService.GetCustomers();
            response.RspCode = Ok().StatusCode.ToString();
            if (_logger != null)
                _logger.LogInformation("Customers listed by {1}", ((ClaimsIdentity)User.Identity).Name);
            return Ok(response);
        }

        [HttpPost]
        public IActionResult AddCustomer(Customer customer)
        {
            var response = new BaseResponse<Customer>();
            _customerService.AddCustomer(customer);
            CreatedAtRoute("Customer", new { id = customer.CustomerNumber }, customer);
            response.RspCode = Ok().StatusCode.ToString();
            response.ResultObj = customer;
            if (_logger != null)
                _logger.LogInformation("Customer No {0} added by {1}", customer.CustomerNumber, ((ClaimsIdentity)User.Identity).Name);
            return Ok(response);
        }

        [HttpGet("{id}", Name = "Customer")]
        public IActionResult GetCustomerById(string id)
        {
            var response = new BaseResponse<Customer>();
            response.ResultObj = _customerService.GetCustomerById(id);
            response.RspCode = Ok().StatusCode.ToString();
            if (_logger != null)
                _logger.LogInformation("Customer No {0} screened by {1}", id, ((ClaimsIdentity)User.Identity).Name);
            return Ok(response);

        }

        [HttpDelete("{id}", Name = "Customer")]
        public IActionResult DeleteCustomer(string id)
        {
            var response = new BaseResponse<string>();
            response.RspCode = Ok().StatusCode.ToString();
            response.ResultObj = _customerService.DeleteCustomer(id);
            response.RspCode = Ok().StatusCode.ToString();
            if (_logger != null)
                _logger.LogInformation("Customer No {0} has deleted by {1}",id, ((ClaimsIdentity)User.Identity).Name);
            return Ok(response);
        }
    }
}
