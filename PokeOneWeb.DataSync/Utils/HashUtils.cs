using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace PokeOneWeb.DataSync.Utils
{
    public static class HashUtils
    {
        public static string GetHashForDataRow(List<object> data)
        {
            var stringToHash = string.Join(';', data.Select(x => x.ToString()));
            var bytesToHash = Encoding.UTF8.GetBytes(stringToHash);
            var hash = SHA1.HashData(bytesToHash);
            return Convert.ToHexString(hash);
        }
    }
}