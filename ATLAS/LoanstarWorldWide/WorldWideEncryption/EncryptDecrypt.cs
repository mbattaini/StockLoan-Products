using System; 
using System.Text; 
using System.Security.Cryptography; 
using System.IO; 

namespace StockLoan.Encryption
{
    public static class EncryptDecrypt 
    {
        private const string eKey = "UF1Gt2h2pUVXU748WS/KifVHd76jSZY5VrudQMCKm3w=";
     
        public static string Encrypt(string input)
        { 
            byte[] utfData = UTF8Encoding.UTF8.GetBytes(input); 
            byte[] saltBytes = Encoding.UTF8.GetBytes(eKey); 
            string encryptedString = string.Empty; 
            
            using (AesManaged aes = new AesManaged()) 
            { 
                Rfc2898DeriveBytes rfc = new Rfc2898DeriveBytes(eKey, saltBytes); 
  
                aes.BlockSize = aes.LegalBlockSizes[0].MaxSize; 
                aes.KeySize = aes.LegalKeySizes[0].MaxSize; 
                aes.Key = rfc.GetBytes(aes.KeySize / 8); 
                aes.IV = rfc.GetBytes(aes.BlockSize / 8); 
  
                using (ICryptoTransform encryptTransform = aes.CreateEncryptor()) 
                { 
                    using (MemoryStream encryptedStream = new MemoryStream()) 
                    { 
                        using (CryptoStream encryptor =  
                            new CryptoStream(encryptedStream, encryptTransform, CryptoStreamMode.Write)) 
                        { 
                            encryptor.Write(utfData, 0, utfData.Length); 
                            encryptor.Flush(); 
                            encryptor.Close(); 
  
                            byte[] encryptBytes = encryptedStream.ToArray(); 
                            encryptedString = Convert.ToBase64String(encryptBytes); 
                        } 
                    } 
                } 
            } 

            return encryptedString; 
        } 
  
        public static string Decrypt(string input)
        { 
            byte[] encryptedBytes = Convert.FromBase64String(input); 
            byte[] saltBytes = Encoding.UTF8.GetBytes(eKey); 
            string decryptedString = string.Empty;
            
            using (var aes = new AesManaged())
            {
                Rfc2898DeriveBytes rfc = new Rfc2898DeriveBytes(eKey, saltBytes);
                aes.BlockSize = aes.LegalBlockSizes[0].MaxSize;
                aes.KeySize = aes.LegalKeySizes[0].MaxSize;
                aes.Key = rfc.GetBytes(aes.KeySize / 8);
                aes.IV = rfc.GetBytes(aes.BlockSize / 8);

                using (ICryptoTransform decryptTransform = aes.CreateDecryptor())
                {
                    using (MemoryStream decryptedStream = new MemoryStream())
                    {
                        CryptoStream decryptor = new CryptoStream(decryptedStream, decryptTransform, CryptoStreamMode.Write);
                        decryptor.Write(encryptedBytes, 0, encryptedBytes.Length);
                        decryptor.Flush();
                        decryptor.Close();

                        byte[] decryptBytes = decryptedStream.ToArray();
                        decryptedString = UTF8Encoding.UTF8.GetString(decryptBytes, 0, decryptBytes.Length);
                    }
                }
            }

            return decryptedString; 
        } 
    } 
} 

