namespace Sealed_class
{
    /// <summary>
    /// User class one (base class)
    /// </summary>
    public class USR01
    {
        /// <summary>
        /// usr id
        /// </summary>
        public int R01F01 { get; set; }

        //public sealed virtual string UserName { get; set; }

        /// <summary>
        /// user name
        /// </summary>
        public virtual string R01F02 { get; set; }

        /// <summary>
        /// password
        /// </summary>
        public virtual string R01F03 { get; set; }

        /// <summary>
        /// Prints greetings message
        /// </summary>
        public virtual void Print()
        {
            Console.WriteLine("Greetings from USR01");
        }
    }

}
