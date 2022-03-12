namespace BF1.ServerAdminTools.Features.API2.RespJson
{
    public class Servers
    {
        public List<ServersItem> servers { get; set; }

        public class ServersItem
        {
            public string prefix { get; set; }
            public int playerAmount { get; set; }
            public int maxPlayers { get; set; }
            public int inQue { get; set; }
            public string serverInfo { get; set; }
            public string url { get; set; }
            public string mode { get; set; }
            public string currentMap { get; set; }
            public string ownerId { get; set; }
            public string region { get; set; }
            public string platform { get; set; }
            public string smallMode { get; set; }
            public Teams teams { get; set; }
            public bool official { get; set; }
            public string gameId { get; set; }
        }

        public class Teams
        {
            public TeamOne teamOne { get; set; }
            public TeamTwo teamTwo { get; set; }
        }

        public class TeamOne
        {
            public string image { get; set; }
            public string key { get; set; }
            public string name { get; set; }
        }

        public class TeamTwo
        {
            public string image { get; set; }
            public string key { get; set; }
            public string name { get; set; }
        }
    }
}
