using MaterialDesignThemes.Wpf;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace BusinessSolution
{
    /// <summary>
    /// Interaction logic for EmployeeWindow.xaml
    /// </summary>
    public partial class EmployeeWindow : Window
    {
        public EmployeeWindow(string name)
        {
            InitializeComponent();

            var menuProductManager = new List<SubItem>();
            menuProductManager.Add(new SubItem("Categories", new Categories()));
            menuProductManager.Add(new SubItem("Add Categories", new AddCategories()));
            menuProductManager.Add(new SubItem("Delete Catergories", new DeleteCategory()));
            menuProductManager.Add(new SubItem("Add Product", new AddProduct()));
            menuProductManager.Add(new SubItem("Inventory", new Inventory()));
            menuProductManager.Add(new SubItem("Delete Product", new RemoveProduct()));
            menuProductManager.Add(new SubItem("Search Product", new Search()));
            var item2 = new ItemMenu("Products", menuProductManager, PackIconKind.CreateOutline);

            ProductManagerMenu.Children.Add(new ProductManagerUserControl(item2, this));

            DataContext = new EmployeeWindowViewModel();

            loggedInBy.Text = name;
        }

        internal void SwitchScreen(object sender)
        {
            var screen = ((UserControl)sender);

            if (StackPanelEmployee_Product.Children.Count == 0 && screen != null)
            {
                StackPanelEmployee_Product.Children.Add(screen);
            }
            else if (StackPanelEmployee_Product.Children.Contains(screen))
            {
                StackPanelEmployee_Product.Children.Clear();
            }
            else if (StackPanelEmployee_Product.Children.Count != 0 && screen != null)
            {
                StackPanelEmployee_Product.Children.Clear();
                StackPanelEmployee_Product.Children.Add(screen);
            }
        }

        private void EmployeeWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void ButtonLogout_Click(object sender, RoutedEventArgs e)
        {

            Login login = new Login();
            login.Show();
            this.Close();
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            ButtonCloseMenu.Visibility = Visibility.Visible;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ProductManager_Click(object sender, RoutedEventArgs e)
        {
            if (ProductManagerPage.Visibility == Visibility.Collapsed)
                ProductManagerPage.Visibility = Visibility.Visible;
            else
                ProductManagerPage.Visibility = Visibility.Collapsed;
        }


        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
