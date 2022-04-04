using BF1.ServerAdminTools.BF1API.Core;
using BF1.ServerAdminTools.Common.Utils;

namespace BF1.ServerAdminTools.BF1API.Chat;

internal static class ChatHelper
{
    /// <summary>
    /// 按键间隔延迟，单位：毫秒
    /// </summary>
    public static int KeyPressDelay = 50;

    public static void KeyPress(WinVK winVK, int delay)
    {
        Thread.Sleep(delay);
        WinAPI.Keybd_Event(winVK, WinAPI.MapVirtualKey(winVK, 0), 0, 0);
        Thread.Sleep(delay);
        WinAPI.Keybd_Event(winVK, WinAPI.MapVirtualKey(winVK, 0), 2, 0);
        Thread.Sleep(delay);
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

    public static int GetStrLength(string str)
    {
        if (string.IsNullOrEmpty(str))
            return 0;

        ASCIIEncoding ascii = new();
        int tempLen = 0;
        byte[] s = ascii.GetBytes(str);
        for (int i = 0; i < s.Length; i++)
        {
            if ((int)s[i] == 63)
            {
                tempLen += 3;
            }
            else
            {
                tempLen += 1;
            }
        }

        return tempLen;
    }

    // 发送中文到战地1聊天框
    public static void SendText2Bf1Game(string msg)
    {
        // 如果内容为空，则跳过
        if (string.IsNullOrEmpty(msg))
            return;

        // 将窗口置顶
        MemoryHook.SetForegroundWindow();
        Thread.Sleep(KeyPressDelay);

        // 如果聊天框开启，让他关闭
        if (ChatMsg.GetChatIsOpen())
            KeyPress(WinVK.RETURN, KeyPressDelay);

        // 模拟按键，开启聊天框
        KeyPress(WinVK.J, KeyPressDelay);

        if (ChatMsg.GetChatIsOpen())
        {
            if (ChatMsg.ChatMessagePointer() != 0)
            {
                // 挂起战地1进程
                NtProc.SuspendProcess(MemoryHook.GetProcessId());

                msg = ChsUtil.ToTraditionalChinese(ToDBC(msg).Trim());
                var length = GetStrLength(msg);
                MemoryHook.WriteStringUTF8(ChatMsg.GetAllocateMemoryAddress(), null, msg);

                var startPtr = ChatMsg.ChatMessagePointer() + ChatMsg.OFFSET_CHAT_MESSAGE_START;
                var endPtr = ChatMsg.ChatMessagePointer() + ChatMsg.OFFSET_CHAT_MESSAGE_END;

                var oldStartPtr = MemoryHook.Read<long>(startPtr);
                var oldEndPtr = MemoryHook.Read<long>(endPtr);

                MemoryHook.Write<long>(startPtr, ChatMsg.GetAllocateMemoryAddress());
                MemoryHook.Write<long>(endPtr, ChatMsg.GetAllocateMemoryAddress() + length);

                // 恢复战地1进程
                NtProc.ResumeProcess(MemoryHook.GetProcessId());
                KeyPress(WinVK.RETURN, KeyPressDelay);

                // 挂起战地1进程
                NtProc.SuspendProcess(MemoryHook.GetProcessId());
                MemoryHook.Write<long>(startPtr, oldStartPtr);
                MemoryHook.Write<long>(endPtr, oldEndPtr);
                // 恢复战地1进程
                NtProc.ResumeProcess(MemoryHook.GetProcessId());
            }
        }
    }
}

public partial class Core
{
    public void SendText(string data)
        => ChatHelper.SendText2Bf1Game(data);
}