
using System;
using JS.Alert.WS.API.DTO.Request.Alert;
using JS.Alert.WS.API.Services.IServices;
using System.Net.Mail;
using System.Net;
using System.Configuration;

namespace JS.Alert.WS.API.Services
{
    public class AlertService : IAlertService
    {

        public bool SendMail(Mail request)
        {
            bool response = false;
            string[] mails = request.MailAddresses.Split(',');

            request.Body = request.Body.Replace("\\r\\n", "");
            request.Body = request.Body.Replace("\"", "");

            try
            {
                var fromAddress = new MailAddress(ConfigurationManager.AppSettings["MailAddress"], ConfigurationManager.AppSettings["MailName"]);
                string fromPassword = ConfigurationManager.AppSettings["Password"];

                foreach (var item in mails)
                {
                    var toAddress = new MailAddress(item);
                    string subject = request.Subject;
                    string body = request.Body;

                    var smtp = new SmtpClient
                    {
                        Host = ConfigurationManager.AppSettings["Host"],
                        Port = Convert.ToInt32(ConfigurationManager.AppSettings["Port"]),
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(fromAddress.Address, fromPassword)

                    };
                    using (var message = new MailMessage(fromAddress, toAddress)
                    {
                        Subject = subject,
                        Body = body,
                        IsBodyHtml = true,
                    })
                    {
                        smtp.Send(message);
                    }
                }

                response = true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error enviando el correo" + ex.InnerException);
            }

            return response;
        }

    }
}