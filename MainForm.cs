using System;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;
using System.IO.Compression;
using System.ComponentModel;
using System.Threading.Tasks;

namespace BF2Updater
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Additional setup and initialization can be done here.
        }

        private void btnBrowseInput_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    txtInputDirectory.Text = dialog.SelectedPath;
                }
            }
        }

        private void btnBrowseDestination_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    txtDestinationFolder.Text = dialog.SelectedPath;
                }
            }
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            string inputDirectory = txtInputDirectory.Text;
            string destinationFolder = txtDestinationFolder.Text;

            // Check if the input directory exists
            if (!Directory.Exists(inputDirectory))
            {
                MessageBox.Show("Input directory does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Set the file path for the filelist.txt in the input directory
            string fileListPath = Path.Combine(inputDirectory, "filelist.txt");

            // Get all files in the input directory and its subfolders
            string[] files = Directory.GetFiles(inputDirectory, "*", SearchOption.AllDirectories);

            // Create an empty string to store the file hashes
            string fileHashes = "";

            // Calculate the MD5 hash for each file in parallel
            Parallel.For(0, files.Length, i =>
            {
                string file = files[i];
                string relativePath = file.Substring(inputDirectory.Length + 1);
                byte[] hashBytes;
                using (MD5 md5 = MD5.Create())
                {
                    hashBytes = md5.ComputeHash(File.ReadAllBytes(file));
                }
                string hash = BitConverter.ToString(hashBytes).Replace("-", "");
                fileHashes += hash + " " + relativePath.Replace("\\", "/") + "\r\n";
            });

            // Write the file hashes to filelist.txt
            File.WriteAllText(fileListPath, fileHashes, System.Text.Encoding.ASCII);

            // Check if the destination folder exists
            if (!Directory.Exists(destinationFolder))
            {
                MessageBox.Show("Destination folder does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Disable the button to prevent multiple clicks
            btnProcess.Enabled = false;

            // Initialize progress counters
            int filesProcessed = 0;
            int totalFiles = files.Length;

            // Start the background worker
            backgroundWorker.RunWorkerAsync();

            void ReportProgress()
            {
                // Calculate progress percentages
                int perFileProgress = filesProcessed * 100 / totalFiles;
                int overallProgress = (int)((double)filesProcessed / totalFiles * 100);

                // Report progress
                backgroundWorker.ReportProgress(perFileProgress, overallProgress);
            }

            void LogMessage(string message)
            {
                // Log the message to log.txt
                string logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log.txt");
                string logMessage = $"{DateTime.Now}: {message}";
                File.AppendAllText(logFilePath, logMessage + Environment.NewLine);
            }

            void ArchiveFile(string file)
            {
                try
                {
                    string relativePath = file.Substring(inputDirectory.Length + 1);
                    string archivePath = Path.Combine(destinationFolder, relativePath.Replace("\\", "/") + ".zip");

                    // Create the destination directory for the file in the archive
                    string destinationDirectory = Path.GetDirectoryName(archivePath);
                    Directory.CreateDirectory(destinationDirectory);

                    // Compress the current file to a separate ZIP archive with optimal compression level
                    using (ZipArchive archive = ZipFile.Open(archivePath, ZipArchiveMode.Create))
                    {
                        archive.CreateEntryFromFile(file, Path.GetFileName(file), CompressionLevel.Optimal);
                    }

                    // Increment the filesProcessed counter
                    lock (this)
                    {
                        filesProcessed++;
                    }
                    ReportProgress();
                }
                catch (Exception ex)
                {
                    // Log the error in the log file
                    string logMessage = $"Error occurred during archive creation for {Path.GetFileName(file)}: {ex.Message}";
                    LogMessage(logMessage);
                }
            }

            void CopyFileList()
            {
                // Copy filelist.txt to the destination folder
                string destinationFilelistPath = Path.Combine(destinationFolder, "filelist.txt");
                File.Copy(fileListPath, destinationFilelistPath, true);
            }

            void DeleteFileList()
            {
                // Remove filelist.txt from the input directory
                File.Delete(fileListPath);
            }

            void LogSuccessMessage()
            {
                // Log success message in the log file
                LogMessage("Process completed successfully.");
            }

            // Background worker events
            backgroundWorker.DoWork += (s, args) =>
            {
                // Archive files in parallel
                Parallel.ForEach(files, file =>
                {
                    ArchiveFile(file);
                });
            };

            backgroundWorker.ProgressChanged += (s, args) =>
            {
                // Update the per-file progress bar
                perFileProgressBar.Value = Math.Min(args.ProgressPercentage, perFileProgressBar.Maximum);

                // Update the overall progress bar
                overallProgressBar.Value = (int)args.UserState;
            };

            backgroundWorker.RunWorkerCompleted += (s, args) =>
            {
                // Perform any necessary actions after the background worker completes its task.

                // Enable the button
                btnProcess.Enabled = true;

                // Copy filelist.txt to the destination folder
                CopyFileList();

                // Remove filelist.txt from the input directory
                DeleteFileList();

                // Log success message in the log file
                LogSuccessMessage();
            };

            // Start the background worker
            if (!backgroundWorker.IsBusy)
            {
                backgroundWorker.RunWorkerAsync();
            }
        }
    }
}
