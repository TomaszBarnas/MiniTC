using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Diagnostics;

namespace MiniTC
{
    internal class TCList
    {
        public ObservableCollection<string> Drives { get; set; }

        public void Copy(string FirstPath, string SecondPath, string SelectedListItem, ObservableCollection<ListItemViewModel> ItemList)
        {
            if (SelectedListItem.Contains("<D>"))
            {
                string sourcePath = @$"{FirstPath}{SelectedListItem.Remove(0, 3)}";
                string targetPath = $@"{SecondPath}{SelectedListItem.Remove(0, 3)}";
                foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
                {
                    Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));
                }

                foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
                {
                    File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
                }
            }
            else if (!SelectedListItem.Contains("<D>") && SelectedListItem != "..")
            {
                if (File.Exists(@$"{SecondPath}{SelectedListItem}"))
                    MessageBox.Show("File already exists", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                else File.Copy(@$"{FirstPath}{SelectedListItem}", @$"{SecondPath}{SelectedListItem}");
            }
            RefreshList(SecondPath, ItemList);
        }

        public string PleaseGetMeADirectoryName(string oldpath)
        {
            string newPath = Path.GetDirectoryName(oldpath.Remove(oldpath.Length - 1));
            return newPath;
        }

        public void RefreshList(string path, ObservableCollection<ListItemViewModel> ItemList)
        {
            ItemList.Clear();
            if (!Drives.Contains(path)) { ItemList.Add(new ListItemViewModel("..", false)); }
            foreach (var d in Directory.GetDirectories(path)
                .Select(s => new DirectoryInfo(s))
                .Where(s => s.Attributes.HasFlag(FileAttributes.Directory))
                .Where(s => !s.Attributes.HasFlag(FileAttributes.System))
                .Where(s => !s.Attributes.HasFlag(FileAttributes.Hidden))
                .Select(s => s.FullName).ToArray())
            {
                var dirName = new DirectoryInfo(d).Name;
                ItemList.Add(new ListItemViewModel(dirName, true));
            }

            foreach (var f in Directory.GetFiles(path)
                .Select(s => new FileInfo(s))
                .Where(s => !s.Attributes.HasFlag(FileAttributes.System))
                .Where(s => !s.Attributes.HasFlag(FileAttributes.Hidden))
                .Select(s => s.FullName).ToArray())
            {
                var fileName = new FileInfo(f).Name;
                ItemList.Add(new ListItemViewModel(fileName, false));
            }
        }

        public TCList()
        {
            Drives = new ObservableCollection<string>();
            foreach (string drive in Directory.GetLogicalDrives()) { Drives.Add(drive); }
        }
    }
}
