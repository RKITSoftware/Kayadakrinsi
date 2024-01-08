using System.Diagnostics.Contracts;

// Base Class Animal (implemets herarical inheritance)
public class Animal
{
    #region Public Members

    public static int noOfFeet = 4;
    public string? animalType;

    #endregion


    #region Public Methods

    // Displays sound of animal
    public void Sound()
    {
        Console.WriteLine("Animal sound...");
    }

    #endregion
}

// Class Dog inherits class Animal
public class Dog : Animal
{
    #region Public Members

    public string bark;

    #endregion


    #region Constructors

    public Dog(string animalType, string bark) : base()
    {
        this.animalType = animalType;
        this.bark = bark;
    }

    #endregion


    #region Public Methods

    // Overrides sound() method and displays sound of Dog
    public void Sound()
    {
        Console.WriteLine("Dog sound : " + this.bark);
    }

    #endregion

}

// Class Huskey inherits class Dog (MultiLevel inheritance)
public class Huskey : Dog
{
    #region Public Members

    public static string color = "White and grey";

    #endregion


    #region Constructors

    public Huskey(string animalType, string bark) : base(animalType,bark)
    {
        this.animalType = animalType;
        this.bark = bark;
    }

    #endregion


    #region Public Methods

    // Overrides sound() method and displays sound of Huskey
    public void Sound()
    {
        Console.WriteLine("Huskey sound : " + this.bark);
    }

    // Displays color of Huskey
    public void ColorDisplay()
    {
        Console.WriteLine("The color of Huskey is : " + Huskey.color);
    }

    #endregion

}

// Class Cat inherits class Animal
public class Cat : Animal
{
    #region Public Members

    public string purr;

    #endregion


    #region Constructors

    public Cat(string animalType, string purr)
    {
        this.animalType = animalType;
        this.purr = purr;
    }

    #endregion


    #region Public Methods

    // Overrides sound() method and displays sound of Cat
    public void Sound()
    {
        Console.WriteLine("Cat sound : " + this.purr);
    }

    #endregion

}

// Sealed class
sealed class Cars
{
    #region Public Members

    public static int noOfTyres = 4;
    public string Model { get; set; }
    public int topSpeed;
    public string color;

    #endregion


    #region Constructors

    public Cars() { }
    public Cars(string model, int topSpeed, string color)
    {
        Model = model;
        this.topSpeed = topSpeed;
        this.color = color;
    }

    #endregion


    #region Public Methods

    // Displays details of car
    public void Details()
    {
        Console.WriteLine("Car model : " + Model);
        Console.WriteLine("Top speed : " + topSpeed);
        Console.WriteLine("Color : " + color);
    }

    #endregion
}

// Try to inherit sealed class
//public class mustang : cars
//{

//}

public class ProgramInheritance
{
    public static void Main()
    {
        Console.WriteLine("Static property number of feet is " + Animal.noOfFeet);
        Animal animal = new Animal();
        animal.Sound();
        Console.WriteLine();

        Console.WriteLine("Static property number of feet is " + Dog.noOfFeet);
        Dog doggo = new Dog("dog", "Bhaw Bhaw...");
        doggo.Sound();
        Console.WriteLine();

        Console.WriteLine("Static property number of feet is " + Cat.noOfFeet);
        Cat milli = new Cat("Cat", "Meow Meow...");
        milli.Sound();
        Console.WriteLine();

        Console.WriteLine("Static property number of feet is " + Huskey.noOfFeet);
        Huskey huskey = new Huskey("Dog", "Howww Howww...");
        huskey.Sound();
        huskey.ColorDisplay();
        Console.WriteLine();

        Cars cars = new Cars("Bugatti", 450, "Black");
        cars.Details();
    }
}