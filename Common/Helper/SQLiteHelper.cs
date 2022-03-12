using BF1.ServerAdminTools.Common.Utils;
using System.Data;
using System.Data.SQLite;

namespace BF1.ServerAdminTools.Common.Helper
{
    public class SQLiteHelper
    {
        private static string kickDbFile = FileUtil.D_DB_Path + @"\KickLog.db";

        private static SQLiteConnection conn = null;

        public static void Initialize()
        {
            bool isFirst = false;

            // 判断数据库文件是否存在
            if (!File.Exists(kickDbFile))
            {
                SQLiteConnection.CreateFile(kickDbFile);
                isFirst = true;
            }

            conn = new SQLiteConnection("data source=" + kickDbFile);
            conn.Open();

            if (isFirst)
            {
                string creatSheet1 = "CREATE TABLE kick1 ( ID INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, Name TEXT, PersonaId TEXT, Reason TEXT, Status TEXT, Date TEXT )";
                string creatSheet2 = "CREATE TABLE kick2 ( ID INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, Name TEXT, PersonaId TEXT, Reason TEXT, Status TEXT, Date TEXT )";

                ExecuteNonQuery(creatSheet1);
                ExecuteNonQuery(creatSheet2);
            }
        }

        public static void CloseConnection()
        {
            if (conn != null)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        public static void ExecuteNonQuery(string sqlStr)
        {
            using (SQLiteCommand cmd = new SQLiteCommand(sqlStr, conn))
            {
                cmd.ExecuteNonQuery();
            }
        }
    }
}
