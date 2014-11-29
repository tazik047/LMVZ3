using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lmvz3
{
    public partial class CreateForm : Form
    {
        private bool cancel;
        private Func<string, bool> check;
        private string errorMes;

        public CreateForm(String mess, Func<string,bool> check, string er)
        {
            InitializeComponent();
            label1.Text = mess;
            cancel = false;
            this.check = check;
            errorMes = er;
        }

        public string WrittenName
        {
            get
            {
                if (!cancel)
                    return textBox1.Text;
                else
                    return "";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cancel = true;
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text =="")
            {
                MessageBox.Show("Название должно быть не пустым", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!check(textBox1.Text))
            {
                MessageBox.Show(errorMes, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Close();
            }
            
        }

        private void CreateForm_Paint(object sender, PaintEventArgs e)
        {
        }

    }
}
