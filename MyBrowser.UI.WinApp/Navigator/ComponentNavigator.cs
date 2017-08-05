using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MyBrowser.UI.WinApp.Navigator
{
    /// <summary>
    /// View中的控件导航器
    /// </summary>
    public class ComponentNavigator
    {
        /// <summary>
        /// 获取控件
        /// </summary>
        /// <typeparam name="Owner">控件所有者（Window或Page）</typeparam>
        /// <typeparam name="Component">需要获取的目标控件</typeparam>
        /// <returns></returns>
        public static List<Component> GetComponent<Owner, Component>() where Owner : DependencyObject
        {
            if (!SimpleIoc.Default.ContainsCreated<Owner>())
                return null;

            return FindComponent(SimpleIoc.Default.GetInstance<Owner>() as DependencyObject, typeof(Component).ToString()).Select(x => (Component)x).ToList();
        }

        /// <summary>
        /// 获取指定控件中的所有符合条件的子控件
        /// </summary>
        /// <typeparam name="T">指代控件的泛型</typeparam>
        /// <returns></returns>
        public static List<T> FindComponent<T>(DependencyObject owner) where T : DependencyObject
        {
            List<T> resultList = new List<T> { };
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(owner); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(owner, i);
                if (child != null && child is T)
                {
                    resultList.Add((T)child);
                }
                else
                {
                    List<T> childOfChildren = FindComponent<T>(child);
                    if (childOfChildren != null)
                        resultList.AddRange(childOfChildren);
                }
            }
            return resultList;
        }

        /// <summary>
        /// 获取指定组件中的所有符合条件的组件
        /// </summary>
        /// <param name="owner">父控件</param>
        /// <param name="componentUri">控件类名全称</param>
        /// <returns></returns>
        private static List<Object> FindComponent(DependencyObject owner, string componentUri)
        {
            List<Object> resultList = new List<Object>();
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(owner); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(owner, i);
                if (child != null && child.GetType().ToString() == componentUri)
                {
                    resultList.Add(child);
                }
                else
                {
                    List<Object> childOfChildren = FindComponent(child, componentUri);
                    if (childOfChildren != null)
                        resultList.AddRange(childOfChildren);
                }
            }
            return resultList;
        }
    }
}
