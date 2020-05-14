using JS.Base.WS.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JS.Base.WS.API.Global
{
    public static class Constants
    {

        private static ConfigurationParameterService ConfigurationParameterService;

        static Constants()
        {
            ConfigurationParameterService = new ConfigurationParameterService();
        }

        public static class UserStatuses
        {
            public const string Active = "Active";
            public const string Inactive = "Inactive";
            public const string PendingToActive = "PendingToActive";
        }

        public static class Genders
        {
            public const string Male = "Male";
            public const string Female = "Female";
        }

        public static class ConfigurationParameter
        {
            public static string SystemConfigurationTemplate { get { return ConfigurationParameterService.GetParameter("SystemConfigurationTemplate"); } }
            public static string StatusExternalUser { get { return ConfigurationParameterService.GetParameter("StatusExternalUser") ?? UserStatuses.PendingToActive; } }
            public static string LoginTime { get { return ConfigurationParameterService.GetParameter("LoginTime")?? "5"; } }
            public static string RoleExternalUser { get { return ConfigurationParameterService.GetParameter("RoleExternalUser") ?? "Client"; } }

        }

    }
}