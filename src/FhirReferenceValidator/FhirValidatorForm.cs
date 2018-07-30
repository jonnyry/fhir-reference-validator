using FhirReferenceValidator.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FhirReferenceValidator
{
    public partial class FhirValidatorForm : Form
    {
        public FhirValidatorForm()
        {
            InitializeComponent();
        }

        private void btnValidateReferences_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbProfileDirectory.Text))
            {
                MessageBox.Show(this, "Please choose a profile directory to validate.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Directory.Exists(tbProfileDirectory.Text))
            {
                MessageBox.Show(this, "The profile directory could not be found.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                FhirProfileReferenceValidator validator = new FhirProfileReferenceValidator();
                validator.ValidateReferences(tbProfileDirectory.Text, LogMessage);
            }
            catch (Exception ex)
            {
                LogMessage("");
                LogMessage("Exception occurred:");
                LogMessage("");
                tbOutput.AppendText(ex.ToString() + Environment.NewLine);
                LogMessage("");
                LogMessage("Halting.");
            }
        }

        private void LogMessage(string line)
        {
            tbOutput.AppendText(line + Environment.NewLine);
        }

        private void btnChooseDirectory_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fileDialog = new FolderBrowserDialog();

            if (fileDialog.ShowDialog() == DialogResult.OK)
                this.tbProfileDirectory.Text = fileDialog.SelectedPath;
        }

        private void FhirValidatorForm_Load(object sender, EventArgs e)
        {
            tbProfileDirectory.Text = Properties.Settings.Default.ChosenFolder;
        }

        private void FhirValidatorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.ChosenFolder = tbProfileDirectory.Text;
            Properties.Settings.Default.Save();
        }

        private void viewHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, "Validates FHIR profiles within a directory (and subdirectories) are fully self referencing." + Environment.NewLine +
                "" + Environment.NewLine +
                "Note:  Currently supports validation of profiles in XML format only.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
