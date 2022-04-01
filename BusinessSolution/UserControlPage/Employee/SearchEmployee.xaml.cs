using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
    /// Interaction logic for SearchEmployee.xaml
    /// </summary>
    public partial class SearchEmployee : UserControl
    {
        string connectionString = ConfigurationManager.ConnectionStrings["BusinessSolution.Properties.Settings.BusinessSolutionDBv2ConnectionString"].ConnectionString;
        EmployeeManagerQuery employeeManagerQuery = new EmployeeManagerQuery();

        public SearchEmployee()
        {
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            (this.Parent as StackPanel).Children.Remove(this);
            ClearText();
        }

        private void SearchEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlDataAdapter sqlData = new SqlDataAdapter(employeeManagerQuery.SearchEmployee(FirstName.Text.ToString().ToUpper(), LastName.Text.ToString().ToUpper()), sqlConnection);
                    DataTable dataTable = new DataTable("Employee Information");
                    sqlConnection.Open();
                    if (DataGridEmployee.Visibility == Visibility.Visible)
                    {
                        DataGridEmployee.Visibility = Visibility.Collapsed;
                    }
                    else if (dataTable.Rows.Count != 0)
                    {
                        if (DataGridEmployee.Visibility == Visibility.Collapsed)
                        {
                            DataGridEmployee.Visibility = Visibility.Visible;
                        }
                        else
                            DataGridEmployee.Visibility = Visibility.Collapsed;
                    }
                    else
                    {

                        sqlData.Fill(dataTable);
                        DataGridEmployee.ItemsSource = dataTable.DefaultView;
                        sqlData.Update(dataTable);

                        if (dataTable.Rows.Count == 0)
                        {
                            ConfirmationMessage.Foreground = Brushes.Red;
                            ConfirmationMessage.Content = "Employee NOT FOUND!!!!";
                            ConfirmationMessage.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            ConfirmationMessage.Visibility = Visibility.Collapsed;
                            if (DataGridEmployee.Visibility == Visibility.Collapsed)
                            {
                                DataGridEmployee.Visibility = Visibility.Visible;
                            }
                            sqlConnection.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
             
        }


        public void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            (this.Parent as StackPanel).Children.Remove(this);
            ClearText();
        }

        private void ClearText()
        {
            try
            {
                FirstName.Text = "";
                LastName.Text = "";
                ConfirmationMessage.Visibility = Visibility.Collapsed;
                DataGridEmployee.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
    
}
