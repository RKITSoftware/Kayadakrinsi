using System.Security.Cryptography;
using System.Text;

public class Program
{
    #region Public Members

    /// <summary>
    /// Text to be encrypt
    /// </summary>
    public static string plaintext = "Hello world";

    #endregion

    #region Public Methods

    /// <summary>
    /// Encrypts plaintext, Decrypts ciphertext and prints the results using DES
    /// </summary>
    public static void CryptographyUsingAES()
    {
        Aes objAES = Aes.Create(); 
        objAES.KeySize = 256; // key size
        objAES.Mode = CipherMode.CBC; // cipher block mode = cipher block chaining
        objAES.GenerateKey(); // generate key
        objAES.GenerateIV(); // generate initialization vector

        byte[] ciphertext;

        using (ICryptoTransform encryptor = objAES.CreateEncryptor())
        {
            byte[] plaintextBytes = Encoding.UTF8.GetBytes(plaintext);
            ciphertext = encryptor.TransformFinalBlock(plaintextBytes, 0, plaintextBytes.Length);
        }

        string decryptedText;

        using (ICryptoTransform decryptor = objAES.CreateDecryptor(objAES.Key, objAES.IV))
        {
            byte[] decryptedBytes = decryptor.TransformFinalBlock(ciphertext, 0, ciphertext.Length);
            decryptedText = Encoding.UTF8.GetString(decryptedBytes);
        }

        Console.WriteLine($"Plaintext : {plaintext}");
        Console.WriteLine($"Ciphertext : {Convert.ToBase64String(ciphertext)}");
        Console.WriteLine($"Decrypted text : {decryptedText}");
    }

    /// <summary>
    /// Encrypts plaintext, Decrypts ciphertext and prints the results using AES
    /// </summary>
    public static void CryptographyUsingDES()
    {
        DES objDES = DES.Create();

        objDES.GenerateKey();
        objDES.GenerateIV();

        byte[] ciphertext;

        using (ICryptoTransform encryptor = objDES.CreateEncryptor())
        {
            byte[] plaintextBytes = Encoding.UTF8.GetBytes(plaintext);
            ciphertext = encryptor.TransformFinalBlock(plaintextBytes, 0, plaintextBytes.Length);
        }

        string decryptedText;

        using (ICryptoTransform decryptor = objDES.CreateDecryptor(objDES.Key, objDES.IV))
        {
            byte[] decryptedBytes = decryptor.TransformFinalBlock(ciphertext, 0, ciphertext.Length);
            decryptedText = Encoding.UTF8.GetString(decryptedBytes);
        }

        Console.WriteLine($"Plaintext : {plaintext}");
        Console.WriteLine($"Ciphertext : {Convert.ToBase64String(ciphertext)}");
        Console.WriteLine($"Decrypted text : {decryptedText}");

    }

    /// <summary>
    /// Encrypts plaintext, Decrypts ciphertext and prints the results using RSA
    /// </summary>
    public static void CryptographyUsingRSA()
    {
        RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
        UnicodeEncoding ByteConverter = new UnicodeEncoding();

        byte[] plaintextBytes = Encoding.UTF8.GetBytes(plaintext) ;
        byte[] ciphertext = RSAEncryption(plaintextBytes, RSA.ExportParameters(false), false);

        byte[] decryptedBytes= RSADecryption(ciphertext, RSA.ExportParameters(true), false);
        string decryptedText = Encoding.UTF8.GetString(decryptedBytes);

        Console.WriteLine($"Plaintext : {plaintext}");
        Console.WriteLine($"Ciphertext : {Convert.ToBase64String(ciphertext)}");
        Console.WriteLine($"Decrypted text : {decryptedText}");
    }

    /// <summary>
    /// Encryption logic for RSA
    /// </summary>
    /// <param name="DataToEncrypt">plaintext bytes</param>
    /// <param name="RSAKeyInfo">key information</param>
    /// <param name="DoOAEPPadding">weather to add padding or not</param>
    /// <returns></returns>
    public static byte[] RSAEncryption(byte[] DataToEncrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
    {
        try
        {
            byte[] encryptedData;
            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            {
                RSA.ImportParameters(RSAKeyInfo);
                encryptedData = RSA.Encrypt(DataToEncrypt, DoOAEPPadding);
            }
            return encryptedData;
        }
        catch (CryptographicException e)
        {
            Console.WriteLine(e.Message);

            return null;
        }
    }

    /// <summary>
    /// Decryption logic for RSA
    /// </summary>
    /// <param name="DataToDecrypt">ciphertext bytes</param>
    /// <param name="RSAKeyInfo">key information</param>
    /// <param name="DoOAEPPadding">weather to add padding or not</param>
    /// <returns></returns>
    public static byte[] RSADecryption(byte[] DataToDecrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
    {
        try
        {
            byte[] decryptedData;
            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            {
                RSA.ImportParameters(RSAKeyInfo);  
                decryptedData = RSA.Decrypt(DataToDecrypt, DoOAEPPadding);
            }
            return decryptedData;
        }
        catch (CryptographicException e)
        {
            Console.WriteLine(e.ToString());

            return null;
        }
    }
    
    /// <summary>
    /// Driver code
    /// </summary>
    /// <param name="args"></param>
    public static void Main(string[] args)
    {
        Console.WriteLine("\nAdvance data Encryption standard...");
        CryptographyUsingAES();
        Console.WriteLine("\nData Encryption standard...");
        CryptographyUsingDES();
        Console.WriteLine("\nRSA...");
        CryptographyUsingRSA();
    }

    #endregion
}