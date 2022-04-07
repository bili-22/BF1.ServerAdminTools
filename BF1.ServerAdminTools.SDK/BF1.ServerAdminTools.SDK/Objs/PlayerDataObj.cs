using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BF1.ServerAdminTools.SDK.Objs;

public record PlayerDataObj
{
    public bool Admin { get; set; }
    public bool VIP { get; set; }

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

    public string WeaponS0CH { get; set; }
}
