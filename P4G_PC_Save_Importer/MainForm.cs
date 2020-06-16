using Ookii.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Yoshitsune;
using Yoshitsune.Struct;
using Yoshitsune.Utils;

namespace P4G_PC_Save_Importer
{
    public partial class MainForm : Form
    {
        private bool fileOpened { set; get; }

        public MainForm()
        {
            InitializeComponent();
        }
        
        // Convert button
        private void btnImport_Click(object sender, EventArgs e)
        {
            if (sourcePath.Text == "")
            {
                MessageBox.Show("Select a game save data path first", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (tbxSourceSaveFile.Text == "")
            {
                MessageBox.Show("Select a source save file first", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (cbxSlotsSelect.SelectedItem == null || cbxSlotsSelect.SelectedItem.ToString() == "")
            {
                MessageBox.Show("Select a slot first", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var choice = MessageBox.Show("It will overwrite your save data on your save data path. Are you sure?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (choice == DialogResult.Yes)
            {
                string baseFinalPath = $"{sourcePath.Text}\\data00{cbxSlotsSelect.SelectedItem.ToString()}.bin";

                Debug.WriteLine($"Saving to {baseFinalPath}");

                // Pick the binslot first
                Binslot binslot = new Binslot($"{baseFinalPath}slot");

                // Calculating new hash
                byte[] newHash = HashMD5.getMd5BytesHash(File.ReadAllBytes(tbxSourceSaveFile.Text));
                string strHash = HashMD5.getMd5StringHash(File.ReadAllBytes(tbxSourceSaveFile.Text));

                Debug.WriteLine($"New hash: {strHash}");

                // Storing the newer hash
                binslot.saveFileHash = newHash;

                // Saving the new file binslot
                binslot.saveBinslot();

                // Copying the save into save file path
                File.Copy(tbxSourceSaveFile.Text, baseFinalPath, true);

                MessageBox.Show("Save import finished", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSelectSavePath_onClick(object sender, EventArgs e)
        {
            VistaFolderBrowserDialog dlg = new VistaFolderBrowserDialog();
            dlg.SelectedPath = @"C:\Program Files (x86)\Steam\userdata\";
            dlg.ShowNewFolderButton = true;

            // Show the FolderBrowserDialog.
            DialogResult result = dlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                var folderName = dlg.SelectedPath;

                string[] slots = PathChecker.checkPath(folderName);

                bool valid = false;

                cbxSlotsSelect.Items.Clear();

                foreach (string slot in slots)
                    if (slot != null)
                    {
                        cbxSlotsSelect.Items.Add(slot);
                        valid = true;
                    }

                if (!valid)
                {
                    MessageBox.Show("This doesn't look like a P4G PC save path", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                sourcePath.Text = folderName;
            }
        }

        private void btnSelectSourceSaveFile_onClick(object sender, EventArgs e)
        {
            var fileDialog = new System.Windows.Forms.OpenFileDialog();

            fileDialog.DefaultExt = "bin";
            fileDialog.Filter = "P4G save file (PC/Vita) (*.bin)|*.bin";

            DialogResult result = fileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                string openFileName = fileDialog.FileName;
                try
                {
                    // Output the requested file in richTextBox1.
                    Stream s = fileDialog.OpenFile();
                    s.Close();

                    fileOpened = true;

                    tbxSourceSaveFile.Text = openFileName;
                }
                catch (Exception exp)
                {
                    MessageBox.Show("An error occurred while attempting to load the file. The error is:"
                                    + System.Environment.NewLine + exp.ToString() + System.Environment.NewLine);
                    fileOpened = false;
                }
            }
        }

        /// <summary>
        /// Opens "About" page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutThisToolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutForm().Show();
        }

        /// <summary>
        /// Opens tutorial page on GitHub
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tutorialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Opens README.md for now
            System.Diagnostics.Process.Start("https://github.com/RayFirefist/P4G_PC_Save_Importer/blob/master/README.md");
        }
    }
}
