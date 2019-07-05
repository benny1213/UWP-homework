项目共分为7个部分，分别为翻译、背单词、BBC新闻、每日一句、视频、计算器和用户管理。由于背单词模块和用户管理模块都需要用到数据，为了数据管理的简洁，我使用了Windows下的子系统ubuntu的mysql数据库。
 
![image](https://github.com/benny1213/Yenny/readmeIMG/image001.jpg)
主界面为一个汉堡菜单和一个视窗组成，汉堡菜单可以折叠打开，核心代码如图所示：
![image](https://github.com/benny1213/Yenny/readmeIMG/image002.jpg)



翻译界面由两个按钮和两个输入输出框组成
![image](https://github.com/benny1213/Yenny/readmeIMG/image003.jpg)
由于是在线翻译所以添加了一个process ring，在翻译结束之前会进行读条
![image](https://github.com/benny1213/Yenny/readmeIMG/image004.jpg)
在制作翻译系统的过程中从https://www.cnblogs.com/marso/p/google_translate_api.html这个网站学习到了网络编程方面的知识，了解了json格式并运用到我的代码当中。
此外，由于原代码实在控制台中以单线程形式运行，在uwp应用中会造成页面卡顿，所以我在代码中增加了异步方法将线程交还给用户界面如图
![image](https://github.com/benny1213/Yenny/readmeIMG/image005.jpg)
在背单词模块当中，
![image](https://github.com/benny1213/Yenny/readmeIMG/image006.jpg)
我使用了四个背单词界面，分别为Recite_StartPage、Recite、showword和endrecite
另外，创建了一个操纵数据库的cs文件：DBControl，在背单词开始界面（Recite_StartPage）中调用DBcontrol中的方法读取数据库文件储存至一个word（word.cs）类型的数组当中并将数组使用navigate方法的参数传递功能将数组传递至recite（背单词）页面当中如图所示。
![image](https://github.com/benny1213/Yenny/readmeIMG/image007.jpg)
另外，在数据库的构建过程中，我使用了https://github.com/skywind3000/ECDICT此项目中的数据库
![image](https://github.com/benny1213/Yenny/readmeIMG/image008.jpg)

在此项目中下载了csv文件通过navicat数据库管理软件导入到了数据库当中
![image](https://github.com/benny1213/Yenny/readmeIMG/image009.jpg)
这个词典虽然有通过python代码导入数据库的功能，但是我并没有成功，所以直接使用了csv文件导入的方式。
回到背单词界面，我使用了stackpanel将单词信息堆叠在页面当中，显示的时候通过控件的visibility属性控制控件的显示与合拢
![image](https://github.com/benny1213/Yenny/readmeIMG/image010.jpg)
当点击认识的时候会直接跳转至showword中（也是通过navigate方法传递参数）
![image](https://github.com/benny1213/Yenny/readmeIMG/image011.jpg)
当点击不认识的时候
![image](https://github.com/benny1213/Yenny/readmeIMG/image012.jpg)
程序会将“太简单”按钮不显示，并将熟悉程度降一个等级，显示出英文释义，如果此时点击认识按钮会直接跳转至showword页面当中，当点击不认识按钮会将用户对应单词的熟悉程度再次降低一个等级然后给出查看详情按钮
![image](https://github.com/benny1213/Yenny/readmeIMG/image013.jpg)
最后都导向showword页面中。
当20个一组单词背完的时候会显示endrecite（背单词结束页面）
此页面通篇显示20个一组单词并用红绿色标出用户对单词的熟悉程度
当点击下一组按钮的时候，会将页面导向recite（背单词页面），并将刚刚背诵的数据记录到数据库当中如图所示
![image](https://github.com/benny1213/Yenny/readmeIMG/image014.jpg)
其中userid为用户的id，wordid为单词的id，f_rate为该用户对该单词的熟悉程度这两列在数据库中以一组主键的形式存在。此外，数据库符合BCNF范式。
接下来是展示视频页面
![image](https://github.com/benny1213/Yenny/readmeIMG/image015.jpg)
用户可以通过左边的按钮选择视频播放
在计算器页面中，在老师的计算机的基础上添加了灰体字，暂时记录了运算过程、修改了一些bug比如直接按等号会出现错误等。
![image](https://github.com/benny1213/Yenny/readmeIMG/image016.jpg)
在用户管理界面中，分为两栏，左侧为用户选择，使用者可以在登陆后进入此页面，管理用户的名字，用户组等
![image](https://github.com/benny1213/Yenny/readmeIMG/image017.jpg)
右侧的详细信息显示通过x:bind mode=twoway 来双向绑定用户数据来修改用户的数据
当普通权限的用户点击保存或者新建按钮时会弹出对话框显示权限不足
![image](https://github.com/benny1213/Yenny/readmeIMG/image018.jpg)
以管理员用户登入时
![image](https://github.com/benny1213/Yenny/readmeIMG/image019.jpg)
可以新建，保存用户信息
![image](https://github.com/benny1213/Yenny/readmeIMG/image020.jpg)






此外，在绑定单词数据的时候需要使用函数将字符串拼接，单位使用x:bind的时候遇到了一些uwp的bug，我在StackOverflow网站中得到了解答（并没有真正解决问题）：
https://stackoverflow.com/questions/56344751/getting-wrong-when-i-use-xbind-in-uwp-invalid-binding-path-err
![image](https://github.com/benny1213/Yenny/readmeIMG/image021.jpg)
![image](https://github.com/benny1213/Yenny/readmeIMG/image022.jpg)
 
项目源码上传至Github：
https://github.com/benny1213/Yenny


以上



参考文献
中文字典的双解词典数据库
 https://github.com/skywind3000/ECDICT

C#实现谷歌翻译API
 https://www.cnblogs.com/marso/p/google_translate_api.html

Data binding overview 
https://docs.microsoft.com/en-us/windows/uwp/data-binding/data-binding-quickstart

Data binding and MVVM
https://docs.microsoft.com/en-us/windows/uwp/data-binding/data-binding-and-mvvm

Getting wrong when i use x:bind in uwp: invalid binding path err
https://stackoverflow.com/questions/56344751/getting-wrong-when-i-use-xbind-in-uwp-invalid-binding-path-err

HamburgerMenu XAML Control
https://docs.microsoft.com/en-us/windows/communitytoolkit/archive/hamburgermenu
