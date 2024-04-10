using System.Security.Cryptography;
using System.Text;

namespace Cryptography
{
    /// <summary>
    /// Contains Logic for DES cryptography
    /// </summary>
    public class DES01
    {
        /// <summary>
        /// Encrypts plaintext, Decrypts ciphertext and prints the results using AES
        /// </summary>
        public void CryptographyUsingDES()
        {
            DES objDES = DES.Create();

            objDES.GenerateKey();
            objDES.GenerateIV();

            byte[] ciphertext;

            using (ICryptoTransform encryptor = objDES.CreateEncryptor())
            {
                byte[] plaintextBytes = Encoding.UTF8.GetBytes(Program.plaintext);
                ciphertext = encryptor.TransformFinalBlock(plaintextBytes, 0, plaintextBytes.Length);
            }

            string decryptedText;

            using (ICryptoTransform decryptor = objDES.CreateDecryptor(objDES.Key, objDES.IV))
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
