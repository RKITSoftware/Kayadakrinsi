using System.Security.Cryptography;
using System.Text;

namespace Cryptography
{
    /// <summary>
    /// Contains Logic for RSA cryptography
    /// </summary>
    public class RSA01
    {
        /// <summary>
        /// Encrypts plaintext, Decrypts ciphertext and prints the results using RSA
        /// </summary>
        public void CryptographyUsingRSA()
        {
            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();

            byte[] plaintextBytes = Encoding.UTF8.GetBytes(Program.plaintext);
            byte[] ciphertext = RSAEncryption(plaintextBytes, RSA.ExportParameters(false), false);

            byte[] decryptedBytes = RSADecryption(ciphertext, RSA.ExportParameters(true), false);
            string decryptedText = Encoding.UTF8.GetString(decryptedBytes);

            Console.WriteLine($"Plaintext : {Program.plaintext}");
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
        public byte[] RSAEncryption(byte[] DataToEncrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
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
        public byte[] RSADecryption(byte[] DataToDecrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
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
    }
}
