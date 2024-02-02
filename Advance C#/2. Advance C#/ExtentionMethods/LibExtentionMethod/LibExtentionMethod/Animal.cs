namespace LibExtentionMethod
{
    /// <summary>
    /// Sealed class Animal
    /// </summary>
    sealed public class Animal
    {
        /// <summary>
        /// Prints animal sound
        /// </summary>
        /// <param name="sound">sound of animal</param>
        public void Sound(string sound)
        {
            Console.WriteLine($"Sound : {sound}...{sound}");
        }

        /// <summary>
        /// Prints animal food
        /// </summary>
        /// <param name="food">food of animal</param>
        public void Food(string food)
        {
            Console.WriteLine($"Food : {food}");
        }
    }
}