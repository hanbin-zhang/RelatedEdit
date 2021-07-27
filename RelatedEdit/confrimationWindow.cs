using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RelatedEdit
{   
    public partial class ConfrimationWindow : Form
    {
        private readonly DAL.table table_type;
        readonly String delete_index1;
        readonly sql_interactor interactor;

        public ConfrimationWindow(string item_name, DAL.table table, String item_index, interaction_type type)
        {
            InitializeComponent();
            // 根据函数给定的参数来判断初始化哪一种Interactor
            if (type == interaction_type.delete) interactor = new DeleteInteractor();
            label1.Text = string.Format(interactor.getConfirmationMessage(), table.ToString(), item_name);
            table_type = table;
            delete_index1 = item_index;
        }

        private void delete_confirmation_Load(object sender, EventArgs e)
        {


        }

        private void button1_Click(object sender, EventArgs e)
        { 
            if (table_type == DAL.table.T3)
            {
                interactor.interactT3(delete_index1);
            }
            else if (table_type == DAL.table.T2)
            {
                interactor.interactT2(delete_index1);
            }
            else if (table_type == DAL.table.T1)
            {
                interactor.interactT1(delete_index1);
            }
            MessageBox.Show(interactor.getFinishMessage());
            this.Close();
        }
    }
}
