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
        List<Student> students;
        public Main()
        {
            InitializeComponent();
            create();
            studentBindingSource.DataSource = students;
        }

        private void create()
        {
            students = new List<Student>();
            for(int i=0; i<10; i++)
            {
                var s = new Student();
                s.Birth = DateTime.Now;
                s.Faculty = "asdasd";
                s.FIO = "ghklfh flghjf lfjhlkf";
                s.FormOfStudy = i % 2 == 0 ? "Бюджет" : "Контракт";
                s.Home = "sfgkljdflgk";
                s.ID = i;
                s.Number = 0953123838;
                s.Pass = "AX000000";
                s.Group = new Group("fsdf", 1);
                students.Add(s);

            }
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
            studentBindingSource.DataSource = students.Where(s => s.ID % 2 == 0).ToList();
        }
    }
}
