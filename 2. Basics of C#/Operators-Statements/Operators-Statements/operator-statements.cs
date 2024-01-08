using System.Diagnostics;

public class Program
{
    #region Public Methods

    public static void Main(string[] args)
    {
        int age = 20;

        //Concatination string using + operator
        string firstName = "Krinsi";
        string lastName = "Kayada";
        string fullName = firstName + " " + lastName;
        Console.WriteLine(fullName + "\n");

        //Arithmetic Operators
        Console.WriteLine("Age plus 2 : " + (age + 2));
        Console.WriteLine("Age minus 2 : " + (age - 2));
        Console.WriteLine("Age into 2 : " + (age * 2));
        Console.WriteLine("Age divide by 2 : " + (age / 2));
        Console.WriteLine("Age module 2 : " + (age % 2));
        Console.WriteLine("Age post incremented : " + (age++));
        Console.WriteLine("Age pre incremeted : " + (++age));
        Console.WriteLine("Age post decremented : " + (age--));
        Console.WriteLine("Age pre decremented : " + (--age) + "\n");

        //Comparison Operators
        Console.WriteLine("Is age less than 18 : " + (age < 18));
        Console.WriteLine("Is age greater than 18 : " + (age > 18));
        Console.WriteLine("Is age less or equal to 18 : " + (age <= 18));
        Console.WriteLine("Is age greater or equal to 18 : " + (age >= 18));
        Console.WriteLine("Is age equal to 18 : " + (age == 18));
        Console.WriteLine("Is age not equal to 18 : " + (age != 18) + "\n");

        //Logical Operators
        Console.WriteLine("Logical or : " + (age < 18 || age > 0));
        Console.WriteLine("Logical and : " + (age < 18 && age > 0));
        Console.WriteLine("Logical not : " + !(age < 18 || age > 0) + "\n");

        //Assignment Operator
        int value = 10;
        Console.WriteLine("Value : " + value);
        value += 5;
        Console.WriteLine("Value += 5 : " + value);
        value -= 5;
        Console.WriteLine("Value -= 5 : " + value);
        value *= 5;
        Console.WriteLine("Value *= 5 : " + value);
        value /= 5;
        Console.WriteLine("Value /= 5 : " + value);
        value %= 5;
        Console.WriteLine("Value %= 5 : " + value);
        value = 20;
        Console.WriteLine("Value : " + value);
        value &= 5;
        Console.WriteLine("Value &= 5 : " + value);
        value |= 5;
        Console.WriteLine("Value |= 5 : " + value);
        value ^= 5;
        Console.WriteLine("Value ^= 5 : " + value);
        value >>= 5;
        Console.WriteLine("Value >>= 5 : " + value);
        value <<= 5;
        Console.WriteLine("Value <<= 5 : " + value + "\n");

        //Ternary Operator
        Console.WriteLine(value == 20 ? "Value is 20\n" : "Value is not 20\n");

        //Loops
        Console.WriteLine("\nPrinting first name using for loop...");
        for (int i = 0; i < firstName.Length; i++)
        {
            Console.WriteLine(firstName[i]);
        }

        Console.WriteLine("\nPrinting last name using while loop...");
        int j = 0;
        while (j < lastName.Length)
        {
            Console.WriteLine(lastName[j]);
            j++;
        }

        Console.WriteLine("\nPrinting full name using do while loop...");
        int k = 0;
        do
        {
            Console.WriteLine(fullName[k]);
            k++;
        } while (k < fullName.Length);

        // Switch case
        Console.WriteLine("\nSwitch case...");
        int day = 3;
        switch (day)
        {
            case 0:
                {
                    Console.WriteLine("Sunday");
                    break;
                }
            case 1:
                {
                    Console.WriteLine("Monday");
                    break;
                }
            case 2:
                {
                    Console.WriteLine("Tuesday");
                    break;
                }
            case 3:
                {
                    Console.WriteLine("Wednesday");
                    break;
                }
            default:
                {
                    Console.WriteLine("Happy day");
                    break;
                }

        }
    }

    #endregion
}