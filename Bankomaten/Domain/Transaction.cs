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
    private DateTime DateTime { get; set; }

    /// <summary>
    /// Gets the type of transaction that was performed.
    /// </summary>
    private TransactionType TransactionType { get; set; }
    
    /// <summary>
    /// Gets the amount of money involved in the transaction.
    /// </summary>
    private decimal Amount { get; set; }
    
    /// <summary>
    /// Gets the name of the account from which the transaction originated.
    /// </summary>
    private string FromAccountName { get; set; }
    
    /// <summary>
    /// Gets the name of the receiving account, user, or counterparty involved in the transaction.
    /// </summary>
    private string CounterpartyName { get; set; }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="Transaction"/> class.
    /// </summary>
    public Transaction(DateTime dateTime, TransactionType transactionType,
        decimal amount, string fromAccount, string counterpartyName)
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
            .Append(TransactionType.ToDisplayString())
            .Append(" Från: " + FromAccountName +" |")
            .Append(" Till: " + CounterpartyName +" |")
            .Append($" Belopp: {Amount:C} |")
            .Append(" Datum: " + DateTime)
            .ToString();
    }
}