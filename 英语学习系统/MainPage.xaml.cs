using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace 英语学习系统
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            ObservableCollection<MenuItem> menuItems = new ObservableCollection<MenuItem>()
            {
                new MenuItem() { Icon=Symbol.Character, Text="翻译", label = typeof(Scenes.Translate)},
                new MenuItem() { Icon=Symbol.Tag, Text="背单词", label = typeof(Scenes.ReciteF.Recite_StarPage)},
                new MenuItem() { Icon=Symbol.PreviewLink, Text="BBC新闻", label = typeof(Scenes.BBCnews)},
                new MenuItem() { Icon=Symbol.CalendarDay, Text="每日一句", label = typeof(Scenes.ReciteF.Recite)},//暂时挂在recite中
                new MenuItem() { Icon=Symbol.Video, Text="视频", label = typeof(Scenes.ShowVideo)}



            };
            mainListView.ItemsSource = menuItems;
            contentVewFrame.Navigate(typeof(Login));//默认页
        }
        
        private void HanburgButton_Click(object sender, RoutedEventArgs e)
        {
            mainSplitView.IsPaneOpen = !mainSplitView.IsPaneOpen;
        }

        private void MainListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            MenuItem link = e.ClickedItem as MenuItem;
            if(link != null)
            {
                contentVewFrame.Navigate(link.label);
                mainSplitView.IsPaneOpen = false;
            }
        }
    }
    public class MenuItem
    {
        public Symbol Icon { get; set; }
        public String Text { get; set; }
        public Type label { get; set; }

    }
}
