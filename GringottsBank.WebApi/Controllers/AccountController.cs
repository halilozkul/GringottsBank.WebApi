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
    [Route("Account")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        
        private ILogger<AccountController> _logger;
        
        public AccountController(IAccountService accountService, ILogger<AccountController> logger)
        {
            _accountService = accountService;

            _logger = logger;
        }

        [HttpPost]
        public IActionResult AddAccount(AccountDetail accountDetail)
        {
            var response = new BaseResponse<AccountDetail>();
            _accountService.AddAccount(accountDetail);
            response.RspCode = Ok().StatusCode.ToString();
            response.ResultObj = accountDetail;
            if(_logger != null)
                _logger.LogInformation("New Account Created by user {0}, for customer no {1}", ((ClaimsIdentity)User.Identity).Name, accountDetail.CustomerNumber);
            return Ok(response);
        }

        [HttpGet]
        public IActionResult GetAccountByCustomer([FromQuery] string customerId)
        {
            var response = new BaseResponse<List<Account>>();
            response.ResultObj = _accountService.GetAccountByCustomer(customerId);
            response.RspCode = Ok().StatusCode.ToString();
            if (_logger != null)
                _logger.LogInformation("Accounts of customer no {0} listed by {1}", customerId, ((ClaimsIdentity)User.Identity).Name);
            return Ok(response);
        }

        [HttpGet("Detail")]
        public IActionResult GetAccountDetailByCustomer([FromQuery] string customerId)
        {
            var response = new BaseResponse<List<AccountDetail>>();
            response.ResultObj = _accountService.GetAccountDetailByCustomer(customerId);
            response.RspCode = Ok().StatusCode.ToString();
            if (_logger != null)
                _logger.LogInformation("Account details of customer no {0} listed by {1}", customerId, ((ClaimsIdentity)User.Identity).Name);
            return Ok(response);
        }
    }
}
