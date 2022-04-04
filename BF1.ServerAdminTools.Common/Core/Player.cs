namespace BF1.ServerAdminTools.BF1API.Core;

internal static class Player
{
    /// <summary>
    /// 解密玩家指针
    /// </summary>
    /// <param name="EncryptedPlayerMgr">玩家指针</param>
    /// <param name="id">玩家ID</param>
    /// <returns></returns>
    public static long EncryptedPlayerMgr_GetPlayer(long EncryptedPlayerMgr, int id)
    {
        long XorValue1 = MemoryHook.Read<long>(EncryptedPlayerMgr + 0x20) ^ MemoryHook.Read<long>(EncryptedPlayerMgr + 0x8);
        long XorValue2 = XorValue1 ^ MemoryHook.Read<long>(EncryptedPlayerMgr + 0x10);
        if (!MemoryHook.IsValid(XorValue2))
        {
            return 0;
        }

        return XorValue1 ^ MemoryHook.Read<long>(XorValue2 + 0x8 * id);
    }

    /// <summary>
    /// 读其他玩家指针
    /// </summary>
    /// <param name="id">玩家ID</param>
    /// <returns></returns>
    public static long GetPlayerById(int id)
    {
        long pClientGameContext = MemoryHook.Read<long>(Offsets.OFFSET_CLIENTGAMECONTEXT);
        if (!MemoryHook.IsValid(pClientGameContext))
        {
            return 0;
        }

        long pPlayerManager = MemoryHook.Read<long>(pClientGameContext + 0x68);
        if (!MemoryHook.IsValid(pPlayerManager))
        {
            return 0;
        }

        long pObfuscationMgr = MemoryHook.Read<long>(Offsets.OFFSET_OBFUSCATIONMGR);
        if (!MemoryHook.IsValid(pObfuscationMgr))
        {
            return 0;
        }

        long PlayerListXorValue = MemoryHook.Read<long>(pPlayerManager + 0xF8);
        long PlayerListKey = PlayerListXorValue ^ MemoryHook.Read<long>(pObfuscationMgr + 0x70);

        long mpBucketArray = MemoryHook.Read<long>(pObfuscationMgr + 0x10);

        // 这两个用Int32读
        int mnBucketCount = MemoryHook.Read<int>(pObfuscationMgr + 0x18);
        if (mnBucketCount == 0)
        {
            return 0;
        }
        //int mnElementCount = RPM.ReadMemory<int>(pObfuscationMgr + 0x1C);

        int startCount = (int)PlayerListKey % mnBucketCount;

        // node
        long mpBucketArray_startCount = MemoryHook.Read<long>(mpBucketArray + Convert.ToInt64(startCount * 8));
        long node_first = MemoryHook.Read<long>(mpBucketArray_startCount);
        long node_second = MemoryHook.Read<long>(mpBucketArray_startCount + 0x8);
        long node_mpNext = MemoryHook.Read<long>(mpBucketArray_startCount + 0x10);

        while (PlayerListKey != node_first)
        {
            mpBucketArray_startCount = node_mpNext;

            node_first = MemoryHook.Read<long>(mpBucketArray_startCount);
            node_second = MemoryHook.Read<long>(mpBucketArray_startCount + 0x8);
            node_mpNext = MemoryHook.Read<long>(mpBucketArray_startCount + 0x10);
        }

        long EncryptedPlayerMgr = node_second;
        //long MaxPlayerCount = RPM.ReadMemory<long>(EncryptedPlayerMgr + 0x18);

        return EncryptedPlayerMgr_GetPlayer(EncryptedPlayerMgr, id);
    }

    /// <summary>
    /// 读取自己指针
    /// </summary>
    /// <returns></returns>
    public static long GetLocalPlayer()
    {
        long pClientGameContext = MemoryHook.Read<long>(Offsets.OFFSET_CLIENTGAMECONTEXT);
        if (!MemoryHook.IsValid(pClientGameContext))
        {
            return 0;
        }

        long pPlayerManager = MemoryHook.Read<long>(pClientGameContext + 0x68);
        if (!MemoryHook.IsValid(pPlayerManager))
        {
            return 0;
        }

        long pObfuscationMgr = MemoryHook.Read<long>(Offsets.OFFSET_OBFUSCATIONMGR);
        if (!MemoryHook.IsValid(pObfuscationMgr))
        {
            return 0;
        }

        long LocalPlayerListXorValue = MemoryHook.Read<long>(pPlayerManager + 0xF0);
        long LocalPlayerListKey = LocalPlayerListXorValue ^ MemoryHook.Read<long>(pObfuscationMgr + 0x70);

        long mpBucketArray = MemoryHook.Read<long>(pObfuscationMgr + 0x10);

        int mnBucketCount = MemoryHook.Read<int>(pObfuscationMgr + 0x18);
        if (mnBucketCount == 0)
        {
            return 0;
        }
        //int mnElementCount = RPM.ReadMemory<int>(pObfuscationMgr + 0x1C);

        int startCount = (int)LocalPlayerListKey % mnBucketCount;

        // node
        long mpBucketArray_startCount = MemoryHook.Read<long>(mpBucketArray + Convert.ToInt64(startCount * 8));
        long node_first = MemoryHook.Read<long>(mpBucketArray_startCount);
        long node_second = MemoryHook.Read<long>(mpBucketArray_startCount + 0x8);
        long node_mpNext = MemoryHook.Read<long>(mpBucketArray_startCount + 0x10);

        while (LocalPlayerListKey != node_first)
        {
            mpBucketArray_startCount = node_mpNext;

            node_first = MemoryHook.Read<long>(mpBucketArray_startCount);
            node_second = MemoryHook.Read<long>(mpBucketArray_startCount + 0x8);
            node_mpNext = MemoryHook.Read<long>(mpBucketArray_startCount + 0x10);
        }

        long EncryptedPlayerMgr = node_second;
        //long MaxPlayerCount = RPM.ReadMemory<long>(EncryptedPlayerMgr + 0x18);

        return EncryptedPlayerMgr_GetPlayer(EncryptedPlayerMgr, 0);
    }
}
