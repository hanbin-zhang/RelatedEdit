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
        DAL.table table_type;
        string t1_selected_type;
        public FMain()
        {
            InitializeComponent();
            table_type = DAL.table.Null;
            grid_view_restore_helper();
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
            if (gridView1.GetFocusedRowCellValue("Index").ToString() == t1_selected_type) return;
            else if (gridView1.GetFocusedRow() != null)
            {
                string cellvalue = gridView1.GetFocusedRowCellValue("Index").ToString();
                t1_selected_type = cellvalue;
                gridControl3.DataSource = new DataTable();
                gridView3.RefreshData();
                int index = -1;

                if (int.TryParse(cellvalue, out index))
                {
                    DataTable DT = DAL.LoadDefectiveData(index, DAL.table.T2);
                    //绑定T2数据到子列表
                    gridControl2.DataSource = DT;
                    gridView2.RefreshData();

                }
            }
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            this.table_type = DAL.table.T1;
            grid_view_restore_helper();
            selected_table_helper(gridView1);
            label1.Text = table_type.ToString();
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

        public void Reload_form(DAL.table table_type, int index)
        {   
            if(table_type == DAL.table.T1)
            {
                gridControl1.DataSource = DAL.LoadT1Data();
                gridView1.RefreshData();
            }
            else if(table_type == DAL.table.T2)
            {
                string cellvalue = gridView1.GetFocusedRowCellValue("Index").ToString();
                gridControl2.DataSource = DAL.LoadDefectiveData(Convert.ToInt32(cellvalue), table_type);
                gridView2.RefreshData();
            }
            else if(table_type == DAL.table.T3)
            {
                string cellvalue = gridView2.GetFocusedRowCellValue("Index").ToString();
                gridControl3.DataSource = DAL.LoadDefectiveData(Convert.ToInt32(cellvalue), table_type);
                gridView3.RefreshData();
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Form export = new ExportToExcel();
            export.ShowDialog();
        }

        private void FMain_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            int a = gridViewselectHelper(out string item_name, out string item_index);

            if (a == 0) return;
            // 打开确认删除窗口
            // 打开确认窗口, c
            Form confirmation_form = new ConfirmationWindow(item_name, this.table_type, item_index, interaction_type.change);
            confirmation_form.ShowDialog();

            // 重新加载窗体
            Reload_form(table_type, Convert.ToInt32(item_index));
        }

        private void delete_click(object sender, EventArgs e)
        {
            int a = gridViewselectHelper(out string item_name, out string item_index);
            if (a == 0) return;
            // 打开确认删除窗口
            // 打开确认窗口, c
            Form confirmation_form = new ConfirmationWindow(item_name, this.table_type, item_index, interaction_type.delete);
            confirmation_form.ShowDialog();

            // 重新加载窗体
            Reload_form(table_type, Convert.ToInt32(item_index));
            if (table_type == DAL.table.T2)
            {
                gridControl3.DataSource = new DataTable();
                gridView3.RefreshData();
            }
            else if(table_type == DAL.table.T1)
            {
                gridControl2.DataSource = new DataTable();
                gridView2.RefreshData();
                gridControl3.DataSource = new DataTable();
                gridView3.RefreshData();
            }
        }

        private int gridViewselectHelper(out string item_name, out String item_index)
        {
            item_name = "";
            item_index = "";
            if (this.table_type == DAL.table.Null)
            {
                MessageBox.Show("请选择需要操作的类别（双击以选中）");
                return 0;
            }
            else if (this.table_type == DAL.table.T1)
            {
                if (gridView1.GetFocusedRowCellValue("Index") == null) { MessageBox.Show("请先选择需要操作的项"); return 0; }
                item_index = gridView1.GetFocusedRowCellValue("Index").ToString();
                item_name = gridView1.GetFocusedRowCellValue("Name").ToString();
            }
            else if (this.table_type == DAL.table.T2)
            {
                if (gridView2.GetFocusedRowCellValue("Index") == null) { MessageBox.Show("请先选择需要操作的项"); return 0; }
                item_index = gridView2.GetFocusedRowCellValue("Index").ToString();
                item_name = gridView2.GetFocusedRowCellValue("Name").ToString();
            }
            else if (this.table_type == DAL.table.T3)
            {
                if (gridView3.GetFocusedRowCellValue("Index") == null) { MessageBox.Show("请先选择需要操作的项"); return 0; }
                item_index = gridView3.GetFocusedRowCellValue("Index").ToString();
                item_name = gridView3.GetFocusedRowCellValue("Name").ToString();
            }
            return 1;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string item_name = "";
            string item_index = "-1";
            if (this.table_type == DAL.table.Null)
            {
                MessageBox.Show("请选择需要操作的类别（双击以选中）");
                return;
            }
            else if (this.table_type == DAL.table.T2)
            {
                if (gridView1.GetFocusedRowCellValue("Index") == null) { MessageBox.Show("请先选择新增项的母项"); return; }
                item_index = gridView1.GetFocusedRowCellValue("Index").ToString();
                item_name = gridView1.GetFocusedRowCellValue("Name").ToString();
            }
            else if (this.table_type == DAL.table.T3)
            {
                if (gridView2.GetFocusedRowCellValue("Index") == null) { MessageBox.Show("请先选择新增项的母项"); return; }
                item_index = gridView2.GetFocusedRowCellValue("Index").ToString();
                item_name = gridView2.GetFocusedRowCellValue("Name").ToString();
            }

            Form confirmation_form = new ConfirmationWindow(item_name, this.table_type, item_index, interaction_type.add);
            confirmation_form.ShowDialog();

            // 重新加载窗体
            Reload_form(table_type, Convert.ToInt32(item_index));
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        
        // a helper method used to change the apperance of gridview when it is double clicked
        private void selected_table_helper(DevExpress.XtraGrid.Views.Grid.GridView gridView)
        {
            gridView.Appearance.ViewCaption.ForeColor = Color.Blue;
            gridView.Appearance.ViewCaption.BackColor = Color.Yellow;
            gridView.Appearance.ViewCaption.Font = new Font(gridView.Appearance.ViewCaption.Font.FontFamily, gridView.Appearance.ViewCaption.Font.Size, FontStyle.Bold);
        }

        // a helper method which restore all the viewcaption to default status
        private void grid_view_restore_helper()
        {
            DevExpress.XtraGrid.Views.Grid.GridView[] gridViews = { gridView1, gridView2, gridView3};
            foreach (DevExpress.XtraGrid.Views.Grid.GridView gridView in gridViews)
            {
                gridView.Appearance.ViewCaption.ForeColor = Color.Black;
                gridView.Appearance.ViewCaption.Font = new Font(gridView.Appearance.ViewCaption.Font.FontFamily, Convert.ToSingle(12));
            }
        }

        private void gridControl2_DoubleClick(object sender, EventArgs e)
        {
            this.table_type = DAL.table.T2;
            grid_view_restore_helper();
            selected_table_helper(gridView2);
            label1.Text = table_type.ToString();
        }

        private void gridControl3_DoubleClick(object sender, EventArgs e)
        {
            this.table_type = DAL.table.T3;
            grid_view_restore_helper();
            selected_table_helper(gridView3);
            label1.Text = table_type.ToString();
        }
    }
}
