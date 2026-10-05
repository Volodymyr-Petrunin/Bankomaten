using Bankomaten.Domain;
using Bankomaten.UI.MenuOptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bankomaten.UI
{
    public class MainMenu
    {
        IMenuOptions[] _options;

        public MainMenu(IMenuOptions[] options)
        {
            _options = options;
        }

        public void Run(User user)
        {
            bool running = true;
            while (running)
            {
                Console.Clear();
                Console.WriteLine($"Välkommen {user.UserName}!");
            }
        }


    }
}
