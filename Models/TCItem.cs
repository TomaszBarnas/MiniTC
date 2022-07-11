using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniTC
{
    internal class TCItem
    {
        public string ItemName { get; set; }
        public bool IsFolder { get; set; }

        public TCItem()
        {
            ItemName = string.Empty;
            IsFolder = false;
        }
         public TCItem(string itemName, bool isFolder)
        {
            ItemName = itemName;
            IsFolder = isFolder;

        }
        public override string ToString()
        {
            if (IsFolder) return $"<D>{ItemName}";
            else return $"{ItemName}";
        }
    }
}
