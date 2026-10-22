using Bankomaten.Login;
using System.Text;

namespace Bankomaten.UI
{

    /// <summary>
    /// Class representing the start menu of the application.
    /// Takes in the main menu, console input validation, and user login as dependencies.
    /// </summary>
    internal class StartMenu(MainMenu mainMenu, ConsoleInputValidation consoleInputValidation, UserLogin userLogin)
    {
        
        private const string InvalidMenuChoice = "Ogiltigt val. Välj ett av alternativen i menyn.";
        
        private readonly MenuService _menuService = new MenuService(consoleInputValidation);

        /// <summary>
        /// Displays the start menu and handles user input for login or exit.
        /// </summary>
        /// <returns>        
        /// <c>true</c> if the user want to continue.
        /// <c>false</c> if the user wants to exit.
        /// </returns>
        public bool Run()
        {
            Console.WriteLine(BuildStartMenu());
            
            if (consoleInputValidation.ReadInteger("Ange ditt val: ", 1, 2, InvalidMenuChoice) == 1)
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
        private string BuildStartMenu()
        {
            return new StringBuilder()
                .AppendLine(_menuService.CenterTitle("Startmeny"))
                .AppendLine("Välkommen!")
                .AppendLine("Välj ett av alternativen nedan:")
                .AppendLine()
                .AppendLine("1. Logga in")
                .AppendLine()
                .AppendLine("2. Avsluta")
                .AppendLine()
                .ToString();
        }
    }
}
