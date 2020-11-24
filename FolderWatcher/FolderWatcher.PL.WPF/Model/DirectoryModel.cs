using System.Collections.ObjectModel;
using FolderWatcher.PL.WPF.Model.Base;

namespace FolderWatcher.PL.WPF.Model
{
    public class DirectoryModel : BaseModel
    {
        private bool _isExpanded;
        private string _name;
        private string _fullName;
        private decimal _totalSize;
        private ObservableCollection<DirectoryModel> _directories;
        private ObservableCollection<FileModel> _files;

        public bool IsExpanded
        {
            get { return _isExpanded; }
            set { _isExpanded = value; base.OnPropertyChanged(); }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; base.OnPropertyChanged(); }
        }
        public string FullName
        {
            get { return _fullName; }
            set { _fullName = value; base.OnPropertyChanged(); }
        }
        public decimal TotalSize
        {
            get { return _totalSize; }
            set { _totalSize = value; base.OnPropertyChanged(); }
        }
        public ObservableCollection<DirectoryModel> Directories
        {
            get { return _directories ?? (_directories = new ObservableCollection<DirectoryModel>()); }
            set { _directories = value;  base.OnPropertyChanged(); } 
        }
        public ObservableCollection<FileModel> Files
        {
            get { return _files ?? (_files = new ObservableCollection<FileModel>()); }
            set { _files = value; base.OnPropertyChanged(); } 
        }
        public string ImagePath { get; set; } = @"/Images/Folder.png";
    }
}