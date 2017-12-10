using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HashKey
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        frmHashKey form = new frmHashKey();
        //也就是你FORM1类型的.

        //在定义个公共方法.
        public void GetForm1(frmHashKey form1)
        {
            this.form = form1;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if(this.textBox1.Text.Trim() == "888888")
            {
                form.tabPage5.Parent = form.tabControl2;
                //MessageBox.Show("密码正确！");
                this.Close();
            }
            else
            {
                MessageBox.Show("密码错误！");
            }
        }
    }
}
