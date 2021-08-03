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
    public partial class ConfirmationWindow : Form
    {
        private readonly DAL.table table_type;
        readonly String delete_index1;
        readonly sql_interactor interactor;
        readonly interaction_type interaction_Type;

        public ConfirmationWindow(string item_name, DAL.table table, String item_index, interaction_type type)
        {
            InitializeComponent();
            // 根据函数给定的参数来判断初始化哪一种Interactor
            if (type == interaction_type.delete) interactor = new DeleteInteractor();
            else if(type == interaction_type.change)
            {
                textBox1.Visible = true;
                interactor = new ChangeInteractor();
            }
            else if (type == interaction_type.add)
            {
                textBox1.Visible = true;
                interactor = new AddInteractor();
            }
            label1.Text = interactor.getConfirmationMessage(table, item_name);
            table_type = table;
            delete_index1 = item_index;
            interaction_Type = type;
        }

        private void delete_confirmation_Load(object sender, EventArgs e)
        {


        }

        private void button1_Click(object sender, EventArgs e)
        {   
            if (textBox1.Visible && textBox1.Text == string.Empty)
            {
                MessageBox.Show("请输入更改后的内容");
                return;
            }
            if (table_type == DAL.table.T3)
            {   
                try {
                    interactor.interactT3(delete_index1, textBox1.Text);
                }
                
                catch(ArgumentException)
                {    
                        MessageBox.Show("请勿输入重复名字");
                        return;
                }
            }
            else if (table_type == DAL.table.T2)
            {   
                try
                {
                    interactor.interactT2(delete_index1, textBox1.Text);
                }


                catch(ArgumentException)
                {
                    MessageBox.Show("请勿输入重复名字");
                    return;
                }
            }
            else if (table_type == DAL.table.T1)
            {
                try
                {
                    interactor.interactT1(delete_index1, textBox1.Text);
                }

                catch (ArgumentException)
                {
                    MessageBox.Show("请勿输入重复名字");
                    return;
                }
            }
            MessageBox.Show(interactor.getFinishMessage());
            this.Close();
        }
    }
}
