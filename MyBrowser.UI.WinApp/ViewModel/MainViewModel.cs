using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyBrowser.UI.WinApp.ViewModel
{
    public class MainViewModel : ViewModel
    {
        private string _title = "我的浏览器";

        public MainViewModel()
        {
            
        }        

        public string Title
        {
            get
            {
                return _title;
            }

            set
            {
                if (_title == value)
                    return;

                _title = value;
                RaisePropertyChanged(() => Title);
            }
        }
    }
}
