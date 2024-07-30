using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace AlexPacientes.Helpers
{
    public class EncryptionHelper
    {
        string key;
        string iv;
        int minutesToLive;
        byte[] keyBytes;
        public EncryptionHelper(string key, string iv, int minutesToLive)
        {
            this.key = key;
            this.iv = iv;
            this.minutesToLive = minutesToLive;
            this.keyBytes = Encoding.ASCII.GetBytes(key);
        }

        string AESEncrypt(string data)
        {
            byte[] result;
            using (var hasher = new SHA256Managed())
            {
                byte[] bytes = UTF8Encoding.UTF8.GetBytes(data);
                byte[] KeyBytes = hasher.ComputeHash(UTF8Encoding.UTF8.GetBytes(this.key));
                byte[] IVBytes = UTF8Encoding.UTF8.GetBytes(this.iv);

                using (MemoryStream memoryStream = new MemoryStream())
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, GetAlgorithm().CreateEncryptor(KeyBytes, IVBytes), CryptoStreamMode.Write))
                {

                    cryptoStream.Write(bytes, 0, bytes.Length);
                    cryptoStream.FlushFinalBlock();
                    cryptoStream.Close();
                    result = memoryStream.ToArray();
                }
            }
            return Convert.ToBase64String(result);
        }

        string AESDecrypt(string data)
        {
            byte[] outputBytes = Convert.FromBase64String(data);
            string result = string.Empty;
            using (var hasher = new SHA256Managed())
            {
                byte[] KeyBytes = hasher.ComputeHash(UTF8Encoding.UTF8.GetBytes(this.key));
                byte[] IVBytes = UTF8Encoding.UTF8.GetBytes(this.iv);

                using (MemoryStream memoryStream = new MemoryStream(outputBytes))
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, GetAlgorithm().CreateDecryptor(KeyBytes, IVBytes), CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(cryptoStream))
                    {
                        result = srDecrypt.ReadToEnd();
                    }
                }
            }
            return result;
        }

        public string Encrypt(string data)
        {
            return HttpUtility.UrlEncode(AESEncrypt(data));
        }

        public string Encrypt(object data)
        {
            return HttpUtility.UrlEncode(AESEncrypt(JsonConvert.SerializeObject(data)));
        }

        public T Decrypt<T>(string data)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(AESDecrypt(HttpUtility.UrlDecode(data)));
            }
            catch
            {
                return default;
            }
        }

        public string Decrypt(string data)
        {
            return AESDecrypt(HttpUtility.UrlDecode(data));
        }

        RijndaelManaged GetAlgorithm()
        {
            RijndaelManaged al = new RijndaelManaged();
            al.Padding = PaddingMode.PKCS7;
            al.Mode = CipherMode.CBC;
            al.KeySize = 128;
            al.BlockSize = 128;
            return al;
        }

    }
}
