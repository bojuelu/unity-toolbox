using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using UnityEngine;

public static class TripleDESCrypto
{
    private static string[] keys =
        {
            "ULgwGy5f9ljtM2zBCVRev0JT",
            "EGZA7eUzVPxLGHpCPKyqK0xT",
            "4HtdUIy0AHSqIqYJkVjaPDcU",
            "yQbvbts3HLMhuTMJbf5023GB",
            "Q6AMSo3wsplYf6odZ8I2AdkP",
            "MwMkVm4k0nQqEgb4xD5Xc84A",
            "X75SSuaPbwBX30R0KfckUpdq",
            "hdqm0op6ZLkgmZmDNaaE6FTY",
            "dKiBj2Dgy0Ls09q9zJyEUlJC",
            "tm4OVGScvza5kcAzXbS3hung"
        };
    private static string[] ivs =
        {
            "IfDTKZ4P",
            "I2NVxWNy",
            "sbKmEJi2",
            "BMUu0UTb",
            "1i9JUQDL",
            "nLAguMxX",
            "19kgM0Py",
            "oaahcioL",
            "cya7bE5N",
            "5EdnZ0ff"
        };

    public static string Encrypt(string clearText, int keyivIndex)
    {
        try
        {
            byte[] rgbKey = Encoding.UTF8.GetBytes(keys[keyivIndex]);
            byte[] rgbIV = Encoding.UTF8.GetBytes(ivs[keyivIndex]);

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

    public static string Decrypt(string cipherText, int keyivIndex)
    {
        try
        {
            cipherText = cipherText.Replace("\\n", "");  // encrypt via python will have '\n' at tail.
            byte[] rgbKey = Encoding.UTF8.GetBytes(keys[keyivIndex]);
            byte[] rgbIV = Encoding.UTF8.GetBytes(ivs[keyivIndex]);
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
