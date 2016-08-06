using System;

namespace NotificationSystemWithAutoRecovery.Services
{
    public class EmailService : IEmailService
    {
        public void SendEmail(string to, string content)
        {
            Console.WriteLine($"[{this.GetType().Name}] Sending email to {to}");
        }
    }
}