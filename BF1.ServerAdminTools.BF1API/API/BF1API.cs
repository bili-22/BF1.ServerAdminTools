using BF1.ServerAdminTools.Common.Data;
using BF1.ServerAdminTools.Common.Utils;
using RestSharp;
using System.Diagnostics;
using System.Net;

namespace BF1.ServerAdminTools.BF1API.API
{
    public static class BF1API
    {
        private const string Host = "https://sparta-gw.battlelog.com/jsonrpc/pc/api";

        private static RestClient client;
        private static Dictionary<string, string> headers;

        /// <summary>
        /// 初始化RestSharp
        /// </summary>
        public static void Init()
        {
            if (client == null)
            {
                var options = new RestClientOptions(Host)
                {
                    Timeout = 5000
                };

                client = new RestClient(options);

                headers = new Dictionary<string, string>();
                headers["User-Agent"] = "ProtoHttp 1.3/DS 15.1.2.1.0 (Windows)";
                headers["X-GatewaySession"] = Globals.SessionId;
                headers["X-ClientVersion"] = "release-bf1-lsu35_26385_ad7bf56a_tunguska_all_prod";
                headers["X-DbId"] = "Tunguska.Shipping2PC.Win32";
                headers["X-CodeCL"] = "3779779";
                headers["X-DataCL"] = "3779779";
                headers["X-SaveGameVersion"] = "26";
                headers["X-HostingGameId"] = "tunguska";
                headers["X-Sparta-Info"] = "tenancyRootEnv=unknown; tenancyBlazeEnv=unknown";
            }
        }

        /// <summary>
        /// 获取战地1欢迎语
        /// </summary>
        public static async Task<RespContent> GetWelcomeMessage()
        {
            Stopwatch sw = new();
            sw.Start();

            RespContent respContent = new();

            try
            {
                headers["X-GatewaySession"] = Globals.SessionId;
                respContent.IsSuccess = false;

                var reqBody = new
                {
                    jsonrpc = "2.0",
                    method = "Onboarding.welcomeMessage",
                    @params = new
                    {
                        game = "tunguska",
                        minutesToUTC = "-480"
                    },
                    id = Guid.NewGuid()
                };

                var request = new RestRequest()
                    .AddHeaders(headers)
                    .AddJsonBody(reqBody);

                var response = await client.ExecutePostAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    respContent.IsSuccess = true;
                    respContent.Message = response.Content;
                }
                else
                {
                    var respError = JsonUtil.JsonDese<RespError>(response.Content);

                    respContent.Message = $"{respError.error.code} {respError.error.message}";
                }
            }
            catch (Exception ex)
            {
                respContent.Message = ex.Message;
            }

            sw.Stop();
            respContent.ExecTime = sw.Elapsed.TotalSeconds;

            return respContent;
        }

        /// <summary>
        /// 设置API语言
        /// </summary>
        public static async Task<RespContent> SetAPILocale()
        {
            Stopwatch sw = new();
            sw.Start();

            RespContent respContent = new();

            try
            {
                headers["X-GatewaySession"] = Globals.SessionId;
                respContent.IsSuccess = false;

                var reqBody = new
                {
                    jsonrpc = "2.0",
                    method = "CompanionSettings.setLocale",
                    @params = new
                    {
                        locale = "zh_TW"
                    },
                    id = Guid.NewGuid()
                };

                var request = new RestRequest()
                    .AddHeaders(headers)
                    .AddJsonBody(reqBody);

                var response = await client.ExecutePostAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    respContent.IsSuccess = true;
                    respContent.Message = response.Content;
                }
                else
                {
                    var respError = JsonUtil.JsonDese<RespError>(response.Content);

                    respContent.Message = $"{respError.error.code} {respError.error.message}";
                }
            }
            catch (Exception ex)
            {
                respContent.Message = ex.Message;
            }

            sw.Stop();
            respContent.ExecTime = sw.Elapsed.TotalSeconds;

            return respContent;
        }

        /// <summary>
        /// 踢出指定玩家
        /// </summary>
        public static async Task<RespContent> AdminKickPlayer(string personaId, string reason)
        {
            Stopwatch sw = new();
            sw.Start();

            RespContent respContent = new();

            try
            {
                headers["X-GatewaySession"] = Globals.SessionId;
                respContent.IsSuccess = false;

                var reqBody = new
                {
                    jsonrpc = "2.0",
                    method = "RSP.kickPlayer",
                    @params = new
                    {
                        game = "tunguska",
                        gameId = Globals.GameId,
                        personaId = personaId,
                        reason = reason
                    },
                    id = Guid.NewGuid()
                };

                var request = new RestRequest()
                    .AddHeaders(headers)
                    .AddJsonBody(reqBody);

                var response = await client.ExecutePostAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    respContent.IsSuccess = true;
                    respContent.Message = response.Content;
                }
                else
                {
                    var respError = JsonUtil.JsonDese<RespError>(response.Content);

                    respContent.Message = $"{respError.error.code} {respError.error.message}";
                }
            }
            catch (Exception ex)
            {
                respContent.Message = ex.Message;
            }

            sw.Stop();
            respContent.ExecTime = sw.Elapsed.TotalSeconds;

            return respContent;
        }

        /// <summary>
        /// 更换指定玩家队伍
        /// </summary>
        public static async Task<RespContent> AdminMovePlayer(string personaId, string teamId)
        {
            Stopwatch sw = new();
            sw.Start();

            RespContent respContent = new();

            try
            {
                headers["X-GatewaySession"] = Globals.SessionId;
                respContent.IsSuccess = false;

                var reqBody = new
                {
                    jsonrpc = "2.0",
                    method = "RSP.movePlayer",
                    @params = new
                    {
                        game = "tunguska",
                        personaId = personaId,
                        gameId = Globals.GameId,
                        teamId = teamId,
                        forceKill = true,
                        moveParty = false
                    },
                    id = Guid.NewGuid()
                };

                var request = new RestRequest()
                    .AddHeaders(headers)
                    .AddJsonBody(reqBody);

                var response = await client.ExecutePostAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    respContent.IsSuccess = true;
                    respContent.Message = response.Content;
                }
                else
                {
                    var respError = JsonUtil.JsonDese<RespError>(response.Content);

                    respContent.Message = $"{respError.error.code} {respError.error.message}";
                }
            }
            catch (Exception ex)
            {
                respContent.Message = ex.Message;
            }

            sw.Stop();
            respContent.ExecTime = sw.Elapsed.TotalSeconds;

            return respContent;
        }

        /// <summary>
        /// 更换服务器地图
        /// </summary>
        public static async Task<RespContent> ChangeServerMap(string persistedGameId, string levelIndex)
        {
            Stopwatch sw = new();
            sw.Start();

            RespContent respContent = new();

            try
            {
                headers["X-GatewaySession"] = Globals.SessionId;
                respContent.IsSuccess = false;

                var reqBody = new
                {
                    jsonrpc = "2.0",
                    method = "RSP.chooseLevel",
                    @params = new
                    {
                        game = "tunguska",
                        persistedGameId = persistedGameId,
                        levelIndex = levelIndex
                    },
                    id = Guid.NewGuid()
                };

                var request = new RestRequest()
                    .AddHeaders(headers)
                    .AddJsonBody(reqBody);

                var response = await client.ExecutePostAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    respContent.IsSuccess = true;
                    respContent.Message = response.Content;
                }
                else
                {
                    var respError = JsonUtil.JsonDese<RespError>(response.Content);

                    respContent.Message = $"{respError.error.code} {respError.error.message}";
                }
            }
            catch (Exception ex)
            {
                respContent.Message = ex.Message;
            }

            sw.Stop();
            respContent.ExecTime = sw.Elapsed.TotalSeconds;

            return respContent;
        }

        /// <summary>
        /// 获取完整服务器详情
        /// </summary>
        public static async Task<RespContent> GetFullServerDetails()
        {
            Stopwatch sw = new();
            sw.Start();

            RespContent respContent = new();

            try
            {
                headers["X-GatewaySession"] = Globals.SessionId;
                respContent.IsSuccess = false;

                var reqBody = new
                {
                    jsonrpc = "2.0",
                    method = "GameServer.getFullServerDetails",
                    @params = new
                    {
                        game = "tunguska",
                        gameId = Globals.GameId
                    },
                    id = Guid.NewGuid()
                };

                var request = new RestRequest()
                    .AddHeaders(headers)
                    .AddJsonBody(reqBody);

                var response = await client.ExecutePostAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    respContent.IsSuccess = true;
                    respContent.Message = response.Content;
                }
                else
                {
                    var respError = JsonUtil.JsonDese<RespError>(response.Content);

                    respContent.Message = $"{respError.error.code} {respError.error.message}";
                }
            }
            catch (Exception ex)
            {
                respContent.Message = ex.Message;
            }

            sw.Stop();
            respContent.ExecTime = sw.Elapsed.TotalSeconds;

            return respContent;
        }

        /// <summary>
        /// 添加服务器管理员
        /// </summary>
        public static async Task<RespContent> AddServerAdmin(string personaName)
        {
            Stopwatch sw = new();
            sw.Start();

            RespContent respContent = new();

            try
            {
                headers["X-GatewaySession"] = Globals.SessionId;
                respContent.IsSuccess = false;

                var reqBody = new
                {
                    jsonrpc = "2.0",
                    method = "RSP.addServerAdmin",
                    @params = new
                    {
                        game = "tunguska",
                        serverId = Globals.ServerId,
                        personaName = personaName
                    },
                    id = Guid.NewGuid()
                };

                var request = new RestRequest()
                    .AddHeaders(headers)
                    .AddJsonBody(reqBody);

                var response = await client.ExecutePostAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    respContent.IsSuccess = true;
                    respContent.Message = response.Content;
                }
                else
                {
                    var respError = JsonUtil.JsonDese<RespError>(response.Content);

                    respContent.Message = $"{respError.error.code} {respError.error.message}";
                }
            }
            catch (Exception ex)
            {
                respContent.Message = ex.Message;
            }

            sw.Stop();
            respContent.ExecTime = sw.Elapsed.TotalSeconds;

            return respContent;
        }

        /// <summary>
        /// 移除服务器管理员
        /// </summary>
        public static async Task<RespContent> RemoveServerAdmin(string personaId)
        {
            Stopwatch sw = new();
            sw.Start();

            RespContent respContent = new();

            try
            {
                headers["X-GatewaySession"] = Globals.SessionId;
                respContent.IsSuccess = false;

                var reqBody = new
                {
                    jsonrpc = "2.0",
                    method = "RSP.removeServerAdmin",
                    @params = new
                    {
                        game = "tunguska",
                        serverId = Globals.ServerId,
                        personaId = personaId
                    },
                    id = Guid.NewGuid()
                };

                var request = new RestRequest()
                    .AddHeaders(headers)
                    .AddJsonBody(reqBody);

                var response = await client.ExecutePostAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    respContent.IsSuccess = true;
                    respContent.Message = response.Content;
                }
                else
                {
                    var respError = JsonUtil.JsonDese<RespError>(response.Content);

                    respContent.Message = $"{respError.error.code} {respError.error.message}";
                }
            }
            catch (Exception ex)
            {
                respContent.Message = ex.Message;
            }

            sw.Stop();
            respContent.ExecTime = sw.Elapsed.TotalSeconds;

            return respContent;
        }

        /// <summary>
        /// 添加服务器VIP
        /// </summary>
        public static async Task<RespContent> AddServerVip(string personaName)
        {
            Stopwatch sw = new();
            sw.Start();

            RespContent respContent = new();

            try
            {
                headers["X-GatewaySession"] = Globals.SessionId;
                respContent.IsSuccess = false;

                var reqBody = new
                {
                    jsonrpc = "2.0",
                    method = "RSP.addServerVip",
                    @params = new
                    {
                        game = "tunguska",
                        serverId = Globals.ServerId,
                        personaName = personaName
                    },
                    id = Guid.NewGuid()
                };

                var request = new RestRequest()
                    .AddHeaders(headers)
                    .AddJsonBody(reqBody);

                var response = await client.ExecutePostAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    respContent.IsSuccess = true;
                    respContent.Message = response.Content;
                }
                else
                {
                    var respError = JsonUtil.JsonDese<RespError>(response.Content);

                    respContent.Message = $"{respError.error.code} {respError.error.message}";
                }
            }
            catch (Exception ex)
            {
                respContent.Message = ex.Message;
            }

            sw.Stop();
            respContent.ExecTime = sw.Elapsed.TotalSeconds;

            return respContent;
        }

        /// <summary>
        /// 移除服务器VIP
        /// </summary>
        public static async Task<RespContent> RemoveServerVip(string personaId)
        {
            Stopwatch sw = new();
            sw.Start();

            RespContent respContent = new();

            try
            {
                headers["X-GatewaySession"] = Globals.SessionId;
                respContent.IsSuccess = false;

                var reqBody = new
                {
                    jsonrpc = "2.0",
                    method = "RSP.removeServerVip",
                    @params = new
                    {
                        game = "tunguska",
                        serverId = Globals.ServerId,
                        personaId = personaId
                    },
                    id = Guid.NewGuid()
                };

                var request = new RestRequest()
                    .AddHeaders(headers)
                    .AddJsonBody(reqBody);

                var response = await client.ExecutePostAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    respContent.IsSuccess = true;
                    respContent.Message = response.Content;
                }
                else
                {
                    var respError = JsonUtil.JsonDese<RespError>(response.Content);

                    respContent.Message = $"{respError.error.code} {respError.error.message}";
                }
            }
            catch (Exception ex)
            {
                respContent.Message = ex.Message;
            }

            sw.Stop();
            respContent.ExecTime = sw.Elapsed.TotalSeconds;

            return respContent;
        }

        /// <summary>
        /// 添加服务器BAN
        /// </summary>
        public static async Task<RespContent> AddServerBan(string personaName)
        {
            Stopwatch sw = new();
            sw.Start();

            RespContent respContent = new();

            try
            {
                headers["X-GatewaySession"] = Globals.SessionId;
                respContent.IsSuccess = false;

                var reqBody = new
                {
                    jsonrpc = "2.0",
                    method = "RSP.addServerBan",
                    @params = new
                    {
                        game = "tunguska",
                        serverId = Globals.ServerId,
                        personaName = personaName
                    },
                    id = Guid.NewGuid()
                };

                var request = new RestRequest()
                    .AddHeaders(headers)
                    .AddJsonBody(reqBody);

                var response = await client.ExecutePostAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    respContent.IsSuccess = true;
                    respContent.Message = response.Content;
                }
                else
                {
                    var respError = JsonUtil.JsonDese<RespError>(response.Content);

                    respContent.Message = $"{respError.error.code} {respError.error.message}";
                }
            }
            catch (Exception ex)
            {
                respContent.Message = ex.Message;
            }

            sw.Stop();
            respContent.ExecTime = sw.Elapsed.TotalSeconds;

            return respContent;
        }

        /// <summary>
        /// 移除服务器BAN
        /// </summary>
        public static async Task<RespContent> RemoveServerBan(string personaId)
        {
            Stopwatch sw = new();
            sw.Start();

            RespContent respContent = new();

            try
            {
                headers["X-GatewaySession"] = Globals.SessionId;
                respContent.IsSuccess = false;

                var reqBody = new
                {
                    jsonrpc = "2.0",
                    method = "RSP.removeServerBan",
                    @params = new
                    {
                        game = "tunguska",
                        serverId = Globals.ServerId,
                        personaId = personaId
                    },
                    id = Guid.NewGuid()
                };

                var request = new RestRequest()
                    .AddHeaders(headers)
                    .AddJsonBody(reqBody);

                var response = await client.ExecutePostAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    respContent.IsSuccess = true;
                    respContent.Message = response.Content;
                }
                else
                {
                    var respError = JsonUtil.JsonDese<RespError>(response.Content);

                    respContent.Message = $"{respError.error.code} {respError.error.message}";
                }
            }
            catch (Exception ex)
            {
                respContent.Message = ex.Message;
            }

            sw.Stop();
            respContent.ExecTime = sw.Elapsed.TotalSeconds;

            return respContent;
        }

        /// <summary>
        /// 获取服务器RSP信息
        /// </summary>
        public static async Task<RespContent> GetServerDetails()
        {
            Stopwatch sw = new();
            sw.Start();

            RespContent respContent = new();

            try
            {
                headers["X-GatewaySession"] = Globals.SessionId;
                respContent.IsSuccess = false;

                var reqBody = new
                {
                    jsonrpc = "2.0",
                    method = "RSP.getServerDetails",
                    @params = new
                    {
                        game = "tunguska",
                        serverId = Globals.ServerId
                    },
                    id = Guid.NewGuid()
                };

                var request = new RestRequest()
                    .AddHeaders(headers)
                    .AddJsonBody(reqBody);

                var response = await client.ExecutePostAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    respContent.IsSuccess = true;
                    respContent.Message = response.Content;
                }
                else
                {
                    var respError = JsonUtil.JsonDese<RespError>(response.Content);

                    respContent.Message = $"{respError.error.code} {respError.error.message}";
                }
            }
            catch (Exception ex)
            {
                respContent.Message = ex.Message;
            }

            sw.Stop();
            respContent.ExecTime = sw.Elapsed.TotalSeconds;

            return respContent;
        }

        /// <summary>
        /// 更新服务器信息
        /// </summary>
        public static async Task<RespContent> UpdateServer(UpdateServerReqBody reqBody)
        {
            Stopwatch sw = new();
            sw.Start();

            RespContent respContent = new();

            try
            {
                headers["X-GatewaySession"] = Globals.SessionId;
                respContent.IsSuccess = false;

                var request = new RestRequest()
                    .AddHeaders(headers)
                    .AddJsonBody(reqBody);

                var response = await client.ExecutePostAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    respContent.IsSuccess = true;
                    respContent.Message = response.Content;
                }
                else
                {
                    var respError = JsonUtil.JsonDese<RespError>(response.Content);

                    respContent.Message = $"{respError.error.code} {respError.error.message}";
                }
            }
            catch (Exception ex)
            {
                respContent.Message = ex.Message;
            }

            sw.Stop();
            respContent.ExecTime = sw.Elapsed.TotalSeconds;

            return respContent;
        }

        /// <summary>
        /// 获取玩家SessionID
        /// </summary>
        public static async Task<RespContent> GetEnvIdViaAuthCode(string authCode)
        {
            Stopwatch sw = new();
            sw.Start();

            RespContent respContent = new();

            try
            {
                headers["X-GatewaySession"] = Globals.SessionId;
                respContent.IsSuccess = false;

                var reqBody = new
                {
                    jsonrpc = "2.0",
                    method = "Authentication.getEnvIdViaAuthCode",
                    @params = new
                    {
                        authCode = authCode,
                        locale = "zh-tw"
                    },
                    id = Guid.NewGuid()
                };

                var request = new RestRequest()
                    .AddHeaders(headers)
                    .AddJsonBody(reqBody);

                var response = await client.ExecutePostAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    respContent.IsSuccess = true;
                    respContent.Message = response.Content;
                }
                else
                {
                    var respError = JsonUtil.JsonDese<RespError>(response.Content);

                    respContent.Message = $"{respError.error.code} {respError.error.message}";
                }
            }
            catch (Exception ex)
            {
                respContent.Message = ex.Message;
            }

            sw.Stop();
            respContent.ExecTime = sw.Elapsed.TotalSeconds;

            return respContent;
        }

        /// <summary>
        /// 获取玩家SessionID
        /// </summary>
        public static async Task<RespContent> GetCareerForOwnedGamesByPersonaId(string personaId)
        {
            Stopwatch sw = new();
            sw.Start();

            RespContent respContent = new();

            try
            {
                headers["X-GatewaySession"] = Globals.SessionId;
                respContent.IsSuccess = false;

                var reqBody = new
                {
                    jsonrpc = "2.0",
                    method = "Stats.getCareerForOwnedGamesByPersonaId",
                    @params = new
                    {
                        game = "tunguska",
                        personaId = personaId
                    },
                    id = Guid.NewGuid()
                };

                var request = new RestRequest()
                    .AddHeaders(headers)
                    .AddJsonBody(reqBody);

                var response = await client.ExecutePostAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    respContent.IsSuccess = true;
                    respContent.Message = response.Content;
                }
                else
                {
                    var respError = JsonUtil.JsonDese<RespError>(response.Content);

                    respContent.Message = $"{respError.error.code} {respError.error.message}";
                }
            }
            catch (Exception ex)
            {
                respContent.Message = ex.Message;
            }

            sw.Stop();
            respContent.ExecTime = sw.Elapsed.TotalSeconds;

            return respContent;
        }
    }
}
