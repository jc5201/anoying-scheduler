using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tulpep.NotificationWindow;

namespace scheduler
{
    class NotificationManager
    {
        public void showNotificationWithMsg(string msg)
        {
            PopupNotifier popup = new PopupNotifier();
            popup.TitleText = "AS";
            popup.ContentText = msg;
            popup.Popup();
        }
    }
}
