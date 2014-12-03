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
            mainTable.Location = new Point(groups.Width , 0);
            main.ClientSize = new Size(edit.ClientSize.Width, this.ClientSize.Height - 28);
            edit.Location = new Point(groups.Width, 0);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            StaticData.faculties = IOClass.LoadFac();
            StaticData.students = IOClass.LoadStudent();

            mainTable = new Main(dataGridView1_SelectionChanged);

            groups = new GroupTree(mainTable.filter) {MdiParent = this};
            groups.Show();
            mainTable.MdiParent = this;
            mainTable.Show();
            
            edit = new Edit();
            edit.MdiParent = this;
            edit.Show();
            edit.SendToBack();

            edit.RefreshData += mainTable.RefreshData;
            edit.DelStud += mainTable.RefreshAfterDel;
            groups.UpdateFaculties += edit.faculties;
            groups.RefreshData += mainTable.RefreshData;
            
            
            mainTable.dataGridView1.ClearSelection();
            edit.SelectGroup += groups.SelectItem;
            edit.WantClose += (o, args) =>
            {
                var ind = mainTable.SelectItem;
                dataGridView1_SelectionChanged(null, e);
                mainTable.SelectItem = ind;
            };
            //edit.Hide2();
            edit.studen = null;
            main = mainTable;
            Form1_Resize(this, EventArgs.Empty);
            //groups.Width = 335;
            /*edit.Width = 543;*/
            var s = new Size(Convert.ToInt32(633*0.618), 0);
            groups.MaximumSize = s;
            groups.Width = s.Width;
            mainTable.Width = 633;
            Width = groups.Width + 653;
            
            Form1_Resize(this, EventArgs.Empty);
        }

        public void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (sender != null)
            {
                //main.SendToBack();
                var d = sender as DataGridView;
                var source = d.DataSource as BindingSource;
                var s = source.Current as Student;
                if (edit == null || s == null) return;
                edit.EditStud(s, mainTable.currentNode);
                edit.Show2();
                edit.ScrollToDefault();
                main = edit;
            }
            else
            {
                mainTable.dataGridView1.ClearSelection();
                edit.studen = null;
                if (main.Text == "main")
                    return;
                //main.SendToBack();
                main = mainTable;
            }
            main.BringToFront();
            
        }

        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var create = new newStud();
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
            Help.ShowHelp(this, IOClass.PathHelp, HelpNavigator.Index);
        }

        private void поискToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, IOClass.PathHelp, HelpNavigator.Find,"");
        }

        private void содержаниеToolStripMenuItem_Click_2(object sender, EventArgs e)
        {
            Help.ShowHelp(this, IOClass.PathHelp, HelpNavigator.Topic, "index.html");
        }

        private void Form1_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            Help.ShowHelp(this, IOClass.PathHelp, HelpNavigator.Topic, "main.html");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (main.Text == edit.Text)
                dataGridView1_SelectionChanged(null, e);
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Все данные успешно сохранены", "Сохранение", MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

    }
}
