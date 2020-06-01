using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DBApp.Helpers
{
    /// <summary>
    /// Helper used for getting a random salt
    /// </summary>
    public static class SaltHelper
    {
        public static string GetRandomSalt()
        {
            RNGCryptoServiceProvider rngCSP = new RNGCryptoServiceProvider();
            StringBuilder builder = new StringBuilder();
            byte[] salt = new byte[16];

            rngCSP.GetBytes(salt);
            for(int i=0; i<salt.Length; i++)
            {
                builder.Append(salt[i].ToString("x2"));
            }

            return builder.ToString();
        }
    }
}
