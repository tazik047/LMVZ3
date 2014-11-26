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
        List<Control> textboxes = new List<Control>();
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
            foreach (Control controls in textboxes)
                controls.Enabled = false;
            comboBox1.Items.AddRange(StaticData.faculties.ToArray());
            textBox1.BringToFront();
        }

        public void EditStud(Student stud) 
        {
            textBox1.Text = stud.FIO;
            maskedTextBox1.Text = stud.ID;
            maskedTextBox2.Text = stud.Pass;
            maskedTextBox3.Text = stud.Number.ToString();
            dateTimePicker1.Value = stud.Birth;
            comboBox1.SelectedItem = stud.Faculty;
            comboBox2.SelectedItem = stud.Group;
            comboBox3.SelectedItem = stud.FormOfStudy;
        }
        public Edit()
        {
            InitializeComponent();
            Basic();
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
            
            comboBox2.Items.AddRange(((Faculty)comboBox1.SelectedItem).Groups.ToArray());
        }


  }
}
