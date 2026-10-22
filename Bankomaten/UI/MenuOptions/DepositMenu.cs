using Bankomaten.Domain;
using Bankomaten.Services;
using Bankomaten.UI.Exceptions;

namespace Bankomaten.UI.MenuOptions
{
    public class DepositMenu(string title, ConsoleInputValidation consoleInputValidation,
        BankService bankService, UserManagement userManagement) : IMenuOptions
    {
        private readonly MenuService _menuService = new MenuService(consoleInputValidation);

        private readonly string _nl = Environment.NewLine;
        private const string ToAccountQuestion = "Välj konto för insättning: ";
        private const string AmountQuestion = "Ange belopp: ";
        private const string NotEnoughAccountsMessage = "Du behöver minst ett konto för att sätta in pengar.";
        private const string SuccessMessage = "Insättningen är godkänd.";
        private const string FailedMessage = "Insättningen kunde inte genomföras.";
        private const string AtmMessage = "Bankomatinsättning";
        public string Title { get; } = title;

        /// <summary>
        /// Manage user selection.
        /// </summary>
        /// <exception cref="UserInputException"></exception>
        public void Execute(User user)
        {
            Console.WriteLine(_menuService.CenterTitle(Title));
            
            DepositToUserAccount(user);

            Console.Clear();
        }

        /// <summary>
        /// Allow user to deposit funds to their own account.
        /// Validates that the user has at least one one account. If so the user will be presented a list with their account(s).
        /// The amount that can be deposited is between "decimal.MaxValue" (approx. 79 trillion trillion) and "decimal default" (0,01).
        /// </summary>
        /// <exception cref="UserInputException"></exception>
        private void DepositToUserAccount(User user)
        {

            // Check so the user have at least one account
            if (user.Accounts.Count < 1)
            {
                throw new UserInputException(NotEnoughAccountsMessage);
            }
            
            Account? toAccount = _menuService.ChooseAccount(ToAccountQuestion, user.Accounts);

            if (toAccount == null)
            {
                return;
            }
            
            decimal amount = consoleInputValidation.ReadDecimal(_nl + AmountQuestion, decimal.MaxValue, default);

            if (!bankService.DepositToOwnAccount(user, toAccount, amount))
            {
                Console.WriteLine(FailedMessage);
                return;
            }

            // Add transaction history
            userManagement.AddUserTransaction(user, TransactionType.OwnTransfer, amount, AtmMessage, toAccount.Name);
            
            _menuService.WriteSuccessMessage(_nl + SuccessMessage, toAccount);
        }
    }
}
