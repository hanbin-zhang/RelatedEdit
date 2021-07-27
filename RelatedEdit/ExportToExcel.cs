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
                string SQL = "SELECT GX_NAME, Defective, Defective2 FROM [NCMR].[dbo].[T1_GX] AS T1 LEFT JOIN [NCMR].[dbo].[T2_Defective] AS T2 ON T1.GX_NO = T2.GX_NO LEFT JOIN [NCMR].[dbo].[T3_Defective2] AS T3 ON T2.TD2_NO = T3.TD2_NO; ";
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
                            row[0].Value = dataRow[0].ToString();
                            row[1].Value = dataRow[1].ToString();
                            row[2].Value = dataRow[2].ToString();
                            n += 1;
                        }
                    }
                }
                conn.Close();

                workbook.Worksheets[0].Columns[0].AutoFit();
                workbook.Worksheets[0].Columns[1].AutoFit();
                workbook.Worksheets[0].Columns[2].AutoFit();
                workbook.SaveDocument("SavedDocument.xlsx", DocumentFormat.Xlsx);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MessageBox.Show("导出成功");
        }
    }
}
