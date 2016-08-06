namespace NotificationSystemWithAutoRecovery.Services
{
    public interface IPrintingService
    {
        void Print(string printerName, string jobId);
    }
}