using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using System.Windows;

namespace MyBrowser.UI.WinApp.ViewModel
{
    /// <summary>
    /// ViewModel��λ��������������ViewModel�����ã���������xaml���ṩ��ЩViewModel����ڵ㡣
    /// ��App.xaml�У�
    /// <Application.Resources>
    ///     <vm:ViewModelLocator xmlns:vm="clr-namespace:MyBrowser.UI.WinApp.ViewModel" x:Key="Locator" />
    /// </Application.Resources>
    /// ����ͼ�У�
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
            * ��������λ����ServiceLocator��ָ��IOC
            * ��ΪIOC��ʵ���ж���
            */
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            /*
            * IOC����һ������������
            * ��Ҫ��������ע�ᣬ���ⲿ������Ҫ������͵Ķ���ʱ�����Զ�������ʵ�������������������ٴ�ʹ��
            *
            * �����ǽ�MainViewModel����ע�ᣬ���ڽ����ṩ��ʵ��
            */
            //SimpleIoc.Default.Register<LoginViewModel>();
            SimpleIoc.Default.Register<MainViewModel>();
        }

        /// <summary>
        /// LoginViewModel��ʵ���������
        /// </summary>
        //public LoginViewModel LoginViewModel => ServiceLocator.Current.GetInstance<LoginViewModel>();

        /// <summary>
        /// MainViewModel��ʵ���������
        /// </summary>
        public MainViewModel MainViewModel => ServiceLocator.Current.GetInstance<MainViewModel>();


        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}