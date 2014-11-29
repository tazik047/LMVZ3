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
    public partial class Main : Form
    {

        private List<Student> currentStud;

        private TreeNode currentNode;

        public Main(Action<object,EventArgs> select)
        {
            InitializeComponent();
            StaticData.students = IOClass.LoadStudent();
            studentBindingSource.Sort = "FIO";
            studentBindingSource.DataSource = StaticData.students;
            currentStud = StaticData.students;
            dataGridView1.SelectionChanged += new System.EventHandler(select);
            Text = "main";
            
        }


        public void filter(object sender, TreeNodeMouseClickEventArgs e)
        {
            /*
            if (e.Button == MouseButtons.Left)
            {
                var n = e.Node;
                if (n.Parent == null)
                {
                    MessageBox.Show(String.Format("Вы выбрали факультет - {0}", n.Text), "Click");
                }
            }*/
            currentNode = e.Node;
            if (e.Node.Parent == null && e.Node.Text == "Все факультеты")
            {
                studentBindingSource.DataSource = StaticData.students;
                label1.Text = "Все студенты";
            }
            else if (e.Node.Parent == null)
            {
                label1.Text = string.Format("Студенты из факультета {0}", e.Node.Text);
                studentBindingSource.DataSource = IOClass.findByFaculty(StaticData.students,
                    new List<Faculty>() { new Faculty() { Title = e.Node.Text } });
            }
            else
            {
                label1.Text = string.Format("Студенты из группы {0}", e.Node.Text);
                studentBindingSource.DataSource = IOClass.findByGroup(StaticData.students,
                    new List<Group>() {new Group() { Title = e.Node.Text }});
            }
            currentStud = (List<Student>)studentBindingSource.DataSource;
            //studentBindingSource.DataSource = students.Where(s => s.ID  == 0).ToList();
        }

        private void Main_Resize(object sender, EventArgs e)
        {
            dataGridView1.Columns[1].Width = this.ClientSize.Width - dataGridView1.Columns[0].Width -2;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            studentBindingSource.DataSource = currentStud.Where(s => s.FIO.ToLower()
                    .Contains(textBox1.Text.ToLower())).ToList();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
        }

        public void RefreshData(object sender, EventArgs e)
        {

            if(sender==null)
            {
                studentBindingSource.DataSource = StaticData.students;
                label1.Text = "Все студенты";
                studentBindingSource.ResetBindings(true);
                return;
            }
            studentBindingSource.ResetBindings(true);
            if (!studentBindingSource.DataSource.Equals(StaticData.students))
            {
                var s = (Student)sender;
                if (currentNode != null && currentNode.Parent == null && currentNode.Text == s.Faculty.Title)
                    currentStud.Add(s);
                else if (currentNode != null && currentNode.Parent != null && currentNode.Text == s.Group.Title)
                    currentStud.Add(s);
                studentBindingSource.DataSource = currentStud.OrderBy(st => st.FIO).ToList();
            }
            studentBindingSource.ResetBindings(true);
        }

        internal void RefreshAfterDel(object sender, EventArgs e)
        {
            studentBindingSource.DataSource = ((List<Student>)studentBindingSource.DataSource).Where(s => StaticData.students.Contains(s)).ToList();
            currentStud = ((List<Student>)studentBindingSource.DataSource);
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            textBox1.ForeColor = Color.Black;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            if (textBox1.Text == "Поиск...")
                textBox1.Text = "";
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox1.ForeColor = System.Drawing.SystemColors.ActiveBorder;
            if (textBox1.Text == "")
            {
                textBox1.Text = "Поиск...";
                studentBindingSource.DataSource = currentStud;
            }
        }
    }
}
