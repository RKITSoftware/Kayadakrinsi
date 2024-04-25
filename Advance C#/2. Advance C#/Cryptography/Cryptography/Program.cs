using Cryptography;

public class Program
{
    #region Public Members

    /// <summary>
    /// Text to be encrypt
    /// </summary>
    public static string plaintext = "Hello world";

    #endregion


    /// <summary>
    /// Driver code
    /// </summary>
    /// <param name="args"></param>
    public static void Main(string[] args)
    {
        AES01 objAES = new AES01();
        DES01 objDES = new DES01();
        RSA01 objRSA = new RSA01(); 
        AESMAnaged obj = new AESMAnaged();

        Console.WriteLine("\nAdvance data Encryption standard...");
        objAES.CryptographyUsingAES();

        Console.WriteLine("\nData Encryption standard...");
        objDES.CryptographyUsingDES();

        Console.WriteLine("\nRSA...");
        objRSA.CryptographyUsingRSA();

        Console.WriteLine("Pswd : ");
        string pswd = obj.EncryptAes("123");
        Console.Write(pswd);
    }

}