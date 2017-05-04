/// <summary>
/// Powered by A Ghazal.
/// answered Dec 15 '14 at 12:44
/// A Ghazal
/// 53748
/// http://stackoverflow.com/questions/10168240/encrypting-decrypting-a-string-in-c-sharp
/// </summary>

using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

public static class EncryptionHelper
{
    private static string[] passwords =
        {
            "8b02bf6153e348d69632ae8b3356b3c3",
            "r5jpvl39ob4dka8pciq8eay86k7m6obg",
            "uh8ia8otirmh5docrgxgaji8lqdbzh62",
            "9ul780xqkz3tde5k2w5s3gtjhfmu9wet",
            "f06dmnpzrl9e9zsmvwvmtfo32xjcjseo",
            "j4abfri9vm0nvbed2xgue0mbavwa4a64",
        };
    private static byte[][] salts =
        {
            new byte[] {
                0x04, 0x05, 0x06, 0x07,
                0x00, 0x01, 0x02, 0x03,
                0x12, 0x13, 0x14, 0x15,
                0x08, 0x09, 0x10, 0x11,
            },
            new byte[] {
                0x08, 0x09, 0x10, 0x11,
                0x12, 0x13, 0x14, 0x15,
                0x00, 0x01, 0x02, 0x03,
                0x04, 0x05, 0x06, 0x07,
            },
            new byte[] {
                0x00, 0x01, 0x02, 0x03,
                0x08, 0x09, 0x10, 0x11,
                0x12, 0x13, 0x14, 0x15,
                0x04, 0x05, 0x06, 0x07,
            },
            new byte[] {
                0x04, 0x05, 0x06, 0x07,
                0x00, 0x01, 0x02, 0x03,
                0x08, 0x09, 0x10, 0x11,
                0x12, 0x13, 0x14, 0x15,
            },
            new byte[] {
                0x12, 0x13, 0x14, 0x15,
                0x00, 0x01, 0x02, 0x03,
                0x08, 0x09, 0x10, 0x11,
                0x04, 0x05, 0x06, 0x07,
            },
            new byte[] {
                0x08, 0x09, 0x10, 0x11,
                0x12, 0x13, 0x14, 0x15,
                0x04, 0x05, 0x06, 0x07,
                0x00, 0x01, 0x02, 0x03,
            },
        };

    private static Encoding encoding = Encoding.UTF8;
    public static Encoding Encoding { get { return encoding; } }

    // if keys array, salts array, or encoding changed, version must changed, too.
    private static float version = 1.1f;
    public static  float Version { get { return version; } }

    public static byte[] GetBytes(string str)
    {
        return encoding.GetBytes(str);
    }

    public static string MD5ChecksumCode(Encoding encoding, string fileFullPath)
    {
        try
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(fileFullPath))
                {
                    return encoding.GetString(md5.ComputeHash(stream));
                }
            }
        }
        catch (System.Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            System.Console.WriteLine(ex.Source);
            System.Console.WriteLine(ex.StackTrace);
        }
        return "";
    }

    public static string Encrypt(string clearText, int keyIndex)
    {
        byte[] clearBytes = GetBytes(clearText);
        using (Aes encryptor = Aes.Create())
        {
            string password = passwords[keyIndex];
            byte[] salt = salts[keyIndex];
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(password, salt);
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                clearText = Convert.ToBase64String(ms.ToArray());
            }
        }
        return clearText;
    }
    public static string Decrypt(string cipherText, int keyIndex)
    {
        cipherText = cipherText.Replace(" ", "+");
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        using (Aes encryptor = Aes.Create())
        {
            string password = passwords[keyIndex];
            byte[] salt = salts[keyIndex];
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(password, salt);
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }
                cipherText = encoding.GetString(ms.ToArray());
            }
        }
        return cipherText;
    }
}
