using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BF1.ServerAdminTools.Wpf.Models;

public class WeaponInfoModel : ObservableObject
{
    private string english;
    public string English
    {
        get
        {
            return english;
        }
        set
        {
            english = value;
            OnPropertyChanged();
        }
    }
    private string chinese;
    public string Chinese
    {
        get
        {
            return chinese;
        }
        set
        {
            chinese = value;
            OnPropertyChanged();
        }
    }
    private string mark;
    public string Mark
    {
        get
        {
            return mark;
        }
        set
        {
            mark = value;
            OnPropertyChanged();
        }
    }
}
