/// <summary>
/// Abstract class Animal2
/// </summary>
public abstract class Animal2
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
/// Inherits abstract class Animal2
/// </summary>
public class Dog2 : Animal2
{
    /// <summary>
    /// Overrides abstract property
    /// </summary>
    public override string Sound => "Bark";

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="name">Name of Dog</param>
    public Dog2(string name) : base()
    {
        Name = name;
    }
}

