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
            new WeaponName(){ English="123", Chinese="M1911", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="P08 手槍", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="Mle 1903", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="C93", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="3 號左輪手槍", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="Kolibri", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="納甘左輪手槍", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="Obrez 手槍", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="Mk VI 左輪手槍", ShortTxt="12g JC" },

            new WeaponName(){ English="123", Chinese="地獄戰士 M1911", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="紅男爵的 P08", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="M1911（消音器）", ShortTxt="12g JC" },

            // 手榴弹
            new WeaponName(){ English="123", Chinese="破片手榴彈", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="毒氣手榴彈", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="衝擊手榴彈", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="燃燒手榴彈", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="小型手榴彈", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="煙霧手榴彈", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="輕型反坦克手榴彈", ShortTxt="12g JC" },

            new WeaponName(){ English="123", Chinese="土製手榴彈", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="俄羅斯標準手榴彈", ShortTxt="12g JC" },

            // 近战
            new WeaponName(){ English="123", Chinese="野戰刀", ShortTxt="12g JC" },

            // 其他
            new WeaponName(){ English="Gas Mask", Chinese="防毒面具", ShortTxt="Gas Mask" },

            ////////////////////////////////// 突击兵 Assault //////////////////////////////////

            // 主要武器
            new WeaponName(){ English="123", Chinese="Model 10-A（霰彈塊）", ShortTxt="10A XDK" },
            new WeaponName(){ English="U_RemingtonM10_Wep_Choke", Chinese="Model 10-A（獵人）", ShortTxt="10A LR" },
            new WeaponName(){ English="123", Chinese="Model 10-A（原廠）", ShortTxt="10A YC" },
            new WeaponName(){ English="123", Chinese="M97 戰壕槍（掃蕩）", ShortTxt="M97 SD" },
            new WeaponName(){ English="123", Chinese="M97 戰壕槍（Back-Bored）", ShortTxt="M97 BB" },
            new WeaponName(){ English="123", Chinese="M97 戰壕槍（獵人）", ShortTxt="M97 LR" },
            new WeaponName(){ English="123", Chinese="MP 18（壕溝戰）", ShortTxt="MP18 HGZ" },
            new WeaponName(){ English="123", Chinese="MP 18（實驗）", ShortTxt="MP18 SY" },
            new WeaponName(){ English="123", Chinese="MP 18（瞄準鏡）", ShortTxt="MP18 MZJ" },
            new WeaponName(){ English="123", Chinese="M1918 自動衝鋒槍（壕溝戰）", ShortTxt="MP1918 HGZ" },
            new WeaponName(){ English="123", Chinese="M1918 自動衝鋒槍（衝鋒）", ShortTxt="MP1918 CF" },
            new WeaponName(){ English="123", Chinese="M1918 自動衝鋒槍（原廠）", ShortTxt="MP1918 YC" },
            new WeaponName(){ English="123", Chinese="12g 自動霰彈槍（Back-Bored）", ShortTxt="12g BB" },
            new WeaponName(){ English="123", Chinese="12g 自動霰彈槍（獵人）", ShortTxt="12g LR" },
            new WeaponName(){ English="123", Chinese="12g 自動霰彈槍（加長）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="Hellriegel 1915（原廠）", ShortTxt="H1915 YC" },
            new WeaponName(){ English="123", Chinese="Hellriegel 1915（防禦）", ShortTxt="H1915 FY" },
            new WeaponName(){ English="123", Chinese="地獄戰士戰壕霰彈槍", ShortTxt="M97 DYZS" },
            new WeaponName(){ English="123", Chinese="Sjögren Inertial（原廠）", ShortTxt="H1915 FY" },
            new WeaponName(){ English="123", Chinese="Sjögren Inertial（霰彈塊）", ShortTxt="H1915 FY" },
            new WeaponName(){ English="123", Chinese="利貝羅勒 1918（原廠）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="Ribeyrolles 1918（瞄準鏡）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="Model 1900（原廠）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="Model 1900（原廠）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="SMG 08/18（原廠）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="SMG 08/18（瞄準鏡）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="M1912/P.16（衝鋒）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="Maschinenpistole M1912/P.16（實驗）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="M1917 戰壕卡賓槍", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="M1917 卡賓槍（巡邏）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="RSC 衝鋒槍（原廠）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="RSC 衝鋒槍（瞄準鏡）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="Annihilator（壕溝）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="Annihilator（衝鋒）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="費羅梅爾停止手槍（自動）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="短管霰彈槍", ShortTxt="12g JC" },

            // 配枪
            new WeaponName(){ English="123", Chinese="加塞 M1870", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="Howdah 手槍", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="1903 Hammerless", ShortTxt="12g JC" },

            // 配备一二
            new WeaponName(){ English="123", Chinese="炸藥", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="反坦克手榴彈", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="反坦克地雷", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="反坦克火箭砲", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="防空火箭砲", ShortTxt="12g JC" },

            ////////////////////////////////// 医疗兵 Medic   //////////////////////////////////

            // 主要武器
            new WeaponName(){ English="123", Chinese="Cei-Rigotti（壕溝戰）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="Cei-Rigotti（瞄準鏡）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="Cei-Rigotti（原廠）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="Selbstlader M1916（神射手）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="Selbstlader M1916（瞄準鏡）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="Selbstlader M1916（原廠）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="M1907 半自動步槍（壕溝戰）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="M1907 半自動步槍（掃蕩）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="M1907 半自動步槍（原廠）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="蒙德拉貢步槍（瞄準鏡）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="蒙德拉貢步槍（衝鋒）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="蒙德拉貢步槍（狙擊手）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="自動裝填步槍 8.35（原廠）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="自動裝填步槍 8.35（神射手）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="自動裝填步槍 8.25（加長）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="Selbstlader 1906（原廠）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="Selbstlader 1906（狙擊手）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="RSC 1917（瞄準鏡）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="RSC 1917（原廠）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="費德洛夫自動步槍（壕溝戰）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="費德洛夫自動步槍（瞄準鏡）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="劉將軍步槍（原廠）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="劉將軍步槍（衝鋒）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="Farquhar-Hill 步槍（瞄準鏡）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="Farquhar-Hill 步槍（衝鋒）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="Howell 自動步槍（原廠）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="Howell 自動步槍（狙擊手）", ShortTxt="12g JC" },

            // 配枪
            new WeaponName(){ English="123", Chinese="自動左輪手槍", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="C96", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="Taschenpistole M1914", ShortTxt="12g JC" },

            // 配备一二
            new WeaponName(){ English="123", Chinese="醫療用針筒", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="醫護箱", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="繃帶包", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="步槍手榴彈（破片）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="步槍手榴彈（煙霧）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="步槍手榴彈（高爆）", ShortTxt="12g JC" },

            ////////////////////////////////// 支援兵 Support //////////////////////////////////

            // 主要武器
            new WeaponName(){ English="123", Chinese="路易士機槍（壓制）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="路易士機槍（瞄準鏡）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="路易士機槍（輕量化）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="M1909 貝內特·梅西耶機槍（衝鋒）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="M1909 貝內特·梅西耶機槍（瞄準鏡）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="M1909 貝內特·梅西耶機槍（望遠瞄具）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="麥德森機槍（壕溝戰）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="麥德森機槍（衝鋒）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="麥德森機槍（輕量化）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="MG15 n.A.（壓制）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="MG15 n.A.（衝鋒）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="MG15 n.A.（輕量化）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="M1918 白朗寧自動步槍（壕溝戰）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="M1918 白朗寧自動步槍（衝鋒）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="M1918 白朗寧自動步槍（望遠瞄具）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="Huot 自動步槍（輕量化）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="Huot 自動步槍（瞄準鏡）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="紹沙輕機槍（輕量化）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="紹沙輕機槍（望遠瞄具）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="Parabellum MG14/17（輕量化）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="Parabellum MG14/17（壓制）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="Perino Model 1908（輕量化）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="Perino Model 1908（防禦）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="M1917 機槍（輕量化）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="M1917 機槍（望遠瞄具）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="輕機槍 08/18（輕量化）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="輕機槍 08/18（壓制）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="波頓 LMR（戰壕）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="波頓 LMR（瞄準鏡）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="C96（卡賓槍）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="P08 Artillerie", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="皮珀 M1893", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="M1911（加長）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="Mle 1903（加長）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="C93（卡賓槍）", ShortTxt="12g JC" },

            // 配枪
            new WeaponName(){ English="123", Chinese="Repetierpistole M1912", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="鬥牛犬左輪手槍", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="Modello 1915", ShortTxt="12g JC" },

            // 配备一二
            new WeaponName(){ English="123", Chinese="彈藥箱", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="彈藥包", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="迫擊砲（空爆）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="迫擊砲（高爆）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="維修工具", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="磁吸地雷", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="十字弓發射器（破片）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="十字弓發射器（高爆）", ShortTxt="12g JC" },

            ////////////////////////////////// 侦察兵 Scout   //////////////////////////////////

            // 主要武器
            new WeaponName(){ English="123", Chinese="Russian 1895（壕溝戰）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="Russian 1895（狙擊手）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="Russian 1895（步兵）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="Gewehr 98（神射手）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="Gewehr 98（狙擊手）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="Gewehr 98（步兵）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="SMLE MKIII（神射手）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="SMLE MKIII（卡賓槍）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="SMLE MKIII（步兵）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="Gewehr M.95（神射手）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="Gewehr M.95（卡賓槍）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="Gewehr M.95（步兵）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="M1903（神射手）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="M1903（狙擊手）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="M1903（實驗）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="馬提尼·亨利步槍（步兵）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="馬提尼·亨利步槍（狙擊手）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="阿拉伯的勞倫斯的 SMLE", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="勒貝爾 M1886（狙擊手）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="勒貝爾 M1886（步兵）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="莫辛-納甘 M91（步兵）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="莫辛-納甘 M91（神射手）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="Vetterli-Vitali M1870/87（步兵）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="Vetterli-Vitali M1870/87（卡賓槍）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="三八式步槍（步兵）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="三八式步槍（巡邏）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="卡爾卡諾 M91 卡賓槍", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="卡爾卡諾 M91 卡賓槍（巡邏）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="羅斯 MKIII（步兵）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="羅斯 MKIII（神射手）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="M1917 Enfield（步兵）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="M1917 Enfield（消音器）", ShortTxt="12g JC" },

            // 配枪
            new WeaponName(){ English="123", Chinese="Mars 自動手槍", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="Bodeo 1889", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="費羅梅爾停止手槍", ShortTxt="12g JC" },

            // 配备一二
            new WeaponName(){ English="123", Chinese="信號槍（偵察）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="信號槍（閃光）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="K 彈", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="戰壕潛望鏡", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="狙擊手護盾", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="狙擊手誘餌", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="絆索炸彈（高爆）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="絆索炸彈（毒氣）", ShortTxt="12g JC" },
            new WeaponName(){ English="123", Chinese="絆索炸彈（燃燒）", ShortTxt="12g JC" },

            ///////////////////////////////////////////////////////////////////////////////////

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
