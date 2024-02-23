using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace HospitalAdvance.BusinessLogic
{
	/// <summary>
	/// Handles logic for encrypting and decrypting password
	/// </summary>
	public class BLSecurity
	{
		#region Public Members

		/// <summary>
		/// Static key
		/// </summary>
		public static readonly string key = "0123456789ABCDEF0123456789ABCDEF";

		/// <summary>
		/// Static initialization vector
		/// </summary>
		public static readonly string iv = "0123456789ABCDEF";

		#endregion

		#region Public Methods 

		/// <summary>
		/// Encrypts string using AES
		/// </summary>
		/// <param name="plaintext">Text to be encrypted</param>
		/// <param name="key">Key to be used</param>
		/// <param name="iv">IV to be used</param>
		/// <returns>Encrypted string</returns>
		public static string EncryptAes(string plaintext, string key, string iv)
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

		/// <summary>
		/// Decrypts string using AES
		/// </summary>
		/// <param name="encryptedString">Encrypted text</param>
		/// <param name="key">Key to be used</param>
		/// <param name="iv">IV to be used</param>
		/// <returns></returns>
		public static string DecryptAes(string encryptedString, string key, string iv)
		{
			using (AesManaged aesAlg = new AesManaged())
			{
				aesAlg.Key = Encoding.UTF8.GetBytes(key);
				aesAlg.IV = Encoding.UTF8.GetBytes(iv);
				aesAlg.Padding = PaddingMode.PKCS7;

				ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

				using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(encryptedString)))
				{
					using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
					{
						using (StreamReader srDecrypt = new StreamReader(csDecrypt))
						{
							return srDecrypt.ReadToEnd();
						}
					}
				}
			}
		}

		#endregion
	}
}