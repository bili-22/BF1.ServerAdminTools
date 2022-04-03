using BF1.ServerAdminTools.Common.Data;
using BF1.ServerAdminTools.Common.Helper;

namespace BF1.ServerAdminTools.Views
{
    /// <summary>
    /// LogView.xaml 的交互逻辑
    /// </summary>
    public partial class LogView : UserControl
    {
        public static Action<BreakRuleInfo> _dAddKickOKLog;
        public static Action<BreakRuleInfo> _dAddKickNOLog;

        public static Action<ChangeTeamInfo> _dAddChangeTeamInfo;

        public LogView()
        {
            InitializeComponent();

            _dAddKickOKLog = AddKickOKLog;
            _dAddKickNOLog = AddKickNOLog;

            _dAddChangeTeamInfo = AddChangeTeamLog;

            MainWindow.ClosingDisposeEvent += MainWindow_ClosingDisposeEvent;
        }

        private void MainWindow_ClosingDisposeEvent()
        {

        }

        /////////////////////////////////////////////////////

        private void AppendKickOKLog(string msg)
        {
            TextBox_KickOKLog.AppendText(msg + "\n");
        }

        private void AppendKickNOLog(string msg)
        {
            TextBox_KickNOLog.AppendText(msg + "\n");
        }

        private void AppendChangeTeamLog(string msg)
        {
            TextBox_ChangeTeamLog.AppendText(msg + "\n");
        }

        /////////////////////////////////////////////////////

        private void AddKickOKLog(BreakRuleInfo info)
        {
            this.Dispatcher.Invoke(() =>
            {
                if (TextBox_KickOKLog.LineCount >= 1000)
                {
                    TextBox_KickOKLog.Clear();
                }

                AppendKickOKLog("操作时间: " + DateTime.Now.ToString());
                AppendKickOKLog("玩家ID: " + info.Name);
                AppendKickOKLog("玩家数字ID: " + info.PersonaId);
                AppendKickOKLog("踢出理由: " + info.Reason);
                AppendKickOKLog("状态: " + info.Status + "\n");

                SQLiteHelper.AddLog2SQLite("kick_ok", info);
            });
        }

        private void AddKickNOLog(BreakRuleInfo info)
        {
            this.Dispatcher.Invoke(() =>
            {
                if (TextBox_KickNOLog.LineCount >= 1000)
                {
                    TextBox_KickNOLog.Clear();
                }

                AppendKickNOLog("操作时间: " + DateTime.Now.ToString());
                AppendKickNOLog("玩家ID: " + info.Name);
                AppendKickNOLog("玩家数字ID: " + info.PersonaId);
                AppendKickNOLog("踢出理由: " + info.Reason);
                AppendKickNOLog("状态: " + info.Status + "\n");

                SQLiteHelper.AddLog2SQLite("kick_no", info);
            });
        }

        private void AddChangeTeamLog(ChangeTeamInfo info)
        {
            this.Dispatcher.Invoke(() =>
            {
                if (TextBox_ChangeTeamLog.LineCount >= 1000)
                {
                    TextBox_ChangeTeamLog.Clear();
                }

                AppendChangeTeamLog("操作时间: " + DateTime.Now.ToString());
                AppendChangeTeamLog("玩家等级: " + info.Rank);
                AppendChangeTeamLog("玩家ID: " + info.Name);
                AppendChangeTeamLog("玩家数字ID: " + info.PersonaId);
                AppendChangeTeamLog("状态: " + info.Status + "\n");

                SQLiteHelper.AddLog2SQLite(info);
            });
        }
    }
}
