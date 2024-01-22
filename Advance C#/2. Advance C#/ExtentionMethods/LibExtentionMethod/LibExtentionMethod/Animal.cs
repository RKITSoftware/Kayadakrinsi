namespace LibExtentionMethod
{
    sealed public class Animal
    {
        public void Sound(string sound)
        {
            Console.WriteLine($"Sound : {sound}...{sound}");
        }
        public void Food(string food)
        {
            Console.WriteLine($"Food : {food}");
        }
    }
}