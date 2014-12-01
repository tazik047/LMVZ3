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
    public partial class GroupTree : Form
    {
        public event EventHandler RefreshData;

        TreeNode selectedNode;

        public event EventHandler UpdateFaculties;

        private Action<Object, TreeNodeMouseClickEventArgs> selectItem;

        public GroupTree(Action<Object, TreeNodeMouseClickEventArgs> filter)
        {
            InitializeComponent();
            //faculties = IOClass.LoadFaculty();
            StaticData.faculties = IOClass.LoadFac();
            CreateNodes();
            treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(filter);
            selectItem = filter;
            Text = "treeGroup";
        }

        private void CreateNodes()
        {
            var t = new TreeNode("Все факультеты");
            t.ContextMenu = null;
            treeView1.Nodes.Add(t);
            foreach (Faculty t1 in StaticData.faculties)
            {
                var ns = new TreeNode[t1.Groups.Count];
                for (int j = 0; j < t1.Groups.Count; j++)
                {
                    ns[j] = new TreeNode(t1.Groups[j].Title);
                    ns[j].ContextMenuStrip = contextMenuStripGroup;
                }
                var n = new TreeNode(t1.Title, ns);
                n.ContextMenuStrip = contextMenuStripFac;
                treeView1.Nodes.Add(n);
            }
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                selectedNode = e.Node;
            }
        }

        private bool checkFac(string title)
        {
            return StaticData.faculties.Count(f => f.Title.Equals(title)) == 0;
        }

        private bool checkGroup(string title)
        {
            return StaticData.faculties.All(t => t.Groups.Count(g => g.Title.Equals(title)) == 0);
        }

        private void добавитьФакультетToolStripMenuItem1_MouseUp(object sender, MouseEventArgs e)
        {
            if(e.Button== MouseButtons.Left)
            {
                CreateForm c = new CreateForm("Введите название нового факультета:", checkFac, "Такой факультет уже присутствует");
                c.Text = "Добавление факультета";
                c.ShowDialog();
                if (c.WrittenName != "")
                {
                    var f = new Faculty(0, c.WrittenName, new List<Group>());
                    StaticData.faculties.Add(f);
                    var n = new TreeNode(f.Title);
                    n.ContextMenuStrip = contextMenuStripFac;
                    treeView1.Nodes.Add(n);
                    treeView1.Sort();
                }
                IOClass.Save(StaticData.faculties);
                if (UpdateFaculties != null)
                    UpdateFaculties(this, EventArgs.Empty);
            }
        }

        private void добавитьГруппуToolStripMenuItem_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                CreateForm c = new CreateForm("Введите название новой группы:", checkGroup, "Такая группа уже присутствует");
                c.Text = "Добавление группы";
                c.ShowDialog();
                if (c.WrittenName != "")
                {
                    var g = new Group(c.WrittenName, 0);
                    //faculties.Add(f);
                    var n = new TreeNode(g.Title);
                    n.ContextMenuStrip = contextMenuStripGroup;
                    string title;
                    if (selectedNode.Parent == null)
                    {
                        selectedNode.Nodes.Add(n);
                        title = selectedNode.Text;
                    }
                    else
                    {
                        selectedNode.Parent.Nodes.Add(n);
                        title = selectedNode.Parent.Text;
                    }
                    StaticData.faculties.First(f => f.Title.Equals(title)).Groups.Add(g);
                    treeView1.Sort();
                }
                IOClass.Save(StaticData.faculties);
                if (UpdateFaculties != null)
                    UpdateFaculties(this, EventArgs.Empty);
            }
        }

        private void удалитьГруппуToolStripMenuItem_MouseUp(object sender, MouseEventArgs e)
        {
            if (MessageBox.Show(string.Format("Вы собираетесь удалить группу {0}\nЭто действие"
                + " нельзя будет отменить. \nБудут утеряны все данный связанные с этой группой.", selectedNode.Text),
                "Вы уверены?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                foreach (Faculty t in StaticData.faculties)
                    if (t.Title.Equals(selectedNode.Parent.Text))
                    {
                        for (int j = 0; j < t.Groups.Count; j++)
                        {
                            if (t.Groups[j].Title.Equals(selectedNode.Text))
                            {
                                t.Groups.RemoveAt(j);
                                break;
                            }
                        }
                        break;
                    }
                string title = selectedNode.Text;
                selectedNode.Remove();
                IOClass.Save(StaticData.faculties);
                if (UpdateFaculties != null)
                    UpdateFaculties(this, EventArgs.Empty);

                IOClass.Save(StaticData.students.Where(s => s.Group.Title != title).ToList());
                if (RefreshData != null)
                    RefreshData(null, EventArgs.Empty);
                
            }
        }

        private void удалитьФакультетToolStripMenuItem_MouseUp(object sender, MouseEventArgs e)
        {
            if (MessageBox.Show(string.Format("Вы собираетесь удалить факультет {0}\nЭто действие"
                + " нельзя будет отменить. \nБудут утеряны все данный связанные с этим факультетом.", selectedNode.Text),
                "Вы уверены?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                for (int i = 0; i < StaticData.faculties.Count; i++)
                    if (StaticData.faculties[i].Title.Equals(selectedNode.Text))
                    {
                        StaticData.faculties.RemoveAt(i);
                        break;
                    }
                string title = selectedNode.Text;
                treeView1.Nodes.Remove(selectedNode);
                IOClass.Save(StaticData.faculties);
                if (UpdateFaculties != null)
                    UpdateFaculties(this, EventArgs.Empty);

                IOClass.Save(StaticData.students.Where(s => s.Faculty.Title != title).ToList());
                if (RefreshData != null)
                    RefreshData(null, EventArgs.Empty);
            }
        }

        private void GroupTree_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            HelpNavigator navigator = HelpNavigator.Topic;
            Help.ShowHelp(this, IOClass.PathHelp, navigator, "fac.html");
        }

        internal void SelectItem(object sender, TreeNode e)
        {
            if (selectItem != null)
                selectItem(sender, new TreeNodeMouseClickEventArgs(e, MouseButtons.Left, 1, 0, 0));
        }
    }
}
