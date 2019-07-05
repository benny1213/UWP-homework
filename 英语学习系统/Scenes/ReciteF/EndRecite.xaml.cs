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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace 英语学习系统.Scenes.ReciteF
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EndRecite : Page
    {
        word[] words;
        public EndRecite()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter.GetType().Equals(typeof(word[])))
            {
                words = (word[])e.Parameter;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DBControl dB = new DBControl();
            foreach(word x in words)
            {
                英语学习系统.App.userid = 1;
                dB.write("INSERT INTO `recite` (`userID`, `wordID`, `fRate`) VALUES ('" + 英语学习系统.App.userid +"', '"+x.Sid+"', '"+x.Sf_rate+"')");//todo:userID 要改成全局变量
            }

            words = dB.read("frq != 0  order by rand() limit 20", 20);
            Frame.Navigate(typeof(Scenes.ReciteF.Recite), words);
        }
    }
}
