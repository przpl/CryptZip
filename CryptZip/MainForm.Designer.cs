namespace CryptZip
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.chooseFileButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.pathLabel = new System.Windows.Forms.Label();
            this.optionsGroupBox = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.keyTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.modesComboBox = new System.Windows.Forms.ComboBox();
            this.encryptComboBox = new System.Windows.Forms.ComboBox();
            this.compressComboBox = new System.Windows.Forms.ComboBox();
            this.encryptCheckBox = new System.Windows.Forms.CheckBox();
            this.compressCheckBox = new System.Windows.Forms.CheckBox();
            this.processingGroupBox = new System.Windows.Forms.GroupBox();
            this.statusLabel = new System.Windows.Forms.Label();
            this.processButton = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.fileGroupBox = new System.Windows.Forms.GroupBox();
            this.modeLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.optionsGroupBox.SuspendLayout();
            this.processingGroupBox.SuspendLayout();
            this.fileGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // chooseFileButton
            // 
            this.chooseFileButton.Location = new System.Drawing.Point(300, 37);
            this.chooseFileButton.Name = "chooseFileButton";
            this.chooseFileButton.Size = new System.Drawing.Size(75, 23);
            this.chooseFileButton.TabIndex = 0;
            this.chooseFileButton.Text = "Choose file";
            this.chooseFileButton.UseVisualStyleBackColor = true;
            this.chooseFileButton.Click += new System.EventHandler(this.chooseFileButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Path:";
            // 
            // pathLabel
            // 
            this.pathLabel.AutoSize = true;
            this.pathLabel.Location = new System.Drawing.Point(36, 16);
            this.pathLabel.Name = "pathLabel";
            this.pathLabel.Size = new System.Drawing.Size(33, 13);
            this.pathLabel.TabIndex = 5;
            this.pathLabel.Text = "None";
            // 
            // optionsGroupBox
            // 
            this.optionsGroupBox.Controls.Add(this.label4);
            this.optionsGroupBox.Controls.Add(this.keyTextBox);
            this.optionsGroupBox.Controls.Add(this.label1);
            this.optionsGroupBox.Controls.Add(this.modesComboBox);
            this.optionsGroupBox.Controls.Add(this.encryptComboBox);
            this.optionsGroupBox.Controls.Add(this.compressComboBox);
            this.optionsGroupBox.Controls.Add(this.encryptCheckBox);
            this.optionsGroupBox.Controls.Add(this.compressCheckBox);
            this.optionsGroupBox.Location = new System.Drawing.Point(12, 85);
            this.optionsGroupBox.Name = "optionsGroupBox";
            this.optionsGroupBox.Size = new System.Drawing.Size(381, 106);
            this.optionsGroupBox.TabIndex = 6;
            this.optionsGroupBox.TabStop = false;
            this.optionsGroupBox.Text = "Options";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Encryption key:";
            // 
            // keyTextBox
            // 
            this.keyTextBox.Location = new System.Drawing.Point(9, 75);
            this.keyTextBox.MaxLength = 32;
            this.keyTextBox.Name = "keyTextBox";
            this.keyTextBox.PasswordChar = '*';
            this.keyTextBox.Size = new System.Drawing.Size(366, 20);
            this.keyTextBox.TabIndex = 9;
            this.keyTextBox.TextChanged += new System.EventHandler(this.keyTextBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(224, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Mode:";
            // 
            // modesComboBox
            // 
            this.modesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.modesComboBox.FormattingEnabled = true;
            this.modesComboBox.Location = new System.Drawing.Point(264, 48);
            this.modesComboBox.Name = "modesComboBox";
            this.modesComboBox.Size = new System.Drawing.Size(109, 21);
            this.modesComboBox.TabIndex = 4;
            // 
            // encryptComboBox
            // 
            this.encryptComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.encryptComboBox.FormattingEnabled = true;
            this.encryptComboBox.Location = new System.Drawing.Point(264, 21);
            this.encryptComboBox.Name = "encryptComboBox";
            this.encryptComboBox.Size = new System.Drawing.Size(109, 21);
            this.encryptComboBox.TabIndex = 3;
            // 
            // compressComboBox
            // 
            this.compressComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.compressComboBox.FormattingEnabled = true;
            this.compressComboBox.Location = new System.Drawing.Point(79, 21);
            this.compressComboBox.Name = "compressComboBox";
            this.compressComboBox.Size = new System.Drawing.Size(109, 21);
            this.compressComboBox.TabIndex = 2;
            // 
            // encryptCheckBox
            // 
            this.encryptCheckBox.AutoSize = true;
            this.encryptCheckBox.Checked = true;
            this.encryptCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.encryptCheckBox.Location = new System.Drawing.Point(202, 23);
            this.encryptCheckBox.Name = "encryptCheckBox";
            this.encryptCheckBox.Size = new System.Drawing.Size(62, 17);
            this.encryptCheckBox.TabIndex = 1;
            this.encryptCheckBox.Text = "Encrypt";
            this.encryptCheckBox.UseVisualStyleBackColor = true;
            this.encryptCheckBox.CheckedChanged += new System.EventHandler(this.encryptCheckBox_CheckedChanged);
            // 
            // compressCheckBox
            // 
            this.compressCheckBox.AutoSize = true;
            this.compressCheckBox.Checked = true;
            this.compressCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.compressCheckBox.Location = new System.Drawing.Point(8, 23);
            this.compressCheckBox.Name = "compressCheckBox";
            this.compressCheckBox.Size = new System.Drawing.Size(72, 17);
            this.compressCheckBox.TabIndex = 0;
            this.compressCheckBox.Text = "Compress";
            this.compressCheckBox.UseVisualStyleBackColor = true;
            this.compressCheckBox.CheckedChanged += new System.EventHandler(this.compressCheckBox_CheckedChanged);
            // 
            // processingGroupBox
            // 
            this.processingGroupBox.Controls.Add(this.statusLabel);
            this.processingGroupBox.Controls.Add(this.processButton);
            this.processingGroupBox.Controls.Add(this.progressBar);
            this.processingGroupBox.Location = new System.Drawing.Point(12, 197);
            this.processingGroupBox.Name = "processingGroupBox";
            this.processingGroupBox.Size = new System.Drawing.Size(381, 80);
            this.processingGroupBox.TabIndex = 7;
            this.processingGroupBox.TabStop = false;
            this.processingGroupBox.Text = "Processing";
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(8, 53);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(24, 13);
            this.statusLabel.TabIndex = 2;
            this.statusLabel.Text = "Idle";
            // 
            // processButton
            // 
            this.processButton.Enabled = false;
            this.processButton.Location = new System.Drawing.Point(301, 48);
            this.processButton.Name = "processButton";
            this.processButton.Size = new System.Drawing.Size(75, 23);
            this.processButton.TabIndex = 1;
            this.processButton.Text = "Process";
            this.processButton.UseVisualStyleBackColor = true;
            this.processButton.Click += new System.EventHandler(this.processButton_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(7, 19);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(369, 23);
            this.progressBar.TabIndex = 0;
            // 
            // fileGroupBox
            // 
            this.fileGroupBox.Controls.Add(this.modeLabel);
            this.fileGroupBox.Controls.Add(this.label3);
            this.fileGroupBox.Controls.Add(this.label2);
            this.fileGroupBox.Controls.Add(this.pathLabel);
            this.fileGroupBox.Controls.Add(this.chooseFileButton);
            this.fileGroupBox.Location = new System.Drawing.Point(12, 12);
            this.fileGroupBox.Name = "fileGroupBox";
            this.fileGroupBox.Size = new System.Drawing.Size(381, 67);
            this.fileGroupBox.TabIndex = 8;
            this.fileGroupBox.TabStop = false;
            this.fileGroupBox.Text = "Files";
            // 
            // modeLabel
            // 
            this.modeLabel.AutoSize = true;
            this.modeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.modeLabel.Location = new System.Drawing.Point(41, 37);
            this.modeLabel.Name = "modeLabel";
            this.modeLabel.Size = new System.Drawing.Size(36, 13);
            this.modeLabel.TabIndex = 7;
            this.modeLabel.Text = "Pack";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Mode:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(405, 286);
            this.Controls.Add(this.fileGroupBox);
            this.Controls.Add(this.processingGroupBox);
            this.Controls.Add(this.optionsGroupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "CryptZip 1.0";
            this.optionsGroupBox.ResumeLayout(false);
            this.optionsGroupBox.PerformLayout();
            this.processingGroupBox.ResumeLayout(false);
            this.processingGroupBox.PerformLayout();
            this.fileGroupBox.ResumeLayout(false);
            this.fileGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button chooseFileButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label pathLabel;
        private System.Windows.Forms.GroupBox optionsGroupBox;
        private System.Windows.Forms.ComboBox encryptComboBox;
        private System.Windows.Forms.ComboBox compressComboBox;
        private System.Windows.Forms.CheckBox encryptCheckBox;
        private System.Windows.Forms.CheckBox compressCheckBox;
        private System.Windows.Forms.GroupBox processingGroupBox;
        private System.Windows.Forms.Button processButton;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.GroupBox fileGroupBox;
        private System.Windows.Forms.Label modeLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox modesComboBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox keyTextBox;
        private System.Windows.Forms.Label statusLabel;
    }
}

