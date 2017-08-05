using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBrowser.UI.WinApp.Message
{
    public class ActionBaseMessage : MessageBase
    {
        private int _actType = 0;
        private object[] _args = null;

        public ActionBaseMessage(int actType, params object[] args)
        {
            _actType = actType;
            _args = args;
        }

        public object[] Args
        {
            get
            {
                return _args;
            }
        }

        public int ActType
        {
            get
            {
                return _actType;
            }
        }
    };
}
