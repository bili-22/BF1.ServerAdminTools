using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace BF1.ServerAdminTools.Models
{
    public class QueryModel : ObservableObject
    {
        private string playerName;
        /// <summary>
        /// 玩家名称
        /// </summary>
        public string PlayerName
        {
            get { return playerName; }
            set { playerName = value; OnPropertyChanged(); }
        }

        private Visibility loadingVisibility;
        /// <summary>
        /// 加载动画
        /// </summary>
        public Visibility LoadingVisibility
        {
            get { return loadingVisibility; }
            set { loadingVisibility = value; OnPropertyChanged(); }
        }
    }
}
