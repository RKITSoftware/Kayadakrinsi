namespace Sealed_class
{
    /// <summary>
    /// User class five inherites and overrides propertirs and methods of USR01
    /// </summary>
    public class USR05 : USR01
    {
        /// <summary>
        /// overrides user name property of USR01
        /// </summary>
        public override string R01F02 => "Hello";

        /// <summary>
        /// overrides password property of USR01
        /// </summary>
        public sealed override string R01F03 => "World";

        /// <summary>
        /// Prints greetings message
        /// </summary>
        public sealed override void Print()
        {
            Console.WriteLine("Greetings from USR05");
        }
    }

}
