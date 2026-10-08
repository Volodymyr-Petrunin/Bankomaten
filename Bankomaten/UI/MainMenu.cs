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
        /// Add new menu options here to make them available in the main menu.
        /// </summary>
        IMenuOption[] menuOptions = new IMenuOption[] { };

        ConsoleInputValidation _consoleInput = new ConsoleInputValidation();


        /// <summary>
        /// Displays the main menu for the user.
        /// </summary>
        public bool Show(User user)
        {
            Console.WriteLine(userMenuTitle);
            Console.WriteLine($"Välkommen {user.UserName}!");
            Console.WriteLine("Välj ett alternativ nedan:\n");

            for (int i = 0; i < menuOptions.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {menuOptions[i].Title}");
            }

            Console.WriteLine($"\n{menuOptions.Length + 1}. Logga ut\n");
            int selectedMainMenu = _consoleInput.ReadInteger("Ange ditt val:", 1, menuOptions.Length + 1);

            if (selectedMainMenu == menuOptions.Length + 1)
            {
                Console.Clear();
                Console.WriteLine("Du har loggat ut.");
                return false;
            }

            menuOptions[selectedMainMenu - 1].Execute(user);
            _consoleInput.WaitForEnter("Tryck på Enter för att återgå till huvumenyn.");
            return true;
        }

        /// <summary>
        /// The title of the user menu, displayed when the user logs in.
        /// </summary>
        string userMenuTitle = """
            ==========================
                    Huvudmeny!
            ==========================
            """;


    }
}
