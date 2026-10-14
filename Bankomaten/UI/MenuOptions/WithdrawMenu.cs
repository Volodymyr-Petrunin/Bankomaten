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
        private const string FromAccountQuestion = "Välj konto att ta ut pengar från: ";
        private const string AmountQuestion = "Hur mycket vill du ta ut: ";
        private const string NotEnoughAccountsMessage = "Du behöver minst ett konto för att ta ut pengar.";
        private const string SuccessMessage = "Uttaget är klart.";
        private const string FailedMessage = "Uttaget kunde inte genomföras.";
        private const string AtmMessage = "Bankomatuttag";
        private const string EmptyAccountMessage = "Kontot är tomt.";
        private const string WrongPinMessage = "Fel PIN-kod.";
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
                    WithdrawFromUserAccount(user);
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
        /// Withdraw funds from the user's account after verifying the account, amount, and PIN code
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

            List<Account> accounts = user.Accounts.ToList();
            Account fromAccount = _accountView.ChooseAccount(FromAccountQuestion, accounts);

            //checks if balance of the selected account is 0 and stops the process if so
            if (fromAccount.GetBalance() == 0)
            {
                throw new UserInputException(EmptyAccountMessage);
            }

            //asks user for an amount to withdraw
            decimal amount = consoleInputValidation.ReadDecimal(_nl + AmountQuestion, fromAccount.GetBalance());

            // Ask for PIN code
            string pin = consoleInputValidation.HandlePinUserInput("ange PIN: ", true);

            // checks if the PIN code is correct
            if (!bankService.VerifyPassword(user, pin))
            {
                throw new UserInputException(WrongPinMessage);
            }

            // checks if the withdrawal was unsuccessful
            if (!fromAccount.Withdraw(amount))
            {
                Console.WriteLine(FailedMessage);
                return;
            }

            // completes the withdrawal and adds a transaction to the user's account
            userManagement.AddUserTransaction(user, TransactionType.Withdrawal, amount, AtmMessage, fromAccount.Name);

            Console.WriteLine($"{_nl}{SuccessMessage}");
            _accountView.PrintBalance(fromAccount);
        }
    }
}
