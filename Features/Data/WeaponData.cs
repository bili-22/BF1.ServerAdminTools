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
        
        /// <summary>
        /// 全部武器信息
        /// </summary>
        public static List<WeaponName> AllWeaponInfo = new List<WeaponName>()
        {
            // 配枪
            new WeaponName(){ English="U_M1911", Chinese="M1911", ShortTxt="12g JC" },
            new WeaponName(){ English="U_LugerP08", Chinese="P08 手槍", ShortTxt="12g JC" },
            new WeaponName(){ English="U_FN1903", Chinese="Mle 1903", ShortTxt="12g JC" },
            new WeaponName(){ English="U_BorchardtC93", Chinese="C93", ShortTxt="12g JC" },
            new WeaponName(){ English="U_SmithWesson", Chinese="3 號左輪手槍", ShortTxt="12g JC" },
            new WeaponName(){ English="U_Kolibri", Chinese="Kolibri", ShortTxt="12g JC" },
            new WeaponName(){ English="U_NagantM1895", Chinese="納甘左輪手槍", ShortTxt="12g JC" },
            new WeaponName(){ English="U_Obrez", Chinese="Obrez 手槍", ShortTxt="12g JC" },
            new WeaponName(){ English="U_Webley_Mk6", Chinese="Mk VI 左輪手槍", ShortTxt="12g JC" },

            new WeaponName(){ English="U_M1911_Preorder_Hellfighter", Chinese="地獄戰士 M1911", ShortTxt="12g JC" },
            new WeaponName(){ English="U_LugerP08_Wep_Preorder", Chinese="紅男爵的 P08", ShortTxt="12g JC" },
            new WeaponName(){ English="U_M1911_Suppressed", Chinese="M1911（消音器）", ShortTxt="12g JC" },

            new WeaponName(){ English="U_SingleActionArmy", Chinese="维和左轮 Peacekeeper", ShortTxt="12g JC" },

            // 手榴弹
            new WeaponName(){ English="U_FragGrenade", Chinese="棒式手榴彈", ShortTxt="12g JC" },
            new WeaponName(){ English="U_GermanStick", Chinese="破片手榴彈", ShortTxt="12g JC" },
            new WeaponName(){ English="U_GasGrenade", Chinese="毒氣手榴彈", ShortTxt="12g JC" },
            new WeaponName(){ English="U_ImpactGrenade", Chinese="衝擊手榴彈", ShortTxt="12g JC" },
            new WeaponName(){ English="U_Incendiary", Chinese="燃燒手榴彈", ShortTxt="12g JC" },
            new WeaponName(){ English="U_MiniGrenade", Chinese="小型手榴彈", ShortTxt="12g JC" },
            new WeaponName(){ English="U_SmokeGrenade", Chinese="煙霧手榴彈", ShortTxt="12g JC" },
            new WeaponName(){ English="U_Grenade_AT", Chinese="輕型反坦克手榴彈", ShortTxt="12g JC" },

            new WeaponName(){ English="U_ImprovisedGrenade", Chinese="土製手榴彈", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="俄羅斯標準手榴彈", ShortTxt="12g JC" },

            new WeaponName(){ English="U_Incendiary_Hero", Chinese="喷火兵 燃燒手榴彈", ShortTxt="12g JC" },

            // 近战
            new WeaponName(){ English="123", Chinese="野戰刀", ShortTxt="12g JC" },

            // 其他
            new WeaponName(){ English="U_GasMask", Chinese="防毒面具", ShortTxt="Gas Mask" },

            ////////////////////////////////// 突击兵 Assault //////////////////////////////////

            // 主要武器
            new WeaponName(){ English="U_RemingtonM10_Wep_Slug", Chinese="Model 10-A（霰彈塊）", ShortTxt="10A XDK" },
            new WeaponName(){ English="U_RemingtonM10_Wep_Choke", Chinese="Model 10-A（獵人）", ShortTxt="10A LR" },
            new WeaponName(){ English="U_RemingtonM10", Chinese="Model 10-A（原廠）", ShortTxt="10A YC" },
            new WeaponName(){ English="U_Winchester1897_Wep_Sweeper", Chinese="M97 戰壕槍（掃蕩）", ShortTxt="M97 SD" },
            new WeaponName(){ English="U_Winchester1897_Wep_LowRecoil", Chinese="M97 戰壕槍（Back-Bored）", ShortTxt="M97 BB" },
            new WeaponName(){ English="U_Winchester1897_Wep_Choke", Chinese="M97 戰壕槍（獵人）", ShortTxt="M97 LR" },
            new WeaponName(){ English="U_MP18_Wep_Trench", Chinese="MP 18（壕溝戰）", ShortTxt="MP18 HGZ" },
            new WeaponName(){ English="U_MP18_Wep_Burst", Chinese="MP 18（實驗）", ShortTxt="MP18 SY" },
            new WeaponName(){ English="U_MP18_Wep_Accuracy", Chinese="MP 18（瞄準鏡）", ShortTxt="MP18 MZJ" },
            new WeaponName(){ English="U_BerettaM1918_Wep_Trench", Chinese="M1918 自動衝鋒槍（壕溝戰）", ShortTxt="MP1918 HGZ" },
            new WeaponName(){ English="U_BerettaM1918_Wep_Stability", Chinese="M1918 自動衝鋒槍（衝鋒）", ShortTxt="MP1918 CF" },
            new WeaponName(){ English="U_BerettaM1918", Chinese="M1918 自動衝鋒槍（原廠）", ShortTxt="MP1918 YC" },
            new WeaponName(){ English="U_BrowningA5_Wep_LowRecoil", Chinese="12g 自動霰彈槍（Back-Bored）", ShortTxt="12g BB" },
            new WeaponName(){ English="U_BrowningA5_Wep_Choke", Chinese="12g 自動霰彈槍（獵人）", ShortTxt="12g LR" },
            new WeaponName(){ English="U_BrowningA5_Wep_ExtensionTube", Chinese="12g 自動霰彈槍（加長）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_Hellriegel1915", Chinese="Hellriegel 1915（原廠）", ShortTxt="H1915 YC" },
            new WeaponName(){ English="U_Hellriegel1915_Wep_Accuracy", Chinese="Hellriegel 1915（防禦）", ShortTxt="H1915 FY" },
            new WeaponName(){ English="U_Winchester1897_Wep_Preorder", Chinese="地獄戰士戰壕霰彈槍", ShortTxt="M97 DYZS" },
            new WeaponName(){ English="U_SjogrenShotgun", Chinese="Sjögren Inertial（原廠）", ShortTxt="H1915 FY" },
            new WeaponName(){ English="U_SjogrenShotgun_Wep_Slug", Chinese="Sjögren Inertial（霰彈塊）", ShortTxt="H1915 FY" },
            new WeaponName(){ English="U_Ribeyrolles", Chinese="利貝羅勒 1918（原廠）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_Ribeyrolles_Wep_Optical", Chinese="Ribeyrolles 1918（瞄準鏡）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_RemingtonModel1900", Chinese="Model 1900（原廠）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_RemingtonModel1900_Wep_Slug", Chinese="Model 1900（霰彈塊）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_MaximSMG", Chinese="SMG 08/18（原廠）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_MaximSMG_Wep_Accuracy", Chinese="SMG 08/18（瞄準鏡）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_SteyrM1912_P16", Chinese="M1912/P.16（衝鋒）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_SteyrM1912_P16_Wep_Burst", Chinese="Maschinenpistole M1912/P.16（實驗）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_Mauser1917Trench", Chinese="M1917 戰壕卡賓槍", ShortTxt="12g JC" },
            new WeaponName(){ English="U_Mauser1917Trench_Wep_Scope", Chinese="M1917 卡賓槍（巡邏）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_ChauchatSMG", Chinese="RSC 衝鋒槍（原廠）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_ChauchatSMG_Wep_Optical", Chinese="RSC 衝鋒槍（瞄準鏡）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_M1919Thompson_Wep_Trench", Chinese="Annihilator（壕溝）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_M1919Thompson_Wep_Stability", Chinese="Annihilator（衝鋒）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_FrommerStopAuto", Chinese="費羅梅爾停止手槍（自動）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_SawnOffShotgun", Chinese="短管霰彈槍", ShortTxt="12g JC" },

            // 配枪
            new WeaponName(){ English="U_GasserM1870", Chinese="加塞 M1870", ShortTxt="12g JC" },
            new WeaponName(){ English="U_LancasterHowdah", Chinese="Howdah 手槍", ShortTxt="12g JC" },
            new WeaponName(){ English="U_Hammerless", Chinese="1903 Hammerless", ShortTxt="12g JC" },

            // 配备一二
            new WeaponName(){ English="U_Dynamite", Chinese="炸藥", ShortTxt="12g JC" },
            new WeaponName(){ English="U_ATGrenade", Chinese="反坦克手榴彈", ShortTxt="12g JC" },
            new WeaponName(){ English="U_ATMine", Chinese="反坦克地雷", ShortTxt="12g JC" },
            new WeaponName(){ English="U_BreechGun", Chinese="反坦克火箭砲", ShortTxt="12g JC" },
            new WeaponName(){ English="U_BreechGun_Flak", Chinese="防空火箭砲", ShortTxt="12g JC" },

            ////////////////////////////////// 医疗兵 Medic   //////////////////////////////////

            // 主要武器
            new WeaponName(){ English="U_CeiRigottiM1895_Wep_Trench", Chinese="Cei-Rigotti（壕溝戰）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_CeiRigottiM1895_Wep_Range", Chinese="Cei-Rigotti（瞄準鏡）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_CeiRigottiM1895", Chinese="Cei-Rigotti（原廠）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_MauserSL1916_Wep_Scope", Chinese="Selbstlader M1916（神射手）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_MauserSL1916_Wep_Range", Chinese="Selbstlader M1916（瞄準鏡）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_MauserSL1916", Chinese="Selbstlader M1916（原廠）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_WinchesterM1907_Wep_Trench", Chinese="M1907 半自動步槍（壕溝戰）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_WinchesterM1907_Wep_Auto", Chinese="M1907 半自動步槍（掃蕩）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_WinchesterM1907", Chinese="M1907 半自動步槍（原廠）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_Mondragon_Wep_Range", Chinese="蒙德拉貢步槍（瞄準鏡）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_Mondragon_Wep_Stability", Chinese="蒙德拉貢步槍（衝鋒）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_Mondragon_Wep_Bipod", Chinese="蒙德拉貢步槍（狙擊手）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_RemingtonModel8", Chinese="自動裝填步槍 8.35（原廠）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_RemingtonModel8_Wep_Scope", Chinese="自動裝填步槍 8.35（神射手）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_RemingtonModel8_Wep_ExtendedMag", Chinese="自動裝填步槍 8.25（加長）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_Luger1906", Chinese="Selbstlader 1906（原廠）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_Luger1906_Wep_Scope", Chinese="Selbstlader 1906（狙擊手）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_RSC1917_Wep_Range", Chinese="RSC 1917（瞄準鏡）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_RSC1917", Chinese="RSC 1917（原廠）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_FedorovAvtomat_Wep_Trench", Chinese="費德洛夫自動步槍（壕溝戰）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_FedorovAvtomat_Wep_Range", Chinese="費德洛夫自動步槍（瞄準鏡）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_GeneralLiuRifle", Chinese="劉將軍步槍（原廠）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_GeneralLiuRifle_Wep_Stability", Chinese="劉將軍步槍（衝鋒）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_FarquharHill_Wep_Range", Chinese="Farquhar-Hill 步槍（瞄準鏡）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_FarquharHill_Wep_Stability", Chinese="Farquhar-Hill 步槍（衝鋒）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_BSAHowellM1916", Chinese="Howell 自動步槍（原廠）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_BSAHowellM1916_Wep_Scope", Chinese="Howell 自動步槍（狙擊手）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_FedorovDegtyarev", Chinese="費德洛夫 Degtyarev", ShortTxt="12g JC" },

            // 配枪
            new WeaponName(){ English="U_WebFosAutoRev_455Webley", Chinese="自動左輪手槍", ShortTxt="12g JC" },
            new WeaponName(){ English="U_MauserC96", Chinese="C96", ShortTxt="12g JC" },
            new WeaponName(){ English="U_Mauser1914", Chinese="Taschenpistole M1914", ShortTxt="12g JC" },

            // 配备一二
            new WeaponName(){ English="U_Syringe", Chinese="醫療用針筒", ShortTxt="12g JC" },
            new WeaponName(){ English="U_MedicBag", Chinese="醫護箱", ShortTxt="12g JC" },
            new WeaponName(){ English="U_Bandages", Chinese="繃帶包", ShortTxt="12g JC" },

            // 步枪手榴弹（破片）
            new WeaponName(){ English="U_CeiRigottiM1895_RGL_Frag", Chinese="步槍手榴彈（破片）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_GeneralLiuRifle_RGL_Frag", Chinese="步槍手榴彈（破片）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_RSC1917_RGL_Frag", Chinese="步槍手榴彈（破片）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_FedorovAvtomat_RGL_Frag", Chinese="步槍手榴彈（破片）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_CeiRigottiM1895_RGL_Frag_Grip", Chinese="步槍手榴彈（破片）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_MauserSL1916_RGL_Frag_Grip", Chinese="步槍手榴彈（破片）", ShortTxt="12g JC" },
            
            // 步枪手榴弹（煙霧）
            new WeaponName(){ English="U_MauserSL1916_RGL_Smoke_Grip", Chinese="步槍手榴彈（煙霧）", ShortTxt="12g JC" },
            
            // 步枪手榴弹（高爆）
            new WeaponName(){ English="U_MauserSL1916_RGL_HE", Chinese="步槍手榴彈（高爆）", ShortTxt="12g JC" },

            ////////////////////////////////// 支援兵 Support //////////////////////////////////

            // 主要武器
            new WeaponName(){ English="U_LewisMG_Wep_Suppression", Chinese="路易士機槍（壓制）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_LewisMG_Wep_Range", Chinese="路易士機槍（瞄準鏡）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_LewisMG", Chinese="路易士機槍（輕量化）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_HotchkissM1909_Wep_Stability", Chinese="M1909 貝內特·梅西耶機槍（衝鋒）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_HotchkissM1909_Wep_Range", Chinese="M1909 貝內特·梅西耶機槍（瞄準鏡）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_HotchkissM1909_Wep_Bipod", Chinese="M1909 貝內特·梅西耶機槍（望遠瞄具）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_MadsenMG_Wep_Trench", Chinese="麥德森機槍（壕溝戰）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_MadsenMG_Wep_Stability", Chinese="麥德森機槍（衝鋒）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_MadsenMG", Chinese="麥德森機槍（輕量化）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_Bergmann1915MG_Wep_Suppression", Chinese="MG15 n.A.（壓制）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_Bergmann1915MG_Wep_Stability", Chinese="MG15 n.A.（衝鋒）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_Bergmann1915MG", Chinese="MG15 n.A.（輕量化）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_BARM1918_Wep_Trench", Chinese="M1918 白朗寧自動步槍（壕溝戰）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_BARM1918_Wep_Stability", Chinese="M1918 白朗寧自動步槍（衝鋒）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_BARM1918_Wep_Bipod", Chinese="M1918 白朗寧自動步槍（望遠瞄具）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_HuotAutoRifle", Chinese="Huot 自動步槍（輕量化）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_HuotAutoRifle_Wep_Range", Chinese="Huot 自動步槍（瞄準鏡）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_Chauchat", Chinese="紹沙輕機槍（輕量化）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_Chauchat_Wep_Bipod", Chinese="紹沙輕機槍（望遠瞄具）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_ParabellumLMG", Chinese="Parabellum MG14/17（輕量化）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_ParabellumLMG_Wep_Suppression", Chinese="Parabellum MG14/17（壓制）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_PerinoM1908", Chinese="Perino Model 1908（輕量化）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_PerinoM1908_Wep_Defensive", Chinese="Perino Model 1908（防禦）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_BrowningM1917", Chinese="M1917 機槍（輕量化）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_BrowningM1917_Wep_Suppression", Chinese="M1917 機槍（望遠瞄具）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_MG0818", Chinese="輕機槍 08/18（輕量化）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_MG0818_Wep_Defensive", Chinese="輕機槍 08/18（壓制）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_WinchesterBurton_Wep_Trench", Chinese="波頓 LMR（戰壕）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_WinchesterBurton_Wep_Optical", Chinese="波頓 LMR（瞄準鏡）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_MauserC96AutoPistol", Chinese="C96（卡賓槍）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_LugerArtillery", Chinese="P08 Artillerie", ShortTxt="12g JC" },
            new WeaponName(){ English="U_PieperCarbine", Chinese="皮珀 M1893", ShortTxt="12g JC" },
            new WeaponName(){ English="U_M1911_Stock", Chinese="M1911（加長）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_FN1903stock", Chinese="Mle 1903（加長）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_C93Carbine", Chinese="C93（卡賓槍）", ShortTxt="12g JC" },

            // 配枪
            new WeaponName(){ English="U_SteyrM1912", Chinese="Repetierpistole M1912", ShortTxt="12g JC" },
            new WeaponName(){ English="U_Bulldog", Chinese="鬥牛犬左輪手槍", ShortTxt="12g JC" },
            new WeaponName(){ English="U_BerettaM1915", Chinese="Modello 1915", ShortTxt="12g JC" },

            // 配备一二
            new WeaponName(){ English="U_AmmoCrate", Chinese="彈藥箱", ShortTxt="12g JC" },
            new WeaponName(){ English="U_AmmoPouch", Chinese="彈藥包", ShortTxt="12g JC" },
            new WeaponName(){ English="U_Mortar", Chinese="迫擊砲（空爆）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_Mortar_HE", Chinese="迫擊砲（高爆）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_Wrench", Chinese="維修工具", ShortTxt="12g JC" },
            new WeaponName(){ English="U_LimpetMine", Chinese="磁吸地雷", ShortTxt="12g JC" },
            new WeaponName(){ English="U_Crossbow", Chinese="十字弓發射器（破片）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_Crossbow_HE", Chinese="十字弓發射器（高爆）", ShortTxt="12g JC" },

            ////////////////////////////////// 侦察兵 Scout   //////////////////////////////////

            // 主要武器
            new WeaponName(){ English="U_WinchesterM1895_Wep_Trench", Chinese="Russian 1895（壕溝戰）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_WinchesterM1895_Wep_Long", Chinese="Russian 1895（狙擊手）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_WinchesterM1895", Chinese="Russian 1895（步兵）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_Gewehr98_Wep_Scope", Chinese="Gewehr 98（神射手）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_Gewehr98_Wep_LongRange", Chinese="Gewehr 98（狙擊手）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_Gewehr98", Chinese="Gewehr 98（步兵）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_LeeEnfieldSMLE_Wep_Scope", Chinese="SMLE MKIII（神射手）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_LeeEnfieldSMLE_Wep_Med", Chinese="SMLE MKIII（卡賓槍）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_LeeEnfieldSMLE", Chinese="SMLE MKIII（步兵）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_SteyrManM1895_Wep_Scope", Chinese="Gewehr M.95（神射手）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_SteyrManM1895_Wep_Med", Chinese="Gewehr M.95（卡賓槍）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_SteyrManM1895", Chinese="Gewehr M.95（步兵）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_SpringfieldM1903_Wep_Scope", Chinese="M1903（神射手）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_SpringfieldM1903_Wep_LongRange", Chinese="M1903（狙擊手）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_SpringfieldM1903_Wep_Pedersen", Chinese="M1903（實驗）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_MartiniHenry", Chinese="馬提尼·亨利步槍（步兵）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_MartiniHenry_Wep_LongRange", Chinese="馬提尼·亨利步槍（狙擊手）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_LeeEnfieldSMLE_Wep_Preorder", Chinese="阿拉伯的勞倫斯的 SMLE", ShortTxt="12g JC" },
            new WeaponName(){ English="U_Lebel1886_Wep_LongRange", Chinese="勒貝爾 M1886（狙擊手）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_Lebel1886", Chinese="勒貝爾 M1886（步兵）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_MosinNagant1891", Chinese="莫辛-納甘 M91（步兵）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_MosinNagant1891_Wep_Scope", Chinese="莫辛-納甘 M91（神射手）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_VetterliVitaliM1870", Chinese="Vetterli-Vitali M1870/87（步兵）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_VetterliVitaliM1870_Wep_Med", Chinese="Vetterli-Vitali M1870/87（卡賓槍）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_Type38Arisaka", Chinese="三八式步槍（步兵）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_Type38Arisaka_Wep_Scope", Chinese="三八式步槍（巡邏）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_CarcanoCarbine", Chinese="卡爾卡諾 M91 卡賓槍", ShortTxt="12g JC" },
            new WeaponName(){ English="U_CarcanoCarbine_Wep_Scope", Chinese="卡爾卡諾 M91 卡賓槍（巡邏）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_RossMkIII", Chinese="羅斯 MKIII（步兵）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_RossMkIII_Wep_Scope", Chinese="羅斯 MKIII（神射手）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_Enfield1917", Chinese="M1917 Enfield（步兵）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_Enfield1917_Wep_LongRange", Chinese="M1917 Enfield（消音器）", ShortTxt="12g JC" },

            // 配枪
            new WeaponName(){ English="U_MarsAutoPistol", Chinese="Mars 自動手槍", ShortTxt="12g JC" },
            new WeaponName(){ English="U_Bodeo1889", Chinese="Bodeo 1889", ShortTxt="12g JC" },
            new WeaponName(){ English="U_FrommerStop", Chinese="費羅梅爾停止手槍", ShortTxt="12g JC" },

            // 配备一二
            new WeaponName(){ English="U_FlareGun", Chinese="信號槍（偵察）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_FlareGun_Flash", Chinese="信號槍（閃光）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_TrPeriscope", Chinese="戰壕潛望鏡", ShortTxt="12g JC" },
            new WeaponName(){ English="U_Shield", Chinese="狙擊手護盾", ShortTxt="12g JC" },
            new WeaponName(){ English="U_HelmetDecoy", Chinese="狙擊手誘餌", ShortTxt="12g JC" },
            new WeaponName(){ English="U_TripWireBomb", Chinese="絆索炸彈（高爆）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_TripWireGas", Chinese="絆索炸彈（毒氣）", ShortTxt="12g JC" },
            new WeaponName(){ English="U_TripWireBurn", Chinese="絆索炸彈（燃燒）", ShortTxt="12g JC" },

            // K弹
            new WeaponName(){ English="U_WinchesterM1895_KBullet", Chinese="K 彈", ShortTxt="12g JC" },
            new WeaponName(){ English="U_WinchesterM1895_KBullet_6x", Chinese="K 彈", ShortTxt="12g JC" },
            new WeaponName(){ English="U_WinchesterM1895_KBullet", Chinese="K 彈", ShortTxt="12g JC" },
            new WeaponName(){ English="U_Gewehr98_KBullet_4x", Chinese="K 彈", ShortTxt="12g JC" },
            new WeaponName(){ English="U_Gewehr98_KBullet_6x", Chinese="K 彈", ShortTxt="12g JC" },
            new WeaponName(){ English="U_Gewehr98_KBullet", Chinese="K 彈", ShortTxt="12g JC" },
            new WeaponName(){ English="U_BSAHowellM1916_RGL_Frag", Chinese="K 彈", ShortTxt="12g JC" },
            new WeaponName(){ English="U_Type38Arisaka_KBullet_Scope", Chinese="K 彈", ShortTxt="12g JC" },
            new WeaponName(){ English="U_LeeEnfieldSMLE_KBullet_4x", Chinese="K 彈", ShortTxt="12g JC" },
            new WeaponName(){ English="U_MartiniHenry_KBullet_Scope", Chinese="K 彈", ShortTxt="12g JC" },

            /////////////////////////////////////////////////////////////////////////////

            // 精英兵
            new WeaponName(){ English="U_MaximMG0815", Chinese="哨兵 MG 08/15", ShortTxt="MaximMG0815" },
            new WeaponName(){ English="U_FlameThrower", Chinese="喷火兵 Wex", ShortTxt="Wex" },

            new WeaponName(){ English="Royal Club", Chinese="战壕奇兵 奇兵棒", ShortTxt="Royal Club" },
            new WeaponName(){ English="MartiniGrenadeLauncher", Chinese="入侵者 马提尼·亨利步兵榴弹发射器", ShortTxt="Martini GL" },

            new WeaponName(){ English="SpawnBeacon", Chinese="重生信标", ShortTxt="SpawnBeacon" },

            ///////////////////////////////////////////////////////////////////////////////////

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
