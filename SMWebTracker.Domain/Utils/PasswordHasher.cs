using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SMWebTracker.Domain.Utils
{
    public class PasswordHasher
    {
        private const int HASH_ITERATIONS = 1783;
        #warning TODO: Levar HASH_ITERATIONS para as configurações

        public static string[] GerarHash(string password)
        {
            var salt = RandomNumberGenerator.GetBytes(32);          

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, HASH_ITERATIONS);
            byte[] hash = pbkdf2.GetBytes(64);

            return new string[] { Convert.ToBase64String(hash), Convert.ToBase64String(salt) };
        }

        public static bool IsValid(string testPassword, string hash, string salt)
        {
            var saltbytes = Convert.FromBase64String(salt);

            var pbkdf2 = new Rfc2898DeriveBytes(testPassword, saltbytes, HASH_ITERATIONS);
            byte[] testHash = pbkdf2.GetBytes(64);

            if (Convert.ToBase64String(testHash).Equals(hash))
                return true;

            return false;
        }
    }
}
