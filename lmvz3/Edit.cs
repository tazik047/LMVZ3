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
        List<Control> controls = new List<Control>();
        List<PictureBox> hints = new List<PictureBox>();
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
                pictureBox10.Image = ((System.Drawing.Image)(Properties.Resources.report));
                foreach (Control controls in textboxes)
                    controls.Enabled = false;
                HideHint();
            }
            else
            {
                label1.Text = "Режим редактирования";
                pictureBox10.Image = ((System.Drawing.Image)(Properties.Resources.edit));
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
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                textBox1.BackColor = Color.Red;
                pictureBox1.Image = ((System.Drawing.Image)(Properties.Resources.warn));
                check = false;
            }
            else 
            { 
                textBox1.BackColor = Color.White;
                pictureBox1.Image = ((System.Drawing.Image)(Properties.Resources.help));
            }
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                textBox2.BackColor = Color.Red;
                pictureBox2.Image = ((System.Drawing.Image)(Properties.Resources.warn));
                check = false;
            }
            else 
            {
                textBox2.BackColor = Color.White;
                pictureBox2.Image = ((System.Drawing.Image)(Properties.Resources.help));
            }
            List<MaskedTextBox> masked = new List<MaskedTextBox>();
            List<PictureBox> mashints = new List<PictureBox>();
            masked.Add(maskedTextBox1);
            masked.Add(maskedTextBox2);
            masked.Add(maskedTextBox3);
            mashints.Add(pictureBox3);
            mashints.Add(pictureBox4);
            mashints.Add(pictureBox9);
            for (int i = 0; i < masked.Count; i++ )
            {
                if (masked[i].MaskFull == false)
                {
                    masked[i].BackColor = Color.Red;
                    mashints[i].Image = ((System.Drawing.Image)(Properties.Resources.warn));
                    check = false;
                }
                else
                {
                    masked[i].BackColor = Color.White;
                    mashints[i].Image = ((System.Drawing.Image)(Properties.Resources.help));
                }
            }
            List<ComboBox> combos = new List<ComboBox>();
            List<PictureBox> combhints = new List<PictureBox>();
            combos.Add(comboBox1);
            combos.Add(comboBox2);
            combos.Add(comboBox3);
            combhints.Add(pictureBox6);
            combhints.Add(pictureBox7);
            combhints.Add(pictureBox8);
            for (int i = 0; i < combos.Count; i++)  
            {
                if (combos[i].SelectedItem == null && combos[i].Text == "")
                {
                    combos[i].BackColor = Color.Red;
                    combhints[i].Image = ((System.Drawing.Image)(Properties.Resources.warn));
                    check = false;
                }
                else
                {
                    combos[i].BackColor = Color.White;
                    combhints[i].Image = ((System.Drawing.Image)(Properties.Resources.help));
                }
            }
            return check;
        }
        public void Add3() 
        {
           
            this.controls.Add(this.pictureBox1);
            this.controls.Add(this.pictureBox2);
            this.controls.Add(this.pictureBox3);
            this.controls.Add(this.pictureBox4);
            this.controls.Add(this.pictureBox5);
            this.controls.Add(this.pictureBox6);
            this.controls.Add(this.pictureBox7);
            this.controls.Add(this.pictureBox8);
            this.controls.Add(this.pictureBox9);
            this.controls.Add(this.pictureBox10);

            this.controls.Add(this.textBox2);
            this.controls.Add(this.label13);
            this.controls.Add(this.cancel);
            this.controls.Add(this.Save);
            this.controls.Add(this.delete);
            this.controls.Add(this.maskedTextBox3);
            this.controls.Add(this.maskedTextBox2);
            this.controls.Add(this.maskedTextBox1);
            this.controls.Add(this.trackBar1);
            this.controls.Add(this.comboBox3);
            this.controls.Add(this.comboBox2);
            this.controls.Add(this.comboBox1);
            this.controls.Add(this.dateTimePicker1);
            this.controls.Add(this.textBox1);
            this.controls.Add(this.label1);
            this.controls.Add(this.number);
            this.controls.Add(this.formStudy);
            this.controls.Add(this.group);
            this.controls.Add(this.faculty);
            this.controls.Add(this.birth);
            this.controls.Add(this.pass);
            this.controls.Add(this.id);
            this.controls.Add(this.fio);

            this.hints.Add(this.pictureBox1);
            this.hints.Add(this.pictureBox2);
            this.hints.Add(this.pictureBox3);
            this.hints.Add(this.pictureBox4);
            this.hints.Add(this.pictureBox5);
            this.hints.Add(this.pictureBox6);
            this.hints.Add(this.pictureBox7);
            this.hints.Add(this.pictureBox8);
            this.hints.Add(this.pictureBox9);

        }
        public void Hide2()
        {
            foreach (Control control in this.controls)
                control.Hide();
            label15.Show();
        }
        public void HideHint()
        {
            foreach (PictureBox control in this.hints)
            {
                control.Hide();
                control.Image = ((System.Drawing.Image)(Properties.Resources.help));
            }
            Save.Hide();
            cancel.Hide();
            delete.Hide();
            for (int i = 9; i < controls.Count; i++)
                controls[i].BackColor = Color.White;
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
            foreach (Control control in this.controls)
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
            toolTip1.SetToolTip(this.pictureBox1, "Введите ФИО\nНапример: Потёмкин Константин Юрьевич");
            toolTip1.SetToolTip(this.pictureBox2, "Введите адрес\nНапример: 61254, г.Харьков,\nул. Иванова, 23, кв.231");
            toolTip1.SetToolTip(this.pictureBox3, "Введите идентификационный номер\nНапример: 1234567890");
            toolTip1.SetToolTip(this.pictureBox4, "Введите номер пасспорта\nНапример: АВ123456");
            toolTip1.SetToolTip(this.pictureBox5, "Выберите дату рождения");
            toolTip1.SetToolTip(this.pictureBox6, "Выберите факультет");
            toolTip1.SetToolTip(this.pictureBox7, "Выберите группу");
            toolTip1.SetToolTip(this.pictureBox8, "Выберите форму обучения");
            toolTip1.SetToolTip(this.pictureBox9, "Введите номер телефона\nНапример: +38(066)123 4567");
        
        }

        private void Edit_HelpButtonClicked(object sender, CancelEventArgs e)
        {
           
        }

        private void Edit_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            HelpNavigator navigator = HelpNavigator.Topic;
            Help.ShowHelp(this, IOClass.PathHelp, navigator, "edit.html");
        }
  }
}
