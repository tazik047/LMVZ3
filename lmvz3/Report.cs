using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using System.Diagnostics;

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
            studentBindingSource.Sort = "FIO";
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
            var groups = StaticData.faculties.First(f => f.Title.Equals(checkedListBox1.Items[e.Index].ToString())).Groups.ToArray();
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
                    if (groups.Count(g => g.Title.Equals(checkedListBox2.Items[i].ToString())) == 0) continue;
                    checkedListBox2.SetItemChecked(i, false);
                    checkedListBox2.Items.RemoveAt(i);
                    i--;
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
                groups = groups.Where(g => !g.Title.Equals(checkedListBox2.Items[e.Index].ToString())).ToList();
            students = IOClass.findByGroup(StaticData.students, groups);
            studentBindingSource.DataSource = students;
            otherCriter();
        }

        private void otherCriter()
        {
            if (radioButton1.Checked)
                studentBindingSource.DataSource = students.Where(s => s.Birth < dateTimePicker1.Value).ToList();
            else if (radioButton3.Checked)
                studentBindingSource.DataSource = students.Where(s => s.FormOfStudy.ToLower() == "бюджет" && s.Birth < dateTimePicker1.Value).ToList();
            else
                studentBindingSource.DataSource = students.Where(s => s.FormOfStudy.ToLower() == "контракт" && s.Birth < dateTimePicker1.Value).ToList();
            dataGridView1.AutoResizeColumns();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Creating iTextSharp Table from the DataTable data
            PdfPTable pdfTable = new PdfPTable(dataGridView1.ColumnCount);
            pdfTable.DefaultCell.Padding = 3;
            pdfTable.WidthPercentage = 100;
            pdfTable.HeaderRows = 2;
            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfTable.DefaultCell.BorderWidth = 1;
            BaseFont font = BaseFont.CreateFont(@"C:\Windows\Fonts\arial.ttf", "Identity-H", false); 
            iTextSharp.text.Font fgFont = new iTextSharp.text.Font(font, 14, iTextSharp.text.Font.NORMAL,
                iTextSharp.text.Color.BLACK);
            //Adding Header row
            /*PdfPCell cell1 = new PdfPCell(new Phrase("№", fgFont));
            cell1.BackgroundColor = new iTextSharp.text.Color(240, 240, 240);
            pdfTable.AddCell(cell1);*/

            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText, fgFont));
                cell.BackgroundColor = new iTextSharp.text.Color(240, 240, 240);
                pdfTable.AddCell(cell);
            }

            int ind = 1;

            //Adding DataRow
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                /*pdfTable.AddCell(new Phrase(ind.ToString(), fgFont));
                ind++;*/
                foreach (DataGridViewCell cell in row.Cells)
                {
                    pdfTable.AddCell(cell.ValueType.Name == dateTimePicker1.Value.GetType().Name
                        ? new Phrase(((DateTime) cell.Value).ToShortDateString(), fgFont)
                        : new Phrase(cell.Value.ToString(), fgFont));
                }
            }

            if(pdfTable.Rows.Count==2)
            {
                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                    pdfTable.AddCell("");
            }

            //Exporting to PDF
            using (FileStream stream = new FileStream("DataGridViewExport.pdf", FileMode.Create))
            {
                Document pdfDoc = new Document(PageSize.A2, 10f, 10f, 10f, 0f);
                //pdfDoc.AddHeader("Отчет", "по группам, по тдавлдподлвпорвдлпрвлдопр");
                PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();
                //da
                string res = students.Select(s => s.Group.Title).Distinct().Aggregate("Список студентов из групп: ", (current, i) => current + (i + ", "));
                res = res.Substring(0, res.Length - 2) + ".\n";
                res += "Форма обучения: " + (radioButton1.Checked ? "любая.\n" : (radioButton2.Checked ? "контрактная.\n" : "бюджетная.\n"));
                res += "Студенты старше: " + dateTimePicker1.Value.ToShortDateString() + "\n \n ";
                
                Paragraph p = new Paragraph(res, fgFont);
                //pdfDoc.aDDP
                pdfDoc.Add(p);
                pdfDoc.Add(pdfTable);
                pdfDoc.Close();
            }
            if (MessageBox.Show("Желаете открыть сформированный отчет?", "Открыть", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try { Process.Start("DataGridViewExport.pdf"); }
                catch { }
            }
        }

        private void Report_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoResizeColumns();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            otherCriter();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            otherCriter();
        }

        private void Report_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Control && e.KeyCode==Keys.F1)
            {
                HelpNavigator navigator = HelpNavigator.AssociateIndex;
                Help.ShowHelp(this, IOClass.PathHelp, navigator, "report");
            }
        }

        private void Report_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            HelpNavigator navigator = HelpNavigator.Topic;
            Help.ShowHelp(this, IOClass.PathHelp, navigator, "report.html");
        }

        private void Report_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            HelpNavigator navigator = HelpNavigator.Topic;
            HelpNavigator navigator1 = HelpNavigator.Index;
            
            Help.ShowHelp(this, IOClass.PathHelp, navigator, "report.html");
        }
    }
}
