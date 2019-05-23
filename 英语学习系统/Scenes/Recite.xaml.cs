using MySql.Data.MySqlClient;//使用MySQL连接
using System;
using System.Collections.Generic;
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

namespace 英语学习系统.Scenes
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Recite : Page
    {
        Random random;
        MySqlConnection connection;
        int frq;
        MySqlDataReader reader;

        word[] words = new word[100];
        
        public Recite()
        {
            this.InitializeComponent();
            this.Loaded += Recite_Loaded;
        }

        private void Recite_Loaded(object sender, RoutedEventArgs e)
        {
            //this.bd.DataContext = Data.students[0];
            random = new Random();
            connection = new MySqlConnection("datasource=127.0.0.1;port=3306;username=root;password=root");

        }

        private void doKnow_Click(object sender, RoutedEventArgs e)
        {
            //temp.Text = "doKnow hit";
            //Frame.Navigate(typeof(Scenes.ReciteF.doKnow)); //查看详情
        }

        private void DoNotKnow_Click(object sender, RoutedEventArgs e)
        {
            //temp.Text = "doNotKnow hit";
            //Frame.Navigate(typeof(Scenes.ReciteF.doNotKnow));
        }

        private void Connect_database_Click(object sender, RoutedEventArgs e)//测试用
        {
            //todo: 由于操作数据库有短暂的延迟，所以先取出100个单词放在表中再背 好像还可以用DataSet?




            for(int i = 0; i < 100; i++)
            {
                frq = random.Next(100, 4000);
                connection.Open();
                MySqlCommand com = new MySqlCommand("select * from test.stardict where frq = " + frq, connection);
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    words[i].Sid = Convert.ToInt32(reader["id"]);
                }
                connection.Close();
            }
        }
    }
}
