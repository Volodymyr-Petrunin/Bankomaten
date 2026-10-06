using Bankomaten.Domain;

namespace Bankomaten.Services;

public class BankService
{
    public readonly Dictionary<long, User> _users;

    public BankService(Dictionary<long, User> users)
    {
        _users = users;
    }

    public bool TransferBetweenUserAccounts(User user, Account fromAccount, Account toAccount, decimal amount)
    {
        if (!user.Accounts.Contains(fromAccount) || !user.Accounts.Contains(toAccount))
        {
            throw new ArgumentException("One or both accounts do not belong to this user.");
        }

        if (fromAccount == toAccount)
        {
            throw new ArgumentException("Cannot transfer to the same account.");
        }

        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be positive.");
        }

        if (!fromAccount.Withdraw(amount))
        {
            return false;
        }

        if (!toAccount.Deposit(amount))
        {
            fromAccount.Deposit(amount);
            return false;
        }

        return true;

    }
}