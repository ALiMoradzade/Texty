using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace Texty.Tools.Directory_Manager
{
    public partial class NewFoldersForm : Form
    {
        public NewFoldersForm()
        {
            InitializeComponent();
          
            toolTip1.SetToolTip(richTextBoxNewFolders, "Every line is a new folder");
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void richTextBoxNewFolders_TextChanged(object sender, EventArgs e)
        {
            if (richTextBoxNewFolders.Text.Length > 0) buttonApply.Enabled = true;
            else buttonApply.Enabled = false;
        }
    }
}
