using Gringotts.Core.DbUtil.Interface;
using Gringotts.Core.Exceptions;
using Gringotts.Core.Model;
using Gringotts.Core.ServiceInterface;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gringotts.Core.Service
{
    public class AccountService : IAccountService
    {
        public IMongoCollection<AccountDetail> _accountDetail { get; set; }
        public IMongoCollection<Customer> _customer { get; set; }

        private ILogger<AccountService> _logger;

        public AccountService(IGringottsDbSettings settings, ILogger<AccountService> logger)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _accountDetail = database.GetCollection<AccountDetail>(settings.AccountCollection);
            _customer = database.GetCollection<Customer>(settings.CustomerCollection);
            _logger = logger;
        }
        public AccountDetail AddAccount(AccountDetail accountDetail)
        {
            var customerList = _customer.Find(cust => true).ToList();
            if (!customerList.Any(x => x.CustomerNumber == accountDetail.CustomerNumber))
                throw new Exception(ExceptionMessages.CustomerNotFoundException(accountDetail.CustomerNumber));
            accountDetail.UpdateDateTime = DateTime.MinValue;
            accountDetail.InsertDateTime = DateTime.Now;
            _accountDetail.InsertOne(accountDetail);
            _logger.LogInformation("AddAccount Service called succesfully");
            return accountDetail;
        }

        public List<Account> GetAccountByCustomer(string customerNumber)
        {
            var customer = _customer.Find(x => x.CustomerNumber == customerNumber).FirstOrDefault();

            if (customer == null)
                throw new Exception(ExceptionMessages.CustomerWithAccountNotFoundException(customerNumber));

            List<Account> AccountList = new List<Account>();
            var detailList = _accountDetail.Find(x => x.CustomerNumber == customerNumber).ToList();

            if (detailList == null || detailList.Count == 0)
                throw new Exception(ExceptionMessages.AccountForCustomerWasNotFoundException(customerNumber));

            detailList.ForEach(x => AccountList.Add(new Account
            {
                AccountNumber = x.AccountNumber,
                CustomerNumber = x.CustomerNumber,
                CustomerName = customer.Name,
                CustomerSurName = customer.Surname
            }));
            _logger.LogInformation("GetAccountByCustomer Service called succesfully");
            return AccountList;
        }

        public List<AccountDetail> GetAccountDetailByCustomer(string customerNumber)
        {
            var accountDetailList = new List<AccountDetail>();

            var customer = _customer.Find(x => x.CustomerNumber == customerNumber).First();

            if (customer == null)
                throw new Exception(ExceptionMessages.CustomerWithAccountNotFoundException(customerNumber));

            accountDetailList = _accountDetail.Find(x => x.CustomerNumber == customerNumber).ToList();

            if (accountDetailList == null || accountDetailList.Count == 0)
                throw new Exception(ExceptionMessages.AccountForCustomerWasNotFoundException(customerNumber));
            _logger.LogInformation("GetAccountDetailByCustomer Service called succesfully");
            return accountDetailList;
        }
    }
}
