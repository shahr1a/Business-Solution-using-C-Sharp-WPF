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
    /// Interaction logic for EmployeeManagerUserControlPage.xaml
    /// </summary>
    public partial class EmployeeManagerUserControlPage : UserControl
    {
        public EmployeeManagerUserControlPage()
        {
            InitializeComponent();

            DataContext = new EmployeeManagerViewModel();
        }

        private void AddEmployeeButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
