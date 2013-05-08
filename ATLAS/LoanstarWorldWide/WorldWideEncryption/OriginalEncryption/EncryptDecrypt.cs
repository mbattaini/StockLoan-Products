using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Security.Cryptography;
using System.IO;


namespace StockLoan.Encryption
{
    public class EncryptDecrypt
    {
        private const string eKey = "RIwdiqlMgQXwFA+CRvmaOQ==";

        private static string Encrypt(string Text, byte[] key, byte[] VectorBytes)
        {
            try
            {

                byte[] TextBytes = Encoding.UTF8.GetBytes(Text);
                RijndaelManaged rijKey = new RijndaelManaged();
                rijKey.Mode = CipherMode.CBC;
                ICryptoTransform encryptor = rijKey.CreateEncryptor(key, VectorBytes);
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
                cryptoStream.Write(TextBytes, 0, TextBytes.Length);
                cryptoStream.FlushFinalBlock();
                byte[] cipherTextBytes = memoryStream.ToArray();
                memoryStream.Close();
                cryptoStream.Close();
                string cipherText = Convert.ToBase64String(cipherTextBytes);
                return cipherText;
            }
            catch (Exception e)
            {
                string t = "";
                return t;
            }
        }

        private static string Decrypt(string Text, byte[] keyBytes, byte[] VectorBytes)
        {
            try
            {
                byte[] TextBytes = Convert.FromBase64String(Text);
                RijndaelManaged rijKey = new RijndaelManaged();
                rijKey.Mode = CipherMode.CBC;
                ICryptoTransform decryptor = rijKey.CreateDecryptor(keyBytes, VectorBytes);
                MemoryStream memoryStream = new MemoryStream(TextBytes);
                CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
                byte[] pTextBytes = new byte[TextBytes.Length];
                int decryptedByteCount = cryptoStream.Read(pTextBytes, 0, pTextBytes.Length);
                memoryStream.Close();
                cryptoStream.Close();
                string plainText = Encoding.UTF8.GetString(pTextBytes, 0, decryptedByteCount);
                return plainText;
            }
            catch (Exception a)
            {
                string t = "";
                return t;
            }
        }


        public static string EncryptString(string pwd)
        {

            //byte[] salt = { 0, 0, 0, 0, 0, 0, 0, 0 };
            byte[] salt = { 18, 54, 90, 16, 61, 93, 15, 61 };
            byte[] V = { 63, 64, 65, 66, 68, 69, 71, 47, 49, 83, 24, 42, 67, 78, 39, 12 };
            PasswordDeriveBytes cdk = new PasswordDeriveBytes(eKey, salt);
            byte[] kex = cdk.CryptDeriveKey("RC2", "SHA1", 128, salt);
            string answer = Encrypt(pwd, kex, V);
            return answer;

        }


        public static string DecryptString(string pwd)
        {
            byte[] salt = { 18, 54, 90, 16, 61, 93, 15, 61 };
            //byte[] V = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            byte[] V = { 63, 64, 65, 66, 68, 69, 71, 47, 49, 83, 24, 42, 67, 78, 39, 12 };
            PasswordDeriveBytes cdk = new PasswordDeriveBytes(eKey, salt);
            byte[] kex = cdk.CryptDeriveKey("RC2", "SHA1", 128, salt);
            string answer = Decrypt(pwd, kex, V);
            return answer;

        }

    }
}