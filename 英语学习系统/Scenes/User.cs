using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 英语学习系统.Scenes
{
    class User
    {
        int sid;
        string sname;
        string spassword;
        string simg;
        int susergroup;

        public int Sid { get => sid; set => sid = value; }
        public string Sname { get => sname; set => sname = value; }
        public string Spassword { get => spassword; set => spassword = value; }
        public string Simg { get => simg; set => simg = value; }
        public int Susergroup { get => susergroup; set => susergroup = value; }
    }
}
