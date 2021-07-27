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
            InitializecomboBoxforDelete(comboBoxforDelete);
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
            gridControl1.DataSource = DAL.LoadT1Data();
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
                    DataTable DT = DAL.LoadDefectiveData(index, DAL.table.T2);
                    //绑定T2数据到子列表
                    gridControl2.DataSource = DT;
                    gridView2.RefreshData();
                    // MessageBox.Show("我是你爹");
                }
            }
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            MessageBox.Show("无不无聊");
        }

        private void gridControl2_Click(object sender, EventArgs e)
        {
            if (gridView2.RowCount == 0)
            {
                return;
            }

            if (gridView2.GetFocusedRow() != null)
            {
                string cellvalue = gridView2.GetFocusedRowCellValue("Index").ToString();

                int index = -1;

                if (int.TryParse(cellvalue, out index))
                {
                    DataTable DT = DAL.LoadDefectiveData(index, DAL.table.T3);
                    //绑定T3数据到子列表
                    gridControl3.DataSource = DT;
                    gridView3.RefreshData();
                }
            }
        }

        public static void InitializecomboBoxforDelete (ComboBox comboBoxforDelete)
        {
            comboBoxforDelete.Items.Add("T1 GX项");
            comboBoxforDelete.Items.Add("T2 错误1");
            comboBoxforDelete.Items.Add("T3 错误2");

        }

        private void Button3_Click(object sender, EventArgs e)
        {

        }

        public void Reload_form()
        {
            this.Hide(); //先隐藏主窗体

            Form form1 = new FMain(); //重新实例化此窗体

            form1.ShowDialog();//已模式窗体的方法重新打开

            this.Close();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Form export = new ExportToExcel();
            export.ShowDialog();
        }

        private void FMain_Load(object sender, EventArgs e)
        {

        }
    }
}
