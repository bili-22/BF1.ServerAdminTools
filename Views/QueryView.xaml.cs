using BF1.ServerAdminTools.Models;
using BF1.ServerAdminTools.Common.Utils;
using BF1.ServerAdminTools.Features.Utils;
using BF1.ServerAdminTools.Features.API2;
using BF1.ServerAdminTools.Features.API2.RespJson;
using Microsoft.Toolkit.Mvvm.Input;

namespace BF1.ServerAdminTools.Views
{
    /// <summary>
    /// QueryView.xaml 的交互逻辑
    /// </summary>
    public partial class QueryView : UserControl
    {
        public QueryModel QueryModel { get; set; }
        public ObservableCollection<string> PlayerDatas { get; set; }
        public ObservableCollection<All.WeaponsItem> WeaponsItems { get; set; }
        public ObservableCollection<All.VehiclesItem> VehiclesItems { get; set; }

        public RelayCommand QueryPlayerCommand { get; set; }

        ////////////////////////////////////////////
        
        public delegate void QuickQueryPalyerDelegate(string playerName);
        public static QuickQueryPalyerDelegate queryPalyerDelegate;

        public QueryView()
        {
            InitializeComponent();

            this.DataContext = this;

            QueryModel = new QueryModel();

            PlayerDatas = new ObservableCollection<string>();
            WeaponsItems = new ObservableCollection<All.WeaponsItem>();
            VehiclesItems = new ObservableCollection<All.VehiclesItem>();

            QueryPlayerCommand = new RelayCommand(QueryPlayer);

            QueryModel.LoadingVisibility = Visibility.Collapsed;

            QueryModel.PlayerName = "CrazyZhang666";

            queryPalyerDelegate = QuickQueryPlayerData;
        }

        private void QuickQueryPlayerData(string playerName)
        {
            AudioUtil.ClickSound();

            QueryModel.PlayerName = playerName;
            QueryPlayer();
        }

        private async void QueryPlayer()
        {
            AudioUtil.ClickSound();

            if (!string.IsNullOrEmpty(QueryModel.PlayerName))
            {
                PlayerDatas.Clear();
                WeaponsItems.Clear();
                VehiclesItems.Clear();
                QueryModel.LoadingVisibility = Visibility.Visible;

                QueryModel.PlayerName = QueryModel.PlayerName.Trim();

                MainWindow.dSetOperatingState(2, $"正在查询玩家 {QueryModel.PlayerName} 数据中...");

                var result = await GTAPI.GetPlayerAllData(QueryModel.PlayerName);

                if (result.IsSuccess)
                {
                    var all = JsonUtil.JsonDese<All>(result.Message);

                    PlayerDatas.Add($"玩家ID : {all.userName}");
                    PlayerDatas.Add($"数字ID : {all.id}");
                    PlayerDatas.Add($"等级 : {all.rank}");
                    PlayerDatas.Add($"游戏时间 : {PlayerUtil.GetPlayTime(all.timePlayed)} 小时");

                    PlayerDatas.Add("");

                    PlayerDatas.Add($"KD : {all.killDeath}");
                    PlayerDatas.Add($"KPM : {all.killsPerMinute}");
                    PlayerDatas.Add($"SPM : {all.scorePerMinute}");

                    PlayerDatas.Add($"命中率 : {all.accuracy}");
                    PlayerDatas.Add($"爆头数 : {all.headshots}");
                    PlayerDatas.Add($"爆头率 : {all.headShots}");

                    PlayerDatas.Add($"最高连续击杀数 : {all.highestKillStreak}");
                    PlayerDatas.Add($"最远爆头距离 : {all.longestHeadShot}");

                    PlayerDatas.Add("");

                    PlayerDatas.Add($"击杀 : {all.kills}");
                    PlayerDatas.Add($"死亡 : {all.deaths}");
                    PlayerDatas.Add($"协助击杀数 : {all.killAssists}");

                    PlayerDatas.Add($"胜率 : {all.winPercent}");
                    PlayerDatas.Add($"技巧值 : {all.skill}");

                    PlayerDatas.Add($"步兵KD : {all.infantryKillDeath}");
                    PlayerDatas.Add($"步兵KPM : {all.infantryKillsPerMinute}");
                    PlayerDatas.Add($"最佳兵种 : {all.bestClass}");

                    PlayerDatas.Add("");

                    PlayerDatas.Add($"仇敌击杀数 : {all.avengerKills}");
                    PlayerDatas.Add($"救星击杀数 : {all.saviorKills}");
                    PlayerDatas.Add($"急救数 : {all.revives}");
                    PlayerDatas.Add($"治疗分 : {all.heals}");
                    PlayerDatas.Add($"修理分 : {all.repairs}");

                    PlayerDatas.Add($"取得狗牌数 : {all.dogtagsTaken}");
                    PlayerDatas.Add($"胜利场数 : {all.wins}");
                    PlayerDatas.Add($"战败场数 : {all.loses}");
                    PlayerDatas.Add($"游戏总局数 : {all.roundsPlayed}");

                    PlayerDatas.Add("");

                    PlayerDatas.Add($"小隊分数 : {all.squadScore}");
                    PlayerDatas.Add($"奖励分数 : {all.awardScore}");
                    PlayerDatas.Add($"加成分数 : {all.bonusScore}");

                    PlayerDatas.Add($"当前等级进度 : {all.currentRankProgress}");
                    PlayerDatas.Add($"总计等级进度 : {all.totalRankProgress}");

                    all.weapons.Sort((a, b) => b.kills.CompareTo(a.kills));
                    all.vehicles.Sort((a, b) => b.kills.CompareTo(a.kills));

                    foreach (var item in all.weapons)
                    {
                        if (item.kills != 0)
                        {
                            item.type = PlayerUtil.GetKillStar(item.kills);
                            item.image = PlayerUtil.GetTempImagePath(item.image, "weapons2");
                            WeaponsItems.Add(item);
                        }
                    }

                    foreach (var item in all.vehicles)
                    {
                        if (item.kills != 0)
                        {
                            item.type = PlayerUtil.GetKillStar(item.kills);
                            item.image = PlayerUtil.GetTempImagePath(item.image, "vehicles2");
                            VehiclesItems.Add(item);
                        }
                    }

                    MainWindow.dSetOperatingState(1, $"玩家 {QueryModel.PlayerName} 数据查询成功  |  耗时: {result.ExecTime:0.00} 秒");
                }
                else
                {
                    MainWindow.dSetOperatingState(3, $"玩家 {QueryModel.PlayerName} 数据查询失败  |  耗时: {result.ExecTime:0.00} 秒");
                }

                QueryModel.LoadingVisibility = Visibility.Collapsed;
            }
            else
            {
                MainWindow.dSetOperatingState(2, $"请输入正确的玩家名称");
            }
        }
    }
}
