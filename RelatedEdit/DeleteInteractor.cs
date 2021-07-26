using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace RelatedEdit
{
    class DeleteInteractor: sql_interactor
    {
        public void interactT3(String index)
        {   
            string command1 = "DELETE FROM [T3_Defective2] WHERE [TD3_NO] = " + index;
            List<string> commands = new List<string>();
            commands.Add(command1);

            delete_helper(commands);
        }

        public void interactT2(String index)
        {
            string command1 = "DELETE FROM [T3_Defective2] WHERE [TD2_NO] = " + index;
            string command2 = "DELETE FROM [T2_Defective] WHERE [TD2_NO] = " + index;
            List<string> commands = new List<string>();
            commands.Add(command1);
            commands.Add(command2);

            delete_helper(commands);
        }

        public void interactT1(String index)
        {
            List<string> commands = new List<string>();
            DataTable DT = new DataTable();
            try
            {
                SqlConnection conn = new SqlConnection(Common.ConnString);
                System.Diagnostics.Debug.Print(Common.ConnString);
                string SQL = "SELECT [TD2_NO] FROM [NCMR].[dbo].[T2_Defective] WHERE [GX_NO] = " + index;
                using (SqlCommand sc = new SqlCommand(SQL, conn))
                {
                    conn.Open();
                    using (SqlDataAdapter sda = new SqlDataAdapter(sc))
                    {
                        sda.Fill(DT);
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
            }

            foreach (DataRow myRow in DT.Rows)
            {
                interactT2(myRow[0].ToString());
            }

            commands.Add("DELETE FROM [T1_GX] WHERE [GX_NO] = " + index);
            delete_helper(commands);
        }

        private void delete_helper(List<string> commands)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Common.ConnString);
                foreach (String command in commands)
                {
                    conn.Open();
                    using (SqlCommand sc = new SqlCommand(command, conn))
                    {
                        sc.ExecuteNonQuery();
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
            }
        }

        string sql_interactor.getConfirmationMessage()
        {
            return "请问确定要删除{0}表下所属的{1}及其所有下属关联项吗？";
        }

        string sql_interactor.getFinishMessage()
        {
            return "删除成功";
        }
    }
}
