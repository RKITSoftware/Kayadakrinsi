/// <summary>
/// Abstract class Animal
/// </summary>
public abstract class Animal{

    /// <summary>
    /// Name of animal
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Sound of animal
    /// </summary>
    public string Sound { get; set; }

    /// <summary>
    /// Abstract method display
    /// </summary>
    public abstract void Display();
    
}

/// <summary>
/// Class Dog inherits abstract class Animal
/// </summary>
public class Dog : Animal
{
    /// <summary>
    /// Constructor 
    /// </summary>
    /// <param name="name">Name of dog</param>
    /// <param name="sound">Sound of dog</param>
   public Dog(string name,string sound) : base()
    {
        Name = name;
        Sound = sound;
    }

    /// <summary>
    /// Overriding mabstract method Display
    /// </summary>
    public override void Display()
    {
        Console.WriteLine($"Animal {Name} sounds like {Sound}");
    }
}


