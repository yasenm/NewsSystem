namespace NewsSystem.Web.Helpers
{
    using NewsSystem.Web.Helpers.Contracts;
    using System.Net.Mail;

    public class EmailHelper : IEmailHelper
    {
        public void SendEmail(MailMessage message)
        {
            using (var smtp = new SmtpClient())
            {
                smtp.Send(message);
            }
        }

        public void SendEmail(string to, string from, string subject, string body)
        {
            using (var message = new MailMessage(from, to))
            {
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = true;
                SendEmail(message);
            }
        }
    }
}
