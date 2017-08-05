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

namespace MyBrowser.UI.WinApp.CustomControl.Dialog
{
    public partial class MessageDialog : UserControl
    {
        private MessageBoxButton _button = MessageBoxButton.OK;

        public MessageDialog()
        {
            InitializeComponent();
        }

        public MessageBoxButton Button
        {
            set
            {
                if (value == MessageBoxButton.OK)
                {
                    this.btnConfirm.Visibility = Visibility.Visible;
                    this.btnCancel.Visibility = Visibility.Collapsed;
                    SetConfirmBtnMarginRight(16);
                }
                else
                {
                    this.btnConfirm.Visibility = Visibility.Visible;
                    this.btnCancel.Visibility = Visibility.Visible;
                    SetConfirmBtnMarginRight(96);
                }

                if (value == MessageBoxButton.OK)
                {
                    this.btnConfirm.Content = "确定";
                }
                else if (value == MessageBoxButton.OKCancel)
                {
                    this.btnConfirm.Content = "确定";
                    this.btnCancel.Content = "取消";
                }
                else if (value == MessageBoxButton.YesNo || Button == MessageBoxButton.YesNoCancel)
                {
                    this.btnConfirm.Content = "是";
                    this.btnCancel.Content = "否";
                }

                _button = value;
            }
            get
            {
                return _button;
            }
        }

        private void SetConfirmBtnMarginRight(float val)
        {
            Thickness margin = this.btnConfirm.Margin;
            margin.Right = val;
            this.btnConfirm.Margin = margin;
        }
    }
}
