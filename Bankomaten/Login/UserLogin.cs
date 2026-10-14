using Bankomaten.Domain;
using Bankomaten.Services;
using Bankomaten.UI;
using Bankomaten.UI.MenuOptions;
using System.Text;
using System.Threading.Channels;

namespace Bankomaten.Login
{
    internal class UserLogin (UserManagement userManagement, ConsoleInputValidation consoleInputValidation)
    {
        private int loginAttempsCounter = 0;

        /// <summary>
        /// Handle user input information.
        /// </summary>
        public User UserInput()
        {
            User user;

            // The loop continues until the user has successfully logged in to prevent return of a null-value
            while (true)
            {
                string usernameInput = consoleInputValidation.ReadText("Ange användarnamn: ");
                Console.WriteLine();

                string userPinInput = consoleInputValidation.HandlePinUserInput("Ange din PIN-kod: ", true);

                user = UserCredentialAuthentication(usernameInput, userPinInput);

                if (user is null)
                {
                    CheckLoginAttemps();
                }
                else
                {
                    loginAttempsCounter = 0; // Reset loginAttemps
                    return user;
                }
            }
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

            loginAttempsCounter = 0; // Reset loginAttemps

            Console.Clear();
        }

        /// <summary>
        /// Authenticate user input credentials with the stored credentials.
        /// </summary>
        /// <returns></returns>
        private User UserCredentialAuthentication(string usernameInput, string userPinInput)
        {
            if (userManagement != null)
            {
                User? user = userManagement.Authenticate(usernameInput, userPinInput);

                if (user != null)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Clear();
                    Console.WriteLine(Environment.NewLine + "Inloggningen lyckades" + Environment.NewLine);
                    Console.ResetColor();

                    return user;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Användarnamnet eller lösenordet är felaktigt!" + Environment.NewLine);
                    Console.ResetColor();
                    Console.WriteLine($"Försök {loginAttempsCounter + 1}/3." + Environment.NewLine);
                }
            }
            return null!;
        }
    }
}
