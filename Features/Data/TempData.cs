namespace BF1.ServerAdminTools.Features.Data
{
    public class TempData
    {
        public struct ClientPlayer
        {
            public long BaseAddress;

            public byte Mark;
            public int TeamID;
            public byte Spectator;
            public string Name;
            public long PersonaId;
            public int PartyId;

            public string[] WeaponSlot;
        }

        public struct ClientSoldierEntity
        {
            public long pClientVehicleEntity;
            public long pVehicleEntityData;

            public long pClientSoldierEntity;
            public long pClientSoldierWeaponComponent;
            public long m_handler;
            public long pClientSoldierWeapon;
            public long pWeaponEntityData;
        }

        public struct ClientPlayerScore
        {
            public long BaseAddress;

            public long Offset;
            public long Offset0;

            public byte Mark;
            public int Rank;
            public int Kill;
            public int Dead;
            public int Score;
        }
    }
}
