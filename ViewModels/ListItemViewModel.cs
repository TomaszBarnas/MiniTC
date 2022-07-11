using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace MiniTC
{
    public class ListItemViewModel : BaseViewModel
    {
        private TCItem mItem;

        public string ItemName
        {
            get { return mItem.ItemName; }
            set { mItem.ItemName = value; OnPropertyChanged(nameof(ItemName)); }
        }

        public bool IsFolder
        {
            get { return mItem.IsFolder; }
            set { mItem.IsFolder = value; OnPropertyChanged(nameof(IsFolder)); }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set { description = value; OnPropertyChanged(nameof(Description)); }
        }

        public override string ToString()
        {
            return mItem.ToString();
        }

        public ListItemViewModel()
        {
            mItem = new TCItem();
            description = this.ToString();
        }

        public ListItemViewModel(string itemName, bool isFolder)
        {
            mItem = new TCItem(itemName, isFolder);
            description = this.ToString();
        }
    }
}
