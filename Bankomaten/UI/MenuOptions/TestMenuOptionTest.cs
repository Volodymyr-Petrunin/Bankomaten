using System;
using System.Collections.Generic;
using System.Text;

namespace Bankomaten.UI.MenuOptions
{
    public class TestMenuOptionTest : IMenuOption
    {
        public string Title => "TestMenuOption";
        public void Execute(Domain.User user)
        {
            Console.WriteLine($"Test menyval för användare: {user.UserName}");
        }
    }
}
