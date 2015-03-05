using System;
using System.Security.Cryptography;
using System.Text;

namespace Framesharp.Common.Cryptography
{
    public class PasswordHashHelper
    {
        public static void GetPasswordHash(string password, out byte[] hash, out byte[] salt)
        {
            salt = GenerateSaltForPasswordHash(7);

            hash = ComputeHash(password, salt);
        }

        public static byte[] ComputeHash(string password, byte[] salt)
        {
            return ComputeHash(Encoding.UTF8.GetBytes(password), salt);
        }

        public static byte[] ComputeHash(byte[] passwordBytes, byte[] salt)
        {
            if (passwordBytes == null || salt == null)
                throw new ArgumentNullException("Password could not be processed.");

            byte[] computedHash = new byte[passwordBytes.Length + salt.Length];

            Array.Copy(passwordBytes, computedHash, passwordBytes.Length);
            Array.Copy(salt, 0, computedHash, passwordBytes.Length, salt.Length);

            return new SHA256Managed().ComputeHash(computedHash);
        }

        public static byte[] GenerateSaltForPasswordHash(int size)
        {
            var cryptoService = new RNGCryptoServiceProvider();

            var result = new byte[size];

            cryptoService.GetNonZeroBytes(result);

            return result;
        }

        /// <summary>
        /// Checks the password and does a byte-by-byte comparision between the password 
        ///  that was supplied by the client and the one stored in the database.
        /// </summary>
        /// <param name="clientHash">Password supplied by the client</param>
        /// <param name="serverHash">Password supplied by the server</param>
        /// <returns>Result of the passwords comparision</returns>
        public static bool CheckPassword(byte[] clientHash, byte[] serverHash)
        {
            if (!clientHash.Length.Equals(serverHash.Length)) return false;

            for (int i = 0; i < clientHash.Length; i++)
            {
                if (!clientHash[i].Equals(serverHash[i])) return false;
            }

            return true;
        }
    }
}
