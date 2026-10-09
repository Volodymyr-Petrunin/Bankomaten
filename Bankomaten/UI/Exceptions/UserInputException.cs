namespace Bankomaten.UI.Exceptions;

/// <summary>
/// Represents an exception that is thrown when there is an error related to user input in the system.
/// Typically used in scenarios where user-provided input violates validation rules or business constraints.
/// </summary>
public class UserInputException(string message) : Exception(message);