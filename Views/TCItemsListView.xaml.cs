using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MiniTC.ViewModels;

namespace MiniTC.Views
{
    /// <summary>
    /// Logika interakcji dla klasy TCItemsListView.xaml
    /// </summary>
    public partial class TCItemsListView : UserControl
    {
        public TCItemsListView()
        {
            InitializeComponent();
        }

        // ---------------------------------------------------------------------

        public static readonly DependencyProperty DoubleClickCommandProperty =
        DependencyProperty.Register(
             nameof(DoubleClickCommand),
             typeof(RelayCommand),
             typeof(TCItemsListView)
         );

        public RelayCommand DoubleClickCommand
        {

            get { return (RelayCommand)GetValue(DoubleClickCommandProperty); }
            set { SetValue(DoubleClickCommandProperty, value); }
        }

        // ---------------------------------------------------------------------

        public static readonly DependencyProperty LostFocusCommandProperty =
        DependencyProperty.Register(
             nameof(LostFocusCommand),
             typeof(RelayCommand),
             typeof(TCItemsListView)
         );

        public RelayCommand LostFocusCommand
        {

            get { return (RelayCommand)GetValue(LostFocusCommandProperty); }
            set { SetValue(LostFocusCommandProperty, value); }
        }

        // ---------------------------------------------------------------------

        public static readonly DependencyProperty GotFocusCommandProperty =
        DependencyProperty.Register(
             nameof(GotFocusCommand),
             typeof(RelayCommand),
             typeof(TCItemsListView)
         );

        public RelayCommand GotFocusCommand
        {

            get { return (RelayCommand)GetValue(GotFocusCommandProperty); }
            set { SetValue(GotFocusCommandProperty, value); }
        }

        // ---------------------------------------------------------------------

        public static readonly DependencyProperty TCItemListProperty =
        DependencyProperty.Register(
             nameof(TCItemList),
             typeof(ObservableCollection<ListItemViewModel>),
             typeof(TCItemsListView)
         );

        public ObservableCollection<ListItemViewModel> TCItemList
        {

            get { return (ObservableCollection<ListItemViewModel>)GetValue(TCItemListProperty); }
            set { SetValue(TCItemListProperty, value); }
        }

        // ---------------------------------------------------------------------

        public static readonly DependencyProperty DriveProperty =
        DependencyProperty.Register(
            nameof(Drive),
            typeof(string),
            typeof(TCItemsListView)
        );

        public string Drive
        {

            get { return (string)GetValue(DriveProperty); }
            set { SetValue(DriveProperty, value); }
        }

        // ---------------------------------------------------------------------

        public static readonly DependencyProperty ChangeDriveCommandProperty =
        DependencyProperty.Register(
            nameof(ChangeDriveCommand),
            typeof(RelayCommand),
            typeof(TCItemsListView)
        );

        public RelayCommand ChangeDriveCommand
        {

            get { return (RelayCommand)GetValue(ChangeDriveCommandProperty); }
            set { SetValue(ChangeDriveCommandProperty, value); }
        }

        // ---------------------------------------------------------------------

        public static readonly DependencyProperty TCPathProperty =
        DependencyProperty.Register(
            nameof(TCPath),
            typeof(string),
            typeof(TCItemsListView)
        );

        public string TCPath
        {

            get { return (string)GetValue(TCPathProperty); }
            set { SetValue(TCPathProperty, value); }
        }

        // ---------------------------------------------------------------------

        public static readonly DependencyProperty SelectedListItemProperty =
        DependencyProperty.Register(
            nameof(SelectedListItem),
            typeof(string),
            typeof(TCItemsListView)
        );

        public string SelectedListItem
        {

            get { return (string)GetValue(SelectedListItemProperty); }
            set { SetValue(SelectedListItemProperty, value); }
        }

        // ---------------------------------------------------------------------

        public static readonly DependencyProperty SelectedListIndexProperty =
        DependencyProperty.Register(
            nameof(SelectedListIndex),
            typeof(int),
            typeof(TCItemsListView)
        );

        public int SelectedListIndex
        {

            get { return (int)GetValue(SelectedListIndexProperty); }
            set { SetValue(SelectedListIndexProperty, value); }
        }
    }
}
