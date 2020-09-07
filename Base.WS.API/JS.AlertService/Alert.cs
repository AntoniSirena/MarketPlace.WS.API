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

        public static ClientResponse<bool> SendMail(Mail request, string contenType, Dictionary<string, string> headers)
        {
            var response = new ClientResponse<bool>();

            string url = string.Format("{0}{1}", Constant.IPE.JSAlert, "alert/SendMail");

            var requestAlert = new
            {
                MailAddresses = request.MailAddresses,
                Subject = request.Subject,
                Body = request.Body,
            };

            string dataRequest = JsonConvert.SerializeObject(requestAlert);

            var wsResponse = JS_HttpRequest.Post(url, dataRequest, contenType, headers);

            return response;
        }

    }
}
