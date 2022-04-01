using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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
    /// Interaction logic for AddEmployee.xaml
    /// </summary>
    public partial class AddEmployee : UserControl
    {
        string connectionString = ConfigurationManager.ConnectionStrings["BusinessSolution.Properties.Settings.BusinessSolutionDBv2ConnectionString"].ConnectionString;
        EmployeeManagerQuery employeeManagerQuery = new EmployeeManagerQuery();

        public AddEmployee()
        {
            InitializeComponent();

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            (this.Parent as StackPanel).Children.Clear();
            ConfirmationMessage.Visibility = Visibility.Collapsed;
            ClearText();
            ClearErrorIcon();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            if (FirstName.Text == "" || LastName.Text == "" || PhoneNumber.Text == "" || Email.Text == "" || AddressLine.Text == ""
                || City.Text == "" || Country.Text == "" || Password.Password == "" || ConfirmPassword.Password == "")
            {
                if (FirstName.Text == "")
                {
                    if (FirstNameMissing.Visibility == Visibility.Collapsed)
                    {
                        FirstNameMissing.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        FirstNameMissing.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    FirstNameMissing.Visibility = Visibility.Collapsed;
                }

                if (LastName.Text == "")
                {
                    if (LastNameMissing.Visibility == Visibility.Collapsed)
                    {
                        LastNameMissing.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        LastNameMissing.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    LastNameMissing.Visibility = Visibility.Collapsed;
                }

                if (PhoneNumber.Text == "")
                {
                    if (PhoneNumberMissing.Visibility == Visibility.Collapsed)
                    {
                        PhoneNumberMissing.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        PhoneNumberMissing.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    PhoneNumberMissing.Visibility = Visibility.Collapsed;
                }

                if (Email.Text == "")
                {
                    if (EmailMissing.Visibility == Visibility.Collapsed)
                    {
                        EmailMissing.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        EmailMissing.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    EmailMissing.Visibility = Visibility.Collapsed;
                }

                if (AddressLine.Text == "")
                {
                    if (AddressLineMissing.Visibility == Visibility.Collapsed)
                    {
                        AddressLineMissing.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        AddressLineMissing.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    AddressLineMissing.Visibility = Visibility.Collapsed;
                }

                if (City.Text == "")
                {
                    if (CityMissing.Visibility == Visibility.Collapsed)
                    {
                        CityMissing.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        CityMissing.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    CityMissing.Visibility = Visibility.Collapsed;
                }

                if (Country.Text == "")
                {
                    if (CountryMissing.Visibility == Visibility.Collapsed)
                    {
                        CountryMissing.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        CountryMissing.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    CountryMissing.Visibility = Visibility.Collapsed;
                }

                if (Password.Password == "")
                {
                    if (PasswordMissing.Visibility == Visibility.Collapsed)
                    {
                        PasswordMissing.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        PasswordMissing.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    PasswordMissing.Visibility = Visibility.Collapsed;
                }

                if (ConfirmPassword.Password == "")
                {
                    if (ConfirmPasswordMissing.Visibility == Visibility.Collapsed)
                    {
                        ConfirmPasswordMissing.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        ConfirmPasswordMissing.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    ConfirmPasswordMissing.Visibility = Visibility.Collapsed;
                }

            }
            else
            {

                try
                {
                    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                    {
                        sqlConnection.Open();

                        SqlCommand sqlcmd = new SqlCommand(employeeManagerQuery.AddEmployeeQuery(FirstName.Text.ToString().ToUpper(), LastName.Text.ToString().ToUpper(), PhoneNumber.Text.ToString().ToUpper(),
                            Email.Text.ToString().ToUpper(), AddressLine.Text.ToString().ToUpper(), City.Text.ToString().ToUpper(), Country.Text.ToString().ToUpper(), Password.Password.ToString().ToUpper()), sqlConnection);

                        sqlcmd.ExecuteNonQuery();

                        if (ConfirmationMessage.Visibility == Visibility.Collapsed)
                        {
                            ConfirmationMessage.Visibility = Visibility.Visible;
                            ConfirmationMessage.Foreground = Brushes.Green;
                            ConfirmationMessage.Content = "Employee ADDED into the database!!";
                        }
                        else
                        {
                            ConfirmationMessage.Foreground = Brushes.Green;
                            ConfirmationMessage.Content = "Employee ADDED into the database!!.";
                        }
                    }
                    MessageBox.Show("Employee Added!");
                    ClearText();
                    ClearErrorIcon();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void ClearText()
        {
            FirstName.Text = "";
            LastName.Text = "";
            PhoneNumber.Text = "";
            Email.Text = "";
            AddressLine.Text = "";
            City.Text = "";
            Country.Text = "";
            Password.Password = "";
            ConfirmPassword.Password = "";
            ConfirmationMessage.Content = "";



        }

        private void ClearErrorIcon()
        {
            FirstNameMissing.Visibility = Visibility.Collapsed;
            LastNameMissing.Visibility = Visibility.Collapsed;
            PhoneNumberMissing.Visibility = Visibility.Collapsed;
            PasswordMissing.Visibility = Visibility.Collapsed;
            EmailMissing.Visibility = Visibility.Collapsed;
            AddressLineMissing.Visibility = Visibility.Collapsed;
            CityMissing.Visibility = Visibility.Collapsed;
            CountryMissing.Visibility = Visibility.Collapsed;
            ConfirmPasswordMissing.Visibility = Visibility.Collapsed;
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MainWindow mainWindow = new MainWindow();
                var parentWindow = Window.GetWindow(this);
                parentWindow.Close();
                mainWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
