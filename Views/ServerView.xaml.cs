using BF1.ServerAdminTools.Models;
using BF1.ServerAdminTools.Common.Utils;
using BF1.ServerAdminTools.Features.Utils;
using BF1.ServerAdminTools.Features.API2;
using BF1.ServerAdminTools.Features.API2.RespJson;
using Microsoft.Toolkit.Mvvm.Input;

namespace BF1.ServerAdminTools.Views
{
    /// <summary>
    /// ServerView.xaml 的交互逻辑
    /// </summary>
    public partial class ServerView : UserControl
    {
        public ServerModel ServerModel { get; set; }
        public ObservableCollection<Servers.ServersItem> ServersItems { get; set; }

        public RelayCommand QueryServerCommand { get; private set; }
        public RelayCommand<string> ServerInfoCommand { get; private set; }

        public ServerView()
        {
            InitializeComponent();

            this.DataContext = this;

            ServerModel = new ServerModel();
            ServersItems = new ObservableCollection<Servers.ServersItem>();

            QueryServerCommand = new RelayCommand(QueryServer);
            ServerInfoCommand = new RelayCommand<string>(ServerInfo);

            ServerModel.LoadingVisibility = Visibility.Collapsed;

            ServerModel.ServerName = "DICE";
        }

        private async void QueryServer()
        {
            AudioUtil.ClickSound();

            if (!string.IsNullOrEmpty(ServerModel.ServerName))
            {
                ServersItems.Clear();
                ServerModel.LoadingVisibility = Visibility.Visible;

                ServerModel.ServerName = ServerModel.ServerName.Trim();

                MainWindow.dSetOperatingState(2, $"正在查询服务器 {ServerModel.ServerName} 数据中...");

                var result = await GTAPI.GetServersData(ServerModel.ServerName);

                if (result.IsSuccess)
                {
                    var servers = JsonUtil.JsonDese<Servers>(result.Message);

                    foreach (var item in servers.servers)
                    {
                        item.url = PlayerUtil.GetTempImagePath(item.url, "maps");
                        item.platform = new Random().Next(25, 45).ToString();
                        ServersItems.Add(item);
                    }

                    MainWindow.dSetOperatingState(1, $"服务器 {ServerModel.ServerName} 数据查询成功  |  耗时: {result.ExecTime:0.00} 秒");
                }
                else
                {
                    MainWindow.dSetOperatingState(3, $"服务器 {ServerModel.ServerName} 数据查询失败  |  耗时: {result.ExecTime:0.00} 秒");
                }

                ServerModel.LoadingVisibility = Visibility.Collapsed;
            }
            else
            {
                MainWindow.dSetOperatingState(2, $"请输入正确的服务器名称");
            }
        }

        private void ServerInfo(string gameid)
        {

        }
    }
}
