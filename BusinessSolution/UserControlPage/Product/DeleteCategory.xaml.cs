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
    /// Interaction logic for DeleteCategory.xaml
    /// </summary>
    public partial class DeleteCategory : UserControl
    {
        string connectionString = ConfigurationManager.ConnectionStrings["BusinessSolution.Properties.Settings.BusinessSolutionDBv2ConnectionString"].ConnectionString;
        ProductManagerQuery productManagerQuery = new ProductManagerQuery();
        ShowAllCategory showAllCategory = new ShowAllCategory();
        DataTable dataTable = new DataTable("Categories");

        public DeleteCategory()
        {
            InitializeComponent();
        }

        private void DeleteCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            using(SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlcmdDelete = new SqlCommand(productManagerQuery.DeleteCategory(), sqlConnection);
                sqlcmdDelete.Parameters.AddWithValue("@CategoryType", categoryName.Text);
                sqlcmdDelete.ExecuteNonQuery();


                if (categorySearchResult.Visibility == Visibility.Collapsed)
                {
                    categorySearchResult.Visibility = Visibility.Visible;
                    categorySearchResult.Content = "Category DELETED Successfully!!!";
                    DeleteCategoryButton.Visibility = Visibility.Visible;
                }
                else
                {
                    categorySearchResult.Content = "Category DELETED Successfully!!!";
                    DeleteCategoryButton.Visibility = Visibility.Visible;
                }
                sqlConnection.Close();
            }

        }


        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            (this.Parent as StackPanel).Children.Remove(this);
            categorySearchResult.Content = "";
            categorySearchResult.Visibility = Visibility.Collapsed;
            DeleteCategoryButton.Visibility = Visibility.Collapsed;
            SearchCategoryButton.Visibility = Visibility.Visible;
            dataTable.Clear();
            DataGridCategory.Visibility = Visibility.Collapsed;

        }

        private void SearchCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            categorySearchResult.Content = "";

            if(categoryName.Text == "")
            {
                categorySearchResult.Foreground = Brushes.Red;
                if (categorySearchResult.Visibility == Visibility.Collapsed)
                {
                    categorySearchResult.Visibility = Visibility.Visible;
                    
                    categorySearchResult.Content = "Fields cannot be empty!!";
                }
                else
                {
                    categorySearchResult.Content = "Fields cannot be empty!!";
                }
            }

            else
            {
                try
                {
                    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                    {
                        categorySearchResult.Content = "";
                        sqlConnection.Open();
                        SqlCommand sqlcmdSearch = new SqlCommand(productManagerQuery.SearchCategory(), sqlConnection);
                        sqlcmdSearch.Parameters.AddWithValue("@CategoryType", categoryName.Text.ToUpper());
                        categorySearchResult.Foreground = Brushes.Green;
                        sqlcmdSearch.ExecuteNonQuery();

                        if (Convert.ToInt32(sqlcmdSearch.ExecuteScalar()) > 0)
                        {
                            if (categorySearchResult.Visibility == Visibility.Collapsed)
                            {
                                categorySearchResult.Visibility = Visibility.Visible;
                                categorySearchResult.Content = "Category FOUNDED Successfully!!!";
                                DeleteCategoryButton.Visibility = Visibility.Visible;
                            }
                            else
                            {
                                categorySearchResult.Content = "Category FOUNDED Successfully!!!";
                                DeleteCategoryButton.Visibility = Visibility.Visible;
                            }
                            sqlConnection.Close();
                        }
                        else
                        {
                            if (categorySearchResult.Visibility == Visibility.Collapsed)
                            {
                                categorySearchResult.Visibility = Visibility.Visible;
                                categorySearchResult.Content = "Category NOT FOUNDED Successfully!!!";
                                DeleteCategoryButton.Visibility = Visibility.Visible;
                            }
                            else
                            {
                                categorySearchResult.Content = "Category NOT FOUNDED Successfully!!!";
                                DeleteCategoryButton.Visibility = Visibility.Visible;
                            }
                        }

                    }                    
                    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                    {
                        sqlConnection.Open();
                        SqlDataAdapter sqlData = new SqlDataAdapter(productManagerQuery.GetSpecificCategory() + categoryName.Text + "'", sqlConnection);
                    
                    
                    
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
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
