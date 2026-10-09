using Bankomaten.UI;

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
        /// Loops the username and pin code separately so the user do not need to enter everything if any user failure.
        /// When input format is validated, call the "UserCredentialAuthentication" for credential authentication.
        /// </summary>
        private void UserInput()
        {
            bool isUsernameInputFormatOK = false;
            bool isPinInputFormatOk = false;
            string usernameInput = string.Empty;
            string userPinInput = string.Empty;

            ConsoleInputValidation civ = new ConsoleInputValidation();

            while (!isUsernameInputFormatOK)
            {
                Console.Write("Skriv in ditt användarnamn: ");
                usernameInput = Console.ReadLine()!.Trim();
                Console.WriteLine();

                // Check if the user input is not null, empty or whitespace
                if (civ.IsStringValid(usernameInput))
                {
                    isUsernameInputFormatOK = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Inskrivet format är felaktigt.\n");
                    Console.ResetColor();
                    isUsernameInputFormatOK = false;
                }
            }

            while (!isPinInputFormatOk)
            {
                Console.Write("Skriv in din pinkod: ");
                userPinInput = Console.ReadLine()!.Trim();
                Console.WriteLine();

                // Check if the user input is not null, empty or whitespace and if it only contain digits
                if (civ.IsStringValid(userPinInput) && civ.IsStringDigits(userPinInput))
                {
                    isPinInputFormatOk = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Inskrivet format är felaktigt.\n");
                    Console.ResetColor();
                    isPinInputFormatOk = false;
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
                Console.WriteLine($"Temporär spärr aktiverad!\n");
                Console.WriteLine("Återstående tid: " + blockWaitingTime.ToString() + "s");
            }

            loginAttempsCounter = 0; // Reset loginAttemps

            Console.Clear();
            UserInput(); // After the blocking time, start from where user enter the credentials.
        }

        /// <summary>
        /// Authenticate user input credentials with the stored credentials.
        /// </summary>
        /// <returns></returns>
        private void UserCredentialAuthentication(string usernameInput, string userPinInput)
        {
            UserManagement um = new UserManagement();

            if (um.Authenticate(usernameInput, userPinInput))
            {
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Clear();
                    Console.WriteLine("\nInloggningen lyckades\n");
                    Console.ResetColor();

                    return;
                }
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Användarnamnet eller lösenordet är felaktigt!\n");
            Console.ResetColor();
            Console.WriteLine($"Försök {loginAttempsCounter + 1}/3.\n");
            CheckLoginAttemps();
        }
    }
}
