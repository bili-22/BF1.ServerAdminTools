namespace BF1.ServerAdminTools.Common.Utils
{
    public class FileUtil
    {
        public static string MyDocument = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        public static string Default_Path = MyDocument + @"\BF1 Server";

        public static string D_Admin_Path = Default_Path + @"\Admin";
        public static string D_Config_Path = Default_Path + @"\Config";
        public static string D_Cache_Path = Default_Path + @"\Cache";
        public static string D_DB_Path = Default_Path + @"\DB";
        public static string D_Log_Path = Default_Path + @"\Log";

        public static string F_Settings_Path = D_Config_Path + @"\Settings.ini";

        public static string F_BlackList_Path = D_Admin_Path + @"\BlackList.txt";
        public static string F_WeaponList_Path = D_Admin_Path + @"\WeaponList.txt";
        public static string F_WhiteList_Path = D_Admin_Path + @"\WhiteList.txt";

        /// <summary>
        /// 保存指定Log文件到本地
        /// </summary>
        /// <param name="logContent">保存内容</param>
        public static void SaveErrorLog(string logContent)
        {
            try
            {
                string path = D_Log_Path + @"\ErrorLog";
                Directory.CreateDirectory(path);
                path += $@"\#ErrorLog# { DateTime.Now:yyyyMMdd_HH-mm-ss_ffff}.log";
                File.WriteAllText(path, logContent);
            }
            catch (Exception) { }
        }
    }
}
