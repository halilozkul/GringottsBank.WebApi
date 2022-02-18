using Gringotts.Core.Model;
using System.Collections.Generic;

namespace Gringotts.Core.ServiceInterface
{
    public interface ICustomerService
    {
        List<Customer> GetCustomers();
        Customer AddCustomer(Customer customer);
        Customer GetCustomerById(string id);
        string DeleteCustomer(string id);
    }
}
