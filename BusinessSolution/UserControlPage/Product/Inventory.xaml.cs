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
    /// Interaction logic for Inventory.xaml
    /// </summary>
    public partial class Inventory : UserControl
    {
        ProductManagerQuery productManagerQuery = new ProductManagerQuery();
        string connectionString = ConfigurationManager.ConnectionStrings["BusinessSolution.Properties.Settings.BusinessSolutionDBv2ConnectionString"].ConnectionString;
        DataTable dataTable = new DataTable("Inventory");
        public Inventory()
        {
            InitializeComponent();
        }

        private void ShowAllInventoryButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    if (ProductBrand.Text == "")
                    {
                        SqlDataAdapter sqlData = new SqlDataAdapter(productManagerQuery.GetFullInventory(), sqlConnection);

                        if (DataGridCategory.Visibility == Visibility.Visible)
                        {
                            DataGridCategory.Visibility = Visibility.Collapsed;
                        }

                        else if (dataTable.Rows.Count != 0)
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
                    else
                    {
                        SqlDataAdapter sqlData = new SqlDataAdapter(productManagerQuery.GetBrandInventory(ProductBrand.Text.ToString()), sqlConnection);

                        if (DataGridCategory.Visibility == Visibility.Visible)
                        {
                            DataGridCategory.Visibility = Visibility.Collapsed;
                        }

                        else if (dataTable.Rows.Count != 0)
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
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    DataGridCategory.ItemsSource = null;
                    DataGridCategory.DataContext = null;
                    dataTable.Clear();
                    DataGridCategory.Columns.Clear();
                    DataGridCategory.Items.Clear();
                    if (ProductBrand.Text == "")
                    {
                        SqlDataAdapter sqlData = new SqlDataAdapter(productManagerQuery.GetFullInventory(), sqlConnection);
                        DataGridCategory.ItemsSource = dataTable.DefaultView;
                        DataGridCategory.Items.Refresh();
                        sqlData.Fill(dataTable);
                        sqlData.Update(dataTable);
                        sqlConnection.Close();
                    }
                    else
                    {
                        SqlDataAdapter sqlData = new SqlDataAdapter(productManagerQuery.GetBrandInventory(ProductBrand.Text.ToString()), sqlConnection);
                        DataGridCategory.ItemsSource = dataTable.DefaultView;
                        DataGridCategory.Items.Refresh();
                        sqlData.Fill(dataTable);
                        sqlData.Update(dataTable);
                        DataGridCategory.Items.Refresh();
                        sqlConnection.Close();
                    }

                }
            }
            catch(Exception ex)
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
                DataGridCategory.ItemsSource = null;
                DataGridCategory.DataContext = null;
                dataTable.Clear();
                DataGridCategory.Columns.Clear();
                DataGridCategory.Items.Clear();
                DataGridCategory.Visibility = Visibility.Collapsed;
                (this.Parent as StackPanel).Children.Remove(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
