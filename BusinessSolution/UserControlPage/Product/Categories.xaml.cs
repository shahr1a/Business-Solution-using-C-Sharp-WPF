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
    public partial class Categories : UserControl
    {
        ShowAllCategory showAllCategory = new ShowAllCategory();
        string connectionString = ConfigurationManager.ConnectionStrings["BusinessSolution.Properties.Settings.BusinessSolutionDBv2ConnectionString"].ConnectionString;
        DataTable dataTable = new DataTable("Categories");
        public Categories()
        {
            InitializeComponent();

        }

        private void ShowAllCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlDataAdapter sqlData = new SqlDataAdapter(showAllCategory.ShowAllCategoryMethod(), sqlConnection);
                    sqlConnection.Open();
                    if (DataGridCategory.Visibility == Visibility.Visible)
                    {
                        DataGridCategory.Visibility = Visibility.Collapsed;
                    }
                    else if(dataTable.Rows.Count != 0)
                    {
                        if (DataGridCategory.Visibility == Visibility.Collapsed)
                        {
                            DataGridCategory.Visibility = Visibility.Visible;
                        }
                        else
                            DataGridCategory.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        sqlData.Fill(dataTable);
                        DataGridCategory.ItemsSource = dataTable.DefaultView;
                        sqlData.Update(dataTable);

                        if (DataGridCategory.Visibility == Visibility.Collapsed)
                        {
                            DataGridCategory.Visibility = Visibility.Visible;
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
            try
            {
                (this.Parent as StackPanel).Children.Remove(this);
                DataGridCategory.Visibility = Visibility.Collapsed;
                dataTable.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using(SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    dataTable.Clear();
                    SqlDataAdapter sqlData = new SqlDataAdapter(showAllCategory.ShowAllCategoryMethod(), sqlConnection);
                    DataGridCategory.ItemsSource = dataTable.DefaultView;
                    sqlData.Fill(dataTable);
                    sqlData.Update(dataTable);
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
