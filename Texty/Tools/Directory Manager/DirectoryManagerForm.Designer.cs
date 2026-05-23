namespace Texty.Tools.Directory_Manager
{
    partial class DirectoryManagerForm
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
            this.buttonLoad = new System.Windows.Forms.Button();
            this.listViewCurrentDirectories = new System.Windows.Forms.ListView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.undoDeleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonAddNewFolder = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonApply = new System.Windows.Forms.Button();
            this.listViewRenamedDirectories = new System.Windows.Forms.ListView();
            this.contextMenuStrip3 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.undoRenameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listViewNewDirectories = new System.Windows.Forms.ListView();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuStrip3.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonLoad
            // 
            this.buttonLoad.Location = new System.Drawing.Point(75, 185);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(103, 23);
            this.buttonLoad.TabIndex = 3;
            this.buttonLoad.Text = "Load Directories";
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // listViewCurrentDirectories
            // 
            this.listViewCurrentDirectories.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listViewCurrentDirectories.ContextMenuStrip = this.contextMenuStrip1;
            this.listViewCurrentDirectories.FullRowSelect = true;
            this.listViewCurrentDirectories.HideSelection = false;
            this.listViewCurrentDirectories.Location = new System.Drawing.Point(75, 12);
            this.listViewCurrentDirectories.MultiSelect = false;
            this.listViewCurrentDirectories.Name = "listViewCurrentDirectories";
            this.listViewCurrentDirectories.ShowGroups = false;
            this.listViewCurrentDirectories.Size = new System.Drawing.Size(168, 167);
            this.listViewCurrentDirectories.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listViewCurrentDirectories.TabIndex = 5;
            this.listViewCurrentDirectories.UseCompatibleStateImageBehavior = false;
            this.listViewCurrentDirectories.View = System.Windows.Forms.View.List;
            this.listViewCurrentDirectories.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listViewCurrentDirectories_MouseUp);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.renameToolStripMenuItem,
            this.deleteToolStripMenuItem1,
            this.undoDeleteToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip3";
            this.contextMenuStrip1.Size = new System.Drawing.Size(140, 70);
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            this.renameToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.renameToolStripMenuItem.Text = "Rename";
            this.renameToolStripMenuItem.Click += new System.EventHandler(this.renameCurrentDirectoryToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem1
            // 
            this.deleteToolStripMenuItem1.Name = "deleteToolStripMenuItem1";
            this.deleteToolStripMenuItem1.Size = new System.Drawing.Size(139, 22);
            this.deleteToolStripMenuItem1.Text = "Delete";
            this.deleteToolStripMenuItem1.Click += new System.EventHandler(this.deleteFromCurrentDirectoryToolStripMenuItem1_Click);
            // 
            // undoDeleteToolStripMenuItem
            // 
            this.undoDeleteToolStripMenuItem.Name = "undoDeleteToolStripMenuItem";
            this.undoDeleteToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.undoDeleteToolStripMenuItem.Text = "Undo Delete";
            this.undoDeleteToolStripMenuItem.Click += new System.EventHandler(this.undoDeleteFromCurrentFirectoryToolStripMenuItem_Click);
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.ShowNewFolderButton = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 26);
            this.label2.TabIndex = 7;
            this.label2.Text = "Current\r\nDirectories";
            // 
            // buttonAddNewFolder
            // 
            this.buttonAddNewFolder.Location = new System.Drawing.Point(75, 214);
            this.buttonAddNewFolder.Name = "buttonAddNewFolder";
            this.buttonAddNewFolder.Size = new System.Drawing.Size(103, 23);
            this.buttonAddNewFolder.TabIndex = 9;
            this.buttonAddNewFolder.Text = "Add New Folder(s)";
            this.buttonAddNewFolder.UseVisualStyleBackColor = true;
            this.buttonAddNewFolder.Click += new System.EventHandler(this.buttonAddNewFolderCurrenctDirectory_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(263, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 26);
            this.label3.TabIndex = 10;
            this.label3.Text = "New \nDirectories";
            // 
            // buttonApply
            // 
            this.buttonApply.Enabled = false;
            this.buttonApply.Location = new System.Drawing.Point(554, 214);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(110, 23);
            this.buttonApply.TabIndex = 11;
            this.buttonApply.Text = "Apply The Changes";
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
            // 
            // listViewRenamedDirectories
            // 
            this.listViewRenamedDirectories.ContextMenuStrip = this.contextMenuStrip3;
            this.listViewRenamedDirectories.FullRowSelect = true;
            this.listViewRenamedDirectories.GridLines = true;
            this.listViewRenamedDirectories.HideSelection = false;
            this.listViewRenamedDirectories.Location = new System.Drawing.Point(577, 12);
            this.listViewRenamedDirectories.MultiSelect = false;
            this.listViewRenamedDirectories.Name = "listViewRenamedDirectories";
            this.listViewRenamedDirectories.ShowGroups = false;
            this.listViewRenamedDirectories.Size = new System.Drawing.Size(168, 167);
            this.listViewRenamedDirectories.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listViewRenamedDirectories.TabIndex = 13;
            this.listViewRenamedDirectories.UseCompatibleStateImageBehavior = false;
            this.listViewRenamedDirectories.View = System.Windows.Forms.View.List;
            // 
            // contextMenuStrip3
            // 
            this.contextMenuStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoRenameToolStripMenuItem});
            this.contextMenuStrip3.Name = "contextMenuStrip1";
            this.contextMenuStrip3.Size = new System.Drawing.Size(150, 26);
            // 
            // undoRenameToolStripMenuItem
            // 
            this.undoRenameToolStripMenuItem.Name = "undoRenameToolStripMenuItem";
            this.undoRenameToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.undoRenameToolStripMenuItem.Text = "Undo Rename";
            this.undoRenameToolStripMenuItem.Click += new System.EventHandler(this.undoRenameRenamedDirectoryToolStripMenuItem_Click);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip1";
            this.contextMenuStrip2.Size = new System.Drawing.Size(108, 26);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteFromNewDirectoriesToolStripMenuItem_Click);
            // 
            // listViewNewDirectories
            // 
            this.listViewNewDirectories.ContextMenuStrip = this.contextMenuStrip2;
            this.listViewNewDirectories.FullRowSelect = true;
            this.listViewNewDirectories.GridLines = true;
            this.listViewNewDirectories.HideSelection = false;
            this.listViewNewDirectories.Location = new System.Drawing.Point(326, 12);
            this.listViewNewDirectories.MultiSelect = false;
            this.listViewNewDirectories.Name = "listViewNewDirectories";
            this.listViewNewDirectories.ShowGroups = false;
            this.listViewNewDirectories.Size = new System.Drawing.Size(168, 167);
            this.listViewNewDirectories.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listViewNewDirectories.TabIndex = 16;
            this.listViewNewDirectories.UseCompatibleStateImageBehavior = false;
            this.listViewNewDirectories.View = System.Windows.Forms.View.List;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(514, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 26);
            this.label4.TabIndex = 17;
            this.label4.Text = "Renamed \nDirectories";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(670, 214);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 18;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // DirectoryManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(757, 249);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.listViewNewDirectories);
            this.Controls.Add(this.listViewRenamedDirectories);
            this.Controls.Add(this.buttonApply);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonAddNewFolder);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listViewCurrentDirectories);
            this.Controls.Add(this.buttonLoad);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "DirectoryManagerForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Directory Manager";
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip3.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonLoad;
        private System.Windows.Forms.ListView listViewCurrentDirectories;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonAddNewFolder;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.ListView listViewRenamedDirectories;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip3;
        private System.Windows.Forms.ListView listViewNewDirectories;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStripMenuItem undoRenameToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem undoDeleteToolStripMenuItem;
        private System.Windows.Forms.Button button1;
    }
}