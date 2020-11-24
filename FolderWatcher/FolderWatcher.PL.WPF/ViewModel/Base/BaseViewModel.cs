using CommonServiceLocator;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FolderWatcher.BLL.Services.Interfaces;
using FolderWatcher.PL.WPF.Services.Interfaces;
using FolderWatcher.PL.WPF.Model;
using FolderWatcher.BLL.DTOs;

namespace FolderWatcher.PL.WPF.ViewModel.Base
{
    internal class BaseViewModel : INotifyPropertyChanged
    {
        protected virtual IMessageBoxService Messenger => ServiceLocator.Current.GetInstance<IMessageBoxService>();
        protected virtual IDialogService Dialog => ServiceLocator.Current.GetInstance<IDialogService>();
        protected virtual IDirectoryService DirectoryService => ServiceLocator.Current.GetInstance<IDirectoryService>();
        protected virtual ISentinelDirectoryService Watcher => ServiceLocator.Current.GetInstance<ISentinelDirectoryService>();
        protected virtual IModelConversionService<DirectoryModel, TransferDirectory> ConversionService => ServiceLocator.Current.GetInstance<IModelConversionService<DirectoryModel, TransferDirectory>>();

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}