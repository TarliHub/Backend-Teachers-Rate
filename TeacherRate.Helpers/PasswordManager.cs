using System.Security.Cryptography;
using System.Text;

namespace TeacherRate.Helpers;

public static class PasswordManager
{
    public static string ComputeHashPassword(string password)
    {
        var sha = SHA256.Create();

        var bytes = Encoding.UTF8.GetBytes(password);
        var shaPassword = sha.ComputeHash(bytes);

        return Convert.ToBase64String(shaPassword);
    }
}
