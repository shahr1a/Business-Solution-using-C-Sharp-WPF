using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BusinessSolution
{
    public class ItemMenu
    {
        public ItemMenu(string header, List<SubItem> subItems, PackIconKind icon)
        {
            Header = header;
            SubItems = subItems;
            Icon = icon;
        }

        #region Public Properties

        public string Header { get; private set; }

        public PackIconKind Icon { get; private set; }

        public List<SubItem> SubItems { get; private set; }

        public UserControl Screen { get; private set; }
        #endregion
    }
}
