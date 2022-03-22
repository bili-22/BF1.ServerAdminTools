using BF1.ServerAdminTools.Common.Utils;
using BF1.ServerAdminTools.Common.Helper;

namespace BF1.ServerAdminTools.Views
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
    }
}
