using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Timers;
using System.Reflection;

namespace scheduler
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Timer timer = new System.Timers.Timer();
            timer.Interval = 5 * 1000; 
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            timer.Start();
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (GoogleCalendarManager.ExistTaskInADay() && BanListManager.IsBanProcessRunning())
            {
                String[] msgs = { "Stop playing Game!", "What are you doing?", "Go Back to your work!", "Did you finished your task?" };
                Random rnd = new Random();
                Dispatcher.BeginInvoke(new Action(delegate {
                    new NotificationManager().showNotificationWithMsg(msgs[rnd.Next() % (msgs.Length)]);
                    }));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BanSetting bWin = new BanSetting();
            bWin.Show();
        }

        private void Calendar_Initialized(object sender, EventArgs e)
        {
            GoogleCalendarManager.init();
            Calendar_Select_From_Google();
        }
        

        private void Calendar_Select_From_Google()
        {
            calendar.SelectedDatesChanged -= Calendar_SelectedDatesChanged;
            calendar.SelectedDates.Clear();
            foreach (DateTime dt in GoogleCalendarManager.GetDateTimes())
            {
                calendar.SelectedDates.Add(dt);
            }
            calendar.SelectedDatesChanged += Calendar_SelectedDatesChanged;
        }

        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if(calendar.SelectedDates.Count == 1)
            {
                if (taskBox != null)
                {
                    TaskBox_update_at(calendar.SelectedDates.ElementAt(0));
                    Calendar_Select_From_Google();
                }
            }
        }

        private void TaskBox_update_at(DateTime dt)
        {
            taskBox.Items.Clear();
            foreach (string s in GoogleCalendarManager.GetTasksOfDay(dt))
            {
                taskBox.Items.Add(s);
            }
        }
        
    }
}
