using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratorDataView.Client.FileUtility
{
    public class FileWatcher
    {
        private readonly FileSystemWatcher _fileSystemWatcher;
        private readonly Action<string> _onFileCreated;

        public FileWatcher(string path, string filter, Action<string> onFileCreated)
        {
            _onFileCreated = onFileCreated;
            _fileSystemWatcher = new FileSystemWatcher
            {
                Path = path,
                Filter = filter,
                NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite
            };

            _fileSystemWatcher.Created += OnCreated;
            _fileSystemWatcher.EnableRaisingEvents = true;
        }

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            // Wait for the file to become accessible
            WaitForFile(e.FullPath);

            // Notify observer
            _onFileCreated(e.FullPath);
        }

        private static void WaitForFile(string path)
        {
            while (true)
            {
                try
                {
                    using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None))
                    {
                        break;
                    }
                }
                catch (IOException)
                {
                    // Retry until the file is ready
                    System.Threading.Thread.Sleep(50);
                }
            }
        }
    }
}
