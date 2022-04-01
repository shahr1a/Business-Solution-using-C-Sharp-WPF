using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Data.SqlClient;
using System.Configuration;

namespace BusinessSolution
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        SqlConnection sqlConnection;
        public Login()
        {
            InitializeComponent();
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["BusinessSolution.Properties.Settings.BusinessSolutionDBv2ConnectionString"].ConnectionString;
                sqlConnection = new SqlConnection(connectionString);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            if (uName.Text == "" && uPass.Password == "")
            {
                loginErrorMessage.Foreground = Brushes.Red;
                loginErrorMessage.Content = "Fields cannot be empty!!!";
            }
            else if (uName.Text == "")
            {
                loginErrorMessage.Foreground = Brushes.Red;
                loginErrorMessage.Content = "Username required!!!!!";
            }
            else if (uPass.Password == "")
            {
                loginErrorMessage.Foreground = Brushes.Red;
                loginErrorMessage.Content = "Password required!!!!";
            }
            else
            {
                loginErrorMessage.Content = "";
                //sqlConnection = new SqlConnection(connectionString);

                try
                {

                    sqlConnection.Open();
                    string query1 = "Select count(*) from [Admin] where AdminLoginId= '" + uName.Text + "' AND AdminLoginPassword= '" + uPass.Password + "' ";
                    string query2 = "Select count(*) from [Employee] where EmployeeLoginId= '" + uName.Text + "' AND EmployeeLoginPassword= '" + uPass.Password + "' ";
                    SqlCommand sqlcmd1 = new SqlCommand(query1, sqlConnection);
                    SqlCommand sqlcmd2 = new SqlCommand(query2, sqlConnection);
                    
                    if (Convert.ToInt32(sqlcmd1.ExecuteScalar()) == 1)
                    {                  
                        
                        string nameQuery = "Select (FirstName + ' ' + LastName) as FullName from [Admin]  where AdminLoginId= '" + uName.Text + "' AND AdminLoginPassword= '" + uPass.Password + "' ";
                        SqlCommand sqlcmd3 = new SqlCommand(nameQuery, sqlConnection);

                        AdminWindow adminWidow = new AdminWindow(sqlcmd3.ExecuteScalar().ToString());
                        adminWidow.Show();
                        Close();
                    }
                    else if (Convert.ToInt32(sqlcmd2.ExecuteScalar()) == 1)
                    {

                        string nameQuery = "Select (FirstName + ' ' + LastName) as FullName from [Employee]  where EmployeeLoginId= '" + uName.Text + "' AND EmployeeLoginPassword= '" + uPass.Password + "' ";
                        SqlCommand sqlcmd3 = new SqlCommand(nameQuery, sqlConnection);

                        EmployeeWindow employeeWindow = new EmployeeWindow(sqlcmd3.ExecuteScalar().ToString());
                        employeeWindow.Show();

                        Close();
                    }
                    else
                    {
                        loginButton.Background = Brushes.Red;
                        loginButton.BorderBrush = Brushes.Red;
                        loginErrorMessage.Content = "Invalid Authenticaiton! Try again!!";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    sqlConnection.Close();
                }
            }

        }

        private void Login_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}
