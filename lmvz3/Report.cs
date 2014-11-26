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
    public partial class Report : Form
    {
        public Report()
        {
            InitializeComponent();
            checkedListBox1.Items.AddRange(StaticData.faculties.ToArray());
        }

        private void label4_Click(object sender, EventArgs e)
        {
            checkOrNotCheck(checkedListBox1, true);
        }

        private void checkOrNotCheck(CheckedListBox check, bool value)
        {
            for (int i = 0; i < check.Items.Count; i++)
            {
                check.SetItemChecked(i, value);
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {
            checkOrNotCheck(checkedListBox1, false);
        }

        private void label9_Click(object sender, EventArgs e)
        {
            checkOrNotCheck(checkedListBox2, true);
        }

        private void label7_Click(object sender, EventArgs e)
        {
            checkOrNotCheck(checkedListBox2, false);
        }
    }
}
