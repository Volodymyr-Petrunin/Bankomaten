using System;
using System.Collections.Generic;
using System.Text;
using Bankomaten.Domain;

namespace Bankomaten.UI.MenuOptions
{
    internal class AccountOverviewOption : IMenuOption
    {
        public string Title => "Se dina konton och saldo";
        public void Execute(User user)
        {
       
        }
    }
}
