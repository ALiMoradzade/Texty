using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Json_Editor
{
    public partial class JsonEditorForm : Form
    {
        public JsonEditorForm()
        {
            InitializeComponent();
        }

        private void JsonEditorForm_Resize(object sender, EventArgs e)
        {
            int width = (Width - 15) / 2;
            if (width > 0)
            {
                textBox1.Size = new Size(width, textBox1.Height);
                treeView1.Size = new Size(width, textBox1.Height);
            }
        }

        #region Message Box
        private static DialogResult MessageBoxPropertyDoesntFound(string property)
        {
            var r = MessageBox.Show($"The JSON property \"{property}\" could not be found",
                                     "Property Not Found",
                                     MessageBoxButtons.OK,
                                     MessageBoxIcon.Error);
            return r;
        }

        private static DialogResult MessageBoxPropertyRenameChoice(string oldProperty, string newProperty)
        {
            var r = MessageBox.Show($"Are you sure you want to rename the property \"{oldProperty}\" to \"{newProperty}\"?",
                                     "Rename Property",
                                     MessageBoxButtons.YesNo,
                                     MessageBoxIcon.Information,
                                     MessageBoxDefaultButton.Button2);
            return r;
        }

        private static DialogResult MessageBoxPropertyRenamedSuccessful()
        {
            var r = MessageBox.Show($"The property was renamed successfully",
                                     "Property Renamed",
                                     MessageBoxButtons.OK,
                                     MessageBoxIcon.Information);
            return r;
        }

        private static DialogResult MessageBoxPropertyRemoveChoice(string property)
        {
            var r = MessageBox.Show($"Are you sure you want to remove the property \"{property}\"?",
                                     "Remove Property",
                                     MessageBoxButtons.YesNo,
                                     MessageBoxIcon.Information,
                                     MessageBoxDefaultButton.Button2);
            return r;
        }

        private static DialogResult MessageBoxPropertyRemovedSuccessful()
        {
            var r = MessageBox.Show($"The property was removed successfully",
                                     "Property Removed",
                                     MessageBoxButtons.OK,
                                     MessageBoxIcon.Information);
            return r;
        }

        private static DialogResult MessageBoxJsonAlreadyBeautifyied()
        {
            var r = MessageBox.Show("The JSON is already beautified",
                                    "JSON Beautified",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);
            return r;
        }

        private static DialogResult MessageBoxJsonBeautifiedSuccessful()
        {
            var r = MessageBox.Show("The JSON was beautified successfully",
                                    "JSON Beautified",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
            return r;
        }

        private static DialogResult MessageBoxJsonAlreadyMinified()
        {
            var r = MessageBox.Show("The JSON is already minified",
                                    "JSON Minified",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);
            return r;
        }

        private static DialogResult MessageBoxJsonMinifiedSuccessful()
        {
            var r = MessageBox.Show("The JSON was minified successfully",
                                    "JSON Minified",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
            return r;
        }
        #endregion

        #region File Tab
        private async void openJSONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (StreamReader streamReader = new StreamReader(openFileDialog1.FileName))
                {
                    textBox1.Text = await streamReader.ReadToEndAsync();
                }

                JsonTreeConverter jsonTreeConverter = new JsonTreeConverter(textBox1.Text);

                treeView1.Nodes.Clear();
                treeView1.Nodes.Add(jsonTreeConverter.Result);
            }
        }

        private void saveAsJSONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog1.FileName, textBox1.Text);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        #region Edit Tab
        private void beautifyJSONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text)) return;

            JToken token = JToken.Parse(textBox1.Text);
            string beautified = token.ToString(Formatting.Indented);

            if (textBox1.Text == beautified)
            {
                MessageBoxJsonAlreadyBeautifyied();
                return;
            }

            textBox1.Text = beautified;
            MessageBoxJsonBeautifiedSuccessful();
        }

        private void minifyJSONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text)) return;

            JToken token = JToken.Parse(textBox1.Text);
            string minified = token.ToString(Formatting.None);

            if (textBox1.Text == minified)
            {
                MessageBoxJsonAlreadyMinified();
                return;
            }

            textBox1.Text = minified;
            MessageBoxJsonMinifiedSuccessful();
        }

        private void renamePropertyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text)) return;

            RenamePropertyForm form = new RenamePropertyForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (!textBox1.Text.Contains(form.OldPropertyName))
                {
                    MessageBoxPropertyDoesntFound(form.OldPropertyName);
                    return;
                }

                if (MessageBoxPropertyRenameChoice(form.OldPropertyName, form.NewPropertyName) == DialogResult.Yes)
                {
                    textBox1.Text = textBox1.Text.Replace(form.OldPropertyName, form.NewPropertyName);
                    MessageBoxPropertyRenamedSuccessful();
                }
            }
        }

        private void removePropertyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text)) return;

            RemovePropertyForm form = new RemovePropertyForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (!textBox1.Text.Contains(form.PropertyName))
                {
                    MessageBoxPropertyDoesntFound(form.PropertyName);
                    return;
                }

                if (MessageBoxPropertyRemoveChoice(form.PropertyName) == DialogResult.Yes)
                {
                    textBox1.Lines = textBox1.Lines.Where(line => !line.Contains(form.PropertyName)).ToArray();
                    MessageBoxPropertyRemovedSuccessful();
                }
            }
        }
        #endregion

        #region Right Click TextBox
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Copy();
        }
        #endregion

        #region Right Click TextBox
        private void reloadJSONTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            JsonTreeConverter jsonTreeConverter = new JsonTreeConverter(textBox1.Text);

            treeView1.Nodes.Clear();
            treeView1.Nodes.Add(jsonTreeConverter.Result);
        }

        private void collapseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            treeView1.CollapseAll();
        }

        private void expandAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            treeView1.ExpandAll();
        }
        #endregion

    }
}
