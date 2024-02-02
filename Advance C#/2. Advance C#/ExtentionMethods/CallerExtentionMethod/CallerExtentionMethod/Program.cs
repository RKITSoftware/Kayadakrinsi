using LibExtentionMethod;
namespace CallerExtentionMethod
{
    /// <summary>
    /// Extended class
    /// </summary>
    public static class Extend
    {
        /// <summary>
        /// Extens class animal to print color of animal
        /// </summary>
        /// <param name="objAnimal">reference to Animal class with it's object</param>
        /// <param name="color">color of animal</param>
        public static void Color(this Animal objAnimal,string color)
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