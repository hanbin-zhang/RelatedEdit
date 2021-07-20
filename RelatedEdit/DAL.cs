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
        public static DataTable LoadT1Data()
        {
            DataTable DT = new DataTable();
            try
            {
                SqlConnection conn = new SqlConnection(Common.ConnString);
                System.Diagnostics.Debug.Print(Common.ConnString);
                string SQL = "SELECT [GX_NO], [GX_NAME] FROM [T1_GX]";
                
                using(SqlCommand sc = new SqlCommand(SQL, conn)) {
                    using(SqlDataAdapter sda = new SqlDataAdapter(sc)) {
                        sda.Fill(DT);
                    }
                }  
                DT.Columns["GX_NO"].ColumnName = "Index";
                DT.Columns["GX_NAME"].ColumnName = "Name";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
            }
            return DT;
        }
        public static DataTable LoadT2Data(int T1Index)
        {
            DataTable DT = new DataTable();
            try
            {
                SqlConnection conn = new SqlConnection(Common.ConnString);
                System.Diagnostics.Debug.Print(Common.ConnString);
                string SQL = "SELECT [TD2_NO], [Defective] FROM [T2_Defective] Where GX_NO=" + T1Index.ToString();

                conn.Open();
                String state = conn.State.ToString();
                SqlCommand sc = new SqlCommand(SQL, conn);
                SqlDataAdapter sda = new SqlDataAdapter(sc);
                sda.Fill(DT);
                DT.Columns["TD2_NO"].ColumnName = "Index";
                DT.Columns["Defective"].ColumnName = "Name";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
            }
            return DT;
        }
    }
}
