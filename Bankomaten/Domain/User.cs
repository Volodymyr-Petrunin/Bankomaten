using System.Text;

namespace Bankomaten.Domain
{
    public class User
    {

        /// <summary>
        /// Username of the user.
        /// </summary>
        public string UserName { get; private set; }
        /// <summary>
        /// Pin for the user.
        /// </summary>
        public string Pin { get; private set; }

        /// <summary>
        /// List of accounts for the user
        /// </summary>
        public List<Account> Accounts { get; private set; }

        /// <summary>
        /// Gets the transaction history for the user.
        /// </summary>
        public Queue<Transaction> TransactionsHistory { get; private set; }

        /// <summary>
        /// Constructor for new user, with an empty list Accounts and queue TransactionsHistory.
        /// </summary>
        public User(string userName, string pin)
        {
            UserName = userName;
            Pin = pin;

            Accounts = new List<Account>();
            TransactionsHistory = new Queue<Transaction>();
        }

        /// <summary>Just overriding ToString</summary>
        /// <returns>Returns string with contains main info</returns>
        public override string ToString()
        {
            return new StringBuilder()
                .Append("User name: " + UserName)
                .Append(" Pin: " + Pin)
                .Append(" Accounts: " + Accounts.Count)
                .Append(" Transactions: " + TransactionsHistory.Count)
                .ToString();
        }
    }
}
