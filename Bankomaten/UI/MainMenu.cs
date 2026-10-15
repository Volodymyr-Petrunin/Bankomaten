using Bankomaten.Domain;
using Bankomaten.UI.Exceptions;
using Bankomaten.UI.MenuOptions;

namespace Bankomaten.UI
{
    internal class MainMenu (IMenuOptions[] menuOptions, ConsoleInputValidation consoleInput)
    {
        /// <summary>
        /// Empty line, replaces \n.
        /// </summary>
        private readonly string _nl = Environment.NewLine;

        /// <summary>
        /// Displays the main menu for the user.
        /// </summary>
        public bool Show(User user)
        {
            Console.WriteLine(consoleInput.CenterTitle("Huvudmeny"));
            Console.WriteLine($"Du är inloggad som: {user.UserName}.");
            Console.WriteLine($"Välj ett alternativ nedan:{_nl}");

            for (int index = 0; index < menuOptions.Length; index++)
            {
                Console.WriteLine($"{index + 1}. {menuOptions[index].Title}");
            }

            Console.WriteLine($"{_nl}{menuOptions.Length + 1}. Logga ut{_nl}");
            int selectedMainMenu = consoleInput.ReadInteger("Ange ditt val: ", 1, menuOptions.Length + 1, "Ogiltigt val. Välj ett av alternativen i menyn.");

            if (selectedMainMenu == menuOptions.Length + 1)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{_nl}Du har loggats ut.{_nl}");
                Console.ResetColor();
                return false;
            }

            try
            {
                Console.Clear();
                menuOptions[selectedMainMenu - 1].Execute(user);
            }
            catch (UserInputException exception)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(exception.Message);
                Console.ResetColor();
            }
            
            return true;
        }
    }
}
