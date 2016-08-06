namespace NotificationSystemWithAutoRecovery.Services
{
    public interface IEmailService
    {
        void SendEmail(string to, string content);
    }
}