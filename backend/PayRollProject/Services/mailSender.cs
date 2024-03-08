using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Model;
using System.Diagnostics;

namespace PayRollProject.Services
{
    public class mailSender
    {
        public static void SendEmail(string senderEmail, string senderName, string recieverMail, string recieverName, string subject, string message)
        {
            var apiInstance = new TransactionalEmailsApi();

            SendSmtpEmailSender sender = new SendSmtpEmailSender(senderName, senderEmail);

            SendSmtpEmailTo reciever1 = new SendSmtpEmailTo(recieverMail, recieverName);
            List<SendSmtpEmailTo> To = new List<SendSmtpEmailTo>();
            To.Add(reciever1);

            string HtmlContent = null;
            string TextContent = message;
            //string Subject = "My {{params.subject}}";

            try
            {
                var sendSmtpEmail = new SendSmtpEmail(sender, To, null, null, HtmlContent, TextContent, subject);
                CreateSmtpEmail result = apiInstance.SendTransacEmail(sendSmtpEmail);

                Console.WriteLine("Brevo response: " + result.ToJson());

            }
            catch (Exception e)
            {
                Debug.WriteLine("We have an exception: " + e.Message);
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }
        }
    }
}
