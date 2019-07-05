using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace 英语学习系统.Scenes
{
    
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class UserManage : Page
    {
        DBControl dBControl = new DBControl();
        bool flag = false;
        User[] users;
        public UserManage()
        {
            this.InitializeComponent();
            load();
        }

        private async void load()
        {

            users = dBControl.readuser();
            foreach (User user in users)
            {
                Image image = new Image();
                image.Source = await SetImg(user.Simg);
                image.Width = 300;
                image.Height = 400;

                TextBlock tbid = new TextBlock();
                tbid.Text = "id :" + user.Sid;
                tbid.HorizontalAlignment = HorizontalAlignment.Center;

                TextBlock tbname = new TextBlock();
                tbname.Text = "name :" + user.Sname;
                tbname.HorizontalAlignment = HorizontalAlignment.Center;

                TextBlock tbuserGroup = new TextBlock();
                tbuserGroup.Text = "group :" + (user.Susergroup == 1 ? "管理员" : "普通用户");
                tbuserGroup.HorizontalAlignment = HorizontalAlignment.Center;

                Border border = new Border();
                StackPanel stackPanel = new StackPanel();
                stackPanel.HorizontalAlignment = HorizontalAlignment.Center;
                border.BorderBrush = new SolidColorBrush(Colors.Black);
                border.BorderThickness = new Thickness(0);
                border.Padding = new Thickness(1);
                border.Margin = new Thickness(0,10,0,0);

                border.PointerEntered += Border_PointerEntered;
                border.PointerReleased += Border_PointerReleased;
                border.PointerExited += Border_PointerExited;

                stackPanel.Orientation = Orientation.Vertical;

                stackPanel.Children.Add(image);
                stackPanel.Children.Add(tbid);
                stackPanel.Children.Add(tbname);
                stackPanel.Children.Add(tbuserGroup);
                border.Child = stackPanel;
                spusers.Children.Add(border);

            }
        }

        private void Border_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            (sender as Border)
                .BorderThickness = new Thickness(0);
        }

        private async void Border_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            StackPanel sp = (StackPanel)(sender as Border).Child;
            var textBlock = (TextBlock)sp.Children[1];
            var id = Convert.ToInt32(textBlock.Text.Substring(4));
            foreach(User user in users)
            {
                if(user.Sid == id)
                {
                    gduserid.Text = user.Sid.ToString();
                    gdusername.Text = user.Sname;
                    gdpassword.Text = user.Spassword;
                    gdimg.Source = await SetImg(user.Simg);
                }
            }
        }

        private void Border_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            (sender as Border)
               .BorderThickness = new Thickness(1);
        }

        private void Btn_save(object sender, RoutedEventArgs e)
        {
            try
            {
                dBControl.write("update user set userName = '" + gdusername.Text + "', userPassword = '" + gdpassword.Text + "' where id = " + Convert.ToInt32(gduserid.Text) + ";");
                showMessage("保存成功");
            }
            catch(Exception ex)
            {
                showMessage(e.ToString());
            }
        }
        private void Btn_selectImg(object sender, RoutedEventArgs e)
        {

        }
        private void Btn_change(object sender, RoutedEventArgs e)
        {
            foreach(User user in users)
            {
                if(英语学习系统.App.userid == user.Sid)
                {
                    if(user.Susergroup == 1)
                    {
                        if(flag == false)
                        {
                            gdimg.Source = null;
                            gduserid.Text = "用户id为自增字段，无需更改";
                            gdusername.Text = "";
                            gdpassword.Text = "";
                            gdBtnimg.Visibility = Visibility.Visible;
                            gdgroup.Visibility = Visibility.Visible;
                            flag = !flag;
                            return;
                        }
                       
                        dBControl.write("insert into test.user(userName, userPassword, profilePhoto, userGroup) values('"+gdusername.Text+"', '"+gdpassword.Text+"', '"+"4.jpg"+"', "+Convert.ToInt32(gdgroup.Text)+");");
                        showMessage("新建成功");
                        return;
                    }
                }
            }
            showMessage("你没有权限新建用户");
        }
        public async Task<BitmapImage> SetImg(string imgfile)
        {
            try
            {
                StorageFolder folder = Windows.Storage.KnownFolders.PicturesLibrary;
                StorageFile file = await (await folder.GetFolderAsync("PrifilePicture")).GetFileAsync(imgfile);
                var filestream = await file.OpenAsync(FileAccessMode.Read);
                BitmapImage ImgSource = new BitmapImage();
                ImgSource.SetSource(filestream);
                return ImgSource;
            }
            catch (Exception e)
            {

                return null;
            }
        }
        private async void showMessage(string message)
        {
            var msgDialog = new Windows.UI.Popups.MessageDialog(message) { Title = "提示" };
            await msgDialog.ShowAsync();

        }

       
    }
}
