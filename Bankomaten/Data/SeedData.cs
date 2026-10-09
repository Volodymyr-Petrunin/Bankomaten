using Bankomaten.Domain;

namespace Bankomaten.Data;

public static class SeedData
{
    /// <summary>
    /// A collection of predefined account names.
    /// These names are referred to when creating accounts for users.
    /// </summary>
    private static readonly string[] AccountNames = ["Lönekonto", "Sparkonto", "Buffert", "Semesterkonto", "Bilkonto"];

    /// <summary>
    /// A predefined collection of user objects.
    /// Each user has a unique username, pin, and an associated list of accounts.
    /// Primarily used as seed data for initializing user data in the application.
    /// </summary>
    private static readonly List<User> Users =
    [
        new User("Volodymyr", "1234"),
        new User("Nils", "0987"),
        new User("Marcus", "7654"),
        new User("Ben", "4567"),
        new User("Karl", "1010"),
    ];

    /// <summary>
    /// Generates a dictionary of users with unique IDs as keys and User instances as values.
    /// </summary>
    /// <returns>
    /// A dictionary where each key is a byte representing a unique user ID,
    /// and each value is a User object containing user information.
    /// </returns>
    public static Dictionary<byte, User> GenerateUsers()
    {
        byte id = 1;

        AddAccountsToUsers();

        return Users.ToDictionary(user => id++);
    }

    /// <summary>
    /// Automatically assigns accounts to each user in the predefined list of users.
    /// The number of accounts assigned to each user varies and is determined
    /// based on a shuffled collection of account counts.
    /// </summary>
    private static void AddAccountsToUsers()
    {
        // Every user must have a different number of accounts, so the counts are shuffled, not random
        int[] accountCounts = [.. Enumerable.Range(1, AccountNames.Length)];
        Random.Shared.Shuffle(accountCounts);

        for (int index = 0; index < Users.Count - 1; index++)
        {
            Users[index].Accounts.AddRange(CreateAccounts(accountCounts[index]));
        }
    }

    /// <summary>
    /// Creates a list of accounts with specified count using predefined account names.
    /// Each account is initialized with a random balance within a predefined range.
    /// </summary>
    /// <param name="count">
    /// The number of accounts to generate.
    /// </param>
    /// <returns>
    /// A list of Account objects, where each account contains a unique name and a randomly generated balance.
    /// </returns>
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