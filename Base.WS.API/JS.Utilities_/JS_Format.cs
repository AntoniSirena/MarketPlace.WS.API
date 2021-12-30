using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.Utilities
{
    public static class JS_Format
    {

        public static string PhoneNumber(string phoneNumber)
        {
            return phoneNumber.Insert(3, "-").Insert(7, "-");
        }

    }
}
