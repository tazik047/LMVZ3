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
       
        public Edit()
        {
            InitializeComponent();
            textboxes.Add(textBox1);
            textboxes.Add(textBox2);
            textboxes.Add(textBox3);
            textboxes.Add(textBox4);
            textboxes.Add(dateTimePicker1);
            textboxes.Add(comboBox1);
            textboxes.Add(comboBox2);
            textboxes.Add(comboBox3);
            foreach (Control controls in textboxes)
                controls.Enabled = false;
            textBox1.BringToFront();
        }
          

        private void trackBar1_Scroll(object sender, EventArgs e)
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

  }
}
