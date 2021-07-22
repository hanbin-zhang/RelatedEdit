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
        String delete_index1;

        public delete_confirmation(string delete_info, DAL.table table, String delete_index)
        {
            InitializeComponent();
            label1.Text = "请问确定要删除" + table.ToString() + "表下所属的" + delete_info + "及其所有下属关联项吗？";
            table_type = table;
            delete_index1 = delete_index;
        }

        private void delete_confirmation_Load(object sender, EventArgs e)
        {


        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (table_type == DAL.table.T3)
            {
                DeleteFromSQL.deleteFromT3(delete_index1);
            }
            else if (table_type == DAL.table.T2)
            {
                DeleteFromSQL.deleteFromT2(delete_index1);
            }
            else if (table_type == DAL.table.T1)
            {
                DeleteFromSQL.deleteFromT1(delete_index1);
            }
            MessageBox.Show("删除成功");
            this.Close();
        }
    }
}
