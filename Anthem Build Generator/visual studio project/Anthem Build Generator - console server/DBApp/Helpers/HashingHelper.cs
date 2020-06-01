using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DBApp.Helpers
{
    /// <summary>
    /// Helper used for hashing a password
    /// </summary>
    public static class HashingHelper
    {
        /// <summary>
        /// Hashes a password
        /// </summary>
        /// <param name="text">Password+salt</param>
        /// <returns>Hashed password</returns>
        public static string GetHashedPassword(string text)
        {
            HashAlgorithm shaA = new SHA256CryptoServiceProvider();
            byte[] hash = shaA.ComputeHash(Encoding.UTF8.GetBytes(text));
            StringBuilder builder = new StringBuilder();

            for(int i=0; i<hash.Length; i++)
            {
                builder.Append(hash[i].ToString("x2"));
            }


            return builder.ToString();
        }
    }
}
