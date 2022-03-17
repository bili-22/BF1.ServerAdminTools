using BF1.ServerAdminTools.Common.Utils;
using BF1.ServerAdminTools.Common.Helper;
using BF1.ServerAdminTools.Features.Chat;
using BF1.ServerAdminTools.Features.Core;
using BF1.ServerAdminTools.Features.Utils;

namespace BF1.ServerAdminTools.Views
{
    /// <summary>
    /// ChatView.xaml 的交互逻辑
    /// </summary>
    public partial class ChatView : UserControl
    {
        private string[] defaultMsg = new string[10];

        private Timer timerAutoSendMsg;
        private List<string> queueMsg;

        private int queueMsgSleep = 1;

        private Timer timerNoAFK;

        public ChatView()
        {
            InitializeComponent();

            defaultMsg[0] = IniHelper.ReadString("ChatMsg", "Msg0", "", FileUtil.F_Settings_Path);
            defaultMsg[1] = IniHelper.ReadString("ChatMsg", "Msg1", "", FileUtil.F_Settings_Path);
            defaultMsg[2] = IniHelper.ReadString("ChatMsg", "Msg2", "", FileUtil.F_Settings_Path);
            defaultMsg[3] = IniHelper.ReadString("ChatMsg", "Msg3", "", FileUtil.F_Settings_Path);
            defaultMsg[4] = IniHelper.ReadString("ChatMsg", "Msg4", "", FileUtil.F_Settings_Path);
            defaultMsg[5] = IniHelper.ReadString("ChatMsg", "Msg5", "", FileUtil.F_Settings_Path);
            defaultMsg[6] = IniHelper.ReadString("ChatMsg", "Msg6", "", FileUtil.F_Settings_Path);
            defaultMsg[7] = IniHelper.ReadString("ChatMsg", "Msg7", "", FileUtil.F_Settings_Path);
            defaultMsg[8] = IniHelper.ReadString("ChatMsg", "Msg8", "", FileUtil.F_Settings_Path);
            defaultMsg[9] = IniHelper.ReadString("ChatMsg", "Msg9", "", FileUtil.F_Settings_Path);

            if (string.IsNullOrEmpty(defaultMsg[0]))
            {
                defaultMsg[0] = "战地1中文输入测试，最大30个汉字";
            }

            TextBox_InputMsg.Text = defaultMsg[0];

            MainWindow.ClosingDisposeEvent += MainWindow_ClosingDisposeEvent;

            timerAutoSendMsg = new Timer();
            timerAutoSendMsg.AutoReset = true;
            timerAutoSendMsg.Elapsed += TimerAutoSendMsg_Elapsed;

            queueMsg = new List<string>();

            timerNoAFK = new Timer();
            timerNoAFK.AutoReset = true;
            timerNoAFK.Interval = 1000 * 30;
            timerNoAFK.Elapsed += TimerNoAFK_Elapsed;
        }

        private void MainWindow_ClosingDisposeEvent()
        {
            defaultMsg[RadioButtonWhoIsChecked()] = TextBox_InputMsg.Text;

            IniHelper.WriteString("ChatMsg", "Msg0", defaultMsg[0], FileUtil.F_Settings_Path);
            IniHelper.WriteString("ChatMsg", "Msg1", defaultMsg[1], FileUtil.F_Settings_Path);
            IniHelper.WriteString("ChatMsg", "Msg2", defaultMsg[2], FileUtil.F_Settings_Path);
            IniHelper.WriteString("ChatMsg", "Msg3", defaultMsg[3], FileUtil.F_Settings_Path);
            IniHelper.WriteString("ChatMsg", "Msg4", defaultMsg[4], FileUtil.F_Settings_Path);
            IniHelper.WriteString("ChatMsg", "Msg5", defaultMsg[5], FileUtil.F_Settings_Path);
            IniHelper.WriteString("ChatMsg", "Msg6", defaultMsg[6], FileUtil.F_Settings_Path);
            IniHelper.WriteString("ChatMsg", "Msg7", defaultMsg[7], FileUtil.F_Settings_Path);
            IniHelper.WriteString("ChatMsg", "Msg8", defaultMsg[8], FileUtil.F_Settings_Path);
            IniHelper.WriteString("ChatMsg", "Msg9", defaultMsg[9], FileUtil.F_Settings_Path);
        }

        private void TimerAutoSendMsg_Elapsed(object sender, ElapsedEventArgs e)
        {
            for (int i = 0; i < queueMsg.Count; i++)
            {
                ChatHelper.SendText2Bf1Game(queueMsg[i]);
                Thread.Sleep(queueMsgSleep * 1000);
            }
        }

        private void TimerNoAFK_Elapsed(object sender, ElapsedEventArgs e)
        {
            Memory.SetForegroundWindow();
            Thread.Sleep(20);

            WinAPI.Keybd_Event(WinVK.TAB, WinAPI.MapVirtualKey(WinVK.TAB, 0), 0, 0);
            Thread.Sleep(3000);
            WinAPI.Keybd_Event(WinVK.TAB, WinAPI.MapVirtualKey(WinVK.TAB, 0), 2, 0);
            Thread.Sleep(20);
        }

        private void Button_SendMsg2Bf1Game_Click(object sender, RoutedEventArgs e)
        {
            AudioUtil.ClickSound();

            if (string.IsNullOrEmpty(TextBox_InputMsg.Text.Trim()))
            {
                MainWindow.dSetOperatingState(2, "聊天框内容为空，操作取消");
                return;
            }

            if (ChatMsg.GetAllocateMemoryAddress() != 0)
            {
                Memory.SetForegroundWindow();
                Thread.Sleep(20);

                ChatHelper.KeyPress(WinVK.J);

                if (ChatMsg.GetChatIsOpen())
                {
                    if (ChatMsg.ChatMessagePointer() != 0)
                    {
                        // 挂起战地1进程
                        WinAPI.NtSuspendProcess(Memory.GetHandle());
                        string msg = TextBox_InputMsg.Text.Trim();
                        msg = ChsUtil.ToTraditionalChinese(ChatHelper.ToDBC(msg));
                        var length = PlayerUtil.GetStrLength(msg);
                        Memory.WriteStringUTF8(ChatMsg.GetAllocateMemoryAddress(), null, msg);

                        var startPtr = ChatMsg.ChatMessagePointer() + ChatMsg.OFFSET_CHAT_MESSAGE_START;
                        var endPtr = ChatMsg.ChatMessagePointer() + ChatMsg.OFFSET_CHAT_MESSAGE_END;

                        var oldStartPtr = Memory.Read<long>(startPtr);
                        var oldEndPtr = Memory.Read<long>(endPtr);

                        Memory.Write<long>(startPtr, ChatMsg.GetAllocateMemoryAddress());
                        Memory.Write<long>(endPtr, ChatMsg.GetAllocateMemoryAddress() + length);

                        // 恢复战地1进程
                        WinAPI.NtResumeProcess(Memory.GetHandle());
                        ChatHelper.KeyPress(WinVK.RETURN);

                        // 挂起战地1进程
                        WinAPI.NtSuspendProcess(Memory.GetHandle());
                        Memory.Write<long>(startPtr, oldStartPtr);
                        Memory.Write<long>(endPtr, oldEndPtr);
                        // 恢复战地1进程
                        WinAPI.NtResumeProcess(Memory.GetHandle());

                        MainWindow.dSetOperatingState(1, "发送文本到战地1聊天框成功");
                    }
                    else
                    {
                        MainWindow.dSetOperatingState(2, "聊天框消息指针未发现");
                    }
                }
                else
                {
                    MainWindow.dSetOperatingState(2, "聊天框未开启");
                }
            }
            else
            {
                MainWindow.dSetOperatingState(3, "聊天功能初始化失败，请重启程序");
            }
        }

        private void TextBox_InputMsg_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBlock_TxtLength.Text = $"当前文本长度 : {PlayerUtil.GetStrLength(TextBox_InputMsg.Text)} 字符";

            defaultMsg[RadioButtonWhoIsChecked()] = TextBox_InputMsg.Text;
        }

        private void RadioButton_DefaultText0_Click(object sender, RoutedEventArgs e)
        {
            AudioUtil.ClickSound();

            TextBox_InputMsg.Text = defaultMsg[RadioButtonWhoIsChecked()];
        }

        private int RadioButtonWhoIsChecked()
        {
            if (RadioButton_DefaultText0 != null && RadioButton_DefaultText0.IsChecked == true)
                return 0;

            if (RadioButton_DefaultText1 != null && RadioButton_DefaultText1.IsChecked == true)
                return 1;

            if (RadioButton_DefaultText2 != null && RadioButton_DefaultText2.IsChecked == true)
                return 2;

            if (RadioButton_DefaultText3 != null && RadioButton_DefaultText3.IsChecked == true)
                return 3;

            if (RadioButton_DefaultText4 != null && RadioButton_DefaultText4.IsChecked == true)
                return 4;

            if (RadioButton_DefaultText5 != null && RadioButton_DefaultText5.IsChecked == true)
                return 5;

            if (RadioButton_DefaultText6 != null && RadioButton_DefaultText6.IsChecked == true)
                return 6;

            if (RadioButton_DefaultText7 != null && RadioButton_DefaultText7.IsChecked == true)
                return 7;

            if (RadioButton_DefaultText8 != null && RadioButton_DefaultText8.IsChecked == true)
                return 8;

            if (RadioButton_DefaultText9 != null && RadioButton_DefaultText9.IsChecked == true)
                return 9;

            return 0;
        }

        private void CheckBox_ActiveAutoSendMsg_Click(object sender, RoutedEventArgs e)
        {
            if (CheckBox_ActiveAutoSendMsg.IsChecked == true)
            {
                queueMsg.Clear();

                if (CheckBox_DefaultText0 != null && CheckBox_DefaultText0.IsChecked == true)
                    queueMsg.Add(defaultMsg[0]);

                if (CheckBox_DefaultText1 != null && CheckBox_DefaultText1.IsChecked == true)
                    queueMsg.Add(defaultMsg[1]);

                if (CheckBox_DefaultText2 != null && CheckBox_DefaultText2.IsChecked == true)
                    queueMsg.Add(defaultMsg[2]);

                if (CheckBox_DefaultText3 != null && CheckBox_DefaultText3.IsChecked == true)
                    queueMsg.Add(defaultMsg[3]);

                if (CheckBox_DefaultText4 != null && CheckBox_DefaultText4.IsChecked == true)
                    queueMsg.Add(defaultMsg[4]);

                if (CheckBox_DefaultText5 != null && CheckBox_DefaultText5.IsChecked == true)
                    queueMsg.Add(defaultMsg[5]);

                if (CheckBox_DefaultText6 != null && CheckBox_DefaultText6.IsChecked == true)
                    queueMsg.Add(defaultMsg[6]);

                if (CheckBox_DefaultText7 != null && CheckBox_DefaultText7.IsChecked == true)
                    queueMsg.Add(defaultMsg[7]);

                if (CheckBox_DefaultText8 != null && CheckBox_DefaultText8.IsChecked == true)
                    queueMsg.Add(defaultMsg[8]);

                if (CheckBox_DefaultText9 != null && CheckBox_DefaultText9.IsChecked == true)
                    queueMsg.Add(defaultMsg[9]);

                queueMsgSleep = (int)Slider_AutoSendMsgSleep.Value;

                timerAutoSendMsg.Interval = Slider_AutoSendMsg.Value * 1000 * 60;
                timerAutoSendMsg.Start();

                MainWindow.dSetOperatingState(1, "已启用定时发送指定文本功能");
            }
            else
            {
                timerAutoSendMsg.Stop();
                MainWindow.dSetOperatingState(1, "已关闭定时发送指定文本功能");
            }
        }

        private void CheckBox_ActiveNoAFK_Click(object sender, RoutedEventArgs e)
        {
            if (CheckBox_ActiveNoAFK.IsChecked == true)
            {
                timerNoAFK.Start();
                MainWindow.dSetOperatingState(1, "已启用游戏内挂机防踢功能");
            }
            else
            {
                timerNoAFK.Stop();
                MainWindow.dSetOperatingState(1, "已关闭游戏内挂机防踢功能");
            }
        }
    }
}
