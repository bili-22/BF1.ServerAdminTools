using BF1.ServerAdminTools.Wpf.Models;
using BF1.ServerAdminTools.Wpf.Windows;
using BF1.ServerAdminTools.Wpf.Extension;
using BF1.ServerAdminTools.Common.Data;
using BF1.ServerAdminTools.Common.Utils;
using BF1.ServerAdminTools.BF1API.API;
using BF1.ServerAdminTools.BF1API.Core;
using BF1.ServerAdminTools.BF1API.Data;
using BF1.ServerAdminTools.BF1API.Utils;
using BF1.ServerAdminTools.Wpf.Utils;

namespace BF1.ServerAdminTools.Wpf.Views
{
    /// <summary>
    /// ScoreView.xaml 的交互逻辑
    /// </summary>
    public partial class ScoreView : UserControl
    {
        public ServerInfoModel ServerInfoModel { get; set; }
        public PlayerOtherModel PlayerOtherModel { get; set; }

        private List<PlayerData> PlayerList_All = new List<PlayerData>();
        private List<PlayerData> PlayerList_Team0 = new List<PlayerData>();
        private List<PlayerData> PlayerList_Team1 = new List<PlayerData>();
        private List<PlayerData> PlayerList_Team2 = new List<PlayerData>();

        public static List<PlayerData> PlayerDatas_Team1 = new List<PlayerData>();
        public static List<PlayerData> PlayerDatas_Team2 = new List<PlayerData>();

        // 正在执行踢人请求的玩家列表，保留指定时间秒数
        private List<BreakRuleInfo> Kicking_PlayerList = new List<BreakRuleInfo>();

        public ObservableCollection<PlayerListModel> DataGrid_PlayerList_Team1 { get; set; }
        public ObservableCollection<PlayerListModel> DataGrid_PlayerList_Team2 { get; set; }

        private const int MaxPlayer = 74;

        private TempData.ClientPlayer _tdCP;
        private TempData.ClientSoldierEntity _tdCSE;

        private struct ClientPlayer
        {
            public long BaseAddress;

            public int TeamID;
            public byte Spectator;

            public long PersonaId;
            public string PlayerName;
        }
        private ClientPlayer _localPlayer;

        private struct StatisticData
        {
            public int MaxPlayerCount;
            public int PlayerCount;
            public int Rank150PlayerCount;

            public int AllKillCount;
            public int AllDeadCount;
        }
        private StatisticData _statisticData_Team1;
        private StatisticData _statisticData_Team2;

        private struct ServerInfo
        {
            public long Offset0;

            public string ServerName;
            public long ServerID;
            public float ServerTime;

            public int Team1Score;
            public int Team2Score;

            public int Team1FromeKill;
            public int Team2FromeKill;

            public int Team1FromeFlag;
            public int Team2FromeFlag;
        }
        private ServerInfo _serverInfo;

        private struct DataGridSelcContent
        {
            public bool IsOK;
            public int TeamID;
            public int Rank;
            public string Name;
            public long PersonaId;
        }
        private DataGridSelcContent _dataGridSelcContent;

        ///////////////////////////////////////////////////////

        public ScoreView()
        {
            InitializeComponent();

            this.DataContext = this;

            ServerInfoModel = new ServerInfoModel();
            PlayerOtherModel = new PlayerOtherModel();

            PlayerList_All = new List<PlayerData>();
            PlayerList_Team0 = new List<PlayerData>();
            PlayerList_Team1 = new List<PlayerData>();
            PlayerList_Team2 = new List<PlayerData>();

            DataGrid_PlayerList_Team1 = new ObservableCollection<PlayerListModel>();
            DataGrid_PlayerList_Team2 = new ObservableCollection<PlayerListModel>();

            _tdCP.WeaponSlot = new string[8] { "", "", "", "", "", "", "", "" };

            var thread0 = new Thread(UpdatePlayerList);
            thread0.IsBackground = true;
            thread0.Start();
        }

        private void UpdatePlayerList()
        {
            while (true)
            {
                //////////////////////////////// 数据初始化 ////////////////////////////////

                PlayerList_All.Clear();
                PlayerList_Team0.Clear();
                PlayerList_Team1.Clear();
                PlayerList_Team2.Clear();

                Globals.Server_SpectatorList.Clear();

                Array.Clear(_tdCP.WeaponSlot, 0, _tdCP.WeaponSlot.Length);

                _statisticData_Team1.MaxPlayerCount = 0;
                _statisticData_Team1.PlayerCount = 0;
                _statisticData_Team1.Rank150PlayerCount = 0;
                _statisticData_Team1.AllKillCount = 0;
                _statisticData_Team1.AllDeadCount = 0;

                _statisticData_Team2.MaxPlayerCount = 0;
                _statisticData_Team2.PlayerCount = 0;
                _statisticData_Team2.Rank150PlayerCount = 0;
                _statisticData_Team2.AllKillCount = 0;
                _statisticData_Team2.AllDeadCount = 0;

                Globals.BreakRuleInfo_PlayerList.Clear();

                //////////////////////////////// 自己数据 ////////////////////////////////

                _localPlayer.BaseAddress = Player.GetLocalPlayer();

                _localPlayer.TeamID = Memory.Read<int>(_localPlayer.BaseAddress + 0x1C34);
                PlayerOtherModel.MySelfTeamID = $"队伍ID : {_localPlayer.TeamID}";

                _localPlayer.Spectator = Memory.Read<byte>(_localPlayer.BaseAddress + 0x1C31);
                _localPlayer.PersonaId = Memory.Read<long>(_localPlayer.BaseAddress + 0x38);
                _localPlayer.PlayerName = Memory.ReadString(_localPlayer.BaseAddress + 0x2156, 64);
                if (_localPlayer.PlayerName != "")
                {
                    PlayerOtherModel.MySelfName = $"玩家ID : {_localPlayer.PlayerName}";
                }
                else
                {
                    PlayerOtherModel.MySelfName = "玩家ID : 未知";
                }

                //////////////////////////////// 玩家数据 ////////////////////////////////

                for (int i = 0; i < MaxPlayer; i++)
                {
                    _tdCP.BaseAddress = Player.GetPlayerById(i);
                    if (!Memory.IsValid(_tdCP.BaseAddress))
                        continue;

                    _tdCP.Mark = Memory.Read<byte>(_tdCP.BaseAddress + 0x1D7C);
                    _tdCP.TeamID = Memory.Read<int>(_tdCP.BaseAddress + 0x1C34);
                    _tdCP.Spectator = Memory.Read<byte>(_tdCP.BaseAddress + 0x1C31);
                    _tdCP.PersonaId = Memory.Read<long>(_tdCP.BaseAddress + 0x38);
                    _tdCP.PartyId = Memory.Read<int>(_tdCP.BaseAddress + 0x1E50);
                    _tdCP.Name = Memory.ReadString(_tdCP.BaseAddress + 0x2156, 64);
                    if (string.IsNullOrEmpty(_tdCP.Name))
                        continue;

                    _tdCSE.pClientVehicleEntity = Memory.Read<long>(_tdCP.BaseAddress + 0x1D38);
                    if (Memory.IsValid(_tdCSE.pClientVehicleEntity))
                    {
                        _tdCSE.pVehicleEntityData = Memory.Read<long>(_tdCSE.pClientVehicleEntity + 0x30);
                        _tdCP.WeaponSlot[0] = Memory.ReadString(Memory.Read<long>(_tdCSE.pVehicleEntityData + 0x2F8), 64);

                        for (int j = 1; j < 8; j++)
                        {
                            _tdCP.WeaponSlot[j] = "";
                        }
                    }
                    else
                    {
                        _tdCSE.pClientSoldierEntity = Memory.Read<long>(_tdCP.BaseAddress + 0x1D48);
                        _tdCSE.pClientSoldierWeaponComponent = Memory.Read<long>(_tdCSE.pClientSoldierEntity + 0x698);
                        _tdCSE.m_handler = Memory.Read<long>(_tdCSE.pClientSoldierWeaponComponent + 0x8A8);

                        for (int j = 0; j < 8; j++)
                        {
                            var offset0 = Memory.Read<long>(_tdCSE.m_handler + j * 0x8);

                            offset0 = Memory.Read<long>(offset0 + 0x4A30);
                            offset0 = Memory.Read<long>(offset0 + 0x20);
                            offset0 = Memory.Read<long>(offset0 + 0x38);
                            offset0 = Memory.Read<long>(offset0 + 0x20);

                            _tdCP.WeaponSlot[j] = Memory.ReadString(offset0, 64);
                        }
                    }

                    int index = PlayerList_All.FindIndex(val => val.Name == _tdCP.Name);
                    if (index == -1)
                    {
                        PlayerList_All.Add(new PlayerData()
                        {
                            Mark = _tdCP.Mark,
                            TeamID = _tdCP.TeamID,
                            Spectator = _tdCP.Spectator,
                            Clan = PlayerUtil.GetPlayerTargetName(_tdCP.Name, true),
                            Name = PlayerUtil.GetPlayerTargetName(_tdCP.Name, false),
                            PersonaId = _tdCP.PersonaId,
                            SquadId = PlayerUtil.GetSquadChsName(_tdCP.PartyId),

                            Rank = 0,
                            Kill = 0,
                            Dead = 0,
                            Score = 0,

                            KD = 0,
                            KPM = 0,

                            WeaponS0 = _tdCP.WeaponSlot[0],
                            WeaponS1 = _tdCP.WeaponSlot[1],
                            WeaponS2 = _tdCP.WeaponSlot[2],
                            WeaponS3 = _tdCP.WeaponSlot[3],
                            WeaponS4 = _tdCP.WeaponSlot[4],
                            WeaponS5 = _tdCP.WeaponSlot[5],
                            WeaponS6 = _tdCP.WeaponSlot[6],
                            WeaponS7 = _tdCP.WeaponSlot[7],
                        });
                    }
                }

                //////////////////////////////// 得分板数据 ////////////////////////////////

                var pClientScoreBA = Memory.Read<long>(Memory.GetBaseAddress() + 0x39EB8D8);
                pClientScoreBA = Memory.Read<long>(pClientScoreBA + 0x68);

                for (int i = 0; i < MaxPlayer; i++)
                {
                    pClientScoreBA = Memory.Read<long>(pClientScoreBA);
                    var pClientScoreOffset = Memory.Read<long>(pClientScoreBA + 0x10);
                    if (!Memory.IsValid(pClientScoreBA))
                        continue;

                    var Mark = Memory.Read<byte>(pClientScoreOffset + 0x300);
                    var Rank = Memory.Read<int>(pClientScoreOffset + 0x304);
                    if (Rank == 0)
                        continue;
                    var Kill = Memory.Read<int>(pClientScoreOffset + 0x308);
                    var Dead = Memory.Read<int>(pClientScoreOffset + 0x30C);
                    var Score = Memory.Read<int>(pClientScoreOffset + 0x314);

                    int index = PlayerList_All.FindIndex(val => val.Mark == Mark);
                    if (index != -1)
                    {
                        PlayerList_All[index].Rank = Rank;
                        PlayerList_All[index].Kill = Kill;
                        PlayerList_All[index].Dead = Dead;
                        PlayerList_All[index].Score = Score;
                        PlayerList_All[index].KD = PlayerUtil.GetPlayerKD(Kill, Dead);
                        PlayerList_All[index].KPM = PlayerUtil.GetPlayerKPM(Kill, PlayerUtil.SecondsToMM(_serverInfo.ServerTime));
                    }
                }

                //////////////////////////////// 队伍数据整理 ////////////////////////////////

                foreach (var item in PlayerList_All)
                {
                    item.Admin = PlayerUtil.CheckAdminVIP(item.PersonaId.ToString(), Globals.Server_AdminList);
                    item.VIP = PlayerUtil.CheckAdminVIP(item.PersonaId.ToString(), Globals.Server_VIPList);

                    if (item.TeamID == 0)
                    {
                        PlayerList_Team0.Add(item);
                    }
                    if (item.TeamID == 1)
                    {
                        PlayerList_Team1.Add(item);
                    }
                    else if (item.TeamID == 2)
                    {
                        PlayerList_Team2.Add(item);
                    }

                    // 检查违规玩家
                    if (item.TeamID == 1 || item.TeamID == 2)
                    {
                        CheckPlayerIsBreakRule(item);
                    }
                }

                // 观战玩家信息
                foreach (var item in PlayerList_Team0)
                {
                    Globals.Server_SpectatorList.Add(new SpectatorInfo()
                    {
                        Name = item.Name,
                        PersonaId = item.PersonaId.ToString(),
                    });
                }

                // 队伍1数据统计
                foreach (var item in PlayerList_Team1)
                {
                    // 统计当前服务器玩家数量
                    if (item.Rank != 0)
                    {
                        _statisticData_Team1.MaxPlayerCount++;
                    }

                    // 统计当前服务器存活玩家数量
                    if (item.WeaponS0 != "")
                    {
                        _statisticData_Team1.PlayerCount++;
                    }

                    // 统计当前服务器150级玩家数量
                    if (item.Rank == 150)
                    {
                        _statisticData_Team1.Rank150PlayerCount++;
                    }

                    // 总击杀总死亡数统计
                    _statisticData_Team1.AllKillCount += item.Kill;
                    _statisticData_Team1.AllDeadCount += item.Dead;
                }

                // 队伍2数据统计
                foreach (var item in PlayerList_Team2)
                {
                    // 统计当前服务器玩家数量
                    if (item.Rank != 0)
                    {
                        _statisticData_Team2.MaxPlayerCount++;
                    }

                    // 统计当前服务器存活玩家数量
                    if (item.WeaponS0 != "" ||
                        item.WeaponS1 != "" ||
                        item.WeaponS2 != "" ||
                        item.WeaponS3 != "" ||
                        item.WeaponS4 != "" ||
                        item.WeaponS5 != "" ||
                        item.WeaponS6 != "" ||
                        item.WeaponS7 != "")
                    {
                        _statisticData_Team2.PlayerCount++;
                    }

                    // 统计当前服务器150级玩家数量
                    if (item.Rank == 150)
                    {
                        _statisticData_Team2.Rank150PlayerCount++;
                    }

                    _statisticData_Team2.AllKillCount += item.Kill;
                    _statisticData_Team2.AllDeadCount += item.Dead;
                }

                // 是否显示中文武器名称
                if (Globals.IsShowCHSWeaponName)
                {
                    for (int i = 0; i < PlayerList_Team1.Count; i++)
                    {
                        PlayerList_Team1[i].WeaponS0 = PlayerUtil.GetWeaponChsName(PlayerList_Team1[i].WeaponS0);
                        PlayerList_Team1[i].WeaponS1 = PlayerUtil.GetWeaponChsName(PlayerList_Team1[i].WeaponS1);
                        PlayerList_Team1[i].WeaponS2 = PlayerUtil.GetWeaponChsName(PlayerList_Team1[i].WeaponS2);
                        PlayerList_Team1[i].WeaponS3 = PlayerUtil.GetWeaponChsName(PlayerList_Team1[i].WeaponS3);
                        PlayerList_Team1[i].WeaponS4 = PlayerUtil.GetWeaponChsName(PlayerList_Team1[i].WeaponS4);
                        PlayerList_Team1[i].WeaponS5 = PlayerUtil.GetWeaponChsName(PlayerList_Team1[i].WeaponS5);
                        PlayerList_Team1[i].WeaponS6 = PlayerUtil.GetWeaponChsName(PlayerList_Team1[i].WeaponS6);
                        PlayerList_Team1[i].WeaponS7 = PlayerUtil.GetWeaponChsName(PlayerList_Team1[i].WeaponS7);
                    }

                    for (int i = 0; i < PlayerList_Team2.Count; i++)
                    {
                        PlayerList_Team2[i].WeaponS0 = PlayerUtil.GetWeaponChsName(PlayerList_Team2[i].WeaponS0);
                        PlayerList_Team2[i].WeaponS1 = PlayerUtil.GetWeaponChsName(PlayerList_Team2[i].WeaponS1);
                        PlayerList_Team2[i].WeaponS2 = PlayerUtil.GetWeaponChsName(PlayerList_Team2[i].WeaponS2);
                        PlayerList_Team2[i].WeaponS3 = PlayerUtil.GetWeaponChsName(PlayerList_Team2[i].WeaponS3);
                        PlayerList_Team2[i].WeaponS4 = PlayerUtil.GetWeaponChsName(PlayerList_Team2[i].WeaponS4);
                        PlayerList_Team2[i].WeaponS5 = PlayerUtil.GetWeaponChsName(PlayerList_Team2[i].WeaponS5);
                        PlayerList_Team2[i].WeaponS6 = PlayerUtil.GetWeaponChsName(PlayerList_Team2[i].WeaponS6);
                        PlayerList_Team2[i].WeaponS7 = PlayerUtil.GetWeaponChsName(PlayerList_Team2[i].WeaponS7);
                    }
                }

                ////////////////////////////////////////////////////////////////////////////////

                // 服务器名称
                _serverInfo.ServerName = Memory.ReadString(Memory.GetBaseAddress() + Offsets.ServerName_Offset, Offsets.ServerName, 64);
                _serverInfo.ServerName = _serverInfo.ServerName == "" ? "未知" : _serverInfo.ServerName;

                // 如果玩家没有进入服务器，要进行一些数据清理
                if (PlayerList_Team1.Count == 0 && PlayerList_Team2.Count == 0 && _serverInfo.ServerName == "未知")
                {
                    // 清理服务器ID（GameID）
                    _serverInfo.ServerID = 0;
                    Globals.GameId = string.Empty;

                    Globals.Server_AdminList.Clear();
                    Globals.Server_Admin2List.Clear();
                    Globals.Server_VIPList.Clear();
                }
                else
                {
                    // 服务器数字ID
                    _serverInfo.ServerID = Memory.Read<long>(Memory.GetBaseAddress() + Offsets.ServerID_Offset, Offsets.ServerID);
                    Globals.GameId = _serverInfo.ServerID.ToString();
                }

                // 服务器时间
                _serverInfo.ServerTime = Memory.Read<float>(Memory.GetBaseAddress() + Offsets.ServerTime_Offset, Offsets.ServerTime);

                _serverInfo.Offset0 = Memory.Read<long>(Memory.GetBaseAddress() + Offsets.ServerScore_Offset, Offsets.ServerScoreTeam);

                // 队伍1、队伍2分数
                _serverInfo.Team1Score = Memory.Read<int>(_serverInfo.Offset0 + 0x2B0);
                _serverInfo.Team2Score = Memory.Read<int>(_serverInfo.Offset0 + 0x2B0 + 0x08);

                // 队伍1、队伍2从击杀获取得分
                _serverInfo.Team1FromeKill = Memory.Read<int>(_serverInfo.Offset0 + 0x2B0 + 0x60);
                _serverInfo.Team2FromeKill = Memory.Read<int>(_serverInfo.Offset0 + 0x2B0 + 0x68);

                // 队伍1、队伍2从旗帜获取得分
                _serverInfo.Team1FromeFlag = Memory.Read<int>(_serverInfo.Offset0 + 0x2B0 + 0x100);
                _serverInfo.Team2FromeFlag = Memory.Read<int>(_serverInfo.Offset0 + 0x2B0 + 0x108);

                ////////////////////////////////////////////////////////////////////////////////

                ServerInfoModel.ServerName = $"服务器名称 : {_serverInfo.ServerName}  |  GameID : {_serverInfo.ServerID}";

                ServerInfoModel.ServerTime = PlayerUtil.SecondsToMMSS(_serverInfo.ServerTime);

                if (_serverInfo.Team1Score >= 0 && _serverInfo.Team1Score <= 1000 &&
                    _serverInfo.Team2Score >= 0 && _serverInfo.Team2Score <= 1000)
                {
                    ServerInfoModel.Team1ScoreWidth = _serverInfo.Team1Score / 6.25;
                    ServerInfoModel.Team2ScoreWidth = _serverInfo.Team2Score / 6.25;

                    ServerInfoModel.Team1Score = $"{_serverInfo.Team1Score}";
                    ServerInfoModel.Team2Score = $"{_serverInfo.Team2Score}";
                }
                else if (_serverInfo.Team1Score > 1000 && _serverInfo.Team1Score <= 2000 ||
                    _serverInfo.Team2Score > 1000 && _serverInfo.Team2Score <= 2000)
                {
                    ServerInfoModel.Team1ScoreWidth = _serverInfo.Team1Score / 12.5;
                    ServerInfoModel.Team2ScoreWidth = _serverInfo.Team2Score / 12.5;

                    ServerInfoModel.Team1Score = $"{_serverInfo.Team1Score}";
                    ServerInfoModel.Team2Score = $"{_serverInfo.Team2Score}";
                }
                else
                {
                    ServerInfoModel.Team1ScoreWidth = 0;
                    ServerInfoModel.Team2ScoreWidth = 0;

                    ServerInfoModel.Team1Score = "0";
                    ServerInfoModel.Team2Score = "0";
                }

                if (_serverInfo.Team1FromeFlag < 0 || _serverInfo.Team1FromeFlag > 2000)
                {
                    _serverInfo.Team1FromeFlag = 0;
                }

                if (_serverInfo.Team1FromeKill < 0 || _serverInfo.Team1FromeKill > 2000)
                {
                    _serverInfo.Team1FromeKill = 0;
                }

                if (_serverInfo.Team2FromeFlag < 0 || _serverInfo.Team2FromeFlag > 2000)
                {
                    _serverInfo.Team2FromeFlag = 0;
                }

                if (_serverInfo.Team2FromeKill < 0 || _serverInfo.Team2FromeKill > 2000)
                {
                    _serverInfo.Team2FromeKill = 0;
                }

                ServerInfoModel.Team1FromeFlag = $"从旗帜获取的得分 : {_serverInfo.Team1FromeFlag}";
                ServerInfoModel.Team1FromeKill = $"从击杀获取的得分 : {_serverInfo.Team1FromeKill}";

                ServerInfoModel.Team2FromeFlag = $"从旗帜获取的得分 : {_serverInfo.Team2FromeFlag}";
                ServerInfoModel.Team2FromeKill = $"从击杀获取的得分 : {_serverInfo.Team2FromeKill}";

                ServerInfoModel.Team1Info = $"已部署/队伍1人数 : {_statisticData_Team1.PlayerCount} / {_statisticData_Team1.MaxPlayerCount}  |  150等级人数 : {_statisticData_Team1.Rank150PlayerCount}  |  总击杀数 : {_statisticData_Team1.AllKillCount}  |  总死亡数 : {_statisticData_Team1.AllDeadCount}";
                ServerInfoModel.Team2Info = $"已部署/队伍2人数 : {_statisticData_Team2.PlayerCount} / {_statisticData_Team2.MaxPlayerCount}  |  150等级人数 : {_statisticData_Team2.Rank150PlayerCount}  |  总击杀数 : {_statisticData_Team2.AllKillCount}  |  总死亡数 : {_statisticData_Team2.AllDeadCount}";

                PlayerOtherModel.ServerPlayerCountInfo = $"服务器总人数 : {_statisticData_Team1.MaxPlayerCount + _statisticData_Team2.MaxPlayerCount}";

                ////////////////////////////////////////////////////////////////////////////////

                Application.Current?.Dispatcher.Invoke(() =>
                {
                    UpdateDataGridTeam1();
                    UpdateDataGridTeam2();

                    DataGrid_PlayerList_Team1.Sort();
                    DataGrid_PlayerList_Team2.Sort();
                });

                ////////////////////////////////////////////////////////////////////////////////

                // 自动踢出违规玩家
                AutoKickBreakPlayer();

                ////////////////////////////////////////////////////////////////////////////////

                // 暴露给外部使用
                PlayerDatas_Team1.Clear();
                foreach (var item in PlayerList_Team1)
                {
                    PlayerDatas_Team1.Add(item);
                }

                PlayerDatas_Team2.Clear();
                foreach (var item in PlayerList_Team2)
                {
                    PlayerDatas_Team2.Add(item);
                }

                ////////////////////////////////////////////////////////////////////////////////

                Thread.Sleep(1000);
            }
        }

        // 更新 DataGrid 队伍1
        private void UpdateDataGridTeam1()
        {
            if (PlayerList_Team1.Count == 0 && DataGrid_PlayerList_Team1.Count != 0)
            {
                DataGrid_PlayerList_Team1.Clear();
            }

            if (PlayerList_Team1.Count != 0)
            {
                // 更新DataGrid中现有的玩家数据，并把DataGrid中已经不在服务器的玩家清除
                for (int i = 0; i < DataGrid_PlayerList_Team1.Count; i++)
                {
                    int index = PlayerList_Team1.FindIndex(val => val.Name == DataGrid_PlayerList_Team1[i].Name);
                    if (index != -1)
                    {
                        DataGrid_PlayerList_Team1[i].Rank = PlayerList_Team1[index].Rank;
                        DataGrid_PlayerList_Team1[i].Clan = PlayerList_Team1[index].Clan;
                        DataGrid_PlayerList_Team1[i].Admin = PlayerList_Team1[index].Admin;
                        DataGrid_PlayerList_Team1[i].VIP = PlayerList_Team1[index].VIP;
                        DataGrid_PlayerList_Team1[i].SquadId = PlayerList_Team1[index].SquadId;
                        DataGrid_PlayerList_Team1[i].Kill = PlayerList_Team1[index].Kill;
                        DataGrid_PlayerList_Team1[i].Dead = PlayerList_Team1[index].Dead;
                        DataGrid_PlayerList_Team1[i].KD = PlayerList_Team1[index].KD.ToString("0.00");
                        DataGrid_PlayerList_Team1[i].KPM = PlayerList_Team1[index].KPM.ToString("0.00");
                        DataGrid_PlayerList_Team1[i].Score = PlayerList_Team1[index].Score;
                        DataGrid_PlayerList_Team1[i].WeaponS0 = PlayerList_Team1[index].WeaponS0;
                        DataGrid_PlayerList_Team1[i].WeaponS1 = PlayerList_Team1[index].WeaponS1;
                        DataGrid_PlayerList_Team1[i].WeaponS2 = PlayerList_Team1[index].WeaponS2;
                        DataGrid_PlayerList_Team1[i].WeaponS3 = PlayerList_Team1[index].WeaponS3;
                        DataGrid_PlayerList_Team1[i].WeaponS4 = PlayerList_Team1[index].WeaponS4;
                        DataGrid_PlayerList_Team1[i].WeaponS5 = PlayerList_Team1[index].WeaponS5;
                        DataGrid_PlayerList_Team1[i].WeaponS6 = PlayerList_Team1[index].WeaponS6;
                        DataGrid_PlayerList_Team1[i].WeaponS7 = PlayerList_Team1[index].WeaponS7;
                    }
                    else
                    {
                        DataGrid_PlayerList_Team1.RemoveAt(i);
                    }
                }

                // 增加DataGrid没有的玩家数据
                for (int i = 0; i < PlayerList_Team1.Count; i++)
                {
                    int index = DataGrid_PlayerList_Team1.ToList().FindIndex(val => val.Name == PlayerList_Team1[i].Name);
                    if (index == -1)
                    {
                        DataGrid_PlayerList_Team1.Add(new PlayerListModel()
                        {
                            Rank = PlayerList_Team1[i].Rank,
                            Clan = PlayerList_Team1[i].Clan,
                            Name = PlayerList_Team1[i].Name,
                            PersonaId = PlayerList_Team1[i].PersonaId,
                            Admin = PlayerList_Team1[i].Admin,
                            VIP = PlayerList_Team1[i].VIP,
                            SquadId = PlayerList_Team1[i].SquadId,
                            Kill = PlayerList_Team1[i].Kill,
                            Dead = PlayerList_Team1[i].Dead,
                            KD = PlayerList_Team1[i].KD.ToString("0.00"),
                            KPM = PlayerList_Team1[i].KPM.ToString("0.00"),
                            Score = PlayerList_Team1[i].Score,
                            WeaponS0 = PlayerList_Team1[i].WeaponS0,
                            WeaponS1 = PlayerList_Team1[i].WeaponS1,
                            WeaponS2 = PlayerList_Team1[i].WeaponS2,
                            WeaponS3 = PlayerList_Team1[i].WeaponS3,
                            WeaponS4 = PlayerList_Team1[i].WeaponS4,
                            WeaponS5 = PlayerList_Team1[i].WeaponS5,
                            WeaponS6 = PlayerList_Team1[i].WeaponS6,
                            WeaponS7 = PlayerList_Team1[i].WeaponS7
                        });
                    }
                }

                // 修正序号
                for (int i = 0; i < DataGrid_PlayerList_Team1.Count; i++)
                {
                    DataGrid_PlayerList_Team1[i].Index = i + 1;
                }
            }
        }

        // 更新 DataGrid 队伍2
        private void UpdateDataGridTeam2()
        {
            if (PlayerList_Team2.Count == 0 && DataGrid_PlayerList_Team2.Count != 0)
            {
                DataGrid_PlayerList_Team2.Clear();
            }

            if (PlayerList_Team2.Count != 0)
            {
                // 更新DataGrid中现有的玩家数据，并把DataGrid中已经不在服务器的玩家清除
                for (int i = 0; i < DataGrid_PlayerList_Team2.Count; i++)
                {
                    int index = PlayerList_Team2.FindIndex(val => val.Name == DataGrid_PlayerList_Team2[i].Name);
                    if (index != -1)
                    {
                        DataGrid_PlayerList_Team2[i].Rank = PlayerList_Team2[index].Rank;
                        DataGrid_PlayerList_Team2[i].Clan = PlayerList_Team2[index].Clan;
                        DataGrid_PlayerList_Team2[i].Admin = PlayerList_Team2[index].Admin;
                        DataGrid_PlayerList_Team2[i].VIP = PlayerList_Team2[index].VIP;
                        DataGrid_PlayerList_Team2[i].SquadId = PlayerList_Team2[index].SquadId;
                        DataGrid_PlayerList_Team2[i].Kill = PlayerList_Team2[index].Kill;
                        DataGrid_PlayerList_Team2[i].Dead = PlayerList_Team2[index].Dead;
                        DataGrid_PlayerList_Team2[i].KD = PlayerList_Team2[index].KD.ToString("0.00");
                        DataGrid_PlayerList_Team2[i].KPM = PlayerList_Team2[index].KPM.ToString("0.00");
                        DataGrid_PlayerList_Team2[i].Score = PlayerList_Team2[index].Score;
                        DataGrid_PlayerList_Team2[i].WeaponS0 = PlayerList_Team2[index].WeaponS0;
                        DataGrid_PlayerList_Team2[i].WeaponS1 = PlayerList_Team2[index].WeaponS1;
                        DataGrid_PlayerList_Team2[i].WeaponS2 = PlayerList_Team2[index].WeaponS2;
                        DataGrid_PlayerList_Team2[i].WeaponS3 = PlayerList_Team2[index].WeaponS3;
                        DataGrid_PlayerList_Team2[i].WeaponS4 = PlayerList_Team2[index].WeaponS4;
                        DataGrid_PlayerList_Team2[i].WeaponS5 = PlayerList_Team2[index].WeaponS5;
                        DataGrid_PlayerList_Team2[i].WeaponS6 = PlayerList_Team2[index].WeaponS6;
                        DataGrid_PlayerList_Team2[i].WeaponS7 = PlayerList_Team2[index].WeaponS7;
                    }
                    else
                    {
                        DataGrid_PlayerList_Team2.RemoveAt(i);
                    }
                }

                // 增加DataGrid没有的玩家数据
                for (int i = 0; i < PlayerList_Team2.Count; i++)
                {
                    int index = DataGrid_PlayerList_Team2.ToList().FindIndex(val => val.Name == PlayerList_Team2[i].Name);
                    if (index == -1)
                    {
                        DataGrid_PlayerList_Team2.Add(new PlayerListModel()
                        {
                            Rank = PlayerList_Team2[i].Rank,
                            Clan = PlayerList_Team2[i].Clan,
                            Name = PlayerList_Team2[i].Name,
                            PersonaId = PlayerList_Team2[i].PersonaId,
                            Admin = PlayerList_Team2[i].Admin,
                            VIP = PlayerList_Team2[i].VIP,
                            SquadId = PlayerList_Team2[i].SquadId,
                            Kill = PlayerList_Team2[i].Kill,
                            Dead = PlayerList_Team2[i].Dead,
                            KD = PlayerList_Team2[i].KD.ToString("0.00"),
                            KPM = PlayerList_Team2[i].KPM.ToString("0.00"),
                            Score = PlayerList_Team2[i].Score,
                            WeaponS0 = PlayerList_Team2[i].WeaponS0,
                            WeaponS1 = PlayerList_Team2[i].WeaponS1,
                            WeaponS2 = PlayerList_Team2[i].WeaponS2,
                            WeaponS3 = PlayerList_Team2[i].WeaponS3,
                            WeaponS4 = PlayerList_Team2[i].WeaponS4,
                            WeaponS5 = PlayerList_Team2[i].WeaponS5,
                            WeaponS6 = PlayerList_Team2[i].WeaponS6,
                            WeaponS7 = PlayerList_Team2[i].WeaponS7
                        });
                    }
                }

                // 修正序号
                for (int i = 0; i < DataGrid_PlayerList_Team2.Count; i++)
                {
                    DataGrid_PlayerList_Team2[i].Index = i + 1;
                }
            }
        }

        #region 检查玩家是否违规
        private void CheckPlayerIsBreakRule(PlayerData playerData)
        {
            int index = Globals.BreakRuleInfo_PlayerList.FindIndex(val => val.PersonaId == playerData.PersonaId);
            if (index == -1)
            {
                // 限制玩家击杀
                if (playerData.Kill > ServerRule.MaxKill && ServerRule.MaxKill != 0)
                {
                    Globals.BreakRuleInfo_PlayerList.Add(new BreakRuleInfo
                    {
                        Name = playerData.Name,
                        PersonaId = playerData.PersonaId,
                        Reason = $"Kill Limit {ServerRule.MaxKill:0}"
                    });

                    return;
                }

                // 计算玩家KD最低击杀数
                if (playerData.Kill > ServerRule.KDFlag && ServerRule.KDFlag != 0)
                {
                    // 限制玩家KD
                    if (playerData.KD > ServerRule.MaxKD && ServerRule.MaxKD != 0.00f)
                    {
                        Globals.BreakRuleInfo_PlayerList.Add(new BreakRuleInfo
                        {
                            Name = playerData.Name,
                            PersonaId = playerData.PersonaId,
                            Reason = $"KD Limit {ServerRule.MaxKD:0.00}"
                        });
                    }

                    return;
                }

                // 计算玩家KPM比条件
                if (playerData.Kill > ServerRule.KPMFlag && ServerRule.KPMFlag != 0)
                {
                    // 限制玩家KPM
                    if (playerData.KPM > ServerRule.MaxKPM && ServerRule.MaxKPM != 0.00f)
                    {
                        Globals.BreakRuleInfo_PlayerList.Add(new BreakRuleInfo
                        {
                            Name = playerData.Name,
                            PersonaId = playerData.PersonaId,
                            Reason = $"KPM Limit {ServerRule.MaxKPM:0.00}"
                        });
                    }

                    return;
                }

                // 限制玩家最低等级
                if (playerData.Rank < ServerRule.MinRank && ServerRule.MinRank != 0 && playerData.Rank != 0)
                {
                    Globals.BreakRuleInfo_PlayerList.Add(new BreakRuleInfo
                    {
                        Name = playerData.Name,
                        PersonaId = playerData.PersonaId,
                        Reason = $"Min Rank Limit {ServerRule.MinRank:0}"
                    });

                    return;
                }

                // 限制玩家最高等级
                if (playerData.Rank > ServerRule.MaxRank && ServerRule.MaxRank != 0 && playerData.Rank != 0)
                {
                    Globals.BreakRuleInfo_PlayerList.Add(new BreakRuleInfo
                    {
                        Name = playerData.Name,
                        PersonaId = playerData.PersonaId,
                        Reason = $"Max Rank Limit {ServerRule.MaxRank:0}"
                    });

                    return;
                }

                // 从武器规则里遍历限制武器名称
                for (int i = 0; i < Globals.Custom_WeaponList.Count; i++)
                {
                    var item = Globals.Custom_WeaponList[i];

                    // K 弹
                    if (item == "_KBullet")
                    {
                        if (playerData.WeaponS0.Contains("_KBullet") ||
                            playerData.WeaponS1.Contains("_KBullet") ||
                            playerData.WeaponS2.Contains("_KBullet") ||
                            playerData.WeaponS3.Contains("_KBullet") ||
                            playerData.WeaponS4.Contains("_KBullet") ||
                            playerData.WeaponS5.Contains("_KBullet") ||
                            playerData.WeaponS6.Contains("_KBullet") ||
                            playerData.WeaponS7.Contains("_KBullet"))
                        {
                            Globals.BreakRuleInfo_PlayerList.Add(new BreakRuleInfo
                            {
                                Name = playerData.Name,
                                PersonaId = playerData.PersonaId,
                                Reason = $"Weapon Limit K Bullet"
                            });

                            return;
                        }
                    }

                    // 步枪手榴弹（破片）
                    if (item == "_RGL_Frag")
                    {
                        if (playerData.WeaponS0.Contains("_RGL_Frag") ||
                            playerData.WeaponS1.Contains("_RGL_Frag") ||
                            playerData.WeaponS2.Contains("_RGL_Frag") ||
                            playerData.WeaponS3.Contains("_RGL_Frag") ||
                            playerData.WeaponS4.Contains("_RGL_Frag") ||
                            playerData.WeaponS5.Contains("_RGL_Frag") ||
                            playerData.WeaponS6.Contains("_RGL_Frag") ||
                            playerData.WeaponS7.Contains("_RGL_Frag"))
                        {
                            Globals.BreakRuleInfo_PlayerList.Add(new BreakRuleInfo
                            {
                                Name = playerData.Name,
                                PersonaId = playerData.PersonaId,
                                Reason = $"Weapon Limit RGL Frag"
                            });

                            return;
                        }
                    }

                    // 步枪手榴弹（烟雾）
                    if (item == "_RGL_Smoke")
                    {
                        if (playerData.WeaponS0.Contains("_RGL_Smoke") ||
                            playerData.WeaponS1.Contains("_RGL_Smoke") ||
                            playerData.WeaponS2.Contains("_RGL_Smoke") ||
                            playerData.WeaponS3.Contains("_RGL_Smoke") ||
                            playerData.WeaponS4.Contains("_RGL_Smoke") ||
                            playerData.WeaponS5.Contains("_RGL_Smoke") ||
                            playerData.WeaponS6.Contains("_RGL_Smoke") ||
                            playerData.WeaponS7.Contains("_RGL_Smoke"))
                        {
                            Globals.BreakRuleInfo_PlayerList.Add(new BreakRuleInfo
                            {
                                Name = playerData.Name,
                                PersonaId = playerData.PersonaId,
                                Reason = $"Weapon Limit RGL Smoke"
                            });

                            return;
                        }
                    }

                    // 步枪手榴弹（高爆）
                    if (item == "_RGL_HE")
                    {
                        if (playerData.WeaponS0.Contains("_RGL_HE") ||
                            playerData.WeaponS1.Contains("_RGL_HE") ||
                            playerData.WeaponS2.Contains("_RGL_HE") ||
                            playerData.WeaponS3.Contains("_RGL_HE") ||
                            playerData.WeaponS4.Contains("_RGL_HE") ||
                            playerData.WeaponS5.Contains("_RGL_HE") ||
                            playerData.WeaponS6.Contains("_RGL_HE") ||
                            playerData.WeaponS7.Contains("_RGL_HE"))
                        {
                            Globals.BreakRuleInfo_PlayerList.Add(new BreakRuleInfo
                            {
                                Name = playerData.Name,
                                PersonaId = playerData.PersonaId,
                                Reason = $"Weapon Limit RGL HE"
                            });

                            return;
                        }
                    }

                    if (playerData.WeaponS0 == item ||
                        playerData.WeaponS1 == item ||
                        playerData.WeaponS2 == item ||
                        playerData.WeaponS3 == item ||
                        playerData.WeaponS4 == item ||
                        playerData.WeaponS5 == item ||
                        playerData.WeaponS6 == item ||
                        playerData.WeaponS7 == item)
                    {
                        Globals.BreakRuleInfo_PlayerList.Add(new BreakRuleInfo
                        {
                            Name = playerData.Name,
                            PersonaId = playerData.PersonaId,
                            Reason = $"Weapon Limit {PlayerUtil.GetWeaponShortTxt(item)}"
                        });

                        return;
                    }
                }

                // 黑名单
                for (int i = 0; i < Globals.Custom_BlackList.Count; i++)
                {
                    var item = Globals.Custom_BlackList[i];
                    if (playerData.Name == item)
                    {
                        Globals.BreakRuleInfo_PlayerList.Add(new BreakRuleInfo
                        {
                            Name = playerData.Name,
                            PersonaId = playerData.PersonaId,
                            Reason = "Server Black List"
                        });

                        return;
                    }
                }
            }
        }

        private void AutoKickBreakPlayer()
        {
            // 自动踢出违规玩家开关
            if (Globals.AutoKickBreakPlayer)
            {
                // 遍历违规玩家列表
                for (int i = 0; i < Globals.BreakRuleInfo_PlayerList.Count; i++)
                {
                    var item = Globals.BreakRuleInfo_PlayerList[i];
                    item.Flag = -1;

                    // 跳过管理员
                    if (!Globals.Server_AdminList.Contains(item.PersonaId.ToString()))
                    {
                        // 跳过白名单玩家
                        if (!Globals.Custom_WhiteList.Contains(item.Name))
                        {
                            // 先检查踢出玩家是否在 正在踢人 列表中
                            int index = Kicking_PlayerList.FindIndex(var => var.PersonaId == item.PersonaId);
                            if (index == -1)
                            {
                                // 该玩家不在 正在踢人 列表中
                                item.Flag = 0;
                                item.Status = "正在踢人中...";
                                item.Time = DateTime.Now;
                                Kicking_PlayerList.Add(item);

                                // 执行踢人请求
                                AutoKickPlayer(item);
                            }
                        }
                    }
                }

                for (int i = 0; i < Kicking_PlayerList.Count; i++)
                {
                    if (Kicking_PlayerList.Count != 0)
                    {
                        // 如果超过15秒，清空列表
                        if (CoreUtil.DiffSeconds(Kicking_PlayerList[i].Time, DateTime.Now) > 10)
                        {
                            Kicking_PlayerList.Clear();
                            break;
                        }
                    }

                    if (Kicking_PlayerList.Count != 0 && Kicking_PlayerList[i].Flag == 0)
                    {
                        // 如果超过3秒，移除 正在踢人 玩家
                        if (CoreUtil.DiffSeconds(Kicking_PlayerList[i].Time, DateTime.Now) > 3)
                        {
                            Kicking_PlayerList.RemoveAt(i);
                        }
                    }

                    if (Kicking_PlayerList.Count != 0 && Kicking_PlayerList[i].Flag == 1)
                    {
                        // 如果超过10秒，移除 踢出成功 玩家
                        if (CoreUtil.DiffSeconds(Kicking_PlayerList[i].Time, DateTime.Now) > 10)
                        {
                            Kicking_PlayerList.RemoveAt(i);
                        }
                    }

                    if (Kicking_PlayerList.Count != 0 && Kicking_PlayerList[i].Flag == 2)
                    {
                        // 如果超过5秒，移除 踢出失败 玩家
                        if (CoreUtil.DiffSeconds(Kicking_PlayerList[i].Time, DateTime.Now) > 5)
                        {
                            Kicking_PlayerList.RemoveAt(i);
                        }
                    }
                }
            }
        }

        // 自动踢出违规玩家
        private async void AutoKickPlayer(BreakRuleInfo info)
        {
            var result = await BF1API.API.BF1API.AdminKickPlayer(info.PersonaId.ToString(), info.Reason);

            if (result.IsSuccess)
            {
                info.Flag = 1;
                info.Status = "踢出成功";
                info.Time = DateTime.Now;

                LogView._dAddKickOKLog(info);
            }
            else
            {
                info.Flag = 2;
                info.Status = "踢出失败 " + result.Message;
                info.Time = DateTime.Now;

                LogView._dAddKickNOLog(info);
            }

        }
        #endregion

        // 手动踢出违规玩家
        private async void KickPlayer(string reason)
        {
            if (!string.IsNullOrEmpty(Globals.SessionId))
            {
                if (_dataGridSelcContent.IsOK)
                {
                    MainWindow._SetOperatingState(2, $"正在踢出玩家 {_dataGridSelcContent.Name} 中...");

                    var result = await BF1API.API.BF1API.AdminKickPlayer(_dataGridSelcContent.PersonaId.ToString(), reason);

                    if (result.IsSuccess)
                    {
                        MainWindow._SetOperatingState(1, $"踢出玩家 {_dataGridSelcContent.Name} 成功  |  耗时: {result.ExecTime:0.00} 秒");
                    }
                    else
                    {
                        MainWindow._SetOperatingState(3, $"踢出玩家 {_dataGridSelcContent.Name} 失败 {result.Message}  |  耗时: {result.ExecTime:0.00} 秒");
                    }
                }
                else
                {
                    MainWindow._SetOperatingState(2, "请选择正确的玩家");
                }
            }
            else
            {
                MainWindow._SetOperatingState(2, "请先获取玩家SessionID");
            }
        }

        #region 右键菜单事件
        private void MenuItem_Admin_KickPlayer_Custom_Click(object sender, RoutedEventArgs e)
        {
            // 右键菜单 踢出玩家 - 自定义理由
            if (!string.IsNullOrEmpty(Globals.SessionId))
            {
                if (_dataGridSelcContent.IsOK)
                {
                    var customKickWindow = new CustomKickWindow(_dataGridSelcContent.Name, _dataGridSelcContent.PersonaId.ToString());
                    customKickWindow.Owner = MainWindow.ThisMainWindow;
                    customKickWindow.ShowDialog();
                }
                else
                {
                    MainWindow._SetOperatingState(2, "请选择正确的玩家");
                }
            }
            else
            {
                MainWindow._SetOperatingState(2, "请先获取玩家SessionID");
            }
        }

        private void MenuItem_Admin_KickPlayer_OffensiveBehavior_Click(object sender, RoutedEventArgs e)
        {
            // 右键菜单 踢出玩家 - 攻击性行为
            KickPlayer("OFFENSIVEBEHAVIOR");
        }

        private void MenuItem_Admin_KickPlayer_Latency_Click(object sender, RoutedEventArgs e)
        {
            // 右键菜单 踢出玩家 - 延迟
            KickPlayer("LATENCY");
        }

        private void MenuItem_Admin_KickPlayer_RuleViolation_Click(object sender, RoutedEventArgs e)
        {
            // 右键菜单 踢出玩家 - 违反规则
            KickPlayer("RULEVIOLATION");
        }

        private void MenuItem_Admin_KickPlayer_General_Click(object sender, RoutedEventArgs e)
        {
            // 右键菜单 踢出玩家 - 其他
            KickPlayer("GENERAL");
        }

        private async void MenuItem_Admin_ChangePlayerTeam_Click(object sender, RoutedEventArgs e)
        {
            // 右键菜单 更换玩家队伍
            if (!string.IsNullOrEmpty(Globals.SessionId))
            {
                if (_dataGridSelcContent.IsOK)
                {
                    MainWindow._SetOperatingState(2, $"正在更换玩家 {_dataGridSelcContent.Name} 队伍中...");

                    var result = await BF1API.API.BF1API.AdminMovePlayer(_dataGridSelcContent.PersonaId.ToString(), _dataGridSelcContent.TeamID.ToString());

                    if (result.IsSuccess)
                    {
                        MainWindow._SetOperatingState(1, $"更换玩家 {_dataGridSelcContent.Name} 队伍成功  |  耗时: {result.ExecTime:0.00} 秒");
                    }
                    else
                    {
                        MainWindow._SetOperatingState(3, $"更换玩家 {_dataGridSelcContent.Name} 队伍失败 {result.Message}  |  耗时: {result.ExecTime:0.00} 秒");
                    }
                }
                else
                {
                    MainWindow._SetOperatingState(2, "请选择正确的玩家，操作取消");
                }
            }
            else
            {
                MainWindow._SetOperatingState(2, "请先获取玩家SessionID后，再执行本操作");
            }
        }

        private void MenuItem_CopyPlayerName_Click(object sender, RoutedEventArgs e)
        {
            if (_dataGridSelcContent.IsOK)
            {
                // 复制玩家ID（无队标）
                Clipboard.SetText(_dataGridSelcContent.Name);
                MainWindow._SetOperatingState(1, $"复制玩家ID {_dataGridSelcContent.Name} 到剪切板成功");
            }
            else
            {
                MainWindow._SetOperatingState(2, "请选择正确的玩家，操作取消");
            }
        }

        private void MenuItem_CopyPlayerName_PID_Click(object sender, RoutedEventArgs e)
        {
            if (_dataGridSelcContent.IsOK)
            {
                // 复制玩家数字ID
                Clipboard.SetText(_dataGridSelcContent.PersonaId.ToString());
                MainWindow._SetOperatingState(1, $"复制玩家数字ID {_dataGridSelcContent.PersonaId} 到剪切板成功");
            }
            else
            {
                MainWindow._SetOperatingState(2, "请选择正确的玩家，操作取消");
            }
        }

        private void MenuItem_QueryPlayerRecord_Click(object sender, RoutedEventArgs e)
        {
            if (_dataGridSelcContent.IsOK)
            {
                // 查询玩家战绩
                MainWindow._TabControlSelect(1);
                QueryView._QuickQueryPalyer(_dataGridSelcContent.Name);
            }
            else
            {
                MainWindow._SetOperatingState(2, "请选择正确的玩家，操作取消");
            }
        }

        private void MenuItem_QueryPlayerRecordWeb_BT_Click(object sender, RoutedEventArgs e)
        {
            // 查询玩家战绩（BT）
            if (_dataGridSelcContent.IsOK)
            {
                string playerName = _dataGridSelcContent.Name;

                ProcessUtil.OpenLink(@"https://battlefieldtracker.com/bf1/profile/pc/" + playerName);
                MainWindow._SetOperatingState(1, $"查询玩家（{_dataGridSelcContent.Name}）战绩成功，请前往浏览器查看");
            }
            else
            {
                MainWindow._SetOperatingState(2, "请选择正确的玩家，操作取消");
            }
        }

        private void MenuItem_QueryPlayerRecordWeb_GT_Click(object sender, RoutedEventArgs e)
        {
            // 查询玩家战绩（GT）
            if (_dataGridSelcContent.IsOK)
            {
                string playerName = _dataGridSelcContent.Name;

                ProcessUtil.OpenLink(@"https://gametools.network/stats/pc/name/" + playerName + "?game=bf1");
                MainWindow._SetOperatingState(1, $"查询玩家（{_dataGridSelcContent.Name}）战绩成功，请前往浏览器查看");
            }
            else
            {
                MainWindow._SetOperatingState(2, "请选择正确的玩家，操作取消");
            }
        }

        private void MenuItem_ClearScoreSort_Click(object sender, RoutedEventArgs e)
        {
            // 清理得分板标题排序

            Dispatcher.BeginInvoke(new Action(delegate
            {
                CollectionViewSource.GetDefaultView(DataGrid_Team1.ItemsSource).SortDescriptions.Clear();
                CollectionViewSource.GetDefaultView(DataGrid_Team2.ItemsSource).SortDescriptions.Clear();

                MainWindow._SetOperatingState(1, "清理得分板标题排序成功（默认为玩家得分从高到低排序）");
            }));
        }

        private void MenuItem_ShowWeaponNameZHCN_Click(object sender, RoutedEventArgs e)
        {
            // 显示中文武器名称（参考）
            var item = sender as MenuItem;
            if (item != null)
            {
                if (item.IsChecked)
                {
                    Globals.IsShowCHSWeaponName = true;
                    MainWindow._SetOperatingState(1, $"当前得分板正在显示中文武器名称");
                }
                else
                {
                    Globals.IsShowCHSWeaponName = false;
                    MainWindow._SetOperatingState(1, $"当前得分板正在显示英文武器名称");
                }
            }
        }
        #endregion

        #region DataGrid相关方法
        private void DataGrid_Team1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = DataGrid_Team1.SelectedItem as PlayerListModel;
            if (item != null)
            {
                _dataGridSelcContent.IsOK = true;
                _dataGridSelcContent.TeamID = 1;
                _dataGridSelcContent.Rank = item.Rank;
                _dataGridSelcContent.Name = item.Name;
                _dataGridSelcContent.PersonaId = item.PersonaId;
            }
            else
            {
                _dataGridSelcContent.IsOK = false;
                _dataGridSelcContent.TeamID = -1;
                _dataGridSelcContent.Rank = -1;
                _dataGridSelcContent.Name = string.Empty;
                _dataGridSelcContent.PersonaId = -1;
            }

            Update_DateGrid_Selection();
        }

        private void DataGrid_Team2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = DataGrid_Team2.SelectedItem as PlayerListModel;
            if (item != null)
            {
                _dataGridSelcContent.IsOK = true;
                _dataGridSelcContent.TeamID = 2;
                _dataGridSelcContent.Rank = item.Rank;
                _dataGridSelcContent.Name = item.Name;
                _dataGridSelcContent.PersonaId = item.PersonaId;
            }
            else
            {
                _dataGridSelcContent.IsOK = false;
                _dataGridSelcContent.TeamID = -1;
                _dataGridSelcContent.Rank = -1;
                _dataGridSelcContent.Name = string.Empty;
                _dataGridSelcContent.PersonaId = -1;
            }

            Update_DateGrid_Selection();
        }

        private void Update_DateGrid_Selection()
        {
            StringBuilder sb = new StringBuilder();

            if (_dataGridSelcContent.IsOK)
            {
                sb.Append($"玩家ID : {_dataGridSelcContent.Name}");
                sb.Append($"  |  玩家队伍ID : {_dataGridSelcContent.TeamID}");
                sb.Append($"  |  玩家等级 : {_dataGridSelcContent.Rank}");
                sb.Append($"  |  更新时间 : {DateTime.Now}");
            }
            else
            {
                sb.Append($"当前未选中任何玩家");
                sb.Append($"  |  更新时间 : {DateTime.Now}");
            }

            TextBlock_DataGridSelectionContent.Text = sb.ToString();
        }
        #endregion
    }
}
