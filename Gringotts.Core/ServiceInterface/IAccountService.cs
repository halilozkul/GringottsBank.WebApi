using Gringotts.Core.Model;
using System.Collections.Generic;

namespace Gringotts.Core.ServiceInterface
{
    public interface IAccountService
    {
        AccountDetail AddAccount(AccountDetail accountDetail);
        List<Account> GetAccountByCustomer(string customerNumber);
        List<AccountDetail> GetAccountDetailByCustomer(string customerNumber); 
    }
}
