using System.Text;
using Bankomaten.Domain;
using Bankomaten.Services;

namespace Bankomaten.UI.MenuOptions;

public class TransferMenu(string title, ConsoleInputValidation consoleInputValidation, BankService bankService) : IMenuOptions
{
    
    private const string NoAccountMessage = "You don't have enough accounts to transfer";
    
    private const string AmountQuestion = "How much do you want to transfer?";
    
    private const string SuccessMessage = "Transfer successful!";
    
    private const string UnsuccessMessage = "Transfer unsuccessful!";
    
    private const string FromWithAccountQuestion = "Choose the account to transfer from";
    
    private const string ToWhichAccountQuestion = "Choose the account to transfer to";

    public string Title { get; } = title;

    public void Execute(User user)
    {
        int selectedOption = consoleInputValidation.ReadInteger(BuildMenu(), 1, 2);
        Console.Clear();

        switch (selectedOption)
        {
            case 1:
                TransferBetweenUserAccounts(user);
                break;
            case 2:
                break;
            default:
                break;
        }
    }

    private string BuildMenu()
    {
        return new StringBuilder()
            .AppendLine(Title)
            .AppendLine("1. Transfer to another account")
            .AppendLine("2. Transfer to another user")
            .ToString();
    }

    private void TransferBetweenUserAccounts(User user)
    {
        if (user.Accounts.Count < 2)
        {
            throw new InvalidDataException(NoAccountMessage);
        }
        
        int indexTransferAccountFrom = consoleInputValidation.ReadInteger(
            BuildAccountPrompt(FromWithAccountQuestion, user.Accounts), 1, user.Accounts.Count
        );

        int indexTransferAccountTo = consoleInputValidation.ReadInteger(
            BuildAccountPrompt(ToWhichAccountQuestion, user.Accounts), 1, user.Accounts.Count
        );

        if (indexTransferAccountFrom == indexTransferAccountTo)
        {
            throw new InvalidDataException("You can't chose same account to transfer");
        }
        
        Account fromAccount = user.Accounts[indexTransferAccountFrom - 1];
        Account toAccount = user.Accounts[indexTransferAccountTo - 1];

        decimal amount = consoleInputValidation.ReadDecimal(AmountQuestion, fromAccount.GetBalance());

        bool isTransactionSuccessful = bankService.TransferBetweenUserAccounts(user, fromAccount, toAccount, amount);

        Console.WriteLine(isTransactionSuccessful ? SuccessMessage : UnsuccessMessage);
    }

    private string BuildAccountPrompt(string question, List<Account> accounts)
    {
        var stringBuilder = new StringBuilder().AppendLine(question);
        
        for (int i = 0; i < accounts.Count; i++)
        {
            stringBuilder.AppendLine($"{i + 1}. {accounts[i].Name}  Balance: {accounts[i].GetBalance():C}");
        }
        
        return stringBuilder.ToString();
    }
}