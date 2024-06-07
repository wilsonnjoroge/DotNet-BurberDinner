using BCryptNet = BCrypt.Net.BCrypt;

namespace BurberDinner.Domain.Common.Utils
{
    public static class PasswordHasher
    {
        // Cost factor for Bcrypt hashing
        private const int BcryptWorkFactor = 12;

        public static string HashPassword(string password)
        {
            return BCryptNet.HashPassword(password, BcryptWorkFactor);
        }

        public static bool VerifyPassword(string hashedPassword, string passwordToCheck)
        {
            return BCryptNet.Verify(passwordToCheck, hashedPassword);
        }
    }
}
