using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Bankomaten.Data;
using Bankomaten.Domain;
using Bankomaten.UI;

namespace Bankomaten.Login
{
    internal class UserLogin
    {
        private string? usernameInput;
        private int loginAttempsCounter = 0;

        public void StartBankomaten()
        {
            Console.WriteLine("===== Välkommen till bankomaten =====");
            Console.WriteLine();

            GetUsernameFromUserInput();
            GetPinFromUserInput(usernameInput!);
        }

        /// <summary>
        /// Get username from the user and validate the input.
        /// </summary>
        private void GetUsernameFromUserInput()
        {
            bool isUserNameOk = false;

            while (!isUserNameOk)
            {
                Console.WriteLine("Skriv in ditt användarnamn");
                usernameInput = Console.ReadLine()!;

                if (string.IsNullOrEmpty(usernameInput) || string.IsNullOrWhiteSpace(usernameInput))
                {
                    // NOTE: Login attempts is not logged here. The logins are counted within "GetPinFromUserInput"-method

                    Console.WriteLine("Inskrivet format är felaktigt.");
                    isUserNameOk = false;
                }
                else
                {
                    isUserNameOk = true;
                    bool usernameValidation = UsernameValidation(usernameInput);
                }
            }
        }

        /// <summary>
        /// Get user pin and validate the format.
        /// Check if the pin is correct together with the username.
        /// </summary>
        /// <param name="username"></param>
        private void GetPinFromUserInput(string userPin)
        {
            bool isUserPinOk = false;

            while (!isUserPinOk)
            {
                //Console.WriteLine("Skriv in ditt lösenord");
                //bool intVerify = int.TryParse(Console.ReadLine(), out int pin);

                ConsoleInputValidation civ = new ConsoleInputValidation();

                int inputPinCode = civ.ReadInteger("Skriv in ditt lösenord", 0000, 9999); // Input validation

                UserPinValidation(inputPinCode);

            }
        }

        /// <summary>
        /// Monitor the number of login attempts.
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
        /// Block user from interact with the program after 3 failed login attempts.
        /// </summary>
        private void BlockUserTimePeriod()
        {
            int blockWaitingTime = 0;
            int blockTime = 60; 

            while (blockWaitingTime < blockTime)
            {
                Thread.Sleep(1000); // Block computer thread for 1 second
                blockWaitingTime++; // Count up 1 second

                Console.WriteLine("Remaining time: " + (blockTime - 1).ToString());
                Console.Clear();
            }
            loginAttempsCounter = 0; // Reset loginAttemps
            StartBankomaten(); // After the blocking time start from beginning again.
        }

        /// <summary>
        /// Iterate through all users and compare if the username that the user entered
        /// </summary>
        /// <param name="usernameInput"></param>
        /// <returns></returns>
        private bool UsernameValidation(string usernameInput)
        {
            User user = new User(usernameInput, 0000);

            if (SeedData.UserData  != null)
            {
                //foreach (User user in SeedData.UserData)
                //{
                //    //if (user.UserName == usernameInput && user != null)
                //    //{
                //    //    return true;
                //    //}

                if (SeedData.UserData.Comparer.Compare(usernameInput, User.UserName) == 0)
                {

                }
                //}
            }
            return false;
        }

        private bool UserPinValidation(int inputPinCode)
        {
            if (SeedData.UserData != null)
            {
                foreach (User user in SeedData.UserData)
                {
                    if (user != null)
                    {
                        if (user.UserName == usernameInput && user.Pin == inputPinCode)
                        {
                            return true;
                        }

                        CheckLoginAttemps();
                    }
                }
            }
            return false;
        }
    }
}
