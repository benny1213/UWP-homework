using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Text;
using System.Text.RegularExpressions;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Threading;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace 英语学习系统.Scenes
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Translate : Page
    {
        public Translate()
        {
            this.InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)//click 属性只能全部运行完之后才反馈？
        {
            //todo:美化用户界面
            //todo:加入无法连接网站（断网）时的提示
            progressRing.IsActive = true;
            outbox.Text = await googleTranslation(inbox.Text);
            progressRing.IsActive = false;
        }

        

        public static async Task<string> googleTranslation(string text)
        {
            if (text == "" || text == null)
            {
                return "";
            }
            else
            {
                string result = "";
                string url = "https://translate.google.cn/translate_a/single?client=gtx&sl=en&tl=zh-CN&dt=t&q=" + text;
                //string jsonData = GetInfo(url);
                string jsonData = await myGetInfo(url);
                string pattern = "\"([^\"]*)\"";
                int count = Regex.Matches(jsonData, pattern).Count;
                MatchCollection matches = Regex.Matches(jsonData, pattern);
                for (int i = 0; i < count - 1; i += 2)
                {
                    result += matches[i].Value.Trim().Replace("\"", "");
                }
                

                return result;
            }
        }
        public static bool InChinese(string StrChineseString)
        {
            return Regex.IsMatch(StrChineseString, ".*[\\u4e00-\\u9faf].*");
        }
        public static string GetInfo(string url)//这里是原代码，没有使用async导致在翻译过程会使progressring不能转动
        {
            
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
            //访问http方法  
            string strBuff = "";
            Uri httpURL = new Uri(url);
            ///HttpWebRequest类继承于WebRequest，并没有自己的构造函数，需通过WebRequest的Creat方法建立，并进行强制的类型转换     
            HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(httpURL);
            ///通过HttpWebRequest的GetResponse()方法建立HttpWebResponse,强制类型转换     
            HttpWebResponse httpResp = (HttpWebResponse)httpReq.GetResponse();
            ///GetResponseStream()方法获取HTTP响应的数据流,并尝试取得URL中所指定的网页内容     
            ///若成功取得网页的内容，则以System.IO.Stream形式返回，若失败则产生ProtoclViolationException错误。在此正确的做法应将以下的代码放到一个try块中处理。这里简单处理     
            Stream respStream = httpResp.GetResponseStream();
            ///返回的内容是Stream形式的，所以可以利用StreamReader类获取GetResponseStream的内容，并以     
            //StreamReader类的Read方法依次读取网页源程序代码每一行的内容，直至行尾（读取的编码格式：UTF8）     
            StreamReader respStreamReader = new StreamReader(respStream, Encoding.UTF8);
            strBuff = respStreamReader.ReadToEnd();
            return strBuff;
        }

        public static async Task<string> myGetInfo(String url) {
            Uri httpURL = new Uri(url);
            HttpWebRequest httpreq = (HttpWebRequest)WebRequest.Create(httpURL);
            if (httpreq != null)
            {
                using (WebResponse httpResp = await httpreq.GetResponseAsync())//这里修改了原代码的方法，将其改为Async方法将线程权交还给用户界面，使得progressring 能转动
                {
                    using (Stream httpRespStream = httpResp.GetResponseStream())
                    {
                        StreamReader httpRespStreamReader = new StreamReader(httpRespStream, Encoding.UTF8);
                        return await httpRespStreamReader.ReadToEndAsync();
                    }
                }
            }
            else
                return null;
        }
        public static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            //直接确认，否则打不开  
            return true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string temp = inbox.Text;
            inbox.Text = outbox.Text;
            outbox.Text = temp;

            temp = (string)inbox.Header;
            inbox.Header = outbox.Header;
            outbox.Header = temp;
            
        }
    }
}
