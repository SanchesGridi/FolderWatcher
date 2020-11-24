using FolderWatcher.PL.WPF.Services.Interfaces;
using System.Windows.Forms;
using OpenFileRes = Microsoft.Win32.OpenFileDialog;
using SaveFileRes = Microsoft.Win32.SaveFileDialog;
using FolderBrowserRes = System.Windows.Forms.FolderBrowserDialog;

namespace FolderWatcher.PL.WPF.Services.Classes
{
    public class DialogService : IDialogService
    {
        public string FilePath { get; set; }
        public string DirectoryPath { get; set; }

        public bool OpenFileDialog(string filter)
        {
            var ofr = new OpenFileRes
            {
                Filter = filter
            };
            if (ofr.ShowDialog() == true)
            {
                FilePath = ofr.FileName;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool SaveFileDialog(string filter, string default_name = "")
        {
            var sfr = new SaveFileRes
            {
                Filter = filter
            };          
            if (!default_name.Equals(""))
            {
                sfr.FileName = default_name;
            }

            if (sfr.ShowDialog() == true)
            {
                FilePath = sfr.FileName;
                return true;
            }
            else
            {
                return false;
            }
        }
    
        public bool OpenFolder()
        {
            using (var fbr = new FolderBrowserRes())
            {
                if (fbr.ShowDialog() == DialogResult.OK)
                {
                    DirectoryPath = fbr.SelectedPath;
                    return true;
                }
                else return false;
            }
        }
    }
}