using System;
using System.Collections.Generic;
using System.Text;
using Bankomaten.Domain;

namespace Bankomaten.Login
{
    internal class UserLogin
    {
        private string? userName;
        private int loginAttempsCounter = 0;

        public void WelcomeMessage()
        {
            Console.WriteLine("Välkommen till bankomaten");
            Console.WriteLine();

            GetUsernameFromUser();
            GetPinFromUser(userName!);
        }

        /// <summary>
        /// Get username from the user and validate the input.
        /// </summary>
        private void GetUsernameFromUser()
        {
            bool isUserNameOk = false;

            while (!isUserNameOk)
            {
                Console.WriteLine("Skriv in ditt användarnamn");
                userName = Console.ReadLine()!;

                if (string.IsNullOrEmpty(userName) || string.IsNullOrWhiteSpace(userName))
                {
                    Console.WriteLine("Inskrivet format är felaktigt.");
                    isUserNameOk = false;
                }
                else
                {
                    isUserNameOk = true;

                    // Check if username exists


                }
            }
        }

        /// <summary>
        /// Get user pin and validate the format.
        /// Check if the pin is correct together with the username.
        /// </summary>
        /// <param name="username"></param>
        private void GetPinFromUser(string username)
        {
            bool isUserPinOk = false;

            while (!isUserPinOk)
            {
                Console.WriteLine("Skriv in ditt lösenord");
                bool intVerify = int.TryParse(Console.ReadLine(), out int pin);

                if (intVerify)
                {
                    // Compare if password is correct and belongs to the correct username.

                    isUserPinOk = true;
                }
                else
                {
                    Console.WriteLine("Inskrivet format är felaktigt.");
                    CheckLoginAttemps();
                }
            }
        }

        private void CheckLoginAttemps()
        {
            loginAttempsCounter++;

            if (loginAttempsCounter == 3)
            {
                BlockUserTimePeriod();
            }
        }
        
        /// <summary>
        /// Block user from interact with the program after 3 failed login attempts.
        /// </summary>
        private void BlockUserTimePeriod()
        {
            int blockWaitingTime = 0;
            int blockTime = 60;

            while (blockWaitingTime < blockTime)
            {
                Thread.Sleep(1000); // Block computer thread for 1 second
                blockWaitingTime++; // Count 1 second

                Console.WriteLine("Remaining time: " + (blockTime - 1).ToString());
                Console.Clear();
            }
            loginAttempsCounter = 0; // Reset loginAttemps
        }
    }
}
