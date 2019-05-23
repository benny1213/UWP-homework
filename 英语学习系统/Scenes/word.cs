using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 英语学习系统.Scenes//此类暂存从数据库中取出的单词（100个）
{
    
    class word
    {
        int sid;
        string sword;
        string sphonetic;
        string sdefinition;
        string stranslation;
        string spos;
        int scollins;
        int soxford;
        string stag;
        int sbnc;
        int sfrq;
        string sexchange;
        int sf_rate;

        public int Sid { get => sid; set => sid = value; }
        public string Sword { get => sword; set => sword = value; }
        public string Sphonetic { get => sphonetic; set => sphonetic = value; }
        public string Sdefinition { get => sdefinition; set => sdefinition = value; }
        public string Stranslation { get => stranslation; set => stranslation = value; }
        public string Spos { get => spos; set => spos = value; }
        public int Scollins { get => scollins; set => scollins = value; }
        public int Soxford { get => soxford; set => soxford = value; }
        public string Stag { get => stag; set => stag = value; }
        public int Sbnc { get => sbnc; set => sbnc = value; }
        public int Sfrq { get => sfrq; set => sfrq = value; }
        public string Sexchange { get => sexchange; set => sexchange = value; }
        public int Sf_rate { get => sf_rate; set => sf_rate = value; }
    }
}
