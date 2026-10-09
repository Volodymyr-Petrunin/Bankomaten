using Bankomaten.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bankomaten.UI.MenuOptions
{
    public interface IMenuOption
    {
        /// <summary>
        /// Title of the menu option.
        /// </summary>
        string Title { get; }
        /// <summary>
        /// Executes the menu option for the given user.
        /// </summary>
        void Execute(User user);
    }
}
