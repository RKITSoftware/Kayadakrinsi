using System.Linq;
public class Program
{
    #region Public Methods

    public static void Main(string[] args)
    {
        // Creates array of string
        string[] pets = { "Dog", "Cat", "Panda", "Parrt", "Sparrow" };
        Console.WriteLine(pets[3]);

        // Accesses element of array using index
        pets[3] = "Parrot";
        Console.WriteLine(pets[3]);

        // Prints array using for loop
        Console.WriteLine("\nPrinting pets array using for...");
        for (int i = 0; i < pets.Length; i++)
        {
            Console.WriteLine(pets[i]);
        }

        // Prints array using foreach
        Console.WriteLine("\nPrinting pets array using foreach...");
        foreach (string pet in pets)
        {
            Console.WriteLine(pet);
        }

        // Sorts array and prints array
        Array.Sort(pets);
        Console.WriteLine("\nSorted pets array...");
        foreach (string pet in pets)
        {
            Console.WriteLine(pet);
        }

        // Prints maximum and minimum element of array
        Console.WriteLine("\nMax of pets is " + pets.Max());
        Console.WriteLine("\nMin of pets is " + pets.Min());

        // Creates array of integer and prints sum of all elements
        int[] numbers = { 1, 2, 3, 4, 5 };
        Console.WriteLine("\nSum of numbers is " + numbers.Sum());

        // Creates two dimentional array
        int[,] array = {
            {1,2,3 },
            {4,6,7 }
        };

        // Prints two dimentional array using foreach
        Console.WriteLine("\nPrinting two dimensional array using foreach...");
        foreach (int i in array)
        {
            Console.WriteLine(i);
        }

        // Prints two dimentional array using for loops
        Console.WriteLine("\nPrinting two dimensional array using for...");
        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                Console.WriteLine(array[i, j]);
            }
        }

        // Creates and prints jagged array
        Console.WriteLine("\n");
        int[][] jaggedArray = new int[3][];
        jaggedArray[0] = new int[] { 1, 2, 3, 4 };
        jaggedArray[1] = new int[] { 3, 4, 5 };
        jaggedArray[2] = new int[] { 1, 4, 5, 6, 7, 8 };
        for (int i = 0; i < jaggedArray.Length; i++)
        {
            Console.WriteLine("Elements of row " + i + " : ");
            for (int j = 0; j < jaggedArray[i].Length; j++)
            {
                Console.WriteLine(jaggedArray[i][j]);
            }
        }
    }

    #endregion

}