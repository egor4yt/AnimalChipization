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

    public static IEnumerable<byte> ComputeMd5CheckSum(string baseString)
    {
        using var md5 = MD5.Create();
        return md5.ComputeHash(Encoding.UTF8.GetBytes(baseString));
    }
}