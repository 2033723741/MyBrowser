using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MyBrowser.UI.WinApp.Navigator
{
    /// <summary>
    /// Frame导航器
    /// </summary>
    public class FrameNavigator
    {
        private static List<Page> _history = new List<Page>();
        // 历史窗口路径长度
        private static readonly int _maxHistoryPath = 5;

        public static void GoBack<Owner>(string frameName) where Owner : DependencyObject
        {
            if (_history.Count <= 1)
                return;

            List<Frame> frames = ComponentNavigator.GetComponent<Owner, Frame>();
            if (frames == null || frames.Count == 0)
                return;

            Frame frame = frames.Where(x => x.Name == frameName).FirstOrDefault();
            if (frame == null)
                return;

            Page page = _history[_history.Count - 2];
            _history.RemoveAt(_history.Count - 1);

            frame.Content = page;
        }

        public static void NavigateTo<Owner, T>(string frameName) where T : Page, new() where Owner : DependencyObject
        {
            List<Frame> frames = ComponentNavigator.GetComponent<Owner, Frame>();
            if (frames == null || frames.Count == 0)
                return;

            Frame frame = frames.Where(x => x.Name == frameName).FirstOrDefault();
            if (frame == null)
                return;

            Page page = GetPage<T>();

            while (_history.Count >= _maxHistoryPath)
                _history.RemoveAt(0);

            _history.Add(page);

            frame.Content = page;
        }

        private static Page GetPage<T>() where T : Page, new()
        {
            if (!SimpleIoc.Default.IsRegistered<T>())
                SimpleIoc.Default.Register<T>();

            return SimpleIoc.Default.GetInstance<T>() as Page;
        }

        private static void RemovePage<T>() where T : Page, new()
        {
            if (SimpleIoc.Default.IsRegistered<T>())
                SimpleIoc.Default.Unregister<T>();
        }
    }
}
