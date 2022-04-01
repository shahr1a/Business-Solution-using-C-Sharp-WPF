using System;
using System.Collections.Generic;
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

namespace BusinessSolution
{
    /// <summary>
    /// Interaction logic for ProductManagerUserControl.xaml
    /// </summary>
    public partial class ProductManagerUserControl : UserControl
    {
        EmployeeWindow _context;

        public ProductManagerUserControl(ItemMenu itemMenu, EmployeeWindow context)
        {
            InitializeComponent();

            _context = context;

            ExpanderMenu.Visibility = itemMenu.SubItems == null ? Visibility.Collapsed : Visibility.Visible;
            ListViewItemMenu.Visibility = itemMenu.SubItems == null ? Visibility.Visible : Visibility.Collapsed;

            this.DataContext = itemMenu;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _context.SwitchScreen(((SubItem)((Button)sender).DataContext).Screen);
        }
    }
}
