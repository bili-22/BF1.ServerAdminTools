using BF1.ServerAdminTools.Common.Helper;
using BF1.ServerAdminTools.Common.Utils;
using BF1.ServerAdminTools.Features.Chat;
using BF1.ServerAdminTools.Features.Core;
using BF1.ServerAdminTools.Features.Utils;
using Chinese;

namespace BF1.ServerAdminTools.Views
{
    /// <summary>
    /// ChatView.xaml 的交互逻辑
    /// </summary>
    public partial class ChatView : UserControl
    {
        private string[] defaultMsg = new string[10];

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
        }

        private void MainWindow_ClosingDisposeEvent()
        {
            int index = ComboBox_DefaultText.SelectedIndex;
            defaultMsg[index] = TextBox_InputMsg.Text;

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

                string msg = TextBox_InputMsg.Text.Trim();
                msg = ChineseConverter.ToTraditional(ChatHelper.ToDBC(msg));

                Memory.WriteStringUTF8(ChatMsg.GetAllocateMemoryAddress(), null, msg);

                ChatHelper.KeyPress(WinVK.J);

                if (ChatMsg.GetChatIsOpen())
                {
                    if (ChatMsg.ChatMessagePointer() != 0)
                    {
                        var startPtr = ChatMsg.ChatMessagePointer() + ChatMsg.OFFSET_CHAT_MESSAGE_START;
                        var endPtr = ChatMsg.ChatMessagePointer() + ChatMsg.OFFSET_CHAT_MESSAGE_END;

                        var oldStartPtr = Memory.Read<long>(startPtr);
                        var oldEndPtr = Memory.Read<long>(endPtr);

                        Memory.Write<long>(startPtr, ChatMsg.GetAllocateMemoryAddress());
                        Memory.Write<long>(endPtr, ChatMsg.GetAllocateMemoryAddress() + PlayerUtil.GetStrLength(msg));

                        ChatHelper.KeyPress(WinVK.RETURN);

                        Memory.Write<long>(startPtr, oldStartPtr);
                        Memory.Write<long>(endPtr, oldEndPtr);

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

            if (ComboBox_DefaultText != null)
            {
                int index = ComboBox_DefaultText.SelectedIndex;
                defaultMsg[index] = TextBox_InputMsg.Text;
            }
        }

        private void ComboBox_DefaultText_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ComboBox_DefaultText.SelectedIndex;
            TextBox_InputMsg.Text = defaultMsg[index];
        }
    }
}
