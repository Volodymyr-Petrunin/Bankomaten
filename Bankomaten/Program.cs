using System.Globalization;
using Bankomaten.Data;
using Bankomaten.Services;
using static System.Globalization.CultureInfo;

namespace Bankomaten;

class Program {
    
    private static void Main(string[] args) {
        // TODO Move to configuration file later
        // All parsing and formatting in the app uses Swedish rules, regardless of the machine's settings
        CurrentCulture = new CultureInfo("sv-SE");

        BankService bankService = new BankService(SeedData.GenerateUsers());

        // bankService.TransferBetweenUserAccounts(new Account("test", 22m), new Account("test", 22m), 33m);
    }
    
}
