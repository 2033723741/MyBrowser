using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyBrowser.UI.WinApp.CustomControl.WebView
{
    /// <summary>
    /// WebView.xaml 的交互逻辑
    /// </summary>
    public partial class WebView : UserControl
    {
        public static readonly DependencyProperty UrlProperty =
            DependencyProperty.Register("Url", typeof(string), typeof(WebView), new PropertyMetadata(new PropertyChangedCallback(OnUrlPropertyChanged)));

        public WebView()
        {
            InitializeComponent();
        }

        private static void OnUrlPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var control = sender as WebView;
            if (control == null)
                return;

            control.webBrowser.NavigateTo((string)e.NewValue);            
        }

        public string Url
        {
            get
            {
                return (string)GetValue(UrlProperty);
            }
            set
            {
                SetValue(UrlProperty, value);
            }
        }
    }
}
