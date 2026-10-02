using System;
using System.Collections.Generic;
using System.Text;

namespace Bankomaten.Domain
{
    public class Account
    {

        /// <summary>
        /// The name of the account
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Current Balance of the account,
        /// balance is private so it can't be changed to a negative outside the class
        /// </summary>
        public decimal Balance { get; private set; }

        /// <summary>
        /// Constructor for a new account,
        /// eg: Account TestKonto = new Account("TestKonto",111.111m);
        /// </summary>
        /// <param name="name">The name of the account</param>
        /// <param name="balance">The starting account balance, 0 by default</param>
        /// <exception cref="ArgumentOutOfRangeException">Throw when <paramref name="balance"/> is less than 0</exception>
        public Account(string name, decimal balance = 0)
        {
            if (balance < 0)
            {
                throw new ArgumentOutOfRangeException("Startsaldot för kontot kan inte vara mindre än 0.");
            }

            Name = name;
            Balance = balance;
        }

        /// <summary>
        /// Adds amount to the accounts balance
        /// </summary>
        /// <param name="amount">The amount to add to the account balance</param>
        /// <returns>
        /// <c>true</c> if the deposit was successful
        /// <c>false</c> if the deposit was less than 0
        /// </returns>
        public bool Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                return false;
            }

            Balance += amount;
            return true;
        }

        /// <summary>
        /// Removes amount from the account balance
        /// </summary>
        /// <param name="amount">The amount to remove from the account balance</param>
        /// <returns>
        /// <c>true</c> if the withdrawal was successful
        /// <c>false</c> if the amount was negative or 0, 
        /// or if the amount was more than the account balance
        /// </returns>
        public bool Withdraw(decimal amount)
        {
            if (amount <= 0 || Balance < amount) 
            {
                return false;
            }

            Balance -= amount;
            return true;
        }
    }
}
