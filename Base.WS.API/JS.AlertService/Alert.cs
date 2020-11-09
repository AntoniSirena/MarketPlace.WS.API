using JS.AlertService.Base;
using JS.AlertService.DTO.Request;
using Newtonsoft.Json;
using JS.AlertService.Global;
using JS.Utilities;
using System.Collections.Generic;

namespace JS.AlertService
{
    public static class Alert
    {

        public static ClientResponse<bool> SendMail(Mail request, string contenType = "application/json", Dictionary<string, string> headers = null)
        {
            var response = new ClientResponse<bool>();

            string url = string.Format("{0}{1}", Constant.IP.JSAlert, "alert/SendMail");

            var requestAlert = new
            {
                MailAddresses = request.MailAddresses,
                Subject = request.Subject,
                Body = request.Body,
            };

            string dataRequest = JsonConvert.SerializeObject(requestAlert);

            var wsResponse = JS_HttpRequest.Post(url, dataRequest, contenType, headers);
            dynamic _wsResponse = JsonConvert.DeserializeObject<dynamic>(wsResponse);

            response.Data = _wsResponse.Data;

            return response;
        }

        public static ClientResponse<bool> SendSMS(SMS request, string contenType = "application/json", Dictionary<string, string> headers = null)
        {
            var response = new ClientResponse<bool>();

            string url = string.Format("{0}{1}", Constant.IP.JSAlert, "alert/SendSMS");

            var requestAlert = new
            {
                Body = request.Body,
                PhoneNumber = request.PhoneNumber,
            };

            string dataRequest = JsonConvert.SerializeObject(requestAlert);

            var wsResponse = JS_HttpRequest.Post(url, dataRequest, contenType, headers);
            dynamic _wsResponse = JsonConvert.DeserializeObject<dynamic>(wsResponse);

            response.Data = _wsResponse.Data;

            return response;
        }


        public static string GetOperation(string request, string contenType = "application/json", Dictionary<string, string> headers = null)
        {
            string url = string.Format("{0}{1}{2}", Constant.IP.JSAlert, "alert/GetOperation?shortName=", request);

            var wsResponse = JS_HttpRequest.Get(url, contenType, headers);

            return wsResponse;
        }

    }
}
