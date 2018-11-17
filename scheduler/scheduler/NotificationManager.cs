
using System;
using System.Windows;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Position;
using ToastNotifications.Messages;
using ToastNotifications.Messages.Core;

namespace scheduler
{
    class NotificationManager
    {
        Notifier notifier = new Notifier(cfg =>
        {
            cfg.PositionProvider = new PrimaryScreenPositionProvider(
                corner: Corner.BottomRight,
                offsetX: 10,
                offsetY: 10);

            cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                notificationLifetime: TimeSpan.FromSeconds(3),
                maximumNotificationCount: MaximumNotificationCount.FromCount(5));

            cfg.Dispatcher = Application.Current.Dispatcher;

            cfg.DisplayOptions.Width = 500;
            cfg.DisplayOptions.TopMost = true;
        });

        

        public void showNotificationWithMsg(String msg)
        {
            var options = new ToastNotifications.Core.MessageOptions
            {
                FontSize = 20, // set notification font size
                ShowCloseButton = false, // set the option to show or hide notification close button
                Tag = "Any object or value which might matter in callbacks",
            };
            notifier.ShowError(msg,options);
        }
    }
}
