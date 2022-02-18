namespace Gringotts.Core.Exceptions
{
    public static class ExceptionMessages
    {
        private const string customerNotFoundException = "Customer with id : {0} was not found.";
        private const string customerWithNotFoundException = "Customer with account number : {0} was not found.";
        private const string noAccountWasFountForCustomer = "There is no account for customer number : {0}";
        public const string noCustomerFoundInCollection = "There is no account for customer number : {0}";
        public const string noAccountFoundForTransaction = "There is no account with accountNumber : {0} for transaction";
        public const string accountNotFound = "There is no account with accountNumber : {0}";
        public const string noTransactionFound = "There is no transaction for accountNumber : {0}";

        public static string CustomerNotFoundException(string custId)
        {
            return string.Format(customerNotFoundException, custId);
        }
        public static string CustomerWithAccountNotFoundException(string custId)
        {
            return string.Format(customerWithNotFoundException, custId);
        }
        public static string AccountForCustomerWasNotFoundException(string custId)
        {
            return string.Format(noAccountWasFountForCustomer, custId);
        }
        public static string NoAccountForFoundForTransactionException(string accountNo)
        {
            return string.Format(noAccountFoundForTransaction, accountNo);
        }
        public static string AccountNotFoundException(string accountNo)
        {
            return string.Format(accountNotFound, accountNo);
        }
        public static string NoTransactionWasFoundException(string accountNo)
        {
            return string.Format(noTransactionFound, accountNo);
        }
    }
}
