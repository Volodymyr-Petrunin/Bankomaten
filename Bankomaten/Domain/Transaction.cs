using System.Text;

namespace Bankomaten.Domain;

/// <summary>
/// Represents a financial transaction within the system.
/// </summary>
public class Transaction
{
    /// <summary>
    /// Gets the date and time when the transaction occurred.
    /// </summary>
    public DateTime DateTime { get; private set; }

    /// <summary>
    /// Gets the type of transaction that was performed.
    /// </summary>
    public TransactionType TransactionType { get; private set; }
    
    /// <summary>
    /// Gets the amount of money involved in the transaction.
    /// </summary>
    public decimal Amount { get; private set; }
    
    /// <summary>
    /// Gets the name of the account from which the transaction originated.
    /// </summary>
    public String FromAccountName { get; private set; }
    
    /// <summary>
    /// Gets the name of the receiving account, user, or counterparty involved in the transaction.
    /// </summary>
    public String CounterpartyName { get; private set; }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="Transaction"/> class.
    /// </summary>
    public Transaction(DateTime dateTime, TransactionType transactionType,
        decimal amount, String fromAccount, String counterpartyName)
    {
        DateTime = dateTime;
        TransactionType = transactionType;
        Amount = amount;
        FromAccountName = fromAccount;
        CounterpartyName = counterpartyName;
    }

    /// <summary>
    /// Returns a string representation of the transaction.
    /// </summary>
    /// <returns>A formatted string containing the transaction type, amount, source, counterparty, and date.</returns>
    public override string ToString()
    {
        return new StringBuilder()
            .Append("Type: " + TransactionType)
            .Append($" Amount: {Amount:C}")
            .Append(" From: " + FromAccountName)
            .Append(" To: " + CounterpartyName)
            .Append(" Date: " + DateTime)
            .ToString();
    }
}