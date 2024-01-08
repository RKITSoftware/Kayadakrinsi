using System;
using System.Text;

/// <summary>
/// StringBuilderDemo class is uded to demonstrate how to create instance of 'StringBuilder' class and how to 
/// implement it's properties and methods
/// </summary>
public class StringBuilderDemo
{
    #region Public Methods
    public static void Main()
	{
		StringBuilder firstName = new StringBuilder();
		StringBuilder lastName = new StringBuilder("Kayada");
		Console.WriteLine(firstName.ToString());
		Console.WriteLine(lastName.ToString());	
		firstName.Append("Krinsi");
        Console.WriteLine("After appending : "+firstName);
		Console.WriteLine("Capacity of latName : "+lastName.Capacity);
		Console.WriteLine("Length of lastName : " + lastName.Length);
		Console.WriteLine("Max capacity : "+lastName.MaxCapacity);
		Console.WriteLine("Is equals : "+Equals(firstName, lastName));
		Console.WriteLine("After inserting true at index 1 in lastName : " + lastName.Insert(1, true));
		Console.WriteLine("Replace : " + lastName.Replace("Kay", "HI"));
		lastName.Clear();
		Console.WriteLine("After clearing lastName : ", lastName);
    }

    #endregion

}
