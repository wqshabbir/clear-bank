using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Factories;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.Factories
{
    public class AccountDataStoreFactoryTests
    {
        private readonly IAccountDataStoreFactory _accountDataStoreFactory;
        public AccountDataStoreFactoryTests()
        {
            _accountDataStoreFactory = new AccountDataStoreFactory();
        }

        [Fact]
        public void GetAccountDataStore_WithBackupStoreType_ReturnsBackupAccountDataStore()
        {
            // Act
            var accountDataStore = _accountDataStoreFactory.GetAccountDataStore(Constants.AccountDataStoreTypes.BACK_UP);

            // Assert
            Assert.IsType<BackupAccountDataStore>(accountDataStore);
        }

        [Fact]
        public void GetAccountDataStore_WithRandomStoreType_ReturnsDefaultAccountDataStore()
        {
            string dataStoreType = "Random";
            // Act
            var accountDataStore = _accountDataStoreFactory.GetAccountDataStore(dataStoreType);

            // Assert
            Assert.IsType<AccountDataStore>(accountDataStore);
        }
    }
}
