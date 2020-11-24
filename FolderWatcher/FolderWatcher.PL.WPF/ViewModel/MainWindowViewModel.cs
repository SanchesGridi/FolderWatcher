using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using FolderWatcher.PL.WPF.Infrastructure;
using FolderWatcher.PL.WPF.Model;
using FolderWatcher.PL.WPF.ViewModel.Base;

namespace FolderWatcher.PL.WPF.ViewModel
{
    /// <summary>
    /// Сделать обработку ошибок по доступу в сервисе DirectoryService
    /// </summary>
    internal class MainWindowViewModel : BaseViewModel
    {
        #region Fields
        private ObservableCollection<string> _logs;
        private bool _loadFlag;
        private bool _watcherFlag;
        private string _current_path;
        private DirectoryModel _directory;
        #endregion

        #region Properties
        private CancellationTokenSource Source { get; set; }
        private CancellationToken Token { get; set; }

        public ObservableCollection<string> Logs
        {
            get { return _logs ?? (_logs = new ObservableCollection<string>()); }
            set { _logs = value; base.OnPropertyChanged(); }
        }
        public bool LoadFlag
        {
            get { return _loadFlag; }
            set { _loadFlag = value; base.OnPropertyChanged(); }
        }
        public bool WatcherFlag
        {
            get { return _watcherFlag; }
            set { _watcherFlag = value; base.OnPropertyChanged(); }
        }
        public string CurrentPath
        {
            get { return _current_path; }
            set { _current_path = value; base.OnPropertyChanged(); }
        }
        public DirectoryModel Directory
        {
            get { return _directory; }
            set { _directory = value; base.OnPropertyChanged(); }
        }
        public IEnumerable<DirectoryModel> SelfDirectory
        {
            get { yield return Directory; }
            set { Directory = value.SingleOrDefault(); }
        }

        public ICommand LoadCommand { get; set; }
        public ICommand LungeCommand { get; set; }
        public ICommand StartWatchCommand { get; set; }
        public ICommand ClearStopCommand { get; set; }
        public ICommand ClearConsoleCommand { get; set; }
        #endregion

        #region Ctor
        public MainWindowViewModel()
        {
            LoadFlag = true;

            Source = new CancellationTokenSource();
            Token = Source.Token;

            LoadCommand = new RelayCommand(this.LoadCommandExecute);
            LungeCommand = new RelayCommand(this.LungeCommandExecute, this.LungeCommandCanExecute);
            StartWatchCommand = new RelayCommand(this.StartWatchCommandExecute, this.StartWatchCommandCanExecute);
            ClearStopCommand = new RelayCommand(this.ClearStopCommandExecute, this.ClearStopCommandCanExecute);
            ClearConsoleCommand = new RelayCommand(this.ClearConsoleCommandExecute);

            Watcher.ReturnInfo += (os, ea) => Application.Current.Dispatcher.Invoke(() => Logs.Add(ea.Information));
        }
        #endregion

        #region Commands - Execute / CanExecute
        private async void LoadCommandExecute(object obj)
        {
            if (Dialog.OpenFolder())
            {
                CurrentPath = Dialog.DirectoryPath;

                var transfer_directory = await DirectoryService.GetPartDirectory(Dialog.DirectoryPath);
                await Application.Current.Dispatcher.Invoke(async () => Directory = await ConversionService.ConvertToModel(transfer_directory));
                base.OnPropertyChanged(nameof(SelfDirectory));

                LoadFlag = false;
                WatcherFlag = true;
            }
        }

        private async void LungeCommandExecute(object obj) 
        {
            var node = obj as DirectoryModel;

            node.IsExpanded = true;

            var transfer_directory = await ConversionService.ConvertToTransfer(node);

            var childs = await DirectoryService.AddDirectoriesToRoot(transfer_directory);

            await Application.Current.Dispatcher.Invoke(async () =>
            {
                foreach (var child in childs)
                {
                    var directory = await ConversionService.ConvertToModel(child);

                    node.Directories.Add(directory);
                }
            });
            base.OnPropertyChanged(nameof(SelfDirectory));

            node.IsExpanded = false;
        }
        private bool LungeCommandCanExecute(object obj)
        {
            if (obj is DirectoryModel node)
            {
                var added_dirs = node.Directories.Select(n => n.FullName).ToList();
                if (added_dirs.Count == 0 && DirectoryService.HaveNextDirectories(node.FullName))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private void ClearStopCommandExecute(object obj)
        {
            Source.Cancel();

            Application.Current.Dispatcher.Invoke(() => Directory = null);
            base.OnPropertyChanged(nameof(SelfDirectory));

            LoadFlag = true;
            WatcherFlag = false;
        }
        private bool ClearStopCommandCanExecute(object obj) => Directory != null;

        private void StartWatchCommandExecute(object obj)
        {
            var node = obj as DirectoryModel;

            this.ReinitializeToken();

            Task.Run(async () =>
            {
                while (true)
                {
                    if (Token.IsCancellationRequested)
                    {
                        return;
                    }

                    if (!DirectoryService.CheckDirectory(node.FullName))
                    {
                        if (Messenger.Go("Root folder was changed or deleted, clear the tree to load a new directory?",
                            "Directory not found",
                            MessageBoxButton.OKCancel,
                            MessageBoxImage.Question, MessageBoxResult.OK) == MessageBoxResult.OK) { 

                            Application.Current.Dispatcher.Invoke(() => Directory = null);
                            base.OnPropertyChanged(nameof(SelfDirectory));

                            LoadFlag = true;
                            WatcherFlag = false;
                        }
                        else
                        {
                            Messenger.Go("чтобы  загрузить новую директория - очистите дерево", "Message");
                        }

                        return;
                    }

                    var transfer_directory = await DirectoryService.GetDirectory(node.FullName);
                    var model_directory = await ConversionService.ConvertToModel(transfer_directory);

                    Application.Current.Dispatcher.Invoke(() => Watcher.Check(node, model_directory));
                }
            }, Token);
        }
        private bool StartWatchCommandCanExecute(object obj)
        {
            if (obj is DirectoryModel node)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void ClearConsoleCommandExecute(object obj)
        {
            Logs.Clear();
        }
        #endregion

        #region Private Methods
        private void ReinitializeToken()
        {
            Source = new CancellationTokenSource();
            Token = Source.Token;
        }
        #endregion
    }
}