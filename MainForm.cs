using System;
using System.Windows.Forms;

namespace SyncFolders
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void InputButtonClick(object sender, EventArgs e)
        {
            inputTextBox.Text = SelectFolder();
        }

        private void OutputButtonClick(object sender, EventArgs e)
        {
            outputTextBox.Text = SelectFolder();
        }

        private string SelectFolder()
        {
            var result = folderBrowserDialog.ShowDialog();
            return result == DialogResult.OK ? folderBrowserDialog.SelectedPath : string.Empty;
        }

        private void SyncButtonClick(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(inputTextBox.Text) || string.IsNullOrWhiteSpace(outputTextBox.Text))
                return;

            var dirWatcher = new SyncDir()
            {
                InputDir = inputTextBox.Text, 
                OutputDir = outputTextBox.Text
            };

            dirWatcher.WatchDirectory(realTimeCheckbox.Checked, Convert.ToInt64(numericInputTimer.Value));
        }

        private void RealTimeCheckboxCheckedChanged(object sender, EventArgs e)
        {
            numericInputTimer.Enabled = !numericInputTimer.Enabled;
        }
    }
}
