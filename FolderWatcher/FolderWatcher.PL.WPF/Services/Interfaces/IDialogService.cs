namespace FolderWatcher.PL.WPF.Services.Interfaces
{
    public interface IDialogService
    {
        string DirectoryPath { get; set; }
        string FilePath { get; set; }
        bool OpenFileDialog(string filter);
        bool SaveFileDialog(string filter, string default_name = "");
        bool OpenFolder();
    }
}