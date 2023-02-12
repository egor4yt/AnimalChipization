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
    
    public static string Base64Decode(string base64EncodedData) {
        var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
        return Encoding.UTF8.GetString(base64EncodedBytes);
    }
}