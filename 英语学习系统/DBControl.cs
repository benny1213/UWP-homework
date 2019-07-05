using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace 英语学习系统
{
    
    class DBControl
    {
        MySqlConnection MySqlConnection;
        MySqlCommand MySqlCommand;
        DataTable DataTable;
        DataRow DataRow;
        DataRowCollection DataRowCollection;
        MySqlDataAdapter mySqlDataAdapter;
        Scenes.word[] words;

        public int login(string username, string password)
        {
            MySqlConnection = new MySqlConnection("datasource=127.0.0.1;port=3306;username=root;password=root;database=test");
            MySqlConnection.Open();
            MySqlCommand = new MySqlCommand("select * from user where username = '"+username+"';" , MySqlConnection);
            MySqlDataReader reader = MySqlCommand.ExecuteReader();
            
            while (reader.Read())
            {
                if(password == reader["userPassword"].ToString())
                {
                    int id = Convert.ToInt32(reader["id"]);
                    MySqlConnection.Close();
                    return id;
                }
            }
            MySqlConnection.Close();
            return -1;

        }

        public Scenes.word[] read(String condition, int amount)
        {
            words = new Scenes.word[amount];
            MySqlConnection = new MySqlConnection("datasource=127.0.0.1;port=3306;username=root;password=root");
            MySqlConnection.Open();
            MySqlCommand = new MySqlCommand("select * from test.stardict where " + condition + ";", MySqlConnection);
            mySqlDataAdapter = new MySqlDataAdapter(MySqlCommand);
            DataSet dataSet = new DataSet();
            mySqlDataAdapter.Fill(dataSet, "stardict");

            String test = dataSet.Tables["stardict"].Rows[0][1].ToString();
            DataTable = dataSet.Tables["stardict"];
            DataRowCollection = DataTable.Rows;
            for (int i = 0; i < DataRowCollection.Count; i++)//将数据库中读取到的数据放入数组当中
            {
                words[i] = new Scenes.word();
                DataRow = DataRowCollection[i];
                words[i].Sid = Convert.ToInt32(DataRow[0] == DBNull.Value ? 0 : DataRow[0]); //这里convert中的表达式的目的是避免convert无法转换dbnull类型的变量而报错
                words[i].Sword = DataRow[1].ToString();
                words[i].Sphonetic = DataRow[2].ToString();
                words[i].Sdefinition = DataRow[3].ToString().Replace("\n", " ");
                words[i].Stranslation = DataRow[4].ToString().Replace("\n", " ");
                words[i].Spos = DataRow[5].ToString();
                words[i].Scollins = Convert.ToInt32(DataRow[6] == DBNull.Value ? 0 : DataRow[6]);
                words[i].Soxford = Convert.ToInt32(DataRow[7] == DBNull.Value ? 0 : DataRow[7]);
                words[i].Stag = DataRow[8].ToString();
                words[i].Sbnc = Convert.ToInt32(DataRow[9] == DBNull.Value ? 0 : DataRow[9]);
                words[i].Sfrq = Convert.ToInt32(DataRow[10] == DBNull.Value ? 0 : DataRow[10]);
                words[i].Sexchange = DataRow[11].ToString();
            }

            foreach(Scenes.word word in words)
            {
                //MySqlCommand = new MySqlCommand("select * from test.recite where userID = " + + "" + "");
            }
            MySqlConnection.Close();

            

            return words;
        }

        public bool write(String command)
        {
            MySqlConnection = new MySqlConnection("datasource=127.0.0.1;port=3306;username=root;password=root;database=test");
            MySqlConnection.Open();
            MySqlCommand = new MySqlCommand(command, MySqlConnection);
            MySqlCommand.ExecuteNonQuery();
            MySqlConnection.Close();
            return true;
        }

    }
}
