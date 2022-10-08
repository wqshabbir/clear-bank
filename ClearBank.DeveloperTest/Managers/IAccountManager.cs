using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Managers
{
    public interface IAccountManager
    {
        Account GetByAccountNumber(string accountNumber);
        void UpdateAccount(Account account);

        void DeductPayment(Account account, decimal amount);
    }
}
