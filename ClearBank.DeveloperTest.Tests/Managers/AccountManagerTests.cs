using System.Collections.Generic;
using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Factories;
using ClearBank.DeveloperTest.Managers;
using ClearBank.DeveloperTest.Types;
using FizzWare.NBuilder;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.Managers
{
    public class AccountManagerTests
    {
        private readonly Mock<IAccountDataStoreFactory> _mockAccountDataStoreFactory;
        private readonly Mock<IAccountDataStore> _mockAccountDataStore;

        private readonly IAccountManager _accountManager;
        public AccountManagerTests()
        {
            _mockAccountDataStoreFactory = new Mock<IAccountDataStoreFactory>();
            _mockAccountDataStore = new Mock<IAccountDataStore>();

            var inMemorySettings = new Dictionary<string, string> {
                {Constants.Configuration.DATA_STORE_TYPE, Constants.AccountDataStoreTypes.BACK_UP}
            };
            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            _accountManager = new AccountManager(_mockAccountDataStoreFactory.Object, configuration);
        }

        [Fact]
        public void GetByAccountNumber_ValidAccountDataStore_ReturnsValidAccount()
        {
            // Arrange
            string accountNumber = "123456";
            var account = Builder<Account>.CreateNew().With(account => account.AccountNumber = accountNumber).Build();

            _mockAccountDataStoreFactory.Setup(x => x.GetAccountDataStore(Constants.AccountDataStoreTypes.BACK_UP)).Returns(_mockAccountDataStore.Object);
            _mockAccountDataStore.Setup(x => x.GetAccount(accountNumber)).Returns(account);

            // Act
            var actualAccount = _accountManager.GetByAccountNumber(accountNumber);

            // Assert
            _mockAccountDataStoreFactory.Verify(x => x.GetAccountDataStore(Constants.AccountDataStoreTypes.BACK_UP), Times.Once);
            _mockAccountDataStore.Verify(x => x.GetAccount(accountNumber), Times.Once);
            Assert.NotNull(actualAccount);
            Assert.Equal(accountNumber, actualAccount.AccountNumber);
        }

        [Fact]
        public void GetByAccountNumber_MissingAccountDataStore_ReturnsNull()
        {
            // Arrange
            string accountNumber = "Invalid Account Number";
            var account = Builder<Account>.CreateNew().With(account => account.AccountNumber = accountNumber).Build();

            // Act
            var actualAccount = _accountManager.GetByAccountNumber(accountNumber);

            // Assert           
            Assert.Null(actualAccount);
        }

        [Fact]
        public void GetByAccountNumber_ValidAccountDataStoreAndMissingValidAccount_ReturnsNull()
        {
            // Arrange
            string accoutNumber = "123456";
            var account = Builder<Account>.CreateNew().With(account => account.AccountNumber = accoutNumber).Build();

            _mockAccountDataStoreFactory.Setup(x => x.GetAccountDataStore(Constants.AccountDataStoreTypes.BACK_UP)).Returns(_mockAccountDataStore.Object);

            // Act
            var actualAccount = _accountManager.GetByAccountNumber(accoutNumber);

            // Assert
            _mockAccountDataStoreFactory.Verify(x => x.GetAccountDataStore(Constants.AccountDataStoreTypes.BACK_UP), Times.Once);
            _mockAccountDataStore.Verify(x => x.GetAccount(accoutNumber), Times.Once);
            Assert.Null(actualAccount);
        }

        [Fact]
        public void UpdateAccount_ValidAccountDataStoreAndMissingValidAccount_ReturnsNull()
        {
            // Arrange
            string accoutNumber = "123456";
            var account = Builder<Account>.CreateNew().With(account => account.AccountNumber = accoutNumber).Build();

            _mockAccountDataStoreFactory.Setup(x => x.GetAccountDataStore(Constants.AccountDataStoreTypes.BACK_UP)).Returns(_mockAccountDataStore.Object);

            // Act
            var actualAccount = _accountManager.GetByAccountNumber(accoutNumber);

            // Assert
            _mockAccountDataStoreFactory.Verify(x => x.GetAccountDataStore(Constants.AccountDataStoreTypes.BACK_UP), Times.Once);
            _mockAccountDataStore.Verify(x => x.GetAccount(accoutNumber), Times.Once);
            Assert.Null(actualAccount);
        }

        [Fact]
        public void UpdateAccount_ValidDataStore_UpdatesAccount()
        {
            //Arrange
            string accoutNumber = "123456";
            int balance = 2;
            _mockAccountDataStoreFactory.Setup(x => x.GetAccountDataStore(Constants.AccountDataStoreTypes.BACK_UP)).Returns(_mockAccountDataStore.Object);

            var account = Builder<Account>.CreateNew()
                .With(account => account.AccountNumber = accoutNumber)
                .With(account => account.Balance = balance)
                .Build();

            //Act
            _accountManager.UpdateAccount(account);

            //Assert
            _mockAccountDataStoreFactory.Verify(x => x.GetAccountDataStore(Constants.AccountDataStoreTypes.BACK_UP), Times.Once);
            _mockAccountDataStore.Verify(x => x.UpdateAccount(account), Times.Once);
        }

        [Fact]
        public void UpdateAccount_MissingDataStore_DoesntUpdateAccount()
        {
            //Arrange
            string accoutNumber = "123456";
            int balance = 2;

            var account = Builder<Account>.CreateNew()
                .With(account => account.AccountNumber = accoutNumber)
                .With(account => account.Balance = balance)
                .Build();

            //Act
            _accountManager.UpdateAccount(account);

            //Assert
            _mockAccountDataStore.Verify(x => x.UpdateAccount(account), Times.Never);
        }

        [Fact]
        public void DeductPayment_UpdatesAccountRemainingBalance()
        {
            //Arrange
            string accoutNumber = "123456";
            decimal existingBalance = 100.5m;

            var account = Builder<Account>.CreateNew()
                .With(account => account.AccountNumber = accoutNumber)
                .With(account => account.Balance = existingBalance)
                .Build();

            //Act
            decimal deductMoney = 50.5m;
            _accountManager.DeductPayment(account, deductMoney);

            //Assert
            Assert.Equal(existingBalance - deductMoney, account.Balance);
        }
    }
}
