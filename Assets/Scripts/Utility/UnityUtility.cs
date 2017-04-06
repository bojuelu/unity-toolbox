/// <summary>
/// Unity utility.
/// Author: BoJue.
/// </summary>

using UnityEngine;
using System;
using System.Text;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public static class UnityUtility
{
    public static string Rot38(string str)
    {
        if (str.Length <= 0)
            return "";

        char[] rotUpperL = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', };
        char[] rotUpperR = new char[] { 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', };

        char[] rotLowerL = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', };
        char[] rotLowerR = new char[] { 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', };

        char[] rotSymbolL = new char[] { '~', '!', '@', '#', '$', '%', '^', };
        char[] rotSymbolR = new char[] { '&', '*', '(', ')', '_', '+', ' ', };

        char[] rotNumL = new char[] { '1', '2', '3', '4', '5', };
        char[] rotNumR = new char[] { '6', '7', '8', '9', '0', };

        char[] result = new char[str.Length];
        for (int i = 0; i < str.Length; i++)
        {
            char c = str[i];
            bool nextChar = false;

            // check upper case left table to right table
            if (!nextChar)
            {
                for (int j = 0; j < rotUpperL.Length; j++)
                {
                    if (c == rotUpperL[j])
                    {
                        c = rotUpperR[j];
                        nextChar = true;
                        break;
                    }
                }
            }
            // check upper case right table to left table
            if (!nextChar)
            {
                for (int j = 0; j < rotUpperR.Length; j++)
                {
                    if (c == rotUpperR[j])
                    {
                        c = rotUpperL[j];
                        nextChar = true;
                        break;
                    }
                }
            }
            // check lower case left table to right table
            if (!nextChar)
            {
                for (int j = 0; j < rotLowerL.Length; j++)
                {
                    if (c == rotLowerL[j])
                    {
                        c = rotLowerR[j];
                        nextChar = true;
                        break;
                    }
                }
            }
            // check lower case right table to left table
            if (!nextChar)
            {
                for (int j = 0; j < rotLowerR.Length; j++)
                {
                    if (c == rotLowerR[j])
                    {
                        c = rotLowerL[j];
                        nextChar = true;
                        break;
                    }
                }
            }
            // check symbol left table to right table
            if (!nextChar)
            {
                for (int j = 0; j < rotSymbolL.Length; j++)
                {
                    if (c == rotSymbolL[j])
                    {
                        c = rotSymbolR[j];
                        nextChar = true;
                        break;
                    }
                }
            }
            // check symbol right table to left table
            if (!nextChar)
            {
                for (int j = 0; j < rotSymbolR.Length; j++)
                {
                    if (c == rotSymbolR[j])
                    {
                        c = rotSymbolL[j];
                        nextChar = true;
                        break;
                    }
                }
            }
            // check number left table to right table
            if (!nextChar)
            {
                for (int j = 0; j < rotNumL.Length; j++)
                {
                    if (c == rotNumL[j])
                    {
                        c = rotNumR[j];
                        nextChar = true;
                        break;
                    }
                }
            }
            // check number right table to left table
            if (!nextChar)
            {
                for (int j = 0; j < rotNumR.Length; j++)
                {
                    if (c == rotNumR[j])
                    {
                        c = rotNumL[j];
                        nextChar = true;
                        break;
                    }
                }
            }

            result[i] = c;
        }

        return new string(result);
    }

    public static string GenerateRandomString(int length)
    {
        if (length <= 0)
            return "";

        char[] chars = new char[]
        {
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm',
            'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',

            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
            'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
        };
        char[] pickChars = new char[length];
        for (int i = 0; i < length; i++)
        {
            int pickIndex = UnityEngine.Random.Range(0, chars.Length);
            pickChars[i] = chars[pickIndex];
        }

        string s = new string(pickChars);
        return s;
    }

    public static bool IsPNG(WWW wwwObj)
    {
        try
        {
            if (wwwObj != null)
            {
                if (wwwObj.isDone)
                {
                    if (string.IsNullOrEmpty(wwwObj.error) && wwwObj.texture != null && wwwObj.bytes != null)
                    {
                        return IsPNG(wwwObj.bytes);
                    }
                }
            }
        }
        catch (System.Exception err)
        {
            Debug.LogError(err.Message);
            Debug.LogError(err.StackTrace);
        }
        return false;
    }

    public static bool IsPNG(byte[] bytes)
    {
        if (bytes == null)
            return false;

        // png at least 10 bytes
        if (bytes.Length <= 10)
            return false;

        // 8950 4e47
        if (bytes[0] == 0x89 && bytes[1] == 0x50 && bytes[2] == 0x4E && bytes[3] == 0x47)
            return true;
        else
            return false;
    }

    public static bool IsJPG(WWW wwwObj)
    {
        try
        {
            if (wwwObj != null)
            {
                if (wwwObj.isDone)
                {
                    if (string.IsNullOrEmpty(wwwObj.error) && wwwObj.texture != null && wwwObj.bytes != null)
                    {
                        return IsJPG(wwwObj.bytes);
                    }
                }
            }
        }
        catch (System.Exception err)
        {
            Debug.LogError(err.Message);
            Debug.LogError(err.StackTrace);
        }
        return false;
    }

    public static bool IsJPG(byte[] bytes)
    {
        if (bytes == null)
            return false;

        // jpg at least 10 bytes
        if (bytes.Length <= 10)
            return false;

        // ffd8 
        if (bytes[0] == 0xff && bytes[1] == 0xd8)
            return true;
        else
            return false;
    }

    public static bool IsGIF(WWW wwwObj)
    {
        try
        {
            if (wwwObj != null)
            {
                if (wwwObj.isDone)
                {
                    if (string.IsNullOrEmpty(wwwObj.error) && wwwObj.texture != null && wwwObj.bytes != null)
                    {
                        return IsGIF(wwwObj.bytes);
                    }
                }
            }
        }
        catch (System.Exception err)
        {
            Debug.LogError(err.Message);
            Debug.LogError(err.StackTrace);
        }
        return false;
    }

    public static bool IsGIF(byte[] bytes)
    {
        if (bytes == null)
            return false;

        // gif at least 10 bytes
        if (bytes.Length <= 10)
            return false;

        // 4749 4638
        if (bytes[0] == 0x47 && bytes[1] == 0x49 && bytes[2] == 0x46 && bytes[3] == 0x38)
            return true;
        else
            return false;
    }

    public static bool IsMP4(WWW wwwObj)
    {
        try
        {
            if (wwwObj != null)
            {
                if (wwwObj.isDone)
                {
                    if (string.IsNullOrEmpty(wwwObj.error) && wwwObj.bytes != null && wwwObj.bytes.Length > 24)
                    {
                        return IsMP4(wwwObj.bytes);
                    }
                }
            }
        }
        catch (System.Exception err)
        {
            Debug.LogError(err.Message);
            Debug.LogError(err.StackTrace);
        }
        return false;
    }

    public static bool IsMP4(byte[] bytes)
    {
        if (bytes == null)
            return false;

        // mp4 at least 24 bytes
        if (bytes.Length <= 24)
            return false;

        if (
            // 6674 7970
            bytes[4] == 0x66 && bytes[5] == 0x74 && bytes[6] == 0x79 && bytes[7] == 0x70
        )
        {
            return true;
        }
        else
            return false;
    }

    public static string LocalURLHead()
    {
        return "file://";
    }

    public static string LocalURL(string filePath)
    {
        if (filePath[0] == '/')
            return "file://" + filePath;
        else
            return "file://" + "/" + filePath;
    }

    public static string LocalURLToFilePath(string localFileURL)
    {
        return localFileURL.Replace("file://", "");
    }

    private static string PathFormatPrivateMethod(string path)
    {
        path = path.Trim();
        if (path[path.Length - 1] != Path.DirectorySeparatorChar)
        {
            path += Path.DirectorySeparatorChar;
        }
        return path;
    }

    public static string PathFormat(string path)
    {
        return PathFormatPrivateMethod(path);
    }

    public static string WriteFile(byte[] bytes, string fileName, string fileLocation)
    {
        try
        {
            fileLocation = PathFormatPrivateMethod(fileLocation);
            if (!Directory.Exists(fileLocation))
            {
                Directory.CreateDirectory(fileLocation);
                Debug.Log(string.Format("{0} not exist, create this folder", fileLocation));
            }
            string filePathAndName = fileLocation + fileName;
            File.WriteAllBytes(filePathAndName, bytes);
            Debug.Log("Write file: " + filePathAndName + " success");
            return filePathAndName;
        }
        catch (System.Exception exception)
        {
            Debug.LogError(exception.Message);
            Debug.LogException(exception);
        }
        return null;
    }

    public static string WriteFile(byte[] bytes, string fileFullPath)
    {
        try
        {
            FileInfo fileInfo = new FileInfo(fileFullPath);
            string path = fileInfo.Directory.FullName;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                Debug.Log(string.Format("{0} not exist, create this folder", path));
            }

            File.WriteAllBytes(fileFullPath, bytes);
            Debug.Log("Write file: " + fileFullPath + " success");
            return fileFullPath;
        }
        catch (System.Exception exception)
        {
            Debug.LogError(exception.Message);
            Debug.LogException(exception);
        }
        return null;
    }

    public static string WriteTextFile(string content, Encoding encode, string fileName, string fileLocation)
    {
        try
        {
            fileLocation = PathFormatPrivateMethod(fileLocation);
            if (!Directory.Exists(fileLocation))
            {
                Directory.CreateDirectory(fileLocation);
                Debug.Log(string.Format("{0} not exist, create this folder", fileLocation));
            }
            string filePathAndName = fileLocation + fileName;
            File.WriteAllText(filePathAndName, content, encode);
            Debug.Log("Write text file: " + filePathAndName + " success");
            return filePathAndName;
        }
        catch (System.Exception exception)
        {
            Debug.LogError(exception.Message);
            Debug.LogException(exception);
        }
        return null;
    }

    public static string WriteTextFile(string content, Encoding encode, string fileFullPath)
    {
        try
        {
            File.WriteAllText(fileFullPath, content, encode);
            Debug.Log("Write text file: " + fileFullPath + " success");
            return fileFullPath;
        }
        catch (System.Exception exception)
        {
            Debug.LogError(exception.Message);
            Debug.LogException(exception);
        }
        return null;
    }

    public static byte[] ReadFile(string fileFullPath)
    {
        try
        {
            byte[] bytes = File.ReadAllBytes(fileFullPath);
            Debug.Log("Read file: " + fileFullPath + " success");
            return bytes;
        }
        catch (System.Exception exception)
        {
            Debug.LogError(exception.Message);
            Debug.LogException(exception);
        }
        return null;
    }

    public static byte[] ReadFile(string fileName, string fileLocation)
    {
        try
        {
            fileLocation = PathFormatPrivateMethod(fileLocation);
            string filePathAndName = fileLocation + fileName;
            byte[] bytes = File.ReadAllBytes(filePathAndName);
            Debug.Log("Read file: " + filePathAndName + " success");
            return bytes;
        }
        catch (System.Exception exception)
        {
            Debug.LogError(exception.Message);
            Debug.LogException(exception);
        }
        return null;
    }

    public static string ReadTextFile(string fileFullPath, Encoding encode)
    {
        try
        {
            string str = File.ReadAllText(fileFullPath, encode);
            Debug.Log("Read text file: " + fileFullPath + " success");
            return str;
        }
        catch (System.Exception exception)
        {
            Debug.LogError(exception.Message);
            Debug.LogException(exception);
        }
        return null;
    }

    public static string ReadTextFile(string fileName, string fileLocation, Encoding encode)
    {
        try
        {
            fileLocation = PathFormatPrivateMethod(fileLocation);
            string filePathAndName = fileLocation + fileName;
            string str = File.ReadAllText(filePathAndName, encode);
            Debug.Log("Read text file: " + filePathAndName + " success");
            return str;
        }
        catch (System.Exception exception)
        {
            Debug.LogError(exception.Message);
            Debug.LogException(exception);
        }
        return null;
    }

    public static bool DeleteFile(string fileName, string location)
    {
        try
        {
            location = PathFormatPrivateMethod(location);
            string filePathAndName = location + fileName;
            File.Delete(filePathAndName);
            Debug.Log("Delete file: " + filePathAndName + " success");
            return true;
        }
        catch (System.Exception exception)
        {
            Debug.LogError(exception.Message);
            Debug.LogException(exception);
        }
        return false;
    }

    public static bool DeleteFile(string filePath)
    {
        try
        {
            File.Delete(filePath);
            Debug.Log("Delete file: " + filePath + " success");
            return true;
        }
        catch (System.Exception exception)
        {
            Debug.LogError(exception.Message);
            Debug.LogException(exception);
        }
        return false;
    }

    public static bool CleanFilesAndDirs(string path)
    {
        try
        {
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            foreach (FileInfo file in dirInfo.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in dirInfo.GetDirectories())
            {
                dir.Delete(true);
            }
        }
        catch (System.Exception exception)
        {
            Debug.LogError(exception.Message);
            Debug.LogException(exception);
        }
        return false;
    }

    public static string[] ListFiles(string path)
    {
        try
        {
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            FileInfo[] fis = dirInfo.GetFiles();
            string[] fileLocs = new string[fis.Length];
            for (int i = 0; i < fis.Length; i++)
            {
                fileLocs[i] = fis[i].ToString();
            }
            return fileLocs;
        }
        catch (System.Exception exception)
        {
            Debug.LogError(exception.Message);
            Debug.LogException(exception);
        }
        return null;
    }

    public static string[] ListDirs(string path)
    {
        try
        {
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            DirectoryInfo[] dis = dirInfo.GetDirectories();
            string[] dirLocs = new string[dis.Length];
            for (int i = 0; i < dis.Length; i++)
            {
                dirLocs[i] = dis[i].ToString();
            }
            return dirLocs;
        }
        catch (System.Exception exception)
        {
            Debug.LogError(exception.Message);
            Debug.LogException(exception);
        }
        return null;
    }

    private static bool IsFileExistPrivateMethod(string filePath)
    {
        try
        {
            bool isExist = File.Exists(filePath);
            return isExist;
        }
        catch (System.Exception exception)
        {
            Debug.LogError(exception.Message);
            Debug.LogException(exception);
        }
        return false;
    }

    public static bool IsFileExist(string fileName, string location)
    {
        try
        {
            location = PathFormatPrivateMethod(location);
            string filePathAndName = location + fileName;
            bool isExist = File.Exists(filePathAndName);
            return isExist;
        }
        catch (System.Exception exception)
        {
            Debug.LogError(exception.Message);
            Debug.LogException(exception);
        }
        return false;
    }

    public static bool IsFileExist(string fileFullPath)
    {
        return IsFileExistPrivateMethod(fileFullPath);
    }

    public static bool IsDirectoryExist(string path)
    {
        return Directory.Exists(path);
    }

    public static bool CreateDirectory(string path)
    {
        if (Directory.Exists(path))
        {
            return true;
        }
        else
        {
            try
            {
                Directory.CreateDirectory(path);
                return true;
            }
            catch (System.Exception exception)
            {
                Debug.LogError(exception.Message);
                Debug.LogException(exception);
            }
        }
        return false;
    }

    public static Sprite Texture2DToSprite(Texture2D tex, bool compress)
    {
        Rect rect = new Rect(0, 0, tex.width, tex.height);
        if (compress)
        {
            tex.Compress(true);
        }
        Sprite sp = Sprite.Create(tex, rect, new Vector2(0, 0), 1);
        return sp;
    }

    public static void ResetTransform(Transform t)
    {
        t.localPosition = Vector3.zero;
        t.localRotation = Quaternion.identity;
        t.localScale = Vector3.one;
    }

    public static void CopyTransform(Transform source, Transform target)
    {
        target.localPosition = source.localPosition;
        target.localRotation = source.localRotation;
        target.localScale = source.localScale;
    }

    public static void CopyRectTransform(RectTransform target, RectTransform source)
    {
        target.anchorMax = source.anchorMax;
        target.anchorMin = source.anchorMin;
        target.pivot = source.pivot;
        target.anchoredPosition = source.anchoredPosition;
        target.anchoredPosition3D = source.anchoredPosition3D;
        target.sizeDelta = source.sizeDelta;
    }

    public static string PrintExceptionDetails(System.Exception e)
    {
        string log = "";
        string nl = System.Environment.NewLine;
        log += "Exception occured:" + nl;

        log += "Message: " + ((e.Message != null) ? e.Message : "null") + nl;
        log += "StackTrace: " + ((e.StackTrace != null) ? e.StackTrace : "null") + nl;
        log += "Source: " + ((e.Source != null) ? e.Source : "null") + nl;
        log += "HelpLink" + ((e.HelpLink != null) ? e.HelpLink : "null") + nl;

        Debug.LogError(log);
        return log;
    }

    public static bool IsUnicodeFormat(string strText)
    {
        int aUnicodeLength = 6;
        char[] charArray = strText.ToCharArray();
        if (charArray.Length >= aUnicodeLength)
        {
            // unicode example: \u4e2d
            if (
                charArray[0] == '\\' &&
                charArray[1] == 'u' &&
                charArray[2] != 'u' && charArray[2] != '\\' &&
                charArray[3] != 'u' && charArray[3] != '\\' &&
                charArray[4] != 'u' && charArray[4] != '\\' &&
                charArray[5] != 'u' && charArray[5] != '\\'
            )
            {
                return true;
            }
        }
        return false;
    }

    public static string GrepUnicodeToString(string strText)
    {
        int aUnicodeLength = 6;
        char[] charArray = strText.ToCharArray();
        List<string> parsedStrList = new List<string>();

        if (charArray.Length >= aUnicodeLength)
        {
            for (int i = 0; i < charArray.Length; i++)
            {
                int indexFrom = i;
                int indexTo = i + (aUnicodeLength - 1);

                if (indexTo < charArray.Length)
                {
                    char[] chars = new char[aUnicodeLength];
                    for (int j = 0; j < chars.Length; j++)
                    {
                        chars[j] = charArray[indexFrom + j];
                    }
                    string s = new string(chars);
                    if (IsUnicodeFormat(s))
                    {
                        s = UnicodeToString(s);
                        parsedStrList.Add(s);
                        i = indexTo;
                        continue;
                    }
                    else
                    {
                        char c = charArray[indexFrom];
                        string s1 = System.Convert.ToString(c);
                        parsedStrList.Add(s1);
                    }
                }
                else
                {
                    for (int j = indexFrom; j < charArray.Length; j++)
                    {
                        char c = charArray[j];
                        string s = System.Convert.ToString(c);
                        parsedStrList.Add(s);
                    }
                    break;
                }
            }

            string finalStr = "";
            for (int i = 0; i < parsedStrList.Count; i++)
            {
                finalStr += parsedStrList[i];
            }
            parsedStrList = null;
            return finalStr;
        }
        else
        {
            return strText;
        }
    }

    public static string StringToUnicode(string strText)
    {
        try
        {
            string dst = "";
            char[] src = strText.ToCharArray();
            for (int i = 0; i < src.Length; i++)
            {
                byte[] bytes = Encoding.Unicode.GetBytes(src[i].ToString());
                string str = @"\u" + bytes[1].ToString("X2") + bytes[0].ToString("X2");
                dst += str;
            }
            return dst;
        }
        catch (System.Exception exc)
        {
            Debug.LogError(exc.Message);
            Debug.LogException(exc);
            return "?";
        }
    }

    public static string UnicodeToString(string unicodeText)
    {
        try
        {
            int aUnicodeLength = 6;
            string dst = "";
            string src = unicodeText;
            int len = unicodeText.Length / aUnicodeLength;

            for (int i = 0; i <= len - 1; i++)
            {
                string str = "";
                str = src.Substring(0, aUnicodeLength).Substring(2);
                src = src.Substring(aUnicodeLength);
                byte[] bytes = new byte[2];
                bytes[1] = byte.Parse(int.Parse(str.Substring(0, 2), System.Globalization.NumberStyles.HexNumber).ToString());
                bytes[0] = byte.Parse(int.Parse(str.Substring(2, 2), System.Globalization.NumberStyles.HexNumber).ToString());
                dst += Encoding.Unicode.GetString(bytes);
            }
            return dst;
        }
        catch (System.Exception exc)
        {
            Debug.LogError(exc.Message);
            Debug.LogException(exc);
            return "?";
        }
    }
}
