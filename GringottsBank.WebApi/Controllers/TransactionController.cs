using Gringotts.Core.Model;
using Gringotts.Core.ServiceInterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace GringottsBank.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("Transaction")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        private ILogger<TransactionController> _logger;
        
        public TransactionController(ITransactionService transactionService, ILogger<TransactionController> logger)
        {
            _transactionService = transactionService;

            _logger = logger;
        }
        [HttpPut]
        public IActionResult AddTransaction(Transaction transaction)
        {
            var response = new BaseResponse<Transaction>();
            _transactionService.AddTransaction(transaction);
            response.RspCode = Ok().StatusCode.ToString();
            response.ResultObj = transaction;
            if (_logger != null)
                _logger.LogInformation("Transaction no {0} executed by {1}", transaction.TransactionId, ((ClaimsIdentity)User.Identity).Name);
            return Ok(response);
        }

        [HttpGet]
        public IActionResult GetTransactionsByAccount([FromQuery] string accountId)
        {
            var response = new BaseResponse<List<Transaction>>();
            response.ResultObj = _transactionService.GetTransactionsByAccount(accountId);
            response.RspCode = Ok().StatusCode.ToString();
            if (_logger != null)
                _logger.LogInformation("Transactions for account no {0} listed by {1}", accountId, ((ClaimsIdentity)User.Identity).Name);
            return Ok(response);
        }

        [HttpGet("ByDate")]
        public IActionResult GetTransactionsByDate([FromQuery] string customerNumber, [FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
        {
            var response = new BaseResponse<List<Transaction>>();
            response.ResultObj = _transactionService.GetTransactionsByDate(customerNumber, fromDate, toDate);
            response.RspCode = Ok().StatusCode.ToString();
            if (_logger != null)
                _logger.LogInformation("Transactions for customer no {0} listed by {1}", customerNumber, ((ClaimsIdentity)User.Identity).Name);
            return Ok(response);
        }
    }
}
