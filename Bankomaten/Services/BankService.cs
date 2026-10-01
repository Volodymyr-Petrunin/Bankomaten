using Bankomaten.Domain;

namespace Bankomaten.Services;

public class BankService
{
    private readonly Dictionary<long, User> _users;

    public BankService(Dictionary<long, User> users)
    {
        _users = users;
    }

    public bool TransferBetweenUserAccounts(Account fromAccount, Account toAccount, decimal amount)
    {
        foreach (var user in _users)
        {
            Console.WriteLine(user.Key + " " + user.Value);
            user.Value.Accounts.ForEach(Console.WriteLine);
        }
        
        return true;
    }

    private bool IsAccountsBelongingToSameUser(Account fromAccount, Account toAccount)
    {
        return false;
    }
}