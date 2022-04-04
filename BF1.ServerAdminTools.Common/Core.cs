using BF1.ServerAdminTools.BF1API.Chat;
using BF1.ServerAdminTools.BF1API.Core;
using BF1.ServerAdminTools.Common.Helper;
using BF1.ServerAdminTools.Common.Utils;
using Chinese;

namespace BF1.ServerAdminTools.Common;

public interface IMsgCall
{
    public void Info(string data);
    public void Error(string data, Exception e);
}

public static class Core
{

    public static IMsgCall Msg;

    public static void Init(IMsgCall call)
    {
        Msg = call;
    }

    public static bool IsAppRun()
    {
        return ProcessUtil.IsAppRun(Globals.TargetAppName);
    }

    public static void WriteErrorLog(string data)
        => ConfigHelper.WriteErrorLog(data);

    public static void SaveAll()
        => ConfigHelper.SaveAll();

    public static void ConfigInit()
    {
        try
        {
            LoggerHelper.Info($"正在读取配置文件");
            ConfigHelper.LoadConfig();
            LoggerHelper.Info($"配置文件读取完成");
        }
        catch (Exception e)
        {
            LoggerHelper.Error($"配置文件读取失败", e);
            Msg.Error("配置文件读取失败", e);
        }
    }

    public static void LogInfo(string data)
        => LoggerHelper.Info(data);

    public static void LogError(string data)
        => LoggerHelper.Error(data);

    public static void LogError(string data, Exception e)
        => LoggerHelper.Error(data, e);

    public static void SQLInit()
    {
        try
        {
            LoggerHelper.Info($"SQLite数据库正在初始化");
            SQLiteHelper.Initialize();
            LoggerHelper.Info($"SQLite数据库初始化成功");
        }
        catch (Exception e)
        {
            LoggerHelper.Error($"SQLite数据库初始化失败", e);
            Msg.Error("SQLite数据库初始化失败", e);
        }
    }
    public static bool HookInit()
        => MemoryHook.Initialize(Globals.TargetAppName);

    public static bool MsgAllocateMemory()
        => ChatMsg.AllocateMemory();

    public static long MsgGetAllocateMemoryAddress()
        => ChatMsg.GetAllocateMemoryAddress();

    public static void MsgFreeMemory()
        => ChatMsg.FreeMemory();
}
