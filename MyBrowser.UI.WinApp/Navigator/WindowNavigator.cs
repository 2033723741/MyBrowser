using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace MyBrowser.UI.WinApp.Navigator
{
    /// <summary>
    /// Window导航器
    /// </summary>
    public class WindowNavigator
    {
        private static List<Window> _history = new List<Window>();
        // 历史窗口路径长度
        private static readonly int _maxHistoryPath = 5;

        /// <summary>
        /// 返回上一个窗口
        /// </summary>
        public static void GoBack()
        {
            if (_history.Count == 0)
                return;

            Window window = _history[_history.Count - 1];
            _history.RemoveAt(_history.Count - 1);

            App.Current.MainWindow.Hide();
            App.Current.MainWindow = window;

            window.Show();
        }

        /// <summary>
        ///  切换窗口
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void NavigateTo<T>() where T : Window, new()
        {
            Window window = GetWindow<T>();

            while (_history.Count >= _maxHistoryPath)
                _history.RemoveAt(0);

            _history.Add(App.Current.MainWindow);
            App.Current.MainWindow.Hide();
            App.Current.MainWindow = window;
            

            window.Show();
        }

        /// <summary>
        /// 弹出窗口
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="modal"></param>
        public static void Popup<T>(bool modal) where T : Window, new()
        {
            Window window = GetWindow<T>();
            window.Owner = App.Current.MainWindow;

            if (modal)
                window.ShowDialog();
            else
                window.Show();
        }

        /// <summary>
        /// 关闭或注销窗口
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="destroy"></param>
        public static void Close<T>(bool destroy = true) where T : Window, new()
        {
            if (!SimpleIoc.Default.IsRegistered<T>())
                return;

            Window window = GetWindow<T>();

            if (destroy)
            {
                window.Close();
                RemoveWindow<T>();
            }
            else
                window.Hide();
        }

        public static Window GetWindow<T>() where T: Window, new()
        {
            if (!SimpleIoc.Default.IsRegistered<T>())
                SimpleIoc.Default.Register<T>();

            return SimpleIoc.Default.GetInstance<T>() as Window;
        }

        private static void RemoveWindow<T>() where T : Window, new()
        {
            if (SimpleIoc.Default.IsRegistered<T>())
                SimpleIoc.Default.Unregister<T>();
        }
    }
}
