using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
/*
 * Class that handles the encryption and decryption
 * Reference: https://tekeye.uk/visual_studio/encrypt-decrypt-c-sharp-string
*/
namespace SecurityEncDec
{
	public static class SecurityMeasure
	{

		// This size of the IV (in bytes) must = (keysize / 8).  Default keysize is 256, so the IV must be
		// 32 bytes long.  Using a 16 character string here gives us 32 bytes when converted to a byte array.
		private const string initVector = "atkqgb605ai35dvo";
		// This constant is used to determine the keysize of the encryption algorithm.
		private const int keysize = 256;
		//Encrypt
		public static string EncryptString (string plainText, string passPhrase)
		{
			//convert into bytes to be able to use in the rijndael algorithm
			byte[] initVectorBytes = Encoding.UTF8.GetBytes (initVector);
			byte[] plainTextBytes = Encoding.UTF8.GetBytes (plainText);
			PasswordDeriveBytes password = new PasswordDeriveBytes (passPhrase, null);
			byte[] keyBytes = password.GetBytes (keysize / 8);
			//sets up the algorithm and choices using symmetricKey and CBC mode
			RijndaelManaged symmetricKey = new RijndaelManaged ();
			symmetricKey.Mode = CipherMode.CBC;
			//sets  encryption using the key value 
			ICryptoTransform encryptor = symmetricKey.CreateEncryptor (keyBytes, initVectorBytes);
			MemoryStream memoryStream = new MemoryStream (); //store the encrypted data
			CryptoStream cryptoStream = new CryptoStream (memoryStream, encryptor, CryptoStreamMode.Write); //do the encryption 
			cryptoStream.Write (plainTextBytes, 0, plainTextBytes.Length);
			cryptoStream.FlushFinalBlock ();
			byte[] cipherTextBytes = memoryStream.ToArray ();
			memoryStream.Close ();
			cryptoStream.Close ();
			return Convert.ToBase64String (cipherTextBytes); //convert the bytes to string to be used 
		}
		//Decrypt
		public  static string DecryptString (string cipherText, string passPhrase)
		{
			//convert into bytes to be able to use in the rijndael algorithm
			byte[] initVectorBytes = Encoding.ASCII.GetBytes (initVector);
			byte[] cipherTextBytes = Convert.FromBase64String (cipherText);
			PasswordDeriveBytes password = new PasswordDeriveBytes (passPhrase, null);
			byte[] keyBytes = password.GetBytes (keysize / 8);
			//sets up the algorithm and choices using symmetricKey and CBC mode
			RijndaelManaged symmetricKey = new RijndaelManaged ();
			symmetricKey.Mode = CipherMode.CBC;
			//sets the decryption using the key value
			ICryptoTransform decryptor = symmetricKey.CreateDecryptor (keyBytes, initVectorBytes);
			MemoryStream memoryStream = new MemoryStream (cipherTextBytes); 
			CryptoStream cryptoStream = new CryptoStream (memoryStream, decryptor, CryptoStreamMode.Read); //do the decryption
			byte[] plainTextBytes = new byte[cipherTextBytes.Length];
			int decryptedByteCount = cryptoStream.Read (plainTextBytes, 0, plainTextBytes.Length);
			memoryStream.Close ();
			cryptoStream.Close ();
			return Encoding.UTF8.GetString (plainTextBytes, 0, decryptedByteCount); //convert the bytes to string to be used 
		}

	}
}
