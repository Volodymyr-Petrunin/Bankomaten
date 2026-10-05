using System;
using System.Collections.Generic;
using System.Text;

namespace Bankomaten.Domain
{
    internal class User : IComparable<User>
    {
        /// <summary>
        /// Username of the user
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Pin for the user
        /// </summary>
        public int Pin { get; set; }

        public List<Account> Accounts { get; set; }

        /// <summary>
        /// Constructor for new user, with an empty list Accounts
        /// </summary>
        /// <param name="userName">Name of the user</param>
        /// <param name="pin">Pin for the user</param>
        public User(string userName,int pin) 
        {
            UserName = userName;
            Pin = pin;

            Accounts = new List<Account>();
        }


        public int CompareTo(User? user)
        {

            if (user == null) return 1;

            //string name = name.Compare(this.UserName, user.UserName);

            //return string.Compare(this.UserName, user.UserName);
            return this.UserName.CompareTo(user.UserName);
        }
    }
}
