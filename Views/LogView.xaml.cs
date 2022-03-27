using BF1.ServerAdminTools.Common.Data;
using BF1.ServerAdminTools.Common.Helper;
using BF1.ServerAdminTools.Features.Data;

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

            var thread0 = new Thread(CheckPlayerChangeTeam);
            thread0.IsBackground = true;
            thread0.Start();
        }

        private void MainWindow_ClosingDisposeEvent()
        {

        }

        private void CheckPlayerChangeTeam()
        {
            var Player_Team1 = new List<PlayerData>();
            var Player_Team2 = new List<PlayerData>();

            while (true)
            {
                if (string.IsNullOrEmpty(Globals.GameId))
                    continue;

                if (ScoreView.PlayerDatas_Team1.Count == 0 && ScoreView.PlayerDatas_Team2.Count == 0)
                {
                    continue;
                }

                var temp_Player_Team1 = JsonSerializer.Deserialize<List<PlayerData>>(JsonSerializer.Serialize(ScoreView.PlayerDatas_Team1));
                var temp_Player_Team2 = JsonSerializer.Deserialize<List<PlayerData>>(JsonSerializer.Serialize(ScoreView.PlayerDatas_Team2));

                if (temp_Player_Team1.Count == 0 && temp_Player_Team2.Count == 0)
                    continue;

                // 第一次初始化
                if (Player_Team1.Count == 0 && Player_Team2.Count == 0)
                {
                    Player_Team1 = temp_Player_Team1;
                    Player_Team2 = temp_Player_Team2;
                    continue;
                }

                // 变量保存的队伍1玩家列表
                foreach (var item in Player_Team1)
                {
                    // 查询这个玩家是否在目前的队伍2中
                    int index = temp_Player_Team2.FindIndex(var => var.PersonaId == item.PersonaId);
                    if (index != -1)
                    {
                        _dAddChangeTeamInfo(new ChangeTeamInfo()
                        {
                            Rank = item.Rank,
                            Name = item.Name,
                            PersonaId = item.PersonaId,
                            Status = "从 队伍1 更换到 队伍2",
                            Time = DateTime.Now
                        });
                        break;
                    }
                }

                // 变量保存的队伍2玩家列表
                foreach (var item in Player_Team2)
                {
                    // 查询这个玩家是否在目前的队伍1中
                    int index = temp_Player_Team1.FindIndex(var => var.PersonaId == item.PersonaId);
                    if (index != -1)
                    {
                        _dAddChangeTeamInfo(new ChangeTeamInfo()
                        {
                            Rank = item.Rank,
                            Name = item.Name,
                            PersonaId = item.PersonaId,
                            Status = "从 队伍2 更换到 队伍1",
                            Time = DateTime.Now
                        });
                        break;
                    }
                }

                // 更新保存的数据
                Player_Team1 = temp_Player_Team1;
                Player_Team2 = temp_Player_Team2;

                Thread.Sleep(1000);
            }
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
            Application.Current.Dispatcher.BeginInvoke(() =>
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
            Application.Current.Dispatcher.BeginInvoke(() =>
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
            Application.Current.Dispatcher.BeginInvoke(() =>
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
