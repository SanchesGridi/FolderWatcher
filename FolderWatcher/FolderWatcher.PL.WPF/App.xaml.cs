using Unity;
using System.Windows;
using CommonServiceLocator;
using Unity.ServiceLocation;
using FolderWatcher.BLL.DTOs;
using FolderWatcher.PL.WPF.Model;
using FolderWatcher.BLL.Services.Classes;
using FolderWatcher.BLL.Services.Interfaces;
using FolderWatcher.PL.WPF.Services.Classes;
using FolderWatcher.PL.WPF.Services.Interfaces;

namespace FolderWatcher.PL.WPF
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var container = new UnityContainer();
            container.RegisterSingleton<IMessageBoxService, MessageBoxService>();
            container.RegisterSingleton<IDialogService, DialogService>();
            container.RegisterSingleton<IDirectoryService, DirectoryService>();
            container.RegisterSingleton<IModelConversionService<DirectoryModel, TransferDirectory>, DirectoryConversionService>();
            container.RegisterSingleton<ISentinelDirectoryService, SentinelDirectoryService>();

            UnityServiceLocator locator = new UnityServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => locator);
        }
    }
}