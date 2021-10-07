
using System;
using JS.Alert.WS.API.DTO.Request.Alert;
using JS.Alert.WS.API.Services.IServices;
using System.Net.Mail;
using System.Net;
using System.Configuration;
using Twilio.Exceptions;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using System.Text;

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
                    if (!string.IsNullOrEmpty(item))
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

                        if (request.SendFileAttach)
                        {
                            using (var message = new MailMessage(fromAddress, toAddress)
                            {
                                Subject = subject,
                                Body = body,
                                IsBodyHtml = true,
                            })
                            {
                                var files = request.PathFileAttach.Split(',');
                                foreach (var file in files)
                                {
                                    message.Attachments.Add(new Attachment(file));
                                }
                                
                                smtp.Send(message);
                            }
                        }
                        else
                        {
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


        public bool SendSMS(SMS request)
        {
            bool result = false;

            string accountSid = ConfigurationManager.AppSettings["AccountSid"];
            string authToken = ConfigurationManager.AppSettings["AuthToken"];
            string phoneNumber = ConfigurationManager.AppSettings["PhoneNumber"];

            request.PhoneNumber = request.PhoneNumber.Replace("-", "");
            request.PhoneNumber = request.PhoneNumber.Replace(" ", "");
            request.PhoneNumber = request.PhoneNumber.Replace("(", "");
            request.PhoneNumber = request.PhoneNumber.Replace(")", "");

            try
            {
                TwilioClient.Init(accountSid, authToken);

                var message = MessageResource.Create(
                    body: request.Body,
                    from: new Twilio.Types.PhoneNumber(phoneNumber),
                    to: new Twilio.Types.PhoneNumber(request.PhoneNumber)
                );

                result = true;
            }
            catch (TwilioException ex)
            {
                throw (ex);
            }

            return result;
        }

    }
}