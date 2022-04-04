using BF1.ServerAdminTools.Wpf.API.BF1Server;
using BF1.ServerAdminTools.Wpf.API.BF1Server.RespJson;
using BF1.ServerAdminTools.Wpf.Utils;
using RestSharp;
namespace BF1.ServerAdminTools.Wpf.Helper;

internal static class LoginHelper
{
    public static async Task<string> LoginSessionID()
    {
        string temp;
        if (string.IsNullOrEmpty(Globals.Config.Remid))
        {
            temp = "刷新SessionID失败，玩家Remid为空，请先获取玩家Remid后，再执行本操作";
            Core.LogError(temp);
            return temp;
        }
        var str = "https://accounts.ea.com/connect/auth?response_type=code&locale=zh_CN&client_id=sparta-backend-as-user-pc";
        var options = new RestClientOptions(str)
        {
            Timeout = 5000,
            FollowRedirects = false
        };

        var client = new RestClient(options);
        var request = new RestRequest()
            .AddHeader("Cookie", $"remid={Globals.Config.Remid}");

        Core.LogInfo($"当前Remin为 {Globals.Config.Remid}");

        var response = await client.ExecuteGetAsync(request);
        if (response.StatusCode != HttpStatusCode.Redirect)
        {
            temp = $"刷新SessionID失败，玩家Remid不正确 {Globals.Config.Remid}";
            Core.LogError($"刷新SessionID失败，玩家Remid不正确 {Globals.Config.Remid}");
            return temp;
        }

        string code = response.Headers.ToList()
                .Find(x => x.Name == "Location")
                .Value.ToString();

        Core.LogInfo($"当前Location为 {code}");

        if (!code.Contains("http://127.0.0.1/success?code="))
        {
            temp = $"刷新SessionID失败，code错误 {code}";
            Core.LogError(temp);
            return temp;
        }

        Globals.Config.Remid = response.Cookies[0].Value;
        Globals.Config.Sid = response.Cookies[1].Value;

        Core.LogInfo($"当前Remid为 {Globals.Config.Remid}");
        Core.LogInfo($"当前Sid为 {Globals.Config.Sid}");

        code = code.Replace("http://127.0.0.1/success?code=", "");
        var result = await ServerAPI.GetEnvIdViaAuthCode(code);

        if (result.IsSuccess)
        {
            var envIdViaAuthCode = result.Obj;
            Globals.Config.SessionId = envIdViaAuthCode.result.sessionId;
            temp = $"刷新SessionID成功 {Globals.Config.SessionId} |  耗时: {result.ExecTime:0.00} 秒";
            Core.LogInfo(temp);
        }
        else
        {
            temp = $"刷新SessionID失败，Code无效 {code} |  耗时: {result.ExecTime:0.00} 秒";
            Core.LogError(temp);
        }

        Core.SaveConfig();

        return temp;
    }
}
