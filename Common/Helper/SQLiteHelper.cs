using BF1.ServerAdminTools.Common.Utils;
using System.Data;
using Microsoft.Data.Sqlite;

namespace BF1.ServerAdminTools.Common.Helper
{
    public class SQLiteHelper
    {
        private static string kickDbFile = FileUtil.D_DB_Path + @"\KickLog.db";

        private static SqliteConnection conn = null;

        /// <summary>
        /// 数据库初始化
        /// </summary>
        public static void Initialize()
        {
            var connStr = new SqliteConnectionStringBuilder("Data Source=" + kickDbFile)
            {
                Mode = SqliteOpenMode.ReadWriteCreate
            }.ToString();

            conn = new SqliteConnection(connStr);
            conn.Open();

            string selectSheet1 = @"SELECT COUNT(*) FROM sqlite_master WHERE type='table' AND name='Kick_OK'";
            if (ExecuteScalar(selectSheet1) == 0)
            {
                string creatSheet1 = "CREATE TABLE Kick_OK ( ID INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, Name TEXT, PersonaId TEXT, Reason TEXT, Status TEXT, Date TEXT )";
                ExecuteNonQuery(creatSheet1);
            }

            string selectSheet2 = @"SELECT COUNT(*) FROM sqlite_master WHERE type='table' AND name='Kick_Err'";
            if (ExecuteScalar(selectSheet2) == 0)
            {
                string creatSheet2 = "CREATE TABLE Kick_Err ( ID INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, Name TEXT, PersonaId TEXT, Reason TEXT, Status TEXT, Date TEXT )";
                ExecuteNonQuery(creatSheet2);
            }
        }

        /// <summary>
        /// 关闭数据库连接
        /// </summary>
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

        /// <summary>
        /// 执行SQL命令，执行对数据表的增加、删除、修改操作
        /// </summary>
        /// <param name="sqlStr"></param>
        public static void ExecuteNonQuery(string sqlStr)
        {
            using (SqliteCommand cmd = new SqliteCommand(sqlStr, conn))
            {
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 执行SQL命令，返回查询结果中第 1 行第 1 列的值
        /// </summary>
        /// <param name="sqlStr"></param>
        /// <returns></returns>
        public static int ExecuteScalar(string sqlStr)
        {
            using (var cmd = new SqliteCommand(sqlStr, conn))
            {
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
    }
}
