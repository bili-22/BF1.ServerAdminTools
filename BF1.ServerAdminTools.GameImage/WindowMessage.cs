using BF1.ServerAdminTools.Common;
using BF1.ServerAdminTools.Common.Hook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BF1.ServerAdminTools.GameImage;

public static class WindowMessage
{
    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);

    [StructLayout(LayoutKind.Sequential)]
    private struct RECT
    {
        public int Left;                             //最左坐标
        public int Top;                             //最上坐标
        public int Right;                           //最右坐标
        public int Bottom;                        //最下坐标
    }

    [DllImport("user32")]
    private static extern int mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);
    //移动鼠标 
    private const int MOUSEEVENTF_MOVE = 0x0001;
    //模拟鼠标左键按下 
    private const int MOUSEEVENTF_LEFTDOWN = 0x0002;
    //模拟鼠标左键抬起 
    private const int MOUSEEVENTF_LEFTUP = 0x0004;
    //模拟鼠标右键按下 
    private const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
    //模拟鼠标右键抬起 
    private const int MOUSEEVENTF_RIGHTUP = 0x0010;
    //模拟鼠标中键按下 
    private const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;
    //模拟鼠标中键抬起 
    private const int MOUSEEVENTF_MIDDLEUP = 0x0040;
    //标示是否采用绝对坐标 
    private const int MOUSEEVENTF_ABSOLUTE = 0x8000;
    [DllImport("user32.dll")]
    private static extern bool SetCursorPos(int X, int Y);

    private static void Click(int x, int y)
    {
        if (!Globals.IsToolInit)
            return;
        Core.SetForegroundWindow();
        RECT rc = new();
        GetWindowRect(Core.GetWindowHandle(), ref rc);
        SetCursorPos(x + rc.Left, y + rc.Top);
        mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
        mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
    }

    public static void ToMain() 
    {
        Core.SetForegroundWindow();
        for (int a = 0; a < 5; a++)
        {
            WinAPI.Keybd_Event(WinVK.ESCAPE, WinAPI.MapVirtualKey(WinVK.ESCAPE, 0), 0, 0);
            Thread.Sleep(100);
            WinAPI.Keybd_Event(WinVK.ESCAPE, WinAPI.MapVirtualKey(WinVK.ESCAPE, 0), 2, 0);
            Thread.Sleep(300);
        }
    }
    public static void ToM()
    {
        ToMain();
        Click(160, 80);
    }
    public static void ToServerList()
    {
        Click(650, 250);
    }
    public static void ToServerList1()
    {
        Click(150, 134);
    }
    public static void ToServer()
    {
        Click(300, 200);
    }
    public static void JoinServer()
    {
        Click(400, 290);
    }
    public static void Ok() 
    {
        Click(650, 440);
    }
    public static void Online()
    {
        Click(650, 450);
    }
}
