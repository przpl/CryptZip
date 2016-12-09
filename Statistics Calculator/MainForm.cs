﻿using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace Statistics_Calculator
{
    public partial class MainForm : Form
    {
        private string _filePath;
        private Result _lastResult;

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
            _lastResult = calc.Calculate(fileStream);

            totalCharCountLabel.Text = _lastResult.TotalCount.ToString();
            charCountLabel.Text = _lastResult.UniqueCount.ToString();
            entropyLabel.Text = _lastResult.Entropy.ToString(CultureInfo.InvariantCulture);

            if (histogramCheckBox.Checked)
            {
                var sfd = new SaveFileDialog {Filter = "txt files (*.txt)|*.txt"};

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    WriteHistogramData(sfd.FileName);
                    Process.Start(sfd.FileName);
                }
            }
        }

        private void WriteHistogramData(string path)
        {
            var writer = new StreamWriter(path);

            foreach (var probability in _lastResult.Probabilities)
                writer.WriteLine(probability);

            writer.Close();
        }

        private void saveToClipboardButton_Click(object sender, EventArgs e)
        {
            if (_lastResult == null)
            {
                MessageBox.Show("There is no data.");
                return;
            }

            var probabilities = String.Join(Environment.NewLine, _lastResult.Probabilities);

            Clipboard.SetText(probabilities);
        }
    }
}
