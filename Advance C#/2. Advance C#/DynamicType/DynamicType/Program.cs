/// <summary>
/// For testing dynamic type
/// </summary>
public class Program
{
    /// <summary>
    /// Id of object
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Name of object
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Prints object type (Dynamic type as function parameter)
    /// </summary>
    /// <param name="obj">dynamic object</param>
    public static void Print(dynamic obj)
    {
        dynamic objDynamic = obj;
        Console.WriteLine("{0}",objDynamic.GetType().ToString());
    }

    /// <summary>
    /// Dynamic as return type
    /// </summary>
    /// <param name="obj">dynamic object</param>
    /// <returns>dynamic object</returns>
    public static dynamic Show(dynamic obj)
    {
        return obj;
    }

    public static void Main(string[] args)
    {
        dynamic objDynamic;
        objDynamic = 32;
        Console.WriteLine($"1. {objDynamic}");

        objDynamic = "Sting";
        Console.WriteLine("2. {0}", objDynamic);

        objDynamic = 64.234;
        Console.WriteLine($"3. {objDynamic}");

        objDynamic = new Program();
        Console.WriteLine($"4. {objDynamic}");

        objDynamic.Id = 1;
        objDynamic.Name = "test";
        Console.WriteLine($"Id : {objDynamic.Id}, Name : {objDynamic.Name}");

        var returned = Show(objDynamic);
        Print(returned);
    }
}