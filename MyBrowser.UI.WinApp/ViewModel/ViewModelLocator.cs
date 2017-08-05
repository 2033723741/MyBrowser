using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using System.Windows;

namespace MyBrowser.UI.WinApp.ViewModel
{
    /// <summary>
    /// ViewModel定位器，包含了所有ViewModel的引用，作用是在xaml中提供这些ViewModel的入口点。
    /// 在App.xaml中：
    /// <Application.Resources>
    ///     <vm:ViewModelLocator xmlns:vm="clr-namespace:MyBrowser.UI.WinApp.ViewModel" x:Key="Locator" />
    /// </Application.Resources>
    /// 在视图中：
    /// DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            /*
            * 服务器定位器（ServiceLocator）指定IOC
            * 因为IOC的实现有多种
            */
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            /*
            * IOC类似一个对象容器，
            * 需要先依类型注册，当外部调用需要这个类型的对象时，会自动创建该实例，并缓存起来，供再次使用
            *
            * 这里是将MainViewModel类型注册，以在将来提供其实例
            */
            //SimpleIoc.Default.Register<LoginViewModel>();
            SimpleIoc.Default.Register<MainViewModel>();
        }

        /// <summary>
        /// LoginViewModel的实例引用入口
        /// </summary>
        //public LoginViewModel LoginViewModel => ServiceLocator.Current.GetInstance<LoginViewModel>();

        /// <summary>
        /// MainViewModel的实例引用入口
        /// </summary>
        public MainViewModel MainViewModel => ServiceLocator.Current.GetInstance<MainViewModel>();


        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}