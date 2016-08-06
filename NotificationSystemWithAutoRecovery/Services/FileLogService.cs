using System;

namespace NotificationSystemWithAutoRecovery.Services
{
    public class FileLogService : ILogService
    {
        public void Log(string message)
        {
            Console.WriteLine($"[{this.GetType().Name}] Logging message '{message}' into file");
        }
    }
}