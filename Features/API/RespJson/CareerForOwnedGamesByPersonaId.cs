namespace BF1.ServerAdminTools.Features.API.RespJson
{
    public class CareerForOwnedGamesByPersonaId
    {
        public string jsonrpc { get; set; }
        public string id { get; set; }
        public Result result { get; set; }
        public class Result
        {
            public Stats0 stats { get; set; }
            public GameStats gameStats { get; set; }
            [JsonIgnore]
            public List<string> highlights { get; set; }
            [JsonIgnore]
            public string emblem { get; set; }
            public class Stats0 { }
            public class GameStats
            {
                public Tunguska tunguska { get; set; }
                public Bf4 bf4 { get; set; }
                public class Tunguska
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
                    [JsonIgnore]
                    public List<EquippedDogtagsItem> equippedDogtags { get; set; }
                    public class Rank { }
                    public class RankProgress { }
                    public class FreemiumRank { }
                    public class CompletionItem { }
                    public class HighlightsItem { }
                    public class HighlightsByType
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
                        [JsonIgnore]
                        public List<SidearmItem> sidearm { get; set; }
                        [JsonIgnore]
                        public List<KitItem> kit { get; set; }
                        [JsonIgnore]
                        public List<PrimaryItem> primary { get; set; }
                        public class HeadshotsItem { }
                        public class VehicleItem
                        {
                            public string value { get; set; }
                            public string highlightName { get; set; }
                            public string highlightType { get; set; }
                            public string itemId { get; set; }
                            public string name { get; set; }
                            public string imageUrl { get; set; }
                            public string highlightText { get; set; }
                            public HighlightDetails highlightDetails { get; set; }
                            public class HighlightDetails
                            {
                                [JsonIgnore]
                                public Progression progression { get; set; }
                                public Stats stats { get; set; }
                                [JsonIgnore]
                                public string star { get; set; }
                                [JsonIgnore]
                                public string kitRank { get; set; }
                                public class Progression { }
                                public class Stats
                                {
                                    public Values values { get; set; }
                                    public class Values
                                    {
                                        public float seconds { get; set; }
                                        public float kills { get; set; }
                                        public float destroyed { get; set; }
                                    }
                                }
                            }
                        }
                        public class AccuracyItem { }
                        public class GadgetItem { }
                        public class VehicleCategoryItem { }
                        public class WeaponItem
                        {
                            public string value { get; set; }
                            public string highlightName { get; set; }
                            public string highlightType { get; set; }
                            public string itemId { get; set; }
                            public string name { get; set; }
                            public string imageUrl { get; set; }
                            public string highlightText { get; set; }
                            public HighlightDetails highlightDetails { get; set; }
                            public class HighlightDetails
                            {
                                [JsonIgnore]
                                public Progression progression { get; set; }
                                public Stats stats { get; set; }
                                [JsonIgnore]
                                public string star { get; set; }
                                [JsonIgnore]
                                public string kitRank { get; set; }
                                public class Progression { }
                                public class Stats
                                {
                                    public Values values { get; set; }
                                    public class Values
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
                    public class SidearmItem { }
                    public class KitItem { }
                    public class PrimaryItem { }
                }
                public class EquippedDogtagsItem { }
            }
            public class Bf4 { }
        }
    }
}
