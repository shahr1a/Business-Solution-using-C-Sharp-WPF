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
    /// Interaction logic for Search.xaml
    /// </summary>
    public partial class Search : UserControl
    {
        string connectionString = ConfigurationManager.ConnectionStrings["BusinessSolution.Properties.Settings.BusinessSolutionDBv2ConnectionString"].ConnectionString;
        ProductManagerQuery productManagerQuery = new ProductManagerQuery();
        DataTable dataTable = new DataTable("Products");

        public Search()
        {
            InitializeComponent();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                productSearchResult.Content = "";

                if (SearchProductName.Text == "")
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
                        SqlCommand sqlcmdSearch = new SqlCommand(productManagerQuery.SearchProduct(SearchProductName.Text.ToString()), sqlConnection);

                        productSearchResult.Foreground = Brushes.Green;

                        sqlcmdSearch.ExecuteNonQuery();

                        if (Convert.ToInt32(sqlcmdSearch.ExecuteScalar()) > 0)
                        {
                            SqlDataAdapter sqlData = new SqlDataAdapter(productManagerQuery.GetSpecificProductDetails(SearchProductName.Text.ToString()), sqlConnection);

                            if (DataGridProduct.Visibility == Visibility.Visible)
                            {
                                DataGridProduct.Visibility = Visibility.Collapsed;
                            }
                            else if (dataTable.Rows.Count != 0)
                            {
                                dataTable.Clear();

                                sqlData.Fill(dataTable);
                                DataGridProduct.ItemsSource = dataTable.DefaultView;
                                sqlData.Update(dataTable);

                                if (DataGridProduct.Visibility == Visibility.Collapsed)
                                {
                                    DataGridProduct.Visibility = Visibility.Visible;
                                }
                                else
                                    DataGridProduct.Visibility = Visibility.Collapsed;
                            }
                            else
                            {
                                sqlData.Fill(dataTable);
                                DataGridProduct.ItemsSource = dataTable.DefaultView;
                                sqlData.Update(dataTable);

                                if (DataGridProduct.Visibility == Visibility.Collapsed)
                                {
                                    DataGridProduct.Visibility = Visibility.Visible;
                                }
                                sqlConnection.Close();
                            }
                        }

                        else
                        {
                            if (productSearchResult.Visibility == Visibility.Collapsed)
                            {
                                productSearchResult.Visibility = Visibility.Visible;
                                productSearchResult.Content = "Product NOT FOUNDED Successfully!!!";
                           
                            }
                            else
                            {
                                productSearchResult.Content = "Product NOT FOUNDED Successfully!!!";
                            }
                        }

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
                DataGridProduct.Visibility = Visibility.Collapsed;
                productSearchResult.Content = "";
                productSearchResult.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
