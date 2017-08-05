using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MyBrowser.UI.WinApp.CustomControl.Dialog;
using MyBrowser.UI.WinApp.Helper;
using MyBrowser.UI.WinApp.Message;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MyBrowser.UI.WinApp.ViewModel
{
    /// <summary>
    /// ViewModel基类，继承于MVVMLight的ViewModelBase
    /// </summary>
    public class ViewModel: ViewModelBase
    {
        /// <summary>
        /// 初始化命令
        /// </summary>
        public ICommand InitCommand { set; get; }

        /// <summary>
        /// 终止化命令
        /// </summary>
        public ICommand UnInitCommand { set; get; }


        public ViewModel()
        {
            InitCommand = new RelayCommand(Init);
            UnInitCommand = new RelayCommand(UnInit);

            Messenger.Default.Register<ActionBaseMessage>(this, ActionNotify);
        }

        /// <summary>
        /// 弹出消息框
        /// </summary>
        protected void ShowMessage(string content, MessageBoxButton button, Action<MessageBoxResult> callback = null)
        {
            DialogHelper.ShowMessage(content, button, callback);
        }

        /// <summary>
        /// 弹出Loading动画
        /// </summary>
        protected void ShowProgress()
        {
            DialogHelper.ShowProgress();
        }

        /// <summary>
        /// 对目标ViewModel发送动作执行通知
        /// </summary>
        /// <typeparam name="Target">目标ViewModel</typeparam>
        /// <param name="actType">动作类型</param>
        /// <param name="args">参数集合</param>
        protected void SendActionNotify<Target>(int actType, params object[] args)
        {
            Messenger.Default.Send<ActionBaseMessage, Target>(new ActionBaseMessage(actType, args));
        }

        
        protected void ActionNotify(ActionBaseMessage msg)
        {
            ActionExecute(msg.ActType, msg.Args);
        }

        /// <summary>
        /// 执行动作
        /// </summary>
        /// <param name="actType"></param>
        /// <param name="args"></param>
        protected virtual void ActionExecute(int actType, params object[] args)
        {

        }

        /// <summary>
        /// 视图模型初始化
        /// </summary>
        protected virtual void Init()
        {

        }

        private void UnInit()
        {
            Messenger.Default.Unregister(this);
            this.Cleanup();
        }
    }
}
