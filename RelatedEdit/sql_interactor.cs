using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelatedEdit
{
    interface sql_interactor
    {   
        // 返回一条在窗口展示的确认信息
        string getConfirmationMessage(string table, string content);
        // 返回一条活动完成后的确认信息
        string getFinishMessage();
        // 与表三互动
        void interactT3(string index, string change_content);
        // 与表二互动
        void interactT2(string index, string change_content);
        // 与表一互动
        void interactT1(string index, string change_content);
    }
}
