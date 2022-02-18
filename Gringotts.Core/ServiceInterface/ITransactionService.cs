using Gringotts.Core.Model;
using System;
using System.Collections.Generic;

namespace Gringotts.Core.ServiceInterface
{
    public interface ITransactionService
    {
        Transaction AddTransaction(Transaction transaction);
        List<Transaction> GetTransactionsByAccount(string accountId);
        List<Transaction> GetTransactionsByDate(string customerNumber, DateTime fromDate, DateTime toDate);
    }
}
