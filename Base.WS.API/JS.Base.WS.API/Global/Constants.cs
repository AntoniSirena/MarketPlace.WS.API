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

        public static class RequestStatus
        {
            public const string InProcess = "InProcess";
            public const string Completed = "Completed";
        }

        public static class Areas
        {
            public const string Pending = "Pendiente";
        }

        public static class Indicators
        {
            public const string IndicadorInicial = "IndicadorInicial";
        }

        public static class Varibels
        {
            public const string A = "A";
            public const string B = "B";
            public const string C = "C";
            public const string D = "D";
            public const string E = "E";
            public const string F = "F";
            public const string G = "G";
            public const string H = "H";
        }

        public static class Genders
        {
            public const string Maculino = "Maculino";
            public const string Femenino = "Femenino";
        }

        public static class InternalResponseMessageGood
        {
            public const string Message200 = "Registro creado con éxito";

            public const string Message201 = "Registro actualizado con éxito";

            public const string Message202 = "Registro eliminado con éxito";
        }

        public static class InternalResponseCodeError
        {
            public const string Code300 = "300";
            public const string Message300 = "Estimado usuario el registro no fué encontrado";

            public const string Code301 = "301";
            public const string Message301 = "Estimado usuario ha ocurrido un error procesando su solicitud";

            public const string Code302 = "302";
            public const string Message302 = "Estimado usuario el registro ya existe";

            public const string Code303 = "303";
            public const string Message303 = "Estimado usuario el código que intenta registrar ya existe";

            public const string Code304 = "304";
            public const string Message304 = "Estimado usuario la descripción que intenta registrar ya existe";

            public const string Code305 = "30";
            public const string Message305 = "Favor debes completar los datos de persona, para crear localizadores";

            public const string Code306 = "306";
            public const string Message306 = "El código de seguridad no es valido, si desea puede continuar sin ingresar el mismo";

            public const string Code307 = "307";
            public const string Message307 = "El código de seguridad no es valido";

            public const string Code308 = "308";
            public const string Message308 = "El código de seguridad es obligatorio, para continuar con el proceso";

            public const string Code309 = "309";
            public const string Message309 = "Estimado usuario el nombre corto que intenta registrar ya existe";

            public const string Code310 = "310";
            public const string Message310 = "Estimado usuario el nombre que intenta registrar ya existe";

            public const string Code311 = "311";
            public const string Message311 = "Ya existe un Docente registrado con el mismo número de docummento";

            public const string Code312 = "312";
            public const string Message312 = "No existen Instrumentos de Acompañamientos asociados a su usuario";
        }

        public static class ConfigurationParameter
        {
            public static string SystemConfigurationTemplate { get { return ConfigurationParameterService.GetParameter("SystemConfigurationTemplate"); } }

            public static string StatusExternalUser { get { return ConfigurationParameterService.GetParameter("StatusExternalUser") ?? UserStatuses.PendingToActive; } }

            public static string LoginTime { get { return ConfigurationParameterService.GetParameter("LoginTime")?? "5"; } }

            public static string RoleExternalUser { get { return ConfigurationParameterService.GetParameter("RoleExternalUser") ?? "Client"; } }

            public static string EnableRegistrationButton { get { return ConfigurationParameterService.GetParameter("EnableRegistrationButton") ?? "0"; } }

            public static string Required_SecurityCodeExternaRegister { get { return ConfigurationParameterService.GetParameter("Required_SecurityCodeExternaRegister") ?? "0"; } }

            public static string ViewAllAccompanyingInstrumentRequests_ByRoles { get { return ConfigurationParameterService.GetParameter("ViewAllAccompanyingInstrumentRequests_ByRoles") ?? ","; } }

        }

    }
}