using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.ComponentModel;
using DingoUWP_Csharp.Models;
using DingoUWP_Csharp.Common;
using Windows.Storage;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍
namespace DingoUWP_Csharp.Pages
{
    /// <summary>
    /// 用于用户登录和注册的页面
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class UserLogIn : Page
    {
        internal Frame rootFrame;
        private bool IsJump = false;
        private UserBase _userBaseViewModel = UserBaseViewModel.GetUserBase();
        public UserBase userBaseViewModel
        {
            get
            {
                return _userBaseViewModel;
            }
        }
        public UserLogIn()
        {
            this.InitializeComponent();
            rootFrame = new Frame();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            //ApplicationDataContainer data = ApplicationData.Current.LocalSettings;
            //data.Values["statue"] = "user";
            VerificationCodeTextBlock.Visibility = Visibility.Collapsed;
            VeriFicationCodeTextBox.Visibility = Visibility.Collapsed;
            VeriFicationCodeTextBoxButton.Visibility = Visibility.Collapsed;
        }

        private void LogOn_Click(object sender, RoutedEventArgs e)
        {
            
            //ApplicationDataContainer data = ApplicationData.Current.LocalSettings;
            //data.Values["statue"] = "visit";
            VerificationCodeTextBlock.Visibility = Visibility.Visible;
            VeriFicationCodeTextBox.Visibility = Visibility.Visible;
            VeriFicationCodeTextBoxButton.Visibility = Visibility.Visible;
            // Navigate to mainpage
            if(IsJump)
            {
                rootFrame.Navigate(typeof(Authentication));
                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }
            else
            {
                IsJump = true;
            }
        }
    }
}
