namespace Sealed_class
{
    /// <summary>
    /// User class four inherites and tries override virtual as well as sealed propertirs and methods of USR03
    /// </summary>
    public class USR04 : USR03
    {
        /// <summary>
        /// overrides user name property of USR01
        /// </summary>
        public override string R01F02 => "Girl";

        /// <summary>
        /// Triying to override sealed property of USR03
        /// </summary>
        public sealed override string R01F03 => "Math";

        /// <summary>
        /// Triying to override sealed method of USR03
        /// </summary>
        public override void Print()
        {
            Console.WriteLine("Greetings from USR04");
        }
    }
}
