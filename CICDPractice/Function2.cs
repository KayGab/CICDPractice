using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using System.Net;
using System.Net.Mail;

namespace CICDPractice
{
    public static class Function2
    {
        [FunctionName("Function2")]
        public static void Run([TimerTrigger("0 */1 * * * *")]TimerInfo myTimer, TraceWriter log)
        {
            log.Info($"C# Timer trigger function executed at: {DateTime.Now}");
            try
            {
                Send();
            }
            catch (Exception ex)
            {
                log.Error($"Erro sending mail: {DateTime.Now}");
            }
        }

        private static void Send()
        {
            var fromAddress = new MailAddress("khay4real3383@gmail.com", "Kay Otubu MailTester");
            var toAddress = new MailAddress("oluwakayode.otubu@gmail.com", "Kay Received Name");
            const string fromPassword = "Micah_3383_$";
            const string subject = "Subject";
            const string body = "Hello.......... Checking EMail from Azure Function";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            smtp.Send(message);
        }
    }



}
