namespace BF1.ServerAdminTools.Common.API.BF1Server.RespJson;

public record CareerForOwnedGamesByPersonaId
{
    public string jsonrpc { get; set; }
    public string id { get; set; }
    public Result result { get; set; }
    public record Result
    {
        public Stats0 stats { get; set; }
        public GameStats gameStats { get; set; }
        [JsonIgnore]
        public List<string> highlights { get; set; }
        [JsonIgnore]
        public string emblem { get; set; }
        public record Stats0
        {
            public int timePlayed { get; set; }
            public int wins { get; set; }
            public int losses { get; set; }
            public int kills { get; set; }
            public int deaths { get; set; }
            public float kpm { get; set; }
            public float spm { get; set; }
            public float skill { get; set; }
            public string soldierImageUrl { get; set; }
            [JsonIgnore]
            public Rank rank { get; set; }
            [JsonIgnore]
            public RankProgress rankProgress { get; set; }
            [JsonIgnore]
            public FreemiumRank freemiumRank { get; set; }
            [JsonIgnore]
            public List<CompletionItem> completion { get; set; }
            [JsonIgnore]
            public List<HighlightsItem> highlights { get; set; }
            public HighlightsByType highlightsByType { get; set; }

            public record Rank { }
            public record RankProgress { }
            public record FreemiumRank { }
            public record CompletionItem { }
            public record HighlightsItem { }
            public record HighlightsByType
            {
                [JsonIgnore]
                public List<HeadshotsItem> headshots { get; set; }
                public List<VehicleItem> vehicle { get; set; }
                [JsonIgnore]
                public List<AccuracyItem> accuracy { get; set; }
                [JsonIgnore]
                public List<GadgetItem> gadget { get; set; }
                [JsonIgnore]
                public List<VehicleCategoryItem> vehicleCategory { get; set; }
                public List<WeaponItem> weapon { get; set; }
                public record HeadshotsItem { }
                public record VehicleItem
                {
                    public string value { get; set; }
                    public string highlightName { get; set; }
                    public string highlightType { get; set; }
                    public string itemId { get; set; }
                    public string name { get; set; }
                    public string imageUrl { get; set; }
                    public string highlightText { get; set; }
                    public HighlightDetails highlightDetails { get; set; }
                    public record HighlightDetails
                    {
                        [JsonIgnore]
                        public Progression progression { get; set; }
                        public Stats stats { get; set; }
                        [JsonIgnore]
                        public string star { get; set; }
                        [JsonIgnore]
                        public string kitRank { get; set; }
                        public record Progression { }
                        public record Stats
                        {
                            public Values values { get; set; }
                            public record Values
                            {
                                public float seconds { get; set; }
                                public float kills { get; set; }
                                public float destroyed { get; set; }
                            }
                        }
                    }
                }
                public record AccuracyItem { }
                public record GadgetItem { }
                public record VehicleCategoryItem { }
                public record WeaponItem
                {
                    public string value { get; set; }
                    public string highlightName { get; set; }
                    public string highlightType { get; set; }
                    public string itemId { get; set; }
                    public string name { get; set; }
                    public string imageUrl { get; set; }
                    public string highlightText { get; set; }
                    public HighlightDetails highlightDetails { get; set; }
                    public record HighlightDetails
                    {
                        [JsonIgnore]
                        public Progression progression { get; set; }
                        public Stats stats { get; set; }
                        [JsonIgnore]
                        public string star { get; set; }
                        [JsonIgnore]
                        public string kitRank { get; set; }
                        public record Progression { }
                        public record Stats
                        {
                            public Values values { get; set; }
                            public record Values
                            {
                                public float kills { get; set; }
                                public float headshots { get; set; }
                                public float accuracy { get; set; }
                                public float seconds { get; set; }
                                public float hits { get; set; }
                                public float shots { get; set; }
                            }
                        }
                    }
                }
            }
        }
        public record GameStats
        {

        }
        public record Bf4 { }
    }
}
