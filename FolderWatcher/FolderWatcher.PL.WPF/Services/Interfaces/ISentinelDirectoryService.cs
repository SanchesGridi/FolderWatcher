using System;
using FolderWatcher.BLL.Services.EventArgs;
using FolderWatcher.PL.WPF.Model;

namespace FolderWatcher.PL.WPF.Services.Interfaces
{
    internal interface ISentinelDirectoryService
    {
        void Check(DirectoryModel tree_directory, DirectoryModel current_directory);
        event EventHandler<DirectoryEventArgs> ReturnInfo;
    }
}