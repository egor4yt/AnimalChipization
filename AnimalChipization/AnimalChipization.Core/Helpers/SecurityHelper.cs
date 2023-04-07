using System.Collections;
using System.Globalization;
using System.IO.Compression;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;

namespace AnimalChipization.Core.Helpers;

public static class SecurityHelper
{
    public static string ComputeSha256Hash(string rawData)
    {
        using var sha256Hash = SHA256.Create();
        var inputBytes = Encoding.UTF8.GetBytes(rawData);
        var hashBytes = sha256Hash.ComputeHash(inputBytes);

        return Convert.ToHexString(hashBytes);
    }

    public static string Base64Encode(string plainText)
    {
        var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
        return Convert.ToBase64String(plainTextBytes);
    }

    public static string Base64Decode(string base64EncodedData)
    {
        var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
        return Encoding.UTF8.GetString(base64EncodedBytes);
    }

    private static string HexBytesToString(byte[] hashValue)
    {
        var sb = new StringBuilder();

        // Convert the byte array to string format
        foreach (var b in hashValue) sb.Append($"{b:X2}");
        
        return sb.ToString();
    }

    private static byte[] ComputeMd5CheckSum(string s)
    {

        // Initialize a MD5 hash object
        using var md5 = MD5.Create();
        // Compute the hash of the given string
        var hashValue = md5.ComputeHash(Encoding.UTF8.GetBytes(s));
        return hashValue;
        
    }

    public static string Hex2String(string input)
    {
        var builder = new StringBuilder();
        for (var i = 0; i < input.Length; i += 2)
        {
            //throws an exception if not properly formatted
            var hexdec = input.Substring(i, 2);
            var number = int.Parse(hexdec, NumberStyles.HexNumber);
            var charToAdd = (char)number;
            builder.Append(charToAdd);
        }

        return builder.ToString();
    }
    

    public static string CalculateGeoHashV3(string input)
    {

        // var hexString = Hex2String(md5CheckSum);
        var base64 = Base64Encode(input);
        var md5CheckSum = ComputeSha256Hash(base64);
        var hex2 = Hex2String(md5CheckSum);
        // var hashValue = md5.ComputeHash(Encoding.UTF8.GetBytes(input));

        return "";
    }
}