using NotificationSystemWithAutoRecovery.Services;
using System;

namespace NotificationSystemWithAutoRecovery.Notifications
{
    public class PrintingErrorNotification : IRecoverableNotification
    {
        private readonly IPrintingService printingService;

        public Guid Id { get; set; }

        public string PrinterName { get; set; }
        public string RecoveryPrinterName { get; set; }
        public string JobId { get; set; }


        public PrintingErrorNotification(IPrintingService printingService)
        {
            this.printingService = printingService;
        }


        public void Recover()
        {
            Console.WriteLine($"Recovering notification {Id}");
            Console.WriteLine($"Reprinting job {JobId} of printer {PrinterName} in {RecoveryPrinterName}");

            printingService.Print(RecoveryPrinterName, JobId);
        }
    }
}
