using System.Security.Cryptography;
using System.Text;

namespace Cryptography
{
    /// <summary>
    /// Contains Logic for AES cryptography
    /// </summary>
    public class AES01
    {
        /// <summary>
        /// Encrypts plaintext, Decrypts ciphertext and prints the results using DES
        /// </summary>
        public void CryptographyUsingAES()
        {
            Aes objAES = Aes.Create();
            objAES.KeySize = 256; // key size
            objAES.Mode = CipherMode.CBC; // cipher block mode = cipher block chaining
            objAES.GenerateKey(); // generate key
            objAES.GenerateIV(); // generate initialization vector

            byte[] ciphertext;

            using (ICryptoTransform encryptor = objAES.CreateEncryptor())
            {
                byte[] plaintextBytes = Encoding.UTF8.GetBytes(Program.plaintext);
                ciphertext = encryptor.TransformFinalBlock(plaintextBytes, 0, plaintextBytes.Length);
            }

            string decryptedText;

            using (ICryptoTransform decryptor = objAES.CreateDecryptor(objAES.Key, objAES.IV))
            {
                byte[] decryptedBytes = decryptor.TransformFinalBlock(ciphertext, 0, ciphertext.Length);
                decryptedText = Encoding.UTF8.GetString(decryptedBytes);
            }

            Console.WriteLine($"Plaintext : {Program.plaintext}");
            Console.WriteLine($"Ciphertext : {Convert.ToBase64String(ciphertext)}");
            Console.WriteLine($"Decrypted text : {decryptedText}");
        }
    }
}
