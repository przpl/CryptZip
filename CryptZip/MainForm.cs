using CryptZip.Compression;
using CryptZip.Encryption;
using CryptZip.Encryption.Padding;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CryptZip
{
    public partial class MainForm : Form
    {
        private string _filePath;
        private readonly IPadding _padding = new PKCS7Padding();
        private int _secondsElapsed;

        public MainForm()
        {
            InitializeComponent();
            AddAlgorithms(compressComboBox, new[] {nameof(LZ77), nameof(LZ78), nameof(LZW)});
            AddAlgorithms(encryptComboBox, new[] {nameof(AES), nameof(Serpent), nameof(Twofish)});
            AddAlgorithms(modesComboBox, new[] {nameof(ECB), nameof(CBC)});
        }

        private void AddAlgorithms(ComboBox comboBox, IEnumerable<string> algorithmNames)
        {
            foreach (var algorithmName in algorithmNames)
                comboBox.Items.Add(algorithmName);

            comboBox.Text = algorithmNames.First();
        }

        private void chooseFileButton_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    _filePath = openFileDialog.FileName;
                    modeLabel.Text = Packer.IsPackedFile(_filePath) ? "Unpack" : "Pack";
                }
                else
                    _filePath = null;

                pathLabel.Text = _filePath ?? "None";
            }
        }

        private void processButton_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateUserInput();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (Packer.IsPackedFile(_filePath))
                Unpack();
            else 
                Pack();

            _secondsElapsed = 0;
            timeTimer.Start();
        }

        private void ValidateUserInput()
        {
            if (String.IsNullOrWhiteSpace(_filePath))
                throw new InvalidOperationException("Please choose a file.");

            if (!File.Exists(_filePath))
                throw new InvalidOperationException("File does not exist or has been removed.");

            if (!compressCheckBox.Checked && !encryptCheckBox.Checked && !Packer.IsPackedFile(_filePath))
                throw new InvalidOperationException("Please select at least one action.");

            if ((encryptCheckBox.Checked && !Packer.IsPackedFile(_filePath)) || Packer.IsEncrypted(_filePath))
            {
                if (keyTextBox.Text.Length < 6)
                    throw new InvalidOperationException("Minimum password length is six characters.");
            }
        }

        private void Unpack()
        {
            var header = new FileHeader();
            byte[] keyBytes = Encoding.UTF8.GetBytes(keyTextBox.Text);
            Packer packer = header.GetPacker(_filePath, keyBytes);
            packer.StatusChanged += OnStatusChanged;
            packer.WorkFinished += OnWorkFinished;

            SwitchControls(false);
            packer.UnpackAsync(_filePath);
        }

        private void Pack()
        {
            Packer packer = null;

            if (compressCheckBox.Checked && encryptCheckBox.Checked)
                packer = new FullPacker();
            else if (compressCheckBox.Checked)
                packer = new CompressionPacker();
            else if (encryptCheckBox.Checked)
                packer = new EncryptionPacker();

            packer.Compressor = GetCompressor();
            ICipher cipher = GetCipher();
            packer.Encryptor = GetEncryptor(cipher);
            packer.StatusChanged += OnStatusChanged;
            packer.WorkFinished += OnWorkFinished;

            SwitchControls(false);
            packer.PackAsync(_filePath);
        }

        private void OnStatusChanged(object source, StatusEventArgs e)
        {
            statusLabel.Text = e.Status;
        }

        private void OnWorkFinished(object source, EventArgs e)
        {
            SwitchControls(true);
            timeTimer.Stop();
            
            if (_secondsElapsed > 0)
            {
                string timeInfo = " in ";
                int minutes = _secondsElapsed / 60;
                int seconds = _secondsElapsed % 60;
                if (minutes > 0)
                    timeInfo += minutes + " minutes";
                if (minutes > 0 && seconds > 0)
                    timeInfo += " and ";
                if (seconds > 0)
                    timeInfo += seconds + " seconds";
                statusLabel.Text += timeInfo;
            }
            timeLabel.Text = "00:00";
        }

        private void SwitchControls(bool enabled)
        {
            chooseFileButton.Enabled = enabled;
            compressComboBox.Enabled = enabled;
            compressCheckBox.Enabled = enabled;
            encryptComboBox.Enabled = enabled;
            encryptCheckBox.Enabled = enabled;
            modesComboBox.Enabled = enabled;
            keyTextBox.Enabled = enabled;
            processButton.Enabled = enabled;
            timeLabel.Visible = !enabled;

            Cursor = enabled ? Cursor = DefaultCursor : Cursor = Cursors.WaitCursor;
            progressBar.Style = enabled ? ProgressBarStyle.Blocks : ProgressBarStyle.Marquee;
        }

        private ICipher GetCipher()
        {
            if (!encryptCheckBox.Checked)
                return null;

            byte[] keyBytes = Encoding.UTF8.GetBytes(keyTextBox.Text);
            var key = KeyExtender.Extend(keyBytes, _padding);
            switch (encryptComboBox.Text)
            {
                case nameof(AES):
                    return new AES(key);
                case nameof(Twofish):
                    return new Twofish(key);
                case nameof(Serpent):
                    return new Serpent(key);
                default:
                    return null;
            }
        }

        private IEncryptor GetEncryptor(ICipher cipher)
        {
            if (cipher == null)
                return null;

            switch (modesComboBox.Text)
            {
                case nameof(ECB):
                    return new ECB(cipher, _padding);
                case nameof(CBC):
                    return new CBC(cipher, _padding, IV.GetRandom(cipher.BlockSize));
                default:
                    return null;
            }
        }

        private ICompressor GetCompressor()
        {
            if (!compressCheckBox.Checked)
                return null;

            switch (compressComboBox.Text)
            {
                case nameof(LZ77):
                    return new LZ77();
                case nameof(LZ78):
                    return new LZ78();
                case nameof(LZW):
                    return new LZW();
                default:
                    return null;
            }
        }

        private void timeTimer_Tick(object sender, EventArgs e)
        {
            _secondsElapsed++;
            int minutes = _secondsElapsed/60;
            int seconds = _secondsElapsed%60;
            timeLabel.Text = minutes.ToString("D2") + ":" + seconds.ToString("D2");
        }
    }
}