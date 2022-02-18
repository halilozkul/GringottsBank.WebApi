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
    public class AccountControllerTests
    {
        private readonly Mock<IAccountService> mockAccountService;
        private readonly AccountController accountController;

        public AccountControllerTests()
        {
            mockAccountService = new Mock<IAccountService>();
            accountController = new AccountController(mockAccountService.Object, null);
        }
        [Fact]
        public void Add_WhenCalled_Adds_Account_Returns_AccountDetail()
        {
            var mockAccount = new AccountDetail()
            {
                AccountNumber = "620d65eb2b34f6346406a6d2",
                Balance = 125.15M,
                CustomerNumber = "620d65a32b34f6346406a6d0",
                InsertDateTime = DateTime.Now,
                UpdateDateTime = DateTime.MinValue
            };
            mockAccountService.Setup(mock => mock.AddAccount(mockAccount)).Returns(mockAccount);
            var result = accountController.AddAccount(mockAccount) as OkObjectResult;
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Get_WhenCalledWithCustomerNo_Returns_CustomerList()
        {
            var mockCustomerNo = "620d65a32b34f6346406a6d0";
            var mockAccount = new List<Account>()
            {
                new Account()
                {
                    AccountNumber = "620d65eb2b34f6346406a6d2",
                    CustomerNumber = "620d65a32b34f6346406a6d0",
                    CustomerName = "Halil",
                    CustomerSurName = "Ozkul"
                },
                new Account()
                {
                    AccountNumber = "620d65eb2b34f6346406a6d5",
                    CustomerNumber = "620d65a32b34f6346406a6d0",
                    CustomerName = "Halil",
                    CustomerSurName = "Ozkul"
                }
            };
            mockAccountService.Setup(mock => mock.GetAccountByCustomer(mockCustomerNo)).Returns(mockAccount);
            var result = accountController.GetAccountByCustomer(mockCustomerNo) as OkObjectResult;
            var accountResp = Assert.IsType<List<Account>>(((BaseResponse<List<Account>>)(result.Value)).ResultObj);
            var accounts = accountResp.Where(x => x.CustomerNumber == mockCustomerNo).ToList();
            Assert.Equal(2, accounts.Count());
        }

        [Fact]
        public void Get_WhenCalledWithCustomerNo_Returns_CustomerDetailList()
        {
            var mockCustomerNo = "620d65a32b34f6346406a6d0";
            var mockAccount = new List<AccountDetail>()
            {
                new AccountDetail()
                {
                    AccountNumber = "620d65eb2b34f6346406a6d2",
                    Balance = 125.15M,
                    CustomerNumber = "620d65a32b34f6346406a6d0",
                    InsertDateTime = DateTime.Now,
                    UpdateDateTime = DateTime.MinValue
                },
                new AccountDetail()
                {
                    AccountNumber = "620d65eb2b34f6346406a6d5",
                    Balance = 6785.1M,
                    CustomerNumber = "620d65a32b34f6346406a6d0",
                    InsertDateTime = DateTime.Now,
                    UpdateDateTime = DateTime.MinValue
                }
            };
            mockAccountService.Setup(mock => mock.GetAccountDetailByCustomer(mockCustomerNo)).Returns(mockAccount);
            var result = accountController.GetAccountDetailByCustomer(mockCustomerNo) as OkObjectResult;
            var accountResp = Assert.IsType<List<AccountDetail>>(((BaseResponse<List<AccountDetail>>)(result.Value)).ResultObj);
            var accounts = accountResp.Where(x => x.CustomerNumber == mockCustomerNo).ToList();
            Assert.Equal(2, accounts.Count());
        }
    }
}
