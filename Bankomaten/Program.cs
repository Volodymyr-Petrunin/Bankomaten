using Bankomaten.Domain;

namespace Bankomaten;

class Program {
    
    private static void Main(string[] args) 
    {
        //================================================================================
        // Tester för konstruktor
        Console.WriteLine("--------------------------------------------");
        Console.WriteLine("Test 1: Skapa konto med giltigt startsaldo");
        Account nilsKonto = new Account("NilsTestKonto", 1555.23m);
        Console.WriteLine($"Konto skapat för: {nilsKonto.Name}");
        Console.WriteLine("Förväntat kontosaldo: 1555,23");
        Console.WriteLine($"Faktiskt saldo:       {nilsKonto.Balance}");
        Console.WriteLine("--------------------------------------------");
        Console.ReadKey();

        Console.WriteLine("Test 2: Skapa konto med standardvärde (0 kr)");
        Account standardKonto = new Account("StandardKonto");
        Console.WriteLine($"Konto skapat för: {standardKonto.Name}");
        Console.WriteLine("Förväntat kontosaldo: 0");
        Console.WriteLine($"Faktiskt saldo:       {standardKonto.Balance}");
        Console.WriteLine("--------------------------------------------");
        Console.ReadKey();

        Console.WriteLine("Test 3: Skapa konto med ogiltigt (negativt) saldo");
        try
        {
            Account ogiltigtKonto = new Account("ErrorKonto", -500m);
            Console.WriteLine("FEL: Konstruktorn borde ha kastat ett undantag!");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine("SUCCESS: Konstruktorn kastade förväntat undantag.");
            Console.WriteLine($"Felmeddelande: {ex.Message}");
        }
        Console.WriteLine("--------------------------------------------");
        Console.ReadKey();

     //================================================================================
     // Tester för Deposit();

        Console.WriteLine("Test 4: Giltig insättning");
        Account depositKonto = new Account("InsättningKonto", 100m);
        bool depositResult1 = depositKonto.Deposit(50.50m);
        Console.WriteLine($"Metod returnerade:  {depositResult1} (Förväntat: True)");
        Console.WriteLine("Förväntat saldo:    150,50");
        Console.WriteLine($"Faktiskt saldo:     {depositKonto.Balance}");
        Console.WriteLine("--------------------------------------------");
        Console.ReadKey();

        Console.WriteLine("Test 5: Ogiltig insättning (0 kr eller negativt)");
        bool depositResult2 = depositKonto.Deposit(0m);
        bool depositResult3 = depositKonto.Deposit(-20m);
        Console.WriteLine($"Resultat vid 0 kr:   {depositResult2} (Förväntat: False)");
        Console.WriteLine($"Resultat vid -20 kr: {depositResult3} (Förväntat: False)");
        Console.WriteLine("Förväntat orört saldo: 150,50");
        Console.WriteLine($"Faktiskt saldo:        {depositKonto.Balance}");
        Console.WriteLine("--------------------------------------------");
        Console.ReadKey();

        // =========================================================================
        // Tester för Withdraw();

        Console.WriteLine("Test 6: Giltigt uttag");
        Account withdrawKonto = new Account("UttagKonto", 500m);
        bool withdrawResult1 = withdrawKonto.Withdraw(200m);
        Console.WriteLine($"Metod returnerade:  {withdrawResult1} (Förväntat: True)");
        Console.WriteLine($"Saldo före uttag: {withdrawKonto.Balance}");
        Console.WriteLine("Uttag: 200m");
        Console.WriteLine("Förväntat saldo:    300");
        Console.WriteLine($"Faktiskt saldo:     {withdrawKonto.Balance}");
        Console.WriteLine("--------------------------------------------");
        Console.ReadKey();

        Console.WriteLine("Test 7: Ogiltigt uttag (För stort belopp)");
        bool withdrawResult2 = withdrawKonto.Withdraw(400m);
        Console.WriteLine($"Metod returnerade:  {withdrawResult2} (Förväntat: False)");
        Console.WriteLine("Förväntat orört saldo: 300");
        Console.WriteLine($"Faktiskt saldo:        {withdrawKonto.Balance}");
        Console.WriteLine("--------------------------------------------");
        Console.ReadKey();

        Console.WriteLine("Test 8: Ogiltigt uttag (0 kr eller negativt)");
        bool withdrawResult3 = withdrawKonto.Withdraw(0m);
        bool withdrawResult4 = withdrawKonto.Withdraw(-50m);
        Console.WriteLine($"Resultat vid 0 kr:   {withdrawResult3} (Förväntat: False)");
        Console.WriteLine($"Resultat vid -50 kr: {withdrawResult4} (Förväntat: False)");
        Console.WriteLine("Förväntat orört saldo: 300");
        Console.WriteLine($"Faktiskt saldo:        {withdrawKonto.Balance}");
        Console.WriteLine("--------------------------------------------");
        Console.ReadKey();
    }
}
