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
        void Execute(User user);
    }
}
