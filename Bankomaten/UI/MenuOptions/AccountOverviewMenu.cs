using Bankomaten.Domain;

namespace Bankomaten.UI.MenuOptions
{
    internal class AccountOverviewMenu(string title, ConsoleInputValidation consoleInputValidation) : IMenuOptions
    {
        private const int NumberOfTransactionsToShow = 3;
        
        private readonly MenuService _menuService = new MenuService(consoleInputValidation);
        
        public string Title => title;

        /// <summary>
        /// Executes the account overview option, displaying all the user's accounts and their balances.
        /// </summary>
        public void Execute(User user)
        {
            Console.WriteLine(_menuService.CenterTitle(Title));
            
            foreach (Account account in user.Accounts)
            {
                Console.WriteLine(account.ToString());
            }
            
            PrintTransactionsHistory(user, NumberOfTransactionsToShow);
            consoleInputValidation.WaitForEnter("Tryck på Enter för att återgå till huvudmenyn.");
            Console.Clear();
        }
        
        /// <summary>
        /// Displays the transaction history for the specified user in the console, 
        /// showing up to a specified number of recent transactions.
        /// </summary>
        /// <param name="user">The user whose transaction history is to be displayed.</param>
        /// <param name="numberOfTransactionsToShow">The maximum number of recent transactions to display.</param>
        private static void PrintTransactionsHistory(User user, byte numberOfTransactionsToShow)
        {
            string horizontalLine = new string('-', Console.WindowWidth);
            Console.WriteLine(horizontalLine);
            
            if (user.TransactionsHistory.Count == 0)
            {
                Console.WriteLine("Inga transaktioner ännu.");
                Console.WriteLine(horizontalLine);
                return;
            }
            
            Console.WriteLine($"Senaste {numberOfTransactionsToShow} transaktioner:");
            user.TransactionsHistory.AsEnumerable().Take(numberOfTransactionsToShow).ToList().ForEach(Console.WriteLine);
            Console.WriteLine(horizontalLine);
        }

    }
}
