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
        public event EventHandler RefreshData;
        public event EventHandler DelStud;

        List<Control> textboxes = new List<Control>();
        List<Control> constrols = new List<Control>();
        List<Control> hints = new List<Control>();
        public Student studen = new Student();
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
            Text = "edit";
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
                HideHint();
            }
            else
            {
                label1.Text = "Режим редактирования";
                foreach (Control controls in textboxes)
                    controls.Enabled = true;
                ShowHint();
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
                if (MessageBox.Show("Вы уверены?", "Сохранение клиента", MessageBoxButtons.YesNo) == DialogResult.Yes)
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
                    if (RefreshData != null)
                        RefreshData(this.studen, EventArgs.Empty);
                }
            }
        }

        public void cancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что желаете отменить внесенные изменения?", "Отмена", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                EditStud(this.studen);
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
        public void Add3() 
        {
           
            this.constrols.Add(this.label14);
            this.constrols.Add(this.textBox2);
            this.constrols.Add(this.label13);
            this.constrols.Add(this.cancel);
            this.constrols.Add(this.Save);
            this.constrols.Add(this.delete);
            this.constrols.Add(this.label12);

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

            this.hints.Add(this.label8);
            this.hints.Add(this.label7);
            this.hints.Add(this.label6);
            this.hints.Add(this.label5);
            this.hints.Add(this.label4);
            this.hints.Add(this.label3);
            this.hints.Add(this.label2);
            this.hints.Add(this.label14);
            this.hints.Add(this.label12);

        }
        public void Hide2()
        {
            foreach (Control control in this.constrols)
                control.Hide();
            label15.Show();
        }
        public void HideHint()
        {
            foreach (Control control in this.hints)
                control.Hide();
                Save.Hide();
                cancel.Hide();
                delete.Hide();
            
        }
        public void ShowHint()
        {
            foreach (Control control in this.hints)
                control.Show();
                            Save.Show();
                cancel.Show();
                delete.Show();
            
        }
        public void Show2()
        {
            foreach (Control control in this.constrols)
                control.Show();
            label15.Hide();
            if (trackBar1.Value == 0)
                HideHint();
        }

        public void delete_Click(object sender, EventArgs e)
        {
            if (this.studen == null)
                MessageBox.Show("Нет студентов для редактировани");
            else
            {
                if (MessageBox.Show("Вы уверены, что хотите удалить студента?", "Удаление студента", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    StaticData.students.Remove(this.studen);
                    IOClass.Save(StaticData.students);
                    this.DelStud(null, e);
                    this.studen = null;
                }
            }
        }

        private void Edit_Load(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.SetToolTip(this.label12, "Введите ФИО\nНапример: Потёмкин Константин Юрьевич");
            toolTip1.SetToolTip(this.label14, "Введите адрес\nНапример: 61254, г.Харьков,\nул. Иванова, 23, кв.231");
            toolTip1.SetToolTip(this.label2, "Введите идентификационный номер\nНапример: 1234567890");
            toolTip1.SetToolTip(this.label3, "Введите номер пасспорта\nНапример: АВ123456");
            toolTip1.SetToolTip(this.label4, "Выберите дату рождения");
            toolTip1.SetToolTip(this.label5, "Выберите факультет");
            toolTip1.SetToolTip(this.label6, "Выберите группу");
            toolTip1.SetToolTip(this.label7, "Выберите форму обучения");
            toolTip1.SetToolTip(this.label8, "Введите номер телефона\nНапример: +38(066)123 4567");
        }
  }
}
