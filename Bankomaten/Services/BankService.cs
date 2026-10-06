using Bankomaten.Domain;
using Bankomaten.Data;

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
        
        
        return true;
    }

    private bool IsAccountsBelongingToSameUser(Account fromAccount, Account toAccount)
    {
        return true;
    }
}