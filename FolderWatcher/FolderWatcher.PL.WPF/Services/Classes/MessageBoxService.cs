﻿using System.Windows;
using FolderWatcher.PL.WPF.Services.Interfaces;

namespace FolderWatcher.PL.WPF.Services.Classes
{
    public class MessageBoxService : IMessageBoxService
    {
        public MessageBoxResult Go(string text)
        {
            return MessageBox.Show(text);
        }
        public MessageBoxResult Go(string text, string caption)
        {
            return MessageBox.Show(text, caption);
        }
        public MessageBoxResult Go(string text, string caption, MessageBoxButton messageBoxButtons, MessageBoxImage messageBoxImage)
        {
            return MessageBox.Show(text, caption, messageBoxButtons, messageBoxImage);
        }
        public MessageBoxResult Go(string text, string caption, MessageBoxButton messageBoxButtons, MessageBoxImage messageBoxImage, MessageBoxResult defaultResult)
        {
            return MessageBox.Show(text, caption, messageBoxButtons, messageBoxImage, defaultResult);
        }
    }
}