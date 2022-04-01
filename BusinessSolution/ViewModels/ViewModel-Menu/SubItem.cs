using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BusinessSolution
{
    public class SubItem
    {
        #region Public Properties

        public string Name { get; private set; }

        public UserControl Screen { get; private set; }

        #endregion

        #region Constructor

        public SubItem(string name, UserControl screen = null)
        {
            Name = name;
            Screen = screen;
        }

        #endregion
    }
}
