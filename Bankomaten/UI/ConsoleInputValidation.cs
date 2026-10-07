namespace Bankomaten.UI;

/// <summary>
/// Reads and validates user input from the console.
/// Relies on CultureInfo.CurrentCulture being set to sv-SE in Program class.
/// </summary>
public class ConsoleInputValidation 
{
    /// <summary> Asks until the user enters an integer between min and max.</summary>
    /// <returns>A valid integer between min and max.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Throws only when min param is bigger than max param</exception>
    public int ReadInteger(string question, int min, int max)
    {
        ArgumentOutOfRangeException.ThrowIfGreaterThan(min, max);

        Console.Write(question);

        int result;

        while (!int.TryParse(ReadLineOrThrow(), out result) || result < min || result > max)
        {
            Console.WriteLine($"Ogiltig inmatning. Ange ett tal mellan {min} och {max}.");
            Console.Write(question);
        }

        return result;
    }

    /// <summary> Asks until the user enters an amount between min and max with at most two decimals.</summary>
    /// <returns>A valid amount between min and max.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Throws only when min param is bigger than max param</exception>
    public decimal ReadDecimal(string question, decimal max, decimal min = 0.01m)
    {
        ArgumentOutOfRangeException.ThrowIfGreaterThan(min, max);
        
        Console.Write(question);

        decimal result;

        while (!IsValidDecimal(ReadLineOrThrow(), min, max, out result))
        {
            Console.WriteLine($"Ogiltigt belopp. Ange ett belopp mellan {min:C} och {max:C}, med max två decimaler.");
            Console.Write(question);
        }

        return result;
    }

    /// <summary>
    /// Checks that the input is a number between min and max with at most two decimals.
    /// And "." is accepted as well as "," so both 238,50 and 238.50 will work.
    /// </summary>
    /// <returns>True if the input is a valid amount, otherwise false.</returns>
    private static bool IsValidDecimal(string input, decimal min, decimal max, out decimal amount)
    {
        bool isNumber = decimal.TryParse(input.Replace('.', ','), out amount);
        bool isInRange = amount >= min && amount <= max;
        bool hasAtMostTwoDecimals = decimal.Round(amount, 2) == amount;

        return isNumber && isInRange && hasAtMostTwoDecimals;
    }

    /// <summary> Reads a line from the console.</summary>
    /// <returns>The entered line.</returns>
    /// <exception cref="EndOfStreamException">Thrown when input is closed, to avoid null input</exception>
    private static string ReadLineOrThrow()
    {
        return Console.ReadLine() ?? throw new EndOfStreamException("Input ended unexpectedly.");
    }
}