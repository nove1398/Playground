using BlazorNotifications.Models;
using System;

namespace BlazorNotifications.Services
{
    public interface INotificationService
    {
        event Action<NotificationModel> OnAlert;

        void Success(string message, bool keepAfterRouteChange = false, bool autoClose = true, bool fade = true);

        void Error(string message, bool keepAfterRouteChange = false, bool autoClose = true, bool fade = true);

        void Info(string message, bool keepAfterRouteChange = false, bool autoClose = true, bool fade = true);

        void Warn(string message, bool keepAfterRouteChange = false, bool autoClose = true, bool fade = true);

        void TriggerNotification(NotificationModel alert);

        void ClearNotification(Guid id);
    }

    public class NotificationService : INotificationService
    {
        public event Action<NotificationModel> OnAlert;

        public void Success(string message, bool keepAfterRouteChange = false, bool autoClose = true, bool fade = true)
        {
            TriggerNotification(new NotificationModel
            {
                Type = NotificationType.Success,
                Text = message,
                KeepAfterRouteChange = keepAfterRouteChange,
                AutoClose = autoClose,
                ShouldFade = fade
            });
        }

        public void Error(string message, bool keepAfterRouteChange = false, bool autoClose = true, bool fade = true)
        {
            TriggerNotification(new NotificationModel
            {
                Type = NotificationType.Error,
                Text = message,
                KeepAfterRouteChange = keepAfterRouteChange,
                AutoClose = autoClose,
                ShouldFade = fade
            });
        }

        public void Info(string message, bool keepAfterRouteChange = false, bool autoClose = true, bool fade = true)
        {
            TriggerNotification(new NotificationModel
            {
                Type = NotificationType.Info,
                Text = message,
                KeepAfterRouteChange = keepAfterRouteChange,
                AutoClose = autoClose,
                ShouldFade = fade
            });
        }

        public void Warn(string message, bool keepAfterRouteChange = false, bool autoClose = true, bool fade = true)
        {
            TriggerNotification(new NotificationModel
            {
                Type = NotificationType.Warning,
                Text = message,
                KeepAfterRouteChange = keepAfterRouteChange,
                AutoClose = autoClose,
                ShouldFade = fade
            });
        }

        public void TriggerNotification(NotificationModel alert)
        {
            alert.Id = Guid.NewGuid();
            OnAlert?.Invoke(alert);
        }

        public void ClearNotification(Guid id)
        {
            OnAlert?.Invoke(new NotificationModel { Id = id });
        }
    }
}