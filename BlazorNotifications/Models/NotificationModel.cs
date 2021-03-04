using System;

namespace BlazorNotifications.Models
{
    public class NotificationModel
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public bool Fade { get; set; }
        public bool ShouldFade { get; set; }
        public bool AutoClose { get; set; }
        public bool KeepAfterRouteChange { get; set; }
        public NotificationType Type { get; set; }
    }

    public enum NotificationType
    {
        Default = 1,
        Warning = 2,
        Error = 3,
        Success = 4,
        Info = 5
    }
}