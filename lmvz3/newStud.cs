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
        public event EventHandler RefreshData;
        List<Control> textboxes = new List<Control>();
        public newStud()
        {
            InitializeComponent();
            object sender = new object();
            EventArgs e = new EventArgs();
            faculties(sender, e);
            textboxes.Add(textBox1);
            textboxes.Add(maskedTextBox1);
            textboxes.Add(maskedTextBox2);
            textboxes.Add(maskedTextBox3);
            textboxes.Add(dateTimePicker1);
            textboxes.Add(comboBox1);
            textboxes.Add(comboBox2);
            textboxes.Add(comboBox3);
            textboxes.Add(textBox2);

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
            if (!(comboBox1.SelectedItem == null))
                groups(sender, e);
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Save_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены?", "Сохранение клиента", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (Check())
                {
                    Student studen = new Student();
                    studen.FIO = textBox1.Text;
                    studen.ID = maskedTextBox1.Text;
                    studen.Pass = maskedTextBox2.Text;
                    studen.Number = maskedTextBox3.Text;
                    studen.Birth = dateTimePicker1.Value;
                    studen.Faculty = (Faculty)comboBox1.SelectedItem;
                    studen.Group = (Group)comboBox2.SelectedItem;
                    studen.FormOfStudy = comboBox3.SelectedItem.ToString();
                    studen.Home = textBox2.Text;
                    StaticData.students.Add(studen);
                    IOClass.Save(StaticData.students);
                    if (RefreshData != null)
                        RefreshData(studen, EventArgs.Empty);
                }
                if (MessageBox.Show("Желаете продолжить добавление студентов?", "Новый студент", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    
                    foreach (Control textbox in textboxes)
                    {
                        textbox.Text = "";
                        if (textbox == comboBox1)
                        {
                            comboBox1.SelectedItem = null;
                            faculties(sender, e);
                        }
                        if (textbox == comboBox2)
                        {
                            comboBox2.SelectedItem = null;
                            comboBox2.Items.Clear();
                        }
                        if (textbox == comboBox3)
                        {
                            comboBox3.SelectedItem = null;
                        }
                    }
                }
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
            if (textBox2.Text == null || textBox2.Text == "")
            {
                textBox2.BackColor = Color.Red;
                check = false;
            }
            else textBox2.BackColor = Color.White;
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
                if (maske.SelectedItem == null && maske.Text == "")
                {
                    maske.BackColor = Color.Red;
                    check = false;
                }
                else maske.BackColor = Color.White;
            }
            return check;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.F1)
            {
                HelpNavigator navigator = HelpNavigator.AssociateIndex;
                Help.ShowHelp(this, IOClass.PathHelp, navigator, "new");
            }
        }
    }
}
