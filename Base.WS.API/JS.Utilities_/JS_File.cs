using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.Utilities
{
    public static class JS_File
    {

        public static string GetStrigBase64(string path)
        {
            string result = string.Empty;

            if (!String.IsNullOrEmpty(path))
            {
                byte[] file = File.ReadAllBytes(path);
                result = Convert.ToBase64String(file);
            }

            return result;
        }

    }
}
