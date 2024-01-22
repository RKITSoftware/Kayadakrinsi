using LibExtentionMethod;
namespace CallerExtentionMethod
{
    public static class Extend
    {
        public static void Color(this Animal animal,string color)
        {
            Console.WriteLine($"Color : {color}");
        }
    }
    public class Caller
    {
        public static void Main(String[] args)
        {
            Animal objAnimal = new Animal();
            objAnimal.Sound("Meow");
            objAnimal.Food("Fish");
            objAnimal.Color("Black");
        }
    }
}