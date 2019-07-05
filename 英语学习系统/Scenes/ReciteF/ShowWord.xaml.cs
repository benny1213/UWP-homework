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

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace 英语学习系统.Scenes.ReciteF
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class ShowWord : Page
    {
        word word;
        public ShowWord()//先运行初始化函数然后才会运行OnNavigateTo()
        {
            this.InitializeComponent();
            
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter.GetType().Equals(typeof(word)))
                word = (word)e.Parameter;
            if (word.Sf_rate < 0)
            {
                for (int i = word.Sf_rate; i < 0; i++)
                {
                    SymbolIcon star = new SymbolIcon() { Symbol = Symbol.OutlineStar, Foreground = new SolidColorBrush(Windows.UI.Colors.Red), HorizontalAlignment = HorizontalAlignment.Center };
                    starStackPanel.Children.Add(star);
                }
            }
            else
            {
                if (word.Sf_rate == 0)
                    return;
                for (int i = 0; i < word.Sf_rate; i++)
                {
                    SymbolIcon star = new SymbolIcon() { Symbol = Symbol.SolidStar, Foreground = new SolidColorBrush(Windows.UI.Colors.Blue), HorizontalAlignment = HorizontalAlignment.Center };
                    starStackPanel.Children.Add(star);
                }
            }
        }
        public static String GetString(string a, string b)// 这里发现x:bind在使用system内的重构函数时会出现错误所以在StackOverflow中提出了该问题并得到了解答‘https://stackoverflow.com/questions/56344751/getting-wrong-when-i-use-xbind-in-uwp-invalid-binding-path-err’x:bind中使用函数时匹配函数的是参数的位数而不是参数的类型。
        {
            return String.Concat(a, b);
        }
        public static String GetString2(string a, int b)
        {
            return String.Concat(a, b.ToString());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
        
    }
}
