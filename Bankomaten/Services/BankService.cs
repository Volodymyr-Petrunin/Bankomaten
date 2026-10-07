using Bankomaten.Domain;

namespace Bankomaten.Services;

public class BankService
{
    public bool TransferBetweenUserAccounts(User user, Account fromAccount, Account toAccount, decimal amount)
    {
        if (!user.Accounts.Contains(fromAccount) || !user.Accounts.Contains(toAccount))
            throw new ArgumentException("One or both accounts do not belong to this user.");

        if (fromAccount == toAccount)
            throw new ArgumentException("Cannot transfer to the same account.");

        if (amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be positive.");

        if (!fromAccount.Withdraw(amount))
            return false;

        if (!toAccount.Deposit(amount))
        {
            fromAccount.Deposit(amount);
            return false;
        }

        return true;
    }
    
    /// <summary>
    /// Moves money from one of the sender's accounts to the recipient's main account (their first account).
    /// </summary>
    /// <returns>True if the transfer was successful, false otherwise.</returns>
    public bool TransferBetweenUsers(User sender, Account fromAccount, User recipient, decimal amount)
    {
        if (!sender.Accounts.Contains(fromAccount))
            throw new ArgumentException("The account does not belong to the sender.");

        if (sender == recipient)
            throw new ArgumentException("Use TransferBetweenUserAccounts for the user's own accounts.");

        if (amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be positive.");
        
        Account toAccount = recipient.Accounts[0];

        if (!fromAccount.Withdraw(amount))
            return false;

        toAccount.Deposit(amount);
        return true;
    }
}