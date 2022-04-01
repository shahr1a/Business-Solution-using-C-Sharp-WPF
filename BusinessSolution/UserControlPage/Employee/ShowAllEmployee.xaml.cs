using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;


namespace BusinessSolution
{
    /// <summary>
    /// Interaction logic for Categories.xaml
    /// </summary>
    public partial class ShowAllEmployee : UserControl
    {
        EmployeeManagerQuery employeeManagerQuery = new EmployeeManagerQuery();
        string connectionString = ConfigurationManager.ConnectionStrings["BusinessSolution.Properties.Settings.BusinessSolutionDBv2ConnectionString"].ConnectionString;
        DataTable dataTable = new DataTable("Employees");
        public ShowAllEmployee()
        {
            InitializeComponent();
        }

        private void ShowAllEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlDataAdapter sqlData = new SqlDataAdapter(employeeManagerQuery.ShowAllEmployee(), sqlConnection);
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

                        if (DataGridEmployee.Visibility == Visibility.Collapsed)
                        {
                            DataGridEmployee.Visibility = Visibility.Visible;
                        }
                        sqlConnection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void ExportExcelButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            (this.Parent as StackPanel).Children.Remove(this);

            DataGridEmployee.Visibility = Visibility.Collapsed;
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                dataTable.Clear();
                SqlDataAdapter sqlData = new SqlDataAdapter(employeeManagerQuery.ShowAllEmployee(), sqlConnection);
                DataGridEmployee.ItemsSource = dataTable.DefaultView;
                sqlData.Fill(dataTable);
                sqlData.Update(dataTable);
                sqlConnection.Close();
            }
        }
    }
}
