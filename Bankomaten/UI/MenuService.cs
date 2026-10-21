using System.Text;
using Bankomaten.Domain;

namespace Bankomaten.UI;

internal class MenuService(ConsoleInputValidation consoleInputValidation)
{
    private const string ReturnToMenuMessage = "Tryck på Enter för att återgå till huvudmenyn";

    /// <summary>
    /// Prompts the user to select an account from a list of available accounts.
    /// Ensures that the user inputs a valid selection within the given range.
    /// </summary>
    /// <returns>The account selected by the user, returns null if no account is selected.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if the provided list of accounts is empty or the user's selection is out of range.
    /// </exception>
    public Account? ChooseAccount(string question, List<Account> accounts)
    {
        // Add a "Go Back" option the end of the printed list of accounts
        int goBackOption = accounts.Count + 1;

        int choice = consoleInputValidation.ReadInteger(
            BuildAccountPrompt(question, accounts), 1, goBackOption, "Ogiltigt val. Välj ett av alternativen i menyn."
        );

        if(choice == goBackOption)
        {
            // User chose to go back
            return null; 
        }

        return accounts[choice - 1];
    }

    /// <summary> Change Console color to green and print success message than reset color</summary>
    public void WriteSuccessMessage(string message, Account account)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(message);
        Console.ResetColor();
        
        PrintBalance(account);
        
        consoleInputValidation.WaitForEnter(ReturnToMenuMessage);
    }

    /// <summary>
    /// Displays a success message and updates the balances of the involved accounts.
    /// Highlights the message in green for better visibility and waits for user confirmation before returning to the main menu.
    /// </summary>
    /// <param name="message">The success message to display.</param>
    /// <param name="fromAccount">The account from which the transfer originated.</param>
    /// <param name="toAccount">The account to which the transfer was made.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown if either <paramref name="fromAccount"/> or <paramref name="toAccount"/> is null.
    /// </exception>
    public void WriteSuccessMessage(string message, Account fromAccount, Account toAccount)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(message);
        Console.ResetColor();
        
        PrintBalance(fromAccount);
        PrintBalance(toAccount);

        
        consoleInputValidation.WaitForEnter(ReturnToMenuMessage);
    }

    /// <summary>Displays the current balance of the specified account in a formatted manner.</summary>
    private void PrintBalance(Account account)
    {
        Console.WriteLine($"Nytt saldo på {account.Name}: {account.GetBalance():C}");
    }

    /// <summary>Returns a centered and bordered title string for the console menu.</summary>
    public string CenterTitle(string title)
    {
        int width = Console.WindowWidth;
        string nl = Environment.NewLine;
        string border = new string('=', width);
            
        if (title.Length >= width)
        {
            return border + nl + title + nl + border; // If the title is longer than the width, return it as is.
        }
            
        int leftSpacers = (width - title.Length) / 2;
        int rightSpacers = width - title.Length - leftSpacers;
            
        return border + nl + new string(' ', leftSpacers) + title + new string(' ', rightSpacers) + nl + border + nl;
    }

    /// <summary>
    /// Constructs a formatted prompt string displaying a question and a list of accounts.
    /// Each account is displayed with an index, its name, and the current balance.
    /// </summary>
    /// <returns>A formatted string containing the prompt and account options.</returns>
    private string BuildAccountPrompt(string question, List<Account> accounts)
    {
        var stringBuilder = new StringBuilder().AppendLine(question);

        stringBuilder.AppendLine(); // Empty line for better readability

        // Suggestion rename i to index
        for (int index = 0; index < accounts.Count; index++)
        {
            stringBuilder.AppendLine($"{index + 1}. {accounts[index].Name}  Saldo: {accounts[index].GetBalance():C}");
        }

        stringBuilder.AppendLine(); // Empty line for better readability
        stringBuilder.AppendLine($"{accounts.Count + 1}. Tillbaka till huvudmenyn");
        stringBuilder.AppendLine();
        stringBuilder.Append("Ange ditt val: ");
        
        return stringBuilder.ToString();
    }
}