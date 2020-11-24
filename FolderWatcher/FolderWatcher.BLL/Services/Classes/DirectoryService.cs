using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using FolderWatcher.BLL.DTOs;
using FolderWatcher.BLL.Services.Base;
using FolderWatcher.BLL.Services.Interfaces;

namespace FolderWatcher.BLL.Services.Classes
{
    internal class DirectoryService : ErrorHistory, IDirectoryService 
    {
        #region Public Methods
        public async Task<TransferDirectory> GetDirectory(string directory_path)
        {
            return await Task.Run(async () =>
            {
                await BuildUserDirectory(await BuildRoot(directory_path), UserDirectory.Directories);

                return UserDirectory;
            });
        }
        public async Task<TransferDirectory> GetPartDirectory(string directory_path)
        {
            DirectoryInfo directory = new DirectoryInfo(directory_path);
            TransferDirectory userDirectory = new TransferDirectory
            {
                Name = directory.Name,
                FullName = directory.FullName,
                TotalSize = await GetSizeDirectory(directory.FullName)
            };

            AddFiles(directory, userDirectory);

            return userDirectory;
        }
        public async Task<List<TransferDirectory>> AddDirectoriesToRoot(TransferDirectory transfer_directory)
        {
            return await Task.Run(async () =>
            {
                DirectoryInfo directory_root = new DirectoryInfo(transfer_directory.FullName);
                foreach (var directory in directory_root.GetDirectories())
                {
                    transfer_directory.Directories.Add(await GetPartDirectory(directory.FullName));
                }

                return transfer_directory.Directories;
            });
        }
        public bool CheckDirectory(string directory_path)
        {
            return Directory.Exists(directory_path);
        }
        public bool HaveNextDirectories(string directory_path)
        {
            DirectoryInfo directory = new DirectoryInfo(directory_path);
            if (directory.GetDirectories().Length != 0) return true;
            else return false;
        }
        #endregion

        #region Private Methods and Properties

        #region Build / Get User Directory
        private TransferDirectory UserDirectory { get; set; }

        private void AddFiles(DirectoryInfo directory, TransferDirectory userDirectory)
        {
            foreach (var file in directory.GetFiles())
            {
                var transferFile = new TransferFile
                {
                    Name = file.Name,
                    FullName = file.FullName,
                    Size = file.Length,
                    Extension = file.Extension
                };
                userDirectory.Files.Add(transferFile);
            }
        }
        private async Task<DirectoryInfo[]> BuildRoot(string directory_path)
        {
            DirectoryInfo directory = new DirectoryInfo(directory_path);
            UserDirectory = new TransferDirectory
            {
                Name = directory.Name,
                FullName = directory.FullName,
                TotalSize = await GetSizeDirectory(directory.FullName)
            };

            AddFiles(directory, UserDirectory);
               
            return directory.GetDirectories(); 
        }
        private async Task BuildUserDirectory(DirectoryInfo[] directories, List<TransferDirectory> user_directories) 
        {
            
            for (int i = 0; i < directories.Count(); i++)
            {
                user_directories.Add(new TransferDirectory
                {
                    Name = directories[i].Name,
                    FullName = directories[i].FullName,
                    TotalSize = await GetSizeDirectory(directories[i].FullName),
                });

                AddFiles(directories[i], user_directories[i]);

                if (directories[i].GetDirectories().Length != 0)
                {
                    await BuildUserDirectory(directories[i].GetDirectories(), user_directories[i].Directories);
                }
            }
        }
        #endregion

        #region GetSize
        private IEnumerable<FileInfo> Files { get; set; }

        private DirectoryInfo[] GetDirectoriesFromRoot(string path)
        {
            DirectoryInfo directory = new DirectoryInfo(path);

            Files = directory.GetFiles();

            return directory.GetDirectories();
        }
        private void CheckDirs(DirectoryInfo[] directories)
        {
            foreach (var item in directories)
            {
                Files = Files.Concat(item.GetFiles());

                if (item.GetDirectories().Length != 0) CheckDirs(item.GetDirectories());
            }
        }
        private async Task<decimal> GetSizeDirectory(string directory)
        {
            return await Task.Run(() =>
            {
                CheckDirs(GetDirectoriesFromRoot(directory));
                return Files.Sum(f => f.Length);
            });
        }
        #endregion

        #endregion
    }
}