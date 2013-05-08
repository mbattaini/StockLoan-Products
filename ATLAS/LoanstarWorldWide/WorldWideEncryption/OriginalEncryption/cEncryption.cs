using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Security.Cryptography;
using System.IO;

//Bill Stone 8/05/2008

namespace BillStone.Encryption
{
	public class cEncryption
	{

        public static String fPasswordEncrypt(String sPassword)
		{
			return fEncrypt192bit(sPassword);
		}

        public static String fPasswordDecrypt(String sPassword)
        {
            return fDecrypt192bit(sPassword);
        }
        
        internal bool fPasswordCompare(String sPassword, String sPwd_Encrypted)
		{
			if (sPassword == fDecrypt192bit(sPwd_Encrypted))
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		
		
		//24 byte randomly selected for both the Key and the Initialization Vector
		//the IV is used to encrypt the first block of text so that any repetitive
		//patterns are not apparent
		
		//This is used to tell if key is encrypted
		private static String KEY_192_ENCRYPTED = "X010E02";
		
		//24 byte or 192 bit key and IV
		private static Byte[] KEY_192 = new Byte[] { 44, 14, 94, 156, 78, 4, 218, 32, 15, 167, 44, 80, 26, 250, 155, 112, 2, 94, 11, 204, 119, 35, 184, 194 };
		
		private static Byte[] IV_192 = new Byte[] { 54, 104, 244, 79, 36, 99, 167, 3, 42, 5, 62, 83, 184, 7, 209, 13, 145, 23, 200, 58, 173, 10, 121, 224 };
		
		//24 byte or 192 bit Encryption
		private static string fEncrypt192bit(string value)
		{
			
			string sRet = "";
			
			if (value != "")
			{
				TripleDESCryptoServiceProvider cryptoProvider = new TripleDESCryptoServiceProvider();
				MemoryStream ms = new MemoryStream();
				CryptoStream cs = new CryptoStream(ms, cryptoProvider.CreateEncryptor(KEY_192, IV_192), CryptoStreamMode.Write);
				StreamWriter sw = new StreamWriter(cs);
				sw.Write(value);
				sw.Flush();
				cs.FlushFinalBlock();
				ms.Flush();
				//Convert back to a string
				sRet = KEY_192_ENCRYPTED + Convert.ToBase64String(ms.GetBuffer(), 0, Convert.ToInt32(ms.Length));
				cryptoProvider = null;
				
				ms = null;
				cs = null;
				sw = null;
				GC.Collect();
			}
			
			return sRet;
			
		}
		
		//24 byte or 192 bit Decryption
		private static string fDecrypt192bit(String value)
		{
			
			String sRet = "";
			
			if (value != "")
			{
				if (value.StartsWith(KEY_192_ENCRYPTED))
				{
					value = value.ToString().Substring(KEY_192_ENCRYPTED.Length);
					TripleDESCryptoServiceProvider cryptoProvider = new TripleDESCryptoServiceProvider();
					//convert from string to byte array
					byte[] buffer = Convert.FromBase64String(value);
					MemoryStream ms = new MemoryStream(buffer);
					CryptoStream cs = new CryptoStream(ms, cryptoProvider.CreateDecryptor(KEY_192, IV_192), CryptoStreamMode.Read);
					StreamReader sr = new StreamReader(cs);
					sRet = sr.ReadToEnd();
					cryptoProvider = null;
					
					buffer = null;
					ms = null;
					cs = null;
					sr = null;
				}
				else
				{
					return value;
				}
				GC.Collect();
			}
			
			return sRet;
			
		}
		
	}
	
}
