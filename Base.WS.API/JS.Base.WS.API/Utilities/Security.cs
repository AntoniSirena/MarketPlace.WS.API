using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace JS.Base.WS.API.Utilities
{
    public static class Security
    {
        public static string EncryptOneWay(string Text)
        {
            string result = string.Empty;

            UTF8Encoding encoder = new UTF8Encoding();
            SHA256Managed sha256hasher = new SHA256Managed();
            byte[] hashedDataBytes = sha256hasher.ComputeHash(encoder.GetBytes(Text));
            result = Convert.ToBase64String(hashedDataBytes);

            return result;
        }
    }
}