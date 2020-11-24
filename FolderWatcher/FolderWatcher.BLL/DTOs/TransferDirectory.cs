using System.Collections.Generic;

namespace FolderWatcher.BLL.DTOs
{
    internal class TransferDirectory 
    {
        private List<TransferFile> _files;
        private List<TransferDirectory> _directories;

        public List<TransferFile> Files
        {
            get { return _files ?? (_files = new List<TransferFile>()); }
            set { _files = value; }
        }
        public List<TransferDirectory> Directories
        {
            get { return _directories ?? (_directories = new List<TransferDirectory>()); }
            set { _directories = value; }
        }
        public string Name { get; set; }
        public string FullName { get; set; }
        public decimal TotalSize { get; set; }
    }
}