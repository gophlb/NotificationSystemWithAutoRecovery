using NotificationSystemWithAutoRecovery.Notifications;
using NotificationSystemWithAutoRecovery.Services;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace NotificationSystemWithAutoRecovery
{
    public class Program
    {
        private static ConcurrentQueue<IRecoverableNotification> recoverableNotifications;


        public static void Main(string[] args)
        {
            recoverableNotifications = new ConcurrentQueue<IRecoverableNotification>();
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            AddPrintingErrorNotifications(cancellationTokenSource.Token);
            AddBlockedMachineNotifications(cancellationTokenSource.Token);
            RecoverNotifications(cancellationTokenSource.Token);

            Console.ReadLine();

            cancellationTokenSource.Cancel();

            Console.ReadLine();
        }


        private static void RecoverNotifications(CancellationToken cancellationToken)
        {
            Task reader = Task.Factory.StartNew(
                () =>
                {
                    while (true)
                    {
                        IRecoverableNotification notification;
                        if (recoverableNotifications.TryDequeue(out notification))
                        {
                            notification.Recover();
                            Console.WriteLine();
                            Console.WriteLine();
                        }

                        cancellationToken.ThrowIfCancellationRequested();
                        Task.Delay(2000).Wait();
                    }
                },
                cancellationToken
            );
        }

        private static void AddBlockedMachineNotifications(CancellationToken cancellationToken)
        {
            ILogService logService = new FileLogService();
            IEmailService emailService = new EmailService();

            Task reader = Task.Factory.StartNew(
                () =>
                {
                    while (true)
                    {
                        AddBlockedMachineNotification(emailService, logService);

                        cancellationToken.ThrowIfCancellationRequested();
                        Task.Delay(6000).Wait();
                    }
                },
                cancellationToken
            );
        }


        private static void AddBlockedMachineNotification(IEmailService emailService, ILogService logService)
        {
            BlockedMachineNotification notification = new BlockedMachineNotification(emailService, logService);
            notification.Id = Guid.NewGuid();
            notification.EmailTo = "email@email.com";

            Console.WriteLine($"[] => BlockedMachineNotification added\n\n");
            recoverableNotifications.Enqueue(notification);
        }


        private static void AddPrintingErrorNotifications(CancellationToken cancellationToken)
        {
            IPrintingService printingService = new PrintingService();
            Random random = new Random();

            Task reader = Task.Factory.StartNew(
                () =>
                {
                    while (!cancellationToken.IsCancellationRequested)
                    {
                        AddPrintingErrorNotification(printingService, random);
                        
                        cancellationToken.ThrowIfCancellationRequested();
                        Task.Delay(3000).Wait();
                    }
                }
            );
        }

        private static void AddPrintingErrorNotification(IPrintingService printingService, Random random)
        {
            PrintingErrorNotification notification = new PrintingErrorNotification(printingService);
            notification.Id = Guid.NewGuid();
            notification.JobId = Guid.NewGuid().ToString();
            notification.PrinterName = $"Printer {random.Next(0, 10)}";
            notification.RecoveryPrinterName = "Recovery Printer";

            Console.WriteLine($"[] => PrintingErrorNotification added\n\n");
            recoverableNotifications.Enqueue(notification);
        }
    }
}