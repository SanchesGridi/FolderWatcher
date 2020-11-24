using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using FolderWatcher.BLL.DTOs;
using FolderWatcher.PL.WPF.Model;
using FolderWatcher.PL.WPF.Services.Interfaces;

namespace FolderWatcher.PL.WPF.Services.Classes
{
    internal class DirectoryConversionService : IModelConversionService<DirectoryModel, TransferDirectory>
    {
        #region Model Directory
        public async Task<DirectoryModel> ConvertToModel(TransferDirectory transfer_directory)
        {
            return await Task.Run(() =>
            {
                var directory_model = new DirectoryModel
                {
                    Name = transfer_directory.Name,
                    FullName = transfer_directory.FullName,
                    TotalSize = transfer_directory.TotalSize
                };

                AddFilesToModel(directory_model, transfer_directory);

                CheckDirectoriesForModel(transfer_directory.Directories, directory_model.Directories);

                return directory_model;
            });
        }
        private void AddFilesToModel(DirectoryModel directory_model, TransferDirectory transfer_directory)
        {
            foreach (var file in transfer_directory.Files)
            {
                FileModel file_model = new FileModel
                {
                    Name = file.Name,
                    FullName = file.FullName,
                    Size = file.Size,
                    Extension = file.Extension
                };
                directory_model.Files.Add(file_model);
            }
        }
        private void AddDirectoryToModel(ObservableCollection<DirectoryModel> directory_models, TransferDirectory transfer_directory)
        {
            directory_models.Add(new DirectoryModel
            {
                Name = transfer_directory.Name,
                FullName = transfer_directory.FullName,
                TotalSize = transfer_directory.TotalSize
            });
        }
        private void CheckDirectoriesForModel(List<TransferDirectory> transfer_directories, ObservableCollection<DirectoryModel> directory_models)
        {
            for (int i = 0; i < transfer_directories.Count; i++)
            {
                AddDirectoryToModel(directory_models, transfer_directories[i]);

                AddFilesToModel(directory_models[i], transfer_directories[i]);

                if (transfer_directories[i].Directories.Count != 0)
                {
                    CheckDirectoriesForModel(transfer_directories[i].Directories, directory_models[i].Directories);
                }
            }
        }
        #endregion

        #region Transfer Directory
        public async Task<TransferDirectory> ConvertToTransfer(DirectoryModel directory_model)
        {
            return await Task.Run(() =>
            {
                var transfer_directory = new TransferDirectory
                {
                    Name = directory_model.Name,
                    FullName = directory_model.FullName,
                    TotalSize = directory_model.TotalSize
                };

                AddFilesToTransfer(transfer_directory, directory_model);

                CheckDirectoriesForTransfer(directory_model.Directories, transfer_directory.Directories);

                return transfer_directory;
            });
        }
        private void AddFilesToTransfer(TransferDirectory transfer_directory, DirectoryModel directory_model)
        {
            foreach (var file in directory_model.Files)
            {
                TransferFile file_transfer = new TransferFile
                {
                    Name = file.Name,
                    FullName = file.FullName,
                    Size = file.Size,
                    Extension = file.Extension
                };
                transfer_directory.Files.Add(file_transfer);
            }
        }
        private void AddDirectoryToTransfer(List<TransferDirectory> transfer_directories, DirectoryModel directory_model)
        {
            transfer_directories.Add(new TransferDirectory
            {
                Name = directory_model.Name,
                FullName = directory_model.FullName,
                TotalSize = directory_model.TotalSize
            });
        }
        private void CheckDirectoriesForTransfer(ObservableCollection<DirectoryModel> directory_models, List<TransferDirectory> transfer_directories)
        {
            for (int i = 0; i < directory_models.Count; i++)
            {
                AddDirectoryToTransfer(transfer_directories, directory_models[i]);

                AddFilesToTransfer(transfer_directories[i], directory_models[i]);

                if (directory_models[i].Directories.Count != 0)
                {
                    CheckDirectoriesForTransfer(directory_models[i].Directories, transfer_directories[i].Directories);
                }                   
            }
        }
        #endregion
    }
}