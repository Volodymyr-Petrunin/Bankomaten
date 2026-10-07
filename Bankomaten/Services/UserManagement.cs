using Bankomaten.Data;
using Bankomaten.Domain;
using System;

public class UserManagement
{
    //stores the users from seed data
    private Dictionary<long, User> users = SeedData.GenerateUsers();

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

    public bool Authenticate(string username, string pin)
    {
        //checks if the username and pin match any user in the dictionary and returns true if they do, false otherwise
        foreach (User user in users.Values)
        {
            if (user.UserName == username && user.Pin == pin)
            {
                return true;
            }
        }

        return false;
    }
}