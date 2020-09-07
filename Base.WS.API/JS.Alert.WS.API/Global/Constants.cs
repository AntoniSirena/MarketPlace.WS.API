using System.Configuration;

namespace JS.Alert.WS.API.Global
{
    public static  class Constants
    {
        static Constants()
        {

        }

        public static class ConnectionStrings
        {
            public static string JSBase { get { return ConfigurationManager.ConnectionStrings["JS.Base"].ConnectionString; } }
            public static string JSAlert { get { return ConfigurationManager.ConnectionStrings["JS.Alert"].ConnectionString; } }
        }
    }
}