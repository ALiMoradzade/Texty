using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.AxHost;

namespace Texty.Tools.Directory_Manager
{
    public partial class DirectoryManagerForm : Form
    {
        List<string> deleteDirectories = new List<string>();
        List<string> newDirectories = new List<string>();
        List<DirectoryRename> renamedDirectories = new List<DirectoryRename>();

        public DirectoryManagerForm()
        {
            InitializeComponent();
        }

        private string GetDirectoryPath()
        {
            return folderBrowserDialog1.SelectedPath;
        }

        private ListViewItem[] ConvertToListViewItem(string[] names)
        {
            ListViewItem[] items = names.Select(name => new ListViewItem(name))
                                        .ToArray();
            return items;
        }

        private void EnableVisibilityDeleteOnCurrecntDirectoriesListView(bool state)
        {
            deleteToolStripMenuItem1.Visible = state;
            undoDeleteToolStripMenuItem.Visible = !state;
        }



        #region Message Boxes
        private DialogResult MessageBoxDirectoryAlreadyExists(IEnumerable<string> names)
        {
            string text, caption;
            if (names.Count() > 1)
            {
                text = $"{string.Join(", ", names)} directories already exist";
                caption = "Can't create these folders";
            }
            else
            {
                text = $"{names.First()} directory already exists";
                caption = "Can't create this folder";
            }

            var r = MessageBox.Show(text,
                                    caption,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);
            return r;
        }

        private DialogResult MessageBoxDirectoryDoesntExist()
        {
            var r = MessageBox.Show("Directory doesn't exist",
                                    "Can't delete",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
            return r;
        }

        private DialogResult MessageBoxDirectoryIsOpen()
        {
            var r = MessageBox.Show("Directory is already in use",
                                    "Can't delete",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
            return r;
        }

        private DialogResult MessageBoxDirectoryPathIsEmpty()
        {
            var r = MessageBox.Show("Please, select path",
                                    "Path is empty",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
            return r;
        }
        #endregion

        #region Apply Button Methods
        private void EnableApply(bool state)
        {
            buttonApply.Enabled = state;
        }

        private void CheckApplyButtonEnability()
        {
            if (deleteDirectories.Count() > 0 || newDirectories.Count() > 0 || renamedDirectories.Count > 0)
            {
                buttonApply.Enabled = true;
            }
            else
            {
                buttonApply.Enabled = false;
            }
        }
        #endregion

        #region Directory Manager
        private static bool DoesDirectoryExist(string path, string name)
        {
            return Directory.Exists(Path.Combine(path, name));
        }

        private static bool IsDirectoryOpen(string path, string name)
        {
            return Directory.GetFileSystemEntries(Path.Combine(path, name)).Length != 0;
        }

        private static void DirectoriesDelete(string path, List<string> names)
        {
            foreach (string name in names)
            {
                Directory.Delete(Path.Combine(path, name));
            }
        }

        private static void DirectoriesCreate(string path, List<string> names)
        {
            foreach (string name in names)
            {
                Directory.CreateDirectory(Path.Combine(path, name));
            }
        }

        private static string[] DirectoriesLoad(string path)
        {
            var directories = Directory.EnumerateDirectories(path);
            return directories.ToArray();
            //var files = Directory.EnumerateFiles(path);
            //return files.ToArray();
        }

        private static void DirectoriesRename(string path, List<DirectoryRename> directoryRenames)
        {
            foreach (DirectoryRename dir in directoryRenames)
            {
                Directory.Move(Path.Combine(path, dir.OldName), Path.Combine(path, dir.NewName));
            }
        }
        #endregion


        #region Currecnt Directories List View Methods
        private void AddToCurrentDirectoriesListView(string names)
        {
            listViewCurrentDirectories.Items.Add(names);
        }

        private void AddToCurrentDirectoriesListView(string[] names)
        {
            ListViewItem[] current = ConvertToListViewItem(names);
            listViewCurrentDirectories.Items.AddRange(current);
        }

        private void RemoveFromCurrentDirectoriesListView(string name)
        {
            ListViewItem current = listViewCurrentDirectories.FindItemWithText(name);
            listViewCurrentDirectories.Items.Remove(current);
        }

        private void RemoveAllCurrecntDirectoriesListView()
        {
            listViewCurrentDirectories.Items.Clear();
        }
        #endregion

        #region Currecnt Directories Click Right
        private void renameCurrentDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RenameFolderForm form = new RenameFolderForm(listViewCurrentDirectories.SelectedItems[0].Text);
            var r = form.ShowDialog();

            if (r == DialogResult.OK)
            {
                DirectoryRename directoryRename = new DirectoryRename();
                directoryRename.OldName = listViewCurrentDirectories.SelectedItems[0].Text; ;
                directoryRename.NewName = form.textBoxNewName.Text;

                AddToRenamedDirectoryList(directoryRename);
                AddToRenamedDirectoryListView(directoryRename.NewName);
                RemoveFromCurrentDirectoriesListView(directoryRename.OldName);
            }
        }

        private void deleteFromCurrentDirectoryToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ListViewItem selectedDeleteListVieItem = listViewCurrentDirectories.SelectedItems[0];
            selectedDeleteListVieItem.ForeColor = Color.Red;
            AddToDeleteDirectoriesList(selectedDeleteListVieItem.Text);
        }

        private void undoDeleteFromCurrentFirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewItem selectedDeleteListVieItem = listViewCurrentDirectories.SelectedItems[0];
            selectedDeleteListVieItem.ForeColor = Color.Black;
            RemoveFromDeleteDirectoriesList(selectedDeleteListVieItem.Text);
        }

        private void listViewCurrentDirectories_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (listViewCurrentDirectories.SelectedItems.Count > 0)
                {
                    if (listViewCurrentDirectories.SelectedItems[0].ForeColor == Color.Red)
                    {
                        renameToolStripMenuItem.Enabled = false;
                        EnableVisibilityDeleteOnCurrecntDirectoriesListView(false);
                    }
                    else
                    {
                        renameToolStripMenuItem.Enabled = true;
                        EnableVisibilityDeleteOnCurrecntDirectoriesListView(true);
                    }
                }
                else
                {
                    renameToolStripMenuItem.Enabled = false;
                    deleteToolStripMenuItem1.Enabled = false;
                    EnableVisibilityDeleteOnCurrecntDirectoriesListView(true);
                }
            }
        }
        #endregion


        #region Delete Directory List Methods
        private void AddToDeleteDirectoriesList(string names)
        {
            deleteDirectories.Add(names);
            CheckApplyButtonEnability();
        }

        private int FindIndexInDeleteDirectoriesList(string name)
        {
            int index = deleteDirectories.FindIndex(dir => dir == name);
            return index;
        }

        private void RemoveFromDeleteDirectoriesList(string name)
        {
            int index = FindIndexInDeleteDirectoriesList(name);
            deleteDirectories.RemoveAt(index);
            CheckApplyButtonEnability();
        }
        #endregion


        #region New Directories List View Methods
        private void AddToNewDirectoriesListView(string[] names)
        {
            ListViewItem[] news = ConvertToListViewItem(names);
            listViewNewDirectories.Items.AddRange(news);
        }

        private void RemoveFromNewDirectoriesListView(string name)
        {
            ListViewItem @new = listViewNewDirectories.FindItemWithText(name);
            listViewNewDirectories.Items.Remove(@new);
        }
        #endregion

        #region New Directory List Methods
        private void AddToNewDirectoriesList(string[] names)
        {
            newDirectories.AddRange(names);
            CheckApplyButtonEnability();
        }

        private int FindIndexInNewDirectoriesList(string name)
        {
            int index = newDirectories.FindIndex(dir => dir == name);
            return index;
        }

        private void RemoveFromNewDirectoriesList(string name)
        {
            int index = FindIndexInNewDirectoriesList(name);
            newDirectories.RemoveAt(index);
            CheckApplyButtonEnability();
        }
        #endregion

        #region New Directories Click Right
        private void deleteFromNewDirectoriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string selectedNewNameListView = listViewNewDirectories.SelectedItems[0].Text;

            int index = FindIndexInNewDirectoriesList(selectedNewNameListView);
            if (index == -1)
            {
                MessageBoxDirectoryDoesntExist();
                return;
            }

            RemoveFromNewDirectoriesList(selectedNewNameListView);
            RemoveFromNewDirectoriesListView(selectedNewNameListView);
        }
        #endregion


        #region Rename Directories List View Methods
        private void AddToRenamedDirectoryListView(string name)
        {
            listViewRenamedDirectories.Items.Add(name);
        }

        private void RemoveFromRenamedDirectoriesListView(string name)
        {
            ListViewItem renamed = listViewRenamedDirectories.FindItemWithText(name);
            listViewRenamedDirectories.Items.Remove(renamed);
        }
        #endregion

        #region Rename Directory List Methods
        private void AddToRenamedDirectoryList(DirectoryRename name)
        {
            renamedDirectories.Add(name);
            CheckApplyButtonEnability();
        }

        private int FindIndexInRenamedDirectoriesList(string name)
        {
            int index = renamedDirectories.FindIndex(x => x.NewName == name);
            return index;
        }

        private void RemoveFromRenamedDirectoriesList(string name)
        {
            int index = FindIndexInRenamedDirectoriesList(name);
            renamedDirectories.RemoveAt(index);
            CheckApplyButtonEnability();
        }
        #endregion

        #region Rename Directories Click Right
        private void undoRenameRenamedDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string selectedRenamedListViewName = listViewRenamedDirectories.SelectedItems[0].Text;

            int index = FindIndexInRenamedDirectoriesList(selectedRenamedListViewName);
            if (index == -1)
            {
                MessageBoxDirectoryDoesntExist();
                return;
            }


            DirectoryRename directoryRename = renamedDirectories[index];
            RemoveFromRenamedDirectoriesList(selectedRenamedListViewName);
            RemoveFromRenamedDirectoriesListView(selectedRenamedListViewName);
            AddToCurrentDirectoriesListView(directoryRename.OldName);
        }
        #endregion


        #region Buttons
        private void buttonLoad_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                string[] directories = DirectoriesLoad(GetDirectoryPath());
                RemoveAllCurrecntDirectoriesListView();
                AddToCurrentDirectoriesListView(directories.Select(dir => Path.GetFileName(dir)).ToArray());
            }
        }

        private void buttonAddNewFolderCurrenctDirectory_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(GetDirectoryPath()))
            {
                MessageBoxDirectoryPathIsEmpty();
                return;
            }

            NewFoldersForm form = new NewFoldersForm();
            var r = form.ShowDialog();

            if (r == DialogResult.OK)
            {
                string[] directories = form.richTextBoxNewFolders.Lines;
                
                var existedDirectories = directories.Where(dir => DoesDirectoryExist(GetDirectoryPath(), dir));
                if (existedDirectories.Count() > 0)
                {
                    MessageBoxDirectoryAlreadyExists(existedDirectories);
                    directories = directories.Where(dir => !existedDirectories.Contains(dir)).ToArray();
                }

                AddToNewDirectoriesList(directories);
                AddToNewDirectoriesListView(directories);
            }
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            if (deleteDirectories.Any(name => IsDirectoryOpen(GetDirectoryPath(), name)))
            {
                MessageBoxDirectoryIsOpen();
                return;
            }
            DirectoriesDelete(GetDirectoryPath(), deleteDirectories);

            DirectoriesCreate(GetDirectoryPath(), newDirectories);

            if (renamedDirectories.Any(dir => IsDirectoryOpen(GetDirectoryPath(), dir.OldName)))
            {
                MessageBoxDirectoryIsOpen();
                return;
            }
            DirectoriesRename(GetDirectoryPath(), renamedDirectories);
         
            MessageBox.Show("Changes has been applied",
                            "Operation was successful",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        
    }
}
