using System.Windows;

namespace FolderWatcher.PL.WPF.Services.Interfaces
{
    public interface IMessageBoxService
    {
        MessageBoxResult Go(string text);
        MessageBoxResult Go(string text, string caption);
        MessageBoxResult Go(string text, string caption, MessageBoxButton messageBoxButtons, MessageBoxImage messageBoxImage);
        MessageBoxResult Go(string text, string caption, MessageBoxButton messageBoxButtons, MessageBoxImage messageBoxImage, MessageBoxResult defaultResult);
    }
}