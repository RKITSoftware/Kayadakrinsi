using System;
using System.Runtime.InteropServices;

public class User
{
    #region Public Members

    public string name;
    public int age;
    public int mobileNumber;
    public bool isUser = true;

    public struct Address
    {
        public string city;
        public int pinCode;
    }
    public Address address;

    public enum enmBusinessType
    {
        small,
        retail,
        large
    }
    enmBusinessType businessType = 0;

    #endregion


    #region Constructors

    public User(string name, int age, int mobileNo, string city, int pinCode)
    {
        this.name = name;
        this.age = age;
        this.mobileNumber = mobileNo;
        this.address.city = city;
        this.address.pinCode = pinCode;
    }

    #endregion


    #region Public Methods

    // Displays information of User
    public void DisplayInfo()
    {
        Console.WriteLine("Users details...");
        Console.WriteLine("Name : " + name);
        Console.WriteLine("Age : " + age);
        Console.WriteLine("Mobile Number : " + mobileNumber);
        Console.WriteLine("Adress : " + address.pinCode + " , " + address.city);
        Console.WriteLine("Business Type : " + businessType);
        Console.WriteLine("Is " + name + " real user ? : " + isUser);
    }
    public static void Main(string[] args)
    {
        //Type casting
        char ch = 'K';
        int number = 20;
        int convertedChar = ch;
        string convertedInt = Convert.ToString(number);
        Console.WriteLine("Converted K into int : " + convertedChar);
        Console.WriteLine("Converted number into string : " + convertedInt + "\nType of converted number : " + convertedInt.GetType() + "\n");
        
        //Data types and variables
        User user = new User("Krinsi", 20, 1234567890, "Rajkot", 360002);
        user.DisplayInfo();

    #endregion

    }
}
