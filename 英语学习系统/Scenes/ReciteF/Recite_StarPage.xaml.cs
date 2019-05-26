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
        MySqlConnection MySqlConnection;
        MySqlCommand MySqlCommand;
        DataTable DataTable;
        DataRow DataRow;
        DataRowCollection DataRowCollection;
        MySqlDataAdapter mySqlDataAdapter;

        word[] words = new word[100];//对象数组暂存需要背的单词之后会传递给Recite页面

        public Recite_StarPage()
        {
            
            this.InitializeComponent();

            MySqlConnection = new MySqlConnection("datasource=127.0.0.1;port=3306;username=root;password=root");
            MySqlConnection.Open();
            MySqlCommand = new MySqlCommand("select * from test.stardict where frq != 0  order by rand() limit 20;", MySqlConnection);
            mySqlDataAdapter = new MySqlDataAdapter(MySqlCommand);
            DataSet dataSet = new DataSet();
            mySqlDataAdapter.Fill(dataSet, "stardict");

            String test = dataSet.Tables["stardict"].Rows[0][1].ToString();
            DataTable = dataSet.Tables["stardict"];
            DataRowCollection = DataTable.Rows;
            for(int i = 0; i < DataRowCollection.Count; i++)
            {
                words[i] = new word();
                DataRow = DataRowCollection[i];
                words[i].Sid = Convert.ToInt32(DataRow[0] == DBNull.Value ? 0 : DataRow[0]); //这里convert中的表达式的目的是避免convert无法转换dbnull类型的变量而报错
                words[i].Sword = DataRow[1].ToString();
                words[i].Sphonetic = DataRow[2].ToString();
                words[i].Sdefinition = DataRow[3].ToString();
                words[i].Stranslation = DataRow[4].ToString();
                words[i].Spos = DataRow[5].ToString();
                words[i].Scollins = Convert.ToInt32(DataRow[6] == DBNull.Value ? 0 : DataRow[6]);
                words[i].Soxford = Convert.ToInt32(DataRow[7] == DBNull.Value ? 0 : DataRow[7]);
                words[i].Stag = DataRow[8].ToString();
                words[i].Sbnc = Convert.ToInt32(DataRow[9] == DBNull.Value ? 0 : DataRow[9]);
                words[i].Sfrq = Convert.ToInt32(DataRow[10] == DBNull.Value ? 0 : DataRow[10]);
                words[i].Sexchange = DataRow[11].ToString();
                words[i].Sf_rate = Convert.ToInt32(DataRow[12] == DBNull.Value ? 0 : DataRow[12]);
            }

            MySqlConnection.Close();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            Frame.Navigate(typeof(ReciteF.Recite), words);
        }
    }
}
