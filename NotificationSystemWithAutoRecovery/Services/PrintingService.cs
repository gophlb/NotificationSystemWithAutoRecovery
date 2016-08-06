using System;

namespace NotificationSystemWithAutoRecovery.Services
{
    public class PrintingService : IPrintingService
    {
        public void Print(string printerName, string jobId)
        {
            Console.WriteLine($"[{this.GetType().Name}] Printing job {jobId} in {printerName}");
        }
    }
}