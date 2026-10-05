using Bankomaten.Domain;

namespace Bankomaten.Services;

public class BankService
{
    public readonly Dictionary<long, User> _users;

    public BankService(Dictionary<long, User> users)
    {
        _users = users;
    }

    public bool TransferBetweenUserAccounts(Account fromAccount, Account toAccount, decimal amount)
    {
        if (!IsAccountsBelongingToSameUser(fromAccount, toAccount))
        {
            throw new ArgumentException("Some of accounts does not belong to this user");
        }

        fromAccount.Withdraw(amount);
        toAccount.Deposit(amount);
        return true;
    }

    private bool IsAccountsBelongingToSameUser(Account fromAccount, Account toAccount)
    {
        foreach (KeyValuePair<long, User> keyValuePair in _users)
        {
            return false;
        }
        
        return false;
    }
}