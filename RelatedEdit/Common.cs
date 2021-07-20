using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RelatedEdit
{
    public static class Common  //公共静态类
    {
        //生产环境中，连接字符串内的敏感信息(登录信息)应加密保存，并在运行时读入解密
        public static string ConnString = new StreamReader(@"..\..\paths\connectionString.txt").ReadLine();
        public class Record     //记录类
        {
            public int Index { get; set; }
            public string Name { get; set; }

            public Record(int index) //初始化函数，非必须，可重载
            {
                this.Index = index;
            }
            public Record(int index, string name) //初始化函数，非必须，可重载
            {
                this.Index = index;
                this.Name = name;
            }

        }
    }
}
