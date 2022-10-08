using ClearBank.DeveloperTest.Factories;
using ClearBank.DeveloperTest.Types;
using Microsoft.Extensions.Configuration;

namespace ClearBank.DeveloperTest.Managers
{
    public class AccountManager : IAccountManager
    {
        private readonly IAccountDataStoreFactory _accountDataStoreFactory;
        private readonly string _dataStoreType;

        public AccountManager(IAccountDataStoreFactory accountDataStoreFactory, IConfiguration configuration)
        {
            _accountDataStoreFactory = accountDataStoreFactory;
            _dataStoreType = configuration[Constants.Configuration.DATA_STORE_TYPE];
        }

        public Account GetByAccountNumber(string accountNumber)
        {
            var accountDataStore = _accountDataStoreFactory.GetAccountDataStore(_dataStoreType);
            return accountDataStore?.GetAccount(accountNumber);
        }

        public void UpdateAccount(Account account)
        {
            var dataStore = _accountDataStoreFactory.GetAccountDataStore(_dataStoreType);
            dataStore?.UpdateAccount(account);
        }

        public void DeductPayment(Account account, decimal amount)
        {
            account.Balance -= amount;
        }
    }
}
