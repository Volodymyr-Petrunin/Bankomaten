using System.Diagnostics;
using System.Text;
using Bankomaten.Domain;
using Bankomaten.Services;
using Bankomaten.UI.Exceptions;

namespace Bankomaten.UI.MenuOptions;

public class TransferMenu(string title, ConsoleInputValidation consoleInputValidation,
    BankService bankService, UserManagement userManagement) : IMenuOptions
{
    private const string FromAccountQuestion = "Välj konto att föra över från: ";

    private const string ToAccountQuestion = "Välj konto att föra över till: ";

    private const string AmountQuestion = "Hur mycket vill du föra över: ";

    private const string RecipientQuestion = "Ange mottagarens användarnamn: ";
    
    private const string PinCodeQuestion = "Ange PIN-kod: ";
    
    private const string NotEnoughAccountsMessage = "Du behöver minst två konton för att föra över mellan egna konton.";
    
    private const string EmptyAccountMessage = "Kontot är tomt. Välj ett konto med pengar.";

    private const string RecipientNotFoundMessage = "Det finns ingen användare med det namnet.";
    
    private const string SelfTransferMessage = "Du kan inte skicka pengar till dig själv. Välj överföring mellan egna konton.";
    
    private const string SuccessMessage = "Överföringen är klar.";
    
    private const string FailedMessage = "Överföringen kunde inte genomföras.";
    
    public string Title { get; } = title;

    /// <summary>
    /// Executes the transfer menu for the specified user. Prompts the user to select an option
    /// for transferring funds between their own accounts or to another user, or to exit the menu.
    /// </summary>
    /// <exception cref="UnreachableException">Thrown when an unexpected menu option is selected.</exception>
    public void Execute(User user)
    {
        int selectedOption = consoleInputValidation.ReadInteger(BuildMenu(), 1, 3);
        Console.Clear();

        switch (selectedOption)
        {
            case 1:
                TransferBetweenUserAccounts(user);
                break;
            case 2:
                TransferBetweenUsers(user);
                break;
            case 3:
                break;
            default:
                throw new UnreachableException($"Unexpected menu option: {selectedOption}");
        }
    }

    /// <summary>Builds and returns a formatted string representing a transfer menu with options.</summary>
    /// <returns>A string containing the transfer menu options, formatted for display.</returns>
    private string BuildMenu()
    {
        return new StringBuilder()
            .AppendLine(Title)
            .AppendLine("1. Mellan egna konton")
            .AppendLine("2. Till en annan användare")
            .AppendLine("3. Tillbaka till huvudmenyn")
            .ToString();
    }

    /// <summary>
    /// Facilitates the process of transferring funds between two accounts owned by the same user.
    /// Validates that the user has at least two accounts, allows the user to select source and target accounts,
    /// and ensures a successful transfer in accordance with system constraints.
    /// </summary>
    /// <exception cref="UserInputException">
    /// Thrown if the user does not have at least two accounts required to perform the transfer.
    /// </exception>
    private void TransferBetweenUserAccounts(User user)
    {
        if (user.Accounts.Count < 2)
        {
            throw new UserInputException(NotEnoughAccountsMessage);
        }
        
        Account fromAccount = ChooseSourceAccount(user);

        // The source account is left out, so the same account can never be chosen twice
        List<Account> targetAccounts = user.Accounts.Where(account => account != fromAccount).ToList();
        Account toAccount = ChooseAccount(ToAccountQuestion, targetAccounts);

        decimal amount = consoleInputValidation.ReadDecimal(AmountQuestion, fromAccount.GetBalance());

        if (!VerifyPinCode(user))
            throw new UserInputException("Felaktig PIN-kod.");

        if (!bankService.TransferBetweenUserAccounts(user, fromAccount, toAccount, amount))
        {
            Console.WriteLine(FailedMessage);
            return;
        }
        
        Console.WriteLine(SuccessMessage);
        PrintBalance(fromAccount);
        PrintBalance(toAccount);
    }

    /// <summary>
    /// Handles the process of transferring funds from the sender's account to a recipient user's account.
    /// Validates recipient existence, ensures the transfer complies with business rules, and updates the accounts involved.
    /// </summary>
    /// <exception cref="UserInputException">
    /// Thrown if the recipient user is not found, or if the sender attempts to transfer funds to themselves.
    /// </exception>
    private void TransferBetweenUsers(User sender)
    {
        // The recipient is checked first, so the user does not choose an account for nothing
        string username = consoleInputValidation.ReadText(RecipientQuestion);
        User recipient = userManagement.FindUser(username) ?? throw new UserInputException(RecipientNotFoundMessage);

        if (recipient == sender)
        {
            throw new UserInputException(SelfTransferMessage);
        }

        Account fromAccount = ChooseSourceAccount(sender);
        decimal amount = consoleInputValidation.ReadDecimal(AmountQuestion, fromAccount.GetBalance());
        
        if (!VerifyPinCode(sender))
            throw new UserInputException("Felaktig PIN-kod.");

        if (!bankService.TransferBetweenUsers(sender, fromAccount, recipient, amount))
        {
            Console.WriteLine(FailedMessage);
            return;
        }

        // Only the sender's own balance is shown, never the recipient's accounts
        Console.WriteLine($"{SuccessMessage} {amount:C} har skickats till {recipient.UserName}.");
        PrintBalance(fromAccount);
    }


    /// <summary>
    /// Lets the user choose an account to take money from. Stops if the chosen account is empty.
    /// </summary>
    /// <returns>The account chosen by the user.</returns>
    /// <exception cref="UserInputException">Thrown if the user chooses an empty account.</exception>
    private Account ChooseSourceAccount(User user)
    {
        Account account = ChooseAccount(FromAccountQuestion, user.Accounts);

        // ReadDecimal requires max >= 0.01, so an empty account must be stopped before asking for an amount
        if (account.GetBalance() < 0.01m)
        {
            throw new UserInputException(EmptyAccountMessage);
        }

        return account;
    }

    /// <summary>
    /// Prompts the user to select an account from a list of available accounts.
    /// Ensures that the user inputs a valid selection within the given range.
    /// </summary>
    /// <returns>The account selected by the user.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if the provided list of accounts is empty or the user's selection is out of range.
    /// </exception>
    private Account ChooseAccount(string question, List<Account> accounts)
    {
        int choice = consoleInputValidation.ReadInteger(
            BuildAccountPrompt(question, accounts), 1, accounts.Count
        );
        
        return accounts[choice - 1];
    }

    /// <summary>Displays the current balance of the specified account in a formatted manner.</summary>
    private void PrintBalance(Account account)
    {
        Console.WriteLine($"Nytt saldo på {account.Name}: {account.GetBalance():C}");
    }

    /// <summary>
    /// Constructs a formatted prompt string displaying a question and a list of accounts.
    /// Each account is displayed with an index, its name, and the current balance.
    /// </summary>
    /// <returns>A formatted string containing the prompt and account options.</returns>
    private string BuildAccountPrompt(string question, List<Account> accounts)
    {
        var stringBuilder = new StringBuilder().AppendLine(question);
        
        for (int i = 0; i < accounts.Count; i++)
        {
            stringBuilder.AppendLine($"{i + 1}. {accounts[i].Name}  Balance: {accounts[i].GetBalance():C}");
        }
        
        return stringBuilder.ToString();
    }

    private bool VerifyPinCode(User user)
    {
        return bankService.VerifyPassword(user, consoleInputValidation.ReadText(PinCodeQuestion));
    }
}