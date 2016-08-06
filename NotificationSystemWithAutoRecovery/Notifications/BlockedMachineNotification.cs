using NotificationSystemWithAutoRecovery.Services;
using System;

namespace NotificationSystemWithAutoRecovery.Notifications
{
    public class BlockedMachineNotification : IRecoverableNotification
    {
        private readonly IEmailService emailService;
        private readonly ILogService logService;
        
        public Guid Id { get; set; }
        public string EmailTo { get; set; }

        public BlockedMachineNotification(IEmailService emailService, ILogService logService)
        {
            this.emailService = emailService;
            this.logService = logService;
        }

        public void Recover()
        {
            Console.WriteLine($"Recovering notification {Id}");
            
            emailService.SendEmail(EmailTo, "Machine blocked");
            logService.Log("Machine blocked");
        }
    }
}