using ClearBank.DeveloperTest.Data;

namespace ClearBank.DeveloperTest.Factories
{
    public class AccountDataStoreFactory : IAccountDataStoreFactory
    {
        public IAccountDataStore GetAccountDataStore(string accountDataStoretype)
        {
            return accountDataStoretype == Constants.AccountDataStoreTypes.BACK_UP ? new BackupAccountDataStore() : new AccountDataStore();
        }
    }
}
