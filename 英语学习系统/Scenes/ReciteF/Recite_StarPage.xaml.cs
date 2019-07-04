using MySql.Data.MySqlClient;//使用MySQL连接
using System;
using System.Collections.Generic;
using System.Data;
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
    
    //在此页面加载数据库并随机取出用户frq<2的单词让用户背

    public sealed partial class Recite_StarPage : Page
    {

        word[] words;//对象数组暂存需要背的单词之后会传递给Recite页面

        public Recite_StarPage()
        {
            this.InitializeComponent();
            DBControl db = new DBControl();
            words = db.read("frq != 0  order by rand() limit 20", 20);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            Frame.Navigate(typeof(ReciteF.Recite), words);
        }
    }
}
