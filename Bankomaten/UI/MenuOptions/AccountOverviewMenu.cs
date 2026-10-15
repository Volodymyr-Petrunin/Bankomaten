using Bankomaten.Domain;

namespace Bankomaten.UI.MenuOptions
{
<<<<<<< HEAD:Bankomaten/UI/MenuOptions/AccountOverviewOption.cs
    internal class AccountOverviewOption(string title, ConsoleInputValidation consoleInputValidation) : IMenuOptions
    {
        public string Title { get; } = title;
=======
    internal class AccountOverviewMenu(string title) : IMenuOptions
    {
        public string Title => title;
>>>>>>> origin/main:Bankomaten/UI/MenuOptions/AccountOverviewMenu.cs

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
            Console.WriteLine(new string('-', Console.WindowWidth / 2));
            
            if (user.TransactionsHistory.Count == 0)
            {
                Console.WriteLine("Inga transaktioner ännu.");
                return;
            }
            
            Console.WriteLine($"Senaste {numberOfTransactionsToShow} transaktioner:");
            user.TransactionsHistory.AsEnumerable().Reverse().Take(numberOfTransactionsToShow).ToList().ForEach(Console.WriteLine);
        }

    }
}
