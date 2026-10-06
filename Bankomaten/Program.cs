using Bankomaten.Data;
using Bankomaten.Domain;
using Bankomaten.UI;
using System.Globalization;
using static System.Globalization.CultureInfo;


namespace Bankomaten;

class Program {
    
    private static void Main(string[] args) {
        // TODO Move to configuration file later
        // All parsing and formatting in the app uses Swedish rules, regardless of the machine's settings
        CurrentCulture = new CultureInfo("sv-SE");

        User testUser = new User("Nils", "0987");
        MainMenu menu = new MainMenu();
        menu.Show(testUser);
    }
    
}
