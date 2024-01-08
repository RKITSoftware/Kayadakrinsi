///// <summary>
///// tryCatch class is used to demonstrate how to use try...catch block.
///// </summary>
//public class TryCatch
//{
//    #region Public Methods

//    public static void Main(string[] args)
//    {
//        string[] users = { "Krinsi", "Janvi", "Raj", "Vrundali", "Nidhi" };
//        try
//        {
//            Console.WriteLine(users[10]);
//        }
//        catch (Exception e)
//        {
//            Console.WriteLine("Array index out of bound ! ");
//            Console.WriteLine(e.ToString());
//        }

//        int a = 10, b = 0;
//        try
//        {
//            int c = a / b;
//            Console.WriteLine(c);
//        }
//        catch (Exception e)
//        {
//            Console.WriteLine(e.Message);
//            Console.WriteLine(e.StackTrace);
//            Console.WriteLine(e.HelpLink);
//            Console.WriteLine(e.Data);
//        }
//    }

//    #endregion
//}