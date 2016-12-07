using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace Statistics_Calculator
{
    public partial class MainForm : Form
    {
        private string _filePath;

        public MainForm()
        {
            InitializeComponent();
        }

        private void chooseFileButton_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                    _filePath = openFileDialog.FileName;
                else
                    _filePath = null;

                pathLabel.Text = _filePath ?? "None";
            }
        }

        private void calculateButton_Click(object sender, EventArgs e)
        {
            if (_filePath == null || !File.Exists(_filePath))
            {
                MessageBox.Show("Invalid file path");
                return;
            }

            var fileStream = new FileStream(_filePath, FileMode.Open, FileAccess.Read);

            var calc = new Calculator();
            Result result = calc.Calculate(fileStream);

            totalCharCountLabel.Text = result.TotalCount.ToString();
            charCountLabel.Text = result.UniqueCount.ToString();
            entropyLabel.Text = result.Entropy.ToString(CultureInfo.InvariantCulture);

            if (histogramCheckBox.Checked)
            {
                var sfd = new SaveFileDialog();
                sfd.Filter = "txt files (*.txt)|*.txt";

                if (sfd.ShowDialog() == DialogResult.OK)
                    WriteHistogramData(sfd.FileName, result.Probabilities);
            }
        }

        private void WriteHistogramData(string path, IEnumerable<double> probabilities)
        {
            var writer = new StreamWriter(path);

            foreach (var probability in probabilities)
                writer.WriteLine(probability);

            writer.Close();
        }
    }
}
