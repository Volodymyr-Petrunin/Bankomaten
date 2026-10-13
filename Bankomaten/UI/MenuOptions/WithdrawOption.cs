using System;
using Bankomaten.Domain;
using Bankomaten.Services;

namespace Bankomaten.UI.MenuOptions;

/// <summary>
/// Withdraw option class that implements IMenuOptions interface
/// </summary>
public class WithdrawOption : IMenuOptions
{
    private ConsoleInputValidation consoleInput;
    private UserManagement userManagement;

    public string Title => "Ta ut pengar";


    /// <summary>
    /// Creates the withdraw option
    /// </summary>
    public WithdrawOption(ConsoleInputValidation consoleInput, UserManagement userManagement)
    {
        this.consoleInput = consoleInput;
        this.userManagement = userManagement;
    }

    /// <summary>
    /// Withdraw process logic for the user
    /// </summary>
    public void Execute(User user)
    {
        Console.WriteLine("Välj konto:");

        //shows all the accounts of the user and the balance of each account
        for (int i = 0; i < user.Accounts.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {user.Accounts[i].Name}: {user.Accounts[i].GetBalance():N2} kr");
        }

        //user selects the account to withdraw from
        int choice = consoleInput.ReadInteger("Välj konto: ", 1, user.Accounts.Count);

        Account account = user.Accounts[choice - 1];

        //checks and stops the process if the account balance is 0
        if (account.GetBalance() == 0)
        {
            Console.WriteLine("Kontot är tomt.");
            return;
        }


        decimal amount = consoleInput.ReadDecimal("Belopp: ", account.GetBalance());

        string pin = consoleInput.HandlePinUserInput("PIN: ", true);

        if (userManagement.Authenticate(user.UserName, pin) == null)
        {
            Console.WriteLine("Fel PIN-kod.");
            return;
        }

        account.Withdraw(amount);

        Console.WriteLine($"Nytt saldo: {account.GetBalance():N2} kr");
    }
}