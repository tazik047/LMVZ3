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
    public partial class newStud : Form
    {
        public newStud()
        {
            InitializeComponent();
            object sender = new object();
            EventArgs e = new EventArgs();
            faculties(sender, e);
        }
        public void groups(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            comboBox2.Items.AddRange(((Faculty)comboBox1.SelectedItem).Groups.ToArray());
        }
        public void faculties(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(StaticData.faculties.ToArray());
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            groups(sender, e);
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Save_Click(object sender, EventArgs e)
        {
            if (Check())
            {
                Student studen = new Student();
                studen.FIO = textBox1.Text;
                studen.ID = maskedTextBox1.Text;
                studen.Pass = maskedTextBox2.Text;
                studen.Number = Int32.Parse(maskedTextBox3.Text);
                studen.Birth = dateTimePicker1.Value;
                studen.Faculty = (Faculty)comboBox1.SelectedItem;
                studen.Group = (Group)comboBox2.SelectedItem;
                studen.FormOfStudy = comboBox3.SelectedItem.ToString();
                StaticData.students.Add(studen);
                IOClass.Save(StaticData.students);
            }
            
        }
        public bool Check()
        {
            bool check = true;
            if (textBox1.Text == null || textBox1.Text == "")
            {
                textBox1.BackColor = Color.Red;
                check = false;
            }
            else textBox1.BackColor = Color.White;
            List<MaskedTextBox> masked = new List<MaskedTextBox>();
            masked.Add(maskedTextBox1);
            masked.Add(maskedTextBox2);
            masked.Add(maskedTextBox3);
            foreach (MaskedTextBox maske in masked)
            {
                if (maske.MaskFull == false)
                {
                    maske.BackColor = Color.Red;
                    check = false;
                }
                else maske.BackColor = Color.White;
            }
            List<ComboBox> combos = new List<ComboBox>();
            combos.Add(comboBox1);
            combos.Add(comboBox2);
            combos.Add(comboBox3);
            foreach (ComboBox maske in combos)
            {
                if (maske.SelectedText == null || maske.SelectedText == "")
                {
                    maske.BackColor = Color.Red;
                    check = false;
                }
                else maske.BackColor = Color.White;
            }
            return check;
        }
    }
}
