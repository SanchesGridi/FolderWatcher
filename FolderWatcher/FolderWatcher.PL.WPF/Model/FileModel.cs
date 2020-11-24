using FolderWatcher.PL.WPF.Model.Base;

namespace FolderWatcher.PL.WPF.Model
{
    public class FileModel : BaseModel
    {
        private string _name;
        private string _fullName;
        private string _extension;
        private long _size;

        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }
        public string FullName
        {
            get { return _fullName; }
            set { _fullName = value; OnPropertyChanged(); }
        }
        public string Extension
        {
            get { return _extension; }
            set { _extension = value; OnPropertyChanged(); }
        }
        public long Size
        {
            get { return _size; }
            set { _size = value; OnPropertyChanged(); }
        }
        public string ImagePath { get; set; } = @"/Images/File.png";
    }
}