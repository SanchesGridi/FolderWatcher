using System;
using System.Linq;
using FolderWatcher.BLL.Services.EventArgs;
using FolderWatcher.PL.WPF.Model;
using FolderWatcher.PL.WPF.Services.Interfaces;

namespace FolderWatcher.PL.WPF.Services.Classes
{
    internal class SentinelDirectoryService : ISentinelDirectoryService
    {
        public event EventHandler<DirectoryEventArgs> ReturnInfo;
        protected virtual void OnReturnInfo(string information)
        {
            ReturnInfo?.Invoke(this, new DirectoryEventArgs(information));
        }

        public void Check(DirectoryModel tree_directory, DirectoryModel current_directory)
        {
            this.CheckFiles(tree_directory, current_directory);

            this.CheckDirectories(tree_directory, current_directory);
        }
        private void CheckDirectories(DirectoryModel tree_directory, DirectoryModel current_directory) 
        {
            var old_directories = string.Join($";{Environment.NewLine}", tree_directory.Directories.Select(d => $"папка: {d.Name} размер: {d.TotalSize} байт").ToList());
            var new_directories = string.Join($";{Environment.NewLine}", current_directory.Directories.Select(d => $"папка: {d.Name} размер: {d.TotalSize} байт").ToList());

            if (tree_directory.Directories.Count == current_directory.Directories.Count)
            {
                for (int i = 0; i < current_directory.Directories.Count; i++)
                {
                    this.CheckFiles(tree_directory.Directories[i], current_directory.Directories[i]);
                    if (this.EqualsDirectory(tree_directory.Directories[i], current_directory.Directories[i]))
                    {
                        continue;
                    }
                    else
                    {
                        this.OnReturnInfo($"одна папка была изменена{Environment.NewLine}Старые данные корневой папки:{Environment.NewLine}{old_directories}{Environment.NewLine}Новые данные корневой папки:{Environment.NewLine}{new_directories}");
                        tree_directory.Directories[i] = current_directory.Directories[i];
                    }
                    this.CheckDirectories(tree_directory.Directories[i], current_directory.Directories[i]);
                }
            }
            else
            {
                if (tree_directory.Directories.Count > current_directory.Directories.Count)
                {
                    this.OnReturnInfo($"Удаление папок{Environment.NewLine}Старые данные папки {current_directory.Name}:{Environment.NewLine}{old_directories}{Environment.NewLine}Новые данные папки {current_directory.Name}:{Environment.NewLine}{new_directories}");
                }
                else if(tree_directory.Directories.Count < current_directory.Directories.Count)
                {
                    this.OnReturnInfo($"Добавление папок{Environment.NewLine}Старые данные папки {current_directory.Name}:{Environment.NewLine}{old_directories}{Environment.NewLine}Новые данные папки {current_directory.Name}:{Environment.NewLine}{new_directories}");
                }

                tree_directory.Directories.Clear();

                for (int i = 0; i < current_directory.Directories.Count; i++)
                {
                    tree_directory.Directories.Add(current_directory.Directories[i]);
                }
            }
        }

        private void CheckFiles(DirectoryModel tree_directory, DirectoryModel current_directory)
        {
            var old_files = string.Join($";{Environment.NewLine}", tree_directory.Files.Select(f => $"файл: {f.Name} размер: {f.Size} байт").ToList());
            var new_files = string.Join($";{Environment.NewLine}", current_directory.Files.Select(f => $"файл: {f.Name} размер: {f.Size} байт").ToList());

            if (tree_directory.Files.Count == current_directory.Files.Count) 
            {
                for (int i = 0; i < current_directory.Files.Count; i++)
                {
                    if (this.EqualsFiles(tree_directory.Files[i], current_directory.Files[i]))
                    {
                        continue;
                    }
                    else
                    {
                        this.OnReturnInfo($"один файл изменен{Environment.NewLine}Старые данные папки: {current_directory.Name}:{Environment.NewLine}{old_files}{Environment.NewLine}Новые данные папки {current_directory.Name}:{Environment.NewLine}{new_files}");
                        tree_directory.Files[i] = current_directory.Files[i];
                    }
                }
            }
            else 
            {
                if (tree_directory.Files.Count > current_directory.Files.Count)
                {
                    this.OnReturnInfo($"Удаление файлов{Environment.NewLine}Старые данные папки {current_directory.Name}:{Environment.NewLine}{old_files}{Environment.NewLine}Новые данные папки {current_directory.Name}:{Environment.NewLine}{new_files}");
                }
                else if (tree_directory.Files.Count < current_directory.Files.Count)
                {
                    this.OnReturnInfo($"Добавление файлов{Environment.NewLine}Старые данные папки {current_directory.Name}:{Environment.NewLine}{old_files}{Environment.NewLine}Новые данные папки {current_directory.Name}:{Environment.NewLine}{new_files}");
                }

                tree_directory.Files.Clear();

                for (int i = 0; i < current_directory.Files.Count; i++)
                {
                    tree_directory.Files.Add(current_directory.Files[i]);
                }
            }
        }

        private bool EqualsFiles(FileModel tree_file, FileModel current_file)
        {
            if (tree_file.FullName.Equals(current_file.FullName) && tree_file.Size.Equals(current_file.Size))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool EqualsDirectory(DirectoryModel tree_directory, DirectoryModel current_directory)
        {
            if (tree_directory.FullName.Equals(current_directory.FullName) &&
                tree_directory.TotalSize.Equals(current_directory.TotalSize))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}