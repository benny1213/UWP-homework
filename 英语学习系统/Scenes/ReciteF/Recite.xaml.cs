//todo:显示问题：解决definition 和translation 字符串过长时无法换行的问题


//暂且设定用户对一个新单词点击2次“认识”按钮或者点击“太简单”按钮 这种情况下f_rate=2 且将再不出现此单词
//用户单击一次“不认识”按钮将使f_rate-1 即对一个单词点击一次不认识后将来需要在点击一次认识按钮将f_rate补回



using MySql.Data.MySqlClient;//使用MySQL连接
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
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
using 英语学习系统.Model;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace 英语学习系统.Scenes.ReciteF
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Recite : Page
    {
        int count;
        word[] words;
        int f_rate;


        public Recite()
        {
            this.InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Enabled;

        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {


            base.OnNavigatedTo(e);
            if (e.Parameter.GetType().Equals(typeof(word[])))
            {
                words = (word[])e.Parameter;
            }
            changeword();
            
        }
        
        private void doKnow_Click(object sender, RoutedEventArgs e)
        {
            words[count].Color = "Blue";
            words[count].Sf_rate++;
            count++;
            changeword();
            Frame.Navigate(typeof(Scenes.ReciteF.ShowWord), words[count - 1]);
            definition.Visibility = Visibility.Collapsed;
        }

        private void DoNotKnow_Click(object sender, RoutedEventArgs e)
        {
            words[count].Color = "Red";
            words[count].Sf_rate--;
            //第一次按下则显示definition，第二次按下显示translation且将按钮变成“查看详情”按钮

            if (definition.Visibility == Visibility.Collapsed)//第一次点击显示definition
            {
                tooeasy.Visibility = Visibility.Collapsed;
                definition.Visibility = Visibility.Visible;
                return;
            }
            if(definition.Visibility == Visibility.Visible && translation.Visibility == Visibility.Collapsed)//第二次点击显示definition和translation且更改按钮
            {
                translation.Visibility = Visibility.Visible;
                doKnow.Visibility = Visibility.Collapsed;
                doNotKnow.Visibility = Visibility.Collapsed;
                showdetail.Visibility = Visibility.Visible;
                count++;
                return;
            }
        }
        private void Tooeasy_Click(object sender, RoutedEventArgs e)
        {
            words[count].Color = "Blue";
            showdetail.Visibility = Visibility.Collapsed;
            doKnow.Visibility = Visibility.Visible;
            doNotKnow.Visibility = Visibility.Visible;
            definition.Visibility = Visibility.Collapsed;
            translation.Visibility = Visibility.Collapsed;
            words[count].Sf_rate = 2;
            count++;
            changeword();
        }
        private void Showdetail_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Scenes.ReciteF.ShowWord), words[count-1]);
            showdetail.Visibility = Visibility.Collapsed;
            doKnow.Visibility = Visibility.Visible;
            doNotKnow.Visibility = Visibility.Visible;
            definition.Visibility = Visibility.Collapsed;
            translation.Visibility = Visibility.Collapsed;
            tooeasy.Visibility = Visibility.Visible;
        }

        private void changeword()//考虑到实用性和复杂度，这里的更变单词函数只提供向前更变的选项拒绝用户向前查看单词
        {
            if (count == 20)
            {
                Frame.Navigate(typeof(Scenes.ReciteF.EndRecite), words);
                count = 0;
                return;
            }
            word.Text = words[count].Sword;
            phonetic.Text = words[count].Sphonetic;
            definition.Text = words[count].Sdefinition == "" ? "暂无英文释义" : words[count].Sdefinition;
            translation.Text = words[count].Stranslation == "" ? "暂无中文释义" : words[count].Stranslation;
            f_rate = words[count].Sf_rate;
        }


    }
}
