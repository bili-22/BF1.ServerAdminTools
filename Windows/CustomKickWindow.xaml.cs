using BF1.ServerAdminTools.Features.API;

namespace BF1.ServerAdminTools.Windows
{
    /// <summary>
    /// CustomKickWindow.xaml 的交互逻辑
    /// </summary>
    public partial class CustomKickWindow : Window
    {
        public string PlayerName { get; set; }
        public string PersonaId { get; set; }

        public CustomKickWindow(string playerName, string personaId)
        {
            InitializeComponent();

            this.DataContext = this;

            PlayerName = playerName;
            PersonaId = personaId;
        }

        private async void Button_KickPlayer_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.dSetOperatingState(2, $"正在踢出玩家 {PlayerName} 中...");

            var reason = TextBox_CustomReason.Text.Trim();

            if (reason == "@Kick")
            {
                reason = "ADMINPRIORITY";
            }

            var result = await BF1API.AdminKickPlayer(PersonaId, reason);

            if (result.IsSuccess)
            {
                MainWindow.dSetOperatingState(1, $"踢出玩家 {PlayerName} 成功  |  耗时: {result.ExecTime:0.00} 秒");
                this.Close();
            }
            else
            {
                MainWindow.dSetOperatingState(3, $"踢出玩家 {PlayerName} 失败 {result.Message}  |  耗时: {result.ExecTime:0.00} 秒");
            }
        }
    }
}
