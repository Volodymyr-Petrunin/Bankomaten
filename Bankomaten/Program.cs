using System.Globalization;
using Bankomaten.Data;
using Bankomaten.Services;
using Bankomaten.UI;
using Bankomaten.UI.MenuOptions;
using static System.Globalization.CultureInfo;

namespace Bankomaten;

class Program {
    
    private static void Main(string[] args) {
        // TODO Move to configuration file later
        // All parsing and formatting in the app uses Swedish rules, regardless of the machine's settings
        CurrentCulture = new CultureInfo("sv-SE");

        BankService bankService = new BankService(SeedData.GenerateUsers());

        ConsoleInputValidation consoleInputValidation = new ConsoleInputValidation();
        IMenuOptions[] options = [new TransferMenu("New", consoleInputValidation, bankService)];
        
        options[0].Execute(bankService._users.Values.First());
    }
    
}
