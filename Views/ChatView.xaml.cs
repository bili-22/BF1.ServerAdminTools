using BF1.ServerAdminTools.Common.Utils;
using BF1.ServerAdminTools.Features.Chat;
using BF1.ServerAdminTools.Features.Core;
using Chinese;

namespace BF1.ServerAdminTools.Views
{
    /// <summary>
    /// ChatView.xaml 的交互逻辑
    /// </summary>
    public partial class ChatView : UserControl
    {
        public ChatView()
        {
            InitializeComponent();
        }

        #region 工具类
        private void KeyPress(WinVK winVK)
        {
            Thread.Sleep(20);
            WinAPI.Keybd_Event(winVK, WinAPI.MapVirtualKey(winVK, 0), 0, 0);
            Thread.Sleep(20);
            WinAPI.Keybd_Event(winVK, WinAPI.MapVirtualKey(winVK, 0), 2, 0);
        }

        private string ToDBC(string input)
        {
            char[] c = input.ToCharArray();

            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }

                if (c[i] > 65280 && c[i] < 65375)
                {
                    c[i] = (char)(c[i] - 65248);
                }
            }

            return new string(c);
        }
        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ChatMsg.GetAllocateMemoryAddress() != 0)
            {
                Memory.SetForegroundWindow();

                Thread.Sleep(20);

                string msg = "中文聊天测试";
                msg = ToDBC(msg);
                msg = ChineseConverter.ToTraditional(msg);

                Memory.WriteStringUTF8(ChatMsg.GetAllocateMemoryAddress(), null, msg);

                KeyPress(WinVK.J);

                if (ChatMsg.GetChatIsOpen())
                {
                    if (ChatMsg.ChatMessagePointer() != 0)
                    {
                        var startPtr = ChatMsg.ChatMessagePointer() + ChatMsg.OFFSET_CHAT_MESSAGE_START;
                        var endPtr = ChatMsg.ChatMessagePointer() + ChatMsg.OFFSET_CHAT_MESSAGE_END;

                        var oldStartPtr = Memory.Read<long>(startPtr);
                        var oldEndPtr = Memory.Read<long>(endPtr);

                        Memory.Write<long>(startPtr, ChatMsg.GetAllocateMemoryAddress());
                        Memory.Write<long>(endPtr, ChatMsg.GetAllocateMemoryAddress() + 18);

                        KeyPress(WinVK.RETURN);

                        Memory.Write<long>(startPtr, oldStartPtr);
                        Memory.Write<long>(endPtr, oldEndPtr);
                    }
                    else
                    {
                        MsgBoxUtil.InformationMsgBox("聊天框指针为0");
                    }
                }
                else
                {
                    MsgBoxUtil.InformationMsgBox("聊天框未开启");
                }
            }
        }
    }
}
