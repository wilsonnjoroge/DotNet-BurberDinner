
using System.Security.Cryptography;

namespace BurberDinner.Domain.Common.Utils
{
    public static class PasswordHasher
    {
        private const int SaltSize = 16; // 128 bit
        private const int KeySize = 32; // 256 bit
        private const int Iterations = 10000;

        public static string HashPassword(string password)
        {
            using var algorithm = new Rfc2898DeriveBytes(password, SaltSize, Iterations, HashAlgorithmName.SHA256);
            var salt = algorithm.Salt;
            var key = algorithm.GetBytes(KeySize);
            var hash = new byte[SaltSize + KeySize];
            Buffer.BlockCopy(salt, 0, hash, 0, SaltSize);
            Buffer.BlockCopy(key, 0, hash, SaltSize, KeySize);
            return Convert.ToBase64String(hash);
        }

        public static bool VerifyPassword(string hashedPassword, string passwordToCheck)
        {
            var hash = Convert.FromBase64String(hashedPassword);
            var salt = new byte[SaltSize];
            Buffer.BlockCopy(hash, 0, salt, 0, SaltSize);
            using var algorithm = new Rfc2898DeriveBytes(passwordToCheck, salt, Iterations, HashAlgorithmName.SHA256);
            var key = algorithm.GetBytes(KeySize);
            for (int i = 0; i < KeySize; i++)
            {
                if (hash[i + SaltSize] != key[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
