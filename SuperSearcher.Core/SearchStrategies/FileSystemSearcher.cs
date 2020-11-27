using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SuperSearcher.Core.SearchStrategies
{
    public class FileSystemSearcher : ISearcher
    {
        /// <inheritdoc/>
        public async Task<List<string>> Search(string query, int resultsCount)
        {
            return await Task.Run(() => GetFilesAndDirs(query, resultsCount));
        }

        private List<string> GetFilesAndDirs(string searchPattern, int resultsCount)
        {
            List<string> filesAndDirs = new List<string>();

            foreach (var drive in DriveInfo.GetDrives())
            {
                Console.WriteLine($"Searching on drive: {drive.Name}");
                Stack<string> pending = new Stack<string>();

                pending.Push(drive.Name);

                while (pending.Count != 0)
                {
                    var path = pending.Pop();

                    try
                    {
                        string[] foundFiles = Directory.GetFiles(path, $"*{searchPattern}*.*");
                        if (foundFiles != null && foundFiles.Length != 0)
                            foreach (var file in foundFiles)
                            {
                                filesAndDirs.Add(file);
                                if (filesAndDirs.Count >= resultsCount) return filesAndDirs;
                            }
                    }
                    catch { }
                    try
                    {
                        string[] foundDirs = Directory.GetDirectories(path, $"*{searchPattern}*");
                        if (foundDirs != null && foundDirs.Length != 0)
                            foreach (var dir in foundDirs)
                            {
                                filesAndDirs.Add(dir);
                                if (filesAndDirs.Count >= resultsCount) return filesAndDirs;
                            }
                    }
                    catch { }

                    try
                    {
                        var nextDir = Directory.GetDirectories(path);
                        foreach (var subdir in nextDir) pending.Push(subdir);
                    }
                    catch { }
                }
            }

            return filesAndDirs;
        }
    }
}
