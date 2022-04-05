using BF1.ServerAdminTools.Common;

namespace BF1.ServerAdminTools.Netty;

public class NettyMain
{
    public static void Main()
    {
        Console.WriteLine("BF1.ServerAdminTools 启动中");

        Core.Init(new Log());
        Core.ConfigInit();
        Core.SQLInit();

        if (!Core.IsGameRun())
        {
            Console.WriteLine("检测到游戏未启动");
        }

        Console.WriteLine("BF1.ServerAdminTools 正在读取配置文件");
        InitConfig();

        while (true)
        {
            string input = Console.ReadLine();
        }
    }

    public static void InitConfig() 
    {
        ConfigUtils.Init();
        ConfigUtils.Load();
    }

    public static ConfigObj GetConfig() 
        => ConfigUtils.Config;

    public static void SetConfig(ConfigObj obj) 
    {
        ConfigUtils.Save(obj);
    }
}

internal class Log : IMsgCall
{
    public void Info(string data)
    {
        Console.WriteLine(data);
    }

    public void Error(string data, Exception e)
    {
        Console.WriteLine(data + "\n" + e.ToString());
    }
}