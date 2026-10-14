using Bankomaten.Domain;
using Bankomaten.Login;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bankomaten.UI
{

    /// <summary>
    /// Class representing the start menu of the application.
    /// Takes in the main menu, console input validation, and user login as dependencies.
    /// </summary>
    internal class StartMenu(MainMenu mainMenu, ConsoleInputValidation consoleInput, UserLogin userLogin)
    {

        /// <summary>
        /// Displays the start menu and handles user input for login or exit.
        /// </summary>
        /// <returns>        
        /// <c>true</c> if the user want to continue.
        /// <c>false</c> if the user wants to exit.
        /// </returns>
        public bool Run()
        {
            ShowStartMenu();

            if (consoleInput.ReadInteger("Ange ditt val: ", 1, 2) == 1)
            {
                // The logged in user is saved here and passed to the main menu.
                // The user will stay logged in until they choose to log out from the main menu.
                var user = userLogin.UserInput();
                while (mainMenu.Show(user));
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
        private void ShowStartMenu()
        {
            Console.WriteLine("""
                ===========================
                        Startmeny
                ===========================

                Välkommen till Bankomaten!
                Välj ett alternativ nedan:

                [1] Logga in

                [2] Avsluta

                """);


        }
    }
}
