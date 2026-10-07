using Bankomaten.Data;
using Bankomaten.Domain;
using Bankomaten.Services;

namespace Bankomaten.Login
{
    internal class UserLogin
    {
        private int loginAttempsCounter = 0;

        public void StartBankomaten()
        {
            UserInput();
        }

        /// <summary>
        /// Get user input (username and pin code) and validate the format so it's not null, empty or whitespace.
        /// When input format is validated, call the "UserCredentialAuthentication" for credential authentication.
        /// </summary>
        private void UserInput()
        {
            bool isUsernameInputFormatOK = false;
            bool isPinInputFormatOk = false;
            string usernameInput = string.Empty;
            string userPinInput = string.Empty;

            // Check if user input format is null, empty or whitespace
            while (!isUsernameInputFormatOK)
            {
                Console.WriteLine("Skriv in ditt användarnamn");
                usernameInput = Console.ReadLine()!;
                Console.WriteLine();

                if (string.IsNullOrEmpty(usernameInput) || string.IsNullOrWhiteSpace(usernameInput))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Inskrivet format är felaktigt.\n");
                    Console.ResetColor();
                    isUsernameInputFormatOK = false;
                }
                else
                {
                    isUsernameInputFormatOK = true;
                }
            }

            // Check if user input format is null, empty or whitespace
            while (!isPinInputFormatOk)
            {
                Console.WriteLine("Skriv in din pinkod");
                userPinInput = Console.ReadLine()!;

                if (string.IsNullOrEmpty(userPinInput) || string.IsNullOrWhiteSpace(userPinInput))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Inskrivet format är felaktigt.\n");
                    Console.ResetColor();
                    isPinInputFormatOk = false;
                }
                else
                {
                    isPinInputFormatOk = true;
                }
            }

            UserCredentialAuthentication(usernameInput, userPinInput);
        }

        /// <summary>
        /// Monitor the number of login attempts.
        /// If login attempts is equal to 3, call the block-user function.
        /// </summary>
        private void CheckLoginAttemps()
        {
            loginAttempsCounter++;

            if (loginAttempsCounter == 3)
            {
                BlockUserTimePeriod();
            }
            else
            {
                UserInput();
            }
        }
        
        /// <summary>
        /// Block user from interact with the program.
        /// The user will be able to see the countdown. The console get clear after each count to not fill the entire
        /// console window with messages.
        /// </summary>
        private void BlockUserTimePeriod()
        {
            int blockWaitingTime = 60; 

            while (blockWaitingTime >= 0)
            {
                Thread.Sleep(1000); // Block computer thread for 1s (timeout)
                Console.Clear();

                blockWaitingTime--; // Count down 1s

                Console.WriteLine("Du har angett fel användarnam eller lösenord för många gånger.");
                Console.WriteLine($"Du är nu därför spärrad!\n");
                Console.WriteLine("Återstående tid: " + blockWaitingTime.ToString());
            }

            loginAttempsCounter = 0; // Reset loginAttemps

            Console.Clear();
            UserInput(); // After the blocking time start from where user enter the credentials.
        }

        /// <summary>
        /// Get all users from "SeedData" and authenticate user input credentials with the stored credentials.
        /// The comparison is not case-sensitive.
        /// </summary>
        /// <returns></returns>
        private void UserCredentialAuthentication(string usernameInput, string userPinInput)
        {
            //List<User> allUsers = SeedData.Users;
            BankService bankService = new BankService(SeedData.GenerateUsers());
            List<User> allUsers = bankService._users.Values.ToList();

            foreach (User user in allUsers)
            {
                // Check if "user" is not null and if "user.Username" & "usernameInput" is equal, ignoring case sensitive,
                // and that that the pin is correct for the username.
                if (user != null && string.Equals(user.UserName, usernameInput, StringComparison.OrdinalIgnoreCase) &&
                    user.Pin == userPinInput)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Clear();
                    Console.WriteLine("\nInloggningen lyckades\n");
                    Console.ResetColor();
                }
                else
                {
                    CheckLoginAttemps();
                }
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Användarnamnet eller lösenordet är felaktigt!\n");
            Console.ResetColor();
        }
    }
}
