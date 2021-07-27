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
                using (SqlCommand sc = new SqlCommand(SQL, conn))
                {
                    using (SqlDataReader sdr = sc.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            IDataRecord dataRow = (IDataRecord)sdr;
                            MessageBox.Show(dataRow[0].ToString());
                            MessageBox.Show(dataRow[1].ToString());
                            if (dataRow[2].ToString() != "") MessageBox.Show(dataRow[2].ToString());
                            else MessageBox.Show("afgdfgd");
                        }
                    }
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
