using BF1.ServerAdminTools.Common.Helper;
using BF1.ServerAdminTools.Common.Utils;

namespace BF1.ServerAdminTools.Netty;

internal class ConfigUtils
{
    private static string FileLocal = $"{ConfigLocal.Base}/Netty/config.json";
    public static ConfigObj Config { get; private set; }

    public static void Init() 
        => Directory.CreateDirectory($"{ConfigLocal.Base}/Netty");

    public static void Load()
    {
        if (File.Exists(FileLocal))
            Config = JsonUtil.JsonDese<ConfigObj>(File.ReadAllText(FileLocal));
        else
        {
            Save(Config = new()
            {
                Port = 23232,
                ServerKey = new Random().NextInt64()
            });
        }
    }

    public static void Save(ConfigObj obj)
    {
        Config = obj;
        FileUtil.WriteFile(FileLocal, JsonUtil.JsonSeri(Config));
    }
}
