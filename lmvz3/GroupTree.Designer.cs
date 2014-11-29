namespace lmvz3
{
    partial class GroupTree
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.contextMenuStripMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.добавитьФакультетToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripGroup = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.добавитьГруппуToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьГруппуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripFac = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.добавитьФакультетToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьФакультетToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.добавитьГруппуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripMain.SuspendLayout();
            this.contextMenuStripGroup.SuspendLayout();
            this.contextMenuStripFac.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.treeView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeView1.ContextMenuStrip = this.contextMenuStripMain;
            this.treeView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.treeView1.Location = new System.Drawing.Point(12, 21);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(120, 450);
            this.treeView1.TabIndex = 1;
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            // 
            // contextMenuStripMain
            // 
            this.contextMenuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.добавитьФакультетToolStripMenuItem});
            this.contextMenuStripMain.Name = "contextMenuStripMain";
            this.contextMenuStripMain.Size = new System.Drawing.Size(186, 26);
            // 
            // добавитьФакультетToolStripMenuItem
            // 
            this.добавитьФакультетToolStripMenuItem.Name = "добавитьФакультетToolStripMenuItem";
            this.добавитьФакультетToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.добавитьФакультетToolStripMenuItem.Text = "Добавить факультет";
            this.добавитьФакультетToolStripMenuItem.MouseUp += new System.Windows.Forms.MouseEventHandler(this.добавитьФакультетToolStripMenuItem1_MouseUp);
            // 
            // contextMenuStripGroup
            // 
            this.contextMenuStripGroup.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.добавитьГруппуToolStripMenuItem1,
            this.удалитьГруппуToolStripMenuItem});
            this.contextMenuStripGroup.Name = "contextMenuStripGroup";
            this.contextMenuStripGroup.Size = new System.Drawing.Size(168, 48);
            // 
            // добавитьГруппуToolStripMenuItem1
            // 
            this.добавитьГруппуToolStripMenuItem1.Name = "добавитьГруппуToolStripMenuItem1";
            this.добавитьГруппуToolStripMenuItem1.Size = new System.Drawing.Size(167, 22);
            this.добавитьГруппуToolStripMenuItem1.Text = "Добавить группу";
            this.добавитьГруппуToolStripMenuItem1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.добавитьГруппуToolStripMenuItem_MouseUp);
            // 
            // удалитьГруппуToolStripMenuItem
            // 
            this.удалитьГруппуToolStripMenuItem.Name = "удалитьГруппуToolStripMenuItem";
            this.удалитьГруппуToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.удалитьГруппуToolStripMenuItem.Text = "Удалить группу";
            this.удалитьГруппуToolStripMenuItem.MouseUp += new System.Windows.Forms.MouseEventHandler(this.удалитьГруппуToolStripMenuItem_MouseUp);
            // 
            // contextMenuStripFac
            // 
            this.contextMenuStripFac.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.добавитьФакультетToolStripMenuItem1,
            this.удалитьФакультетToolStripMenuItem,
            this.добавитьГруппуToolStripMenuItem});
            this.contextMenuStripFac.Name = "contextMenuStripFac";
            this.contextMenuStripFac.Size = new System.Drawing.Size(186, 70);
            // 
            // добавитьФакультетToolStripMenuItem1
            // 
            this.добавитьФакультетToolStripMenuItem1.Name = "добавитьФакультетToolStripMenuItem1";
            this.добавитьФакультетToolStripMenuItem1.Size = new System.Drawing.Size(185, 22);
            this.добавитьФакультетToolStripMenuItem1.Text = "Добавить факультет";
            this.добавитьФакультетToolStripMenuItem1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.добавитьФакультетToolStripMenuItem1_MouseUp);
            // 
            // удалитьФакультетToolStripMenuItem
            // 
            this.удалитьФакультетToolStripMenuItem.Name = "удалитьФакультетToolStripMenuItem";
            this.удалитьФакультетToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.удалитьФакультетToolStripMenuItem.Text = "Удалить факультет";
            this.удалитьФакультетToolStripMenuItem.MouseUp += new System.Windows.Forms.MouseEventHandler(this.удалитьФакультетToolStripMenuItem_MouseUp);
            // 
            // добавитьГруппуToolStripMenuItem
            // 
            this.добавитьГруппуToolStripMenuItem.Name = "добавитьГруппуToolStripMenuItem";
            this.добавитьГруппуToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.добавитьГруппуToolStripMenuItem.Text = "Добавить группу";
            this.добавитьГруппуToolStripMenuItem.MouseUp += new System.Windows.Forms.MouseEventHandler(this.добавитьГруппуToolStripMenuItem_MouseUp);
            // 
            // GroupTree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::lmvz3.Properties.Resources.Безымянный;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(144, 497);
            this.Controls.Add(this.treeView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "GroupTree";
            this.Text = "GroupTree";
            this.contextMenuStripMain.ResumeLayout(false);
            this.contextMenuStripGroup.ResumeLayout(false);
            this.contextMenuStripFac.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripMain;
        private System.Windows.Forms.ToolStripMenuItem добавитьФакультетToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripGroup;
        private System.Windows.Forms.ToolStripMenuItem добавитьГруппуToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem удалитьГруппуToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripFac;
        private System.Windows.Forms.ToolStripMenuItem добавитьФакультетToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem удалитьФакультетToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem добавитьГруппуToolStripMenuItem;

    }
}