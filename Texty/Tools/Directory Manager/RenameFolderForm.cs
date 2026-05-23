using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Texty.Tools.Directory_Manager
{
    public partial class RenameFolderForm : Form
    {
        public RenameFolderForm(string oldName)
        {
            InitializeComponent();

            textBoxOldName.Text = oldName;
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void textBoxNewName_TextChanged(object sender, EventArgs e)
        {
            if (textBoxNewName.Text.Length > 0) buttonApply.Enabled = true;
            else buttonApply.Enabled = false;
        }

        private void textBoxNewName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r' && buttonApply.Enabled)
            {
                buttonApply.PerformClick();
            }
        }
    }
}
