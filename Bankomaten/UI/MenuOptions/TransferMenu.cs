using System.Text;
using Bankomaten.Domain;
using Bankomaten.Services;

namespace Bankomaten.UI.MenuOptions;

public class TransferMenu(string title, ConsoleInputValidation consoleInputValidation, BankService bankService) : IMenuOptions
{
    public string Title { get; } = title;

    public void Execute(User user)
    {
        int selectedOption = consoleInputValidation.ReadInteger(BuildMenu(), 1, 2);
        Console.Clear();

        if (selectedOption == 1)
        {
            string choseTransferAccountFrom = ChoseTransferAccountFrom(user);
            Console.WriteLine(choseTransferAccountFrom);
        }
    }

    private string BuildMenu()
    {
        return new StringBuilder()
            .AppendLine("Transfer between accounts")
            .AppendLine("1. Transfer to another account")
            .AppendLine("2. Transfer to another user")
            .ToString();
    }
    
    private string ChoseTransferAccountFrom(User user)
    {
        if (user.Accounts.Count <= 1)
        {
            return "You don't have enough accounts to transfer";
        }
        
        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("Chose from with account you wanna do transfer");
        stringBuilder.AppendLine(AllUserAccountsAsString(user.Accounts));
        
        return stringBuilder.ToString();
    }

    private string AllUserAccountsAsString(List<Account> accounts)
    {
        var stringBuilder = new StringBuilder();

        byte index = 1;
        foreach (Account account in accounts)
        {
            stringBuilder.AppendLine(index + ". " + account.Name + " Balance: " + account.GetBalance());
            index++;
        }
        
        return stringBuilder.ToString();
    }
}