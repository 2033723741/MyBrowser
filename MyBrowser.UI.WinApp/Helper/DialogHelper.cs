using MyBrowser.UI.WinApp.CustomControl.Dialog;
using MyBrowser.UI.WinApp.Navigator;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MyBrowser.UI.WinApp.Helper
{
    public class DialogHelper
    {
        /// <summary>
        /// 弹出消息对话框
        /// </summary>
        /// <param name="content">消息内容</param>
        public static void ShowMessage(string content, MessageBoxButton button, Action<MessageBoxResult> callback = null)
        {
            DialogHost dh = BuildDialogHost("message");
            if (dh == null)
                return;

            dh.ShowDialog(new MessageDialog { Message = { Text = content }, Button = button },
                delegate (object sender, DialogClosingEventArgs args)
                {
                    bool parameter = false;
                    bool.TryParse(args.Parameter.ToString(), out parameter);
                    if (callback != null)
                        callback(BuildResult(parameter, button));
                });
        }

        /// <summary>
        /// 弹出Loading动画
        /// </summary>
        public static void ShowProgress()
        {
            DialogHost dh = BuildDialogHost("loading");
            if (dh == null)
                return;

            dh.ShowDialog(new ProgressDialog (), delegate (object sender, DialogClosingEventArgs args){});
        }

        private static DialogHost BuildDialogHost(string identifier)
        {
            DialogHost dh = ComponentNavigator.FindComponent<DialogHost>(App.Current.MainWindow).FirstOrDefault(x => x.Identifier as string == identifier);
            if (dh != null)
                return dh;

            Grid grid = ComponentNavigator.FindComponent<Grid>(App.Current.MainWindow).FirstOrDefault();
            if (grid == null)
                return null;

            dh = new DialogHost();
            dh.Identifier = identifier;
            dh.DialogContent = new StackPanel();
            grid.Children.Add(dh);

            return dh;
        }

        private static MessageBoxResult BuildResult(bool result, MessageBoxButton button)
        {
            if (button == MessageBoxButton.OK || button == MessageBoxButton.OKCancel)
            {
                if (result)
                    return MessageBoxResult.OK;
                else
                    return MessageBoxResult.Cancel;
            }
            else if (button == MessageBoxButton.YesNo || button == MessageBoxButton.YesNoCancel)
            {
                if (result)
                    return MessageBoxResult.Yes;
                else
                    return MessageBoxResult.No;
            }
            else
                return MessageBoxResult.None;
        }
    }
}
