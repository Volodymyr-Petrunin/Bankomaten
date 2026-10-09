using Bankomaten.Data;
using Bankomaten.Domain;
using System;

public class UserManagement
{
    //stores the users from seed data
    private Dictionary<byte, User> users = SeedData.GenerateUsers();

    public User? FindUser(string username)
    {
        //checks if the username matches any user and returns the user if it does. and if not then null
        foreach (User user in users.Values)
        {
            if (user.UserName == username)
            {
                return user;
            }
        }

        return null;
    }

    public User? Authenticate(string username, string pin)
    {
        //checks if the username and pin match any user in the dictionary and returns the user if they do, null otherwise
        foreach (User user in users.Values)
        {
            if (user.UserName == username && user.Pin == pin)
            {
                return user;
            }
        }

        return null;
    }
}