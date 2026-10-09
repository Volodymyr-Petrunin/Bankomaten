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
                return mainMenu.Show(loginScreen.Login);
            }
            Console.Clear();
            Console.WriteLine("Programmet avslutas...");
            return false;
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
