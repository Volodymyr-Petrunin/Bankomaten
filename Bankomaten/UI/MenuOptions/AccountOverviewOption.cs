using Bankomaten.Domain;

namespace Bankomaten.UI.MenuOptions
{
    internal class AccountOverviewOption : IMenuOptions
    {
        public string Title => "Se dina konton och saldo";

        /// <summary>
        /// Executes the account overview option, displaying all the user's accounts and their balances.
        /// </summary>
        public void Execute(User user)
        {
            foreach (var account in user.Accounts)
            {
                Console.WriteLine(account.ToString());
            }
            
            PrintTransactionsHistory(user, 3);
        }
        
        private static void PrintTransactionsHistory(User user, byte numberOfTransactionsToShow)
        {
            Console.WriteLine(new string('-', Console.WindowWidth / 2));
            
            if (user.TransactionsHistory.Count == 0)
            {
                Console.WriteLine("Inga transaktioner ännu.");
                return;
            }
            
            Console.WriteLine($"Senaste {numberOfTransactionsToShow} transaktionerna:");
            user.TransactionsHistory.AsEnumerable().Reverse().Take(numberOfTransactionsToShow).ToList().ForEach(Console.WriteLine);
        }
    }
}
