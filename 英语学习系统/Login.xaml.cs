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

namespace 英语学习系统
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Login : Page
    {
        string username;
        string userpassword;
        public Login()
        {
            this.InitializeComponent();
        }
        private void pop()
        {
            poplogin.IsOpen = !poplogin.IsOpen;
        }

        private void pop_Click(object sender, RoutedEventArgs e)
        {
            poplogin.IsOpen = !poplogin.IsOpen;
        }
        private void login(object sender, RoutedEventArgs e)
        {
            username = tbusername.Text;
            userpassword = pbpassword.Password;
            DBControl dBControl = new DBControl();
            int userid = dBControl.login(username, userpassword);
            if(userid != -1)
            {
                英语学习系统.App.userid = userid;
                showMessage("登陆成功！");
            }
            else
            {
                showMessage("登陆失败");
            }
        }
        private void rege(object sender, RoutedEventArgs e)
        {
            username = tbusername.Text;
            userpassword = pbpassword.Password;
            DBControl dBControl = new DBControl();
            if (dBControl.write("INSERT INTO user ( userName, userPassword, userGroup ) VALUES ('"+username+"',"+userpassword+",2); "))//todo: 照片
                showMessage("注册成功");
                
        }
        private async void showMessage(string message)
        {
            var msgDialog = new Windows.UI.Popups.MessageDialog(message) { Title = "提示" };
            await msgDialog.ShowAsync();
        }
    }
}
