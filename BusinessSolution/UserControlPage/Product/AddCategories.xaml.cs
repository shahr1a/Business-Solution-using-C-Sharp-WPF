using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace BusinessSolution
{
    /// <summary>
    /// Interaction logic for AddCategories.xaml
    /// </summary>
    public partial class AddCategories : UserControl
    {
        string connectionString = ConfigurationManager.ConnectionStrings["BusinessSolution.Properties.Settings.BusinessSolutionDBv2ConnectionString"].ConnectionString;
        ProductManagerQuery productManagerQuery = new ProductManagerQuery();

        public AddCategories()
        {
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            (this.Parent as StackPanel).Children.Remove(this);
            categoryAddResult.Content = "";
            categoryAddResult.Visibility = Visibility.Collapsed;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                categoryAddResult.Content = "";
                if (categoryName.Text == "")
                {
                    if (categoryAddResult.Visibility == Visibility.Collapsed)
                    {
                        categoryAddResult.Visibility = Visibility.Visible;

                        categoryAddResult.Foreground = Brushes.Red;
                        categoryAddResult.Content = "Fields cannot be empty!!";
                    }
                }

                else
                {
                    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                    {
                        sqlConnection.Open();
                        SqlCommand sqlcmd = new SqlCommand(productManagerQuery.CreateCategory(), sqlConnection);
                        sqlcmd.Parameters.AddWithValue("@CategoryType", categoryName.Text.ToString().ToUpper());
                        sqlcmd.ExecuteNonQuery();

                        if (categoryAddResult.Visibility == Visibility.Collapsed)
                        {
                            categoryAddResult.Visibility = Visibility.Visible;
                            categoryAddResult.Foreground = Brushes.Green;
                            categoryAddResult.Content = "Category Added Successfully!!!";
                        }
                        else
                        {
                            categoryAddResult.Foreground = Brushes.Green;
                            categoryAddResult.Content = "Category Added Successfully!!!";
                        }

                        sqlConnection.Close();
                    }
                    categoryName.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
