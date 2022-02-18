using Gringotts.Core.DbUtil.Interface;
using Gringotts.Core.Exceptions;
using Gringotts.Core.Model;
using Gringotts.Core.ServiceInterface;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace Gringotts.Core.Service
{
    public class TransactionService : ITransactionService
    {
        public IMongoCollection<AccountDetail> _accountDetail { get; set; }
        public IMongoCollection<Transaction> _transaction { get; set; }
        public IMongoCollection<Customer> _customer { get; set; }

        private ILogger<TransactionService> _logger;
        public TransactionService(IGringottsDbSettings settings, ILogger<TransactionService> logger)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _accountDetail = database.GetCollection<AccountDetail>(settings.AccountCollection);
            _transaction = database.GetCollection<Transaction>(settings.TransactionCollection);
            _customer = database.GetCollection<Customer>(settings.CustomerCollection);
            _logger = logger;
        }

        public Transaction AddTransaction(Transaction transaction)
        {
            var accountDetailList = new List<AccountDetail>();
            accountDetailList = _accountDetail.Find(x => x.AccountNumber == transaction.AccountNumber).ToList();
            if (accountDetailList == null || accountDetailList.Count == 0)
                throw new Exception(ExceptionMessages.NoAccountForFoundForTransactionException(transaction.AccountNumber));
            transaction.TransactionDateTime = DateTime.Now;
            _transaction.InsertOne(transaction);
            accountDetailList[0].Balance += transaction.TransactionAmount;
            accountDetailList[0].UpdateDateTime = DateTime.Now;
            _accountDetail.ReplaceOne(x => x.AccountNumber == transaction.AccountNumber, accountDetailList[0]);
            _logger.LogInformation("AddTransaction Service Called Succesfully");
            return transaction;
        }

        public List<Transaction> GetTransactionsByAccount(string accountId)
        {
            var transactions = new List<Transaction>();
            var account = _accountDetail.Find(x => x.AccountNumber == accountId).FirstOrDefault();
            if (account == null)
                throw new Exception(ExceptionMessages.AccountNotFoundException(accountId));
            transactions = _transaction.Find(t => t.AccountNumber == account.AccountNumber).ToList().OrderByDescending(x => x.TransactionDateTime).ToList();
            _logger.LogInformation("GetTransactionsByAccount Service called succesfully");
            return transactions;
        }

        public List<Transaction> GetTransactionsByDate(string customerNo, DateTime fromDate, DateTime toDate)
        {
            var transactions = new List<Transaction>();
            var customer = _customer.Find(x => x.CustomerNumber == customerNo).FirstOrDefault();
            if (customer == null)
                throw new Exception(ExceptionMessages.CustomerNotFoundException(customerNo));
            var accounts = _accountDetail.Find(x => x.CustomerNumber == customer.CustomerNumber).ToList();
            if (accounts.Count == 0)
                throw new Exception(ExceptionMessages.AccountForCustomerWasNotFoundException(customerNo));
            foreach (var account in accounts)
            {
                transactions.AddRange(_transaction.Find(
                    t => t.AccountNumber == account.AccountNumber &&
                    t.TransactionDateTime <= toDate &&
                    t.TransactionDateTime >= fromDate)
                        .ToList()
                        .OrderByDescending(x => x.TransactionDateTime)
                        .ToList());
            }
            _logger.LogInformation("GetTransactionsByDate Service called succesfully");
            return transactions;
        }
    }
}
