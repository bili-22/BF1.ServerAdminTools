using BF1.ServerAdminTools.Common.Utils;
using BF1.ServerAdminTools.Common.Helper;
using BF1.ServerAdminTools.Features.Chat;
using BF1.ServerAdminTools.Features.Core;
using BF1.ServerAdminTools.Features.Utils;
using Microsoft.Toolkit.Mvvm.Input;

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

        public RelayCommand SendChsMessageCommand { get; set; }

        public ChatView()
        {
            InitializeComponent();

            this.DataContext = this;

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
            timerNoAFK.Interval = 30000;
            timerNoAFK.Elapsed += TimerNoAFK_Elapsed;

            SendChsMessageCommand = new RelayCommand(SendChsMessage);
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

        private void SetIMEState()
        {
            // 设置输入法为英文
            Application.Current.Dispatcher.Invoke(() =>
            {
                InputLanguageManager.Current.CurrentInputLanguage = new CultureInfo("en-US");
            });
        }

        private void TimerAutoSendMsg_Elapsed(object sender, ElapsedEventArgs e)
        {
            SetIMEState();
            Thread.Sleep(50);

            for (int i = 0; i < queueMsg.Count; i++)
            {
                ChatHelper.SendText2Bf1Game(queueMsg[i]);
                Thread.Sleep(queueMsgSleep * 1000);
            }
        }

        private void TimerNoAFK_Elapsed(object sender, ElapsedEventArgs e)
        {
            SetIMEState();
            Thread.Sleep(50);

            Memory.SetForegroundWindow();
            Thread.Sleep(50);

            WinAPI.Keybd_Event(WinVK.TAB, WinAPI.MapVirtualKey(WinVK.TAB, 0), 0, 0);
            Thread.Sleep(3000);
            WinAPI.Keybd_Event(WinVK.TAB, WinAPI.MapVirtualKey(WinVK.TAB, 0), 2, 0);
            Thread.Sleep(50);
        }

        private void SendChsMessage()
        {
            AudioUtil.ClickSound();

            SetIMEState();
            Thread.Sleep(20);

            ChatHelper.KeyPressDelay = (int)Slider_KeyPressDelay.Value;

            if (string.IsNullOrEmpty(TextBox_InputMsg.Text.Trim()))
            {
                MainWindow._SetOperatingState(2, "聊天框内容为空，操作取消");
                return;
            }

            if (ChatMsg.GetAllocateMemoryAddress() != 0)
            {
                // 将窗口置顶
                Memory.SetForegroundWindow();
                Thread.Sleep(50);

                // 如果聊天框开启，让他关闭
                if (ChatMsg.GetChatIsOpen())
                    ChatHelper.KeyPress(WinVK.RETURN, ChatHelper.KeyPressDelay);

                // 模拟按键，开启聊天框
                ChatHelper.KeyPress(WinVK.J, ChatHelper.KeyPressDelay);

                if (ChatMsg.GetChatIsOpen())
                {
                    if (ChatMsg.ChatMessagePointer() != 0)
                    {
                        // 挂起战地1进程
                        NtProc.SuspendProcess(Memory.GetProcessId());

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
                        NtProc.ResumeProcess(Memory.GetProcessId());
                        ChatHelper.KeyPress(WinVK.RETURN, ChatHelper.KeyPressDelay);

                        // 挂起战地1进程
                        NtProc.SuspendProcess(Memory.GetProcessId());
                        Memory.Write<long>(startPtr, oldStartPtr);
                        Memory.Write<long>(endPtr, oldEndPtr);
                        // 恢复战地1进程
                        NtProc.ResumeProcess(Memory.GetProcessId());

                        MainWindow._SetOperatingState(1, "发送文本到战地1聊天框成功");
                    }
                    else
                    {
                        MainWindow._SetOperatingState(2, "聊天框消息指针未发现");
                    }
                }
                else
                {
                    MainWindow._SetOperatingState(2, "聊天框未开启");
                }
            }
            else
            {
                MainWindow._SetOperatingState(3, "聊天功能初始化失败，请重启程序");
            }
        }

        private void TextBox_InputMsg_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBlock_TxtLength.Text = $"当前文本长度 : {PlayerUtil.GetStrLength(TextBox_InputMsg.Text)} 字符";

            defaultMsg[RadioButtonWhoIsChecked()] = TextBox_InputMsg.Text;
        }

        private void RadioButton_DefaultText0_Click(object sender, RoutedEventArgs e)
        {
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

                ChatHelper.KeyPressDelay = (int)Slider_KeyPressDelay.Value;

                queueMsgSleep = (int)Slider_AutoSendMsgSleep.Value;

                timerAutoSendMsg.Interval = Slider_AutoSendMsg.Value * 1000 * 60;
                timerAutoSendMsg.Start();

                MainWindow._SetOperatingState(1, "已启用定时发送指定文本功能");
            }
            else
            {
                timerAutoSendMsg.Stop();
                MainWindow._SetOperatingState(1, "已关闭定时发送指定文本功能");
            }
        }

        private void CheckBox_ActiveNoAFK_Click(object sender, RoutedEventArgs e)
        {
            if (CheckBox_ActiveNoAFK.IsChecked == true)
            {
                timerNoAFK.Start();
                MainWindow._SetOperatingState(1, "已启用游戏内挂机防踢功能");
            }
            else
            {
                timerNoAFK.Stop();
                MainWindow._SetOperatingState(1, "已关闭游戏内挂机防踢功能");
            }
        }
    }
}
