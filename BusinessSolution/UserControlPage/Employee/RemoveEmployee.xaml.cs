using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BusinessSolution
{
    /// <summary>
    /// Interaction logic for RemoveEmployee.xaml
    /// </summary>
    public partial class RemoveEmployee : UserControl
    {
        string connectionString = ConfigurationManager.ConnectionStrings["BusinessSolution.Properties.Settings.BusinessSolutionDBv2ConnectionString"].ConnectionString;
        EmployeeManagerQuery employeeManagerQuery = new EmployeeManagerQuery();

        public RemoveEmployee()
        {
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            (this.Parent as StackPanel).Children.Remove(this);
            ClearText();
            SearchEmployeeButton.Visibility = Visibility.Visible;
            DeleteEmployeeButton.Visibility = Visibility.Collapsed;
            DataGridEmployee.Visibility = Visibility.Collapsed;
        }

        private void DeleteEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            if(EmployeeLoginId.Text == "")
            {
                if (EmployeeLoginIdMissing.Visibility == Visibility.Collapsed)
                {
                    EmployeeLoginIdMissing.Visibility = Visibility.Visible;
                }
                else
                {
                    EmployeeLoginIdMissing.Visibility = Visibility.Visible;
                }
            }
            else
            {
                EmployeeLoginIdMissing.Visibility = Visibility.Collapsed;
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();

                    SqlCommand sqlcmd = new SqlCommand(employeeManagerQuery.RemoveEmployeeQuery(Int32.Parse(EmployeeLoginId.Text.ToString())), sqlConnection);

                    sqlcmd.ExecuteNonQuery();

                    if (ConfirmationMessage.Visibility == Visibility.Collapsed)
                    {
                        ConfirmationMessage.Visibility = Visibility.Visible;
                        ConfirmationMessage.Foreground = Brushes.Green;
                        ConfirmationMessage.Content = "Employee Removed from the Database!!";
                    }
                    else
                    {
                        ConfirmationMessage.Foreground = Brushes.Green;
                        ConfirmationMessage.Content = "Employee Removed from the Database!!";
                    }

                    if(SearchEmployeeButton.Visibility == Visibility.Collapsed)
                    {
                        SearchEmployeeButton.Visibility = Visibility.Visible;
                        DeleteEmployeeButton.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        DeleteEmployeeButton.Visibility = Visibility.Collapsed;
                    }

                }

            }
        }

        private void SearchEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlDataAdapter sqlData = new SqlDataAdapter(employeeManagerQuery.SearchEmployeeForDeletingQuery(Int32.Parse(EmployeeLoginId.Text.ToString())), sqlConnection);
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
                            ConfirmationMessage.Content = "Employee NOT FOUND!!";
                            ConfirmationMessage.Visibility = Visibility.Visible;
                            //MessageBox.Show("Check Case!");
                        }
                        else
                        {
                            if (DataGridEmployee.Visibility == Visibility.Collapsed)
                            {
                                DataGridEmployee.Visibility = Visibility.Visible;
                            }
                            sqlConnection.Close();

                            if (DeleteEmployeeButton.Visibility == Visibility.Collapsed)
                            {
                                SearchEmployeeButton.Visibility = Visibility.Collapsed;
                                DeleteEmployeeButton.Visibility = Visibility.Visible;
                            }
                            else
                            {
                                SearchEmployeeButton.Visibility = Visibility.Collapsed;
                            }
                        }

                    }
                }
            }
            //catch(InvalidOperationException)
            //{
            //    MessageBox.Show("Sql.Open() Method can not be called!");
            //}
            //catch(SqlException)
            //{
            //    MessageBox.Show("Sql.Open() can not be openned!");
            //}
            //catch(ConfigurationErrorsException)
            //{
            //    MessageBox.Show("Configuration Error Occured!");
            //}
            //catch(ArgumentNullException)
            //{
            //    MessageBox.Show("No table found!");
            //}
            ////catch(SystemException)
            ////{
            ////    MessageBox.Show("System Exception Occured!");
            ////}
            //catch(DBConcurrencyException)
            //{
            //    MessageBox.Show("No row is effected error occured!");
            //}
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ClearText()
        {
            EmployeeLoginId.Text = "";
            ConfirmationMessage.Content = "";
            ConfirmationMessage.Visibility = Visibility.Collapsed;
        }
    }
}


