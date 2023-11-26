using System.ComponentModel;
using System.Drawing;

namespace BF2Updater
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtInputDirectory = new TextBox();
            txtDestinationFolder = new TextBox();
            btnProcess = new Button();
            btnBrowseInput = new Button();
            btnBrowseDestination = new Button();
            perFileProgressBar = new ProgressBar();
            overallProgressBar = new ProgressBar();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            backgroundWorker = new BackgroundWorker();
            SuspendLayout();
            // 
            // txtInputDirectory
            // 
            txtInputDirectory.Location = new Point(112, 12);
            txtInputDirectory.Name = "txtInputDirectory";
            txtInputDirectory.Size = new Size(471, 23);
            txtInputDirectory.TabIndex = 0;
            // 
            // txtDestinationFolder
            // 
            txtDestinationFolder.Location = new Point(112, 41);
            txtDestinationFolder.Name = "txtDestinationFolder";
            txtDestinationFolder.Size = new Size(471, 23);
            txtDestinationFolder.TabIndex = 1;
            // 
            // btnProcess
            // 
            btnProcess.Location = new Point(589, 66);
            btnProcess.Name = "btnProcess";
            btnProcess.Size = new Size(75, 56);
            btnProcess.TabIndex = 2;
            btnProcess.Text = "Start";
            btnProcess.UseVisualStyleBackColor = true;
            btnProcess.Click += btnProcess_Click;
            // 
            // btnBrowseInput
            // 
            btnBrowseInput.Location = new Point(589, 11);
            btnBrowseInput.Name = "btnBrowseInput";
            btnBrowseInput.Size = new Size(75, 23);
            btnBrowseInput.TabIndex = 3;
            btnBrowseInput.Text = "Select";
            btnBrowseInput.UseVisualStyleBackColor = true;
            btnBrowseInput.Click += btnBrowseInput_Click;
            // 
            // btnBrowseDestination
            // 
            btnBrowseDestination.Location = new Point(589, 37);
            btnBrowseDestination.Name = "btnBrowseDestination";
            btnBrowseDestination.Size = new Size(75, 23);
            btnBrowseDestination.TabIndex = 4;
            btnBrowseDestination.Text = "Select";
            btnBrowseDestination.UseVisualStyleBackColor = true;
            btnBrowseDestination.Click += btnBrowseDestination_Click;
            // 
            // perFileProgressBar
            // 
            perFileProgressBar.Location = new Point(112, 70);
            perFileProgressBar.Name = "perFileProgressBar";
            perFileProgressBar.Size = new Size(471, 23);
            perFileProgressBar.TabIndex = 5;
            // 
            // overallProgressBar
            // 
            overallProgressBar.Location = new Point(112, 99);
            overallProgressBar.Name = "overallProgressBar";
            overallProgressBar.Size = new Size(471, 23);
            overallProgressBar.TabIndex = 6;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 15);
            label1.Name = "label1";
            label1.Size = new Size(71, 15);
            label1.TabIndex = 7;
            label1.Text = "Input Folder";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 45);
            label2.Name = "label2";
            label2.Size = new Size(81, 15);
            label2.TabIndex = 8;
            label2.Text = "Output Folder";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 73);
            label3.Name = "label3";
            label3.Size = new Size(73, 15);
            label3.TabIndex = 9;
            label3.Text = "File progress";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 103);
            label4.Name = "label4";
            label4.Size = new Size(92, 15);
            label4.TabIndex = 10;
            label4.Text = "Overall progress";
            // 
            // backgroundWorker
            // 
            backgroundWorker.WorkerReportsProgress = true;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(684, 131);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(overallProgressBar);
            Controls.Add(perFileProgressBar);
            Controls.Add(btnBrowseDestination);
            Controls.Add(btnBrowseInput);
            Controls.Add(btnProcess);
            Controls.Add(txtDestinationFolder);
            Controls.Add(txtInputDirectory);
            MaximizeBox = false;
            MaximumSize = new Size(700, 170);
            MinimumSize = new Size(700, 170);
            Name = "MainForm";
            ShowIcon = false;
            Text = "Update Builder";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtInputDirectory;
        private TextBox txtDestinationFolder;
        private Button btnProcess;
        private Button btnBrowseInput;
        private Button btnBrowseDestination;
        private ProgressBar perFileProgressBar;
        private ProgressBar overallProgressBar;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private BackgroundWorker backgroundWorker;
    }
}