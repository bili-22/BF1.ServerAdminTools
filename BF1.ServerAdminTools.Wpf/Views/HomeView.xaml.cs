using BF1.ServerAdminTools.Common.Utils;
using BF1.ServerAdminTools.Common.Helper;
using BF1.ServerAdminTools.Wpf.Utils;

namespace BF1.ServerAdminTools.Wpf.Views
{
    /// <summary>
    /// HomeView.xaml 的交互逻辑
    /// </summary>
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();

            Task.Run(() =>
            {
                string notice = HttpHelper.HttpClientGET(CoreUtil.Notice_Address).Result;
                string change = HttpHelper.HttpClientGET(CoreUtil.Change_Address).Result;

                Application.Current.Dispatcher.BeginInvoke(() =>
                {
                    TextBox_Notice.Text = notice;
                    TextBox_Change.Text = change;
                });
            });
        }

        private void MenuItem_RefushNotice_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(() =>
            {
                Application.Current.Dispatcher.BeginInvoke(() =>
                {
                    TextBox_Notice.Text = "加载中...";
                });

                string notice = HttpHelper.HttpClientGET(CoreUtil.Notice_Address).Result;

                Application.Current.Dispatcher.BeginInvoke(() =>
                {
                    TextBox_Notice.Text = notice;
                });
            });
        }
    }
}
