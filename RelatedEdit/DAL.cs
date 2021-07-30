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
                    using (SqlDataAdapter sda = new SqlDataAdapter(sc)) {
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
        public enum table
        {
            T1,
            T2,
            T3
        }
        public static DataTable LoadDefectiveData(int Index, table tableNumber)
        {
            String query = "";
            if (tableNumber == table.T2) query = "SELECT [TD2_NO], [Defective] FROM [T2_Defective] Where GX_NO=";
            else if (tableNumber == table.T3) query = "SELECT [TD3_NO], [Defective2] FROM [T3_Defective2] Where TD2_NO=";
            DataTable DT = new DataTable();
            try
            {
                SqlConnection conn = new SqlConnection(Common.ConnString);
                System.Diagnostics.Debug.Print(Common.ConnString);
                query += Index.ToString();

                conn.Open();
                String state = conn.State.ToString();
                SqlCommand sc = new SqlCommand(query, conn);
                SqlDataAdapter sda = new SqlDataAdapter(sc);
                sda.Fill(DT);
                DT.Columns[0].ColumnName = "Index";
                DT.Columns[1].ColumnName = "Name";
                conn.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
            }
            return DT;
        }
    }
}
