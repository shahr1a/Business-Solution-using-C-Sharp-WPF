using MaterialDesignThemes.Wpf;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace BusinessSolution
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {

        public AdminWindow(string name)
        {
            InitializeComponent();

            var menuEmployeeManager = new List<SubItem>();
            menuEmployeeManager.Add(new SubItem("Add", new AddEmployee()));
            menuEmployeeManager.Add(new SubItem("Remove", new RemoveEmployee()));
            menuEmployeeManager.Add(new SubItem("Search", new SearchEmployee()));
            //menuEmployeeManager.Add(new SubItem("Edit", new EditEmployee()));
            menuEmployeeManager.Add(new SubItem("Show All", new ShowAllEmployee()));
            var item2 = new ItemMenu("Employee Manager", menuEmployeeManager, PackIconKind.Create);

            EmployeeManagerMenu.Children.Add(new EmployeeManagerUserControl(item2, this));

            DataContext = new AdminWindowViewModel();

            loggedInBy.Text = name;


        }

        internal void SwitchScreen(object sender)
        {
            var screen = ((UserControl)sender);

            if (StackPanelAdmin.Children.Count == 0 && screen != null)
            {
                StackPanelAdmin.Children.Add(screen);
            }
            else if(StackPanelAdmin.Children.Contains(screen))
            {
                StackPanelAdmin.Children.Clear();
            }
            else if(StackPanelAdmin.Children.Count != 0 && screen!=null)
            {
                StackPanelAdmin.Children.Clear();
                StackPanelAdmin.Children.Add(screen);
            }
        }

        private void AdminWindow_MouseDown(object sender, MouseButtonEventArgs e)
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

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            var parentWindow = Window.GetWindow(this);
            parentWindow.Close();
            mainWindow.Show();
        }

    }
}
