using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace RelatedEdit
{
    class ChangeInteractor : sql_interactor
    {
        public string getConfirmationMessage(DAL.table table, string content)
        {
            return string.Format("请在下方输入{0}表中的{1}修改后的项目", table.ToString(), content);
        }

        public string getFinishMessage()
        {
            return "修改成功";
        }

        public void interactT1(string index, string change_content)
        {
            try
            {
                duplicateCheckHelper(DAL.table.T1, index, change_content);
            }
            catch (ArgumentException)
            {
                throw new ArgumentException("duplicate name");
            }

            try
            {
                string command = string.Format("UPDATE T1_GX SET GX_NAME = '{0}' WHERE GX_NO = '{1}'", change_content, index);
                SqlConnection conn = new SqlConnection(Common.ConnString);
                conn.Open();
                using (SqlCommand sc = new SqlCommand(command, conn))
                {
                    sc.ExecuteNonQuery();
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
            }
        }

        public void interactT2(string index, string change_content)
        {
            try
            {
                duplicateCheckHelper(DAL.table.T2, index, change_content);
            }
            catch (ArgumentException)
            {
                throw new ArgumentException("duplicate name");
            }

            try
            {
                string command = string.Format("UPDATE T2_Defective SET Defective = '{0}' WHERE TD2_NO = '{1}'", change_content, index);
                SqlConnection conn = new SqlConnection(Common.ConnString);
                conn.Open();
                using (SqlCommand sc = new SqlCommand(command, conn))
                {
                    sc.ExecuteNonQuery();
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
            }
        }

        public void interactT3(string index, string change_content)
        {   
            try
            {
                duplicateCheckHelper(DAL.table.T3, index, change_content);
            }
            catch(ArgumentException)
            {
                throw new ArgumentException("duplicate name");
            }

            try
            {
                string command = string.Format("UPDATE T3_Defective2 SET Defective2 = '{0}' WHERE TD3_NO = '{1}'", change_content, index);
                SqlConnection conn = new SqlConnection(Common.ConnString);
                conn.Open();
                using (SqlCommand sc = new SqlCommand(command, conn))
                {
                    sc.ExecuteNonQuery();
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
            }
        }

        private void duplicateCheckHelper(DAL.table table_type, string index, string change_content)
        {
            SqlConnection conn = new SqlConnection(Common.ConnString);
            conn.Open();

            string command2 = "";
            if (table_type == DAL.table.T1) command2 = string.Format("select count(*) from T1_GX where GX_NAME = '{0}';", change_content);


            else if (table_type == DAL.table.T2)
            {
                string parentIndex = getParentIndexHelper(table_type, index, change_content);
                command2 = string.Format("select count(*) from T2_Defective where GX_NO = '{0}' and Defective = '{1}';", parentIndex, change_content);
            }
            else if (table_type == DAL.table.T3)
            {
                string parentIndex = getParentIndexHelper(table_type, index, change_content);
                command2 = string.Format("select count(*) from T3_Defective2 where TD2_NO = '{0}' and Defective2 = '{1}';", parentIndex, change_content);
            }
            

            using (SqlCommand sc = new SqlCommand(command2, conn))
            {
                using (SqlDataReader sdr = sc.ExecuteReader())
                {
                    sdr.Read();
                    IDataRecord idr = (IDataRecord)sdr;
                    if (Convert.ToInt32(idr[0]) > 0) { throw new ArgumentException("duplicate name"); }
                }
            }
            conn.Close();
        }

        private string getParentIndexHelper(DAL.table table_type, string index, string name)
        {
            SqlConnection conn = new SqlConnection(Common.ConnString);
            conn.Open();
            string command = "";
            if (table_type == DAL.table.T2) command = string.Format("select GX_NO from T2_Defective where TD2_NO = '{0}';", index);
            else if (table_type == DAL.table.T3) command = string.Format("select TD2_NO from T3_Defective2 where TD3_NO = '{0}';", index);
            using (SqlCommand sc = new SqlCommand(command, conn))
            {
                using (SqlDataReader sdr = sc.ExecuteReader())
                {
                    sdr.Read();
                    IDataRecord idr = (IDataRecord)sdr;
                    return idr[0].ToString();
                }
            }
        }
    }
}
