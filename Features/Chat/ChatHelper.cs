using BF1.ServerAdminTools.Features.Core;
using BF1.ServerAdminTools.Features.Utils;
using Chinese;

namespace BF1.ServerAdminTools.Features.Chat
{
    public class ChatHelper
    {
        public static void KeyPress(WinVK winVK)
        {
            WinAPI.Keybd_Event(winVK, WinAPI.MapVirtualKey(winVK, 0), 0, 0);
            Thread.Sleep(20);
            WinAPI.Keybd_Event(winVK, WinAPI.MapVirtualKey(winVK, 0), 2, 0);
            Thread.Sleep(20);
        }

        public static string ToDBC(string input)
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

        // 发送中文到战地1聊天框
        public static void SendText2Bf1Game(string msg)
        {
            Memory.SetForegroundWindow();
            Thread.Sleep(20);

            msg = ChineseConverter.ToTraditional(ToDBC(msg.Trim()));

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
                    Memory.Write<long>(endPtr, ChatMsg.GetAllocateMemoryAddress() + PlayerUtil.GetStrLength(msg));

                    KeyPress(WinVK.RETURN);

                    Memory.Write<long>(startPtr, oldStartPtr);
                    Memory.Write<long>(endPtr, oldEndPtr);
                }
            }
        }
    }
}
