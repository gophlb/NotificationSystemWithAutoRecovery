using System;

namespace NotificationSystemWithAutoRecovery.Notifications
{
    public interface IRecoverableNotification
    {
        Guid Id { get; set; }
        void Recover();
    }
}