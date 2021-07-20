using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace RelatedEdit
{
    public static class DAL   // Data Access Layer
    {
        public static DataTable LoadT2Data(int T1Index)
        {
            DataTable DT = new DataTable();
            try
            {
                SqlConnection conn = new SqlConnection(Common.ConnString);
                System.Diagnostics.Debug.Print(Common.ConnString);
                string SQL = "SELECT * FROM [T2_Defective] Where GX_NO=" + T1Index.ToString();

                conn.Open();
                String state = conn.State.ToString();
                SqlCommand sc = new SqlCommand(SQL, conn);
                SqlDataAdapter sda = new SqlDataAdapter(sc);
                sda.Fill(DT);
                DT.Columns.Remove("GX_NO");
                DT.Columns.Remove("ID");
                DT.Columns.Remove("REM");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
            }
            return DT;
        }
    }
}
