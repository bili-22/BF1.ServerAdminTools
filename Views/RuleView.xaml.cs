using BF1.ServerAdminTools.Common.Data;
using BF1.ServerAdminTools.Common.Utils;
using BF1.ServerAdminTools.Common.Helper;
using BF1.ServerAdminTools.Features.Data;
using BF1.ServerAdminTools.Features.Utils;
using BF1.ServerAdminTools.Features.API;
using BF1.ServerAdminTools.Features.API.RespJson;

namespace BF1.ServerAdminTools.Views
{
    /// <summary>
    /// RuleView.xaml 的交互逻辑
    /// </summary>
    public partial class RuleView : UserControl
    {
        private class WeaponInfo
        {
            public string English { get; set; }
            public string Chinese { get; set; }
            public string Mark { get; set; }
        }

        // 已经踢出的玩家列表，保留指定时间秒数
        private List<BreakRuleInfo> Kicked_PlayerList = new List<BreakRuleInfo>();

        public RuleView()
        {
            InitializeComponent();

            // 添加武器信息列表
            foreach (var item in WeaponData.AllWeaponInfo)
            {
                ListBox_WeaponInfo.Items.Add(new WeaponInfo()
                {
                    English = item.English,
                    Chinese = item.Chinese,
                    Mark = ""
                });
            }
            ListBox_WeaponInfo.SelectedIndex = 0;

            var thread0 = new Thread(AutoKickBreakPlayer);
            thread0.IsBackground = true;
            thread0.Start();

            string temp = string.Empty;
            temp = IniHelper.ReadString("Rules", "MaxKill", "0", FileUtil.F_Settings_Path);
            if (!string.IsNullOrEmpty(temp))
                Slider_MaxKill.Value = Convert.ToInt32(temp);
            temp = IniHelper.ReadString("Rules", "KDFlag", "0", FileUtil.F_Settings_Path);
            if (!string.IsNullOrEmpty(temp))
                Slider_KDFlag.Value = Convert.ToInt32(temp);
            temp = IniHelper.ReadString("Rules", "MaxKD", "0.00", FileUtil.F_Settings_Path);
            if (!string.IsNullOrEmpty(temp))
                Slider_MaxKD.Value = Convert.ToDouble(temp);
            temp = IniHelper.ReadString("Rules", "KPMFlag", "0", FileUtil.F_Settings_Path);
            if (!string.IsNullOrEmpty(temp))
                Slider_KPMFlag.Value = Convert.ToInt32(temp);
            temp = IniHelper.ReadString("Rules", "MaxKPM", "0.00", FileUtil.F_Settings_Path);
            if (!string.IsNullOrEmpty(temp))
                Slider_MaxKPM.Value = Convert.ToDouble(temp);
            temp = IniHelper.ReadString("Rules", "MinRank", "0", FileUtil.F_Settings_Path);
            if (!string.IsNullOrEmpty(temp))
                Slider_MinRank.Value = Convert.ToInt32(temp);
            temp = IniHelper.ReadString("Rules", "MaxRank", "0", FileUtil.F_Settings_Path);
            if (!string.IsNullOrEmpty(temp))
                Slider_MaxRank.Value = Convert.ToInt32(temp);

            if (File.Exists(FileUtil.F_WeaponList_Path))
            {
                using (StreamReader file = new StreamReader(FileUtil.F_WeaponList_Path, Encoding.Default))
                {
                    string s = "";
                    while (s != null)
                    {
                        s = file.ReadLine();
                        if (!string.IsNullOrEmpty(s))
                        {
                            ListBox_BreakWeaponInfo.Items.Add(new WeaponInfo()
                            {
                                English = s,
                                Chinese = PlayerUtil.GetWeaponChsName(s)
                            });
                        }
                    }
                }
            }

            if (ListBox_BreakWeaponInfo.Items.Count != 0)
            {
                ListBox_BreakWeaponInfo.SelectedIndex = 0;
            }

            if (File.Exists(FileUtil.F_BlackList_Path))
            {
                using (StreamReader file = new StreamReader(FileUtil.F_BlackList_Path, Encoding.Default))
                {
                    string s = "";
                    while (s != null)
                    {
                        s = file.ReadLine();
                        if (!string.IsNullOrEmpty(s))
                        {
                            ListBox_Custom_BlackList.Items.Add(s);
                        }
                    }
                }
            }

            if (File.Exists(FileUtil.F_WhiteList_Path))
            {
                using (StreamReader file = new StreamReader(FileUtil.F_WhiteList_Path, Encoding.Default))
                {
                    string s = "";
                    while (s != null)
                    {
                        s = file.ReadLine();
                        if (!string.IsNullOrEmpty(s))
                        {
                            ListBox_Custom_WhiteList.Items.Add(s);
                        }
                    }
                }
            }

            MainWindow.ClosingDisposeEvent += MainWindow_ClosingDisposeEvent;

            for (int i = 0; i < ListBox_BreakWeaponInfo.Items.Count; i++)
            {
                var bwi = ListBox_BreakWeaponInfo.Items[i] as WeaponInfo;
                for (int j = 0; j < ListBox_WeaponInfo.Items.Count; j++)
                {
                    var wi = ListBox_WeaponInfo.Items[j] as WeaponInfo;
                    if (bwi.English == wi.English)
                    {
                        ListBox_WeaponInfo.Items[j] = new WeaponInfo()
                        {
                            English = wi.English,
                            Chinese = wi.Chinese,
                            Mark = "✔"
                        };
                    }
                }
            }
        }

        private void MainWindow_ClosingDisposeEvent()
        {
            IniHelper.WriteString("Rules", "MaxKill", Slider_MaxKill.Value.ToString("0"), FileUtil.F_Settings_Path);
            IniHelper.WriteString("Rules", "KDFlag", Slider_KDFlag.Value.ToString("0"), FileUtil.F_Settings_Path);
            IniHelper.WriteString("Rules", "MaxKD", Slider_MaxKD.Value.ToString("0.00"), FileUtil.F_Settings_Path);
            IniHelper.WriteString("Rules", "KPMFlag", Slider_KPMFlag.Value.ToString("0"), FileUtil.F_Settings_Path);
            IniHelper.WriteString("Rules", "MaxKPM", Slider_MaxKPM.Value.ToString("0.00"), FileUtil.F_Settings_Path);
            IniHelper.WriteString("Rules", "MinRank", Slider_MinRank.Value.ToString("0"), FileUtil.F_Settings_Path);
            IniHelper.WriteString("Rules", "MaxRank", Slider_MaxRank.Value.ToString("0"), FileUtil.F_Settings_Path);

            if (File.Exists(FileUtil.F_WeaponList_Path))
            {
                using (StreamWriter file = new StreamWriter(FileUtil.F_WeaponList_Path, false))
                {
                    foreach (WeaponInfo item in ListBox_BreakWeaponInfo.Items)
                    {
                        file.WriteLine(item.English);
                    }
                }
            }

            if (File.Exists(FileUtil.F_BlackList_Path))
            {
                using (StreamWriter file = new StreamWriter(FileUtil.F_BlackList_Path, false))
                {
                    foreach (var item in ListBox_Custom_BlackList.Items)
                    {
                        file.WriteLine(item);
                    }
                }
            }

            if (File.Exists(FileUtil.F_WhiteList_Path))
            {
                using (StreamWriter file = new StreamWriter(FileUtil.F_WhiteList_Path, false))
                {
                    foreach (var item in ListBox_Custom_WhiteList.Items)
                    {
                        file.WriteLine(item);
                    }
                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////////

        private void AutoKickBreakPlayer()
        {
            while (true)
            {
                // 自动踢出违规玩家
                if (Globals.AutoKickBreakPlayer)
                {
                    var temp = Globals.BreakRuleInfo_PlayerList;
                    for (int i = 0; i < temp.Count; i++)
                    {
                        // 先检查踢出玩家是否在已踢出列表中
                        int index = Kicked_PlayerList.FindIndex(var => var.PersonaId == temp[i].PersonaId);
                        if (index == -1)
                        {
                            AutoKickPlayer(temp[i]);
                        }
                    }

                    for (int i = 0; i < Kicked_PlayerList.Count; i++)
                    {
                        // 如果超过10秒，移除该玩家
                        if (CoreUtil.DiffSeconds(Kicked_PlayerList[i].Time, DateTime.Now) > 10)
                        {
                            Kicked_PlayerList.RemoveAt(i);
                        }
                    }
                }

                Thread.Sleep(3000);
            }
        }

        // 自动踢出违规玩家
        private async void AutoKickPlayer(BreakRuleInfo info)
        {
            // 跳过管理员
            if (!Globals.Server_AdminList.Contains(info.PersonaId.ToString()))
            {
                // 白名单玩家不踢出
                if (!Globals.Custom_WhiteList.Contains(info.Name))
                {
                    // 先检查踢出玩家是否在已踢出列表中
                    int index = Kicked_PlayerList.FindIndex(var => var.PersonaId == info.PersonaId);
                    if (index == -1)
                    {
                        var result = await BF1API.AdminKickPlayer(info.PersonaId.ToString(), info.Reason);

                        if (result.IsSuccess)
                        {
                            info.Status = "踢出成功";
                            info.Time = DateTime.Now;
                            LogView.dAddKickLog1(info);
                            Kicked_PlayerList.Add(info);
                        }
                        else
                        {
                            info.Status = "踢出失败 " + result.Message;
                            LogView.dAddKickLog2(info);
                        }
                    }
                }
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
                var wi = ListBox_WeaponInfo.SelectedItem as WeaponInfo;
                if (string.IsNullOrEmpty(wi.Chinese))
                {
                    MainWindow.dSetOperatingState(2, "请不要把分类项添加到限制武器列表");
                    return;
                }

                for (int i = 0; i < ListBox_BreakWeaponInfo.Items.Count; i++)
                {
                    var bwi = ListBox_BreakWeaponInfo.SelectedItem as WeaponInfo;
                    if (wi.English == bwi.English)
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
                    ListBox_WeaponInfo.Items[ListBox_WeaponInfo.SelectedIndex] = new WeaponInfo()
                    {
                        English = wi.English,
                        Chinese = wi.Chinese,
                        Mark = "✔"
                    };

                    ListBox_WeaponInfo.SelectedIndex = index;

                    int count = ListBox_BreakWeaponInfo.Items.Count;
                    if (count != 0)
                    {
                        ListBox_BreakWeaponInfo.SelectedIndex = count - 1;
                    }

                    MainWindow.dSetOperatingState(1, "添加限制武器成功");
                }
                else
                {
                    MainWindow.dSetOperatingState(2, "当前限制武器已存在，请不要重复添加");
                }
            }
            else
            {
                MainWindow.dSetOperatingState(2, "请选择正确的内容");
            }
        }

        private void Button_BreakWeaponInfo_Remove_Click(object sender, RoutedEventArgs e)
        {
            AudioUtil.ClickSound();

            int index1 = ListBox_WeaponInfo.SelectedIndex;
            int index2 = ListBox_BreakWeaponInfo.SelectedIndex;
            if (index2 != -1)
            {
                var bwi = ListBox_BreakWeaponInfo.SelectedItem as WeaponInfo;
                for (int i = 0; i < ListBox_WeaponInfo.Items.Count; i++)
                {
                    var wi = ListBox_WeaponInfo.Items[i] as WeaponInfo;
                    if (bwi.English == wi.English)
                    {
                        ListBox_WeaponInfo.Items[i] = new WeaponInfo()
                        {
                            English = bwi.English,
                            Chinese = bwi.Chinese,
                            Mark = ""
                        };
                    }
                }

                ListBox_BreakWeaponInfo.Items.RemoveAt(ListBox_BreakWeaponInfo.SelectedIndex);

                int count = ListBox_BreakWeaponInfo.Items.Count;
                if (count != 0)
                {
                    ListBox_BreakWeaponInfo.SelectedIndex = count - 1;
                }

                ListBox_WeaponInfo.SelectedIndex = index1;

                MainWindow.dSetOperatingState(1, "移除限制武器成功");
            }
            else
            {
                MainWindow.dSetOperatingState(2, "请选择正确的内容");
            }
        }

        private void Button_BreakWeaponInfo_Clear_Click(object sender, RoutedEventArgs e)
        {
            AudioUtil.ClickSound();

            int index = ListBox_WeaponInfo.SelectedIndex;

            // 清空限制武器列表
            Globals.Custom_WeaponList.Clear();
            ListBox_BreakWeaponInfo.Items.Clear();

            for (int i = 0; i < ListBox_WeaponInfo.Items.Count; i++)
            {
                var wi = ListBox_WeaponInfo.Items[i] as WeaponInfo;
                ListBox_WeaponInfo.Items[i] = new WeaponInfo()
                {
                    English = wi.English,
                    Chinese = wi.Chinese,
                    Mark = ""
                };
            }

            ListBox_WeaponInfo.SelectedIndex = index;

            MainWindow.dSetOperatingState(1, "清空限制武器列表成功");
        }

        private void AppendLog(string msg)
        {
            TextBox_RuleLog.AppendText(msg + "\n");
        }

        private void Button_ApplyRule_Click(object sender, RoutedEventArgs e)
        {
            AudioUtil.ClickSound();

            ServerRule.MaxKill = Convert.ToInt32(Slider_MaxKill.Value);

            ServerRule.KDFlag = Convert.ToInt32(Slider_KDFlag.Value);
            ServerRule.MaxKD = Convert.ToSingle(Slider_MaxKD.Value);

            ServerRule.KPMFlag = Convert.ToInt32(Slider_KPMFlag.Value);
            ServerRule.MaxKPM = Convert.ToSingle(Slider_MaxKPM.Value);

            ServerRule.MinRank = Convert.ToInt32(Slider_MinRank.Value);
            ServerRule.MaxRank = Convert.ToInt32(Slider_MaxRank.Value);

            if (ServerRule.MinRank >= ServerRule.MaxRank && ServerRule.MinRank != 0 && ServerRule.MaxRank != 0)
            {
                Globals.IsRuleSetRight = false;
                MainWindow.dSetOperatingState(3, $"限制等级规则设置不正确");

                return;
            }

            /////////////////////////////////////////////////////////////////////////////

            // 清空限制武器列表
            Globals.Custom_WeaponList.Clear();
            // 添加自定义限制武器
            foreach (var item in ListBox_BreakWeaponInfo.Items)
            {
                Globals.Custom_WeaponList.Add((item as WeaponInfo).English);
            }

            // 清空黑名单列表
            Globals.Custom_BlackList.Clear();
            // 添加自定义黑名单列表
            foreach (var item in ListBox_Custom_BlackList.Items)
            {
                Globals.Custom_BlackList.Add(PlayerUtil.GetNameNoMark(item as string));
            }

            // 清空白名单列表
            Globals.Custom_WhiteList.Clear();
            // 添加自定义白名单列表
            foreach (var item in ListBox_Custom_WhiteList.Items)
            {
                Globals.Custom_WhiteList.Add(PlayerUtil.GetNameNoMark(item as string));
            }

            Globals.IsRuleSetRight = true;
            MainWindow.dSetOperatingState(1, $"应用当前规则成功，请点击<查询当前规则>检验规则是否正确");
        }

        private void Button_QueryRule_Click(object sender, RoutedEventArgs e)
        {
            AudioUtil.ClickSound();

            TextBox_RuleLog.Clear();

            AppendLog("===== 查询时间 =====");
            AppendLog("");
            AppendLog($"{DateTime.Now:yyyy/MM/dd HH:mm:ss}");
            AppendLog("");

            AppendLog($"玩家最高击杀限制 : {ServerRule.MaxKill}");
            AppendLog("");

            AppendLog($"计算玩家KD的最低击杀数 : {ServerRule.KDFlag}");
            AppendLog($"玩家最高KD限制 : {ServerRule.MaxKD}");
            AppendLog("");

            AppendLog($"计算玩家KPM的最低击杀数 : {ServerRule.KPMFlag}");
            AppendLog($"玩家最高KPM限制 : {ServerRule.MaxKPM}");
            AppendLog("");

            AppendLog($"玩家最低等级限制 : {ServerRule.MinRank}");
            AppendLog($"玩家最高等级限制 : {ServerRule.MaxRank}");
            AppendLog("\n");

            AppendLog($"========== 禁武器列表 ==========");
            AppendLog("");
            foreach (var item in Globals.Custom_WeaponList)
            {
                AppendLog($"武器名称 {Globals.Custom_WeaponList.IndexOf(item) + 1} : {item}");
            }
            AppendLog("\n");

            AppendLog($"========== 黑名单列表 ==========");
            AppendLog("");
            foreach (var item in Globals.Custom_BlackList)
            {
                AppendLog($"玩家ID {Globals.Custom_BlackList.IndexOf(item) + 1} : {item}");
            }
            AppendLog("\n");

            AppendLog($"========== 白名单列表 ==========");
            AppendLog("");
            foreach (var item in Globals.Custom_WhiteList)
            {
                AppendLog($"玩家ID {Globals.Custom_WhiteList.IndexOf(item) + 1} : {item}");
            }
            AppendLog("\n");

            MainWindow.dSetOperatingState(1, $"查询当前规则成功，请点击<检查违规玩家>测试是否正确");
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
            foreach (var item in Globals.BreakRuleInfo_PlayerList)
            {
                if (item.Reason.Contains("Kill Limit"))
                {
                    AppendLog($"玩家ID {index++} : {item.Name}");
                }
            }
            AppendLog("\n");

            index = 1;
            AppendLog($"========== 违规类型 : 限制玩家最高KD ==========");
            AppendLog("");
            foreach (var item in Globals.BreakRuleInfo_PlayerList)
            {
                if (item.Reason.Contains("KD Limit"))
                {
                    AppendLog($"玩家ID {index++} : {item.Name}");
                }
            }
            AppendLog("\n");

            index = 1;
            AppendLog($"========== 违规类型 : 限制玩家最高KPM ==========");
            AppendLog("");
            foreach (var item in Globals.BreakRuleInfo_PlayerList)
            {
                if (item.Reason.Contains("KPM Limit"))
                {
                    AppendLog($"玩家ID {index++} : {item.Name}");
                }
            }
            AppendLog("\n");

            index = 1;
            AppendLog($"========== 违规类型 : 限制玩家等级范围 ==========");
            AppendLog("");
            foreach (var item in Globals.BreakRuleInfo_PlayerList)
            {
                if (item.Reason.Contains("Rank Limit"))
                {
                    AppendLog($"玩家ID {index++} : {item.Name}");
                }
            }
            AppendLog("\n");

            index = 1;
            AppendLog($"========== 违规类型 : 限制玩家使用武器 ==========");
            AppendLog("");
            foreach (var item in Globals.BreakRuleInfo_PlayerList)
            {
                if (item.Reason.Contains("Weapon Limit"))
                {
                    AppendLog($"玩家ID {index++} : {item.Name}");
                }
            }
            AppendLog("\n");

            MainWindow.dSetOperatingState(1, $"检查违规玩家成功，如果符合规则就可以勾选<激活自动踢出违规玩家>了");
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

                    MainWindow.dSetOperatingState(1, $"添加 {TextBox_BlackList_PlayerName.Text} 到黑名单列表成功");
                    TextBox_BlackList_PlayerName.Text = "";
                }
                else
                {
                    MainWindow.dSetOperatingState(2, $"该项 {TextBox_BlackList_PlayerName.Text} 已经存在了，请不要重复添加");
                    TextBox_BlackList_PlayerName.Text = "";
                }
            }
            else
            {
                MainWindow.dSetOperatingState(2, $"待添加黑名单玩家ID为空，添加操作取消");
            }
        }

        private void Button_Remove_BlackList_Click(object sender, RoutedEventArgs e)
        {
            AudioUtil.ClickSound();

            if (ListBox_Custom_BlackList.SelectedIndex != -1)
            {
                MainWindow.dSetOperatingState(1, $"从黑名单列表删除（{ListBox_Custom_BlackList.SelectedItem}）成功");
                ListBox_Custom_BlackList.Items.Remove(ListBox_Custom_BlackList.SelectedItem);
            }
            else
            {
                MainWindow.dSetOperatingState(2, $"请正确选中你要删除的玩家ID或自定义黑名单列表为空，删除操作取消");
            }
        }

        private void Button_Clear_BlackList_Click(object sender, RoutedEventArgs e)
        {
            AudioUtil.ClickSound();

            // 清空黑名单列表
            Globals.Custom_BlackList.Clear();
            ListBox_Custom_BlackList.Items.Clear();

            MainWindow.dSetOperatingState(1, $"清空黑名单列表成功");
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

                    MainWindow.dSetOperatingState(1, $"添加 {TextBox_WhiteList_PlayerName.Text} 到白名单列表成功");

                    TextBox_WhiteList_PlayerName.Text = "";
                }
                else
                {
                    MainWindow.dSetOperatingState(2, $"该项 {TextBox_WhiteList_PlayerName.Text} 已经存在了，请不要重复添加");
                    TextBox_WhiteList_PlayerName.Text = "";
                }
            }
            else
            {
                MainWindow.dSetOperatingState(2, $"待添加白名单玩家ID为空，添加操作取消");
            }
        }

        private void Button_Remove_WhiteList_Click(object sender, RoutedEventArgs e)
        {
            AudioUtil.ClickSound();

            if (ListBox_Custom_WhiteList.SelectedIndex != -1)
            {
                MainWindow.dSetOperatingState(1, $"从白名单列表删除（{ListBox_Custom_WhiteList.SelectedItem}）成功");
                ListBox_Custom_WhiteList.Items.Remove(ListBox_Custom_WhiteList.SelectedItem);
            }
            else
            {
                MainWindow.dSetOperatingState(2, $"请正确选中你要删除的玩家ID或自定义白名单列表为空，删除操作取消");
            }
        }

        private void Button_Clear_WhiteList_Click(object sender, RoutedEventArgs e)
        {
            AudioUtil.ClickSound();

            // 清空白名单列表
            Globals.Custom_WhiteList.Clear();
            ListBox_Custom_WhiteList.Items.Clear();

            MainWindow.dSetOperatingState(1, $"清空白名单列表成功");
        }

        // 开启自动踢人
        private async void CheckBox_RunAutoKick_Click(object sender, RoutedEventArgs e)
        {
            if (CheckBox_RunAutoKick.IsChecked == true)
            {
                TextBox_RuleLog.Clear();

                MainWindow.dSetOperatingState(2, $"正在检查环境...");

                AppendLog("===== 操作时间 =====");
                AppendLog("");
                AppendLog($"{DateTime.Now:yyyy/MM/dd HH:mm:ss}");

                AppendLog("");
                AppendLog("正在检查 SessionId 是否正确...");
                if (string.IsNullOrEmpty(Globals.SessionId))
                {
                    AppendLog("SessionId为空，操作取消");
                    CheckBox_RunAutoKick.IsChecked = false;
                    MainWindow.dSetOperatingState(2, $"环境检查未通过，操作取消");
                    return;
                }
                else
                {
                    AppendLog("SessionId 检查正确");
                }

                AppendLog("");
                AppendLog("正在检查 SessionId 是否有效...");
                var result = await BF1API.GetWelcomeMessage();
                if (!result.IsSuccess)
                {
                    AppendLog("SessionId 已过期，请重新获取，操作取消");
                    CheckBox_RunAutoKick.IsChecked = false;
                    MainWindow.dSetOperatingState(2, $"环境检查未通过，操作取消");
                    return;
                }
                else
                {
                    AppendLog("SessionId 检查有效，可以使用");
                }

                AppendLog("");
                AppendLog("正在检查 GameId 是否正确...");
                if (string.IsNullOrEmpty(Globals.GameId))
                {
                    AppendLog("GameId 为空，操作取消");
                    CheckBox_RunAutoKick.IsChecked = false;
                    MainWindow.dSetOperatingState(2, $"环境检查未通过，操作取消");
                    return;
                }
                else
                {
                    AppendLog("GameId检查正确");
                }

                AppendLog("");
                AppendLog("正在检查 服务器管理员列表 是否正确...");
                if (Globals.Server_AdminList.Count == 0)
                {
                    AppendLog("服务器管理员列表 为空，请先获取当前服务器详情数据，操作取消");
                    CheckBox_RunAutoKick.IsChecked = false;
                    MainWindow.dSetOperatingState(2, $"环境检查未通过，操作取消");
                    return;
                }
                else
                {
                    AppendLog("服务器管理员列表 检查正确");
                }

                AppendLog("");
                AppendLog("正在检查 玩家是否为当前服务器管理...");
                var welcomeMsg = JsonUtil.JsonDese<WelcomeMsg>(result.Message);
                var firstMessage = welcomeMsg.result.firstMessage;
                string playerName = firstMessage.Substring(0, firstMessage.IndexOf("，"));
                if (!Globals.Server_Admin2List.Contains(playerName))
                {
                    AppendLog("玩家不是当前服务器管理，操作取消");
                    CheckBox_RunAutoKick.IsChecked = false;
                    MainWindow.dSetOperatingState(2, $"环境检查未通过，操作取消");
                    return;
                }
                else
                {
                    AppendLog("已确认玩家为当前服务器管理");
                }

                AppendLog("");
                AppendLog("环境检查完毕，自动踢人已开启");

                Globals.AutoKickBreakPlayer = true;
                MainWindow.dSetOperatingState(1, $"自动踢人开启成功");
            }
            else
            {
                Globals.AutoKickBreakPlayer = false;
                MainWindow.dSetOperatingState(1, $"自动踢人关闭成功");
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

            ProcessUtil.OpenLink(FileUtil.Default_Path);
        }

        private async void ManualKickPlayer(BreakRuleInfo info)
        {
            // 跳过管理员
            if (!Globals.Server_AdminList.Contains(info.Name))
            {
                // 白名单玩家不踢出
                if (!Globals.Custom_WhiteList.Contains(info.Name))
                {
                    var result = await BF1API.AdminKickPlayer(info.PersonaId.ToString(), info.Reason);

                    if (result.IsSuccess)
                    {
                        info.Status = "踢出成功";
                        LogView.dAddKickLog1(info);
                    }
                    else
                    {
                        info.Status = "踢出失败 " + result.Message;
                        LogView.dAddKickLog2(info);
                    }
                }
            }
        }

        private void Button_ManualKickBreakRulePlayer_Click(object sender, RoutedEventArgs e)
        {
            AudioUtil.ClickSound();

            for (int i = 0; i < Globals.BreakRuleInfo_PlayerList.Count; i++)
            {
                ManualKickPlayer(Globals.BreakRuleInfo_PlayerList[i]);
            }

            MainWindow.dSetOperatingState(1, "执行手动踢人操作成功");
        }

        private async void Button_CheckKickEnv_Click(object sender, RoutedEventArgs e)
        {
            AudioUtil.ClickSound();

            TextBox_RuleLog.Clear();

            MainWindow.dSetOperatingState(2, $"正在检查环境...");

            AppendLog("===== 检查时间 =====");
            AppendLog("");
            AppendLog($"{DateTime.Now:yyyy/MM/dd HH:mm:ss}");

            AppendLog("");
            AppendLog("正在检查 SessionId 是否正确...");
            if (string.IsNullOrEmpty(Globals.SessionId))
            {
                AppendLog("SessionId为空，操作取消");
                MainWindow.dSetOperatingState(2, $"环境检查未通过，操作取消");
                return;
            }
            else
            {
                AppendLog("SessionId 检查正确");
            }

            AppendLog("");
            AppendLog("正在检查 SessionId 是否有效...");
            var result = await BF1API.GetWelcomeMessage();
            if (!result.IsSuccess)
            {
                AppendLog("SessionId 已过期，请重新获取，操作取消");
                MainWindow.dSetOperatingState(2, $"环境检查未通过，操作取消");
                return;
            }
            else
            {
                AppendLog("SessionId 检查有效，可以使用");
            }

            AppendLog("");
            AppendLog("正在检查 GameId 是否正确...");
            if (string.IsNullOrEmpty(Globals.GameId))
            {
                AppendLog("GameId 为空，操作取消");
                MainWindow.dSetOperatingState(2, $"环境检查未通过，操作取消");
                return;
            }
            else
            {
                AppendLog("GameId检查正确");
            }

            AppendLog("");
            AppendLog("正在检查 服务器管理员列表 是否正确...");
            if (Globals.Server_AdminList.Count == 0)
            {
                AppendLog("服务器管理员列表 为空，请先获取当前服务器详情数据，操作取消");
                MainWindow.dSetOperatingState(2, $"环境检查未通过，操作取消");
                return;
            }
            else
            {
                AppendLog("服务器管理员列表 检查正确");
            }

            AppendLog("");
            AppendLog("正在检查 玩家是否为当前服务器管理...");
            var welcomeMsg = JsonUtil.JsonDese<WelcomeMsg>(result.Message);
            var firstMessage = welcomeMsg.result.firstMessage;
            string playerName = firstMessage.Substring(0, firstMessage.IndexOf("，"));
            if (!Globals.Server_Admin2List.Contains(playerName))
            {
                AppendLog("玩家不是当前服务器管理，操作取消");
                MainWindow.dSetOperatingState(2, $"环境检查未通过，操作取消");
                return;
            }
            else
            {
                AppendLog("已确认玩家为当前服务器管理");
            }

            AppendLog("");
            AppendLog("环境检查完毕，自动踢人可以开启");

            MainWindow.dSetOperatingState(1, $"环境检查完毕，自动踢人可以开启");
        }
    }
}
