﻿namespace Statistics_Calculator
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
            this.fileGroupBox = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pathLabel = new System.Windows.Forms.Label();
            this.chooseFileButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.redundancyLabel = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.maxEntropyLabel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.entropyLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.totalCharCountLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.charCountLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.histogramCheckBox = new System.Windows.Forms.CheckBox();
            this.calculateButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.saveToClipboardButton = new System.Windows.Forms.Button();
            this.roundUpCheckBox = new System.Windows.Forms.CheckBox();
            this.fileGroupBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // fileGroupBox
            // 
            this.fileGroupBox.Controls.Add(this.label2);
            this.fileGroupBox.Controls.Add(this.pathLabel);
            this.fileGroupBox.Controls.Add(this.chooseFileButton);
            this.fileGroupBox.Location = new System.Drawing.Point(12, 12);
            this.fileGroupBox.Name = "fileGroupBox";
            this.fileGroupBox.Size = new System.Drawing.Size(381, 67);
            this.fileGroupBox.TabIndex = 9;
            this.fileGroupBox.TabStop = false;
            this.fileGroupBox.Text = "Files";
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
            // chooseFileButton
            // 
            this.chooseFileButton.Location = new System.Drawing.Point(6, 34);
            this.chooseFileButton.Name = "chooseFileButton";
            this.chooseFileButton.Size = new System.Drawing.Size(75, 23);
            this.chooseFileButton.TabIndex = 0;
            this.chooseFileButton.Text = "Choose file";
            this.chooseFileButton.UseVisualStyleBackColor = true;
            this.chooseFileButton.Click += new System.EventHandler(this.chooseFileButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.roundUpCheckBox);
            this.groupBox1.Controls.Add(this.redundancyLabel);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.maxEntropyLabel);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.entropyLabel);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.totalCharCountLabel);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.charCountLabel);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 85);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(381, 164);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Statistics";
            // 
            // redundancyLabel
            // 
            this.redundancyLabel.AutoSize = true;
            this.redundancyLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.redundancyLabel.Location = new System.Drawing.Point(97, 110);
            this.redundancyLabel.Name = "redundancyLabel";
            this.redundancyLabel.Size = new System.Drawing.Size(16, 17);
            this.redundancyLabel.TabIndex = 10;
            this.redundancyLabel.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label7.Location = new System.Drawing.Point(9, 110);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(92, 17);
            this.label7.TabIndex = 9;
            this.label7.Text = "Redundancy:";
            // 
            // maxEntropyLabel
            // 
            this.maxEntropyLabel.AutoSize = true;
            this.maxEntropyLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.maxEntropyLabel.Location = new System.Drawing.Point(126, 88);
            this.maxEntropyLabel.Name = "maxEntropyLabel";
            this.maxEntropyLabel.Size = new System.Drawing.Size(16, 17);
            this.maxEntropyLabel.TabIndex = 8;
            this.maxEntropyLabel.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label6.Location = new System.Drawing.Point(9, 88);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(122, 17);
            this.label6.TabIndex = 7;
            this.label6.Text = "Maximum entropy:";
            // 
            // entropyLabel
            // 
            this.entropyLabel.AutoSize = true;
            this.entropyLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.entropyLabel.Location = new System.Drawing.Point(65, 66);
            this.entropyLabel.Name = "entropyLabel";
            this.entropyLabel.Size = new System.Drawing.Size(16, 17);
            this.entropyLabel.TabIndex = 6;
            this.entropyLabel.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(9, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 17);
            this.label4.TabIndex = 5;
            this.label4.Text = "Entropy:";
            // 
            // totalCharCountLabel
            // 
            this.totalCharCountLabel.AutoSize = true;
            this.totalCharCountLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.totalCharCountLabel.Location = new System.Drawing.Point(120, 22);
            this.totalCharCountLabel.Name = "totalCharCountLabel";
            this.totalCharCountLabel.Size = new System.Drawing.Size(16, 17);
            this.totalCharCountLabel.TabIndex = 4;
            this.totalCharCountLabel.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(9, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "Total characters:";
            // 
            // charCountLabel
            // 
            this.charCountLabel.AutoSize = true;
            this.charCountLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.charCountLabel.Location = new System.Drawing.Point(133, 44);
            this.charCountLabel.Name = "charCountLabel";
            this.charCountLabel.Size = new System.Drawing.Size(16, 17);
            this.charCountLabel.TabIndex = 2;
            this.charCountLabel.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(9, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Unique characters:";
            // 
            // histogramCheckBox
            // 
            this.histogramCheckBox.AutoSize = true;
            this.histogramCheckBox.Location = new System.Drawing.Point(6, 19);
            this.histogramCheckBox.Name = "histogramCheckBox";
            this.histogramCheckBox.Size = new System.Drawing.Size(145, 17);
            this.histogramCheckBox.TabIndex = 7;
            this.histogramCheckBox.Text = "Generate and save to file";
            this.histogramCheckBox.UseVisualStyleBackColor = true;
            // 
            // calculateButton
            // 
            this.calculateButton.Location = new System.Drawing.Point(312, 311);
            this.calculateButton.Name = "calculateButton";
            this.calculateButton.Size = new System.Drawing.Size(75, 23);
            this.calculateButton.TabIndex = 0;
            this.calculateButton.Text = "Calculate";
            this.calculateButton.UseVisualStyleBackColor = true;
            this.calculateButton.Click += new System.EventHandler(this.calculateButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.histogramCheckBox);
            this.groupBox2.Controls.Add(this.saveToClipboardButton);
            this.groupBox2.Location = new System.Drawing.Point(12, 255);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(375, 50);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Histogram";
            // 
            // saveToClipboardButton
            // 
            this.saveToClipboardButton.Location = new System.Drawing.Point(255, 19);
            this.saveToClipboardButton.Name = "saveToClipboardButton";
            this.saveToClipboardButton.Size = new System.Drawing.Size(114, 23);
            this.saveToClipboardButton.TabIndex = 8;
            this.saveToClipboardButton.Text = "Copy to clipboard";
            this.saveToClipboardButton.UseVisualStyleBackColor = true;
            this.saveToClipboardButton.Click += new System.EventHandler(this.saveToClipboardButton_Click);
            // 
            // roundUpCheckBox
            // 
            this.roundUpCheckBox.AutoSize = true;
            this.roundUpCheckBox.Checked = true;
            this.roundUpCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.roundUpCheckBox.Location = new System.Drawing.Point(12, 135);
            this.roundUpCheckBox.Name = "roundUpCheckBox";
            this.roundUpCheckBox.Size = new System.Drawing.Size(106, 17);
            this.roundUpCheckBox.TabIndex = 9;
            this.roundUpCheckBox.Text = "Round up results";
            this.roundUpCheckBox.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 344);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.fileGroupBox);
            this.Controls.Add(this.calculateButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Statistics Calculator";
            this.fileGroupBox.ResumeLayout(false);
            this.fileGroupBox.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox fileGroupBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label pathLabel;
        private System.Windows.Forms.Button chooseFileButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label charCountLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button calculateButton;
        private System.Windows.Forms.Label entropyLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label totalCharCountLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox histogramCheckBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button saveToClipboardButton;
        private System.Windows.Forms.Label redundancyLabel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label maxEntropyLabel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox roundUpCheckBox;
    }
}

