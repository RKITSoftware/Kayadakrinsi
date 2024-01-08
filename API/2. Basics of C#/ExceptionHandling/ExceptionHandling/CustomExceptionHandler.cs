using System;
/// <summary>
/// CustomExceptionHandler class is used to demonstrate how to implement custom exception handler
/// </summary>
public class CustomExceptionHandler
{
    #region Private Methods
    static void checkAge(int age)
    {
        if (age < 18)
        {
            throw new ArithmeticException("Access denied - You must be at least 18 years old.");
        }
        else
        {
            Console.WriteLine("Access granted - You are old enough!");
        }
    }

    static void Main(string[] args)
    {
        checkAge(20);
    }

    #endregion

}
