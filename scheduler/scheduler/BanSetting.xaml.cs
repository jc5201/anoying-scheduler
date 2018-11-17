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
using System.Windows.Shapes;
using System.IO;

namespace scheduler
{
    /// <summary>
    /// BanSetting.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class BanSetting : Window
    {
		private BanListManager BM = new BanListManager();
		public BanSetting()
        {
            InitializeComponent();
			procList.ItemsSource = ProcessManager.GetProcList();
			banList.ItemsSource = BanListManager.GetBanList();
        }

		private void banProc_Click(object sender, RoutedEventArgs e)
		{
			BM.Add((string)procList.SelectedItem);
			banList.ItemsSource = BanListManager.GetBanList();
		}

		private void unbanProc_Click(object sender, RoutedEventArgs e)
		{
			BM.Rm((string)banList.SelectedItem.ToString());
			banList.ItemsSource = BanListManager.GetBanList();
		}

		private void Refresh_Click(object sender, RoutedEventArgs e)
		{
			procList.ItemsSource = ProcessManager.GetProcList();
		}
	}
}
