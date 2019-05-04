using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Exaxxi.Models
{
    public static class MyHashTool
    {
        public static string ToSHA512(this string input)
        {
            using (var algorithm = SHA512.Create())
            {
                var hashedBytes =
               algorithm.ComputeHash(Encoding.UTF8.GetBytes(input));
                return BitConverter.ToString(hashedBytes).Replace("-",
               "").ToLower();
            }
        }
    }
}
