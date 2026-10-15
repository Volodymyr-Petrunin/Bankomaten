using Bankomaten.Domain;

namespace Bankomaten.UI.MenuOptions
{
    internal class AccountOverviewMenu(string title, ConsoleInputValidation consoleInputValidation) : IMenuOptions
    {
        public string Title => title;

        /// <summary>
        /// Executes the account overview option, displaying all the user's accounts and their balances.
        /// </summary>
        public void Execute(User user)
        {
            Console.WriteLine(consoleInputValidation.CenterTitle(Title));
            foreach (var account in user.Accounts)
            {
                Console.WriteLine(account.ToString());
            }
            
            PrintTransactionsHistory(user, 3);
            consoleInputValidation.WaitForEnter("Tryck på Enter för att återgå till huvudmenyn.");
            Console.Clear();
        }
        
        private static void PrintTransactionsHistory(User user, byte numberOfTransactionsToShow)
        {
            Console.WriteLine(new string('-', Console.WindowWidth));
            
            if (user.TransactionsHistory.Count == 0)
            {
                Console.WriteLine("Inga transaktioner ännu.");
                Console.WriteLine(new string('-', Console.WindowWidth));
                return;
            }
            
            Console.WriteLine($"Senaste {numberOfTransactionsToShow} transaktioner:");
            user.TransactionsHistory.AsEnumerable().Reverse().Take(numberOfTransactionsToShow).ToList().ForEach(Console.WriteLine);
            Console.WriteLine(new string('-', Console.WindowWidth));
        }

    }
}
