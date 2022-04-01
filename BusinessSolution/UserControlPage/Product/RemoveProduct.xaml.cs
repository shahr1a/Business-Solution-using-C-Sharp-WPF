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
    /// Interaction logic for DeleteProduct.xaml
    /// </summary>
    public partial class RemoveProduct : UserControl
    {
        string connectionString = ConfigurationManager.ConnectionStrings["BusinessSolution.Properties.Settings.BusinessSolutionDBv2ConnectionString"].ConnectionString;
        ProductManagerQuery productManagerQuery = new ProductManagerQuery();
        DataTable dataTable = new DataTable("Products");
        
        public RemoveProduct()
        {
            InitializeComponent();
        }

        private void RemoveProductButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using(SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    if(RemoveProductName.Text != null)
                    {
                        sqlConnection.Open();
                        SqlCommand sqlcmd = new SqlCommand(productManagerQuery.RemoveProduct(RemoveProductName.Text.ToString().ToUpper()), sqlConnection);
                        sqlcmd.ExecuteNonQuery();

                        if (productSearchResult.Visibility == Visibility.Collapsed)
                        {
                            productSearchResult.Visibility = Visibility.Visible;
                            productSearchResult.Content = "Category DELETED Successfully!!!";
                            RemoveProductButton.Visibility = Visibility.Collapsed;
                        }
                        else
                        {
                            productSearchResult.Content = "Category DELETED Successfully!!!";
                            RemoveProductButton.Visibility = Visibility.Collapsed;
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

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                (this.Parent as StackPanel).Children.Remove(this);
                productSearchResult.Content = "";
                RemoveProductName.Text = "";
                DataGridCategory.Visibility = Visibility.Collapsed;
                RemoveProductButton.Visibility = Visibility.Collapsed;
                productSearchResult.Visibility = Visibility.Collapsed;
                SearchProductButton.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SearchProductButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                productSearchResult.Content = "";

                if (RemoveProductName.Text == "")
                {
                    productSearchResult.Foreground = Brushes.Red;
                    if (productSearchResult.Visibility == Visibility.Collapsed)
                    {
                        productSearchResult.Visibility = Visibility.Visible;

                        productSearchResult.Content = "Fields cannot be empty!!";
                    }
                    else
                    {
                        productSearchResult.Content = "Fields cannot be empty!!";
                    }
                }

                else
                {
                    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                    {
                        productSearchResult.Content = "";
                        sqlConnection.Open();
                        SqlCommand sqlcmdSearch = new SqlCommand(productManagerQuery.SearchProduct(RemoveProductName.Text.ToString().ToUpper()), sqlConnection);
                    
                        productSearchResult.Foreground = Brushes.Green;
                    
                        sqlcmdSearch.ExecuteNonQuery();

                        if (Convert.ToInt32(sqlcmdSearch.ExecuteScalar()) > 0)
                        {
                            if (productSearchResult.Visibility == Visibility.Collapsed)
                            {
                                productSearchResult.Visibility = Visibility.Visible;
                                productSearchResult.Content = "Product FOUNDED Successfully!!!";
                                SearchProductButton.Visibility = Visibility.Collapsed;
                                RemoveProductButton.Visibility = Visibility.Visible;
                            }
                            else
                            {
                                productSearchResult.Content = "Product FOUNDED Successfully!!!";
                                SearchProductButton.Visibility = Visibility.Collapsed;
                                RemoveProductButton.Visibility = Visibility.Visible;
                            }
                            sqlConnection.Close();
                        }
                        else
                        {
                            if (productSearchResult.Visibility == Visibility.Collapsed)
                            {
                                productSearchResult.Visibility = Visibility.Visible;
                                productSearchResult.Content = "Product NOT FOUNDED!!!";
                                RemoveProductButton.Visibility = Visibility.Collapsed;
                            }
                            else
                            {
                                productSearchResult.Content = "Product NOT FOUNDED!!!";
                                RemoveProductButton.Visibility = Visibility.Collapsed;
                            }
                        }

                    }

                    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                    {
                        sqlConnection.Open();
                        SqlDataAdapter sqlData = new SqlDataAdapter(productManagerQuery.GetSpecificProductDetails(RemoveProductName.Text.ToString()), sqlConnection);

                        if (DataGridCategory.Visibility == Visibility.Visible)
                        {
                            DataGridCategory.Visibility = Visibility.Collapsed;
                        }
                        else if (dataTable.Rows.Count != 0)
                        {
                            dataTable.Clear();

                            sqlData.Fill(dataTable);
                            DataGridCategory.ItemsSource = dataTable.DefaultView;
                            sqlData.Update(dataTable);

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
    }
}
