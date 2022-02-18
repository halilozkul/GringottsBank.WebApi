using Gringotts.Core.Model;
using Gringotts.Core.ServiceInterface;
using GringottsBank.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Xunit;

namespace Gringotts.Test
{
    public class TransactionControllerTests
    {
        private readonly Mock<ITransactionService> mockTransactionService;
        private readonly TransactionController transactionController;

        public TransactionControllerTests()
        {
            mockTransactionService = new Mock<ITransactionService>();
            transactionController = new TransactionController(mockTransactionService.Object, null);
        }

        [Fact]
        public void Add_WhenCalled_Adds_Transaction_Updates_Balance_Of_Account_Returns_Transaction()
        {
            var mockTransaction = new Transaction()
            {
                AccountNumber = "620d65eb2b34f6346406a6d5",
                TransactionAmount = 25.5M,
                TransactionDateTime = DateTime.Now,
                TransactionId = "620f6586604f0aedae13a7c4"
            };

            mockTransactionService.Setup(mock => mock.AddTransaction(mockTransaction)).Returns(mockTransaction);
            var result = transactionController.AddTransaction(mockTransaction) as OkObjectResult;
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Get_WhenCalled_Lists_Transaction_Of_Account_Returns_TransactionList()
        {
            var mockAccountNo = "620d65eb2b34f6346406a6d5";
            var mockTransactionList = new List<Transaction>()
            {
                new Transaction
                {
                    AccountNumber = "620d65eb2b34f6346406a6d5",
                    TransactionAmount = 25.5M,
                    TransactionDateTime = DateTime.Now,
                    TransactionId = "620f6586604f0aedae13a7c4"
                },
                new Transaction
                {
                    AccountNumber = "620d65eb2b34f6346406a6d5",
                    TransactionAmount = 52.26M,
                    TransactionDateTime = DateTime.Now,
                    TransactionId = "620f6586604f0aedae13a7d6"
                },
                new Transaction
                {
                    AccountNumber = "620d65eb2b34f6346406a6c3",
                    TransactionAmount = 52.26M,
                    TransactionDateTime = DateTime.Now,
                    TransactionId = "620f6586604f0aedae13a7d6"
                }
            };
            mockTransactionService.Setup(mock => mock.GetTransactionsByAccount(mockAccountNo)).Returns(mockTransactionList);
            var result = transactionController.GetTransactionsByAccount(mockAccountNo) as OkObjectResult;
            var transactionResp = Assert.IsType<List<Transaction>>(((BaseResponse<List<Transaction>>)(result.Value)).ResultObj);
            var transactions = transactionResp.Where(x => x.AccountNumber == mockAccountNo).ToList();
            Assert.Equal(2, transactions.Count());
        }

        [Fact]
        public void Get_WhenCalled_Lists_Transactions_Of_Account_Between_Dates_Returns_TransactionList()
        {
            var mockAccountNo = "620d65eb2b34f6346406a6d5";
            DateTime fromDate = DateTime.Now.AddDays(-1);
            DateTime toDate = DateTime.Now.AddDays(1);

            var mockTransactionList = new List<Transaction>()
            {
                new Transaction
                {
                    AccountNumber = "620d65eb2b34f6346406a6d5",
                    TransactionAmount = 25.5M,
                    TransactionDateTime = DateTime.Now,
                    TransactionId = "620f6586604f0aedae13a7c4"
                },
                new Transaction
                {
                    AccountNumber = "620d65eb2b34f6346406a6d5",
                    TransactionAmount = 52.26M,
                    TransactionDateTime = DateTime.Now.AddDays(2),
                    TransactionId = "620f6586604f0aedae13a7d6"
                },
                new Transaction
                {
                    AccountNumber = "620d65eb2b34f6346406a6c3",
                    TransactionAmount = 52.26M,
                    TransactionDateTime = DateTime.Now,
                    TransactionId = "620f6586604f0aedae13a7d6"
                }
            };
            mockTransactionService.Setup(mock => mock.GetTransactionsByDate(mockAccountNo, fromDate, toDate)).Returns(mockTransactionList);
            var result = transactionController.GetTransactionsByDate(mockAccountNo, fromDate, toDate) as OkObjectResult;
            var transactionResp = Assert.IsType<List<Transaction>>(((BaseResponse<List<Transaction>>)(result.Value)).ResultObj);
            var transactions = transactionResp.Where(x => x.AccountNumber == mockAccountNo &&
                                                     x.TransactionDateTime <= toDate &&
                                                     x.TransactionDateTime >= fromDate).ToList();
            Assert.Equal(1, transactions.Count());
        }
    }
}
