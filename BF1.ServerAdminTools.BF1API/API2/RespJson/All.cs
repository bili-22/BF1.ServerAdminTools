namespace BF1.ServerAdminTools.BF1API.API2.RespJson;

public record All
{
    public string avatar { get; set; }
    public string userName { get; set; }
    public long id { get; set; }
    public int rank { get; set; }
    public string rankImg { get; set; }
    public string rankName { get; set; }
    public float skill { get; set; }
    public float scorePerMinute { get; set; }
    public float killsPerMinute { get; set; }
    public string winPercent { get; set; }
    public string bestClass { get; set; }
    public string accuracy { get; set; }
    public string headshots { get; set; }
    public string timePlayed { get; set; }
    public int secondsPlayed { get; set; }
    public float killDeath { get; set; }
    public float infantryKillDeath { get; set; }
    public float infantryKillsPerMinute { get; set; }
    public int kills { get; set; }
    public int deaths { get; set; }
    public int wins { get; set; }
    public int loses { get; set; }
    public float longestHeadShot { get; set; }
    public float revives { get; set; }
    public int dogtagsTaken { get; set; }
    public int highestKillStreak { get; set; }
    public int roundsPlayed { get; set; }
    public float awardScore { get; set; }
    public float bonusScore { get; set; }
    public float squadScore { get; set; }
    public float currentRankProgress { get; set; }
    public float totalRankProgress { get; set; }
    public int avengerKills { get; set; }
    public int saviorKills { get; set; }
    public int headShots { get; set; }
    public float heals { get; set; }
    public float repairs { get; set; }
    public float killAssists { get; set; }
    public List<WeaponsItem> weapons { get; set; }
    public List<VehiclesItem> vehicles { get; set; }
    public ActivePlatoon activePlatoon { get; set; }
    public List<PlatoonsItem> platoons { get; set; }
    public List<ClassesItem> classes { get; set; }
    public List<GamemodesItem> gamemodes { get; set; }
    public List<ProgressItem> progress { get; set; }
    public List<SessionsItem> sessions { get; set; }

    public record WeaponsItem
    {
        public string weaponName { get; set; }
        public string type { get; set; }
        public string image { get; set; }
        public int timeEquipped { get; set; }
        public int kills { get; set; }
        public float killsPerMinute { get; set; }
        public int headshotKills { get; set; }
        public string headshots { get; set; }
        public int shotsFired { get; set; }
        public int shotsHit { get; set; }
        public string accuracy { get; set; }
        public float hitVKills { get; set; }

        public string star { get; set; }
        public string time { get; set; }
    }

    public record VehiclesItem
    {
        public string vehicleName { get; set; }
        public string type { get; set; }
        public string image { get; set; }
        public int kills { get; set; }
        public float killsPerMinute { get; set; }
        public int destroyed { get; set; }
        public int timeIn { get; set; }

        public string star { get; set; }
        public string time { get; set; }
    }

    public record ActivePlatoon
    {
        public string id { get; set; }
        public string tag { get; set; }
        public string emblem { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string url { get; set; }
    }

    public record PlatoonsItem
    {
        public string id { get; set; }
        public string tag { get; set; }
        public string emblem { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string url { get; set; }
    }

    public record ClassesItem
    {
        public string className { get; set; }
        public int score { get; set; }
        public int kills { get; set; }
        public float kpm { get; set; }
        public string image { get; set; }
        public string altImage { get; set; }
        public string timePlayed { get; set; }
        public int secondsPlayed { get; set; }
    }

    public record GamemodesItem
    {
        public string gamemodeName { get; set; }
        public int wins { get; set; }
        public int losses { get; set; }
        public int score { get; set; }
        public string winPercent { get; set; }
    }

    public record ProgressItem
    {
        public string progressName { get; set; }
        public int current { get; set; }
        public int total { get; set; }
    }

    public record SessionsItem
    {

    }
}
