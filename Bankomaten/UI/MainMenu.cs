using Bankomaten.UI.MenuOptions;
using System;
using System.Collections.Generic;
using System.Text;
using Bankomaten.Domain;

namespace Bankomaten.UI
{
    internal class MainMenu
    {
        /// <summary>
        /// Array of menu options to display in the main menu.
        /// </summary>
        IMenuOption[] menuOptions = new IMenuOption[] { new TestMenuOptionTest() };



        public void Show(User user)
        {
            Console.WriteLine(userMenuTitle);
            Console.WriteLine($"Välkommen {user.UserName}!");
            Console.WriteLine("Välj ett alternativ nedan:\n");
            for (int i = 0; i < menuOptions.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {menuOptions[i].Title}");
            }
            Console.WriteLine($"{menuOptions.Length + 1}. Logga ut");
            int choice = new ConsoleInputValidation().ReadInteger("Ange ditt val:", 1, menuOptions.Length + 1);
            if (choice == menuOptions.Length + 1)
            {
                Console.WriteLine("Du har loggat ut.");
                return;
            }
            menuOptions[choice - 1].Execute(user);
        }
        string userMenuTitle = """
            ==========================================================================
            ||                                                                      ||
            ||   _   _  _   _  _     _  _   _  ____   __  __ _____ _   _ __     __  ||
            ||  | | | || | | || |   | || | | ||  _ \ |  \/  | ____| \ | |\ \   / /  ||
            ||  | |_| || | | | \ \ / / | | | || | | || |\/| |  _| |  \| | \ \ / /   ||
            ||  |  _  || |_| |  \ V /  | |_| || |_| || |  | | |___| |\  |  \ V /    ||
            ||  |_| |_| \___/    \_/    \___/ |____/ |_|  |_|_____|_| \_|   |_|     ||
            ||                                                                      ||
            ==========================================================================
            
            """;
    }
}
