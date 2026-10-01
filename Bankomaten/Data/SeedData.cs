using Bankomaten.Domain;

namespace Bankomaten.Data;

public static class SeedData
{
    private static readonly string[] AccountNames = ["Lönekonto", "Sparkonto", "Buffert", "Semesterkonto", "Bilkonto"];

    private static readonly List<User> Users =
    [
        new User("Volodymyr", "1234"),
        new User("Nils", "0987"),
        new User("Marcus", "7654"),
        new User("Ben", "4567"),
        new User("Karl", "1010"),
    ];

    public static Dictionary<long, User> GenerateUsers()
    {
        long id = 1;

        AddAccountsToUsers();

        return Users.ToDictionary(user => id++);
    }

    private static void AddAccountsToUsers()
    {
        // Every user must have a different number of accounts, so the counts are shuffled, not random
        int[] accountCounts = [1, 2, 3, 4];
        Random.Shared.Shuffle(accountCounts);

        for (int index = 0; index < Users.Count - 1; index++)
        {
            Users[index].Accounts.AddRange(CreateAccounts(accountCounts[index]));
        }
    }
    
    private static List<Account> CreateAccounts(int count)
    {
        string[] names = (string[])AccountNames.Clone();
        Random.Shared.Shuffle(names);

        var accounts = new List<Account>();

        for (int index = 0; index < count; index++)
        {
            decimal balance = Random.Shared.Next(0, 2_000_001) / 100m;
            accounts.Add(new Account(names[index], balance));
        }

        return accounts;
    }
}