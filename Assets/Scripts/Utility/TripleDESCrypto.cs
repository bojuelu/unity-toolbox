using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using UnityEngine;

public static class TripleDESCrypto
{
    private static string key = "ULgwGy5f9ljtM2zBCVRev0JT";
    private static string iv = "IfDTKZ4P";

    public static string Encrypt(string clearText)
    {
        try
        {
            byte[] rgbKey = Encoding.UTF8.GetBytes(key);
            byte[] rgbIV = Encoding.UTF8.GetBytes(iv);

            byte[] inputByteArray = Encoding.UTF8.GetBytes(clearText);
            TripleDESCryptoServiceProvider dCSP = new TripleDESCryptoServiceProvider();
            dCSP.Mode = CipherMode.CBC;
            dCSP.Padding = PaddingMode.PKCS7;
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            string cipherText = Convert.ToBase64String(mStream.ToArray());
            cStream.Close();
            mStream.Close();

            return cipherText;
        }
        catch (System.Exception ex)
        {
            Debug.LogException(ex);
            return clearText;
        }
    }

    public static string Decrypt(string cipherText)
    {
        try
        {
            cipherText = cipherText.Replace("\\n", "");  // encrypt via python will have '\n' at tail.
            byte[] rgbKey = Encoding.UTF8.GetBytes(key);
            byte[] rgbIV = Encoding.UTF8.GetBytes(iv);
            byte[] inputByteArray = Convert.FromBase64String(cipherText);
            TripleDESCryptoServiceProvider dCSP = new TripleDESCryptoServiceProvider();
            dCSP.Mode = CipherMode.CBC;
            dCSP.Padding = PaddingMode.PKCS7;
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            string clearText = Encoding.UTF8.GetString(mStream.ToArray());
            cStream.Close();
            mStream.Close();

            return clearText;
        }
        catch (System.Exception ex)
        {
            Debug.LogException(ex);
            return cipherText;
        }
    }
}
