using BF1.ServerAdminTools.Common.Data;
using BF1.ServerAdminTools.Common;
using BF1.ServerAdminTools.Common.Data;
using BF1.ServerAdminTools.Common.Helper;

namespace BF1.ServerAdminTools.Wpf.Views
{
    /// <summary>
    /// LogView.xaml 的交互逻辑
    /// </summary>
    public partial class LogView : UserControl
    {
        public static Action<BreakRuleInfo> _dAddKickOKLog;
        public static Action<BreakRuleInfo> _dAddKickNOLog;

        public static Action<ChangeTeamInfo> _dAddChangeTeamInfo;
        public static Semaphore Semaphore = new(0, 5);

        private Dictionary<long, PlayerData> Player_Team1 = new();
        private Dictionary<long, PlayerData> Player_Team2 = new();

        public LogView()
        {
            InitializeComponent();

            _dAddKickOKLog = AddKickOKLog;
            _dAddKickNOLog = AddKickNOLog;

            _dAddChangeTeamInfo = AddChangeTeamLog;

            MainWindow.ClosingDisposeEvent += MainWindow_ClosingDisposeEvent;

            new Thread(CheckPlayerChangeTeam)
            {
                Name = "CheckPlayerChangeTeamThread",
                IsBackground = true
            }.Start();
        }

        private void MainWindow_ClosingDisposeEvent()
        {

        }

        private void CheckPlayerChangeTeam()
        {
            var New_Player_Team1 = new Dictionary<long, PlayerData>();
            var New_Player_Team2 = new Dictionary<long, PlayerData>();

            while (true)
            {
                Semaphore.WaitOne();
                if (string.IsNullOrEmpty(Globals.Config.GameId))
                    continue;

                if (ScoreView.PlayerDatas_Team1?.Count == 0 && ScoreView.PlayerDatas_Team2?.Count == 0)
                {
                    New_Player_Team1.Clear();
                    New_Player_Team2.Clear();
                    Player_Team1.Clear();
                    Player_Team2.Clear();
                    continue;
                }

                // 第一次初始化
                if (Player_Team1.Count == 0 && Player_Team2.Count == 0)
                {
                    foreach (var item in ScoreView.PlayerDatas_Team1)
                    {
                        Player_Team1.Add(item.Key, item.Value);
                    }
                    foreach (var item in ScoreView.PlayerDatas_Team2)
                    {
                        Player_Team2.Add(item.Key, item.Value);
                    }
                    continue;
                }

                New_Player_Team1.Clear();
                New_Player_Team2.Clear();
                // 更新保存的数据
                foreach (var item in ScoreView.PlayerDatas_Team1)
                {
                    New_Player_Team1.Add(item.Key, item.Value);
                }
                foreach (var item in ScoreView.PlayerDatas_Team2)
                {
                    New_Player_Team2.Add(item.Key, item.Value);
                }

                // 变量保存的队伍1玩家列表
                foreach (var item in New_Player_Team1)
                {
                    if (Player_Team2.ContainsKey(item.Key))
                    {
                        _dAddChangeTeamInfo(new ChangeTeamInfo()
                        {
                            Rank = item.Value.Rank,
                            Name = item.Value.Name,
                            PersonaId = item.Value.PersonaId,
                            Status = "从 队伍1 更换到 队伍2"
                        });
                    }
                }

                // 变量保存的队伍2玩家列表
                foreach (var item in New_Player_Team2)
                {
                    if (Player_Team1.ContainsKey(item.Key))
                    {
                        _dAddChangeTeamInfo(new ChangeTeamInfo()
                        {
                            Rank = item.Value.Rank,
                            Name = item.Value.Name,
                            PersonaId = item.Value.PersonaId,
                            Status = "从 队伍1 更换到 队伍2"
                        });
                    }
                }

                Player_Team1.Clear();
                Player_Team2.Clear();
                // 更新保存的数据
                foreach (var item in New_Player_Team1)
                {
                    Player_Team1.Add(item.Key, item.Value);
                }
                foreach (var item in New_Player_Team2)
                {
                    Player_Team2.Add(item.Key, item.Value);
                }
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

                SQLiteHelper.AddLog2SQLite(DataShell.KICKOK, info);
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

                SQLiteHelper.AddLog2SQLite(DataShell.KICKFAIL, info);
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
