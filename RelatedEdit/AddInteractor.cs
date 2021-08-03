using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace RelatedEdit
{
    class AddInteractor : sql_interactor
    {
        public string getConfirmationMessage(DAL.table table, string content)
        {
            if (table == DAL.table.T1) return string.Format("请在下方输入{0}表中新增的项目", table.ToString());
            else return string.Format("请在下方输入{0}表中新增的{1}的子项目", table.ToString(), content);
        }

        public string getFinishMessage()
        {
            return "新增成功";
        }

        public void interactT1(string index, string change_content)
        {
            String getIndexCommand = "SELECT TOP (1) [GX_NO] FROM [NCMR].[dbo].[T1_GX] ORDER BY ID desc";
            String addCommand = "INSERT INTO T1_GX (GX_NO, GX_NAME) VALUES ({0}, '" + change_content + "')";
            String checkDuplicateCommand = string.Format("select count(*) from T1_GX where GX_NAME = '{0}';", change_content);
            add_helper(getIndexCommand, addCommand, checkDuplicateCommand);
        }

        public void interactT2(string index, string change_content)
        {
            String getIndexCommand = "SELECT TOP (1) [TD2_NO] FROM [T2_Defective] ORDER BY ID desc";
            String addCommand = "INSERT INTO T2_Defective ([GX_NO], [TD2_NO], [Defective]) VALUES (" + index + ", " + "{0}, '" + change_content + "')";
            String checkDuplicateCommand = string.Format("select count(*) from T2_Defective where Defective = '{0}' and GX_NO = {1};", change_content, index);
            add_helper(getIndexCommand, addCommand, checkDuplicateCommand);
        }

        public void interactT3(string index, string change_content)
        {
            String getIndexCommand = "SELECT TOP (1) [TD3_NO] FROM [T3_Defective2] ORDER BY ID desc";
            String addCommand = "INSERT INTO T3_Defective2 ([TD2_NO], [TD3_NO], [Defective2]) VALUES (" + index + ", " + "{0}, '" + change_content + "')";
            String checkDuplicateCommand = string.Format("select count(*) from T3_Defective2 where Defective2 = '{0}' and TD2_NO = {1};", change_content, index);
            try
            {
                add_helper(getIndexCommand, addCommand, checkDuplicateCommand);
            }

            catch(ArgumentException)
            {
                throw new ArgumentException("duplicate name");
            }
            
        }

        private void add_helper(String getIndexCommand, String addCommand, string checkDuplicateCommand)
        {
            
                SqlConnection conn = new SqlConnection(Common.ConnString);
                conn.Open();

                using(SqlCommand sc = new SqlCommand(checkDuplicateCommand, conn))
                {
                    using(SqlDataReader sdr = sc.ExecuteReader())
                    {
                        sdr.Read();
                        IDataRecord idr = (IDataRecord)sdr;
                    int n = Convert.ToInt32(idr[0]);
                    if (Convert.ToInt32(idr[0]) > 0) { throw new ArgumentException("duplicate name"); }
                    }
                }

                DataTable DT = new DataTable();
                int index;
                using (SqlCommand sc = new SqlCommand(getIndexCommand, conn))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(sc))
                    {
                        sda.Fill(DT);
                    }
                }
                index = Convert.ToInt32(DT.Rows[0][0]) + 1;

                using (SqlCommand sc = new SqlCommand(string.Format(addCommand, index.ToString()), conn))
                {
                    sc.ExecuteNonQuery();
                }
                conn.Close();
            }
           
        }
}

