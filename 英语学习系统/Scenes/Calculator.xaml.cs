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

namespace 英语学习系统.Scenes
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Calculator : Page
    {
        public Calculator()
        {
            this.InitializeComponent();
        }
        double a, b;

        string flag = "input a";
        string op;
        private void buttonClick(object sender, RoutedEventArgs e)
        {
            string mark = (sender as Button).Tag.ToString();
            switch (mark)
            {
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                case "0":
                    if (tb.Text == "0")//消“0”
                        tb.Text = "";
                    if (flag == "input a")
                    {
                        flag = "inputing a";
                        tb.Text = "";
                    }
                    if (flag == "input b")
                    {
                        if (tb.Text != "0.")
                            tb.Text = "";
                        flag = "inputing b";
                    }
                    tb.Text += mark;
                    break;
                case ".":
                    if (flag == "input b")//当输入b时
                    {
                        tb.Text = "0.";
                        flag = "inputing b";
                    }
                    if (tb.Text.IndexOf(".", 0) == -1)
                    {
                        tb.Text += ".";
                        if (flag == "input a")
                            flag = "inputing a";
                        if (flag == "input b")
                            flag = "inputing b";
                    }
                    break;
                case "B":
                    if (flag == "input b")//输入b时，拒绝用户删除
                        return;
                    if (tb.Text.Length != 1)
                    {
                        tb.Text = tb.Text.Remove(tb.Text.Length - 1, 1);
                    }
                    else
                    {
                        if (tb.Text != "0")
                            tb.Text = "0";
                    }
                    if (tb.Text.IndexOf("-", 0) == 0 && tb.Text.Length == 1)
                    {
                        tb.Text = "0";
                    }
                    break;
                case "+":
                case "-":
                case "×":
                case "÷":
                case "="://fuck
                    switch (flag)
                    {
                        case "inputing a"://第一次按运算符号，储存a值与运算符号并更改flag
                            if (tb.Text.Length == 0)//若不输入字符直接输入运算符号理都不理他，直接返回。
                                return;
                            a = Convert.ToDouble(tb.Text);
                            if (mark != "=")
                            {
                                op = mark;
                                flag = "input b";
                                db.Text += a.ToString() + mark;
                            }
                            break;
                        case "input b": //如果此时flag为"input b"说明用户未输入b，直接按了运算符号
                            db.Text = db.Text.Remove(db.Text.Length - 1, 1) + mark;
                            op = mark;
                            return;
                        case "inputing b"://进行运算,并将flag更改回input b 提示需要输入b
                            b = Convert.ToDouble(tb.Text);
                            db.Text += b.ToString();
                            a = calculating(op, a, b);
                            tb.Text = a.ToString();
                            flag = "input b";
                            db.Text += mark;
                            if (mark != "=" && mark != op)
                                op = mark;
                            if (mark == "=")
                            {
                                flag = "input a";
                                db.Text = "";
                            }
                            break;
                    }
                    break;
                case "C":
                    init();
                    break;


                case "±":
                    if (Convert.ToDouble(tb.Text) == 0)
                        return;
                    if (tb.Text.IndexOf("-") != -1)
                    {
                        tb.Text = tb.Text.Remove(0, 1);
                    }
                    else
                    {
                        tb.Text = tb.Text.Insert(0, "-");
                    }
                    break;
                case "test":
                    tb.Text = tb.Text.Insert(0, "-");
                    break;
            }
        }
        double calculating(string op, double a, double b)
        {
            switch (op)
            {
                case "+":
                    return a + b;
                case "-":
                    return a - b;
                case "×":
                    return a * b;
                case "÷":
                    return a / b;
                case "=":
                    return b;
                default:
                    flag = "err";
                    return 0;
            }
        }
        void init()//计算器初始化
        {
            a = b = 0;
            tb.Text = "0";
            db.Text = "";
            op = null;
            flag = "input a";
        }
    }
}
