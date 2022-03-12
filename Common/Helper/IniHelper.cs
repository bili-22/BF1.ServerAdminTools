namespace BF1.ServerAdminTools.Common.Helper
{
    public class IniHelper
    {
        [DllImport("kernel32.dll")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileString(string section, string key, string defval, StringBuilder retVal, int size, string filePath);

        /// <summary>
        /// 读取INI文件
        /// </summary>
        /// <param name="section">段落名</param>
        /// <param name="key">键名</param>
        /// <param name="def">没有找到时返回的默认值</param>
        /// <param name="filePath">ini文件完整路径</param>
        /// <returns></returns>
        public static string ReadString(string section, string key, string def, string filePath)
        {
            var temp = new StringBuilder(1024);
            GetPrivateProfileString(section, key, def, temp, 1024, filePath);
            return temp.ToString();
        }

        /// <summary>
        /// 写入INI文件
        /// </summary>
        /// <param name="section">段落名</param>
        /// <param name="key">键名</param>
        /// <param name="val">写入值</param>
        /// <param name="filePath">ini文件完整路径</param>
        public static void WriteString(string section, string key, string val, string filePath)
        {
            WritePrivateProfileString(section, key, val, filePath);
        }
    }
}
