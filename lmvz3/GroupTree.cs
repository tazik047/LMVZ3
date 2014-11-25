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
        List<Faculty> faculties;

        public GroupTree(Action<Object, TreeNodeMouseClickEventArgs> filter)
        {
            InitializeComponent();
            //faculties = IOClass.LoadFaculty();
            create();
            for (int i = 0; i < faculties.Count; i++)
            {
                var ns = new TreeNode[faculties[i].Groups.Count];
                for (int j = 0; j < faculties[i].Groups.Count; j++)
                    ns[j] = new TreeNode(faculties[i].Groups[j].Title);
                treeView1.Nodes.Add(new TreeNode(faculties[i].Title, ns));
            }
            treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(filter);
        }

        private void create()
        {
            faculties = new List<Faculty>();
            for (int i = 0; i < 10; i++)
            {
                var gs = new List<Group>();
                for (int j = 0; j < 10; j++)
                    gs.Add(new Group(String.Format("Fac{0}-Group{1}", i, j), i));
                faculties.Add(new Faculty(i, String.Format("Fac{0}", i), gs));
            }
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var n = e.Node;
                if (n.Parent == null)
                {
                    MessageBox.Show(String.Format("Вы выбрали факультет - {0}", n.Text), "Click");
                }
            }
        }
    }
}
