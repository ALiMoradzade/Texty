using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Texty.Utilities.StringCaseConvertor;

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

        #region Click Right

        #region Convert Case To
        private void lazecaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string oldText = textBoxOldName.Text;
            if (!StringCaseConvertor.IsSplitable(oldText))
            {
                StringCaseConvertor.MessageBoxWrongFormat("lazy");
                return;
            }

            textBoxNewName.Text = StringCaseConvertor.ToLazyCase(oldText);
        }

        private void kebabcaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string oldText = textBoxOldName.Text;
            if (!StringCaseConvertor.IsSplitable(oldText))
            {
                StringCaseConvertor.MessageBoxWrongFormat("kebab");
                return;
            }

            textBoxNewName.Text = StringCaseConvertor.ToKebabCase(oldText);
        }

        private void snakecaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string oldText = textBoxOldName.Text;
            if (!StringCaseConvertor.IsSplitable(oldText))
            {
                StringCaseConvertor.MessageBoxWrongFormat("snake");
                return;
            }

            textBoxNewName.Text = StringCaseConvertor.ToSnakeCase(oldText);
        }

        private void dotcaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string oldText = textBoxOldName.Text;
            if (!StringCaseConvertor.IsSplitable(oldText))
            {
                StringCaseConvertor.MessageBoxWrongFormat("dot");
                return;
            }

            textBoxNewName.Text = StringCaseConvertor.ToDotCase(oldText);
        }

        private void spaceCaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string oldText = textBoxOldName.Text;
            if (!StringCaseConvertor.IsSplitable(oldText))
            {
                StringCaseConvertor.MessageBoxWrongFormat("space");
                return;
            }

            textBoxNewName.Text = StringCaseConvertor.ToSpaceCase(oldText);
        }

        private void camelCaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string oldText = textBoxOldName.Text;
            if (!StringCaseConvertor.IsSplitable(oldText))
            {
                StringCaseConvertor.MessageBoxWrongFormat("camel");
                return;
            }

            textBoxNewName.Text = StringCaseConvertor.ToCamelCase(oldText);
        }

        private void camelKebabCaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string oldText = textBoxOldName.Text;
            if (!StringCaseConvertor.IsSplitable(oldText))
            {
                StringCaseConvertor.MessageBoxWrongFormat("camel kebab");
                return;
            }

            textBoxNewName.Text = StringCaseConvertor.ToCamelKebabCase(oldText);
        }

        private void camelSnakeCaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string oldText = textBoxOldName.Text;
            if (!StringCaseConvertor.IsSplitable(oldText))
            {
                StringCaseConvertor.MessageBoxWrongFormat("camel snake");
                return;
            }

            textBoxNewName.Text = StringCaseConvertor.ToCamelSnakeCase(oldText);
        }

        private void camelDotCaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string oldText = textBoxOldName.Text;
            if (!StringCaseConvertor.IsSplitable(oldText))
            {
                StringCaseConvertor.MessageBoxWrongFormat("camel dot");
                return;
            }

            textBoxNewName.Text = StringCaseConvertor.ToCamelDotCase(oldText);
        }

        private void sentenceCaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string oldText = textBoxOldName.Text;
            if (!StringCaseConvertor.IsSplitable(oldText))
            {
                StringCaseConvertor.MessageBoxWrongFormat("sentence");
                return;
            }

            textBoxNewName.Text = StringCaseConvertor.ToSentenceCase(oldText);
        }

        private void pascalCaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string oldText = textBoxOldName.Text;
            if (!StringCaseConvertor.IsSplitable(oldText))
            {
                StringCaseConvertor.MessageBoxWrongFormat("pascal");
                return;
            }

            textBoxNewName.Text = StringCaseConvertor.ToPascalCase(oldText);
        }

        private void trainCaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string oldText = textBoxOldName.Text;
            if (!StringCaseConvertor.IsSplitable(oldText))
            {
                StringCaseConvertor.MessageBoxWrongFormat("train");
                return;
            }

            textBoxNewName.Text = StringCaseConvertor.ToTrainCase(oldText);
        }

        private void pascalSnakeCaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string oldText = textBoxOldName.Text;
            if (!StringCaseConvertor.IsSplitable(oldText))
            {
                StringCaseConvertor.MessageBoxWrongFormat("pascal snake");
                return;
            }

            textBoxNewName.Text = StringCaseConvertor.ToPascalSnakeCase(oldText);
        }

        private void pascalDotCaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string oldText = textBoxOldName.Text;
            if (!StringCaseConvertor.IsSplitable(oldText))
            {
                StringCaseConvertor.MessageBoxWrongFormat("pascal dot");
                return;
            }

            textBoxNewName.Text = StringCaseConvertor.ToPascalDotCase(oldText);
        }

        private void titleCaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string oldText = textBoxOldName.Text;
            if (!StringCaseConvertor.IsSplitable(oldText))
            {
                StringCaseConvertor.MessageBoxWrongFormat("title");
                return;
            }

            textBoxNewName.Text = StringCaseConvertor.ToTitleCase(oldText);
        }

        private void sCREAMINGCASEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string oldText = textBoxOldName.Text;
            if (!StringCaseConvertor.IsSplitable(oldText))
            {
                StringCaseConvertor.MessageBoxWrongFormat("screaming");
                return;
            }

            textBoxNewName.Text = StringCaseConvertor.ToScreamingCase(oldText);
        }

        private void cOBOLCASEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string oldText = textBoxOldName.Text;
            if (!StringCaseConvertor.IsSplitable(oldText))
            {
                StringCaseConvertor.MessageBoxWrongFormat("cobol");
                return;
            }

            textBoxNewName.Text = StringCaseConvertor.ToCobolCase(oldText);
        }

        private void sCREAMINGSNAKECASEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string oldText = textBoxOldName.Text;
            if (!StringCaseConvertor.IsSplitable(oldText))
            {
                StringCaseConvertor.MessageBoxWrongFormat("screming snake");
                return;
            }

            textBoxNewName.Text = StringCaseConvertor.ToScreamingSnakeCase(oldText);
        }

        private void sCREAMINGDOTCASEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string oldText = textBoxOldName.Text;
            if (!StringCaseConvertor.IsSplitable(oldText))
            {
                StringCaseConvertor.MessageBoxWrongFormat("screaming dot");
                return;
            }

            textBoxNewName.Text = StringCaseConvertor.ToScreamingDotCase(oldText);
        }

        private void uPPERSPACECASEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string oldText = textBoxOldName.Text;
            if (!StringCaseConvertor.IsSplitable(oldText))
            {
                StringCaseConvertor.MessageBoxWrongFormat("upper space");
                return;
            }

            textBoxNewName.Text = StringCaseConvertor.ToUpperSpaceCase(oldText);
        }

        private void iNVERTCASEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string oldText = textBoxOldName.Text;
            if (!StringCaseConvertor.IsSplitable(oldText))
            {
                StringCaseConvertor.MessageBoxWrongFormat("invert");
                return;
            }

            textBoxNewName.Text = StringCaseConvertor.ToInvertCase(oldText);
        }
        #endregion


        #endregion

       
    }
}
