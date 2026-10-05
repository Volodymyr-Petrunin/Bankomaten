using Bankomaten.Login;
using System.Globalization;
using static System.Globalization.CultureInfo;

namespace Bankomaten;

class Program {
    
    private static void Main(string[] args) {
        // TODO Move to configuration file later
        // All parsing and formatting in the app uses Swedish rules, regardless of the machine's settings
        CurrentCulture = new CultureInfo("sv-SE");

        UserLogin userLogin = new UserLogin();
        userLogin.StartBankomaten();
        
    }
    
}

