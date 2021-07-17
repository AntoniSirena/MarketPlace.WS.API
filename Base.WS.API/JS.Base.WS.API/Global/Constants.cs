using JS.Base.WS.API.DBContext;
using JS.Base.WS.API.Services;
using System.Configuration;

namespace JS.Base.WS.API.Global
{
    public static class Constants
    {

        private static ConfigurationParameterService ConfigurationParameterService;

        static Constants()
        {
            ConfigurationParameterService = new ConfigurationParameterService();
        }


        public static class ConnectionStrings
        {
            public static string JSBase { get { return ConfigurationManager.ConnectionStrings["JS.Base"].ConnectionString; } }
            public static string JSAlert { get { return ConfigurationManager.ConnectionStrings["JS.Alert"].ConnectionString; } }
        }

        public static class UserStatuses
        {
            public const string Active = "Active";
            public const string Inactive = "Inactive";
            public const string PendingToActive = "PendingToActive";
            public const string PendingToChangePassword = "PendingToChangePassword";
        }


        public static class Genders
        {
            public const string Maculino = "Maculino";
            public const string Femenino = "Femenino";
        }

        public static class AppointmentStatus
        {
            public const string Pending = "Pending";
            public const string OnHold = "OnHold";
            public const string InProcess = "InProcess";
            public const string Finished = "Finished";
            public const string Cancelled = "Cancelled";
        }

        public static class DocumentTypes
        {
            public const string Cédula = "Cédula";
            public const string Pasaporte = "Pasaporte";
            public const string RNC = "RNC";
        }

        public static class UserTypes
        {
            public const string Person = "Person";
            public const string Enterprise = "Enterprise";
        }

        public static class InternalResponseMessageGood
        {
            public const string Message200 = "Registro creado con éxito";

            public const string Message201 = "Registro actualizado con éxito";

            public const string Message202 = "Registro eliminado con éxito";

            public const string Message203 = "Registros actualizados con éxito";

            public const string Message204 = "Registros eliminados con éxito";

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

            public const string Code313 = "313";
            public const string Message313 = "Indicadores no encontrados para este formulario";

            public const string Code314 = "314";
            public const string Message314 = "Comentarios no encontrados para este formulario";

            public const string Code315 = "315";
            public const string Message315 = "Descripciones no encontradas para este formulario";

            public const string Code316 = "316";
            public const string Message316 = "Sugerencias y acuerdos no encontrados para este formulario";

            public const string Code317 = "317";
            public const string Message317 = "";

            public const string Code318 = "318";
            public const string Message318 = "No existe una operación definida";

            public const string Code319 = "319";
            public const string Message319 = "Existe un archivo registrado con el mismo nombre del archivo que intenta subir, favor cambiar el mismo";

            public const string Code320 = "320";
            public const string Message320 = "";

            public const string Code323 = "323";
            public const string Message323 = "El tipo de documento que intenta subir es desconocido";

            public const string Code324 = "324";
            public const string Message324 = "El tipo de imagen que intenta subir es desconocido";

            public const string Code325 = "325";
            public const string Message325 = "Estimado usuario la empresa ya se encuentra registrada";

        }

        public static class ConfigurationParameter
        {
            public static string SystemConfigurationTemplate { get { return ConfigurationParameterService.GetParameter("SystemConfigurationTemplate"); } }

            public static string StatusExternalUser { get { return ConfigurationParameterService.GetParameter("StatusExternalUser") ?? UserStatuses.PendingToActive; } }

            public static string LoginTime { get { return ConfigurationParameterService.GetParameter("LoginTime") ?? "5"; } }

            public static string RoleExternalUser { get { return ConfigurationParameterService.GetParameter("RoleExternalUser") ?? "Client"; } }

            public static string EnableRegistrationButton { get { return ConfigurationParameterService.GetParameter("EnableRegistrationButton") ?? "0"; } }

            public static string Required_SecurityCodeExternaRegister { get { return ConfigurationParameterService.GetParameter("Required_SecurityCodeExternaRegister") ?? "0"; } }

            public static string ViewAllAccompanyingInstrumentRequests_ByRoles { get { return ConfigurationParameterService.GetParameter("ViewAllAccompanyingInstrumentRequests_ByRoles") ?? ","; } }

            public static string FileDirectory { get { return ConfigurationParameterService.GetParameter("FileDirectory") ?? @"C:\Shared\File"; } }

            public static string PublicityFileDirectory { get { return ConfigurationParameterService.GetParameter("PublicityFileDirectory") ?? @"C:\SharedGuerra\Publicity\Novelties"; } }

            public static string FileTypeAllowed { get { return ConfigurationParameterService.GetParameter("FileTypeAllowed") ?? string.Empty; } }

            public static string ImgTypeAllowed { get { return ConfigurationParameterService.GetParameter("ImgTypeAllowed") ?? string.Empty; } }

            public static string URL_ConfirmPassword { get { return ConfigurationParameterService.GetParameter("URL_ConfirmPassword") ?? string.Empty; } }

            public static string SecondFactorAuthentication { get { return ConfigurationParameterService.GetParameter("SecondFactorAuthentication") ?? "false"; } }

            public static string URL_SecondFactorAuthentication { get { return ConfigurationParameterService.GetParameter("URL_SecondFactorAuthentication") ?? string.Empty; } }

            public static string SendEmailAlert_SecondFactorAuthentication { get { return ConfigurationParameterService.GetParameter("SendEmailAlert_SecondFactorAuthentication") ?? "false"; } }

            public static string SendSMSAlert_SecondFactorAuthentication { get { return ConfigurationParameterService.GetParameter("SendSMSAlert_SecondFactorAuthentication") ?? "false"; } }

            public static string SecurityCode_ExpirationTime_SecondFactorAuthentication { get { return ConfigurationParameterService.GetParameter("SecurityCode_ExpirationTime_SecondFactorAuthentication") ?? "2"; } }

            public static string AllowViewAllEnterprisesByRoles { get { return ConfigurationParameterService.GetParameter("AllowViewAllEnterprisesByRoles") ?? "SuperAdmin,Admin"; } }

            public static string EnterpriseImgDirectory { get { return ConfigurationParameterService.GetParameter("EnterpriseImgDirectory") ?? @"C:\SharedGuerra\Enterprise\Images"; } }
            
            public static string AllowViewAllAppointmentByRoles { get { return ConfigurationParameterService.GetParameter("AllowViewAllAppointmentByRoles") ?? "SuperAdmin,Admin"; } }

            public static string MarketImgDirectory { get { return ConfigurationParameterService.GetParameter("MarketImgDirectory") ?? @"C:\SharedGuerra\Market\Images"; } }

            public static string AllowViewAllMarketsByRoles { get { return ConfigurationParameterService.GetParameter("AllowViewAllMarketsByRoles") ?? "SuperAdmin,Admin"; } }

            public static string MaximumImgQuantityMarketDetail { get { return ConfigurationParameterService.GetParameter("MaximumImgQuantityMarketDetail") ?? "5"; } }

            public static string ItemsByPageMarkeetPlace { get { return ConfigurationParameterService.GetParameter("ItemsByPageMarkeetPlace") ?? "6"; } }

            public static string UserAvataDefault { get { return ConfigurationParameterService.GetParameter("UserAvataDefault") ?? ""; } }

            public static string UserAvatarFileDirectory { get { return ConfigurationParameterService.GetParameter("UserAvatarFileDirectory") ?? ""; } }

            public static string EnableShoppingCart { get { return ConfigurationParameterService.GetParameter("EnableShoppingCart") ?? "false"; } }

        }

    }
}