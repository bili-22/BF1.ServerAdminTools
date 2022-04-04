namespace BF1.ServerAdminTools.Wpf.Hook;

internal class SDK
{
    public class ClientSoldierWeapon
    {
        public SoldierWeaponData m_pSoldierWeaponData;  // 0x0030
        public ClientWeapon m_pWeapon;                  // 0x4A30
    }

    public class SoldierWeaponData
    {

    }

    public class ClientWeapon
    {
        public WeaponData m_pWeaponData;                // 0x0008 [WeaponData]
        public WeaponFiringData m_WeaponFiringData;     // 0x0018 [WeaponFiringData]
        public WeaponModifier m_pWeaponModifier;        // 0x0020 
        public float m_CameraFOV;                       // 0x0330 
        public float m_WeaponFOV;                       // 0x0334 
        public float m_FOVScaleFactor;                  // 0x0338 
        public int m_ZoomLevel;                         // 0x034C 
    }

    public class WeaponData
    {

    }

    public class WeaponFiringData
    {

    }

    public class WeaponModifier
    {
        public UnlockAssetBase m_pSoldierWeaponUnlockAsset;    // 0x0038 [SoldierWeaponUnlockAsset]
    }

    public class UnlockAssetBase
    {

    }
}
