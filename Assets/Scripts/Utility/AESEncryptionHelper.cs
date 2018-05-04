using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System;
using System.Security.Cryptography; 

namespace UnityToolbox
{
	public class AESEncryptionHelper {
		
		//32 bit
		private static string[] keys =
		{
			"27a0d2ff182746e6a274c7ba79e7a98f",
			"7a03a541f6504c6e9a0a40e5b53fb387",
			"7e3335becaf347d383ac9cf4d17061ec",
			"8ce1667eb2e74d158d5088395e7011a3",
			"ae8a7d37b1bc4c17a6199f5224f4ee2b",
			"a3ea6beef75740c787dd1a3a6ca6a77f",
			"e9d8ee37942c415383d36561f207fb43",
			"1be22cbafe784b62805ca2cb5942475d",
			"cb5979421ffd4afbb93f0de75714ba8d",
			"c4556bb093154815814cda6668406582",
		};

		//16 bit
		private static string[] vis =
		{
			"938baf95fa514b59",
			"0abf6e11a0974ad7",
			"10a48f8e9cfe4f21",
			"bd4633927e004da0",
			"5bf1fa667cdb44cf",
			"70a6592f9a744e81",
			"1501e1d99c9a466f",
			"f9b36e9e2fad4d0a",
			"fb5e3507064f4090",
			"9bf7b872e0b84205",
		};

		 // if keys array, salts array, or encoding changed, version must changed, too.
		private static float version = 1.0f;
        public static float Version { get { return version; } }

		/// <summary>
		/// use aes 256 encrypt
		/// </summary>
		/// <param name="encryptData">encrypt data</param>
		/// <param name="keyIndex">key index</param>
		/// <returns></returns>
		public static string Encrypt(string encryptData,int keyIndex)
		{

			byte[] sourceBytes = Encoding.UTF8.GetBytes(encryptData);

			var aes = new RijndaelManaged();

			string key = keys[keyIndex];
			string vi = vis[keyIndex];

			aes.Key = Encoding.UTF8.GetBytes(key);

			aes.IV = Encoding.UTF8.GetBytes(vi);

			aes.Mode = CipherMode.CBC;

			aes.Padding = PaddingMode.PKCS7;

			ICryptoTransform transform = aes.CreateEncryptor();

			return Convert.ToBase64String(transform.TransformFinalBlock(sourceBytes, 0, sourceBytes.Length));

		}


		/// <summary>
		/// use aes 256 decrypt
		/// </summary>
		/// <param name="EncryptData">encrypt data</param>
		/// <param name="keyIndex">key index</param>
		/// <returns></returns>
		public static string Decrypt(string encryptData,int keyIndex)
		{

			var encryptBytes = Convert.FromBase64String(encryptData);

			var aes = new RijndaelManaged();

			string key = keys[keyIndex];

			string vi = vis[keyIndex];

			aes.Key = Encoding.UTF8.GetBytes(key);

			aes.IV = Encoding.UTF8.GetBytes(vi);

			aes.Mode = CipherMode.CBC;

			aes.Padding = PaddingMode.PKCS7;

			ICryptoTransform transform = aes.CreateDecryptor();

			return Encoding.UTF8.GetString(transform.TransformFinalBlock(encryptBytes, 0, encryptBytes.Length));

		}
	}
}
