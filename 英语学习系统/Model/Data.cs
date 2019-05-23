using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 英语学习系统.Model
{
    public class Data
    {
        public static List<Student> students = new List<Student>(
            new[]
            {
                new Student("1","linsijia", "abc", "linsijia","linsijia.jpg"),
                new Student("2","linsijia2", "abc", "linsijia2","linsijia2.jpg"),
            }
            );
        
    }
}
