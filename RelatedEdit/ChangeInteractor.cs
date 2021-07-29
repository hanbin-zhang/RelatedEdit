using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace RelatedEdit
{
    class ChangeInteractor : sql_interactor
    {
        public string getConfirmationMessage(string table, string content)
        {
            return string.Format("请在下方输入{0}表中的{1}修改后的内容", table, content);
        }

        public string getFinishMessage()
        {
            return "修改成功";
        }

        public void interactT1(string index, string change_content)
        {
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
    }
}
