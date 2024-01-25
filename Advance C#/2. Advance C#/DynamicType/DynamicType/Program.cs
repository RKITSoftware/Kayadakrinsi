public class Program
{
    public int Id { get; set; }
    public string Name { get; set; }
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
    }
}