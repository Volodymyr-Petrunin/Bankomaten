using Bankomaten.Domain;
using Bankomaten.Services;
using Bankomaten.UI.Exceptions;
using Bankomaten.UI.MenuOptions;
using System.Diagnostics;
using System.Text;

namespace Bankomaten.UI.MenuOptions
{
    public class WithdrawMenu(string title, ConsoleInputValidation consoleInputValidation,
    BankService bankService, UserManagement userManagement) : IMenuOptions
    {
        private readonly AccountView _accountView = new AccountView(consoleInputValidation);

        private readonly string _nl = Environment.NewLine;
        private const string ToAccountQuestion = "Välj konto att föra över till: ";
        private const string AmountQuestion = "Hur mycket vill du föra över: ";
        private const string NotEnoughAccountsMessage = "Du behöver minst ett konto för att sätta in pengar.";
        private const string SuccessMessage = "Överföringen är klar.";
        private const string FailedMessage = "Överföringen kunde inte genomföras.";
        private const string AtmMessage = "Bankomatinsättning";
        public string Title { get; } = title;

        /// <summary>
        /// Manage user selection.
        /// </summary>
        /// <param name="user"></param>
        /// <exception cref="UnreachableException"></exception>
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
                .AppendLine("2. Tillbaka till huvudmenyn")
                .ToString();
        }

        /// <summary>
        /// Allow user to deposit funds to their own account.
        /// Validates that the user has at least one one account. If so the user will be presented a list with their account(s).
        /// The amount that can be deposited is between "decimal.MaxValue" (approx. 79 trillion trillion) and "decimal default" (0,01).
        /// </summary>
        /// <param name="user"></param>
        /// <exception cref="UserInputException"></exception>
        private void WithdrawFromUserAccount(User user)
        {
            // Check so the user have at least one account
            if (user.Accounts.Count < 1)
            {
                throw new UserInputException(NotEnoughAccountsMessage);
            }

            List<Account> targetAccounts = user.Accounts.ToList();
            Account toAccount = _accountView.ChooseAccount(ToAccountQuestion, targetAccounts);

            decimal amount = consoleInputValidation.ReadDecimal(_nl + AmountQuestion, decimal.MaxValue, default);

            if (!bankService.DepositToOwnAccount(user, toAccount, amount))
            {
                Console.WriteLine(FailedMessage);
            }

            // Add transaction history
            userManagement.AddUserTransaction(user, TransactionType.OwnTransfer, amount, AtmMessage, toAccount.Name);

            Console.WriteLine($"{_nl}{SuccessMessage}");
            _accountView.PrintBalance(toAccount);
        }
    }
}
