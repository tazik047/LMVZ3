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
        private List<Student> students;

        public Report()
        {
            InitializeComponent();
            checkedListBox1.Items.AddRange(StaticData.faculties.ToArray());
            studentBindingSource.DataSource = StaticData.students;
            label4_Click(this, EventArgs.Empty);
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

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            var groups = StaticData.faculties.Where(f => f.Title.Equals(checkedListBox1.Items[e.Index].ToString()))
                    .First().Groups.ToArray();
            if (e.NewValue == CheckState.Checked)
            {
                checkedListBox2.Items.AddRange(groups);
                for (int i = 0; i < checkedListBox2.Items.Count; i++)
                {
                    if (groups.Count(g => g.Title.Equals(checkedListBox2.Items[i].ToString())) != 0)
                        checkedListBox2.SetItemChecked(i, true);
                }
                checkedListBox2.Sorted = false;
                checkedListBox2.Sorted = true;
            }
            else
            {
                for(int i = 0; i<checkedListBox2.Items.Count; i++)
                {
                    if(groups.Count(g => g.Title.Equals(checkedListBox2.Items[i].ToString())) != 0)
                    {
                        checkedListBox2.SetItemChecked(i, false);
                        checkedListBox2.Items.RemoveAt(i);
                        i--;
                    }
                }
            }
        }

        private void checkedListBox2_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            var groups = new List<Group>();
            for (int i = 0; i < checkedListBox2.Items.Count; i++)
            {
                if (checkedListBox2.GetItemChecked(i))
                    groups.Add(new Group() { Title = checkedListBox2.Items[i].ToString() });
            }
            if (e.NewValue == CheckState.Checked)
                groups.Add(new Group() { Title = checkedListBox2.Items[e.Index].ToString() });
            else
                groups = groups.Where(g => g.Title.Equals(checkedListBox2.Items[e.Index].ToString())).ToList();
            students = IOClass.findByGroup(StaticData.students, groups);
            studentBindingSource.DataSource = students;
            otherCriter();
        }

        private void otherCriter()
        {
            if (radioButton1.Checked)
                studentBindingSource.DataSource = students;
            else if (radioButton3.Checked)
                studentBindingSource.DataSource = students.Where(s => s.FormOfStudy.ToLower() == "бюджет");
            else
                studentBindingSource.DataSource = students.Where(s => s.FormOfStudy.ToLower() == "контракт");
        }
    }
}
