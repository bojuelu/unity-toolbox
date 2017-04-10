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
    private static string encryptionKey = "8b02bf6153e348d69632ae8b3356b3c3";
    private static byte[] encryptionSalt = new byte[] { 0x48, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 };

    private static Encoding encode = Encoding.Unicode;
    public static Encoding Encode { get { return encode; } }

    public static byte[] GetBytes(string str)
    {
        return encode.GetBytes(str);
    }

    public static string MD5ChecksumCode(string fileFullPath)
    {
        try
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(fileFullPath))
                {
                    return Encoding.Default.GetString(md5.ComputeHash(stream));
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

    public static string Encrypt(string clearText)
    {
        byte[] clearBytes = GetBytes(clearText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, encryptionSalt);
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
    public static string Decrypt(string cipherText)
    {
        cipherText = cipherText.Replace(" ", "+");
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, encryptionSalt);
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }
                cipherText = Encoding.Unicode.GetString(ms.ToArray());
            }
        }
        return cipherText;
    }
}
