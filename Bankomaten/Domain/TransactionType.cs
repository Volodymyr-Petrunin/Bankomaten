namespace Bankomaten.Domain;

// TODO Ask Anas about how to handle enum values name as good looks strings

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