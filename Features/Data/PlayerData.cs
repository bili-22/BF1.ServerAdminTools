namespace BF1.ServerAdminTools.Features.Data
{
    public class PlayerData
    {
        public string Admin { get; set; }
        public string VIP { get; set; }

        public byte Mark { get; set; }
        public int TeamID { get; set; }
        public byte Spectator { get; set; }
        public string Clan { get; set; }
        public string Name { get; set; }
        public long PersonaId { get; set; }
        public string SquadId { get; set; }

        public int Rank { get; set; }
        public int Kill { get; set; }
        public int Dead { get; set; }
        public int Score { get; set; }

        public float KD { get; set; }
        public float KPM { get; set; }

        public string WeaponS0 { get; set; }
        public string WeaponS1 { get; set; }
        public string WeaponS2 { get; set; }
        public string WeaponS3 { get; set; }
        public string WeaponS4 { get; set; }
        public string WeaponS5 { get; set; }
        public string WeaponS6 { get; set; }
        public string WeaponS7 { get; set; }
    }
}
