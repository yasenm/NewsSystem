namespace NewsSystem.Web.Helpers.Contracts
{
    using System.Net.Mail;

    public interface IEmailHelper
    {
        void SendEmail(MailMessage message);
        void SendEmail(string to, string from, string subject, string body);
    }
}
