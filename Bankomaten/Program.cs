using System.Globalization;
using Bankomaten.Data;
using Bankomaten.Login;
using Bankomaten.Services;
using Bankomaten.UI;
using Bankomaten.UI.MenuOptions;
using static System.Globalization.CultureInfo;

// All parsing and formatting in the app uses Swedish rules, regardless of the machine's settings
CurrentCulture = new CultureInfo("sv-SE");

UserManagement userManagement = new UserManagement(SeedData.GenerateUsers());
BankService bankService = new BankService();
ConsoleInputValidation consoleInputValidation = new ConsoleInputValidation();
        
IMenuOptions[] options = [
    new AccountOverviewMenu("Konton & Saldo", consoleInputValidation),
    new TransferMenu("Betala & Överföra", consoleInputValidation, bankService, userManagement),
    new DepositMenu("Insättning", consoleInputValidation, bankService, userManagement),
    new WithdrawMenu("Uttag", consoleInputValidation, bankService, userManagement),
];

MainMenu mainMenu = new MainMenu(options,  consoleInputValidation);
UserLogin userLogin = new UserLogin(userManagement, consoleInputValidation);
StartMenu startMenu = new StartMenu(mainMenu, consoleInputValidation, userLogin);

// Run the start menu in a loop to start the program.
// The loop will continue until the user chooses to exit the program.
while (startMenu.Run());