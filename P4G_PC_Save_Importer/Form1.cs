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

namespace P4G_PC_Save_Importer
{
    public partial class Form1 : Form
    {

        private bool fileOpened { set; get; }

        public Form1()
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
                Utils.Binslot binslot = new Utils.Binslot($"{baseFinalPath}slot");

                // Calculating new hash
                byte[] newHash = Utils.HashMD5.getMd5BytesHash(File.ReadAllBytes(tbxSourceSaveFile.Text));
                string strHash = Utils.HashMD5.getMd5StringHash(File.ReadAllBytes(tbxSourceSaveFile.Text));

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
            var folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            // Show the FolderBrowserDialog.
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                var folderName = folderBrowserDialog1.SelectedPath;

                string[] slots = Utils.PathChecker.checkPath(folderName);

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
    }
}
