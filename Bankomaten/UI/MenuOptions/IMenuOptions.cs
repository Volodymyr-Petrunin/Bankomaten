using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
using Bankomaten.Domain;

namespace Bankomaten.UI.MenuOptions
{
    public interface IMenuOptions
    {
        /// <summary>
        /// Title of the menu option, used for display in the menu
        /// </summary>
        string Title { get; }

        /// <summary>
        /// Executes the menu option, takes a user as parameter
        /// </summary>
        /// <param name="user">The logged in user that executes the menu option</param>
        void Execute(User user);
    }
}
