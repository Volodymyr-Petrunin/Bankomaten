using System.Globalization;
using Bankomaten.Data;
using Bankomaten.Login;
using Bankomaten.Services;
using Bankomaten.UI;
using Bankomaten.UI.MenuOptions;
using Bankomaten.Domain;
using static System.Globalization.CultureInfo;

namespace Bankomaten;

class Program {
    
    private static void Main(string[] args) {
        // TODO Move to configuration file later
        // All parsing and formatting in the app uses Swedish rules, regardless of the machine's settings
        CurrentCulture = new CultureInfo("sv-SE");

        UserManagement userManagement = new UserManagement(SeedData.GenerateUsers());
        BankService bankService = new BankService();
        ConsoleInputValidation consoleInputValidation = new ConsoleInputValidation();
        
        IMenuOptions[] options = [
            new AccountOverviewOption(),
            new TransferMenu("Överföring mellan konton", consoleInputValidation, bankService, userManagement),
        ];

        MainMenu mainMenu = new MainMenu(options,  consoleInputValidation);
        UserLogin userLogin = new UserLogin(userManagement, consoleInputValidation);
        StartMenu startMenu = new StartMenu(mainMenu, consoleInputValidation, userLogin);

        // User login functionality


        while (startMenu.Run());
    }
    
}
