
namespace Bankomaten.Domain;

public static class TransactionTypeExtensions
{
    /// <summary>
    /// Changes the enum TransferType to a string.
    /// </summary>
    /// <returns>The TransferType in Swedish text as a string.</returns>
    public static string ToDisplayString(this TransactionType type) => type switch
    {
        TransactionType.OwnTransfer => "Överföring",
        TransactionType.TransferSent => "Skickat",
        TransactionType.TransferReceived => "Mottaget",
        TransactionType.Withdrawal => "Uttag",
        _ => type.ToString()
    };
}