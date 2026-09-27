using System;
using System.Collections.Generic;
using System.Text;

namespace Bankomaten.Domain
{
    public class Account
    {
        /// <summary>
        /// Get/Set name for the account
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Get/Set current balance of the account
        /// balance is private so it can't be changed to a negative outside the class
        /// </summary>
        public decimal Balance { get; private set; }

        /// <summary>
        /// Constructor for a new account with "name" and "balance"
        /// Balance is 0 by default
        /// Does not accept negative numbers
        /// </summary>
        /// <param name="name"></param>
        /// <param name="balance"></param>
        public Account (string name, decimal balance = 0)
        {
            if (balance < 0)
            {
                throw new ArgumentOutOfRangeException("Startsaldot för kontot kan inte vara mindre än 0.");
            }

            Name = name;
            Balance = balance;
        }
        /// <summary>
        /// Adds "amount" to the account balance and returns true if it succeeds
        /// returns false if the amount is less than 0
        /// </summary>
        /// <param name="amount"></param>
        public bool Deposit(decimal amount)
        {
            if (amount < 0)
            {
                return false;
            }

            Balance += amount;
            return true;
        }
        /// <summary>
        /// Removes "amount" from the account balance if there is enough to withdraw and returns true
        /// returns false if balance is less than amount, or if amount is 0 or less
        /// </summary>
        /// <param name="amount"></param>
        public bool Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                return false;
            }
            if (Balance < amount)
            {
                return false;
            }

            Balance -= amount;
            return true;
        }
    }
}
