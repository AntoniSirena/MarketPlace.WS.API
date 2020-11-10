using JS.Alert.WS.API.DTO.Request.Alert;


namespace JS.Alert.WS.API.Services.IServices
{
    interface IAlertService
    {
        bool SendMail(Mail request);
        bool SendSMS(SMS request);
    }
}
