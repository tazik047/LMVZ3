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
        TreeNode selectedNode;

        public event EventHandler UpdateFaculties;

        public GroupTree(Action<Object, TreeNodeMouseClickEventArgs> filter)
        {
            InitializeComponent();
            //faculties = IOClass.LoadFaculty();
            StaticData.faculties = IOClass.LoadFac();
            CreateNodes();
            treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(filter);
        }

        private void CreateNodes()
        {
            var t = new TreeNode("Все факультеты");
            t.ContextMenu = null;
            treeView1.Nodes.Add(t);
            for (int i = 0; i < StaticData.faculties.Count; i++)
            {
                var ns = new TreeNode[StaticData.faculties[i].Groups.Count];
                for (int j = 0; j < StaticData.faculties[i].Groups.Count; j++)
                {
                    ns[j] = new TreeNode(StaticData.faculties[i].Groups[j].Title);
                    ns[j].ContextMenuStrip = contextMenuStripGroup;
                }
                var n = new TreeNode(StaticData.faculties[i].Title, ns);
                n.ContextMenuStrip = contextMenuStripFac;
                treeView1.Nodes.Add(n);
            }
        }

        private void create()
        {
            StaticData.faculties = new List<Faculty>();
            for (int i = 0; i < 10; i++)
            {
                var gs = new List<Group>();
                for (int j = 0; j < 10; j++)
                    gs.Add(new Group(String.Format("Fac{0}-Group{1}", i, j), i));
                StaticData.faculties.Add(new Faculty(i, String.Format("Fac{0}", i), gs));
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
            for (int i = 0; i < StaticData.faculties.Count; i++)
            {
                if (StaticData.faculties[i].Groups.Count(g => g.Title.Equals(title)) != 0)
                    return false;
            }
            return true;
        }

        private void добавитьФакультетToolStripMenuItem1_MouseUp(object sender, MouseEventArgs e)
        {
            if(e.Button== MouseButtons.Left)
            {
                CreateForm c = new CreateForm("Введите название нового факультета:", checkFac, "Такой факультет уже присутствует");
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
                    StaticData.faculties.Where(f => f.Title.Equals(title)).First().Groups.Add(g);
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
                for (int i = 0; i < StaticData.faculties.Count; i++)
                    if (StaticData.faculties[i].Title.Equals(selectedNode.Parent.Text))
                    {
                        for (int j = 0; j < StaticData.faculties[i].Groups.Count; j++)
                        {
                            if (StaticData.faculties[i].Groups[j].Title.Equals(selectedNode.Text))
                            {
                                StaticData.faculties[i].Groups.RemoveAt(j);
                                break;
                            }
                        }
                        break;
                    }
                selectedNode.Remove();
                IOClass.Save(StaticData.faculties);
                if (UpdateFaculties != null)
                    UpdateFaculties(this, EventArgs.Empty);
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
                treeView1.Nodes.Remove(selectedNode);
                IOClass.Save(StaticData.faculties);
                if (UpdateFaculties != null)
                    UpdateFaculties(this, EventArgs.Empty);
            }
        }
    }
}
