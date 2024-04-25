using System;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace HospitalAdvance.BusinessLogic
{
    /// <summary>
    /// Contains logic for common operations
    /// </summary>
    public class BLCommonHandler
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

        #region Public Methods 

        /// <summary>
        /// Converts single dynamic object into datatable
        /// </summary>
        /// <param name="obj">Object to be convert</param>
        /// <returns>Datatable</returns>
        public DataTable ToDatatable(object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            DataTable dataTable = (DataTable)JsonConvert.DeserializeObject(json, (typeof(DataTable)));
            return dataTable;
        }

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

        /// <summary>
        /// Decrypts string using AES
        /// </summary>
        /// <param name="encryptedString">Encrypted text</param>
        /// <param name="key">Key to be used</param>
        /// <param name="iv">IV to be used</param>
        /// <returns></returns>
        public string DecryptAes(string encryptedString, string key = key, string iv = iv)
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

        /// Takes more time (1000 records in datatable ~852 micro seconds)
        ///// <summary>
        ///// Serializes datatble into string
        ///// </summary>
        ///// <param name="dataTable">Datatable to be serialize</param>
        ///// <returns>Serialized string</returns>
        //public string DataTableSystemTextJson(DataTable dataTable)
        //{
        //    if (dataTable == null)
        //    {
        //        return string.Empty;
        //    }

        //    var data = dataTable.Rows.OfType<DataRow>()
        //                .Select(row => dataTable.Columns.OfType<DataColumn>()
        //                    .ToDictionary(col => col.ColumnName, c => row[c]));

        //    return JsonSerializer.Serialize(data);
        //}


        /// Takes lesser time (1000 records in datatable ~504 micro seconds)
        /// <summary>
        /// Serializes datatble into string
        /// </summary>
        /// <param name="dataTable">Datatable to be serialize</param>
        /// <returns>Serialized string</returns>
        public string ToString(DataTable dataTable)
        {

            if (dataTable == null)
            {
                return string.Empty;
            }

            return Newtonsoft.Json.JsonConvert.SerializeObject(dataTable, Newtonsoft.Json.Formatting.Indented);
        }

        #endregion

    }
}














