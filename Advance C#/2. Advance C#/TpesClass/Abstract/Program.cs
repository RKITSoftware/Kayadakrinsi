/// <summary>
/// For testing abstract methods and properties
/// </summary>
public class Program
{
    public static void Main(string[] args)
    {
        Dog objDog = new Dog("Sheru", "Bark");
        objDog.Display();

        Cat objCat = new Cat("Kitty");
        objCat.Display();
    }
}