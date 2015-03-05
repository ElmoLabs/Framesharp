using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Framesharp.Common.Cryptography
{
    public class HashGenerator
    {
        public static string Encode(string text)
        {
            byte[] key = Encoding.ASCII.GetBytes("abcdefghijklmnopqrstuvwxyz");

            return Encode(text, key);
        }

        public static string Encode(string text, byte[] key)
        {
            HMACSHA1 hmacsha1 = new HMACSHA1(key);
   
            MemoryStream stream = new MemoryStream(Encoding.ASCII.GetBytes(text));

            return hmacsha1.ComputeHash(stream).Aggregate("", (s, e) => s + String.Format("{0:x2}", e), s => s);
        }
    }
}
