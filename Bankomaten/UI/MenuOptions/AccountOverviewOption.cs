using Bankomaten.Domain;
using System;
using System.Collections.Generic;
using System.Text;

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
        }
    }
}
