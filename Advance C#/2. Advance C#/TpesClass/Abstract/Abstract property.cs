/// <summary>
/// Abstract class AnimalProperty
/// </summary>
public abstract class AnimalProperty
{

    /// <summary>
    /// Name of Animal
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Abstract Property Sound
    /// </summary>
    public abstract string Sound { get; }

    /// <summary>
    /// Displays Name and Sound of Animal
    /// </summary>
    public void Display()
    {
        Console.WriteLine($"Animal {Name} sounds like {Sound}");
    }

}

/// <summary>
/// Inherits abstract class AnimalProperty
/// </summary>
public class Cat : AnimalProperty
{
    /// <summary>
    /// Overrides abstract property
    /// </summary>
    public override string Sound => "Meow";

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="name">Name of Dog</param>
    public Cat(string name) : base()
    {
        Name = name;
    }
}

