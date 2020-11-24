using System.Collections.Generic;
using System.Threading.Tasks;
using FolderWatcher.BLL.DTOs;

namespace FolderWatcher.BLL.Services.Interfaces
{
    internal interface IDirectoryService
    {
        Task<TransferDirectory> GetDirectory(string directory_path);
        Task<TransferDirectory> GetPartDirectory(string directory_path);
        Task<List<TransferDirectory>> AddDirectoriesToRoot(TransferDirectory transfer_directory);
        bool CheckDirectory(string directory_path);
        bool HaveNextDirectories(string directory_path);
    }
}