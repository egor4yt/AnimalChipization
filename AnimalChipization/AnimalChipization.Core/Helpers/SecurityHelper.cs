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
}