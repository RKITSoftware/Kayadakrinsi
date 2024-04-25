using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Cryptography
{
    public class AESMAnaged
    {
        #region Public Members

        /// <summary>
        /// Security key
        /// </summary>
        public const string key = "0123456789ABCDEF0123456789ABCDEF";

        /// <summary>
        /// Initialization vector
        /// </summary>
        public const string iv = "0123456789ABCDEF";

        #endregion

        /// <summary>
        /// Encrypts string using AES
        /// </summary>
        /// <param name="plaintext">Text to be encrypted</param>
        /// <param name="key">Key to be used</param>
        /// <param name="iv">IV to be used</param>
        /// <returns>Encrypted string</returns>
        public string EncryptAes(string plaintext, string key = key, string iv = iv)
        {
            using (AesManaged aesAlg = new AesManaged())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                aesAlg.IV = Encoding.UTF8.GetBytes(iv);
                aesAlg.Padding = PaddingMode.PKCS7;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plaintext);
                        }
                    }

                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }
    }
}
