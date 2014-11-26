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
    public partial class Edit : Form
    {
        public event EventHandler Refresh;
        List<Control> textboxes = new List<Control>();
        List<Control> constrols = new List<Control>();
        Student studen = new Student();
        public void Basic()
        {
            textboxes.Add(textBox1);
            textboxes.Add(maskedTextBox1);
            textboxes.Add(maskedTextBox2);
            textboxes.Add(maskedTextBox3);
            textboxes.Add(dateTimePicker1);
            textboxes.Add(comboBox1);
            textboxes.Add(comboBox2);
            textboxes.Add(comboBox3);
            textboxes.Add(textBox2);

            foreach (Control controls in textboxes)
                controls.Enabled = false;
            textBox1.BringToFront();
            object sender = new object();
            EventArgs e = new EventArgs();
            faculties(sender, e);
            
        }

        public void EditStud(Student stud) 
        {
            this.studen = stud;
            textBox1.Text = stud.FIO;
            maskedTextBox1.Text = stud.ID;
            maskedTextBox2.Text = stud.Pass;
            maskedTextBox3.Text = stud.Number.ToString();
            dateTimePicker1.Value = stud.Birth;
            comboBox1.SelectedItem = stud.Faculty;
            comboBox2.SelectedItem = stud.Group;
            comboBox3.SelectedItem = stud.FormOfStudy;
            textBox2.Text = stud.Home;
        }
        public Edit()
        {
            InitializeComponent();
            Basic();
            Add3();
            Hide2();
        }
        
         
        private void colorSlider1_Scroll(object sender, ScrollEventArgs e)
        {
            if (trackBar1.Value == 0)
            {
                label1.Text = "Режим просмотра";
                foreach (Control controls in textboxes)
                    controls.Enabled = false;
            }
            else
            {
                label1.Text = "Режим редактирования";
                foreach (Control controls in textboxes)
                    controls.Enabled = true;
            }
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            groups(sender, e);
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

        private void Save_Click(object sender, EventArgs e)
        {
            if (Check())
            {
                this.studen.FIO = textBox1.Text;
                this.studen.ID = maskedTextBox1.Text;
                this.studen.Pass = maskedTextBox2.Text;
                this.studen.Number = maskedTextBox3.Text;
                this.studen.Birth = dateTimePicker1.Value;
                this.studen.Faculty = (Faculty)comboBox1.SelectedItem;
                this.studen.Group = (Group)comboBox2.SelectedItem;
                this.studen.FormOfStudy = comboBox3.SelectedItem.ToString();
                this.studen.Home = textBox2.Text;
                IOClass.Save(StaticData.students);
                if (Refresh != null)
                    Refresh(this.studen, EventArgs.Empty);
            }
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            EditStud(this.studen);
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
        public void Add3() 
        {
           
            this.constrols.Add(this.label14);
            this.constrols.Add(this.textBox2);
            this.constrols.Add(this.label13);
            this.constrols.Add(this.cancel);
            this.constrols.Add(this.Save);
            this.constrols.Add(this.label12);
            this.constrols.Add(this.label11);
            this.constrols.Add(this.label10);
            this.constrols.Add(this.label9);
            this.constrols.Add(this.label8);
            this.constrols.Add(this.label7);
            this.constrols.Add(this.label6);
            this.constrols.Add(this.label5);
            this.constrols.Add(this.label4);
            this.constrols.Add(this.label3);
            this.constrols.Add(this.maskedTextBox3);
            this.constrols.Add(this.label2);
            this.constrols.Add(this.maskedTextBox2);
            this.constrols.Add(this.maskedTextBox1);
            this.constrols.Add(this.trackBar1);
            this.constrols.Add(this.comboBox3);
            this.constrols.Add(this.comboBox2);
            this.constrols.Add(this.comboBox1);
            this.constrols.Add(this.dateTimePicker1);
            this.constrols.Add(this.textBox1);
            this.constrols.Add(this.label1);
            this.constrols.Add(this.number);
            this.constrols.Add(this.formStudy);
            this.constrols.Add(this.group);
            this.constrols.Add(this.faculty);
            this.constrols.Add(this.birth);
            this.constrols.Add(this.pass);
            this.constrols.Add(this.id);
            this.constrols.Add(this.fio);
        }
        public void Hide2()
        {
            foreach (Control control in this.constrols)
                control.Hide();
            label15.Show();
        }
        public void Show2()
        {
            foreach (Control control in this.constrols)
                control.Show();
            label15.Hide();
        }

        private void delete_Click(object sender, EventArgs e)
        {
            StaticData.students.Remove(this.studen);
            IOClass.Save(StaticData.students);
            this.Refresh(sender, e);

        }
  }
}
