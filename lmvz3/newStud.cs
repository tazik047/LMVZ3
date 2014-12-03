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

            BackColor = System.Drawing.Color.FromArgb(0xFF, 0x86, 0xB9, 0xF6);
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
            if (comboBox1.SelectedItem != null)
                groups(sender, e);
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что желаете выйти?", "Выход", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void Save_Click(object sender, EventArgs e)
        {
             if (Check())
             {
                 if (MessageBox.Show("Вы уверены?", "Сохранение студента", MessageBoxButtons.YesNo) == DialogResult.Yes)
                 {
               
                    Student studen = new Student
                    {
                        FIO = textBox1.Text,
                        ID = maskedTextBox1.Text,
                        Pass = maskedTextBox2.Text,
                        Number = maskedTextBox3.Text,
                        Birth = dateTimePicker1.Value,
                        Faculty = (Faculty) comboBox1.SelectedItem,
                        Group = (Group) comboBox2.SelectedItem,
                        FormOfStudy = comboBox3.SelectedItem.ToString(),
                        Home = textBox2.Text,
                        Start = dateTimePicker2.Value
                    };
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
                else
                {
                    Close();
                }
            }

            
        }

        private bool Check()
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



        private void newStud_Load(object sender, EventArgs e)
        {
            //ToolTip toolTip1 = new ToolTip();
            toolTip1.SetToolTip(this.pictureBox1, "Введите ФИО\nНапример: Иванов Иван Иванович");
            toolTip1.SetToolTip(this.pictureBox2, "Введите адрес\nНапример: 61254, г.Харьков,\nул. Иванова, 23, кв.231");
            toolTip1.SetToolTip(this.pictureBox3, "Введите идентификационный номер\nНапример: 1234567890");
            toolTip1.SetToolTip(this.pictureBox4, "Введите номер пасспорта\nНапример: АВ123456");
            toolTip1.SetToolTip(this.pictureBox5, "Выберите дату рождения");
            toolTip1.SetToolTip(this.pictureBox6, "Выберите факультет");
            toolTip1.SetToolTip(this.pictureBox7, "Выберите группу");
            toolTip1.SetToolTip(this.pictureBox8, "Выберите форму обучения");
            toolTip1.SetToolTip(this.pictureBox9, "Введите номер телефона\nНапример: +38(066)123 4567");
        }

        private void newStud_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            HelpNavigator navigator = HelpNavigator.Topic;
            Help.ShowHelp(this, IOClass.PathHelp, navigator, "add.html");
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void formStudy_Click(object sender, EventArgs e)
        {

        }

        private void fio_Click(object sender, EventArgs e)
        {

        }

    }
}
