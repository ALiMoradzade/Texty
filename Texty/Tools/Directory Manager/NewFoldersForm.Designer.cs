namespace Texty.Tools.Directory_Manager
{
    partial class NewFoldersForm
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
            this.richTextBoxNewFolders = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonApply = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // richTextBoxNewFolders
            // 
            this.richTextBoxNewFolders.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxNewFolders.Location = new System.Drawing.Point(58, 12);
            this.richTextBoxNewFolders.Name = "richTextBoxNewFolders";
            this.richTextBoxNewFolders.Size = new System.Drawing.Size(304, 127);
            this.richTextBoxNewFolders.TabIndex = 0;
            this.richTextBoxNewFolders.Text = "";
            this.richTextBoxNewFolders.TextChanged += new System.EventHandler(this.richTextBoxNewFolders_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 26);
            this.label1.TabIndex = 1;
            this.label1.Text = "New\r\nFolders";
            // 
            // buttonApply
            // 
            this.buttonApply.Enabled = false;
            this.buttonApply.Location = new System.Drawing.Point(287, 145);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(75, 23);
            this.buttonApply.TabIndex = 3;
            this.buttonApply.Text = "Apply";
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 15;
            this.toolTip1.AutoPopDelay = 4000;
            this.toolTip1.InitialDelay = 1500;
            this.toolTip1.ReshowDelay = 3;
            // 
            // NewFoldersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(374, 180);
            this.Controls.Add(this.buttonApply);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.richTextBoxNewFolders);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "NewFoldersForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New Folders";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonApply;
        public System.Windows.Forms.RichTextBox richTextBoxNewFolders;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}