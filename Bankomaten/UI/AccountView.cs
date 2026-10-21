using System.Text;
using Bankomaten.Domain;

namespace Bankomaten.UI;

internal class AccountView(ConsoleInputValidation consoleInputValidation)
{
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

    /// <summary>Displays the current balance of the specified account in a formatted manner.</summary>
    public void PrintBalance(Account account)
    {
        Console.WriteLine($"Nytt saldo på {account.Name}: {account.GetBalance():C}");
    }

    /// <summary> Change Console color to green and print success message than reset color</summary>
    public void WriteSuccessMessage(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(message);
        Console.ResetColor();
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

        for (int i = 0; i < accounts.Count; i++)
        {
            stringBuilder.AppendLine($"{i + 1}. {accounts[i].Name}  Saldo: {accounts[i].GetBalance():C}");
        }

        stringBuilder.AppendLine(); // Empty line for better readability
        stringBuilder.AppendLine($"{accounts.Count + 1}. Tillbaka till huvudmenyn");
        stringBuilder.AppendLine();
        stringBuilder.Append("Ange ditt val: ");
        
        return stringBuilder.ToString();
    }
}