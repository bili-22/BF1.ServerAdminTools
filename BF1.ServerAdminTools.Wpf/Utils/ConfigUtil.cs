using BF1.ServerAdminTools.Common.Data;
using BF1.ServerAdminTools.Common.Helper;
using BF1.ServerAdminTools.Netty;
using BF1.ServerAdminTools.Wpf.Data;

namespace BF1.ServerAdminTools.Common.Utils;

internal static class ConfigUtil
{
    public static string ServerRule { get; } = $"{ConfigLocal.Base}/ServerRule";

    public static void Init()
    {
        Directory.CreateDirectory(ServerRule);
        NettyCore.InitConfig();
    }

    public static void SaveAll()
    {
        foreach (var item in DataSave.Rules)
        {
            FileUtil.WriteFile($"{ServerRule}/{item.Key}.json", JsonUtil.JsonSeri(item.Value));
        }
    }

    public static void LoadAll()
    {
        var dir = new DirectoryInfo(ServerRule);
        foreach (var item in dir.GetFiles())
        {
            if (item.Extension is ".json")
            {
                var name = item.Name.Trim().ToLower().Replace(".json", "");
                var data = File.ReadAllText(item.FullName);
                var rule = JsonUtil.JsonDese<ServerRule>(data);

                if (rule != null)
                {
                    DataSave.Rules.Add(name, rule);
                }
            }
        }

        if (!DataSave.Rules.ContainsKey("default"))
        {
            var rule = new ServerRule()
            {
                Name = "Default"
            };
            DataSave.Rules.Add("default", rule);
            FileUtil.WriteFile($"{ServerRule}/default.json", JsonUtil.JsonSeri(rule));
        }

        NettyCore.LoadConfig();
    }

    public static void DeleteRule(string name)
    {
        File.Delete($"{ServerRule}/{name}.json");
    }

    public static void SaveRule()
    {
        FileUtil.WriteFile($"{ServerRule}/{DataSave.NowRule.Name.Trim().ToLower()}.json",
            JsonUtil.JsonSeri(DataSave.NowRule));
    }

    public static void SaveRule(ServerRule rule)
    {
        FileUtil.WriteFile($"{ServerRule}/{rule.Name.Trim().ToLower()}.json",
            JsonUtil.JsonSeri(rule));
    }
}
