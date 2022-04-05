using BF1.ServerAdminTools.Common.Helper;
using BF1.ServerAdminTools.Common.Utils;

namespace BF1.ServerAdminTools.Common.Views
{
    /// <summary>
    /// HomeView.xaml 的交互逻辑
    /// </summary>
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();

            Task.Run(async () =>
            {
                string notice = await HttpUtil.HttpClientGET(CoreUtil.Notice_Address);
                string change = await HttpUtil.HttpClientGET(CoreUtil.Change_Address);

                Dispatcher.Invoke(() =>
                {
                    TextBox_Notice.Text = notice;
                    TextBox_Change.Text = change;
                });
            });
        }

        private async void MenuItem_RefushNotice_Click(object sender, RoutedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                TextBox_Notice.Text = "加载中...";
            });

            string notice = await HttpUtil.HttpClientGET(CoreUtil.Notice_Address);

            Dispatcher.Invoke(() =>
            {
                TextBox_Notice.Text = notice;
            });
        }
    }
}
