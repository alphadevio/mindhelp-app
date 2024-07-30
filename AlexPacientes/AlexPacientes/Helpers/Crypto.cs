using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using AlexPacientes.Settings;
using System.IO;

namespace AlexPacientes.Helpers
{
    public class Crypto
    {
        private string Key = "";
        private string IV = "";
        public Crypto(string currentKey)
        {
            this.Key = currentKey;
            this.IV = currentKey.Substring(0, 16);
        }
        public string Encrypt(string text)
        {
            byte[] key = Encoding.UTF8.GetBytes(this.Key);
            byte[] iv = Encoding.UTF8.GetBytes(this.IV);
            var encrypted = EncryptStringToBytes(text, key, iv);
           
            return encrypted;
        }

        private string EncryptStringToBytes(string text, byte[] key, byte[] iv)
        {
            using(var aes = new AesManaged())
            {
                aes.Key = key;
                aes.KeySize = 256;
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                var encryptor = aes.CreateEncryptor(key, iv);
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        byte[] data = Encoding.UTF8.GetBytes(text);
                        cs.Write(data, 0, data.Length);
                        cs.FlushFinalBlock();
                        return Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
        }

        public string Decrypt(string encrypted)
        {
            byte[] key = Encoding.UTF8.GetBytes(this.Key);
            byte[] iv = Encoding.UTF8.GetBytes(this.IV);
            var decrypted = DecryptStringToBytes(encrypted, key, iv);

            return decrypted;
        }

        private string DecryptStringToBytes(string encrypted, byte[] key, byte[] iv)
        {
            using (var aes = new AesManaged())
            {
                aes.Key = key;
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                var data = Convert.FromBase64String(encrypted);
                var decryptor = aes.CreateDecryptor(key, iv);
                using (var ms = new MemoryStream(data))
                {
                    using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using(var sr = new StreamReader(cs))
                        {
                            return sr.ReadToEnd();
                        }
                    }
                }
            }
        }

    }
}
