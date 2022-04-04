using BF1.ServerAdminTools.BF1API.API.RespJson;
using BF1.ServerAdminTools.BF1API.Core;
using BF1.ServerAdminTools.BF1API.Data;
using BF1.ServerAdminTools.BF1API.Utils;
using BF1.ServerAdminTools.Common;
using BF1.ServerAdminTools.Common.Data;
using BF1.ServerAdminTools.Common.Helper;
using BF1.ServerAdminTools.Common.Utils;
using BF1.ServerAdminTools.Wpf.Models;
using BF1.ServerAdminTools.Wpf.Utils;
using BF1.ServerAdminTools.Wpf.Windows;

namespace BF1.ServerAdminTools.Wpf.Views
{
    /// <summary>
    /// RuleView.xaml 的交互逻辑
    /// </summary>
    public partial class RuleView : UserControl
    {
        /// <summary>
        /// 是否已经执行
        /// </summary>
        private bool isHasBeenExec = false;
        /// <summary>
        /// 是否执行应用规则
        /// </summary>
        private bool isApplyRule = false;

        public RuleView()
        {
            InitializeComponent();

            // 添加武器信息列表
            foreach (var item in WeaponData.AllWeaponInfo)
            {
                ListBox_WeaponInfo.Items.Add(new WeaponInfoModel()
                {
                    English = item.English,
                    Chinese = item.Chinese,
                    Mark = ""
                });
            }
            ListBox_WeaponInfo.SelectedIndex = 0;

            new Thread(AutoKickLifeBreakPlayer)
            {
                Name= "AutoKickLifeThread",
                IsBackground = true
            }.Start();

            new Thread(CheckState)
            {
                Name = "CheckStateThread",
                IsBackground = true
            }.Start();

            foreach (var item in Globals.Rules)
            {
                Rule_List.Items.Add(item.Value);
            }

            Globals.NowRule = Globals.Rules["default"];

            LoadRule();

            MainWindow.ClosingDisposeEvent += MainWindow_ClosingDisposeEvent;
        }

        private void LoadRule() 
        {
            NowName.Text = Globals.NowRule.Name;

            if (Globals.NowRule.Custom_WeaponList == null)
            {
                Globals.NowRule.Custom_WeaponList = new();
            }

            if (Globals.NowRule.Custom_BlackList == null)
            {
                Globals.NowRule.Custom_BlackList = new();
            }

            if (Globals.NowRule.Custom_WhiteList == null)
            {
                Globals.NowRule.Custom_WhiteList = new();
            }

            Slider_MaxKill.Value = Globals.NowRule.MaxKill;
            Slider_KDFlag.Value = Globals.NowRule.KDFlag;
            Slider_MaxKD.Value = Globals.NowRule.MaxKD;
            Slider_KPMFlag.Value = Globals.NowRule.KPMFlag;
            Slider_MaxKPM.Value = Globals.NowRule.MaxKPM;
            Slider_MinRank.Value = Globals.NowRule.MinRank;
            Slider_MaxRank.Value = Globals.NowRule.MaxRank;

            Slider_LifeMaxKD.Value = Globals.NowRule.LifeMaxKD;
            Slider_LifeMaxKPM.Value = Globals.NowRule.LifeMaxKPM;
            Slider_LifeMaxWeaponStar.Value = Globals.NowRule.LifeMaxWeaponStar;
            Slider_LifeMaxVehicleStar.Value = Globals.NowRule.LifeMaxVehicleStar;

            ListBox_BreakWeaponInfo.Items.Clear();
            foreach (var item in Globals.NowRule.Custom_WeaponList)
            {
                ListBox_BreakWeaponInfo.Items.Add(new WeaponInfoModel()
                {
                    English = item,
                    Chinese = PlayerUtil.GetWeaponChsName(item)
                });
            }

            if (ListBox_BreakWeaponInfo.Items.Count != 0)
            {
                ListBox_BreakWeaponInfo.SelectedIndex = 0;
            }

            ListBox_Custom_BlackList.Items.Clear();
            foreach (var item in Globals.NowRule.Custom_BlackList)
            {
                ListBox_Custom_BlackList.Items.Add(item);
            }

            ListBox_Custom_WhiteList.Items.Clear();
            foreach (var item in Globals.NowRule.Custom_WhiteList)
            {
                ListBox_Custom_WhiteList.Items.Add(item);
            }

            foreach (WeaponInfoModel item in ListBox_WeaponInfo.Items)
            {
                foreach (WeaponInfoModel item1 in ListBox_BreakWeaponInfo.Items)
                {
                    if (item.English == item1.English)
                    {
                        item.Mark = "✔";
                    }
                    else
                    {
                        item.Mark = "";
                    }
                }
            }

            Globals.AutoKickBreakPlayer = false;
            CheckBox_RunAutoKick.IsChecked = false;
        }

        private void MainWindow_ClosingDisposeEvent()
        {
            Globals.NowRule.MaxKill = (int)Slider_MaxKill.Value;
            Globals.NowRule.KDFlag = (int)Slider_KDFlag.Value;
            Globals.NowRule.MaxKD = (float)Slider_MaxKD.Value;
            Globals.NowRule.KPMFlag = (int)Slider_KPMFlag.Value;
            Globals.NowRule.MaxKPM = (float)Slider_MaxKPM.Value;
            Globals.NowRule.MinRank = (int)Slider_MinRank.Value;
            Globals.NowRule.MaxRank = (int)Slider_MaxRank.Value;
            Globals.NowRule.LifeMaxKD = (float)Slider_LifeMaxKD.Value;
            Globals.NowRule.LifeMaxKPM = (float)Slider_LifeMaxKPM.Value;
            Globals.NowRule.LifeMaxWeaponStar = (int)Slider_LifeMaxWeaponStar.Value;
            Globals.NowRule.LifeMaxVehicleStar = (int)Slider_LifeMaxVehicleStar.Value;

            Globals.NowRule.Custom_WeaponList.Clear();
            foreach (WeaponInfoModel item in ListBox_BreakWeaponInfo.Items)
            {
                Globals.NowRule.Custom_WeaponList.Add(item.English);
            }
            Globals.NowRule.Custom_BlackList.Clear();
            foreach (string item in ListBox_Custom_BlackList.Items)
            {
                Globals.NowRule.Custom_BlackList.Add(item);
            }
            Globals.NowRule.Custom_WhiteList.Clear();
            foreach (string item in ListBox_Custom_WhiteList.Items)
            {
                Globals.NowRule.Custom_WhiteList.Add(item);
            }
        }

        ////////////////////////////////////////////////////////////////////////////////

        private void CheckState()
        {
            while (true)
            {
                if (string.IsNullOrEmpty(Globals.Config.GameId))
                {
                    if (!isHasBeenExec)
                    {
                        Dispatcher.BeginInvoke(() =>
                        {
                            if (CheckBox_RunAutoKick.IsChecked == true)
                            {
                                CheckBox_RunAutoKick.IsChecked = false;
                                Globals.AutoKickBreakPlayer = false;
                            }
                        });

                        isHasBeenExec = true;
                    }
                }

                Thread.Sleep(1000);
            }
        }

        ////////////////////////////////////////////////////////////////////////////////

        private void AutoKickLifeBreakPlayer()
        {
            List<PlayerData> players = new();
            while (true)
            {
                // 自动踢出违规玩家
                if (Globals.AutoKickBreakPlayer)
                {
                    if (!Globals.IsGameRun || !Globals.IsToolInit)
                    {
                        Globals.AutoKickBreakPlayer = false;
                        Dispatcher.Invoke(() =>
                        {
                            CheckBox_RunAutoKick.IsChecked = false;
                        });
                    }

                    lock (ScoreView.PlayerDatas_Team1)
                    {
                        players.AddRange(ScoreView.PlayerDatas_Team1.Values);
                    }
                    lock (ScoreView.PlayerDatas_Team2)
                    {
                        players.AddRange(ScoreView.PlayerDatas_Team2.Values);
                    }

                    foreach (var item in players)
                    {
                        CheckBreakLifePlayer(item);
                    }
                }

                Thread.Sleep(5000);
            }
        }

        private async void CheckBreakLifePlayer(PlayerData data)
        {
            // 跳过管理员
            if (Globals.Server_AdminList.Contains(data.PersonaId))
                return;

            // 跳过白名单玩家
            if (Globals.NowRule.Custom_WhiteList.Contains(data.Name))
                return;

            //已经不在服务器了
            if (!ScoreView.PlayerDatas_Team1.ContainsKey(data.PersonaId) && !ScoreView.PlayerDatas_Team2.ContainsKey(data.PersonaId))
                return;

            var resultTemp = await BF1API.API.ServerAPI.GetCareerForOwnedGamesByPersonaId(data.PersonaId.ToString());

            if (resultTemp.IsSuccess)
            {
                var career = JsonUtil.JsonDese<CareerForOwnedGamesByPersonaId>(resultTemp.Message);

                // 拿到该玩家的生涯数据
                int kills = career.result.gameStats.tunguska.kills;
                int deaths = career.result.gameStats.tunguska.deaths;

                float kd = (float)Math.Round((double)kills / deaths, 2);
                float kpm = career.result.gameStats.tunguska.kpm;

                int weaponStar = (int)career.result.gameStats.tunguska.highlightsByType.weapon[0].highlightDetails.stats.values.kills;
                int vehicleStar = (int)career.result.gameStats.tunguska.highlightsByType.vehicle[0].highlightDetails.stats.values.kills;

                weaponStar = weaponStar / 100;
                vehicleStar = vehicleStar / 100;

                // 限制玩家生涯KD
                if (Globals.NowRule.LifeMaxKD != 0 && kd > Globals.NowRule.LifeMaxKD)
                {
                    AutoKickPlayer(new BreakRuleInfo
                    {
                        Name = data.Name,
                        PersonaId = data.PersonaId,
                        Reason = $"Life KD Limit {Globals.NowRule.LifeMaxKD:0.00}",
                        Type = BreakType.Life_KD_Limit
                    });

                    return;
                }

                // 限制玩家生涯KPM
                if (Globals.NowRule.LifeMaxKPM != 0 && kpm > Globals.NowRule.LifeMaxKPM)
                {
                    AutoKickPlayer(new BreakRuleInfo
                    {
                        Name = data.Name,
                        PersonaId = data.PersonaId,
                        Reason = $"Life KPM Limit {Globals.NowRule.LifeMaxKPM:0.00}",
                        Type = BreakType.Life_KPM_Limit
                    });

                    return;
                }

                // 限制玩家武器星级
                if (Globals.NowRule.LifeMaxWeaponStar != 0 && weaponStar > Globals.NowRule.LifeMaxWeaponStar)
                {
                    AutoKickPlayer(new BreakRuleInfo
                    {
                        Name = data.Name,
                        PersonaId = data.PersonaId,
                        Reason = $"Life Weapon Star Limit {Globals.NowRule.LifeMaxWeaponStar:0}",
                        Type = BreakType.Life_Weapon_Star_Limit
                    });

                    return;
                }

                // 限制玩家载具星级
                if (Globals.NowRule.LifeMaxVehicleStar != 0 && vehicleStar > Globals.NowRule.LifeMaxVehicleStar)
                {
                    AutoKickPlayer(new BreakRuleInfo
                    {
                        Name = data.Name,
                        PersonaId = data.PersonaId,
                        Reason = $"Life Vehicle Star Limit {Globals.NowRule.LifeMaxVehicleStar:0}",
                        Type = BreakType.Life_Vehicle_Star_Limit
                    });

                    return;
                }
            }
        }

        // 自动踢出违规玩家
        private async void AutoKickPlayer(BreakRuleInfo info)
        {
            var result = await BF1API.API.ServerAPI.AdminKickPlayer(info.PersonaId.ToString(), info.Reason);

            if (result.IsSuccess)
            {
                info.Status = "踢出成功";
                info.Time = DateTime.Now;
                LogView._dAddKickOKLog(info);
            }
            else
            {
                info.Status = "踢出失败 " + result.Message;
                info.Time = DateTime.Now;
                LogView._dAddKickNOLog(info);
            }
        }

        ////////////////////////////////////////////////////////////////////////////////

        private void Button_BreakWeaponInfo_Add_Click(object sender, RoutedEventArgs e)
        {
            AudioUtil.ClickSound();

            bool isContains = false;

            int index = ListBox_WeaponInfo.SelectedIndex;
            if (index != -1)
            {
                var wi = ListBox_WeaponInfo.SelectedItem as WeaponInfoModel;
                if (string.IsNullOrEmpty(wi.Chinese))
                {
                    MainWindow._SetOperatingState(2, "请不要把分类项添加到限制武器列表");
                    return;
                }

                foreach (WeaponInfoModel item in ListBox_BreakWeaponInfo.Items)
                {
                    if (wi.English == item.English)
                    {
                        isContains = true;
                        break;
                    }
                }

                foreach (var item in ListBox_BreakWeaponInfo.Items)
                {
                    if (ListBox_WeaponInfo.SelectedItem == item)
                    {
                        isContains = true;
                        break;
                    }
                }

                if (!isContains)
                {
                    ListBox_BreakWeaponInfo.Items.Add(ListBox_WeaponInfo.SelectedItem);
                    (ListBox_WeaponInfo.Items[ListBox_WeaponInfo.SelectedIndex] as WeaponInfoModel).Mark = "✔";

                    ListBox_WeaponInfo.SelectedIndex = index;

                    int count = ListBox_BreakWeaponInfo.Items.Count;
                    if (count != 0)
                    {
                        ListBox_BreakWeaponInfo.SelectedIndex = count - 1;
                    }

                    MainWindow._SetOperatingState(1, "添加限制武器成功");
                }
                else
                {
                    MainWindow._SetOperatingState(2, "当前限制武器已存在，请不要重复添加");
                }
            }
            else
            {
                MainWindow._SetOperatingState(2, "请选择正确的内容");
            }
        }

        private void Button_BreakWeaponInfo_Remove_Click(object sender, RoutedEventArgs e)
        {
            AudioUtil.ClickSound();

            int index1 = ListBox_WeaponInfo.SelectedIndex;
            int index2 = ListBox_BreakWeaponInfo.SelectedIndex;
            if (index2 != -1)
            {
                var bwi = ListBox_BreakWeaponInfo.SelectedItem as WeaponInfoModel;
                foreach (WeaponInfoModel item in ListBox_WeaponInfo.Items)
                {
                    if (item.English == bwi.English)
                    {
                        item.Mark = "";
                    }
                }

                ListBox_BreakWeaponInfo.Items.RemoveAt(ListBox_BreakWeaponInfo.SelectedIndex);

                int count = ListBox_BreakWeaponInfo.Items.Count;
                if (count != 0)
                {
                    ListBox_BreakWeaponInfo.SelectedIndex = count - 1;
                }

                ListBox_WeaponInfo.SelectedIndex = index1;

                MainWindow._SetOperatingState(1, "移除限制武器成功");
            }
            else
            {
                MainWindow._SetOperatingState(2, "请选择正确的内容");
            }
        }

        private void Button_BreakWeaponInfo_Clear_Click(object sender, RoutedEventArgs e)
        {
            AudioUtil.ClickSound();

            int index = ListBox_WeaponInfo.SelectedIndex;

            // 清空限制武器列表
            Globals.NowRule.Custom_WeaponList.Clear();
            ListBox_BreakWeaponInfo.Items.Clear();

            foreach (WeaponInfoModel item in ListBox_WeaponInfo.Items)
            {
                item.Mark = "";
            }

            ListBox_WeaponInfo.SelectedIndex = index;

            MainWindow._SetOperatingState(1, "清空限制武器列表成功");
        }

        private void AppendLog(string msg)
        {
            TextBox_RuleLog.AppendText(msg + "\n");
        }

        private void Add_Rule(object sender, RoutedEventArgs e) 
        {
            var name = new InputWindow("新的规则名字", "请输入新的规则名字", "").Set().Trim();
            if (string.IsNullOrWhiteSpace(name))
                return;

            if(Globals.Rules.ContainsKey(name))
            {
                MessageBox.Show("改名字已被占用");
                return;
            }

            var rule = new ServerRule()
            {
                Name = name
            };

            Globals.Rules.Add(name.ToLower(), rule);
            Rule_List.Items.Add(rule);
            FileUtil.SaveRule(rule);

            Rule_List.SelectedItem = null;
        }

        private void Load_Rule(object sender, RoutedEventArgs e)
        {
            var item = Rule_List.SelectedItem as ServerRule;

            if (item == null)
                return;

            Globals.NowRule = item;
            LoadRule();
            isApplyRule = false;
            Rule_List.SelectedItem = null;

            TextBox_RuleLog.Clear();

            AppendLog("已切换规则");
        }

        private void Delete_Rule(object sender, RoutedEventArgs e)
        {
            var item = Rule_List.SelectedItem as ServerRule;

            if (item == null)
                return;

            if (item.Name is "Default")
            {
                MessageBox.Show(messageBoxText: "不能删除默认规则");
                return;
            }

            if (Globals.NowRule == item)
            {
                Globals.NowRule = Globals.Rules["default"];
                LoadRule();
            }

            var name = item.Name.ToLower().Trim();

            Globals.Rules.Remove(name);
            Rule_List.Items.Remove(item);
            FileUtil.DeleteRule(name);

            Rule_List.SelectedItem = null;
        }

        private void Button_ApplyRule_Click(object sender, RoutedEventArgs e)
        {
            AudioUtil.ClickSound();

            TextBox_RuleLog.Clear();

            AppendLog("===== 操作时间 =====");
            AppendLog("");
            AppendLog($"{DateTime.Now:yyyy/MM/dd HH:mm:ss}");
            AppendLog("");

            Globals.NowRule.MaxKill = (int)Slider_MaxKill.Value;

            Globals.NowRule.KDFlag = (int)Slider_KDFlag.Value;
            Globals.NowRule.MaxKD = (float)Slider_MaxKD.Value;

            Globals.NowRule.KPMFlag = (int)Slider_KPMFlag.Value;
            Globals.NowRule.MaxKPM = (float)Slider_MaxKPM.Value;

            Globals.NowRule.MinRank = (int)Slider_MinRank.Value;
            Globals.NowRule.MaxRank = (int)Slider_MaxRank.Value;

            Globals.NowRule.LifeMaxKD = (float)Slider_LifeMaxKD.Value;
            Globals.NowRule.LifeMaxKPM = (float)Slider_LifeMaxKPM.Value;

            Globals.NowRule.LifeMaxWeaponStar = Convert.ToInt32(Slider_LifeMaxWeaponStar.Value);
            Globals.NowRule.LifeMaxVehicleStar = Convert.ToInt32(Slider_LifeMaxVehicleStar.Value);

            if (Globals.NowRule.MinRank >= Globals.NowRule.MaxRank && Globals.NowRule.MinRank != 0 && Globals.NowRule.MaxRank != 0)
            {
                Globals.IsRuleSetRight = false;
                isApplyRule = false;

                AppendLog($"限制等级规则设置不正确");
                AppendLog("");

                MainWindow._SetOperatingState(3, $"限制等级规则设置不正确");

                return;
            }

            /////////////////////////////////////////////////////////////////////////////

            // 清空限制武器列表
            Globals.NowRule.Custom_WeaponList.Clear();
            // 添加自定义限制武器
            foreach (var item in ListBox_BreakWeaponInfo.Items)
            {
                Globals.NowRule.Custom_WeaponList.Add((item as WeaponInfoModel).English);
            }

            // 清空黑名单列表
            Globals.NowRule.Custom_BlackList.Clear();
            // 添加自定义黑名单列表
            foreach (var item in ListBox_Custom_BlackList.Items)
            {
                Globals.NowRule.Custom_BlackList.Add(item as string);
            }

            // 清空白名单列表
            Globals.NowRule.Custom_WhiteList.Clear();
            // 添加自定义白名单列表
            foreach (var item in ListBox_Custom_WhiteList.Items)
            {
                Globals.NowRule.Custom_WhiteList.Add(item as string);
            }

            if (CheckBox_RunAutoKick.IsChecked == true)
            {
                CheckBox_RunAutoKick.IsChecked = false;
                Globals.AutoKickBreakPlayer = false;
            }

            Globals.IsRuleSetRight = true;
            isApplyRule = true;

            AppendLog($"成功提交当前规则，请重新启动自动踢人功能");
            AppendLog("");

            MainWindow._SetOperatingState(1, $"应用当前规则成功，请点击<查询当前规则>检验规则是否正确");

            FileUtil.SaveRule();
        }

        private void Button_QueryRule_Click(object sender, RoutedEventArgs e)
        {
            AudioUtil.ClickSound();

            TextBox_RuleLog.Clear();

            AppendLog("===== 查询时间 =====");
            AppendLog("");
            AppendLog($"{DateTime.Now:yyyy/MM/dd HH:mm:ss}");
            AppendLog("");

            AppendLog($"规则名字 : {Globals.NowRule.Name}");
            AppendLog("");

            AppendLog($"玩家最高击杀限制 : {Globals.NowRule.MaxKill}");
            AppendLog("");

            AppendLog($"计算玩家KD的最低击杀数 : {Globals.NowRule.KDFlag}");
            AppendLog($"玩家最高KD限制 : {Globals.NowRule.MaxKD}");
            AppendLog("");

            AppendLog($"计算玩家KPM的最低击杀数 : {Globals.NowRule.KPMFlag}");
            AppendLog($"玩家最高KPM限制 : {Globals.NowRule.MaxKPM}");
            AppendLog("");

            AppendLog($"玩家最低等级限制 : {Globals.NowRule.MinRank}");
            AppendLog($"玩家最高等级限制 : {Globals.NowRule.MaxRank}");
            AppendLog("");

            AppendLog($"玩家最高生涯KD限制 : {Globals.NowRule.LifeMaxKD}");
            AppendLog($"玩家最高生涯KPM限制 : {Globals.NowRule.LifeMaxKPM}");
            AppendLog("");

            AppendLog($"玩家最高生涯武器星数限制 : {Globals.NowRule.LifeMaxWeaponStar}");
            AppendLog($"玩家最高生涯载具星数限制 : {Globals.NowRule.LifeMaxVehicleStar}");
            AppendLog("\n");

            AppendLog($"========== 禁武器列表 ==========");
            AppendLog("");
            foreach (var item in Globals.NowRule.Custom_WeaponList)
            {
                AppendLog($"武器名称 {Globals.NowRule.Custom_WeaponList.IndexOf(item) + 1} : {item}");
            }
            AppendLog("\n");

            AppendLog($"========== 黑名单列表 ==========");
            AppendLog("");
            foreach (var item in Globals.NowRule.Custom_BlackList)
            {
                AppendLog($"玩家ID {Globals.NowRule.Custom_BlackList.IndexOf(item) + 1} : {item}");
            }
            AppendLog("\n");

            AppendLog($"========== 白名单列表 ==========");
            AppendLog("");
            foreach (var item in Globals.NowRule.Custom_WhiteList)
            {
                AppendLog($"玩家ID {Globals.NowRule.Custom_WhiteList.IndexOf(item) + 1} : {item}");
            }
            AppendLog("\n");

            MainWindow._SetOperatingState(1, $"查询当前规则成功，请点击<检查违规玩家>测试是否正确");
        }

        private void Button_CheckBreakRulePlayer_Click(object sender, RoutedEventArgs e)
        {
            AudioUtil.ClickSound();

            TextBox_RuleLog.Clear();

            AppendLog("===== 查询时间 =====");
            AppendLog("");
            AppendLog($"{DateTime.Now:yyyy/MM/dd HH:mm:ss}");
            AppendLog("");

            int index = 1;
            AppendLog($"========== 违规类型 : 限制玩家最高击杀 ==========");
            AppendLog("");
            var list = Globals.BreakRuleInfo_PlayerList.Values.Where(item => item.Type is BreakType.Kill_Limit);
            foreach (var item in list)
            {
                AppendLog($"玩家ID {index++} : {item.Name}");
            }
            AppendLog("\n");

            index = 1;
            AppendLog($"========== 违规类型 : 限制玩家最高KD ==========");
            AppendLog("");
            list = Globals.BreakRuleInfo_PlayerList.Values.Where(item => item.Type is BreakType.KD_Limit);
            foreach (var item in list)
            {
                AppendLog($"玩家ID {index++} : {item.Name}");
            }
            AppendLog("\n");

            index = 1;
            AppendLog($"========== 违规类型 : 限制玩家最高KPM ==========");
            AppendLog("");
            list = Globals.BreakRuleInfo_PlayerList.Values.Where(item => item.Type is BreakType.KPM_Limit);
            foreach (var item in list)
            {
                AppendLog($"玩家ID {index++} : {item.Name}");
            }
            AppendLog("\n");

            index = 1;
            AppendLog($"========== 违规类型 : 限制玩家等级范围 ==========");
            AppendLog("");
            list = Globals.BreakRuleInfo_PlayerList.Values.Where(item => item.Type is BreakType.Rank_Limit);
            foreach (var item in list)
            {
                AppendLog($"玩家ID {index++} : {item.Name}");
            }
            AppendLog("\n");

            index = 1;
            AppendLog($"========== 违规类型 : 限制玩家使用武器 ==========");
            AppendLog("");
            list = Globals.BreakRuleInfo_PlayerList.Values.Where(item => item.Type is BreakType.Weapon_Limit);
            foreach (var item in list)
            {
                AppendLog($"玩家ID {index++} : {item.Name}");
            }
            AppendLog("\n");

            MainWindow._SetOperatingState(1, $"检查违规玩家成功，如果符合规则就可以勾选<激活自动踢出违规玩家>了");
        }

        private void Button_Add_BlackList_Click(object sender, RoutedEventArgs e)
        {
            AudioUtil.ClickSound();

            if (TextBox_BlackList_PlayerName.Text != "")
            {
                bool isContains = false;

                foreach (var item in ListBox_Custom_BlackList.Items)
                {
                    if ((item as string) == TextBox_BlackList_PlayerName.Text)
                    {
                        isContains = true;
                    }
                }

                if (!isContains)
                {
                    ListBox_Custom_BlackList.Items.Add(TextBox_BlackList_PlayerName.Text);

                    MainWindow._SetOperatingState(1, $"添加 {TextBox_BlackList_PlayerName.Text} 到黑名单列表成功");
                    TextBox_BlackList_PlayerName.Text = "";
                }
                else
                {
                    MainWindow._SetOperatingState(2, $"该项 {TextBox_BlackList_PlayerName.Text} 已经存在了，请不要重复添加");
                    TextBox_BlackList_PlayerName.Text = "";
                }
            }
            else
            {
                MainWindow._SetOperatingState(2, $"待添加黑名单玩家ID为空，添加操作取消");
            }
        }

        private void Button_Remove_BlackList_Click(object sender, RoutedEventArgs e)
        {
            AudioUtil.ClickSound();

            if (ListBox_Custom_BlackList.SelectedIndex != -1)
            {
                MainWindow._SetOperatingState(1, $"从黑名单列表删除（{ListBox_Custom_BlackList.SelectedItem}）成功");
                ListBox_Custom_BlackList.Items.Remove(ListBox_Custom_BlackList.SelectedItem);
            }
            else
            {
                MainWindow._SetOperatingState(2, $"请正确选中你要删除的玩家ID或自定义黑名单列表为空，删除操作取消");
            }
        }

        private void Button_Input_BlackList_Click(object sender, RoutedEventArgs e)
        {
            AudioUtil.ClickSound();

            var res = FileSelectUtil.FileSelect();
            if (res == null)
            {
                return;
            }

            try
            {
                var data = File.ReadAllText(res);

                // 清空黑名单列表
                Globals.NowRule.Custom_BlackList.Clear();
                ListBox_Custom_BlackList.Items.Clear();

                var list = data.Split("\n");
                foreach (var item in list)
                {
                    var name = item.Trim();
                    if (string.IsNullOrWhiteSpace(name))
                        continue;

                    Globals.NowRule.Custom_BlackList.Add(name);
                    ListBox_Custom_BlackList.Items.Add(name);
                }

                MainWindow._SetOperatingState(1, "导入黑名单列表成功");
            }
            catch (Exception ex)
            {
                MsgBoxUtil.ErrorMsgBox("导入黑名单时发生错误");
                LoggerHelper.Error("导入黑名单发生错误", ex);
                MainWindow._SetOperatingState(1, "导入黑名单列表发生错误");
                return;
            }
        }

        private void Button_Clear_BlackList_Click(object sender, RoutedEventArgs e)
        {
            AudioUtil.ClickSound();

            // 清空黑名单列表
            Globals.NowRule.Custom_BlackList.Clear();
            ListBox_Custom_BlackList.Items.Clear();

            MainWindow._SetOperatingState(1, $"清空黑名单列表成功");
        }

        private void Button_Add_WhiteList_Click(object sender, RoutedEventArgs e)
        {
            AudioUtil.ClickSound();

            if (TextBox_WhiteList_PlayerName.Text != "")
            {
                bool isContains = false;

                foreach (var item in ListBox_Custom_WhiteList.Items)
                {
                    if ((item as string) == TextBox_WhiteList_PlayerName.Text)
                    {
                        isContains = true;
                    }
                }

                if (!isContains)
                {
                    ListBox_Custom_WhiteList.Items.Add(TextBox_WhiteList_PlayerName.Text);

                    MainWindow._SetOperatingState(1, $"添加 {TextBox_WhiteList_PlayerName.Text} 到白名单列表成功");

                    TextBox_WhiteList_PlayerName.Text = "";
                }
                else
                {
                    MainWindow._SetOperatingState(2, $"该项 {TextBox_WhiteList_PlayerName.Text} 已经存在了，请不要重复添加");
                    TextBox_WhiteList_PlayerName.Text = "";
                }
            }
            else
            {
                MainWindow._SetOperatingState(2, $"待添加白名单玩家ID为空，添加操作取消");
            }
        }

        private void Button_Remove_WhiteList_Click(object sender, RoutedEventArgs e)
        {
            AudioUtil.ClickSound();

            if (ListBox_Custom_WhiteList.SelectedIndex != -1)
            {
                MainWindow._SetOperatingState(1, $"从白名单列表删除（{ListBox_Custom_WhiteList.SelectedItem}）成功");
                ListBox_Custom_WhiteList.Items.Remove(ListBox_Custom_WhiteList.SelectedItem);
            }
            else
            {
                MainWindow._SetOperatingState(2, $"请正确选中你要删除的玩家ID或自定义白名单列表为空，删除操作取消");
            }
        }

        private void Button_Input_WhiteList_Click(object sender, RoutedEventArgs e)
        {
            AudioUtil.ClickSound();

            var res = FileSelectUtil.FileSelect();
            if (res == null)
            {
                return;
            }

            try
            {
                var data = File.ReadAllText(res);

                // 清空黑名单列表
                Globals.NowRule.Custom_WhiteList.Clear();
                ListBox_Custom_WhiteList.Items.Clear();

                var list = data.Split("\n");
                foreach (var item in list)
                {
                    var name = item.Trim();
                    if (string.IsNullOrWhiteSpace(name))
                        continue;

                    Globals.NowRule.Custom_WhiteList.Add(name);
                    ListBox_Custom_WhiteList.Items.Add(name);
                }

                MainWindow._SetOperatingState(1, "导入白名单列表成功");
            }
            catch (Exception ex)
            {
                MsgBoxUtil.ErrorMsgBox("导入白名单时发生错误");
                LoggerHelper.Error("导入白名单发生错误", ex);
                MainWindow._SetOperatingState(1, "导入白名单列表发生错误");
                return;
            }
        }

        private void Button_Clear_WhiteList_Click(object sender, RoutedEventArgs e)
        {
            AudioUtil.ClickSound();

            // 清空白名单列表
            Globals.NowRule.Custom_WhiteList.Clear();
            ListBox_Custom_WhiteList.Items.Clear();

            MainWindow._SetOperatingState(1, $"清空白名单列表成功");
        }

        /// <summary>
        /// 检查自动踢人环境是否合格
        /// </summary>
        /// <returns></returns>
        private async Task<bool> CheckKickEnv()
        {
            TextBox_RuleLog.Clear();

            MainWindow._SetOperatingState(2, $"正在检查环境...");

            AppendLog("===== 操作时间 =====");
            AppendLog("");
            AppendLog($"{DateTime.Now:yyyy/MM/dd HH:mm:ss}");

            AppendLog("");
            AppendLog("正在检查游戏是否启动...");
            if (!Globals.IsGameRun)
            {
                if (!ProcessUtil.IsAppRun(CoreUtil.TargetAppName))
                {
                    AppendLog("游戏没有启动");
                    return false;
                }
            }
            AppendLog("游戏已启动");

            AppendLog("");
            AppendLog("正在检查游戏是否启动...");
            if (!Globals.IsToolInit)
            {
                if (!Memory.Initialize(CoreUtil.TargetAppName))
                {
                    AppendLog("战地1内存模块初始化失败");
                    return false;
                }
            }
            AppendLog("战地1内存模块初始化完成");

            AppendLog("");
            AppendLog("正在检查玩家是否应用规则...");
            if (!isApplyRule)
            {
                AppendLog("玩家没有正确应用规则，操作取消");
                MainWindow._SetOperatingState(2, $"环境检查未通过，操作取消");
                return false;
            }
            AppendLog("玩家已正确应用规则");

            AppendLog("");
            AppendLog("正在检查 SessionId 是否正确...");
            if (string.IsNullOrEmpty(Globals.Config.SessionId))
            {
                AppendLog("SessionId为空，操作取消");
                MainWindow._SetOperatingState(2, $"环境检查未通过，操作取消");
                return false;
            }
            AppendLog("SessionId 检查正确");

            AppendLog("");
            AppendLog("正在检查 SessionId 是否有效...");
            var result = await BF1API.API.ServerAPI.GetWelcomeMessage();
            if (!result.IsSuccess)
            {
                AppendLog("SessionId 已过期，请重新获取，操作取消");
                MainWindow._SetOperatingState(2, $"环境检查未通过，操作取消");
                return false;
            }
            AppendLog("SessionId 检查有效，可以使用");

            AppendLog("");
            AppendLog("正在检查 GameId 是否正确...");
            if (string.IsNullOrEmpty(Globals.Config.GameId))
            {
                AppendLog("GameId 为空，操作取消");
                MainWindow._SetOperatingState(2, $"环境检查未通过，操作取消");
                return false;
            }
            AppendLog("GameId检查正确");

            AppendLog("");
            AppendLog("正在检查 服务器管理员列表 是否正确...");
            if (Globals.Server_AdminList.Count == 0)
            {
                await DetailView.SLoad();
                if (Globals.Server_AdminList.Count == 0)
                {
                    AppendLog("服务器管理员列表 为空，请先获取当前服务器详情数据，操作取消");
                    MainWindow._SetOperatingState(2, $"环境检查未通过，操作取消");
                    return false;
                }
            }
            AppendLog("服务器管理员列表 检查正确");

            AppendLog("");
            AppendLog("正在检查 玩家是否为当前服务器管理...");
            var welcomeMsg = JsonUtil.JsonDese<WelcomeMsg>(result.Message);
            var firstMessage = welcomeMsg.result.firstMessage;
            string playerName = firstMessage.Substring(0, firstMessage.IndexOf("，"));
            if (!Globals.Server_Admin2List.Contains(playerName))
            {
                AppendLog("玩家不是当前服务器管理，操作取消");
                MainWindow._SetOperatingState(2, $"环境检查未通过，操作取消");
                return false;
            }
            AppendLog("已确认玩家为当前服务器管理");

            return true;
        }

        // 开启自动踢人
        private async void CheckBox_RunAutoKick_Click(object sender, RoutedEventArgs e)
        {
            if (CheckBox_RunAutoKick.IsChecked == true)
            {
                // 检查自动踢人环境
                if (await CheckKickEnv())
                {
                    AppendLog("");
                    AppendLog("环境检查完毕，自动踢人已开启");

                    isHasBeenExec = false;

                    Globals.AutoKickBreakPlayer = true;
                    MainWindow._SetOperatingState(1, $"自动踢人开启成功");
                }
                else
                {
                    CheckBox_RunAutoKick.IsChecked = false;
                    Globals.AutoKickBreakPlayer = false;
                }
            }
            else
            {
                Globals.AutoKickBreakPlayer = false;
                MainWindow._SetOperatingState(1, $"自动踢人关闭成功");
            }
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            ProcessUtil.OpenLink(e.Uri.OriginalString);
            e.Handled = true;
        }

        private void Button_OpenConfigurationFolder_Click(object sender, RoutedEventArgs e)
        {
            AudioUtil.ClickSound();

            ProcessUtil.OpenLink(FileUtil.Base);
        }

        /// <summary>
        /// 手动T人
        /// </summary>
        /// <param name="info"></param>
        private async void ManualKickPlayer(BreakRuleInfo info)
        {
            // 跳过管理员
            if (!Globals.Server_AdminList.Contains(info.PersonaId))
            {
                // 白名单玩家不踢出
                if (!Globals.NowRule.Custom_WhiteList.Contains(info.Name))
                {
                    var result = await BF1API.API.ServerAPI.AdminKickPlayer(info.PersonaId.ToString(), info.Reason);

                    if (result.IsSuccess)
                    {
                        info.Status = "踢出成功";
                        LogView._dAddKickOKLog(info);
                    }
                    else
                    {
                        info.Status = "踢出失败 " + result.Message;
                        LogView._dAddKickNOLog(info);
                    }
                }
            }
        }

        private async void Button_ManualKickBreakRulePlayer_Click(object sender, RoutedEventArgs e)
        {
            AudioUtil.ClickSound();

            // 检查自动踢人环境
            if (await CheckKickEnv())
            {
                AppendLog("");
                AppendLog("环境检查完毕，执行手动踢人操作成功，请查看日志了解执行结果");

                for (int i = 0; i < Globals.BreakRuleInfo_PlayerList.Count; i++)
                {
                    ManualKickPlayer(Globals.BreakRuleInfo_PlayerList[i]);
                }

                var team1Player = new List<PlayerData>();

                lock (ScoreView.PlayerDatas_Team1) 
                {
                    team1Player.AddRange(ScoreView.PlayerDatas_Team1.Values);
                }
                lock (ScoreView.PlayerDatas_Team2) 
                {
                    team1Player.AddRange(ScoreView.PlayerDatas_Team2.Values);
                }

                foreach (var item in team1Player)
                {
                    CheckBreakLifePlayer(item);
                }

                MainWindow._SetOperatingState(1, "执行手动踢人操作成功，请查看日志了解执行结果");
            }
        }
    }
}
