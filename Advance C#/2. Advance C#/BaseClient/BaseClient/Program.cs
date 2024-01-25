using System.Text;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine(CustomStringBuilder.Append("Hello World!"));
        Console.WriteLine(CustomStringBuilder.Append(false));
        StringBuilder objStringBuilder = new StringBuilder("How are you");
        Console.WriteLine($"{objStringBuilder.Length}");
        Console.WriteLine(CustomStringBuilder.Clear(objStringBuilder));
    }
}