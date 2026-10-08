using System;
using System.Collections.Generic;
using System.Text;

namespace Bankomaten.Domain
{
    public class User
    {

        /// <summary>
        /// Username of the user.
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Pin for the user.
        /// </summary>
        public string Pin { get; set; }

        public List<Account> Accounts { get; set; }

        /// <summary>
        /// Constructor for new user, with an empty list Accounts.
        /// </summary>
        public User(string userName, string pin)
        {
            UserName = userName;
            Pin = pin;

            Accounts = new List<Account>();
        }
    }
}
