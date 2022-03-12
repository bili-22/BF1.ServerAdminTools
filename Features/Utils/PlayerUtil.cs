using BF1.ServerAdminTools.Features.Data;

namespace BF1.ServerAdminTools.Features.Utils
{
    public class PlayerUtil
    {
        /// <summary>
        /// 小数类型的时间秒，转为mm:ss格式
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string SecondsToMMSS(float time)
        {
            try
            {
                if (time >= 0 && time <= 36000)
                {
                    TimeSpan timeSpan = TimeSpan.FromSeconds(time);

                    DateTime dateTime = DateTime.Parse(timeSpan.ToString());

                    return $"{dateTime:mm:ss}";
                }
                else
                {
                    return $"00:00";
                }
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// 小数类型的时间秒，转为mm格式
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static int SecondsToMM(float time)
        {
            try
            {
                if (time >= 0 && time <= 36000)
                {
                    int a = (int)(time / 60);

                    if (a != 0)
                    {
                        return a;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// 计算玩家KD比
        /// </summary>
        /// <param name="kill">玩家击杀数</param>
        /// <param name="dead">玩家死亡数</param>
        /// <returns>返回玩家KD比（小数float）<returns>
        public static float GetPlayerKD(int kill, int dead)
        {
            if (kill == 0 && dead >= 0)
            {
                return 0.0f;
            }
            else if (kill > 0 && dead == 0)
            {
                return kill;
            }
            else if (kill > 0 && dead > 0)
            {
                return (float)kill / dead;
            }
            else
            {
                return (float)kill / dead;
            }
        }

        /// <summary>
        /// 计算玩家KPM比
        /// </summary>
        /// <param name="kill"></param>
        /// <param name="minute"></param>
        /// <returns></returns>
        public static float GetPlayerKPM(int kill, float minute)
        {
            if (minute != 0.0f)
            {
                return kill / minute;
            }
            else
            {
                return 0.0f;
            }
        }

        /// <summary>
        /// 判断战地1输入框字符串长度，中文3，英文1
        /// </summary>
        /// <param name="str">需要判断的字符串</param>
        /// <returns></returns>
        public static int GetStrLength(string str)
        {
            if (string.IsNullOrEmpty(str))
                return 0;

            ASCIIEncoding ascii = new ASCIIEncoding();
            int tempLen = 0;
            byte[] s = ascii.GetBytes(str);
            for (int i = 0; i < s.Length; i++)
            {
                if ((int)s[i] == 63)
                {
                    tempLen += 3;
                }
                else
                {
                    tempLen += 1;
                }
            }

            return tempLen;
        }

        /// <summary>
        /// 获取击杀星数
        /// </summary>
        /// <param name="kills"></param>
        /// <returns></returns>
        public static string GetKillStar(int kills)
        {
            if (kills < 100)
            {
                return "";
            }
            else
            {
                int count = kills / 100;

                return $"★ {count}";
            }
        }

        /// <summary>
        /// 获取玩家ID，不带队标
        /// </summary>
        /// <param name="playerName"></param>
        /// <returns></returns>
        public static string GetNameNoMark(string playerName)
        {
            int index = playerName.IndexOf("]");
            if (index != -1)
            {
                return playerName.Substring(index + 1);
            }

            return playerName;
        }

        /// <summary>
        /// 获取武器对应的中文名称
        /// </summary>
        /// <param name="originWeaponName"></param>
        /// <returns></returns>
        public static string GetCHSWeaponName(string originWeaponName)
        {
            int index = WeaponData.AllWeaponInfo.FindIndex(var => var.English == originWeaponName);
            if (index != -1)
            {
                return WeaponData.AllWeaponInfo[index].Chinese;
            }
            else
            {
                return originWeaponName;
            }
        }

        /// <summary>
        /// 获取本地图片路径
        /// </summary>
        /// <param name="url"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetTempImagePath(string url, string type)
        {
            string extension = Path.GetFileName(url);
            switch (type)
            {
                case "maps":
                    return ImageData.MapsDict[extension];
                case "weapons":
                    return ImageData.WeaponsDict[extension];
                case "weapons2":
                    return ImageData.Weapons2Dict[extension];
                case "vehicles":
                    return ImageData.VehiclesDict[extension];
                case "vehicles2":
                    return ImageData.Vehicles2Dict[extension];
                case "classes":
                    return ImageData.ClassesDict[extension];
                case "classes2":
                    return ImageData.Classes2Dict[extension];
                default:
                    return string.Empty;
            }
        }

        public static string CheckAdminVIP(string personaId, List<string> list)
        {
            return list.IndexOf(personaId) != -1 ? "✔" : "";
        }

        public static string GetPlayTime(string timeStr)
        {
            string[] array = timeStr.Split(new string[] { "days,", "day," }, StringSplitOptions.RemoveEmptyEntries);
            if (array.Length == 1)
            {
                var d0 = Convert.ToDateTime(array[0].Trim());
                return d0.Hour.ToString();
            }
            else if (array.Length == 2)
            {
                var d0 = Convert.ToInt32(array[0].Trim());
                var d1 = Convert.ToDateTime(array[1].Trim());
                return (d0 * 24 + d1.Hour).ToString();
            }

            return "";
        }

        /// <summary>
        /// 获取武器简短名称，用于踢人理由
        /// </summary>
        /// <param name="weaponName"></param>
        /// <returns></returns>
        public static string GetWeaponShortTxt(string weaponName)
        {
            int index = WeaponData.AllWeaponInfo.FindIndex(var => var.English.Equals(weaponName));
            if (index != -1)
            {
                return WeaponData.AllWeaponInfo[index].ShortTxt;
            }

            return weaponName;
        }
    }
}
