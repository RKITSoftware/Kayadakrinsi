using System.Text;
/// <summary>
/// StringDemo class is used to demonstrate how to create instance of 'String' class as well as how to impelement 
/// it's methods and properties
/// </summary>
public class StringDemo
{
    #region Public Methods
    public static void Main(string[] args)
    {

        char[] chars = { 'H', 'E', 'L', 'L', 'O', ' ', 'W', 'O', 'R', 'L', 'D' };
        string stringFromChars = new string(chars);
        Console.WriteLine("String from char array : " + stringFromChars);

        string stringRepetativeChar = new string('A', 10);
        Console.WriteLine("String with repetative char : " + stringRepetativeChar);

        Console.WriteLine("String length : " + stringFromChars.Length);
        Console.WriteLine("Accessing string char : " + stringFromChars[0]);
        Console.WriteLine("Clone : " + stringFromChars.Clone() + " Type : " + stringFromChars.Clone().GetType());
        Console.WriteLine("Substring : " + stringFromChars.Substring(3, 5));
        Console.WriteLine("Split string : " + stringFromChars.Split(' ')[0]);
        Console.WriteLine("Index of 'L' : " + stringFromChars.IndexOf('L'));
        Console.WriteLine("Last index of 'L' : " + stringFromChars.LastIndexOf('L'));
        Console.WriteLine("Compare : " + stringFromChars.CompareTo(stringRepetativeChar));
        Console.WriteLine("Lower case : " + stringFromChars.ToLower());
        Console.WriteLine("Upper case : " + stringFromChars.ToUpper());
        Console.WriteLine("Is empty : " + string.IsNullOrEmpty(stringFromChars));
        Console.WriteLine("Replace  : " + stringFromChars.Replace('L', 'K'));
        Console.WriteLine("Remove 'H' : " + stringFromChars.Remove(1, 2));
        char[] charArray = stringFromChars.ToCharArray();
        Console.WriteLine("String to char array : ");
        foreach (char c in charArray)
        {
            Console.WriteLine(c);
        }

    }

    #endregion

}