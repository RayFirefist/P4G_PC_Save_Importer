using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Yoshitsune.Utils
{
    public static class HashMD5
    {
        private static MD5 hasher = MD5.Create();

        /// <summary>
        /// Quick method to convert byte array into a readable hex
        /// </summary>
        /// <param name="hash">input</param>
        /// <returns>string hex</returns>
        public static string bytesToHexString(byte[] hash)
        {
            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
                sBuilder.Append(hash[i].ToString("x2"));

            return sBuilder.ToString();
        }
        
        /// <summary>
        /// Get a string MD5 hash from byte array
        /// </summary>
        /// <param name="input">input to hash</param>
        /// <returns>Hash as a string</returns>
        public static string getMd5StringHash(byte[] input)
        {
            byte[] hash = HashMD5.getMd5BytesHash(input);

            // Return the hexadecimal string.
            return HashMD5.bytesToHexString(hash);
        }

        /// <summary>
        /// Get a byte array MD5 hash from byte array
        /// </summary>
        /// <param name="input">input to hash</param>
        /// <returns>Hash as a byte array</returns>
        public static byte[] getMd5BytesHash(byte[] input)
        {
            // Convert the input string to a byte array and compute the hash.
            return HashMD5.hasher.ComputeHash(input);
        }
    }
}
