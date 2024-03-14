//#undef DEBUG // DEBUG is defined by defualt in VS
//namespace ConditionalCompilation
//{
//    class Program
//    {
//        static void Main()
//        {
//#if DEBUG
//            Console.WriteLine("Debug version");
//#else
//        Console.WriteLine("Release version");
//#endif
//        }
//    }
//}

#define DEBUG // DEBUG is defined by defualt in VS
namespace ConditionalCompilation
{
    /// <summary>
    /// For testing conditional compilation
    /// </summary>
    class Program
    {
        static void Main()
        {
#if DEBUG
            Console.WriteLine("Debug version");
#else
            Console.WriteLine("Release version");
#endif
        }
    }  
}