namespace Bankomaten.Domain;

/// <summary>
/// Represents the type of financial transaction within the system.
/// </summary>
public enum TransactionType
{
    OwnTransfer,
    TransferSent,
    TransferReceived,
    Withdrawal,
}