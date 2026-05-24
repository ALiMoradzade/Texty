namespace Texty.Tools.Directory_Manager
{
    partial class RenameFolderForm
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
            this.textBoxOldName = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.convertCaseToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lazecaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kebabcaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.snakecaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dotcaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spaceCaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.camelCaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.camelKebabCaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.camelSnakeCaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.camelDotCaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.sentenceCaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.pascalCaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trainCaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pascalSnakeCaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pascalDotCaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.titleCaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.sCREAMINGCASEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cOBOLCASEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sCREAMINGSNAKECASEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sCREAMINGDOTCASEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uPPERSPACECASEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBoxNewName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonApply = new System.Windows.Forms.Button();
            this.iNVERTCASEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxOldName
            // 
            this.textBoxOldName.ContextMenuStrip = this.contextMenuStrip1;
            this.textBoxOldName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxOldName.Location = new System.Drawing.Point(81, 12);
            this.textBoxOldName.Name = "textBoxOldName";
            this.textBoxOldName.ReadOnly = true;
            this.textBoxOldName.Size = new System.Drawing.Size(214, 22);
            this.textBoxOldName.TabIndex = 3;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.convertCaseToToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(181, 48);
            // 
            // convertCaseToToolStripMenuItem
            // 
            this.convertCaseToToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lazecaseToolStripMenuItem,
            this.kebabcaseToolStripMenuItem,
            this.snakecaseToolStripMenuItem,
            this.dotcaseToolStripMenuItem,
            this.spaceCaseToolStripMenuItem,
            this.toolStripSeparator1,
            this.camelCaseToolStripMenuItem,
            this.camelKebabCaseToolStripMenuItem,
            this.camelSnakeCaseToolStripMenuItem,
            this.camelDotCaseToolStripMenuItem,
            this.toolStripSeparator2,
            this.sentenceCaseToolStripMenuItem,
            this.toolStripSeparator3,
            this.pascalCaseToolStripMenuItem,
            this.trainCaseToolStripMenuItem,
            this.pascalSnakeCaseToolStripMenuItem,
            this.pascalDotCaseToolStripMenuItem,
            this.titleCaseToolStripMenuItem,
            this.toolStripSeparator4,
            this.sCREAMINGCASEToolStripMenuItem,
            this.cOBOLCASEToolStripMenuItem,
            this.sCREAMINGSNAKECASEToolStripMenuItem,
            this.sCREAMINGDOTCASEToolStripMenuItem,
            this.uPPERSPACECASEToolStripMenuItem,
            this.toolStripSeparator5,
            this.iNVERTCASEToolStripMenuItem});
            this.convertCaseToToolStripMenuItem.Name = "convertCaseToToolStripMenuItem";
            this.convertCaseToToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.convertCaseToToolStripMenuItem.Text = "Convert Case To";
            // 
            // lazecaseToolStripMenuItem
            // 
            this.lazecaseToolStripMenuItem.Name = "lazecaseToolStripMenuItem";
            this.lazecaseToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.lazecaseToolStripMenuItem.Text = "lazecase";
            this.lazecaseToolStripMenuItem.Click += new System.EventHandler(this.lazecaseToolStripMenuItem_Click);
            // 
            // kebabcaseToolStripMenuItem
            // 
            this.kebabcaseToolStripMenuItem.Name = "kebabcaseToolStripMenuItem";
            this.kebabcaseToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.kebabcaseToolStripMenuItem.Text = "kebab-case";
            this.kebabcaseToolStripMenuItem.Click += new System.EventHandler(this.kebabcaseToolStripMenuItem_Click);
            // 
            // snakecaseToolStripMenuItem
            // 
            this.snakecaseToolStripMenuItem.Name = "snakecaseToolStripMenuItem";
            this.snakecaseToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.snakecaseToolStripMenuItem.Text = "snake_case";
            this.snakecaseToolStripMenuItem.Click += new System.EventHandler(this.snakecaseToolStripMenuItem_Click);
            // 
            // dotcaseToolStripMenuItem
            // 
            this.dotcaseToolStripMenuItem.Name = "dotcaseToolStripMenuItem";
            this.dotcaseToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.dotcaseToolStripMenuItem.Text = "dot.case";
            this.dotcaseToolStripMenuItem.Click += new System.EventHandler(this.dotcaseToolStripMenuItem_Click);
            // 
            // spaceCaseToolStripMenuItem
            // 
            this.spaceCaseToolStripMenuItem.Name = "spaceCaseToolStripMenuItem";
            this.spaceCaseToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.spaceCaseToolStripMenuItem.Text = "space case";
            this.spaceCaseToolStripMenuItem.Click += new System.EventHandler(this.spaceCaseToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(211, 6);
            // 
            // camelCaseToolStripMenuItem
            // 
            this.camelCaseToolStripMenuItem.Name = "camelCaseToolStripMenuItem";
            this.camelCaseToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.camelCaseToolStripMenuItem.Text = "camelCase";
            this.camelCaseToolStripMenuItem.Click += new System.EventHandler(this.camelCaseToolStripMenuItem_Click);
            // 
            // camelKebabCaseToolStripMenuItem
            // 
            this.camelKebabCaseToolStripMenuItem.Name = "camelKebabCaseToolStripMenuItem";
            this.camelKebabCaseToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.camelKebabCaseToolStripMenuItem.Text = "camel-Kebab-Case";
            this.camelKebabCaseToolStripMenuItem.Click += new System.EventHandler(this.camelKebabCaseToolStripMenuItem_Click);
            // 
            // camelSnakeCaseToolStripMenuItem
            // 
            this.camelSnakeCaseToolStripMenuItem.Name = "camelSnakeCaseToolStripMenuItem";
            this.camelSnakeCaseToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.camelSnakeCaseToolStripMenuItem.Text = "camel_Snake_Case";
            this.camelSnakeCaseToolStripMenuItem.Click += new System.EventHandler(this.camelSnakeCaseToolStripMenuItem_Click);
            // 
            // camelDotCaseToolStripMenuItem
            // 
            this.camelDotCaseToolStripMenuItem.Name = "camelDotCaseToolStripMenuItem";
            this.camelDotCaseToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.camelDotCaseToolStripMenuItem.Text = "camel.Dot.Case";
            this.camelDotCaseToolStripMenuItem.Click += new System.EventHandler(this.camelDotCaseToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(211, 6);
            // 
            // sentenceCaseToolStripMenuItem
            // 
            this.sentenceCaseToolStripMenuItem.Name = "sentenceCaseToolStripMenuItem";
            this.sentenceCaseToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.sentenceCaseToolStripMenuItem.Text = "Sentence case";
            this.sentenceCaseToolStripMenuItem.Click += new System.EventHandler(this.sentenceCaseToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(211, 6);
            // 
            // pascalCaseToolStripMenuItem
            // 
            this.pascalCaseToolStripMenuItem.Name = "pascalCaseToolStripMenuItem";
            this.pascalCaseToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.pascalCaseToolStripMenuItem.Text = "PascalCase";
            this.pascalCaseToolStripMenuItem.Click += new System.EventHandler(this.pascalCaseToolStripMenuItem_Click);
            // 
            // trainCaseToolStripMenuItem
            // 
            this.trainCaseToolStripMenuItem.Name = "trainCaseToolStripMenuItem";
            this.trainCaseToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.trainCaseToolStripMenuItem.Text = "Train-Case";
            this.trainCaseToolStripMenuItem.Click += new System.EventHandler(this.trainCaseToolStripMenuItem_Click);
            // 
            // pascalSnakeCaseToolStripMenuItem
            // 
            this.pascalSnakeCaseToolStripMenuItem.Name = "pascalSnakeCaseToolStripMenuItem";
            this.pascalSnakeCaseToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.pascalSnakeCaseToolStripMenuItem.Text = "Pascal_Snake_Case";
            this.pascalSnakeCaseToolStripMenuItem.Click += new System.EventHandler(this.pascalSnakeCaseToolStripMenuItem_Click);
            // 
            // pascalDotCaseToolStripMenuItem
            // 
            this.pascalDotCaseToolStripMenuItem.Name = "pascalDotCaseToolStripMenuItem";
            this.pascalDotCaseToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.pascalDotCaseToolStripMenuItem.Text = "Pascal.Dot.Case";
            this.pascalDotCaseToolStripMenuItem.Click += new System.EventHandler(this.pascalDotCaseToolStripMenuItem_Click);
            // 
            // titleCaseToolStripMenuItem
            // 
            this.titleCaseToolStripMenuItem.Name = "titleCaseToolStripMenuItem";
            this.titleCaseToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.titleCaseToolStripMenuItem.Text = "Title Case";
            this.titleCaseToolStripMenuItem.Click += new System.EventHandler(this.titleCaseToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(211, 6);
            // 
            // sCREAMINGCASEToolStripMenuItem
            // 
            this.sCREAMINGCASEToolStripMenuItem.Name = "sCREAMINGCASEToolStripMenuItem";
            this.sCREAMINGCASEToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.sCREAMINGCASEToolStripMenuItem.Text = "SCREAMINGCASE";
            this.sCREAMINGCASEToolStripMenuItem.Click += new System.EventHandler(this.sCREAMINGCASEToolStripMenuItem_Click);
            // 
            // cOBOLCASEToolStripMenuItem
            // 
            this.cOBOLCASEToolStripMenuItem.Name = "cOBOLCASEToolStripMenuItem";
            this.cOBOLCASEToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.cOBOLCASEToolStripMenuItem.Text = "COBOL-CASE";
            this.cOBOLCASEToolStripMenuItem.Click += new System.EventHandler(this.cOBOLCASEToolStripMenuItem_Click);
            // 
            // sCREAMINGSNAKECASEToolStripMenuItem
            // 
            this.sCREAMINGSNAKECASEToolStripMenuItem.Name = "sCREAMINGSNAKECASEToolStripMenuItem";
            this.sCREAMINGSNAKECASEToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.sCREAMINGSNAKECASEToolStripMenuItem.Text = "SCREAMING_SNAKE_CASE";
            this.sCREAMINGSNAKECASEToolStripMenuItem.Click += new System.EventHandler(this.sCREAMINGSNAKECASEToolStripMenuItem_Click);
            // 
            // sCREAMINGDOTCASEToolStripMenuItem
            // 
            this.sCREAMINGDOTCASEToolStripMenuItem.Name = "sCREAMINGDOTCASEToolStripMenuItem";
            this.sCREAMINGDOTCASEToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.sCREAMINGDOTCASEToolStripMenuItem.Text = "SCREAMING.DOT.CASE";
            this.sCREAMINGDOTCASEToolStripMenuItem.Click += new System.EventHandler(this.sCREAMINGDOTCASEToolStripMenuItem_Click);
            // 
            // uPPERSPACECASEToolStripMenuItem
            // 
            this.uPPERSPACECASEToolStripMenuItem.Name = "uPPERSPACECASEToolStripMenuItem";
            this.uPPERSPACECASEToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.uPPERSPACECASEToolStripMenuItem.Text = "UPPER SPACE CASE";
            this.uPPERSPACECASEToolStripMenuItem.Click += new System.EventHandler(this.uPPERSPACECASEToolStripMenuItem_Click);
            // 
            // textBoxNewName
            // 
            this.textBoxNewName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxNewName.Location = new System.Drawing.Point(81, 40);
            this.textBoxNewName.Name = "textBoxNewName";
            this.textBoxNewName.Size = new System.Drawing.Size(214, 22);
            this.textBoxNewName.TabIndex = 0;
            this.textBoxNewName.TextChanged += new System.EventHandler(this.textBoxNewName_TextChanged);
            this.textBoxNewName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxNewName_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Old Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "New Name:";
            // 
            // buttonApply
            // 
            this.buttonApply.Enabled = false;
            this.buttonApply.Location = new System.Drawing.Point(220, 68);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(75, 23);
            this.buttonApply.TabIndex = 1;
            this.buttonApply.Text = "Apply";
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
            // 
            // iNVERTCASEToolStripMenuItem
            // 
            this.iNVERTCASEToolStripMenuItem.Name = "iNVERTCASEToolStripMenuItem";
            this.iNVERTCASEToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.iNVERTCASEToolStripMenuItem.Text = "iNVERT cASE";
            this.iNVERTCASEToolStripMenuItem.Click += new System.EventHandler(this.iNVERTCASEToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(211, 6);
            // 
            // RenameFolderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(307, 103);
            this.Controls.Add(this.buttonApply);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxNewName);
            this.Controls.Add(this.textBoxOldName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "RenameFolderForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Rename Folder";
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxOldName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonApply;
        public System.Windows.Forms.TextBox textBoxNewName;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem convertCaseToToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lazecaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kebabcaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem snakecaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dotcaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem spaceCaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem camelCaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem camelKebabCaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem camelSnakeCaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem camelDotCaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem sentenceCaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem pascalCaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem trainCaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pascalSnakeCaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pascalDotCaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem titleCaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem sCREAMINGCASEToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cOBOLCASEToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sCREAMINGSNAKECASEToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sCREAMINGDOTCASEToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uPPERSPACECASEToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem iNVERTCASEToolStripMenuItem;
    }
}