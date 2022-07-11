using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.IO;
//ciezkie rzeczy z io mają byc w warstwie modelu (przenies i ładnie ponazywaj) model to jest warstwa z dowolną iloscia klas, przyklad klikamy w comboboxa a view wysyla zdarzenie view model to obsluguje bo ma taka funkcjonalnosc, wolamy funkscje funkcja zwraca t w odpowiednim formacie i wstawia do widoku
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Windows;

namespace MiniTC.ViewModels
{
    internal class ViewModel : BaseViewModel
    {
        private ObservableCollection<string> drives;
        public ObservableCollection<string> Drives
        {
            get { return drives; }
            set { drives = value; OnPropertyChanged(nameof(Drives)); }
        }

        private string firstPath;
        public string FirstPath
        {
            get { return firstPath; }
            set { firstPath = value; OnPropertyChanged(nameof(FirstPath)); }
        }

        private string firstDrive;
        public string FirstDrive
        {
            get { return firstDrive; }
            set { firstDrive = value; OnPropertyChanged(nameof(FirstDrive)); }
        }

        private string secondPath;
        public string SecondPath
        {
            get { return secondPath; }
            set { secondPath = value; OnPropertyChanged(nameof(SecondPath)); }
        }

        private string secondDrive;
        public string SecondDrive
        {
            get { return secondDrive; }
            set { secondDrive = value; OnPropertyChanged(nameof(SecondDrive)); }
        }

        private ObservableCollection<ListItemViewModel> firstItemList;
        public ObservableCollection<ListItemViewModel> FirstItemList
        {
            get { return firstItemList; }
            set { firstItemList = value; OnPropertyChanged(nameof(FirstItemList)); }
        }

        private ObservableCollection<ListItemViewModel> secondItemList;
        public ObservableCollection<ListItemViewModel> SecondItemList
        {
            get { return secondItemList; }
            set { secondItemList = value; OnPropertyChanged(nameof(SecondItemList)); }
        }

        private string selectedFirstListItem;
        public string SelectedFirstListItem
        {
            get { return selectedFirstListItem; }
            set { selectedFirstListItem = value; OnPropertyChanged(nameof(SelectedFirstListItem)); }
        }

        private string selectedSecondListItem;
        public string SelectedSecondListItem
        {
            get { return selectedSecondListItem; }
            set { selectedSecondListItem = value; OnPropertyChanged(nameof(SelectedSecondListItem)); }
        }

        private int firstSelectedIndex;
        public int FirstSelectedIndex
        {
            get { return firstSelectedIndex; }
            set { firstSelectedIndex = value; OnPropertyChanged(nameof(FirstSelectedIndex)); }
        }

        private int secondSelectedIndex;
        public int SecondSelectedIndex
        {
            get { return secondSelectedIndex; }
            set { secondSelectedIndex = value; OnPropertyChanged(nameof(SecondSelectedIndex)); }
        }

 

        private RelayCommand copyCommand;
        public RelayCommand CopyCommand
        {
            get
            {
                if (copyCommand == null)
                    copyCommand = new RelayCommand(argument =>
                    {

                        if (firstSelectedIndex != -1)
                        {
                            if (selectedFirstListItem.Contains("<D>"))
                            {
                                string sourcePath = @$"{firstPath}{selectedFirstListItem.Remove(0, 3)+"\\"}";
                                string targetPath = $@"{secondPath}{selectedFirstListItem.Remove(0, 3)+"\\"}";
                                foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
                                {
                                    Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));
                                }

                                foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
                                {
                                    File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
                                }
                            }
                            else if (!selectedFirstListItem.Contains("<D>") && selectedFirstListItem != "..")
                            { if (File.Exists(@$"{secondPath}{selectedFirstListItem}"))
                                    MessageBox.Show("File already exists", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                else File.Copy(@$"{firstPath}{selectedFirstListItem}", @$"{secondPath}{selectedFirstListItem}"); }
                            RefreshSecondList();
                        }
                        if (secondSelectedIndex != -1)
                        {
                            if (selectedSecondListItem.Contains("<D>")) 
                            {
                                string sourcePath = @$"{secondPath}{selectedSecondListItem.Remove(0, 3)}";
                                string targetPath = $@"{firstPath}{selectedSecondListItem.Remove(0, 3)}";
                                foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
                                {
                                    Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));
                                }

                                foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
                                {
                                    File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
                                }
                            }
                            else if (!selectedSecondListItem.Contains("<D>") && selectedSecondListItem != "..")
                            { if (File.Exists(@$"{firstPath}{selectedSecondListItem}"))
                                    MessageBox.Show("File already exists", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                else File.Copy(@$"{secondPath}{selectedSecondListItem}", @$"{firstPath}{selectedSecondListItem}"); }
                            RefreshFirstList();
                        }
                    }, argument => true);
                return copyCommand;
            }
        }

        private RelayCommand changeFirstDriveCommand;
        public RelayCommand ChangeFirstDriveCommand
        {
            get
            {
                if (changeFirstDriveCommand == null)
                    changeFirstDriveCommand = new RelayCommand(argument =>
                     {
                         FirstPath = firstDrive;
                         RefreshFirstList();
                     }, argument => true);
                return changeFirstDriveCommand;
            }
        }

        private RelayCommand changeSecondDriveCommand;
        public RelayCommand ChangeSecondDriveCommand
        {
            get
            {
                if (changeSecondDriveCommand == null)
                    changeSecondDriveCommand = new RelayCommand(argument =>
                    {
                        SecondPath = secondDrive;
                        RefreshSecondList();
                    }, argument => true);
                return changeSecondDriveCommand;
            }
        }

        private RelayCommand firstListDoubleClickCommand;
        public RelayCommand FirstListDoubleClickCommand
        {
            get
            {
                if (firstListDoubleClickCommand == null)
                    firstListDoubleClickCommand = new RelayCommand(argument =>
                    {
                        if (selectedFirstListItem == "..") { FirstPath = Path.GetDirectoryName(firstPath.Remove(firstPath.Length - 1)); RefreshFirstList(); }
                        else if (selectedFirstListItem.Contains("<D>")) { FirstPath = FirstPath + selectedFirstListItem.Remove(0, 3) + "\\"; RefreshFirstList(); }
                    }, argument => true);
                return firstListDoubleClickCommand;
            }
        }

        private RelayCommand secondListDoubleClickCommand;
        public RelayCommand SecondListDoubleClickCommand
        {
            get
            {
                if (secondListDoubleClickCommand == null)
                    secondListDoubleClickCommand = new RelayCommand(argument =>
                    {
                        Trace.WriteLine(selectedFirstListItem + " XD");
                        Trace.WriteLine(firstSelectedIndex + " XD");
                        if (selectedSecondListItem == "..") { SecondPath = Path.GetDirectoryName(secondPath.Remove(secondPath.Length - 1)); RefreshSecondList(); }
                        else if (selectedSecondListItem.Contains("<D>")) { SecondPath = SecondPath + selectedSecondListItem.Remove(0, 3) + "\\"; RefreshSecondList(); }
                    }, argument => true);
                return secondListDoubleClickCommand;
            }
        }

        private RelayCommand firstLostFocusCommand;
        public RelayCommand FirstLostFocusCommand
        {
            get
            {
                if (firstLostFocusCommand == null)
                    firstLostFocusCommand = new RelayCommand(argument =>
                    {
                        if (secondSelectedIndex != -1) FirstSelectedIndex = -1;
                    }, argument => true);
                return firstLostFocusCommand;
            }
        }

        private RelayCommand secondLostFocusCommand;
        public RelayCommand SecondLostFocusCommand
        {
            get
            {
                if (secondLostFocusCommand == null)
                    secondLostFocusCommand = new RelayCommand(argument =>
                    {
                        if (firstSelectedIndex != -1) SecondSelectedIndex = -1;
                    }, argument => true);
                return secondLostFocusCommand;
            }
        }

        private RelayCommand firstGotFocusCommand;
        public RelayCommand FirstGotFocusCommand
        {
            get
            {
                if (firstGotFocusCommand == null)
                    firstGotFocusCommand = new RelayCommand(argument =>
                    {
                        if (secondSelectedIndex != -1) SecondSelectedIndex = -1;
                    }, argument => true);
                return firstGotFocusCommand;
            }
        }

        private RelayCommand secondGotFocusCommand;
        public RelayCommand SecondGotFocusCommand
        {
            get
            {
                if (secondGotFocusCommand == null)
                    secondGotFocusCommand = new RelayCommand(argument =>
                    {
                        if (firstSelectedIndex != -1) FirstSelectedIndex = -1;
                    }, argument => true);
                return secondGotFocusCommand;
            }
        }

        

        public void RefreshFirstList()
        {
            firstItemList.Clear();
            if (!drives.Contains(firstPath)) { firstItemList.Add(new ListItemViewModel("..", false)); }
            foreach (var d in Directory.GetDirectories(firstPath)
                .Select(s => new DirectoryInfo(s))
                .Where(s => s.Attributes.HasFlag(FileAttributes.Directory))
                .Where(s => !s.Attributes.HasFlag(FileAttributes.System))
                .Where(s => !s.Attributes.HasFlag(FileAttributes.Hidden))
                .Select(s => s.FullName).ToArray())
            {
                var dirName = new DirectoryInfo(d).Name;
                firstItemList.Add(new ListItemViewModel(dirName, true));
            }
            foreach (var f in Directory.GetFiles(firstPath)
                .Select(s => new FileInfo(s))
                .Where(s => !s.Attributes.HasFlag(FileAttributes.System))
                .Where(s => !s.Attributes.HasFlag(FileAttributes.Hidden))
                .Select(s => s.FullName).ToArray())
            {
                var fileName = new FileInfo(f).Name;
                firstItemList.Add(new ListItemViewModel(fileName, false));
            }
        }

        public void RefreshSecondList()
        {
            secondItemList.Clear();
            if (!drives.Contains(secondPath)) { secondItemList.Add(new ListItemViewModel("..", false)); }
            foreach (var d in Directory.GetDirectories(secondPath)
                .Select(s => new DirectoryInfo(s))
                .Where(s => s.Attributes.HasFlag(FileAttributes.Directory))
                .Where(s => !s.Attributes.HasFlag(FileAttributes.System))
                .Where(s => !s.Attributes.HasFlag(FileAttributes.Hidden))
                .Select(s => s.FullName).ToArray())
            {
                var dirName = new DirectoryInfo(d).Name;
                secondItemList.Add(new ListItemViewModel(dirName, true));
            }

            foreach (var f in Directory.GetFiles(secondPath)
                .Select(s => new FileInfo(s))
                .Where(s => !s.Attributes.HasFlag(FileAttributes.System))
                .Where(s => !s.Attributes.HasFlag(FileAttributes.Hidden))
                .Select(s => s.FullName).ToArray())
            {
                var fileName = new FileInfo(f).Name;
                secondItemList.Add(new ListItemViewModel(fileName, false));
            }
        }

        //konstruktor

        public ViewModel()
        {
            FirstSelectedIndex = -1;
            SecondSelectedIndex = -1;
            drives = new ObservableCollection<string>();
            firstItemList = new ObservableCollection<ListItemViewModel>();
            secondItemList = new ObservableCollection<ListItemViewModel>();
            foreach (string drive in Directory.GetLogicalDrives()) { drives.Add(drive); }
            firstDrive = drives[0]; secondDrive = drives[1]; firstPath = drives[0]; secondPath = drives[1];
            RefreshFirstList();
            RefreshSecondList();
        }

    }
}
