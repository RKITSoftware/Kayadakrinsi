namespace LinQList
{
    /// <summary>
    /// Contains logic and main method
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// List of flowers
        /// </summary>
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

        /// <summary>
        /// Prints flower whoose color is given
        /// </summary>
        /// <param name="color">color of floweer</param>
        public void QueryColorFlower(string color)
        {
            var colorFlower = from flower in lstFlower
                              where flower.Color == color
                              select flower;

            
            foreach (var flower in colorFlower)
            {
                Console.WriteLine($"Name : {flower.Name}, Color : {flower.Color}");
            }
        }

        /// <summary>
        /// Prints sorted flower by flower name length and then color(thenby)
        /// </summary>
        public void QueryThenBy()
        {
            // Then by
            var data = from flower in lstFlower
                       orderby flower.Name.Length, flower.Color descending
                       select flower;
            
            foreach (var flower in data)
            {
                Console.WriteLine($"Name : {flower.Name}, Color : {flower.Color}");
            }
        }
        
        /// <summary>
        /// Prints sorted flwers in ascending order
        /// </summary>
        public void QuerySort()
        {
            // Order by
            var data = lstFlower.OrderBy(f => f.Name).ToList();

            foreach (var flower in data)
            {
                Console.WriteLine($"Name : {flower.Name}, Color : {flower.Color}");
            }
        }

        public static void Main(string[] args)
        {
            Program obj = new Program();

            // Query color
            obj.QueryColorFlower("Yellow");

            Console.WriteLine("\n");
            // Sorting then by
            obj.QueryThenBy();

            Console.WriteLine("\n");
            // Sorting 
            obj.QuerySort();

            // Element at
            Console.WriteLine($"\nElement at 1 : {lstFlower.ElementAt(1).Name}");

            // First element
            Console.WriteLine($"\nFirst element : {lstFlower.First().Name}");

            // Last element
            Console.WriteLine($"\nLast element : {lstFlower.Last().Name}");

            // Total number of elements inside list
            Console.WriteLine($"\nTotal number of elements : {lstFlower.LongCount()}");
        }
    }
}
