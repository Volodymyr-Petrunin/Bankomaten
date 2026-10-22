using Bankomaten.Domain;
using Bankomaten.Services;
using Bankomaten.UI.Exceptions;

namespace Bankomaten.UI.MenuOptions
{
    public class WithdrawMenu(string title, ConsoleInputValidation consoleInputValidation,
    BankService bankService, UserManagement userManagement) : IMenuOptions
    {
        private readonly string _nl = Environment.NewLine;
        private const string FromAccountQuestion = "Välj konto för uttag: ";
        private const string AmountQuestion = "Ange beloop: ";
        private const string NotEnoughAccountsMessage = "Du behöver minst ett konto för att ta ut pengar.";
        private const string SuccessMessage = "Uttaget lyckades.";
        private const string FailedMessage = "Uttaget kunde inte genomföras.";
        private const string AtmMessage = "Bankomatuttag";
        private const string EmptyAccountMessage = "Kontot är tomt.";
        private const string WrongPinMessage = "Fel PIN-kod.";
        
        private readonly MenuService _menuService = new MenuService(consoleInputValidation);
        
        public string Title { get; } = title;

        /// <summary>
        /// Manage user selection.
        /// </summary>
        /// <exception cref="UserInputException"></exception>
        public void Execute(User user)
        {
            Console.WriteLine(_menuService.CenterTitle(Title));
            
            WithdrawFromUserAccount(user);
            
            Console.Clear();
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
            
            Account? fromAccount = _menuService.ChooseAccount(FromAccountQuestion, user.Accounts);

            if (fromAccount == null)
            {
                return;
            }

            //checks if balance of the selected account is 0 and stops the process if so
            if (fromAccount.GetBalance() == 0)
            {
                throw new UserInputException(EmptyAccountMessage);
            }

            //asks user for an amount to withdraw
            decimal amount = consoleInputValidation.ReadDecimal(_nl + AmountQuestion, fromAccount.GetBalance());

            // Ask for PIN code
            string pin = consoleInputValidation.HandlePinUserInput("Ange PIN: ", true);

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
            
            _menuService.WriteSuccessMessage(_nl + SuccessMessage, fromAccount);
        }
    }
}
