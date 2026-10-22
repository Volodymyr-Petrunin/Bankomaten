using Bankomaten.Domain;
using Bankomaten.Services;
using Bankomaten.UI;

namespace Bankomaten.Login
{
    internal class UserLogin (UserManagement userManagement, ConsoleInputValidation consoleInputValidation)
    {
        private const int MaxAttempts = 3;
        
        private int _loginAttemptsCounter = 0;

        /// <summary>
        /// Handle user input information.
        /// </summary>
        public User UserInput()
        {
            // The loop continues until the user has successfully logged in to prevent return of a null-value
            while (true)
            {
                Console.WriteLine();
                string usernameInput = consoleInputValidation.ReadText("Ange användarnamn: ");
                Console.WriteLine();

                string userPinInput = consoleInputValidation.HandlePinUserInput("Ange din PIN-kod: ", true);

                User? user = UserCredentialAuthentication(usernameInput, userPinInput);

                if (user is null)
                {
                    CheckLoginAttempts();
                }
                else
                {
                    _loginAttemptsCounter = 0; // Reset loginAttemps
                    return user;
                }
            }
        }

        /// <summary>
        /// Monitor the number of login attempts.
        /// If login attempts is equal to 3, call the block-user function.
        /// </summary>
        private void CheckLoginAttempts()
        {
            _loginAttemptsCounter++;

            if (_loginAttemptsCounter == MaxAttempts)
            {
                BlockUserTimePeriod();
            }
        }
        
        /// <summary>
        /// Block user from interact with the program.
        /// The user will be able to see the countdown. The console get clear after each count to not fill the entire
        /// console window with messages.
        /// </summary>
        private void BlockUserTimePeriod()
        {
            int blockWaitingTime = 180; 

            while (blockWaitingTime >= 0)
            {
                Thread.Sleep(1000); // Block computer thread for 1s (timeout)
                Console.Clear();

                blockWaitingTime--; // Count down 1s

                Console.WriteLine("Du har angett fel användarnamn eller PIN-kod för många gånger.");
                Console.WriteLine($"Temporär spärr aktiverad" + Environment.NewLine);
                Console.WriteLine("Återstående tid: " + blockWaitingTime.ToString() + "s");
            }

            _loginAttemptsCounter = 0; // Reset loginAttemps

            Console.Clear();
        }

        /// <summary>
        /// Authenticate user input credentials with the stored credentials.
        /// </summary>
        /// <returns>User object if authenticate otherwise null</returns>
        /// <exception cref="ArgumentNullException">Throw exception if userManagement object is null</exception>
        private User? UserCredentialAuthentication(string usernameInput, string userPinInput)
        {
            ArgumentNullException.ThrowIfNull(userManagement);

            User? user = userManagement.Authenticate(usernameInput, userPinInput);
        
            if (user is null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Användarnamnet eller PIN-koden är felaktigt!" + Environment.NewLine);
                Console.ResetColor();
                Console.WriteLine($"Försök {_loginAttemptsCounter + 1}/3." + Environment.NewLine);
        
                return null;
            }
        
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(Environment.NewLine + "Inloggningen lyckades" + Environment.NewLine);
            Console.ResetColor();
            
            return user;
        }
    }
}
