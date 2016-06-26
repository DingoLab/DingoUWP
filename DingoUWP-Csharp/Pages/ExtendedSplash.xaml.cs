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
using Windows.ApplicationModel.Activation;
using Windows.UI.Core;
using DingoUWP_Csharp.Models;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace DingoUWP_Csharp.Pages
{
    /// <summary>
    /// 扩展的初始页面
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class ExtendedSplash : Page
    {
        //储存系统为应用显示初始屏幕图像的位置坐标
        internal Rect splashImageRect;
        //储存SplashScreen对象
        private SplashScreen splash;
        //跟踪是否已解除系统所显示的初始屏幕
        internal bool dismissed = false;
        internal Frame rootFrame;

        /// <summary>
        /// 用于侦听窗口调整大小事件
        /// 、将图像和（可选）进度控件放置在延长的初始屏幕上
        /// 、为导航创建一个框架，以及调用异步方法来还原保存的会话状态。
        /// </summary>
        /// <param name="splashcreen"></param>
        /// <param name="loadState"></param>
        public ExtendedSplash(SplashScreen splashcreen,bool loadState)
        {
            this.InitializeComponent();
            Window.Current.SizeChanged += new WindowSizeChangedEventHandler(ExtendedSplash_OnResize);
            splash = splashcreen;
            if(splash!=null)
            {
                splash.Dismissed += new TypedEventHandler<SplashScreen, object>(DismissedEventHandler);
                splashImageRect = splash.ImageLocation;
                PositionImage();
                PositionRing();
            }
            rootFrame = new Frame();
        }

        /// <summary>
        /// 当用户调整窗口大小时，调整扩展初始屏幕布局
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExtendedSplash_OnResize(object sender, WindowSizeChangedEventArgs e)
        {
            if(splash!=null)
            {
                splashImageRect = splash.ImageLocation;
                PositionImage();
                PositionRing();
            }
        }

        /// <summary>
        /// 当系统已从 默认初始屏幕 来到 扩展的初始屏幕(应用程序的第一个视图)时
        /// 要执行的代码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void DismissedEventHandler(SplashScreen sender, object args)
        {
            dismissed = true;
        }

        /// <summary>
        /// 将 ProgressRing 居中放置在图像的下方。
        /// </summary>
        private void PositionRing()
        {
            ExtendedSplashProgressRing.SetValue(Canvas.LeftProperty, splashImageRect.X + (splashImageRect.Width * 0.9) - (splashImageRect.Width * 0.5));
            ExtendedSplashProgressRing.SetValue(Canvas.TopProperty, (splashImageRect.Y + splashImageRect.Height + splashImageRect.Height * 0.0));
        }

        /// <summary>
        /// 使用 splashImageRect 类变量将图像放置在延长的初始屏幕页面上
        /// </summary>
        private void PositionImage()
        {
            ExtendedSplashImage.SetValue(Canvas.LeftProperty, splashImageRect.X);
            ExtendedSplashImage.SetValue(Canvas.TopProperty, splashImageRect.Y);
            ExtendedSplashImage.Height = splashImageRect.Height;
            ExtendedSplashImage.Width = splashImageRect.Width;
        }

        /// <summary>
        /// 以还原保存的会话状态
        /// </summary>
        /// <param name="loadState"></param>
        void RestoreStateAsync(bool loadState)
        {
            if (loadState)
            {

            }
        }
        /*async void RestoreStateAsync(bool loadState)
        {
            if (loadState)
            {

            }
        }*/


        /// <summary>
        /// 结束扩展的初始屏幕后导航到MainPage
        /// </summary>
        void DismissExtendedSplash()
        {
            // Navigate to mainpage
            rootFrame.Navigate(typeof(UserLogIn));
            // Place the frame in the current Window
            Window.Current.Content = rootFrame;
        }
        private void text_Click(object sender, RoutedEventArgs e)
        {
            DismissExtendedSplash();
        }
    }
}
