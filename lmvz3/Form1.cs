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
        private Edit edit;

        private Form main;
        

        public Form1()
        {
            InitializeComponent();
         }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (groups == null || main == null || edit == null) return;
            groups.ClientSize = new Size(groups.ClientSize.Width, this.ClientSize.Height - 28);
            main.Location = new Point(groups.Width , 0);
            main.ClientSize = new Size(edit.ClientSize.Width, this.ClientSize.Height - 28);
            main.Location = new Point(groups.Width, 0);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            mainTable = new Main(dataGridView1_SelectionChanged);

            groups = new GroupTree(mainTable.filter) {MdiParent = this};
            groups.Show();
            mainTable.MdiParent = this;
            mainTable.Show();
            
            edit = new Edit {MdiParent = this};
            //edit.Show();

            edit.RefreshData += mainTable.RefreshData;
            edit.DelStud += mainTable.RefreshAfterDel;
            groups.UpdateFaculties += edit.faculties;
            groups.RefreshData += mainTable.RefreshData;
            
            groups.Width = edit.Width - 35;
            
            mainTable.dataGridView1.ClearSelection();
            //edit.Hide2();
            edit.studen = null;
            main = mainTable;
            Form1_Resize(this, EventArgs.Empty);
            
        }

        public void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            
            if (sender != null)
            {
                
                var d = sender as DataGridView;
                var source = d.DataSource as BindingSource;
                var s = source.Current as Student;
                if (edit == null || s == null) return;
                edit.EditStud(s);
                edit.Show2();
                main = edit;
            }
            else
            {
                mainTable.dataGridView1.ClearSelection();
                if (main.Text == "main")
                    return;
                main.Hide();
                main = mainTable;
            }
            main.Show();
            Form1_Resize(sender, e);
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
