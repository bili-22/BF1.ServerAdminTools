using BF1.ServerAdminTools.Common.Utils;
using BF1.ServerAdminTools.Features.Core;
using BF1.ServerAdminTools.Features.Utils;

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
            if (string.IsNullOrEmpty(msg))
                return;

            Memory.SetForegroundWindow();
            Thread.Sleep(20);

            KeyPress(WinVK.J);

            if (ChatMsg.GetChatIsOpen())
            {
                if (ChatMsg.ChatMessagePointer() != 0)
                {
                    // 挂起战地1进程
                    WinAPI.NtSuspendProcess(Memory.GetHandle());
                    msg = ChsUtil.ToTraditionalChinese(ToDBC(msg.Trim()));
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
                    KeyPress(WinVK.RETURN);

                    // 挂起战地1进程
                    WinAPI.NtSuspendProcess(Memory.GetHandle());
                    Memory.Write<long>(startPtr, oldStartPtr);
                    Memory.Write<long>(endPtr, oldEndPtr);
                    // 恢复战地1进程
                    WinAPI.NtResumeProcess(Memory.GetHandle());
                }
            }
        }
    }
}
