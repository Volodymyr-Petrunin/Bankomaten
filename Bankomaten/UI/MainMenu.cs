using Bankomaten.Domain;
using Bankomaten.UI.Exceptions;
using Bankomaten.UI.MenuOptions;

namespace Bankomaten.UI
{
    internal class MainMenu (IMenuOptions[] menuOptions, ConsoleInputValidation consoleInput)
    {
        /// <summary>
        /// The title of the user menu, displayed when the user logs in.
        /// </summary>
        private const string UserMenuTitle = """
                                             ==========================
                                                     Huvudmeny!
                                             ==========================
                                             """;

        /// <summary>
        /// Empty line, replaces \n.
        /// </summary>
        private readonly string _nl = Environment.NewLine;

        /// <summary>
        /// Displays the main menu for the user.
        /// </summary>
        public bool Show(User user)
        {
            Console.WriteLine(UserMenuTitle);
            Console.WriteLine($"Välkommen {user.UserName}!");
            Console.WriteLine($"Välj ett alternativ nedan:{_nl}");

            for (int index = 0; index < menuOptions.Length; index++)
            {
                Console.WriteLine($"{index + 1}. {menuOptions[index].Title}");
            }

            Console.WriteLine($"{_nl}{menuOptions.Length + 1}. Logga ut{_nl}");
            int selectedMainMenu = consoleInput.ReadInteger("Ange ditt val:", 1, menuOptions.Length + 1);

            if (selectedMainMenu == menuOptions.Length + 1)
            {
                Console.Clear();
                Console.WriteLine("Du har loggat ut.");
                return false;
            }

            try
            {
                menuOptions[selectedMainMenu - 1].Execute(user);
            }
            catch (UserInputException exception)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(exception.Message);
                Console.ResetColor();
            }
            
            consoleInput.WaitForEnter("Tryck på Enter för att återgå till huvudmenyn.");
            return true;
        }
    }
}
