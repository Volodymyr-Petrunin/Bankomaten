using Bankomaten.Domain;
using Bankomaten.Services;
using Bankomaten.UI.Exceptions;
using Bankomaten.UI.MenuOptions;
using System.Diagnostics;
using System.Text;

namespace Bankomaten.UI.MenuOptions
{
    public class DepositMenu(string title, ConsoleInputValidation consoleInputValidation,
    BankService bankService, UserManagement userManagement) : IMenuOptions
    {
        private readonly string _nl = Environment.NewLine;
        private const string ToAccountQuestion = "Välj konto att föra över till: ";
        private const string AmountQuestion = "Hur mycket vill du föra över: ";
        private const string NotEnoughAccountsMessage = "Du behöver minst ett konto för att sätta in pengar.";
        private const string SuccessMessage = "Överföringen är klar.";
        private const string FailedMessage = "Överföringen kunde inte genomföras.";
        private const string AtmMessage = "Bankomatinsättning";
        public string Title { get; } = title;

        public void Execute(User user)
        {
            int selectedOption = consoleInputValidation.ReadInteger(BuildMenu(), 1, 2);
            Console.Clear();

            switch (selectedOption)
            {
                case 1:
                    DepositToUserAccount(user);
                    break;
                case 2:
                    break;
                default:
                    throw new UnreachableException($"Ogiltigt menyval: {selectedOption}");
            }
        }

        // <summary>
        // Builds and returns a formatted string representing a transfer menu with options.
        // </summary>
        // <returns>A string containing the transfer menu options, formatted for display.</returns>
        private string BuildMenu()
        {
            return new StringBuilder()
                .AppendLine(Title)
                .AppendLine("1. Fortsätt")
                .AppendLine("2. Tillbaka till huvudmenyn") // This takes the user back to an unneccessary menu!
                .ToString();
        }

        /// <summary>
        /// Allow user to deposit funds to their own account.
        /// Validates that the user has at least one one account. If so the user will be presented a list with their account(s).
        /// The amount that can be deposited is between "decimal.MaxValue" (approx. 79 trillion trillion) and "decimal default" (0,01).
        /// </summary>
        /// <param name="user"></param>
        /// <exception cref="UserInputException"></exception>
        private void DepositToUserAccount(User user)
        {
            // Check so the user have at least one account
            if (user.Accounts.Count < 1)
            {
                throw new UserInputException(NotEnoughAccountsMessage);
            }

            List<Account> targetAccounts = user.Accounts.ToList();
            Account toAccount = ChooseAccount(ToAccountQuestion, targetAccounts);

            decimal amount = consoleInputValidation.ReadDecimal(_nl + AmountQuestion, decimal.MaxValue, default);

            if (!bankService.DepositToOwnAccount(user, toAccount, amount))
            {
                Console.WriteLine(FailedMessage);
            }

            // Add transaction history
            userManagement.AddUserTransaction(user, TransactionType.OwnTransfer, amount, AtmMessage, toAccount.Name);

            Console.WriteLine($"{_nl}{SuccessMessage}");
            PrintBalance(toAccount);
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

        /// <summary>
        /// Displays the current balance of the specified account in a formatted manner.
        /// </summary>
        private void PrintBalance(Account account)
        {
            Console.WriteLine($"Nytt saldo på {account.Name}: {account.GetBalance():C}{_nl}");
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
                stringBuilder.AppendLine($"{i + 1}. {accounts[i].Name}  Saldo: {accounts[i].GetBalance():C}");
            }

            return stringBuilder.ToString();
        }
    }
}
