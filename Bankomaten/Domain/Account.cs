using System.Text;

namespace Bankomaten.Domain
{
    public class Account
    {

        /// <summary>
        /// Get/Set name for the account.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Get/Set current balance of the account.
        /// Balance is private so it can't be changed to a negative outside the class.
        /// </summary>
        private decimal Balance { get; set; }

        /// <summary>
        /// Constructor for a new account.
        /// </summary>
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
        /// Adds the amount submitted to the accounts balance.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the deposit was successful.
        /// <c>false</c> if the deposit was less than 0.
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
        /// Removes the submitted amount from the account balance.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the withdrawal was successful.
        /// <c>false</c> if the amount was negative or 0, 
        /// or if the amount was more than the account balance.
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

        /// <summary>
        /// Just return Balance decimal.
        /// </summary>
        /// <returns></returns>
        public decimal GetBalance() => Balance;

        /// <summary>Just overriding ToString</summary>
        /// <returns>Returns string with contains main info</returns>
        public override string ToString()
        {
            return $"Account Name: {Name}, Balance: {Balance:C}";
        }
    }
}
