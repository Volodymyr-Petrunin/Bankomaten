using System;
using System.Collections.Generic;
using System.Text;

namespace Bankomaten.UI
{

    /// <summary>
    /// Class representing the start menu of the application.
    /// </summary>
    internal class StartMenu
    {
        ConsoleInputValidation consoleInput = new ConsoleInputValidation();
        UserLogin loginScreen = new UserLogin();

        /// <summary>
        /// Displays the start menu and handles user input for login or exit.
        /// </summary>
        /// <returns>        
        /// <c>true</c> if the user want to continue.
        /// <c>false</c> if the user wants to exit.
        /// </returns>
        public bool Show(MainMenu mainMenu)
        {
            ShowMenu();

            if (consoleInput.ReadInteger("Ange ditt val:", 1, 2) == 1)
            {
                while (mainMenu.Show(loginScreen.Login));
                return true;
            }

            // Return false and exit the application.
            Console.Clear();
            Console.WriteLine("Programmet avslutas...");
            return false;
        }
        /// <summary>
        /// Displays the start menu options to the console.
        /// </summary>
        private void ShowMenu()
        {
            Console.WriteLine("""
                ===========================
                        Startmeny
                ===========================

                Välkommen till Bankomaten!
                Välj ett alternativ nedan:

                1. Logga in

                2. Avsluta

                """);


        }
    }
}
