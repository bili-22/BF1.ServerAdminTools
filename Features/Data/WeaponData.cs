namespace BF1.ServerAdminTools.Features.Data
{
    public class WeaponData
    {
        public struct WeaponName
        {
            public string English;
            public string Chinese;
            public string ShortTxt;
        }

        public static List<WeaponName> AllWeaponInfo=new List<WeaponName>()
        {
            // 需要添加的按照这个格式来就可以了

            // 装备
            new WeaponName(){ English="Gas Mask", Chinese="防毒面具", ShortTxt="Gas Mask" },

            /////////////////////////////////////////////////////////////////////////////

            // 近战武器
            new WeaponName(){ English="Royal Sabre", Chinese="军刀", ShortTxt="Royal Sabre" },

            /////////////////////////////////////////////////////////////////////////////
            
            // 手雷
            new WeaponName(){ English="Gas Grenade", Chinese="毒气手榴弹", ShortTxt="Gas GREN" },
            new WeaponName(){ English="Incendiary Grenade", Chinese="燃烧手榴弹", ShortTxt="Incendiary GREN" },
            new WeaponName(){ English="Smoke Grenade", Chinese="烟雾手榴弹", ShortTxt="Smoke GREN" },
            new WeaponName(){ English="Mini Grenade", Chinese="小型手榴弹", ShortTxt="Mini GREN" },
            new WeaponName(){ English="Frag Grenade", Chinese="棒式手榴弹", ShortTxt="Frag GREN" },
            new WeaponName(){ English="Impact Grenade", Chinese="冲击手榴弹", ShortTxt="Impact GREN" },

            /////////////////////////////////////////////////////////////////////////////

            // 手枪
            new WeaponName(){ English="Obrez", Chinese="手枪 Obrez手枪", ShortTxt="Obrez" },
            new WeaponName(){ English="Webley Fosbery Auto Revolver", Chinese="手枪 自动左轮手枪", ShortTxt="Auto Revolver" },
            new WeaponName(){ English="Webley MkVI", Chinese="手枪 Mk VI 左轮手枪", ShortTxt="MkVI" },
            new WeaponName(){ English="Beretta M1915", Chinese="手枪 费罗梅尔停止手枪", ShortTxt="M1915" },
            new WeaponName(){ English="The Greatest Handgun Ever Made", Chinese="手枪 和平捍卫者手枪", ShortTxt="Greatest Handgun" },
            new WeaponName(){ English="Colt M1911", Chinese="手枪 M1911", ShortTxt="M1911" },
            new WeaponName(){ English="Borchardt C93", Chinese="手枪 C93", ShortTxt="C93" },
            new WeaponName(){ English="Bulldog", Chinese="手枪 斗牛犬左轮手枪", ShortTxt="Bulldog" },
            new WeaponName(){ English="Mauser C96", Chinese="手枪 C96", ShortTxt="C96" },
            new WeaponName(){ English="Gasser M1870", Chinese="手枪 加塞 M1870", ShortTxt="M1870" },
            new WeaponName(){ English="FN1903", Chinese="手枪 Mle 1903", ShortTxt="FN1903" },
            new WeaponName(){ English="Luger P08", Chinese="手枪 P08手枪", ShortTxt="P08" },
            new WeaponName(){ English="Mars Auto Pistol", Chinese="手枪 Mars自动手枪", ShortTxt="MarsAuto" },
            new WeaponName(){ English="Steyr M1912", Chinese="手枪 斯太尔M1912", ShortTxt="M1912" },

            /////////////////////////////////////////////////////////////////////////////

            // 突击兵
            new WeaponName(){ English="Maxim SMG", Chinese="突击兵 SMG 08/18", ShortTxt="SMG0818" },
            new WeaponName(){ English="Remington M10", Chinese="突击兵 Model 10-A", ShortTxt="10A" },
            new WeaponName(){ English="Thompson", Chinese="突击兵 汤姆逊冲锋枪", ShortTxt="Thompson" },
            new WeaponName(){ English="Beretta M1918", Chinese="突击兵 M1918 自动冲锋枪", ShortTxt="M1918" },
            new WeaponName(){ English="BergmanMP18", Chinese="突击兵 MP 18", ShortTxt="MP18" },
            new WeaponName(){ English="Ribeyrolles", Chinese="突击兵 Ribeyrolles 1918", ShortTxt="Ribeyrolles" },
            new WeaponName(){ English="Hellriegel 1915", Chinese="突击兵 Hellriegel 1915", ShortTxt="1915" },
            new WeaponName(){ English="Browning A5", Chinese="突击兵 12g 自动霰弹枪", ShortTxt="12g" },
            new WeaponName(){ English="Remington 1900", Chinese="突击兵 Remington 1900", ShortTxt="1900" },
            new WeaponName(){ English="M1912/P.16", Chinese="突击兵 斯太尔 M1912/P.16", ShortTxt="M1912" },
            new WeaponName(){ English="Winchester 1897", Chinese="突击兵 地狱战士战壕霰弹枪", ShortTxt="1897" },
            new WeaponName(){ English="CSRG SMG", Chinese="突击兵 RSC 冲锋枪", ShortTxt="CSRG RSC" },

            new WeaponName(){ English="Dynamite", Chinese="炸药", ShortTxt="Dynamite" },
            new WeaponName(){ English="AntiChars", Chinese="反坦克手雷", ShortTxt="AntiChars" },
            new WeaponName(){ English="TankGewehr", Chinese="反坦克火箭炮/M1918反坦克步枪", ShortTxt="TankGewehr" },
            new WeaponName(){ English="ATMine", Chinese="反坦克地雷/磁吸地雷", ShortTxt="ATMine" },

            /////////////////////////////////////////////////////////////////////////////

            // 医疗兵
            new WeaponName(){ English="Winchester M1907 SL", Chinese="医疗兵 M1907/费德洛夫", ShortTxt="M1907 SL" },
            new WeaponName(){ English="RSC 1917", Chinese="医疗兵 RSC 1917", ShortTxt="RSC 1917" },
            new WeaponName(){ English="BSA Howell", Chinese="医疗兵 Howell 自动步枪", ShortTxt="Howell" },
            new WeaponName(){ English="CeiRigotti M1985", Chinese="医疗兵 Cei-Rigotti", ShortTxt="M1985" },
            new WeaponName(){ English="Remington Model 8", Chinese="医疗兵 自动装填步枪 8.25", ShortTxt="8.25" },
            new WeaponName(){ English="Mondragon", Chinese="医疗兵 蒙德拉贡步枪", ShortTxt="Mondragon" },
            new WeaponName(){ English="Mauser SL1916", Chinese="医疗兵 Selbstlader M1916", ShortTxt="M1916" },
            new WeaponName(){ English="Luger 1906", Chinese="医疗兵 Selbstlader 1906", ShortTxt="1906" },
            new WeaponName(){ English="General Liu", Chinese="医疗兵 刘将军步枪", ShortTxt="Liu" },
            new WeaponName(){ English="FarquharHill", Chinese="医疗兵 Farquhar-Hill 步枪", ShortTxt="FarquharHill" },

            new WeaponName(){ English="PortableMedicpack", Chinese="绷带包", ShortTxt="" },
            new WeaponName(){ English="Medic Bag", Chinese="医疗箱", ShortTxt="Medic Bag" },
            new WeaponName(){ English="Syringe", Chinese="医疗用针筒", ShortTxt="Syringe" },

            /////////////////////////////////////////////////////////////////////////////

            // 支援兵
            new WeaponName(){ English="Bergmann MG15", Chinese="支援兵 MG15 n.A./M1917机枪", ShortTxt="MG15" },
            new WeaponName(){ English="MG0818", Chinese="支援兵 轻机枪 08/18", ShortTxt="MG0818" },
            new WeaponName(){ English="Perino M1908", Chinese="支援兵 Perino Model M1908", ShortTxt="M1908" },
            new WeaponName(){ English="Burton LMR", Chinese="支援兵 波顿 LMR", ShortTxt="Burton LMR" },
            new WeaponName(){ English="Parabellum", Chinese="支援兵 Parabellum MG14/17", ShortTxt="MG1417" },
            new WeaponName(){ English="Hotchkiss M1909", Chinese="支援兵 M1909 贝内特·梅西耶机枪", ShortTxt="M1909" },
            new WeaponName(){ English="Chauchat", Chinese="支援兵 邵沙轻机枪", ShortTxt="Chauchat" },
            new WeaponName(){ English="LewisMG", Chinese="支援兵 Huot 自动步枪/路易士机枪", ShortTxt="LewisMG" },
            new WeaponName(){ English="Mauser C96 Auto Pistol", Chinese="支援兵 C96", ShortTxt="C96" },
            new WeaponName(){ English="FN1903 Stock", Chinese="支援兵 Mle 1903", ShortTxt="Mle 1903" },
            new WeaponName(){ English="MadsenMG", Chinese="支援兵 麦德森机枪", ShortTxt="MadsenMG" },

            new WeaponName(){ English="SmallAmmopack", Chinese="弹药包", ShortTxt="Ammopack" },
            new WeaponName(){ English="AmmoCrate", Chinese="弹药箱", ShortTxt="AmmoCrate" },
            new WeaponName(){ English="ID_P_INAME_U_MORTAR", Chinese="空爆迫击炮", ShortTxt="MORTAR" },
            new WeaponName(){ English="ID_P_INAME_MORTAR_HE", Chinese="高爆迫击炮", ShortTxt="MORTAR HE" },
            new WeaponName(){ English="Mortar", Chinese="迫击炮", ShortTxt="Mortar" },
            new WeaponName(){ English="Wrench", Chinese="维修工具", ShortTxt="Wrench" },
            new WeaponName(){ English="RifleGrenadeLauncher", Chinese="十字弓发射器", ShortTxt="Rifle GL" },

            /////////////////////////////////////////////////////////////////////////////

            // 侦察兵
            new WeaponName(){ English="Winchester M1895", Chinese="侦察兵/骑兵 Russian 1895", ShortTxt="M1895" },

            new WeaponName(){ English="Ross MkIII", Chinese="侦察兵 罗斯 MkIII", ShortTxt="Ross MkIII" },
            new WeaponName(){ English="Arisaka", Chinese="侦察兵 三八式步枪", ShortTxt="Arisaka" },
            new WeaponName(){ English="Martini Henry", Chinese="侦察兵 马提尼·亨利步枪", ShortTxt="Martini Henry" },
            new WeaponName(){ English="Gewehr 98", Chinese="侦察兵 Gewehr 98", ShortTxt="G98" },
            new WeaponName(){ English="LeeEnfieldSMLE", Chinese="侦察兵 SMLE MKIII", ShortTxt="Lee SMLE" },
            new WeaponName(){ English="Springfield M1903", Chinese="侦察兵 M1903", ShortTxt="M1903" },
            new WeaponName(){ English="M1917 Enfield", Chinese="侦察兵 M1917 Enfield", ShortTxt="M1917" },
            new WeaponName(){ English="Steyr mannlicher 1895", Chinese="侦察兵 Gewehr M.95", ShortTxt="M95" },
            new WeaponName(){ English="Lebel 1886", Chinese="侦察兵 勒贝尔 M1886", ShortTxt="M1886" },
            new WeaponName(){ English="CarcanoCarbine", Chinese="侦察兵 卡尔卡诺 M91 卡宾枪", ShortTxt="M91" },

            new WeaponName(){ English="TripWireBomb", Chinese="绊索炸弹", ShortTxt="TripWireBomb" },
            new WeaponName(){ English="Webbey Scott Flare Gun", Chinese="信号枪", ShortTxt="Signal Pistol" },
            new WeaponName(){ English="TrenchPeriscope1", Chinese="战壕潜望镜", ShortTxt="Trench Periscope" },

            /////////////////////////////////////////////////////////////////////////////

            // 载具
            new WeaponName(){ English="ID_P_VNAME_MARKV", Chinese="载具 Mark V 巡航坦克", ShortTxt="Mark V" },

            new WeaponName(){ English="ID_P_VNAME_A7V", Chinese="重型坦克 AV7 重型坦克", ShortTxt="AV7" },

            new WeaponName(){ English="ID_P_VNAME_FT17", Chinese="轻型坦克 FT-17 轻型坦克", ShortTxt="FT-17" },

            new WeaponName(){ English="ID_P_VNAME_ARTILLERYTRUCK", Chinese="载具 火炮装甲车", ShortTxt="ARTILLERYTRUCK" },

            new WeaponName(){ English="ID_P_VNAME_STCHAMOND", Chinese="攻击坦克 圣沙蒙", ShortTxt="STCHAMOND" },

            new WeaponName(){ English="ID_P_VNAME_ASSAULTTRUCK", Chinese="突袭装甲车 朴帝洛夫·加福德", ShortTxt="ASSAULTTRUCK" },

            ////////////////

            new WeaponName(){ English="ID_P_VNAME_HALBERSTADT", Chinese="攻击机 哈尔伯施塔特 CL.II 攻击机", ShortTxt="HALBERSTADT" },
            new WeaponName(){ English="ID_P_VNAME_RUMPLER", Chinese="攻击机 Rumpler C.I 攻击机", ShortTxt="RUMPLER" },
            new WeaponName(){ English="ID_P_VNAME_BRISTOL", Chinese="攻击机 布里斯托 F2.B 攻击机", ShortTxt="BRISTOL" },
            new WeaponName(){ English="ID_P_VNAME_SALMSON", Chinese="攻击机 A.E.F 2-A2 攻击机", ShortTxt="SALMSON" },

            new WeaponName(){ English="ID_P_VNAME_DH10", Chinese="轰炸机 Airco DH.10 轰炸机", ShortTxt="DH10" },
            new WeaponName(){ English="ID_P_VNAME_HBG1", Chinese="轰炸机 汉莎·布兰登堡 G.I 轰炸机", ShortTxt="HBG1" },
            new WeaponName(){ English="ID_P_VNAME_CAPRONI", Chinese="轰炸机 卡普罗尼 CA.5 轰炸机", ShortTxt="CAPRONI" },
            new WeaponName(){ English="ID_P_VNAME_GOTHA", Chinese="轰炸机 戈塔 G 轰炸机", ShortTxt="GOTHA" },

            new WeaponName(){ English="ID_P_VNAME_SOPWITH", Chinese="战斗机 索普维斯骆驼式战斗机", ShortTxt="SOPWITH" },
            new WeaponName(){ English="ID_P_VNAME_ALBATROS", Chinese="战斗机 信天翁 D-III 战斗机", ShortTxt="ALBATROS" },
            new WeaponName(){ English="ID_P_VNAME_DR1", Chinese="战斗机 DR.1 战斗机", ShortTxt="DR1" },
            new WeaponName(){ English="ID_P_VNAME_SPAD", Chinese="战斗机 SPAD S XIII 战斗机", ShortTxt="SPAD" },

            new WeaponName(){ English="ID_P_VNAME_ILYAMUROMETS", Chinese="重型轰炸机 伊利亚·穆罗梅茨", ShortTxt="ILYAMUROMETS" },

            ////////////////

            new WeaponName(){ English="ID_P_VNAME_HORSE", Chinese="骑兵 战马", ShortTxt="HORSE" },

            /////////////////////////////////////////////////////////////////////////////

            // 运输载具
            new WeaponName(){ English="ID_P_VNAME_NSU", Chinese="运输载具 MC 3.5HP 附边车摩托车", ShortTxt="NSU" },
            new WeaponName(){ English="ID_P_VNAME_MOTORCYCLE", Chinese="运输载具 MC 18J 附边车摩托车", ShortTxt="MOTORCYCLE" },
            new WeaponName(){ English="ID_P_VNAME_EHRHARDT", Chinese="运输载具 EV4 装甲车", ShortTxt="EHRHARDT" },
            new WeaponName(){ English="ID_P_VNAME_ROMFELL", Chinese="运输载具 Romfell 装甲车", ShortTxt="ROMFELL" },
            new WeaponName(){ English="ID_P_VNAME_ROLLS", Chinese="运输载具 RNAS 装甲车", ShortTxt="ROLLS" },
            new WeaponName(){ English="ID_P_VNAME_MODEL30", Chinese="运输载具 M30 侦察车", ShortTxt="MODEL30" },

            new WeaponName(){ English="ID_P_VNAME_MAS15", Chinese="运输载具 M.A.S 鱼雷快艇", ShortTxt="MAS15" },

            /////////////////////////////////////////////////////////////////////////////

            // 精英兵
            new WeaponName(){ English="MaximMG0815", Chinese="哨兵 MG 08/15", ShortTxt="MaximMG0815" },

            new WeaponName(){ English="Royal Club", Chinese="战壕奇兵 奇兵棒", ShortTxt="Royal Club" },
            new WeaponName(){ English="FlameThrower", Chinese="喷火兵 Wex 火焰喷射器", ShortTxt="Wex" },
            new WeaponName(){ English="MartiniGrenadeLauncher", Chinese="入侵者 马提尼·亨利步兵榴弹发射器", ShortTxt="Martini GL" },

            new WeaponName(){ English="SpawnBeacon", Chinese="重生信标", ShortTxt="SpawnBeacon" },

            /////////////////////////////////////////////////////////////////////////////

            // 定点武器
            new WeaponName(){ English="ID_P_VNAME_BL9", Chinese="定点武器 BL 9.2 攻城炮", ShortTxt="BL9" },
            new WeaponName(){ English="ID_P_VNAME_TURRET", Chinese="定点武器 堡垒火炮", ShortTxt="TURRET" },
            new WeaponName(){ English="ID_P_VNAME_AASTATION", Chinese="定点武器 QF 1 防空炮", ShortTxt="AASTATION" },
            new WeaponName(){ English="ID_P_VNAME_FIELDGUN", Chinese="定点武器 FK 96 野战炮", ShortTxt="FIELDGUN" },
            new WeaponName(){ English="ID_P_INAME_MAXIM", Chinese="定点武器 重机枪", ShortTxt="MAXIM" },

            /////////////////////////////////////////////////////////////////////////////

            // 战争巨兽
            new WeaponName(){ English="ID_P_VNAME_ARMOREDTRAIN", Chinese="战争巨兽 装甲列车", ShortTxt="ARMOREDTRAIN" },
            new WeaponName(){ English="ID_P_VNAME_ZEPPELIN", Chinese="战争巨兽 飞船 L30", ShortTxt="ZEPPELIN" },
            new WeaponName(){ English="ID_P_VNAME_IRONDUKE", Chinese="战争巨兽 无畏舰", ShortTxt="IRONDUKE" },
            new WeaponName(){ English="ID_P_VNAME_CHAR", Chinese="战争巨兽 Char 2C", ShortTxt="CHAR" },
        };
    }
}
