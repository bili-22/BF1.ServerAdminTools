using Microsoft.Web.WebView2.Core;

namespace BF1.ServerAdminTools.Common.Utils
{
    public class CoreUtil
    {
        public const string AppName = "bf1";    // 战地1

        public const string WindowTitle = "战地1服务器管理工具 v";

        public const string Download_Address = "https://gitee.com/CrazyZhang666/BF1Server";

        public const string Version_Address = "https://gitee.com/CrazyZhang666/BF1Server/raw/master/Update/Version.txt";
        public const string Notice_Address = "https://gitee.com/CrazyZhang666/BF1Server/raw/master/Update/Notice.txt";

        /// <summary>
        /// 程序当前版本号，如：1.2.3.4
        /// </summary>
        public static string LocalVersionInfo = Application.ResourceAssembly.GetName().Version.ToString();

        /// <summary>
        /// 计算时间差，即软件运行时间
        /// </summary>
        public static string ExecDateDiff(DateTime dateBegin, DateTime dateEnd)
        {
            TimeSpan ts1 = new TimeSpan(dateBegin.Ticks);
            TimeSpan ts2 = new TimeSpan(dateEnd.Ticks);

            return ts1.Subtract(ts2).Duration().ToString("c").Substring(0, 8);
        }

        /// <summary>
        /// 是否安装了WebView2依赖，安装了返回true，否则返回false
        /// </summary>
        /// <returns></returns>
        public static bool IsWebView2DependencyInstalled()
        {
            try
            {
                return !string.IsNullOrEmpty(CoreWebView2Environment.GetAvailableBrowserVersionString());
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 返回两个时间差秒数
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static double DiffSeconds(DateTime startTime, DateTime endTime)
        {
            TimeSpan secondSpan = new TimeSpan(endTime.Ticks - startTime.Ticks);
            return secondSpan.TotalSeconds;
        }
    }
}
