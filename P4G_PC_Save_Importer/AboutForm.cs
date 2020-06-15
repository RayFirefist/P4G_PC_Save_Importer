using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P4G_PC_Save_Importer
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        private void btnGitHub_Click(object sender, EventArgs e)
        {
            // Open GitHub
            System.Diagnostics.Process.Start("https://github.com/RayFirefist/P4G_PC_Save_Importer");
        }

        private void btnTwitter_Click(object sender, EventArgs e)
        {
            // Open Twitter
            System.Diagnostics.Process.Start("https://twitter.com/RayFirefist");
        }
    }
}
