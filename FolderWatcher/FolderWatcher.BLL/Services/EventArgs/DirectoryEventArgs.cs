namespace FolderWatcher.BLL.Services.EventArgs
{
    internal class DirectoryEventArgs : System.EventArgs
    {
        public string Information { get; set; }

        public DirectoryEventArgs(string information)
        {
            Information = information;
        }
    }
}