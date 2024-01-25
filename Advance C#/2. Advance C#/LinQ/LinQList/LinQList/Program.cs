public class Flower
{
    public string Name { get; set; }
    public string Color { get; set; }

    public static List<Flower> lstFlower = new List<Flower>
        {
            new Flower{Name = "Rose",Color = "Red" },
            new Flower{Name = "Tulip",Color = "Pruple"},
            new Flower{Name = "Marie Gold",Color = "Yellow"},
            new Flower{Name = "Tulip",Color = "Yellow"},
            new Flower{Name = "Rose",Color = "Pink"},
            new Flower{Name = "Tulip",Color = "Pink"},
            new Flower{Name = "Lotus",Color = "Pink"}
        };
    public static void QueryColorFlower(string color)
    {
        var colorFlower = from flower in lstFlower
                         where flower.Color == color
                         select flower;
        foreach (var flower in colorFlower)
        {
            Console.WriteLine($"Name : {flower.Name}, Color : {flower.Color}");
        }
    }
    public static void QuerySort()
    {
        var data = from flower in lstFlower
                   orderby flower.Name.Length,flower.Color descending
                   select flower;
        foreach (var flower in data)
        {
            Console.WriteLine($"Name : {flower.Name}, Color : {flower.Color}");
        }
    }
    private static void Main(string[] args)
    {
        QueryColorFlower("Yellow");
        QuerySort();
        
        Console.WriteLine($"Element at 1 : {lstFlower.ElementAt(1).Name}");
        Console.WriteLine($"First element : {lstFlower.First().Name}");
        Console.WriteLine($"Last element : {lstFlower.Last().Name}");
        Console.WriteLine($"Total number of elements : {lstFlower.LongCount()}");


    }
}