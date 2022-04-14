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
        NettyCore.InitConfig();

        while (true)
        {
            string input = Console.ReadLine();
            if (input == null)
                return;
            string[] arg = input.Split(" ");

        }
    }


}

public class NettyCore
{
    public static void InitConfig()
        => ConfigUtils.Init();

    public static void LoadConfig()
        => ConfigUtils.Load();

    public static ConfigNettyObj GetConfig()
        => ConfigUtils.Config;

    public static void SetConfig(ConfigNettyObj obj)
        => ConfigUtils.Save(obj);

    public static Task StartServer()
        => NettyServer.Start();

    public static Task StopServer()
         => NettyServer.Stop();

    public static bool State
    {
        get
        {
            return NettyServer.State;
        }
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