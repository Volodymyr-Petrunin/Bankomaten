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

        private readonly string _nl = Environment.NewLine;
        ConsoleInputValidation consoleInput = new ConsoleInputValidation();
        UserLogin loginScreen = new UserLogin();
        public bool Show(MainMenu mainMenu)
        {
                ShowMenu();
                int input = consoleInput.ReadInteger("Ange ditt val:", 1, 2);
            if (input == 1)
            {
                while (mainMenu.Show(loginScreen.Login))
                {
                    // Continue showing the login screen until the user logs in successfully
                }
            }
            else if (input == 2)
            {
                Console.Clear();
                Console.WriteLine("Programmet avslutas...");
                return false;
            }
            return true;
        }
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
