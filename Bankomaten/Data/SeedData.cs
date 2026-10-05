using Bankomaten.Domain;

namespace Bankomaten.Data;

internal static class SeedData
{
    public static SortedSet<User>? UserData { get; } = new SortedSet<User>();

    private static readonly List<Account> Accounts =
    [
        new Account("Sparkonto", 10000m),
        new Account("Lönekonto", 4567m),
        new Account("Lönekonto", 101000m),
        new Account("Sparkonto", 4m),
        new Account("Sparkonto", 333.33m),
        new Account("Sparkonto", 8763.75m),
        new Account("Lönekonto", 3290.73m),
        new Account("Lönekonto", 987.03m),
    ];

    private static readonly SortedSet<User> Users =
    [
        new User("Volodymyr", 1234),
        new User("Nils", 0987),
        new User("Marcus", 7654),
        new User("Ben", 4567),
        new User("Karl", 1010),
    ];
}