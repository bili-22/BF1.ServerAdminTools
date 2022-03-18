using BF1.ServerAdminTools.Models;
using BF1.ServerAdminTools.Windows;
using BF1.ServerAdminTools.Common.Data;
using BF1.ServerAdminTools.Common.Utils;
using BF1.ServerAdminTools.Features.Utils;
using BF1.ServerAdminTools.Features.API;
using BF1.ServerAdminTools.Features.API.RespJson;

namespace BF1.ServerAdminTools.Views
{
    /// <summary>
    /// DetailView.xaml 的交互逻辑
    /// </summary>
    public partial class DetailView : UserControl
    {
        public DetailModel DetailModel { get; set; }

        public class Map
        {
            public string mapPrettyName { get; set; }
            public string mapImage { get; set; }
            public string modePrettyName { get; set; }
        }

        public class ListItem
        {
            public int Index { get; set; }
            public string platform { get; set; }
            public string nucleusId { get; set; }
            public string personaId { get; set; }
            public string platformId { get; set; }
            public string displayName { get; set; }
            public string avatar { get; set; }
            public string accountId { get; set; }
        }

        private ServerDetails serverDetails = null;
        private bool isGetServerDetailsOK = false;

        public DetailView()
        {
            InitializeComponent();

            this.DataContext = this;

            DetailModel = new DetailModel();

            MainWindow.ClosingDisposeEvent += MainWindow_ClosingDisposeEvent;
        }

        private void MainWindow_ClosingDisposeEvent()
        {

        }

        private async void Button_GetFullServerDetails_Click(object sender, RoutedEventArgs e)
        {
            AudioUtil.ClickSound();

            if (!string.IsNullOrEmpty(Globals.SessionId))
            {
                if (!string.IsNullOrEmpty(Globals.GameId))
                {
                    DetailModel.ServerName = "获取中...";
                    DetailModel.ServerDescription = "获取中...";
                    DetailModel.ServerOwnerName = "获取中...";
                    DetailModel.ServerOwnerPersonaId = "获取中...";
                    DetailModel.ServerID = "获取中...";
                    DetailModel.ServerGameID = "获取中...";

                    DetailModel.ServerOwnerImage = null;
                    DetailModel.ServerCurrentMap = null;

                    ListBox_Map.Items.Clear();
                    ListBox_Admin.Items.Clear();
                    ListBox_VIP.Items.Clear();
                    ListBox_BAN.Items.Clear();

                    Globals.Server_AdminList.Clear();
                    Globals.Server_Admin2List.Clear();
                    Globals.Server_VIPList.Clear();

                    /////////////////////////////////////////////////////////////////////////////////

                    MainWindow.dSetOperatingState(2, $"正在获取服务器 {Globals.GameId} 详细数据中...");

                    await BF1API.SetAPILocale();
                    var result = await BF1API.GetFullServerDetails();

                    if (result.IsSuccess)
                    {
                        var fullServerDetails = JsonUtil.JsonDese<FullServerDetails>(result.Message);

                        Globals.ServerId = fullServerDetails.result.rspInfo.server.serverId;
                        Globals.PersistedGameId = fullServerDetails.result.rspInfo.server.persistedGameId;

                        DetailModel.ServerName = fullServerDetails.result.serverInfo.name;
                        DetailModel.ServerDescription = fullServerDetails.result.serverInfo.description;

                        DetailModel.ServerID = Globals.ServerId;
                        DetailModel.ServerGameID = Globals.GameId;

                        DetailModel.ServerOwnerName = fullServerDetails.result.rspInfo.owner.displayName;
                        DetailModel.ServerOwnerPersonaId = fullServerDetails.result.rspInfo.owner.personaId;
                        DetailModel.ServerOwnerImage = fullServerDetails.result.rspInfo.owner.avatar;
                        DetailModel.ServerCurrentMap = PlayerUtil.GetTempImagePath(fullServerDetails.result.serverInfo.mapImageUrl, "maps");

                        // 地图列表
                        foreach (var item in fullServerDetails.result.serverInfo.rotation)
                        {
                            ListBox_Map.Items.Add(new Map()
                            {
                                mapImage = PlayerUtil.GetTempImagePath(item.mapImage, "maps"),
                                mapPrettyName = ChsUtil.ToSimplifiedChinese(item.mapPrettyName),
                                modePrettyName = ChsUtil.ToSimplifiedChinese(item.modePrettyName)
                            });
                        }

                        // 服主
                        int index = 0;
                        ListBox_Admin.Items.Add(new ListItem()
                        {
                            Index = index++,
                            avatar = fullServerDetails.result.rspInfo.owner.avatar,
                            displayName = fullServerDetails.result.rspInfo.owner.displayName,
                            personaId = fullServerDetails.result.rspInfo.owner.personaId
                        });
                        Globals.Server_AdminList.Add(fullServerDetails.result.rspInfo.owner.personaId);
                        Globals.Server_Admin2List.Add(fullServerDetails.result.rspInfo.owner.displayName);
                        // 管理员列表
                        foreach (var item in fullServerDetails.result.rspInfo.adminList)
                        {
                            ListBox_Admin.Items.Add(new ListItem()
                            {
                                Index = index++,
                                avatar = item.avatar,
                                displayName = item.displayName,
                                personaId = item.personaId
                            });

                            Globals.Server_AdminList.Add(item.personaId);
                            Globals.Server_Admin2List.Add(item.displayName);
                        }

                        // VIP列表
                        index = 1;
                        foreach (var item in fullServerDetails.result.rspInfo.vipList)
                        {
                            ListBox_VIP.Items.Add(new ListItem()
                            {
                                Index = index++,
                                avatar = item.avatar,
                                displayName = item.displayName,
                                personaId = item.personaId
                            });

                            Globals.Server_VIPList.Add(item.personaId);
                        }

                        // BAN列表
                        index = 1;
                        foreach (var item in fullServerDetails.result.rspInfo.bannedList)
                        {
                            ListBox_BAN.Items.Add(new ListItem()
                            {
                                Index = index++,
                                avatar = item.avatar,
                                displayName = item.displayName,
                                personaId = item.personaId
                            });
                        }

                        MainWindow.dSetOperatingState(1, $"获取服务器 {Globals.GameId} 详细数据成功  |  耗时: {result.ExecTime:0.00} 秒");
                    }
                    else
                    {
                        MainWindow.dSetOperatingState(3, $"获取服务器 {Globals.GameId} 详细数据失败 {result.Message}  |  耗时: {result.ExecTime:0.00} 秒");
                    }
                }
                else
                {
                    MainWindow.dSetOperatingState(2, "请先进入服务器获取GameID");
                }
            }
            else
            {
                MainWindow.dSetOperatingState(2, "请先获取玩家SessionID");
            }
        }

        private async void ListBox_Map_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ListBox_Map.SelectedIndex;
            if (index != -1)
            {
                Map currMap = ListBox_Map.SelectedItem as Map;

                if (!string.IsNullOrEmpty(Globals.PersistedGameId))
                {
                    string mapInfo = currMap.modePrettyName + " - " + currMap.mapPrettyName;

                    var changeMapWindow = new ChangeMapWindow(mapInfo, currMap.mapImage);
                    changeMapWindow.Owner = MainWindow.ThisMainWindow;

                    if (changeMapWindow.ShowDialog() == true)
                    {
                        MainWindow.dSetOperatingState(2, $"正在更换服务器 {Globals.GameId} 地图为 {currMap.mapPrettyName} 中...");

                        var result = await BF1API.ChangeServerMap(Globals.PersistedGameId, index.ToString());

                        if (result.IsSuccess)
                        {
                            MainWindow.dSetOperatingState(1, $"更换服务器 {Globals.GameId} 地图为 {currMap.mapPrettyName} 成功  |  耗时: {result.ExecTime:0.00} 秒");
                        }
                        else
                        {
                            MainWindow.dSetOperatingState(3, $"更换服务器 {Globals.GameId} 地图为 {currMap.mapPrettyName} 失败 {result.Message}  |  耗时: {result.ExecTime:0.00} 秒");
                        }
                    }
                }
                else
                {
                    MainWindow.dSetOperatingState(2, "PersistedGameId异常，请重新获取服务器详细信息");
                }
            }

            // 使ListBox能够相应重复点击
            ListBox_Map.SelectedIndex = -1;
        }

        private async void Button_RemoveSelectedAdmin_Click(object sender, RoutedEventArgs e)
        {
            AudioUtil.ClickSound();

            ListItem currListItem = ListBox_Admin.SelectedItem as ListItem;

            MainWindow.dSetOperatingState(2, $"正在移除服务器管理员 {currListItem.displayName} 中...");

            var result = await BF1API.RemoveServerAdmin(currListItem.personaId);

            if (result.IsSuccess)
            {
                MainWindow.dSetOperatingState(1, $"移除服务器管理员 {currListItem.displayName} 成功  |  耗时: {result.ExecTime:0.00} 秒");
            }
            else
            {
                MainWindow.dSetOperatingState(3, $"移除服务器管理员 {currListItem.displayName} 失败 {result.Message}  |  耗时: {result.ExecTime:0.00} 秒");
            }
        }

        private async void Button_AddNewAdmin_Click(object sender, RoutedEventArgs e)
        {
            AudioUtil.ClickSound();

            MainWindow.dSetOperatingState(2, $"正在增加服务器管理员 {TextBox_NewAdminName.Text} 中...");

            var result = await BF1API.AddServerAdmin(TextBox_NewAdminName.Text);

            if (result.IsSuccess)
            {
                MainWindow.dSetOperatingState(1, $"增加服务器管理员 {TextBox_NewAdminName.Text} 成功  |  耗时: {result.ExecTime:0.00} 秒");
            }
            else
            {
                MainWindow.dSetOperatingState(3, $"增加服务器管理员 {TextBox_NewAdminName.Text} 失败 {result.Message}  |  耗时: {result.ExecTime:0.00} 秒");
            }
        }

        private async void Button_RemoveSelectedVIP_Click(object sender, RoutedEventArgs e)
        {
            AudioUtil.ClickSound();

            ListItem currListItem = ListBox_VIP.SelectedItem as ListItem;

            MainWindow.dSetOperatingState(2, $"正在移除服务器VIP {currListItem.displayName} 中...");

            var result = await BF1API.RemoveServerVip(currListItem.personaId);

            if (result.IsSuccess)
            {
                MainWindow.dSetOperatingState(1, $"移除服务器VIP {currListItem.displayName} 成功  |  耗时: {result.ExecTime:0.00} 秒");
            }
            else
            {
                MainWindow.dSetOperatingState(3, $"移除服务器VIP {currListItem.displayName} 失败 {result.Message}  |  耗时: {result.ExecTime:0.00} 秒");
            }
        }

        private async void Button_AddNewVIP_Click(object sender, RoutedEventArgs e)
        {
            AudioUtil.ClickSound();

            MainWindow.dSetOperatingState(2, $"正在增加服务器VIP {TextBox_NewVIPName.Text} 中...");

            var result = await BF1API.AddServerVip(TextBox_NewVIPName.Text);

            if (result.IsSuccess)
            {
                MainWindow.dSetOperatingState(1, $"增加服务器VIP {TextBox_NewVIPName.Text} 成功  |  耗时: {result.ExecTime:0.00} 秒");
            }
            else
            {
                MainWindow.dSetOperatingState(3, $"增加服务器VIP {TextBox_NewVIPName.Text} 失败 {result.Message}  |  耗时: {result.ExecTime:0.00} 秒");
            }
        }

        private async void Button_RemoveSelectedBAN_Click(object sender, RoutedEventArgs e)
        {
            AudioUtil.ClickSound();

            ListItem currListItem = ListBox_BAN.SelectedItem as ListItem;

            MainWindow.dSetOperatingState(2, $"正在移除服务器BAN {currListItem.displayName} 中...");

            var result = await BF1API.RemoveServerBan(currListItem.personaId);

            if (result.IsSuccess)
            {
                MainWindow.dSetOperatingState(1, $"移除服务器BAN {currListItem.displayName} 成功  |  耗时: {result.ExecTime:0.00} 秒");
            }
            else
            {
                MainWindow.dSetOperatingState(3, $"移除服务器BAN {currListItem.displayName} 失败 {result.Message}  |  耗时: {result.ExecTime:0.00} 秒");
            }
        }

        private async void Button_AddNewBAN_Click(object sender, RoutedEventArgs e)
        {
            AudioUtil.ClickSound();

            MainWindow.dSetOperatingState(2, $"正在增加服务器BAN {TextBox_NewBANName.Text} 中...");

            var result = await BF1API.AddServerBan(TextBox_NewBANName.Text);

            if (result.IsSuccess)
            {
                MainWindow.dSetOperatingState(1, $"增加服务器BAN {TextBox_NewBANName.Text} 成功  |  耗时: {result.ExecTime:0.00} 秒");
            }
            else
            {
                MainWindow.dSetOperatingState(3, $"增加服务器BAN {TextBox_NewBANName.Text} 失败 {result.Message}  |  耗时: {result.ExecTime:0.00} 秒");
            }
        }

        private async void Button_KickSelectedSpectator_Click(object sender, RoutedEventArgs e)
        {
            AudioUtil.ClickSound();

            if (!string.IsNullOrEmpty(Globals.SessionId))
            {
                SpectatorInfo info = ListBox_Spectator.SelectedItem as SpectatorInfo;

                MainWindow.dSetOperatingState(2, $"正在踢出玩家 {info.Name} 中...");

                var result = await BF1API.AdminKickPlayer(info.PersonaId.ToString(), TextBox_KickSelectedSpectatorReason.Text);

                if (result.IsSuccess)
                {
                    MainWindow.dSetOperatingState(1, $"踢出玩家 {info.Name} 成功  |  耗时: {result.ExecTime:0.00} 秒");
                }
                else
                {
                    MainWindow.dSetOperatingState(3, $"踢出玩家 {info.Name} 失败 {result.Message}  |  耗时: {result.ExecTime:0.00} 秒");
                }
            }
            else
            {
                MainWindow.dSetOperatingState(2, "请先获取玩家SessionID");
            }
        }

        private void Button_RefreshSpectatorList_Click(object sender, RoutedEventArgs e)
        {
            AudioUtil.ClickSound();

            ListBox_Spectator.Items.Clear();

            //string defaultAvatar = "https://secure.download.dm.origin.com/production/avatar/prod/1/599/208x208.JPEG";
            int index = 1;
            foreach (var item in Globals.Server_SpectatorList)
            {
                ListBox_Spectator.Items.Add(new SpectatorInfo()
                {
                    Index = index++,
                    Avatar = @"\Assets\Images\Other\Avatar.jpg",
                    Name = item.Name,
                    PersonaId = item.PersonaId
                });
            }
        }

        private async void Button_GetServerDetails_Click(object sender, RoutedEventArgs e)
        {
            AudioUtil.ClickSound();

            if (!string.IsNullOrEmpty(Globals.SessionId))
            {
                if (!string.IsNullOrEmpty(Globals.ServerId))
                {
                    MainWindow.dSetOperatingState(2, $"正在获取服务器 {Globals.ServerId} 数据中...");

                    var result = await BF1API.GetServerDetails();

                    if (result.IsSuccess)
                    {
                        serverDetails = JsonUtil.JsonDese<ServerDetails>(result.Message);

                        TextBox_ServerName.Text = serverDetails.result.serverSettings.name;
                        TextBox_ServerDescription.Text = serverDetails.result.serverSettings.description;

                        isGetServerDetailsOK = true;

                        MainWindow.dSetOperatingState(1, $"获取服务器 {Globals.ServerId} 数据成功  |  耗时: {result.ExecTime:0.00} 秒");
                    }
                    else
                    {
                        MainWindow.dSetOperatingState(3, $"获取服务器 {Globals.ServerId} 数据失败 {result.Message}  |  耗时: {result.ExecTime:0.00} 秒");
                    }
                }
                else
                {
                    MainWindow.dSetOperatingState(2, "请先进入服务器获取ServerID");
                }
            }
            else
            {
                MainWindow.dSetOperatingState(2, "请先获取玩家SessionID");
            }
        }

        private void Button_SetServerDetails2Traditional_Click(object sender, RoutedEventArgs e)
        {
            AudioUtil.ClickSound();

            var serverDescription = TextBox_ServerDescription.Text.Trim();

            if (string.IsNullOrEmpty(serverDescription))
            {
                MainWindow.dSetOperatingState(2, $"服务器描述不能为空");
                return;
            }

            TextBox_ServerDescription.Text = ChsUtil.ToTraditionalChinese(serverDescription);

            MainWindow.dSetOperatingState(1, $"转换服务器描述文本为繁体中文成功");
        }

        private async void Button_UpdateServer_Click(object sender, RoutedEventArgs e)
        {
            AudioUtil.ClickSound();

            if (!isGetServerDetailsOK)
            {
                MainWindow.dSetOperatingState(2, $"请先获取服务器信息后，再执行本操作");
                return;
            }

            var serverName = TextBox_ServerName.Text.Trim();
            var serverDescription = TextBox_ServerDescription.Text.Trim();

            if (string.IsNullOrEmpty(serverName))
            {
                MainWindow.dSetOperatingState(2, $"服务器名称不能为空");
                return;
            }

            if (!string.IsNullOrEmpty(Globals.SessionId))
            {
                if (!string.IsNullOrEmpty(Globals.ServerId))
                {
                    MainWindow.dSetOperatingState(2, $"正在更新服务器 {Globals.ServerId} 数据中...");

                    UpdateServerReqBody reqBody = new UpdateServerReqBody();
                    reqBody.jsonrpc = "2.0";
                    reqBody.method = "RSP.updateServer";

                    var tempParams = new UpdateServerReqBody.Params();

                    tempParams.deviceIdMap = new UpdateServerReqBody.Params.DeviceIdMap()
                    {
                        machash = Guid.NewGuid().ToString()
                    };
                    tempParams.game = "tunguska";
                    tempParams.serverId = Globals.ServerId;
                    tempParams.bannerSettings = new UpdateServerReqBody.Params.BannerSettings()
                    {
                        bannerUrl = "",
                        clearBanner = true
                    };

                    var tempMapRotation = new UpdateServerReqBody.Params.MapRotation();
                    var temp = serverDetails.result.mapRotations[0];
                    var tempMaps = new List<UpdateServerReqBody.Params.MapRotation.MapsItem>();
                    foreach (var item in temp.maps)
                    {
                        tempMaps.Add(new UpdateServerReqBody.Params.MapRotation.MapsItem()
                        {
                            gameMode = item.gameMode,
                            mapName = item.mapName
                        });
                    }
                    tempMapRotation.maps = tempMaps;
                    tempMapRotation.rotationType = temp.rotationType;
                    tempMapRotation.mod = temp.mod;
                    tempMapRotation.name = temp.name;
                    tempMapRotation.description = temp.description;
                    tempMapRotation.id = "100";

                    tempParams.mapRotation = tempMapRotation;

                    tempParams.serverSettings = new UpdateServerReqBody.Params.ServerSettings()
                    {
                        name = serverName,
                        description = serverDescription,

                        message = serverDetails.result.serverSettings.message,
                        password = serverDetails.result.serverSettings.password,
                        bannerUrl = serverDetails.result.serverSettings.bannerUrl,
                        mapRotationId = serverDetails.result.serverSettings.mapRotationId,
                        customGameSettings = serverDetails.result.serverSettings.customGameSettings
                    };

                    reqBody.@params = tempParams;
                    reqBody.id = Guid.NewGuid().ToString();

                    var result = await BF1API.UpdateServer(reqBody);

                    if (result.IsSuccess)
                    {
                        MainWindow.dSetOperatingState(1, $"更新服务器 {Globals.ServerId} 数据成功  |  耗时: {result.ExecTime:0.00} 秒");
                    }
                    else
                    {
                        MainWindow.dSetOperatingState(3, $"更新服务器 {Globals.ServerId} 数据失败 {result.Message}  |  耗时: {result.ExecTime:0.00} 秒");
                    }
                }
                else
                {
                    MainWindow.dSetOperatingState(2, "请先进入服务器获取ServerID");
                }
            }
            else
            {
                MainWindow.dSetOperatingState(2, "请先获取玩家SessionID");
            }

            isGetServerDetailsOK = false;
        }
    }
}
