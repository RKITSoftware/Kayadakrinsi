// Public class
public class PublicClass
{
    #region Private Members

    string _Name { get; set; }

    #endregion


    #region Public Members

    public static int age = 20;
    public string Description { get; set; }

    #endregion


    #region Constructors

    public PublicClass() { }

    public PublicClass(string name, string description)
    {
        this._Name = name;
        this.Description = description;
    }

    #endregion

    #region Public Methods

    // Displays information of about 
    public void About()
    {
        Console.WriteLine("Name : ", this._Name);
        Console.WriteLine("Age : ", PublicClass.age);
        Console.WriteLine("About : ", this.Description);
    }

    #endregion

}

// Child class of public class
public class ChildPublic : PublicClass
{
    #region Public Members

    public string City { get; set; }
    public ChildPublic() { }

    #endregion

    #region Public Methods

    public ChildPublic(string name, string description, string city)
    {
        _Name = name; // Private member of public class is not accessible here
        Description = description;
        City = city;
    }

    #endregion

}

// Private class
class PrivateClass
{
    #region Private Members

    string _Name { get; set; }

    #endregion


    #region Public Members

    public static int age = 20;
    public string Description { get; set; }

    #endregion

    #region Constructors

    public PrivateClass() { }

    public PrivateClass(string name, string description)
    {
        _Name = name;
        Description = description;
    }

    #endregion


    #region Private Methods 

    // Displays information of about 
    void About()
    {
        Console.WriteLine("Name : ", _Name);
        Console.WriteLine("Age : ", age);
        Console.WriteLine("About : ", Description);
    }

    #endregion


    #region Public Methods

    // Returns private member _Name publicly
    public string GetName()
    {
        return _Name;
    }

    // Returns private member _Name in protected mode
    //protected string GetName()
    //{
    //    return Name;
    //}

    #endregion

}

// Child class of Private class
public class ChildPrivate : PrivateClass // Not able to access Private class due to encapsulation
{
    public string _Name { get; set; }
}

public static class StaticClass //static classes are sealed hence not able to inheritance
{
    #region Private Methods

    static int _age = 22;

    #endregion


    #region Public Members

    public static string name = "Krinsi";

    #endregion


    #region Public Methods

    // Displays information of about 
    public static void About()
    {
        Console.WriteLine("Name : " + name);
        Console.WriteLine("Age : " + _age);
    }

    #endregion

}

public class ProgramEncapsulation
{
    public static void Main()
    {
        Console.WriteLine("Age from static class : " + StaticClass._age);
        Console.WriteLine("Name from static class : " + StaticClass.name);
        StaticClass.About();
        Console.WriteLine();

        Console.WriteLine("Age from public class : " + PublicClass.age);
        PublicClass publicClass = new PublicClass("Krinsi", "Hello, I am student");
        publicClass.About();

        Console.WriteLine("Age from private class : " + PrivateClass.age);
        PrivateClass privateClass = new PrivateClass("Kayada", "Hello, I am fine");
        privateClass.About();
        Console.WriteLine("Name from private class : " + privateClass._Name);
        string name = privateClass.GetName();
        Console.WriteLine("Name from private class : " + name);
    }
}