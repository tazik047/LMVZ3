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
    public partial class Form1 : Form
    {
        GroupTree groups;
        Main mainTable;
        Edit edit;
        public Form1()
        {
            InitializeComponent();
            
            
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (groups != null && edit != null && mainTable != null)
            {
                groups.ClientSize = new Size(groups.ClientSize.Width, this.ClientSize.Height - 28);
                edit.ClientSize = new Size(edit.ClientSize.Width, this.ClientSize.Height - 28);
                edit.Location = new Point(this.ClientSize.Width - edit.Width - 4, 0);
                mainTable.Location = new Point(groups.Width + 1, 0);
                mainTable.ClientSize = new Size(edit.Location.X - mainTable.Location.X - 1, this.ClientSize.Height - 28);
                mainTable.Location = new Point(groups.Width + 1, 0);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            mainTable = new Main(dataGridView1_SelectionChanged);

            groups = new GroupTree(mainTable.filter);
            groups.MdiParent = this;
            groups.Show();

            mainTable.MdiParent = this;
            mainTable.Show();

            edit = new Edit();
            edit.MdiParent = this;
            edit.Show();

            edit.RefreshData += mainTable.RefreshData;
            groups.UpdateFaculties += edit.faculties;
            Form1_Resize(this, EventArgs.Empty);
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            var d = sender as DataGridView;
            var source = d.DataSource as BindingSource;
            var s = source.Current as Student;
            if (edit != null && s != null)
            {
                edit.EditStud(s);
                edit.Show2();
            }
        }

        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newStud create = new newStud();
            create.RefreshData += mainTable.RefreshData;
            create.ShowDialog();
        }

        private void печатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var r = new Report();
            r.ShowDialog();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
