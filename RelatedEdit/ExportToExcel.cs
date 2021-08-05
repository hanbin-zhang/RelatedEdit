using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using DevExpress.Spreadsheet;
using System.IO;
using static System.Convert;

namespace RelatedEdit
{
    public partial class ExportToExcel : Form
    {
        public ExportToExcel()
        { 
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Common.ConnString);
                conn.Open();
                string SQL = "SELECT T1.GX_NO, GX_NAME, T2.TD2_NO, Defective, T3.TD3_NO, Defective2 FROM [NCMR].[dbo].[T1_GX] AS T1 LEFT JOIN [NCMR].[dbo].[T2_Defective] AS T2 ON T1.GX_NO = T2.GX_NO LEFT JOIN [NCMR].[dbo].[T3_Defective2] AS T3 ON T2.TD2_NO = T3.TD2_NO; ";
                Workbook workbook = new Workbook();
                workbook.CreateNewDocument();
                using (SqlCommand sc = new SqlCommand(SQL, conn))
                {
                    using (SqlDataReader sdr = sc.ExecuteReader())
                    {   
                        // 此参数用来示意当今在表的第几行
                        int n = 0;
                        while (sdr.Read())
                        {
                            Row row = workbook.Worksheets[0].Rows[n];
                            IDataRecord dataRow = (IDataRecord)sdr;
                            for (int i = 0; i<=5; i++)
                            {   
                                if (dataRow[i].ToString() == "") row[i].Value = dataRow[i].ToString();
                                else if ((i%2)==0) row[i].Value = ToInt32(dataRow[i].ToString());
                                else row[i].Value = dataRow[i].ToString();
                            }
                            n += 1;
                        }
                    }
                }
                conn.Close();

                for (int i = 0; i <= 5; i++)
                {
                    workbook.Worksheets[0].Columns[i].AutoFit();
                }
                workbook.SaveDocument(saveFileDialog1.FileName, DocumentFormat.Xlsx);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            MessageBox.Show("导出成功");
            this.Close();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            DialogResult dr = this.saveFileDialog1.ShowDialog();
            if (dr == DialogResult.OK && this.saveFileDialog1.FileName.Length > 0)
            {
                this.textBox1.Text = saveFileDialog1.FileName;
            }
        }

        private void ExportToExcel_Load(object sender, EventArgs e)
        {

        }

        private void ExportToExcel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
        }

        private void button2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
        }
    }
}
