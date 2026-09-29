namespace Bankomaten.UI;

public class ConsoleInputValidation 
{
    
    public int ReadInteger(string question, int min, int max)
    {
        if (min > max)
        {
            throw new ArgumentOutOfRangeException(nameof(max), "Max must be greater than or equal to min.");
        }

        Console.WriteLine(question);

        int result;

        while (!int.TryParse(ReadLineOrThrow(), out result) || result < min || result > max)
        {
            Console.WriteLine($"Ogiltig inmatning. Ange ett tal mellan {min} och {max}.");
            Console.WriteLine(question);
        }

        return result;
    }

    /// <summary>
    /// Reads a line from the console.
    /// </summary>
    /// <returns> The entered line. </returns>
    /// <exception cref="EndOfStreamException"> Thrown when input is closed, to avoid null input </exception>
    private static string ReadLineOrThrow()
    {
        return Console.ReadLine() ?? throw new EndOfStreamException("Input ended unexpectedly.");
    }
}