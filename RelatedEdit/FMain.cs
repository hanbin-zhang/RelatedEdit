using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace RelatedEdit
{
    public partial class FMain : Form
    {
        public FMain()
        {
            
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*
            GridControl控件支持绑定两种数据源：数据表DataTable和泛型集合List<T>
            GridControl中的列，通过FieldName属性，关联Datatable的ColumnName，或者是集合元素 Common.Record 的属性
            本示例中，已在设计器中创建字段并关联数据，无需代码绑定
            如要动态创建或绑定，方式如下
            
            DevExpress.XtraGrid.Columns.GridColumn gc = gridView1.Columns.Add();//创建列
            gc.Caption = "Index";   //指定列标题
            gc.FieldName = "Index"; //绑定数据表列名或对象属性名
            */

            List<Common.Record> RList = new List<RelatedEdit.Common.Record>();
            RList.Add(new Common.Record(1, "下线"));
            RList.Add(new Common.Record(2, "端子"));
            RList.Add(new Common.Record(3, "点焊"));
            RList.Add(new Common.Record(4, "蘸锡"));
            RList.Add(new Common.Record(5, "锡焊"));


            gridControl1.DataSource = RList;
            gridView1.RefreshData();

        }

        private void button2_Click(object sender, EventArgs e)
        {

            DataTable DT = new DataTable("TableDemo");

            DT.Columns.Add("Index", typeof(System.Int16));
            DT.Columns.Add("Name", typeof(System.String));

            DT.Rows.Add(6, "注塑");
            DT.Rows.Add(7, "焊接");
            DT.Rows.Add(8, "热缩");
            DT.Rows.Add(9, "初装");
            DT.Rows.Add(10, "布线");


            gridControl1.DataSource = DT;
            gridView1.RefreshData();
        }


        private void gridControl1_Click(object sender, EventArgs e)
        {
            if (gridView1.RowCount == 0)
            {
                return;
            }

            if (gridView1.GetFocusedRow() != null)
            {
                string cellvalue =  gridView1.GetFocusedRowCellValue("Index").ToString();

                int index = -1;

                if (int.TryParse(cellvalue, out index))
                {
                    DataTable DT = DAL.LoadT2Data(index);
                    //绑定T2数据到子列表
                    gridControl2.DataSource = DT;
                    gridView2.RefreshData();
                }

                
                MessageBox.Show(gridView1.GetFocusedRowCellValue("Name").ToString());
            }
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {

        }
    }
}
