using Bankomaten.Domain;

namespace Bankomaten.UI.MenuOptions
{
    public interface IMenuOptions
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
