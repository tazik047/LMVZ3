﻿using System;
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
            if (groups == null || edit == null || mainTable == null) return;
            groups.ClientSize = new Size(200, this.ClientSize.Height - 28);
            edit.ClientSize = new Size(400, this.ClientSize.Height - 28);
            edit.Location = new Point(this.ClientSize.Width - edit.Width - 4, 0);
            mainTable.Location = new Point(groups.Width , 0);
            mainTable.ClientSize = new Size(455, this.ClientSize.Height - 28);
            mainTable.Location = new Point(groups.Width, 0);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            mainTable = new Main(dataGridView1_Click);

            groups = new GroupTree(mainTable.filter) {MdiParent = this};
            groups.Show();

            mainTable.MdiParent = this;
            mainTable.Show();

            edit = new Edit {MdiParent = this};
            edit.Show();

            edit.RefreshData += mainTable.RefreshData;
            edit.DelStud += mainTable.RefreshAfterDel;
            groups.UpdateFaculties += edit.faculties;
            groups.RefreshData += mainTable.RefreshData;
            mainTable.dataGridView1.ClearSelection();
            groups.Width = edit.Width - 35;
            edit.Hide2();
            edit.studen = null;
            Form1_Resize(this, EventArgs.Empty);


            отменадействияToolStripMenuItem.Enabled = false;
            удалитьToolStripMenuItem.Enabled = false;
        }

        public void dataGridView1_Click(object sender, EventArgs e)
        {
            var d = sender as DataGridView;
            var source = d.DataSource as BindingSource;
            var s = source.Current as Student;
            if (edit == null || s == null) return;
            else
            {
                edit.EditStud(s);
                edit.Show2();
                отменадействияToolStripMenuItem.Enabled = true;
                удалитьToolStripMenuItem.Enabled = true;
            }
        }

        /*
        public void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            var d = sender as DataGridView;
            var source = d.DataSource as BindingSource;
            var s = source.Current as Student;
            if (edit == null || s == null) return;
            else
            {
                edit.EditStud(s);
                edit.Show2();
                отменадействияToolStripMenuItem.Enabled = true;
                удалитьToolStripMenuItem.Enabled = true;
            }
        }
        */

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

        public void ShowHelp()
        {
            HelpNavigator navigator = HelpNavigator.AssociateIndex;
            Help.ShowHelp(this, IOClass.PathHelp, navigator, ActiveMdiChild.Text);
          
        }

        private void отменадействияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (edit.studen != null)
                edit.cancel_Click(sender, e);
            else
                MessageBox.Show("Нет выбранных студентов для редактировани", "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            edit.delete_Click(sender, e);
        }

        private void индексToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HelpNavigator navigator1 = HelpNavigator.Index;
            Help.ShowHelp(this, IOClass.PathHelp, navigator1);
        }

        private void поискToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HelpNavigator navigator = HelpNavigator.Find;
            Help.ShowHelp(this, IOClass.PathHelp, navigator,"");
        }

        private void содержаниеToolStripMenuItem_Click_2(object sender, EventArgs e)
        {
            HelpNavigator navigator = HelpNavigator.Topic;
            Help.ShowHelp(this, IOClass.PathHelp, navigator, "index.html");
        }

        private void Form1_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            HelpNavigator navigator = HelpNavigator.Topic;
            Help.ShowHelp(this, IOClass.PathHelp, navigator, "main.html");
        }

    }
}
