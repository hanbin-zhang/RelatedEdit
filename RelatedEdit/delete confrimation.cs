using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RelatedEdit
{
    public partial class delete_confirmation : Form
    {
        DAL.table table_type;

        public delete_confirmation(string delete_info, DAL.table table)
        {
            InitializeComponent();
            label1.Text = "请问确定要删除" + table.ToString() + "表下所属的" + delete_info + "及其所有下属关联项吗？";
            table_type = table;
        }

        private void delete_confirmation_Load(object sender, EventArgs e)
        {

        }
    }
}
