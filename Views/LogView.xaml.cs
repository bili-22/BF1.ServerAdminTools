using BF1.ServerAdminTools.Common.Data;
using BF1.ServerAdminTools.Common.Helper;

namespace BF1.ServerAdminTools.Views
{
    /// <summary>
    /// LogView.xaml 的交互逻辑
    /// </summary>
    public partial class LogView : UserControl
    {
        public delegate void DAddKickLog(BreakRuleInfo info);
        public static DAddKickLog dAddKickLog1;
        public static DAddKickLog dAddKickLog2;

        public LogView()
        {
            InitializeComponent();

            dAddKickLog1 = AddKickLog1;
            dAddKickLog2 = AddKickLog2;

            MainWindow.ClosingDisposeEvent += MainWindow_ClosingDisposeEvent;
        }

        private void MainWindow_ClosingDisposeEvent()
        {

        }

        /////////////////////////////////////////////////////

        private void AppendLog1(string msg)
        {
            TextBox_Log1.AppendText(msg + "\n");
        }

        private void AppendLog2(string msg)
        {
            TextBox_Log2.AppendText(msg + "\n");
        }

        /////////////////////////////////////////////////////

        private void AddKickLog1(BreakRuleInfo info)
        {
            Application.Current.Dispatcher.BeginInvoke(() =>
            {
                if (TextBox_Log1.LineCount >= 1000)
                {
                    TextBox_Log1.Clear();
                }

                AppendLog1("操作时间: " + DateTime.Now.ToString());
                AppendLog1("玩家ID: " + info.Name);
                AppendLog1("玩家数字ID: " + info.PersonaId);
                AppendLog1("踢出理由: " + info.Reason);
                AppendLog1("状态: " + info.Status + "\n");

                SQLiteHelper.AddLog2SQLite("Kick_OK", info);
            });
        }

        private void AddKickLog2(BreakRuleInfo info)
        {
            Application.Current.Dispatcher.BeginInvoke(() =>
            {
                if (TextBox_Log2.LineCount >= 1000)
                {
                    TextBox_Log2.Clear();
                }

                AppendLog2("操作时间: " + DateTime.Now.ToString());
                AppendLog2("玩家ID: " + info.Name);
                AppendLog2("玩家数字ID: " + info.PersonaId);
                AppendLog2("踢出理由: " + info.Reason);
                AppendLog2("状态: " + info.Status + "\n");

                SQLiteHelper.AddLog2SQLite("Kick_Err", info);
            });
        }
    }
}
